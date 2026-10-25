mod apps;
mod battery;
mod downloads;
mod hardware;
mod media;
mod native_host;
mod notifications;
mod radios;
mod settings;
mod types;
mod usb;
mod weather;
mod window;

use std::sync::atomic::{AtomicBool, Ordering};
use std::sync::{Arc, Mutex};
use tauri::menu::{Menu, MenuItem};
use tauri::tray::{TrayIconBuilder, TrayIconEvent};
use tauri::{AppHandle, Emitter, Manager, State, WebviewUrl, WebviewWindowBuilder};
use types::*;

pub struct AppState {
    pub settings: Arc<Mutex<settings::SettingsManager>>,
    pub window_mgr: Arc<Mutex<Option<window::WindowManager>>>,
    pub media: Arc<media::MediaService>,
    pub downloads: Arc<downloads::DownloadManager>,
    pub notifications: Arc<notifications::NotificationService>,
    pub weather: Arc<weather::WeatherService>,
    pub running: Arc<AtomicBool>,
    pub hide_fullscreen_setting: Arc<AtomicBool>,
}

#[tauri::command]
async fn get_snapshot(state: State<'_, AppState>) -> Result<Snapshot, String> {
    let settings = state.settings.lock().unwrap().load();
    let toggles = radios::RadioManager::get_initial_toggles().await;
    let downloads = state.downloads.get_active_downloads();
    let battery = battery::get_battery_status();

    Ok(Snapshot {
        media: None,
        battery,
        weather: None,
        hardware: None,
        downloads,
        toggles,
        settings,
        location_name: None,
        hidden: false,
    })
}

#[tauri::command]
fn set_hit_rect(rect: HitRect, state: State<'_, AppState>) -> Result<(), String> {
    let mgr_lock = state.window_mgr.lock().unwrap();
    if let Some(ref mgr) = *mgr_lock {
        mgr.update_hit_rect(rect);
    }
    Ok(())
}

#[tauri::command]
fn set_view_hint(_expanded: bool, _page: String) -> Result<(), String> {
    Ok(())
}

#[tauri::command]
async fn media_control(action: String, state: State<'_, AppState>) -> Result<(), String> {
    match action.as_str() {
        "play" => state.media.play().await,
        "pause" => state.media.pause().await,
        "next" => state.media.next().await,
        "previous" => state.media.previous().await,
        _ => Ok(()),
    }
}

#[tauri::command]
fn launch_app(slot: String, state: State<'_, AppState>) -> Result<(), String> {
    let s = state.settings.lock().unwrap().load();
    let target = match slot.as_str() {
        "1" => s.app_launcher1,
        "2" => s.app_launcher2,
        "3" => s.app_launcher3,
        "4" => s.app_launcher4,
        "5" => s.app_launcher5,
        other => other.to_string(),
    };
    apps::AppLauncher::launch(&target)
}

#[tauri::command]
fn get_app_entries(state: State<'_, AppState>) -> Result<Vec<AppEntry>, String> {
    let s = state.settings.lock().unwrap().load();
    let slots = [
        ("1", s.app_launcher1.as_str()),
        ("2", s.app_launcher2.as_str()),
        ("3", s.app_launcher3.as_str()),
        ("4", s.app_launcher4.as_str()),
        ("5", s.app_launcher5.as_str()),
    ];
    Ok(apps::AppLauncher::get_app_entries(&slots))
}

#[tauri::command]
async fn set_wifi(enabled: bool) -> Result<bool, String> {
    Ok(radios::RadioManager::set_wifi(enabled).await)
}

#[tauri::command]
async fn set_bluetooth(enabled: bool) -> Result<bool, String> {
    Ok(radios::RadioManager::set_bluetooth(enabled).await)
}

#[tauri::command]
fn set_dark_mode(enabled: bool) -> Result<bool, String> {
    Ok(radios::RadioManager::set_dark_mode(enabled))
}

#[tauri::command]
fn set_dnd(enabled: bool) -> Result<bool, String> {
    Ok(radios::RadioManager::set_dnd(enabled))
}

#[tauri::command]
fn get_settings(state: State<'_, AppState>) -> Result<Settings, String> {
    Ok(state.settings.lock().unwrap().load())
}

