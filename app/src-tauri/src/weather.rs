use crate::types::WeatherState;
use std::sync::atomic::{AtomicBool, Ordering};
use std::sync::Arc;
use std::time::Duration;
use tauri::{AppHandle, Emitter};
use windows::core::HSTRING;
use windows::Foundation::Uri;
use windows::Web::Http::HttpClient;

pub struct WeatherService {
    http: HttpClient,
}

impl WeatherService {
    pub fn new() -> Result<Self, String> {
        let http = HttpClient::new().map_err(|e| e.to_string())?;
        Ok(Self { http })
    }

    pub async fn fetch_weather(
        &self,
        lat: f64,
        lon: f64,
        fahrenheit: bool,
    ) -> Result<WeatherState, String> {
        let unit = if fahrenheit {
            "&temperature_unit=fahrenheit"
        } else {
            ""
        };
        let url_str = format!(
            "https://api.open-meteo.com/v1/forecast?latitude={}&longitude={}&current_weather=true{}",
            lat, lon, unit
        );

        let uri = Uri::CreateUri(&HSTRING::from(&url_str)).map_err(|e| e.to_string())?;
        let response = self
            .http
            .GetStringAsync(&uri)
            .map_err(|e| e.to_string())?
            .get()
            .map_err(|e| e.to_string())?;

        let json_str = response.to_string();
        let doc: serde_json::Value =
            serde_json::from_str(&json_str).map_err(|e| e.to_string())?;

        let current = doc
            .get("current_weather")
            .ok_or_else(|| "Missing current_weather".to_string())?;

        let temp = current
            .get("temperature")
            .and_then(|v| v.as_f64())
            .unwrap_or(0.0);
        let code = current
            .get("weathercode")
            .and_then(|v| v.as_i64())
            .unwrap_or(0) as i32;

        let glyph = get_glyph_for_code(code).to_string();
        let temperature = format!("{:.0}°", temp.round());

        Ok(WeatherState {
            temperature,
            glyph,
            code,
        })
    }
}

pub fn get_glyph_for_code(code: i32) -> &'static str {
    match code {
        0 => "☀️",
        1 => "🌤️",
        2 | 3 => "☁️",
        45 | 48 => "🌫️",
        51 | 53 | 55 | 56 | 57 => "🌦️",
        61 | 63 | 65 | 66 | 67 => "🌧️",
        71 | 73 | 75 | 77 => "❄️",
        80 | 81 | 82 => "🌧️",
        85 | 86 => "🌨️",
        95 | 96 | 99 => "⛈️",
        _ => "☀️",
    }
}

pub fn start_weather_worker(
    app: AppHandle,
    running: Arc<AtomicBool>,
    lat: f64,
    lon: f64,
    fahrenheit: bool,
) {
    tokio::spawn(async move {
        let service = match WeatherService::new() {
            Ok(s) => s,
            Err(e) => {
                log::error!("Failed to initialize WeatherService: {}", e);
                return;
            }
        };

        while running.load(Ordering::Relaxed) {
            if let Ok(weather) = service.fetch_weather(lat, lon, fahrenheit).await {
                let _ = app.emit("ln:weather", weather);
            }
            tokio::time::sleep(Duration::from_secs(1800)).await; // 30 minutos
        }
    });
}
