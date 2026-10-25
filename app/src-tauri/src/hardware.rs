use crate::types::HardwareState;
use std::sync::atomic::{AtomicBool, Ordering};
use std::sync::Arc;
use std::time::Duration;
use tauri::{AppHandle, Emitter};
use windows::core::PCWSTR;
use windows::Win32::System::Performance::{
    PdhAddEnglishCounterW, PdhCloseQuery, PdhCollectQueryData, PdhGetFormattedCounterValue,
    PdhOpenQueryW, PDH_FMT_COUNTERVALUE, PDH_FMT_DOUBLE, PDH_HCOUNTER, PDH_HQUERY,
};

pub struct HardwareMonitor {
    query: PDH_HQUERY,
    cpu_counter: PDH_HCOUNTER,
    gpu_counter: Option<PDH_HCOUNTER>,
}

unsafe impl Send for HardwareMonitor {}
unsafe impl Sync for HardwareMonitor {}

impl HardwareMonitor {
    pub fn new() -> Option<Self> {
        unsafe {
            let mut query = PDH_HQUERY::default();
            if PdhOpenQueryW(PCWSTR::null(), 0, &mut query) != 0 {
                return None;
            }

            let mut cpu_counter = PDH_HCOUNTER::default();
            // Contador estándar de CPU disponible en todo Windows
            let cpu_path: Vec<u16> = "\\Processor(_Total)\\% Processor Time\0"
                .encode_utf16()
                .collect();
            if PdhAddEnglishCounterW(query, PCWSTR(cpu_path.as_ptr()), 0, &mut cpu_counter) != 0 {
                let _ = PdhCloseQuery(query);
                return None;
            }

            // Contador de GPU si está disponible
            let mut gpu_counter = PDH_HCOUNTER::default();
            let gpu_path: Vec<u16> = "\\GPU Engine(*)\\Utilization Percentage\0"
                .encode_utf16()
                .collect();
            let has_gpu = PdhAddEnglishCounterW(query, PCWSTR(gpu_path.as_ptr()), 0, &mut gpu_counter) == 0;

            // Recolectar la primera muestra (PDH requiere 2 muestras para calcular deltas)
            let _ = PdhCollectQueryData(query);

            Some(Self {
                query,
                cpu_counter,
                gpu_counter: if has_gpu { Some(gpu_counter) } else { None },
            })
        }
    }

    pub fn read(&self) -> HardwareState {
        unsafe {
            let _ = PdhCollectQueryData(self.query);

            let mut cpu_val = PDH_FMT_COUNTERVALUE::default();
            let mut cpu_load = 0.0;
            if PdhGetFormattedCounterValue(self.cpu_counter, PDH_FMT_DOUBLE, None, &mut cpu_val) == 0 {
                cpu_load = cpu_val.Anonymous.doubleValue.clamp(0.0, 100.0);
            }

            let mut gpu_load = 0.0;
            if let Some(gpu_cnt) = self.gpu_counter {
                let mut gpu_val = PDH_FMT_COUNTERVALUE::default();
                if PdhGetFormattedCounterValue(gpu_cnt, PDH_FMT_DOUBLE, None, &mut gpu_val) == 0 {
                    gpu_load = gpu_val.Anonymous.doubleValue.clamp(0.0, 100.0);
                }
            }

            HardwareState {
                cpu: format!("{:.0}%", cpu_load.round()),
                gpu: format!("{:.0}%", gpu_load.round()),
                cpu_value: Some(cpu_load),
                gpu_value: Some(gpu_load),
                cpu_kind: "load".into(),
                gpu_kind: "load".into(),
                cpu_source: "PDH Win32".into(),
                gpu_source: "PDH Win32".into(),
            }
        }
    }
}

impl Drop for HardwareMonitor {
    fn drop(&mut self) {
        unsafe {
            let _ = PdhCloseQuery(self.query);
        }
    }
}

pub fn start_hardware_worker(
    app: AppHandle,
    running: Arc<AtomicBool>,
    refresh_seconds: u32,
) {
    std::thread::spawn(move || {
        let monitor = match HardwareMonitor::new() {
            Some(m) => m,
            None => {
                log::warn!("Could not initialize PDH hardware counters");
                return;
            }
        };

        let interval = Duration::from_secs(refresh_seconds.max(1) as u64);

        while running.load(Ordering::Relaxed) {
            std::thread::sleep(interval);
            let state = monitor.read();
            let _ = app.emit("ln:hardware", state);
        }
    });
}