#[tauri::command]
fn save_settings(settings: Settings, state: State<'_, AppState>, app: AppHandle) -> Result<Settings, String> {
    state.settings.lock().unwrap().save(&settings)?;
    state.hide_fullscreen_setting.store(settings.hide_in_fullscreen, Ordering::Relaxed);
    let _ = app.emit("ln:settings", &settings);
    Ok(settings)
}

#[tauri::command]
async fn detect_location_ip(state: State<'_, AppState>) -> Result<(f64, f64), String> {
    // Consulta a ip-api.com mediante WinRT HttpClient
    let uri = windows::Foundation::Uri::CreateUri(&windows::core::HSTRING::from("http://ip-api.com/json/"))
        .map_err(|e| e.to_string())?;
    let http = windows::Web::Http::HttpClient::new().map_err(|e| e.to_string())?;
    let resp = http.GetStringAsync(&uri).map_err(|e| e.to_string())?.get().map_err(|e| e.to_string())?;
    let json: serde_json::Value = serde_json::from_str(&resp.to_string()).map_err(|e| e.to_string())?;
    
    let lat = json.get("lat").and_then(|v| v.as_f64()).ok_or("Sin latitud")?;
    let lon = json.get("lon").and_then(|v| v.as_f64()).ok_or("Sin longitud")?;

    let mut current = state.settings.lock().unwrap().load();
    current.weather_latitude = lat;
    current.weather_longitude = lon;
    let _ = state.settings.lock().unwrap().save(&current);

    Ok((lat, lon))
}

#[tauri::command]
async fn request_extension_location(state: State<'_, AppState>) -> Result<bool, String> {
    Ok(state.downloads.request_location_update().await)
}

#[tauri::command]
fn get_integration_status(state: State<'_, AppState>) -> Result<IntegrationStatus, String> {
    let native_host_registered = native_host::is_native_host_registered();
    let extension_connected = state.downloads.has_active_downloads();
    let notification_access = state.notifications.access_status();
    let exe_path = std::env::current_exe()
        .map(|p| p.to_string_lossy().to_string())
        .unwrap_or_default();
    let settings_path = state.settings.lock().unwrap().get_path();

    Ok(IntegrationStatus {
        native_host_registered,
        extension_connected,
        notification_access,
        cpu_source: "Win32 PDH".into(),
        gpu_source: "Win32 PDH".into(),
        version: "2.0.0".into(),
        exe_path,
        settings_path,
    })
}

#[tauri::command]
fn register_native_host() -> Result<bool, String> {
    native_host::register_native_host()
}

#[tauri::command]
fn open_settings(app: AppHandle) -> Result<(), String> {
    if let Some(win) = app.get_webview_window("settings") {
        let _ = win.show();
        let _ = win.set_focus();
        return Ok(());
    }

    let _ = WebviewWindowBuilder::new(&app, "settings", WebviewUrl::App("settings.html".into()))
        .title("Configuración de LockNotch")
        .inner_size(680.0, 520.0)
        .resizable(false)
        .center()
        .decorations(false)
        .build();

    Ok(())
}

#[tauri::command]
fn open_external(target: String) -> Result<(), String> {
    apps::AppLauncher::launch(&target)
}

#[tauri::command]
fn quit_app(app: AppHandle, state: State<'_, AppState>) {
    state.running.store(false, Ordering::Relaxed);
    app.exit(0);
}

