use crate::types::DownloadItem;
use std::collections::HashMap;
use std::sync::atomic::{AtomicBool, Ordering};
use std::sync::{Arc, Mutex};
use std::time::Duration;
use tauri::{AppHandle, Emitter};
use tokio::io::{AsyncReadExt, AsyncWriteExt};
use tokio::net::windows::named_pipe::{NamedPipeServer, ServerOptions};

const PIPE_NAME: &str = r"\\.\pipe\LockNotchDownloadsPipe";

pub struct DownloadManager {
    active_downloads: Arc<Mutex<HashMap<i64, DownloadItem>>>,
    current_pipe_writer: Arc<Mutex<Option<tokio::sync::mpsc::Sender<Vec<u8>>>>>,
}

impl DownloadManager {
    pub fn new() -> Self {
        Self {
            active_downloads: Arc::new(Mutex::new(HashMap::new())),
            current_pipe_writer: Arc::new(Mutex::new(None)),
        }
    }

    pub fn get_active_downloads(&self) -> Vec<DownloadItem> {
        let map = self.active_downloads.lock().unwrap();
        let mut list: Vec<DownloadItem> = map.values().cloned().collect();
        list.sort_by(|a, b| b.timestamp.cmp(&a.timestamp));
        list
    }

    pub fn has_active_downloads(&self) -> bool {
        let map = self.active_downloads.lock().unwrap();
        !map.is_empty()
    }

    pub async fn request_location_update(&self) -> bool {
        let writer_opt = self.current_pipe_writer.lock().unwrap().clone();
        if let Some(writer) = writer_opt {
            let msg = b"{\"type\":\"request_location\"}".to_vec();
            return writer.send(msg).await.is_ok();
        }
        false
    }
}

pub fn start_download_pipe_server(
    app: AppHandle,
    manager: Arc<DownloadManager>,
    running: Arc<AtomicBool>,
) {
    tokio::spawn(async move {
        while running.load(Ordering::Relaxed) {
            let server_res = ServerOptions::new()
                .first_pipe_instance(true)
                .create(PIPE_NAME);

            let mut server = match server_res {
                Ok(s) => s,
                Err(_) => {
                    // Si ya existe otra instancia o falló, esperar e intentar de nuevo
                    tokio::time::sleep(Duration::from_millis(1000)).await;
                    continue;
                }
            };

            if server.connect().await.is_err() {
                continue;
            }

            let (mut reader, mut writer) = tokio::io::split(server);
            let (tx, mut rx) = tokio::sync::mpsc::channel::<Vec<u8>>(32);
            {
                let mut current = manager.current_pipe_writer.lock().unwrap();
                *current = Some(tx);
            }

            // Tarea de escritura de salida hacia la extensión de Chrome
            tokio::spawn(async move {
                while let Some(msg) = rx.recv().await {
                    let len = (msg.len() as u32).to_le_bytes();
                    if writer.write_all(&len).await.is_err() {
                        break;
                    }
                    if writer.write_all(&msg).await.is_err() {
                        break;
                    }
                    let _ = writer.flush().await;
                }
            });

            // Bucle de lectura de mensajes entrantes
            let mut len_buf = [0u8; 4];
            while running.load(Ordering::Relaxed) {
                if reader.read_exact(&mut len_buf).await.is_err() {
                    break; // Extensión desconectada
                }

                let length = u32::from_le_bytes(len_buf) as usize;
                if length == 0 || length > 4 * 1024 * 1024 {
                    break;
                }

                let mut data_buf = vec![0u8; length];
                if reader.read_exact(&mut data_buf).await.is_err() {
                    break;
                }

                if let Ok(json_str) = String::from_utf8(data_buf) {
                    handle_incoming_json(&app, &manager, &json_str);
                }
            }

            {
                let mut current = manager.current_pipe_writer.lock().unwrap();
                *current = None;
            }
        }
    });
}

fn handle_incoming_json(app: &AppHandle, manager: &Arc<DownloadManager>, json: &str) {
    if let Ok(doc) = serde_json::from_str::<serde_json::Value>(json) {
        if let Some(msg_type) = doc.get("type").and_then(|v| v.as_str()) {
            if msg_type == "update_location" {
                if let (Some(lat), Some(lon)) = (
                    doc.get("latitude").and_then(|v| v.as_f64()),
                    doc.get("longitude").and_then(|v| v.as_f64()),
                ) {
                    let loc_str = format!("{},{}", lat, lon);
                    let _ = app.emit("ln:location", loc_str);
                }
                return;
            }

            if let Ok(item) = serde_json::from_str::<DownloadItem>(json) {
                let download_id = item.download_id;
                let is_terminal = item.item_type == "download_completed"
                    || item.item_type == "download_interrupted"
                    || item.item_type == "download_cancelled";

                {
                    let mut map = manager.active_downloads.lock().unwrap();
                    map.insert(download_id, item);
                }

                emit_downloads(app, manager);

                if is_terminal {
                    let mgr_clone = manager.clone();
                    let app_clone = app.clone();
                    tokio::spawn(async move {
                        tokio::time::sleep(Duration::from_millis(3500)).await;
                        {
                            let mut map = mgr_clone.active_downloads.lock().unwrap();
                            map.remove(&download_id);
                        }
                        emit_downloads(&app_clone, &mgr_clone);
                    });
                }
            }
        }
    }
}

fn emit_downloads(app: &AppHandle, manager: &Arc<DownloadManager>) {
    let list = manager.get_active_downloads();
    let _ = app.emit("ln:downloads", list);
}
