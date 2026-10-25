use crate::types::UsbEvent;
use std::collections::HashSet;
use std::sync::atomic::{AtomicBool, Ordering};
use std::sync::Arc;
use std::time::Duration;
use tauri::{AppHandle, Emitter};
use windows::core::PCWSTR;
use windows::Win32::Storage::FileSystem::{
    GetDriveTypeW, GetLogicalDriveStringsW, GetVolumeInformationW,
};

const DRIVE_REMOVABLE: u32 = 2;

pub fn start_usb_monitor(app: AppHandle, running: Arc<AtomicBool>) {
    std::thread::spawn(move || {
        let mut known_drives = get_connected_removable_drives();

        while running.load(Ordering::Relaxed) {
            std::thread::sleep(Duration::from_millis(1500));
            let current_drives = get_connected_removable_drives();

            // Detectar desconexiones
            for drive in known_drives.difference(&current_drives) {
                let _ = app.emit(
                    "ln:usb",
                    UsbEvent {
                        device_id: drive.clone(),
                        friendly_name: format!("Unidad USB ({})", drive),
                        kind: "Almacenamiento".into(),
                        details: "Desconectado".into(),
                        glyph: "\u{E8EB}".into(),
                        connected: false,
                    },
                );
            }

            // Detectar inserciones
            for drive in current_drives.difference(&known_drives) {
                let volume_name = get_drive_label(drive);
                let name = if volume_name.is_empty() {
                    format!("Unidad USB ({})", drive)
                } else {
                    format!("{} ({})", volume_name, drive)
                };

                let _ = app.emit(
                    "ln:usb",
                    UsbEvent {
                        device_id: drive.clone(),
                        friendly_name: name,
                        kind: "Almacenamiento USB".into(),
                        details: "Conectado".into(),
                        glyph: "\u{E8EB}".into(),
                        connected: true,
                    },
                );
            }

            known_drives = current_drives;
        }
    });
}

fn get_connected_removable_drives() -> HashSet<String> {
    let mut set = HashSet::new();
    unsafe {
        let mut buf = [0u16; 512];
        let len = GetLogicalDriveStringsW(Some(&mut buf));
        if len > 0 {
            let mut start = 0;
            for i in 0..len as usize {
                if buf[i] == 0 {
                    if start < i {
                        let drive_str = String::from_utf16_lossy(&buf[start..i]);
                        let drive_wide: Vec<u16> = drive_str.encode_utf16().chain(std::iter::once(0)).collect();
                        if GetDriveTypeW(PCWSTR(drive_wide.as_ptr())) == DRIVE_REMOVABLE {
                            set.insert(drive_str.trim_end_matches('\\').to_string());
                        }
                    }
                    start = i + 1;
                }
            }
        }
    }
    set
}

fn get_drive_label(drive: &str) -> String {
    unsafe {
        let drive_path = format!("{}\\\0", drive);
        let wide: Vec<u16> = drive_path.encode_utf16().collect();
        let mut vol_name = [0u16; 256];
        if GetVolumeInformationW(
            PCWSTR(wide.as_ptr()),
            Some(&mut vol_name),
            None,
            None,
            None,
            None,
        )
        .is_ok()
        {
            let len = vol_name.iter().position(|&c| c == 0).unwrap_or(vol_name.len());
            return String::from_utf16_lossy(&vol_name[..len]);
        }
    }
    "".to_string()
}
