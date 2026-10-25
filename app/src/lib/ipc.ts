import { invoke } from "@tauri-apps/api/core";
import { listen, type UnlistenFn } from "@tauri-apps/api/event";
import type {
  AppEntry,
  BatteryState,
  DownloadItem,
  HardwareState,
  HitRect,
  IntegrationStatus,
  MediaEvent,
  Settings,
  Snapshot,
  Toggles,
  UsbEvent,
  WeatherState,
  WinNotification,
} from "./types";

/** Mapa de eventos emitidos por el backend Rust. */
export interface BackendEvents {
  "ln:media": MediaEvent;
  "ln:battery": BatteryState;
  "ln:weather": WeatherState;
  "ln:hardware": HardwareState;
  "ln:usb": UsbEvent;
  "ln:notification": WinNotification;
  "ln:downloads": DownloadItem[];
  "ln:hidden": boolean;
  "ln:hover": boolean;
  "ln:toggles": Toggles;
  "ln:location": string;
  "ln:settings": Settings;
}

export function on<K extends keyof BackendEvents>(
  event: K,
  handler: (payload: BackendEvents[K]) => void,
): Promise<UnlistenFn> {
  return listen<BackendEvents[K]>(event, (e) => handler(e.payload));
}

export type MediaAction = "play" | "pause" | "next" | "previous";

export const api = {
  snapshot: () => invoke<Snapshot>("get_snapshot"),
  setHitRect: (rect: HitRect) => invoke<void>("set_hit_rect", { rect }),
  setViewHint: (expanded: boolean, page: string) => invoke<void>("set_view_hint", { expanded, page }),
  media: (action: MediaAction) => invoke<void>("media_control", { action }),
  launchApp: (slot: string) => invoke<void>("launch_app", { slot }),
  appEntries: () => invoke<AppEntry[]>("get_app_entries"),
  setWifi: (enabled: boolean) => invoke<boolean>("set_wifi", { enabled }),
  setBluetooth: (enabled: boolean) => invoke<boolean>("set_bluetooth", { enabled }),
  setDarkMode: (enabled: boolean) => invoke<boolean>("set_dark_mode", { enabled }),
  setDnd: (enabled: boolean) => invoke<boolean>("set_dnd", { enabled }),
  getSettings: () => invoke<Settings>("get_settings"),
  saveSettings: (settings: Settings) => invoke<Settings>("save_settings", { settings }),
  detectLocationIp: () => invoke<[number, number]>("detect_location_ip"),
  requestExtensionLocation: () => invoke<boolean>("request_extension_location"),
  integrationStatus: () => invoke<IntegrationStatus>("get_integration_status"),
  registerNativeHost: () => invoke<boolean>("register_native_host"),
  openSettings: () => invoke<void>("open_settings"),
  openExternal: (target: string) => invoke<void>("open_external", { target }),
  quit: () => invoke<void>("quit_app"),
};
