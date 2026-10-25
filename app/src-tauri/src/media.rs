use crate::types::{MediaEvent, MediaState};
use base64::Engine;
use std::sync::atomic::{AtomicBool, Ordering};
use std::sync::{Arc, Mutex};
use tauri::{AppHandle, Emitter};
use windows::Foundation::TypedEventHandler;
use windows::Media::Control::{
    CurrentSessionChangedEventArgs, GlobalSystemMediaTransportControlsSession,
    GlobalSystemMediaTransportControlsSessionManager,
    GlobalSystemMediaTransportControlsSessionPlaybackStatus,
};
use windows::Storage::Streams::{DataReader, InputStreamOptions};

pub struct MediaService {
    manager: Option<GlobalSystemMediaTransportControlsSessionManager>,
    current_session: Arc<Mutex<Option<GlobalSystemMediaTransportControlsSession>>>,
    last_track_key: Arc<Mutex<String>>,
    cached_thumbnail: Arc<Mutex<Option<String>>>,
}

impl MediaService {
    pub async fn new() -> Self {
        let manager_res = GlobalSystemMediaTransportControlsSessionManager::RequestAsync();
        let manager = if let Ok(op) = manager_res {
            op.get().ok()
        } else {
            None
        };

        Self {
            manager,
            current_session: Arc::new(Mutex::new(None)),
            last_track_key: Arc::new(Mutex::new(String::new())),
            cached_thumbnail: Arc::new(Mutex::new(None)),
        }
    }

    pub fn start_tracking(&self, app: AppHandle) {
        let manager = match &self.manager {
            Some(m) => m,
            None => return,
        };

        let current_session_arc = self.current_session.clone();
        let last_track_arc = self.last_track_key.clone();
        let cache_thumb_arc = self.cached_thumbnail.clone();
        let app_clone = app.clone();

        // Obtener la mejor sesión inicial
        let best = Self::select_best_session(manager);
        if let Some(ref sess) = best {
            *current_session_arc.lock().unwrap() = Some(sess.clone());
            let sess_clone = sess.clone();
            let app_c = app.clone();
            let last_c = last_track_arc.clone();
            let thumb_c = cache_thumb_arc.clone();
            tokio::spawn(async move {
                Self::refresh_session(&sess_clone, &app_c, &last_c, &thumb_c).await;
            });
        }

        // Manejar cambio de sesión activa
        let app_for_event = app_clone.clone();
        let sess_for_event = current_session_arc.clone();
        let last_for_event = last_track_arc.clone();
        let thumb_for_event = cache_thumb_arc.clone();

        let handler = TypedEventHandler::<GlobalSystemMediaTransportControlsSessionManager, CurrentSessionChangedEventArgs>::new(
            move |mgr: windows::core::Ref<'_, GlobalSystemMediaTransportControlsSessionManager>, _| {
                if let Some(m) = mgr.as_ref() {
                    let best = Self::select_best_session(m);
                    *sess_for_event.lock().unwrap() = best.clone();

                    if let Some(ref sess) = best {
                        let s = sess.clone();
                        let a = app_for_event.clone();
                        let l = last_for_event.clone();
                        let t = thumb_for_event.clone();
                        tokio::spawn(async move {
                            Self::refresh_session(&s, &a, &l, &t).await;
                        });
                    } else {
                        let _ = app_for_event.emit("ln:media", MediaEvent {
                            media: None,
                            track_changed: true,
                        });
                    }
                }
                Ok(())
            },
        );

