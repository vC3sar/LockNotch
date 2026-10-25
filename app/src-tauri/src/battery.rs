use crate::types::BatteryState;
use std::sync::atomic::{AtomicBool, Ordering};
use std::sync::Arc;
use std::time::Duration;
use tauri::{AppHandle, Emitter};
use windows::Win32::System::Power::{GetSystemPowerStatus, SYSTEM_POWER_STATUS};

pub fn get_battery_status() -> Option<BatteryState> {
    unsafe {
        let mut status = SYSTEM_POWER_STATUS::default();
        if GetSystemPowerStatus(&mut status).is_ok() {
            let percent = if status.BatteryLifePercent == 255 {
                -1 // Sin batería (equipo de sobremesa)
            } else {
                status.BatteryLifePercent.min(100) as i32
            };

            let charging = (status.BatteryFlag & 8) != 0 || status.ACLineStatus == 1;
            let low = (0..=20).contains(&percent);
            let has_battery = status.BatteryLifePercent != 255;

            return Some(BatteryState {
                percent,
                charging,
                low,
                has_battery,
            });
        }
    }
    None
}

pub fn start_battery_worker(app: AppHandle, running: Arc<AtomicBool>) {
    std::thread::spawn(move || {
        let mut last_state: Option<BatteryState> = None;
        while running.load(Ordering::Relaxed) {
            if let Some(state) = get_battery_status() {
                let changed = match &last_state {
                    Some(last) => {
                        last.percent != state.percent
                            || last.charging != state.charging
                            || last.low != state.low
                    }
                    None => true,
                };

                if changed {
                    last_state = Some(state.clone());
                    let _ = app.emit("ln:battery", state);
                }
            }
            std::thread::sleep(Duration::from_secs(5));
        }
    });
}
