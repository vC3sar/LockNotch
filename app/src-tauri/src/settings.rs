use crate::types::Settings;
use std::fs;
use std::path::PathBuf;
use winreg::enums::*;
use winreg::RegKey;

pub struct SettingsManager {
    path: PathBuf,
}

impl SettingsManager {
    pub fn new() -> Self {
        let app_data = std::env::var("APPDATA").unwrap_or_else(|_| "C:\\".into());
        let path = PathBuf::from(app_data).join("LockNotch").join("settings.json");
        Self { path }
    }

    pub fn load(&self) -> Settings {
        if self.path.exists() {
            if let Ok(data) = fs::read_to_string(&self.path) {
                if let Ok(settings) = serde_json::from_str::<Settings>(&data) {
                    return settings;
                }
            }
        }
        let default_settings = Settings::default();
        let _ = self.save(&default_settings);
        default_settings
    }

    pub fn save(&self, settings: &Settings) -> Result<(), String> {
        if let Some(parent) = self.path.parent() {
            let _ = fs::create_dir_all(parent);
        }
        let json = serde_json::to_string_pretty(settings)
            .map_err(|e| format!("Serialization error: {}", e))?;
        fs::write(&self.path, json).map_err(|e| format!("Write error: {}", e))?;

        // Sincronizar inicio con Windows en el Registro
        let _ = Self::apply_startup_registry(settings.start_with_windows);
        Ok(())
    }

    pub fn get_path(&self) -> String {
        self.path.to_string_lossy().to_string()
    }

    pub fn apply_startup_registry(enable: bool) -> Result<(), String> {
        let hkcu = RegKey::predef(HKEY_CURRENT_USER);
        let path = r"Software\Microsoft\Windows\CurrentVersion\Run";
        let (key, _) = hkcu.create_subkey(path).map_err(|e| e.to_string())?;

        if enable {
            if let Ok(exe_path) = std::env::current_exe() {
                let val = format!("\"{}\"", exe_path.to_string_lossy());
                let _ = key.set_value("LockNotch", &val);
            }
        } else {
            let _ = key.delete_value("LockNotch");
        }
        Ok(())
    }
}
