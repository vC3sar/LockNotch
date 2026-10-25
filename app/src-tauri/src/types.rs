use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "camelCase")]
pub struct MediaState {
    pub title: String,
    pub artist: String,
    pub album: String,
    pub is_playing: bool,
    pub source_app: String,
    pub position_ms: i64,
    pub end_ms: i64,
    pub last_updated_ms: i64,
    pub track_key: String,
    pub art_key: Option<String>,
    pub thumbnail: Option<String>,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "camelCase")]
pub struct MediaEvent {
    pub media: Option<MediaState>,
    pub track_changed: bool,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "camelCase")]
pub struct BatteryState {
    pub percent: i32,
    pub charging: bool,
    pub low: bool,
    pub has_battery: bool,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "camelCase")]
pub struct WeatherState {
    pub temperature: String,
    pub glyph: String,
    pub code: i32,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "camelCase")]
pub struct HardwareState {
    pub cpu: String,
    pub gpu: String,
    pub cpu_value: Option<f64>,
    pub gpu_value: Option<f64>,
    pub cpu_kind: String, // "temp" | "load" | "none"
    pub gpu_kind: String,
    pub cpu_source: String,
    pub gpu_source: String,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "camelCase")]
pub struct UsbEvent {
    pub device_id: String,
    pub friendly_name: String,
    pub kind: String,
    pub details: String,
    pub glyph: String,
    pub connected: bool,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "camelCase")]
pub struct WinNotification {
    pub app_name: String,
    pub title: String,
    pub body: String,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "camelCase")]
pub struct DownloadItem {
    #[serde(rename = "type")]
    pub item_type: String,
    pub download_id: i64,
    pub filename: String,
    pub state: String,
    pub total_bytes: i64,
    pub bytes_received: i64,
    pub progress: i32,
    pub speed: i64,
    pub url: String,
    pub timestamp: i64,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "camelCase")]
pub struct Toggles {
    pub wifi: bool,
    pub bluetooth: bool,
    pub dark_mode: bool,
    pub dnd: bool,
}

// Conserva PascalCase para 100% de compatibilidad con settings.json original
#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "PascalCase")]
pub struct Settings {
    pub app_launcher1: String,
    pub app_launcher2: String,
    pub app_launcher3: String,
    pub app_launcher4: String,
    pub app_launcher5: String,
    pub start_with_windows: bool,
    pub hide_in_fullscreen: bool,
    pub weather_latitude: f64,
    pub weather_longitude: f64,
    pub weather_use_fahrenheit: bool,
    pub weather_show_periodic_location: bool,
    pub hardware_refresh_interval_seconds: u32,
    pub theme: String,
    pub font_family: String,
    #[serde(default)]
    pub ambient_glow: bool,
    #[serde(default)]
    pub reduce_motion: bool,
}

impl Default for Settings {
    fn default() -> Self {
        Self {
            app_launcher1: "ms-settings:".into(),
            app_launcher2: "explorer.exe".into(),
            app_launcher3: "calc.exe".into(),
            app_launcher4: "notepad.exe".into(),
            app_launcher5: "mspaint.exe".into(),
            start_with_windows: false,
            hide_in_fullscreen: true,
            weather_latitude: 40.4165,
            weather_longitude: -3.7026,
            weather_use_fahrenheit: false,
            weather_show_periodic_location: true,
            hardware_refresh_interval_seconds: 2,
            theme: "Dark".into(),
            font_family: "Normal".into(),
            ambient_glow: true,
            reduce_motion: false,
        }
    }
}

#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "camelCase")]
pub struct Snapshot {
    pub media: Option<MediaState>,
    pub battery: Option<BatteryState>,
    pub weather: Option<WeatherState>,
    pub hardware: Option<HardwareState>,
    pub downloads: Vec<DownloadItem>,
    pub toggles: Toggles,
    pub settings: Settings,
    pub location_name: Option<String>,
    pub hidden: bool,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "camelCase")]
pub struct IconData {
    pub width: u32,
    pub height: u32,
    pub rgba: String,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "camelCase")]
pub struct AppEntry {
    pub slot: String,
    pub path: String,
    pub name: String,
    pub icon: Option<IconData>,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "camelCase")]
pub struct HitRect {
    pub x: i32,
    pub y: i32,
    pub width: i32,
    pub height: i32,
}

#[derive(Debug, Clone, Serialize, Deserialize)]
#[serde(rename_all = "camelCase")]
pub struct IntegrationStatus {
    pub native_host_registered: bool,
    pub extension_connected: bool,
    pub notification_access: String,
    pub cpu_source: String,
    pub gpu_source: String,
    pub version: String,
    pub exe_path: String,
    pub settings_path: String,
}
