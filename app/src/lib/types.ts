// Contratos compartidos con el backend Rust (serde camelCase, salvo Settings que
// conserva PascalCase para seguir siendo compatible con el settings.json original).

export interface MediaState {
  title: string;
  artist: string;
  album: string;
  isPlaying: boolean;
  sourceApp: string;
  positionMs: number;
  endMs: number;
  /** Instante (epoch ms) en que Windows midió `positionMs`. */
  lastUpdatedMs: number;
  trackKey: string;
  /** Clave de la portada; cambia sólo cuando cambia la imagen. */
  artKey: string | null;
  /** data: URL de la portada. Sólo viaja cuando `artKey` cambia (o en el snapshot). */
  thumbnail: string | null;
}

export interface MediaEvent {
  media: MediaState | null;
  trackChanged: boolean;
}

export interface BatteryState {
  percent: number;
  charging: boolean;
  low: boolean;
  hasBattery: boolean;
}

export interface WeatherState {
  temperature: string;
  glyph: string;
  code: number;
}

export type SensorKind = "temp" | "load" | "none";

export interface HardwareState {
  cpu: string;
  gpu: string;
  cpuValue: number | null;
  gpuValue: number | null;
  cpuKind: SensorKind;
  gpuKind: SensorKind;
  cpuSource: string;
  gpuSource: string;
}

export interface UsbEvent {
  deviceId: string;
  friendlyName: string;
  kind: string;
  details: string;
  glyph: string;
  connected: boolean;
}

export interface WinNotification {
  appName: string;
  title: string;
  body: string;
}

export interface DownloadItem {
  type: string;
  downloadId: number;
  filename: string;
  state: string;
  totalBytes: number;
  bytesReceived: number;
  progress: number;
  speed: number;
  url: string;
  timestamp: number;
}

export interface Toggles {
  wifi: boolean;
  bluetooth: boolean;
  darkMode: boolean;
  dnd: boolean;
}

export interface Settings {
  AppLauncher1: string;
  AppLauncher2: string;
  AppLauncher3: string;
  AppLauncher4: string;
  AppLauncher5: string;
  StartWithWindows: boolean;
  HideInFullscreen: boolean;
  WeatherLatitude: number;
  WeatherLongitude: number;
  WeatherUseFahrenheit: boolean;
  WeatherShowPeriodicLocation: boolean;
  HardwareRefreshIntervalSeconds: number;
  Theme: "Dark" | "Light" | string;
  FontFamily: string;
  AmbientGlow: boolean;
  ReduceMotion: boolean;
}

export interface Snapshot {
  media: MediaState | null;
  battery: BatteryState | null;
  weather: WeatherState | null;
  hardware: HardwareState | null;
  downloads: DownloadItem[];
  toggles: Toggles;
  settings: Settings;
  locationName: string | null;
  hidden: boolean;
}

export interface IconData {
  width: number;
  height: number;
  /** RGBA en base64. */
  rgba: string;
}

export interface AppEntry {
  slot: string;
  path: string;
  name: string;
  icon: IconData | null;
}

export interface IntegrationStatus {
  nativeHostRegistered: boolean;
  extensionConnected: boolean;
  notificationAccess: string;
  cpuSource: string;
  gpuSource: string;
  version: string;
  exePath: string;
  settingsPath: string;
}

export interface HitRect {
  x: number;
  y: number;
  width: number;
  height: number;
}
