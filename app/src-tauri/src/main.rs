#![cfg_attr(not(debug_assertions), windows_subsystem = "windows")]

fn main() {
    std::panic::set_hook(Box::new(|info| {
        let path = std::env::temp_dir().join("locknotch_panic.txt");
        let _ = std::fs::write(path, format!("{}", info));
    }));

    let rt = tokio::runtime::Runtime::new().expect("Failed to create Tokio runtime");
    let _guard = rt.enter();
    locknotch_lib::run();
}
