use crate::types::HitRect;
use std::sync::atomic::{AtomicBool, Ordering};
use std::sync::{Arc, Mutex};
use tauri::{AppHandle, Emitter, WebviewWindow};
use windows::core::PWSTR;
use windows::Win32::Foundation::{HWND, LPARAM, LRESULT, POINT, RECT, WPARAM};
use windows::Win32::Graphics::Gdi::{
    GetMonitorInfoW, MonitorFromPoint, MonitorFromWindow, HMONITOR, MONITORINFO,
    MONITOR_DEFAULTTONEAREST, MONITOR_DEFAULTTOPRIMARY,
};
use windows::Win32::UI::HiDpi::GetDpiForWindow;
use windows::Win32::UI::Shell::{SHQueryUserNotificationState, QUNS_PRESENTATION_MODE, QUNS_RUNNING_D3D_FULL_SCREEN};
use windows::Win32::UI::WindowsAndMessaging::{
    GetClientRect, GetForegroundWindow, GetWindowLongPtrW, GetWindowRect,
    RealGetWindowClassW, SetWindowLongPtrW, SetWindowPos, GWL_EXSTYLE, HWND_TOPMOST,
    SWP_NOACTIVATE, SWP_NOMOVE, SWP_NOSIZE, SWP_SHOWWINDOW, SWP_FRAMECHANGED,
    WS_EX_APPWINDOW, WS_EX_NOACTIVATE, WS_EX_TOOLWINDOW, WS_EX_TRANSPARENT,
};

pub struct WindowManager {
    hwnd: HWND,
    hit_rect: Arc<Mutex<Option<HitRect>>>,
    is_transparent: AtomicBool,
    dpi_scale: f64,
}

// Implementación segura para enviar HWND entre hilos
unsafe impl Send for WindowManager {}
unsafe impl Sync for WindowManager {}

impl WindowManager {
    pub fn new(window: &WebviewWindow) -> Result<Self, String> {
        let hwnd_raw = window
            .hwnd()
            .map_err(|e| format!("Error getting HWND: {}", e))?;
        let hwnd = HWND(hwnd_raw.0 as _);

        let mgr = Self {
            hwnd,
            hit_rect: Arc::new(Mutex::new(None)),
            is_transparent: AtomicBool::new(false),
            dpi_scale: 1.0,
        };

        mgr.apply_tool_window_styles();
        mgr.position_top_center();

        Ok(mgr)
    }

    /// Evita que la ventana tome el foco y la oculta de la barra de tareas y Alt+Tab.
    pub fn apply_tool_window_styles(&self) {
        unsafe {
            let style = GetWindowLongPtrW(self.hwnd, GWL_EXSTYLE);
            let mut new_style = style | (WS_EX_TOOLWINDOW.0 as isize) | (WS_EX_NOACTIVATE.0 as isize);
            new_style &= !(WS_EX_APPWINDOW.0 as isize);
            SetWindowLongPtrW(self.hwnd, GWL_EXSTYLE, new_style);
        }
    }

    /// Centra la ventana físicamente en el borde superior del monitor principal.
    pub fn position_top_center(&self) {
        unsafe {
            let primary_mon = MonitorFromPoint(POINT { x: 0, y: 0 }, MONITOR_DEFAULTTOPRIMARY);
            let mut mi = MONITORINFO {
                cbSize: std::mem::size_of::<MONITORINFO>() as u32,
                ..Default::default()
            };

            if GetMonitorInfoW(primary_mon, &mut mi).as_bool() {
                let mon_width = mi.rcMonitor.right - mi.rcMonitor.left;
                let mut win_rect = RECT::default();
                let _ = GetWindowRect(self.hwnd, &mut win_rect);
                let win_width = win_rect.right - win_rect.left;

                let x = mi.rcMonitor.left + (mon_width - win_width) / 2;
                let y = mi.rcMonitor.top;

                let _ = SetWindowPos(
                    self.hwnd,
                    Some(HWND_TOPMOST),
                    x,
                    y,
                    0,
                    0,
                    SWP_NOSIZE | SWP_NOACTIVATE | SWP_SHOWWINDOW,
                );
            }
        }
    }

    /// Actualiza el rectángulo de colisión interactivo reportado por la isla (en CSS pixels).
    pub fn update_hit_rect(&self, rect: HitRect) {
        let mut hr = self.hit_rect.lock().unwrap();
        *hr = Some(rect);
    }