        let _ = manager.CurrentSessionChanged(&handler);
    }

    fn select_best_session(
        manager: &GlobalSystemMediaTransportControlsSessionManager,
    ) -> Option<GlobalSystemMediaTransportControlsSession> {
        let sessions = match manager.GetSessions() {
            Ok(s) => s,
            Err(_) => return manager.GetCurrentSession().ok(),
        };

        let count = sessions.Size().unwrap_or(0);
        if count == 0 {
            return manager.GetCurrentSession().ok();
        }

        let known_apps = ["spotify", "chrome", "msedge", "firefox", "brave", "vlc", "music", "opera", "tidal", "apple"];

        let mut playing_sessions = Vec::new();
        let mut paused_sessions = Vec::new();

        for i in 0..count {
            if let Ok(sess) = sessions.GetAt(i) {
                if let Ok(info) = sess.GetPlaybackInfo() {
                    if let Ok(status) = info.PlaybackStatus() {
                        if status == GlobalSystemMediaTransportControlsSessionPlaybackStatus::Playing {
                            playing_sessions.push(sess);
                        } else {
                            paused_sessions.push(sess);
                        }
                    }
                }
            }
        }

        // Prioridad 1: Reproduciendo en app conocida
        for sess in &playing_sessions {
            if let Ok(id) = sess.SourceAppUserModelId() {
                let id_lower = id.to_string().to_lowercase();
                if known_apps.iter().any(|k| id_lower.contains(k)) {
                    return Some(sess.clone());
                }
            }
        }

        if let Some(first_playing) = playing_sessions.into_iter().next() {
            return Some(first_playing);
        }

        // Prioridad 2: Pausado en app conocida
        for sess in &paused_sessions {
            if let Ok(id) = sess.SourceAppUserModelId() {
                let id_lower = id.to_string().to_lowercase();
                if known_apps.iter().any(|k| id_lower.contains(k)) {
                    return Some(sess.clone());
                }
            }
        }

        manager.GetCurrentSession().ok()
    }

    pub async fn refresh_session(
        session: &GlobalSystemMediaTransportControlsSession,
        app: &AppHandle,
        last_track: &Arc<Mutex<String>>,
        cache_thumb: &Arc<Mutex<Option<String>>>,
    ) {
        let props_op = match session.TryGetMediaPropertiesAsync() {
            Ok(op) => op,
            Err(_) => return,
        };
        let props = match props_op.get() {
            Ok(p) => p,
            Err(_) => return,
        };

        let title = props.Title().map(|s| s.to_string()).unwrap_or_default();
        let artist = props.Artist().map(|s| s.to_string()).unwrap_or_default();
        let album = props.AlbumTitle().map(|s| s.to_string()).unwrap_or_default();
        let source_app = session
            .SourceAppUserModelId()
            .map(|s| s.to_string())
            .unwrap_or_default();

        if title.is_empty() {
            return;
        }

        let is_playing = session
            .GetPlaybackInfo()
            .and_then(|info| info.PlaybackStatus())
            .map(|s| s == GlobalSystemMediaTransportControlsSessionPlaybackStatus::Playing)
            .unwrap_or(false);

        let timeline = session.GetTimelineProperties().ok();
        let position_ms = timeline
            .as_ref()
            .and_then(|t| t.Position().ok())
            .map(|d| d.Duration / 10000)
            .unwrap_or(0);
        let end_ms = timeline
            .as_ref()
            .and_then(|t| t.EndTime().ok())
            .map(|d| d.Duration / 10000)
            .unwrap_or(0);

        let track_key = format!("{}|{}|{}", source_app, title, artist);
        let mut track_changed = false;
        {
            let mut last = last_track.lock().unwrap();
            if *last != track_key {
                track_changed = true;
                *last = track_key.clone();
            }
        }

        // Thumbnail
        let mut thumbnail = cache_thumb.lock().unwrap().clone();
        if track_changed || thumbnail.is_none() {
            if let Ok(thumb_ref) = props.Thumbnail() {
                if let Ok(stream_op) = thumb_ref.OpenReadAsync() {
                    if let Ok(stream) = stream_op.get() {
                        if let Ok(size) = stream.Size() {
                            if size > 0 && size < 10 * 1024 * 1024 {
                                if let Ok(reader) = DataReader::CreateDataReader(&stream) {
                                    if let Ok(load_op) = reader.LoadAsync(size as u32) {
                                        if load_op.get().is_ok() {
                                            let mut bytes = vec![0u8; size as usize];
                                            if reader.ReadBytes(&mut bytes).is_ok() {
                                                let b64 = base64::engine::general_purpose::STANDARD.encode(&bytes);
                                                let data_url = format!("data:image/jpeg;base64,{}", b64);
                                                thumbnail = Some(data_url.clone());
                                                *cache_thumb.lock().unwrap() = Some(data_url);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        let media_state = MediaState {
            title,
            artist,
            album,
            is_playing,
            source_app,
            position_ms,
            end_ms,
            last_updated_ms: chrono::Utc::now().timestamp_millis(),
            track_key: track_key.clone(),
            art_key: Some(track_key),
            thumbnail,
        };

        let _ = app.emit("ln:media", MediaEvent {
            media: Some(media_state),
            track_changed,
        });
    }

    pub async fn play(&self) -> Result<(), String> {
        let sess_opt = self.current_session.lock().unwrap().clone();
        if let Some(sess) = sess_opt {
            let op = sess.TryPlayAsync().map_err(|e| e.to_string())?;
            let _ = op.get();
        }
        Ok(())
    }

    pub async fn pause(&self) -> Result<(), String> {
        let sess_opt = self.current_session.lock().unwrap().clone();
        if let Some(sess) = sess_opt {
            let op = sess.TryPauseAsync().map_err(|e| e.to_string())?;
            let _ = op.get();
        }
        Ok(())
    }

    pub async fn next(&self) -> Result<(), String> {
        let sess_opt = self.current_session.lock().unwrap().clone();
        if let Some(sess) = sess_opt {
            let op = sess.TrySkipNextAsync().map_err(|e| e.to_string())?;
            let _ = op.get();
        }
        Ok(())
    }

    pub async fn previous(&self) -> Result<(), String> {
        let sess_opt = self.current_session.lock().unwrap().clone();
        if let Some(sess) = sess_opt {
            let op = sess.TrySkipPreviousAsync().map_err(|e| e.to_string())?;
            let _ = op.get();
        }
        Ok(())
    }
}
