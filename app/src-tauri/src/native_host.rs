use std::io::{self, Read, Write};
use std::path::PathBuf;
use winreg::enums::*;
use winreg::RegKey;

const HOST_NAME: &str = "com.locknotch.downloads";
const PIPE_NAME: &str = r"\\.\pipe\LockNotchDownloadsPipe";
const EXTENSION_ID: &str = "kfmdodbaimfnbghpklkhlcgleffjikge";

pub fn is_native_host_call() -> bool {
    std::env::args().any(|arg| arg.starts_with("chrome-extension://"))
}

pub fn run_native_host_proxy() {
    let rt = match tokio::runtime::Builder::new_current_thread()
        .enable_all()
        .build()
    {
        Ok(r) => r,
        Err(_) => return,
    };

    rt.block_on(async {
        use tokio::io::{AsyncReadExt, AsyncWriteExt};
        use tokio::net::windows::named_pipe::ClientOptions;

        let client = match ClientOptions::new().open(PIPE_NAME) {
            Ok(c) => c,
            Err(_) => return,
        };

        let (mut pipe_read, mut pipe_write) = tokio::io::split(client);

        // Hilo 1: Pipe hacia stdout (mensajes de LockNotch a Chrome)
        tokio::spawn(async move {
            let mut stdout = io::stdout();
            let mut len_buf = [0u8; 4];
            while pipe_read.read_exact(&mut len_buf).await.is_ok() {
                let len = u32::from_le_bytes(len_buf) as usize;
                let mut data = vec![0u8; len];
                if pipe_read.read_exact(&mut data).await.is_ok() {
                    let _ = stdout.write_all(&len_buf);
                    let _ = stdout.write_all(&data);
                    let _ = stdout.flush();
                } else {
                    break;
                }
            }
        });

        // Hilo 2: stdin hacia Pipe (mensajes de Chrome a LockNotch)
        let mut stdin = io::stdin();
        let mut len_buf = [0u8; 4];
        while stdin.read_exact(&mut len_buf).is_ok() {
            let len = u32::from_le_bytes(len_buf) as usize;
            if len == 0 || len > 4 * 1024 * 1024 {
                break;
            }
            let mut data = vec![0u8; len];
            if stdin.read_exact(&mut data).is_ok() {
                if pipe_write.write_all(&len_buf).await.is_err() {
                    break;
                }
                if pipe_write.write_all(&data).await.is_err() {
                    break;
                }
                let _ = pipe_write.flush().await;
            } else {
                break;
            }
        }
    });
}

pub fn register_native_host() -> Result<bool, String> {
    let current_exe = std::env::current_exe().map_err(|e| e.to_string())?;
    let manifest_dir = current_exe
        .parent()
        .unwrap_or_else(|| std::path::Path::new("C:\\"));
    let manifest_path = manifest_dir.join(format!("{}.json", HOST_NAME));

    let manifest_content = serde_json::json!({
        "name": HOST_NAME,
        "description": "LockNotch Native Messaging Host",
        "path": current_exe.to_string_lossy(),
        "type": "stdio",
        "allowed_origins": [
            format!("chrome-extension://{}/", EXTENSION_ID)
        ]
    });

    let json_str = serde_json::to_string_pretty(&manifest_content).map_err(|e| e.to_string())?;
    std::fs::write(&manifest_path, json_str).map_err(|e| e.to_string())?;

    let hkcu = RegKey::predef(HKEY_CURRENT_USER);
    let key_path = format!(r"Software\Google\Chrome\NativeMessagingHosts\{}", HOST_NAME);
    let (key, _) = hkcu.create_subkey(&key_path).map_err(|e| e.to_string())?;
    key.set_value("", &manifest_path.to_string_lossy().to_string())
        .map_err(|e| e.to_string())?;

    Ok(true)
}

pub fn is_native_host_registered() -> bool {
    let hkcu = RegKey::predef(HKEY_CURRENT_USER);
    let key_path = format!(r"Software\Google\Chrome\NativeMessagingHosts\{}", HOST_NAME);
    if let Ok(key) = hkcu.open_subkey(&key_path) {
        if let Ok(path) = key.get_value::<String, _>("") {
            return PathBuf::from(path).exists();
        }
    }
    false
}