pub fn run() {
    // Si se invoca desde Chrome como Native Messaging Host
    if native_host::is_native_host_call() {
        native_host::run_native_host_proxy();
        return;
    }

    tauri::Builder::default()
        .plugin(tauri_plugin_single_instance::init(|app, _argv, _cwd| {
            if let Some(win) = app.get_webview_window("island") {
                let _ = win.show();
            }
        }))
        .plugin(tauri_plugin_dialog::init())
        .setup(|app| {
            let settings_mgr = Arc::new(Mutex::new(settings::SettingsManager::new()));
            let initial_settings = settings_mgr.lock().unwrap().load();

            let downloads = Arc::new(downloads::DownloadManager::new());
            let running = Arc::new(AtomicBool::new(true));
            let hide_fullscreen = Arc::new(AtomicBool::new(initial_settings.hide_in_fullscreen));

            // Configurar ventana de la isla
            let island_win = app.get_webview_window("island").expect("island window missing");
            let win_mgr = match window::WindowManager::new(&island_win) {
                Ok(m) => Arc::new(Mutex::new(Some(m))),
                Err(_) => Arc::new(Mutex::new(None)),
            };

            // Mostrar la ventana ya posicionada
            let _ = island_win.show();

            // Servicios asíncronos
            let app_handle = app.handle().clone();
            let downloads_clone = downloads.clone();
            let running_clone = running.clone();
            downloads::start_download_pipe_server(app_handle.clone(), downloads_clone, running_clone);

            let media_service = Arc::new(tokio::task::block_in_place(|| {
                tokio::runtime::Handle::current().block_on(media::MediaService::new())
            }));
            media_service.start_tracking(app_handle.clone());

            let notif_service = Arc::new(tokio::task::block_in_place(|| {
                tokio::runtime::Handle::current().block_on(notifications::NotificationService::new())
            }));
            let _ = notif_service.start_listening(app_handle.clone());

            battery::start_battery_worker(app_handle.clone(), running.clone());
            hardware::start_hardware_worker(
                app_handle.clone(),
                running.clone(),
                initial_settings.hardware_refresh_interval_seconds,
            );
            usb::start_usb_monitor(app_handle.clone(), running.clone());
            window::start_fullscreen_detector(app_handle.clone(), hide_fullscreen.clone());

            let weather_service = Arc::new(weather::WeatherService::new().unwrap());
            weather::start_weather_worker(
                app_handle.clone(),
                running.clone(),
                initial_settings.weather_latitude,
                initial_settings.weather_longitude,
                initial_settings.weather_use_fahrenheit,
            );

            // Bucle de hit-test interactivo para click-through transparente
            let win_mgr_click = win_mgr.clone();
            let running_click = running.clone();
            std::thread::spawn(move || {
                use windows::Win32::UI::WindowsAndMessaging::GetCursorPos;
                use windows::Win32::Foundation::POINT;

                while running_click.load(Ordering::Relaxed) {
                    std::thread::sleep(std::time::Duration::from_millis(50));
                    unsafe {
                        let mut pt = POINT::default();
                        if GetCursorPos(&mut pt).is_ok() {
                            let mgr_lock = win_mgr_click.lock().unwrap();
                            if let Some(ref mgr) = *mgr_lock {
                                let hovered = mgr.check_cursor_hover(pt);
                                // Si está encima, la ventana captura clics; si está fuera, los clics pasan a la app de abajo
                                mgr.set_click_through(!hovered);
                            }
                        }
                    }
                }
            });

            // Bandeja del sistema (System Tray)
            let settings_item = MenuItem::with_id(app, "settings", "Configuración", true, None::<&str>)?;
            let quit_item = MenuItem::with_id(app, "quit", "Salir de LockNotch", true, None::<&str>)?;
            let menu = Menu::with_items(app, &[&settings_item, &quit_item])?;

            let app_handle_tray = app.handle().clone();
            let _tray = TrayIconBuilder::new()
                .icon(app.default_window_icon().unwrap().clone())
                .menu(&menu)
                .show_menu_on_left_click(false)
                .on_menu_event(move |_app, event| match event.id.as_ref() {
                    "settings" => {
                        let _ = open_settings(app_handle_tray.clone());
                    }
                    "quit" => {
                        app_handle_tray.exit(0);
                    }
                    _ => {}
                })
                .on_tray_icon_event(|tray, event| {
                    if let TrayIconEvent::DoubleClick { .. } = event {
                        let _ = open_settings(tray.app_handle().clone());
                    }
                })
                .build(app)?;

            // Auto-registrar host nativo de Chrome al iniciar si no está configurado
            let _ = native_host::register_native_host();

            // Guardar estado global
            app.manage(AppState {
                settings: settings_mgr,
                window_mgr: win_mgr,
                media: media_service,
                downloads,
                notifications: notif_service,
                weather: weather_service,
                running,
                hide_fullscreen_setting: hide_fullscreen,
            });

            Ok(())
        })
        .invoke_handler(tauri::generate_handler![
            get_snapshot,
            set_hit_rect,
            set_view_hint,
            media_control,
            launch_app,
            get_app_entries,
            set_wifi,
            set_bluetooth,
            set_dark_mode,
            set_dnd,
            get_settings,
            save_settings,
            detect_location_ip,
            request_extension_location,
            get_integration_status,
            register_native_host,
            open_settings,
            open_external,
            quit_app,
        ])
        .run(tauri::generate_context!())
        .expect("error while running tauri application");
}
