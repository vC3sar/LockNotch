use crate::types::Toggles;
use std::os::windows::process::CommandExt;
use std::process::Command;
use winreg::enums::*;
use winreg::RegKey;

const CREATE_NO_WINDOW: u32 = 0x08000000;

pub struct RadioManager;

impl RadioManager {
    pub async fn get_initial_toggles() -> Toggles {
        let dark_mode = Self::is_dark_mode_enabled();
        let dnd = Self::is_dnd_enabled();
        let (wifi, bluetooth) = Self::query_radios().await;

        Toggles {
            wifi,
            bluetooth,
            dark_mode,
            dnd,
        }
    }

    async fn query_radios() -> (bool, bool) {
        use windows::Devices::Radios::{Radio, RadioKind, RadioState};

        let mut wifi = false;
        let mut bt = false;

        if let Ok(op) = Radio::GetRadiosAsync() {
            if let Ok(radios) = op.get() {
                if let Ok(count) = radios.Size() {
                    for i in 0..count {
                        if let Ok(radio) = radios.GetAt(i) {
                            if let (Ok(kind), Ok(state)) = (radio.Kind(), radio.State()) {
                                if kind == RadioKind::WiFi {
                                    wifi = state == RadioState::On;
                                } else if kind == RadioKind::Bluetooth {
                                    bt = state == RadioState::On;
                                }
                            }
                        }
                    }
                }
            }
        }
        (wifi, bt)
    }

    pub async fn set_wifi(enabled: bool) -> bool {
        use windows::Devices::Radios::{Radio, RadioKind, RadioState};

        let target_state = if enabled {
            RadioState::On
        } else {
            RadioState::Off
        };

        if let Ok(op) = Radio::GetRadiosAsync() {
            if let Ok(radios) = op.get() {
                if let Ok(count) = radios.Size() {
                    for i in 0..count {
                        if let Ok(radio) = radios.GetAt(i) {
                            if let Ok(kind) = radio.Kind() {
                                if kind == RadioKind::WiFi {
                                    if let Ok(set_op) = radio.SetStateAsync(target_state) {
                                        let _ = set_op.get();
                                        return enabled;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // Fallback vía netsh
        let state_str = if enabled { "admin=enable" } else { "admin=disable" };
        let _ = Command::new("netsh")
            .args(["interface", "set", "interface", "Wi-Fi", state_str])
            .creation_flags(CREATE_NO_WINDOW)
            .status();

        enabled
    }

    pub async fn set_bluetooth(enabled: bool) -> bool {
        use windows::Devices::Radios::{Radio, RadioKind, RadioState};

        let target_state = if enabled {
            RadioState::On
        } else {
            RadioState::Off
        };

        if let Ok(op) = Radio::GetRadiosAsync() {
            if let Ok(radios) = op.get() {
                if let Ok(count) = radios.Size() {
                    for i in 0..count {
                        if let Ok(radio) = radios.GetAt(i) {
                            if let Ok(kind) = radio.Kind() {
                                if kind == RadioKind::Bluetooth {
                                    if let Ok(set_op) = radio.SetStateAsync(target_state) {
                                        let _ = set_op.get();
                                        return enabled;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // Fallback vía PowerShell servicio bthserv
        let cmd = if enabled { "Start-Service bthserv" } else { "Stop-Service bthserv -Force" };
        let _ = Command::new("powershell")
            .args(["-NoProfile", "-WindowStyle", "Hidden", "-Command", cmd])
            .creation_flags(CREATE_NO_WINDOW)
            .status();

        enabled
    }

    pub fn set_dark_mode(enabled: bool) -> bool {
        let hkcu = RegKey::predef(HKEY_CURRENT_USER);
        let path = r"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        if let Ok(key) = hkcu.open_subkey_with_flags(path, KEY_SET_VALUE) {
            let val: u32 = if enabled { 0 } else { 1 };
            let _ = key.set_value("AppsUseLightTheme", &val);
            let _ = key.set_value("SystemUsesLightTheme", &val);
        }
        enabled
    }

    pub fn is_dark_mode_enabled() -> bool {
        let hkcu = RegKey::predef(HKEY_CURRENT_USER);
        let path = r"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        if let Ok(key) = hkcu.open_subkey(path) {
            if let Ok(val) = key.get_value::<u32, _>("AppsUseLightTheme") {
                return val == 0;
            }
        }
        true
    }

    pub fn set_dnd(enabled: bool) -> bool {
        let hkcu = RegKey::predef(HKEY_CURRENT_USER);
        let path = r"Software\Policies\Microsoft\Windows\CurrentVersion\QuietHours";
        if let Ok((key, _)) = hkcu.create_subkey(path) {
            let val: u32 = if enabled { 1 } else { 0 };
            let _ = key.set_value("EnableQuietHours", &val);
        }
        enabled
    }

    pub fn is_dnd_enabled() -> bool {
        let hkcu = RegKey::predef(HKEY_CURRENT_USER);
        let path = r"Software\Policies\Microsoft\Windows\CurrentVersion\QuietHours";
        if let Ok(key) = hkcu.open_subkey(path) {
            if let Ok(val) = key.get_value::<u32, _>("EnableQuietHours") {
                return val == 1;
            }
        }
        false
    }
}
