use crate::types::WinNotification;
use std::sync::atomic::{AtomicBool, Ordering};
use std::sync::Arc;
use tauri::{AppHandle, Emitter};
use windows::Foundation::TypedEventHandler;
use windows::UI::Notifications::Management::{
    UserNotificationListener, UserNotificationListenerAccessStatus,
};
use windows::UI::Notifications::{
    KnownNotificationBindings, UserNotificationChangedEventArgs, UserNotificationChangedKind,
};

pub struct NotificationService {
    listener: Option<UserNotificationListener>,
    access_status: String,
}

impl NotificationService {
    pub async fn new() -> Self {
        let listener_res = UserNotificationListener::Current();
        if let Ok(listener) = listener_res {
            let access_op = listener.RequestAccessAsync();
            if let Ok(access) = access_op {
                if let Ok(status) = access.get() {
                    let status_str = match status {
                        UserNotificationListenerAccessStatus::Allowed => "Permitido".to_string(),
                        UserNotificationListenerAccessStatus::Denied => "Denegado".to_string(),
                        _ => "No especificado".to_string(),
                    };
                    return Self {
                        listener: Some(listener),
                        access_status: status_str,
                    };
                }
            }
        }
        Self {
            listener: None,
            access_status: "No disponible".to_string(),
        }
    }

    pub fn access_status(&self) -> String {
        self.access_status.clone()
    }

    pub fn start_listening(&self, app: AppHandle) -> Result<(), String> {
        if let Some(ref listener) = self.listener {
            let app_clone = app.clone();
            let listener_clone = listener.clone();

            let handler = TypedEventHandler::<UserNotificationListener, UserNotificationChangedEventArgs>::new(
                move |_sender: windows::core::Ref<'_, UserNotificationListener>, args: windows::core::Ref<'_, UserNotificationChangedEventArgs>| {
                    if let Some(ev) = args.as_ref() {
                        if let Ok(kind) = ev.ChangeKind() {
                            if kind == UserNotificationChangedKind::Added {
                                if let Ok(id) = ev.UserNotificationId() {
                                    if let Ok(notif) = listener_clone.GetNotification(id) {
                                        let mut app_name = "Windows".to_string();
                                        if let Ok(app_info) = notif.AppInfo() {
                                            if let Ok(disp_info) = app_info.DisplayInfo() {
                                                if let Ok(name) = disp_info.DisplayName() {
                                                    app_name = name.to_string();
                                                }
                                            }
                                        }

                                        if let Ok(visual) = notif.Notification().and_then(|n| n.Visual()) {
                                            if let Ok(binding) = visual.GetBinding(&KnownNotificationBindings::ToastGeneric().unwrap_or_default()) {
                                                if let Ok(texts) = binding.GetTextElements() {
                                                    let mut title = "".to_string();
                                                    let mut body = "".to_string();

                                                    if let Ok(first) = texts.GetAt(0) {
                                                        if let Ok(t) = first.Text() {
                                                            title = t.to_string();
                                                        }
                                                    }
                                                    if let Ok(second) = texts.GetAt(1) {
                                                        if let Ok(b) = second.Text() {
                                                            body = b.to_string();
                                                        }
                                                    }

                                                    if !title.is_empty() {
                                                        let _ = app_clone.emit("ln:notification", WinNotification {
                                                            app_name,
                                                            title,
                                                            body,
                                                        });
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    Ok(())
                },
            );

            listener.NotificationChanged(&handler).map_err(|e| e.to_string())?;
        }
        Ok(())
    }
}