    /// Aplica o remueve el estilo de transparencia de clics (click-through)
    pub fn set_click_through(&self, pass_through: bool) {
        let currently = self.is_transparent.load(Ordering::Relaxed);
        if currently == pass_through {
            return;
        }

        unsafe {
            let style = GetWindowLongPtrW(self.hwnd, GWL_EXSTYLE);
            let new_style = if pass_through {
                style | (WS_EX_TRANSPARENT.0 as isize)
            } else {
                style & !(WS_EX_TRANSPARENT.0 as isize)
            };
            SetWindowLongPtrW(self.hwnd, GWL_EXSTYLE, new_style);
            let _ = SetWindowPos(
                self.hwnd,
                Some(HWND::default()),
                0,
                0,
                0,
                0,
                SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE,
            );
            self.is_transparent.store(pass_through, Ordering::Relaxed);
        }
    }

    /// Comprueba si el cursor se encuentra dentro del área interactiva de la isla
    pub fn check_cursor_hover(&self, cursor_pt: POINT) -> bool {
        let hr_opt = self.hit_rect.lock().unwrap();
        if let Some(ref r) = *hr_opt {
            unsafe {
                let mut win_rect = RECT::default();
                if GetWindowRect(self.hwnd, &mut win_rect).is_ok() {
                    let dpi = GetDpiForWindow(self.hwnd);
                    let scale = if dpi > 0 { dpi as f64 / 96.0 } else { 1.0 };

                    let phys_x = win_rect.left + (r.x as f64 * scale).round() as i32;
                    let phys_y = win_rect.top + (r.y as f64 * scale).round() as i32;
                    let phys_w = (r.width as f64 * scale).round() as i32;
                    let phys_h = (r.height as f64 * scale).round() as i32;

                    return cursor_pt.x >= phys_x
                        && cursor_pt.x <= phys_x + phys_w
                        && cursor_pt.y >= phys_y
                        && cursor_pt.y <= phys_y + phys_h;
                }
            }
        }
        false
    }
}

/// Tarea en segundo plano para detección de pantalla completa (Juegos, YouTube F11, etc.)
pub fn start_fullscreen_detector(app: AppHandle, is_hidden_setting: Arc<AtomicBool>) {
    std::thread::spawn(move || {
        let mut last_state = false;
        loop {
            std::thread::sleep(std::time::Duration::from_millis(800));
            if !is_hidden_setting.load(Ordering::Relaxed) {
                if last_state {
                    last_state = false;
                    let _ = app.emit("ln:hidden", false);
                }
                continue;
            }

            let is_fullscreen = detect_fullscreen();
            if is_fullscreen != last_state {
                last_state = is_fullscreen;
                let _ = app.emit("ln:hidden", is_fullscreen);
            }
        }
    });
}

fn detect_fullscreen() -> bool {
    unsafe {
        if let Ok(state) = SHQueryUserNotificationState() {
            if state == QUNS_RUNNING_D3D_FULL_SCREEN || state == QUNS_PRESENTATION_MODE {
                return true;
            }
        }

        // 2. Detección por tamaño de la ventana activa en primer plano
        let hwnd = GetForegroundWindow();
        if hwnd.0.is_null() {
            return false;
        }

        // Ignorar ventanas del sistema/escritorio
        let mut class_buf = [0u16; 256];
        let len = RealGetWindowClassW(hwnd, &mut class_buf);
        if len > 0 {
            let class_name = String::from_utf16_lossy(&class_buf[..len as usize]);
            if class_name == "Progman"
                || class_name == "WorkerW"
                || class_name == "Shell_TrayWnd"
                || class_name == "Shell_SecondaryTrayWnd"
            {
                return false;
            }
        }

        let mon = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);
        if mon.0.is_null() {
            return false;
        }

        let mut mi = MONITORINFO {
            cbSize: std::mem::size_of::<MONITORINFO>() as u32,
            ..Default::default()
        };

        if !GetMonitorInfoW(mon, &mut mi).as_bool() {
            return false;
        }

        let mut client_rect = RECT::default();
        if !GetClientRect(hwnd, &mut client_rect).is_ok() {
            return false;
        }

        let mut top_left = POINT {
            x: client_rect.left,
            y: client_rect.top,
        };
        let mut bottom_right = POINT {
            x: client_rect.right,
            y: client_rect.bottom,
        };

        let _ = windows::Win32::Graphics::Gdi::ClientToScreen(hwnd, &mut top_left);
        let _ = windows::Win32::Graphics::Gdi::ClientToScreen(hwnd, &mut bottom_right);

        // Si la ventana cubre el monitor completo es fullscreen
        top_left.x <= mi.rcMonitor.left
            && top_left.y <= mi.rcMonitor.top
            && bottom_right.x >= mi.rcMonitor.right
            && bottom_right.y >= mi.rcMonitor.bottom
    }
}
