use crate::types::{AppEntry, IconData};
use base64::Engine;
use std::os::windows::process::CommandExt;
use std::process::Command;
use windows::core::PCWSTR;
use windows::Win32::Graphics::Gdi::{
    CreateCompatibleDC, DeleteDC, DeleteObject, GetDIBits, GetObjectW, SelectObject, BITMAP,
    BITMAPINFO, BITMAPINFOHEADER, DIB_RGB_COLORS, HDC, HGDIOBJ,
};
use windows::Win32::UI::Shell::ExtractIconExW;
use windows::Win32::UI::WindowsAndMessaging::{DestroyIcon, GetIconInfo, HICON, ICONINFO};

const CREATE_NO_WINDOW: u32 = 0x08000000;

pub struct AppLauncher;

impl AppLauncher {
    pub fn launch(target: &str) -> Result<(), String> {
        if target.is_empty() {
            return Ok(());
        }

        // Si es URI de Windows (ej: ms-settings:), abrir mediante explorer o cmd
        if target.contains(':') && !target.contains('\\') && !target.contains('/') {
            let _ = Command::new("cmd")
                .args(["/c", "start", "", target])
                .creation_flags(CREATE_NO_WINDOW)
                .spawn();
            return Ok(());
        }

        let _ = Command::new("cmd")
            .args(["/c", "start", "", target])
            .creation_flags(CREATE_NO_WINDOW)
            .spawn();

        Ok(())
    }

    pub fn get_app_entries(slots: &[(&str, &str)]) -> Vec<AppEntry> {
        slots
            .iter()
            .map(|(slot, path)| {
                let name = get_app_name(path);
                let icon = extract_icon(path);
                AppEntry {
                    slot: slot.to_string(),
                    path: path.to_string(),
                    name,
                    icon,
                }
            })
            .collect()
    }
}

fn get_app_name(path: &str) -> String {
    if path.starts_with("ms-settings:") {
        return "Configuración".into();
    }
    let p = std::path::Path::new(path);
    if let Some(stem) = p.file_stem() {
        return stem.to_string_lossy().to_string();
    }
    path.to_string()
}

fn extract_icon(path: &str) -> Option<IconData> {
    if !path.ends_with(".exe") && !path.ends_with(".ico") {
        return None;
    }

    unsafe {
        let wide: Vec<u16> = path.encode_utf16().chain(std::iter::once(0)).collect();
        let mut large_icon = HICON::default();
        let num_extracted = ExtractIconExW(PCWSTR(wide.as_ptr()), 0, Some(&mut large_icon), None, 1);

        if num_extracted == 0 || large_icon.0.is_null() {
            return None;
        }

        let mut icon_info = ICONINFO::default();
        if GetIconInfo(large_icon, &mut icon_info).is_err() {
            let _ = DestroyIcon(large_icon);
            return None;
        }

        let hdc = CreateCompatibleDC(Some(HDC::default()));
        let mut bm = BITMAP::default();
        GetObjectW(
            HGDIOBJ(icon_info.hbmColor.0),
            std::mem::size_of::<BITMAP>() as i32,
            Some(&mut bm as *mut _ as *mut _),
        );

        let width = bm.bmWidth as u32;
        let height = bm.bmHeight as u32;
        if width == 0 || height == 0 {
            let _ = DeleteDC(hdc);
            let _ = DeleteObject(HGDIOBJ(icon_info.hbmColor.0));
            let _ = DeleteObject(HGDIOBJ(icon_info.hbmMask.0));
            let _ = DestroyIcon(large_icon);
            return None;
        }

        let mut bi = BITMAPINFO {
            bmiHeader: BITMAPINFOHEADER {
                biSize: std::mem::size_of::<BITMAPINFOHEADER>() as u32,
                biWidth: width as i32,
                biHeight: -(height as i32), // Top-down
                biPlanes: 1,
                biBitCount: 32,
                biCompression: 0,
                ..Default::default()
            },
            ..Default::default()
        };

        let mut pixels = vec![0u8; (width * height * 4) as usize];
        GetDIBits(
            hdc,
            icon_info.hbmColor,
            0,
            height,
            Some(pixels.as_mut_ptr() as *mut _),
            &mut bi,
            DIB_RGB_COLORS,
        );

        // Convertir BGRA a RGBA
        for chunk in pixels.chunks_exact_mut(4) {
            chunk.swap(0, 2);
        }

        // Codificar en PNG en memoria
        let mut png_bytes = Vec::new();
        let encoder = image::codecs::png::PngEncoder::new(&mut png_bytes);
        let _ = image::ImageEncoder::write_image(
            encoder,
            &pixels,
            width,
            height,
            image::ExtendedColorType::Rgba8,
        );

        let b64 = base64::engine::general_purpose::STANDARD.encode(&png_bytes);

        let _ = DeleteDC(hdc);
        let _ = DeleteObject(HGDIOBJ(icon_info.hbmColor.0));
        let _ = DeleteObject(HGDIOBJ(icon_info.hbmMask.0));
        let _ = DestroyIcon(large_icon);

        Some(IconData {
            width,
            height,
            rgba: b64,
        })
    }
}
