This file is a merged representation of the entire codebase, combined into a single document by Repomix.
The content has been processed where comments have been removed, empty lines have been removed, content has been compressed (code blocks are separated by ⋮---- delimiter).

# File Summary

## Purpose
This file contains a packed representation of the entire repository's contents.
It is designed to be easily consumable by AI systems for analysis, code review,
or other automated processes.

## File Format
The content is organized as follows:
1. This summary section
2. Repository information
3. Directory structure
4. Repository files (if enabled)
5. Multiple file entries, each consisting of:
  a. A header with the file path (## File: path/to/file)
  b. The full contents of the file in a code block

## Usage Guidelines
- This file should be treated as read-only. Any changes should be made to the
  original repository files, not this packed version.
- When processing this file, use the file path to distinguish
  between different files in the repository.
- Be aware that this file may contain sensitive information. Handle it with
  the same level of security as you would the original repository.

## Notes
- Some files may have been excluded based on .gitignore rules and Repomix's configuration
- Binary files are not included in this packed representation. Please refer to the Repository Structure section for a complete list of file paths, including binary files
- Files matching patterns in .gitignore are excluded
- Files matching default ignore patterns are excluded
- Code comments have been removed from supported file types
- Empty lines have been removed from all files
- Content has been compressed - code blocks are separated by ⋮---- delimiter
- Files are sorted by Git change count (files with more changes are at the bottom)

# Directory Structure
````
app/
  src/
    assets/
      fonts/
        Audiowide.ttf
        Manrope.ttf
        NothingDots.otf
        Oxanium.ttf
        SpaceGrotesk.ttf
    lib/
      format.ts
      ipc.ts
      islandController.ts
      notificationQueue.ts
      spring.ts
      types.ts
    styles/
      fonts.css
      island.css
      settings.css
    island.ts
    settings.ts
  src-tauri/
    capabilities/
      default.json
    gen/
      schemas/
        acl-manifests.json
        capabilities.json
        desktop-schema.json
        windows-schema.json
    icons/
      128x128.png
      128x128@2x.png
      32x32.png
      64x64.png
      icon.icns
      icon.ico
      icon.png
      Square107x107Logo.png
      Square142x142Logo.png
      Square150x150Logo.png
      Square284x284Logo.png
      Square30x30Logo.png
      Square310x310Logo.png
      Square44x44Logo.png
      Square71x71Logo.png
      Square89x89Logo.png
      StoreLogo.png
    src/
      apps.rs
      battery.rs
      downloads.rs
      hardware.rs
      lib.rs
      main.rs
      media.rs
      native_host.rs
      notifications.rs
      radios.rs
      settings.rs
      types.rs
      usb.rs
      weather.rs
      window.rs
    build.rs
    Cargo.toml
    run_log.txt
    tauri.conf.json
  tests/
    format.test.ts
    islandController.test.ts
    spring.test.ts
  index.html
  package.json
  settings.html
  tsconfig.json
  vite.config.ts
KeyGen/
  KeyGen.csproj
  Program.cs
lockNotch_chrome/
  background.js
  com.locknotch.downloads.json
  install_host.bat
  manifest.json
  offscreen.html
  offscreen.js
src/
  LockNotch/
    Controls/
      MarqueeTextBlock.cs
    Converters/
      BoolToStringConverter.cs
    Helpers/
      AppearanceManager.cs
      NotchHelper.cs
      ProgressBarExtensions.cs
    Interop/
      NativeMethods.cs
      WindowHelper.cs
    Models/
      IslandState.cs
      MediaInfo.cs
      NotificationRequest.cs
    Resources/
      Fonts/
        Audiowide.ttf
        Manrope.ttf
        NothingDots.otf
        Oxanium.ttf
        SpaceGrotesk.ttf
      Themes/
        Dark.xaml
        Light.xaml
      icon.ico
      icon.png
      Styles.xaml
    Services/
      BatteryService.cs
      ClockService.cs
      DownloadService.cs
      FullscreenService.cs
      HardwareService.cs
      IBatteryService.cs
      IClockService.cs
      IFullscreenService.cs
      IHardwareService.cs
      IMediaService.cs
      INotificationService.cs
      IUsbService.cs
      IWeatherService.cs
      MediaService.cs
      NativeMessagingProxy.cs
      NotificationService.cs
      SettingsService.cs
      UsbService.cs
      WeatherService.cs
    ViewModels/
      IslandViewModel.cs
    app.manifest
    App.xaml
    App.xaml.cs
    LockNotch.csproj
    MainWindow.xaml
    MainWindow.xaml.cs
    SettingsWindow.xaml
    SettingsWindow.xaml.cs
  nothing-font-5x7.otf/
    license.txt
    nothing-font-5x7.otf
    readme.txt
  nothing-font-5x7.otf.zip
  Roboto,Space_Grotesk.zip
.gitignore
arreglar_locknotch.bat
automatizar.bat
dev.bat
Directory.Build.props
find_iscc.ps1
iniciar_locknotch.bat
Instalar_LockNotch.bat
Instalar_LockNotch.ps1
ispack.exe
LockNotch.sln
README.md
run_log.txt
Setup.iss
````

# Files

## File: app/src-tauri/run_log.txt
````
main started
calling locknotch_lib::run
````

## File: iniciar_locknotch.bat
````batch
@echo off
cd /d "%~dp0"
title LockNotch Launcher
echo Cerrando instancias previas...
taskkill /F /IM locknotch.exe >nul 2>&1
taskkill /F /IM LockNotch.exe >nul 2>&1
ping 127.0.0.1 -n 2 >nul
echo Iniciando LockNotch (Tauri Release)...
start "" "%~dp0app\src-tauri\target\release\locknotch.exe"
echo LockNotch iniciado correctamente en la parte superior de tu pantalla.
ping 127.0.0.1 -n 3 >nul
````

## File: run_log.txt
````
main started
calling locknotch_lib::run
main started
calling locknotch_lib::run
main started
calling locknotch_lib::run
main started
calling locknotch_lib::run
````

## File: app/src/lib/format.ts
````typescript
import type { DownloadItem, MediaState } from "./types";
⋮----
export function formatDuration(ms: number): string
⋮----
export function currentPosition(media: MediaState, now: number = Date.now()): number
⋮----
export function progressFraction(media: MediaState, now: number = Date.now()): number
⋮----
export function batteryGlyph(percent: number, charging: boolean): string
⋮----
export function batteryText(percent: number): string
⋮----
export interface ClockParts {
  time: string;
  period: string;
}
⋮----
export function clockParts(date: Date, locale?: string): ClockParts
⋮----
export function speedText(speed: number): string
⋮----
export function isTerminal(d: DownloadItem): boolean
⋮----
export function downloadDetail(d: DownloadItem): string
⋮----
export interface DownloadSummary {
  compactText: string;
  title: string;
  progress: number;
  progressText: string;
}
⋮----
/** Réplica de OnDownloadsChanged del ViewModel original. */
export function summarizeDownloads(active: DownloadItem[]): DownloadSummary
⋮----
export function truncate(text: string, max = 40): string
⋮----
export function notificationGlyph(appName: string): string
⋮----
export function notificationText(title: string, body: string): string
⋮----
export function gaugeFraction(value: number | null, kind: "temp" | "load" | "none"): number
⋮----
export function gaugeLevel(value: number | null, kind: "temp" | "load" | "none"): "ok" | "warm" | "hot"
````

## File: app/src/lib/ipc.ts
````typescript
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
⋮----
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
⋮----
export function on<K extends keyof BackendEvents>(
  event: K,
  handler: (payload: BackendEvents[K]) => void,
): Promise<UnlistenFn>
⋮----
export type MediaAction = "play" | "pause" | "next" | "previous";
````

## File: app/src/lib/islandController.ts
````typescript
import type { TimerApi } from "./notificationQueue";
⋮----
export type IslandMode = "hidden" | "notification" | "expanded" | "compact";
⋮----
export class IslandController
⋮----
constructor(
⋮----
get mode(): IslandMode
⋮----
get isPointerOver(): boolean
⋮----
pointerEntered(): void
⋮----
pointerExited(): void
⋮----
click(): void
⋮----
expandTemporarily(ms: number): void
⋮----
collapse(): void
⋮----
setHidden(hidden: boolean): void
⋮----
setNotifying(notifying: boolean): void
⋮----
private setExpanded(value: boolean): void
⋮----
private scheduleCollapse(ms: number): void
⋮----
private cancelCollapse(): void
⋮----
private emitIfChanged(before: IslandMode): void
⋮----
export type PageId = "media" | "apps" | "control" | "hardware" | "downloads";
⋮----
export class Pager
⋮----
constructor(private readonly onChange: (page: PageId, index: number, count: number) => void)
⋮----
get pages(): PageId[]
⋮----
get count(): number
⋮----
get current(): PageId
⋮----
get currentIndex(): number
⋮----
next(): void
⋮----
previous(): void
⋮----
go(index: number): void
⋮----
goTo(page: PageId): void
⋮----
setHasDownloads(has: boolean): void
⋮----
private emit(): void
⋮----
export class WheelPager
⋮----
feed(delta: number, now: number): number
⋮----
reset(): void
````

## File: app/src/lib/notificationQueue.ts
````typescript
export type Tone = "accent" | "primary" | "secondary" | "danger";
⋮----
export interface NotificationRequest {
  id: string;
  glyph: string;
  text: string;
  tone: Tone;
  durationMs: number;
  image?: string | null;
}
⋮----
export interface TimerApi {
  set(fn: () => void, ms: number): unknown;
  clear(handle: unknown): void;
}
⋮----
set(fn: ()
clear(handle: unknown): void;
⋮----
export class NotificationQueue
⋮----
constructor(
⋮----
get visible(): NotificationRequest | null
⋮----
get pending(): readonly NotificationRequest[]
⋮----
enqueue(req: NotificationRequest): void
⋮----
dismiss(): void
⋮----
clear(): void
⋮----
private next(): void
⋮----
private finishCurrent(): void
⋮----
private arm(ms: number, fn: () => void): void
⋮----
private disarm(): void
````

## File: app/src/lib/spring.ts
````typescript
export interface SpringConfig {
  stiffness: number;
  damping: number;
  mass: number;
}
⋮----
export class Spring
⋮----
constructor(initial: number, config: SpringConfig = SpringPresets.bouncy)
⋮----
step(dt: number): void
⋮----
get settled(): boolean
⋮----
snap(): void
⋮----
export type FrameScheduler = {
  request(cb: (t: number) => void): number;
  cancel(id: number): void;
};
⋮----
request(cb: (t: number)
cancel(id: number): void;
⋮----
export class SpringGroup<K extends string>
⋮----
constructor(
    initial: Record<K, number>,
    private readonly onFrame: (values: Record<K, number>) => void,
    private readonly onSettle: () => void = () => {},
    private readonly scheduler: FrameScheduler = rafScheduler,
)
⋮----
get animating(): boolean
⋮----
values(): Record<K, number>
⋮----
targets(): Record<K, number>
⋮----
animateTo(targets: Partial<Record<K, number>>, config: SpringConfig, immediate = false): void
⋮----
stop(): void
````

## File: app/src/lib/types.ts
````typescript
export interface MediaState {
  title: string;
  artist: string;
  album: string;
  isPlaying: boolean;
  sourceApp: string;
  positionMs: number;
  endMs: number;
  lastUpdatedMs: number;
  trackKey: string;
  artKey: string | null;
  thumbnail: string | null;
}
⋮----
export interface MediaEvent {
  media: MediaState | null;
  trackChanged: boolean;
}
⋮----
export interface BatteryState {
  percent: number;
  charging: boolean;
  low: boolean;
  hasBattery: boolean;
}
⋮----
export interface WeatherState {
  temperature: string;
  glyph: string;
  code: number;
}
⋮----
export type SensorKind = "temp" | "load" | "none";
⋮----
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
⋮----
export interface UsbEvent {
  deviceId: string;
  friendlyName: string;
  kind: string;
  details: string;
  glyph: string;
  connected: boolean;
}
⋮----
export interface WinNotification {
  appName: string;
  title: string;
  body: string;
}
⋮----
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
⋮----
export interface Toggles {
  wifi: boolean;
  bluetooth: boolean;
  darkMode: boolean;
  dnd: boolean;
}
⋮----
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
⋮----
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
⋮----
export interface IconData {
  width: number;
  height: number;
  rgba: string;
}
⋮----
export interface AppEntry {
  slot: string;
  path: string;
  name: string;
  icon: IconData | null;
}
⋮----
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
⋮----
export interface HitRect {
  x: number;
  y: number;
  width: number;
  height: number;
}
````

## File: app/src/styles/fonts.css
````css
@font-face {
````

## File: app/src/styles/island.css
````css
:root {
⋮----
[data-theme='light'] {
⋮----
* {
⋮----
html, body {
⋮----
#overlay-root {
⋮----
#island {
⋮----
#island.ambient-glow {
⋮----
.concave-ear {
⋮----
.concave-ear.left {
⋮----
.concave-ear.right {
⋮----
#vinyl-disc-host {
⋮----
#vinyl-disc-host.visible {
⋮----
.vinyl-disc {
⋮----
.vinyl-disc.spinning {
⋮----
.vinyl-grooves {
⋮----
.vinyl-sheen {
⋮----
.vinyl-label {
⋮----
.vinyl-label img {
⋮----
.vinyl-spindle {
⋮----
.island-view {
⋮----
.island-view.hidden {
⋮----
#view-compact {
⋮----
.compact-subview {
⋮----
.compact-subview.hidden {
⋮----
.compact-left, .compact-right {
⋮----
.compact-time {
⋮----
.compact-weather-glyph {
⋮----
.compact-weather-temp {
⋮----
.compact-battery-text {
⋮----
.compact-battery-icon {
⋮----
.compact-battery-icon.charging {
⋮----
.compact-battery-icon.low {
⋮----
.media-thumb-mini {
⋮----
.equalizer-bars {
⋮----
.eq-bar {
⋮----
.equalizer-bars.animating .eq-bar:nth-child(1) {
.equalizer-bars.animating .eq-bar:nth-child(2) {
.equalizer-bars.animating .eq-bar:nth-child(3) {
.equalizer-bars.animating .eq-bar:nth-child(4) {
⋮----
#view-notification {
⋮----
.notif-icon {
⋮----
.notif-text {
⋮----
#view-expanded {
⋮----
.carousel-viewport {
⋮----
.carousel-page {
⋮----
.carousel-page.page-left {
⋮----
.carousel-page.page-right {
⋮----
.carousel-page.page-active {
⋮----
.media-meta {
⋮----
.media-artist {
⋮----
.media-title {
⋮----
.media-progress-bar {
⋮----
.media-progress-fill {
⋮----
.media-times {
⋮----
.media-controls {
⋮----
.media-btn {
⋮----
.media-btn:hover {
⋮----
.media-btn:active {
⋮----
.media-btn.play-pause {
⋮----
.page-title {
⋮----
.apps-grid {
⋮----
.app-slot {
⋮----
.app-slot:hover {
⋮----
.app-slot:active {
⋮----
.app-slot img {
⋮----
.app-slot .app-fallback-icon {
⋮----
.control-grid {
⋮----
.control-toggle {
⋮----
.control-toggle.active {
⋮----
.control-toggle.active .toggle-icon,
⋮----
.control-toggle:hover:not(.active) {
⋮----
.toggle-icon {
⋮----
.toggle-label {
⋮----
.hardware-cards {
⋮----
.telemetry-card {
⋮----
.telemetry-header {
⋮----
.telemetry-label {
⋮----
.telemetry-value {
⋮----
.telemetry-bar-bg {
⋮----
.telemetry-bar-fill {
⋮----
.telemetry-bar-fill.warm {
⋮----
.telemetry-bar-fill.hot {
⋮----
.downloads-list {
⋮----
.download-item-card {
⋮----
.download-item-row {
⋮----
.download-item-name {
⋮----
.download-item-speed {
⋮----
.download-item-bar {
⋮----
.download-item-progress {
⋮----
.paginator {
⋮----
.paginator-thumb {
````

## File: app/src/styles/settings.css
````css
:root {
⋮----
[data-theme='light'] {
⋮----
* {
⋮----
body {
⋮----
.titlebar {
⋮----
.titlebar-brand {
⋮----
.titlebar-brand img {
⋮----
.titlebar-controls {
⋮----
.titlebar-btn {
⋮----
.titlebar-btn:hover {
⋮----
.titlebar-btn.close:hover {
⋮----
.settings-layout {
⋮----
.sidebar {
⋮----
.nav-item {
⋮----
.nav-item:hover {
⋮----
.nav-item.active {
⋮----
.nav-icon {
⋮----
.content-area {
⋮----
.tab-content {
⋮----
.tab-content.active {
⋮----
.section-header h2 {
⋮----
.section-header p {
⋮----
.setting-card {
⋮----
.setting-row {
⋮----
.setting-row.stacked {
⋮----
.setting-info {
⋮----
.setting-title {
⋮----
.setting-desc {
⋮----
.toggle-switch {
⋮----
.toggle-switch input {
⋮----
.slider {
⋮----
.slider:before {
⋮----
input:checked + .slider {
⋮----
input:checked + .slider:before {
⋮----
.input-text {
⋮----
.input-text:focus {
⋮----
.input-group {
⋮----
.btn {
⋮----
.btn:hover {
⋮----
.btn:active {
⋮----
.btn.primary {
⋮----
.btn.primary:hover {
⋮----
.select-dropdown {
⋮----
.footer-bar {
⋮----
.status-badge {
⋮----
.status-badge.ok {
⋮----
.status-badge.warning {
````

## File: app/src/island.ts
````typescript
import { api, on } from './lib/ipc';
import {
  formatDuration,
  currentPosition,
  progressFraction,
  batteryGlyph,
  batteryText,
  clockParts,
  speedText,
  gaugeFraction,
  gaugeLevel,
  notificationGlyph,
  notificationText,
  Glyph
} from './lib/format';
import { IslandController, Pager, WheelPager, type PageId, type IslandMode } from './lib/islandController';
import { NotificationQueue } from './lib/notificationQueue';
import { SpringGroup, SpringPresets } from './lib/spring';
import type {
  Snapshot,
  MediaState,
  DownloadItem,
  Toggles,
  AppEntry
} from './lib/types';
⋮----
function reportHitRect()
⋮----
function applyMode(mode: IslandMode)
⋮----
function applyExpandedGeometry(page: PageId)
⋮----
function updateCarousel(page: PageId, index: number, count: number)
⋮----
function updateCompactView()
⋮----
function updateClock()
⋮----
function applyTheme(themeName: string)
⋮----
function applyFont(fontName: string)
⋮----
function updateMediaProgress()
⋮----
async function init()
⋮----
async function loadAppEntries()
⋮----
function renderDownloads(list: DownloadItem[])
⋮----
function applySnapshot(snap: Snapshot)
````

## File: app/src/settings.ts
````typescript
import { api } from './lib/ipc';
import type { Settings } from './lib/types';
import { getCurrentWindow } from '@tauri-apps/api/window';
import { open } from '@tauri-apps/plugin-dialog';
⋮----
async function loadSettings()
⋮----
function applyTheme(theme: string)
````

## File: app/src-tauri/capabilities/default.json
````json
{
  "$schema": "../gen/schemas/desktop-schema.json",
  "identifier": "default",
  "description": "Permisos de la isla y de la ventana de configuración",
  "windows": ["island", "settings"],
  "permissions": [
    "core:default",
    "core:window:allow-start-dragging",
    "core:window:allow-minimize",
    "core:window:allow-close",
    "core:window:allow-hide",
    "core:window:allow-show",
    "core:window:allow-set-focus",
    "dialog:allow-open"
  ]
}
````

## File: app/src-tauri/gen/schemas/acl-manifests.json
````json
{"core":{"default_permission":{"identifier":"default","description":"Default core plugins set.","permissions":["core:path:default","core:event:default","core:window:default","core:webview:default","core:app:default","core:image:default","core:resources:default","core:menu:default","core:tray:default"]},"permissions":{},"permission_sets":{},"global_scope_schema":null},"core:app":{"default_permission":{"identifier":"default","description":"Default permissions for the plugin.","permissions":["allow-version","allow-name","allow-tauri-version","allow-identifier","allow-bundle-type","allow-register-listener","allow-remove-listener","allow-supports-multiple-windows"]},"permissions":{"allow-app-hide":{"identifier":"allow-app-hide","description":"Enables the app_hide command without any pre-configured scope.","commands":{"allow":["app_hide"],"deny":[]}},"allow-app-show":{"identifier":"allow-app-show","description":"Enables the app_show command without any pre-configured scope.","commands":{"allow":["app_show"],"deny":[]}},"allow-bundle-type":{"identifier":"allow-bundle-type","description":"Enables the bundle_type command without any pre-configured scope.","commands":{"allow":["bundle_type"],"deny":[]}},"allow-default-window-icon":{"identifier":"allow-default-window-icon","description":"Enables the default_window_icon command without any pre-configured scope.","commands":{"allow":["default_window_icon"],"deny":[]}},"allow-exit":{"identifier":"allow-exit","description":"Enables the exit command without any pre-configured scope.","commands":{"allow":["exit"],"deny":[]}},"allow-fetch-data-store-identifiers":{"identifier":"allow-fetch-data-store-identifiers","description":"Enables the fetch_data_store_identifiers command without any pre-configured scope.","commands":{"allow":["fetch_data_store_identifiers"],"deny":[]}},"allow-identifier":{"identifier":"allow-identifier","description":"Enables the identifier command without any pre-configured scope.","commands":{"allow":["identifier"],"deny":[]}},"allow-name":{"identifier":"allow-name","description":"Enables the name command without any pre-configured scope.","commands":{"allow":["name"],"deny":[]}},"allow-register-listener":{"identifier":"allow-register-listener","description":"Enables the register_listener command without any pre-configured scope.","commands":{"allow":["register_listener"],"deny":[]}},"allow-remove-data-store":{"identifier":"allow-remove-data-store","description":"Enables the remove_data_store command without any pre-configured scope.","commands":{"allow":["remove_data_store"],"deny":[]}},"allow-remove-listener":{"identifier":"allow-remove-listener","description":"Enables the remove_listener command without any pre-configured scope.","commands":{"allow":["remove_listener"],"deny":[]}},"allow-set-app-theme":{"identifier":"allow-set-app-theme","description":"Enables the set_app_theme command without any pre-configured scope.","commands":{"allow":["set_app_theme"],"deny":[]}},"allow-set-dock-visibility":{"identifier":"allow-set-dock-visibility","description":"Enables the set_dock_visibility command without any pre-configured scope.","commands":{"allow":["set_dock_visibility"],"deny":[]}},"allow-supports-multiple-windows":{"identifier":"allow-supports-multiple-windows","description":"Enables the supports_multiple_windows command without any pre-configured scope.","commands":{"allow":["supports_multiple_windows"],"deny":[]}},"allow-tauri-version":{"identifier":"allow-tauri-version","description":"Enables the tauri_version command without any pre-configured scope.","commands":{"allow":["tauri_version"],"deny":[]}},"allow-version":{"identifier":"allow-version","description":"Enables the version command without any pre-configured scope.","commands":{"allow":["version"],"deny":[]}},"deny-app-hide":{"identifier":"deny-app-hide","description":"Denies the app_hide command without any pre-configured scope.","commands":{"allow":[],"deny":["app_hide"]}},"deny-app-show":{"identifier":"deny-app-show","description":"Denies the app_show command without any pre-configured scope.","commands":{"allow":[],"deny":["app_show"]}},"deny-bundle-type":{"identifier":"deny-bundle-type","description":"Denies the bundle_type command without any pre-configured scope.","commands":{"allow":[],"deny":["bundle_type"]}},"deny-default-window-icon":{"identifier":"deny-default-window-icon","description":"Denies the default_window_icon command without any pre-configured scope.","commands":{"allow":[],"deny":["default_window_icon"]}},"deny-exit":{"identifier":"deny-exit","description":"Denies the exit command without any pre-configured scope.","commands":{"allow":[],"deny":["exit"]}},"deny-fetch-data-store-identifiers":{"identifier":"deny-fetch-data-store-identifiers","description":"Denies the fetch_data_store_identifiers command without any pre-configured scope.","commands":{"allow":[],"deny":["fetch_data_store_identifiers"]}},"deny-identifier":{"identifier":"deny-identifier","description":"Denies the identifier command without any pre-configured scope.","commands":{"allow":[],"deny":["identifier"]}},"deny-name":{"identifier":"deny-name","description":"Denies the name command without any pre-configured scope.","commands":{"allow":[],"deny":["name"]}},"deny-register-listener":{"identifier":"deny-register-listener","description":"Denies the register_listener command without any pre-configured scope.","commands":{"allow":[],"deny":["register_listener"]}},"deny-remove-data-store":{"identifier":"deny-remove-data-store","description":"Denies the remove_data_store command without any pre-configured scope.","commands":{"allow":[],"deny":["remove_data_store"]}},"deny-remove-listener":{"identifier":"deny-remove-listener","description":"Denies the remove_listener command without any pre-configured scope.","commands":{"allow":[],"deny":["remove_listener"]}},"deny-set-app-theme":{"identifier":"deny-set-app-theme","description":"Denies the set_app_theme command without any pre-configured scope.","commands":{"allow":[],"deny":["set_app_theme"]}},"deny-set-dock-visibility":{"identifier":"deny-set-dock-visibility","description":"Denies the set_dock_visibility command without any pre-configured scope.","commands":{"allow":[],"deny":["set_dock_visibility"]}},"deny-supports-multiple-windows":{"identifier":"deny-supports-multiple-windows","description":"Denies the supports_multiple_windows command without any pre-configured scope.","commands":{"allow":[],"deny":["supports_multiple_windows"]}},"deny-tauri-version":{"identifier":"deny-tauri-version","description":"Denies the tauri_version command without any pre-configured scope.","commands":{"allow":[],"deny":["tauri_version"]}},"deny-version":{"identifier":"deny-version","description":"Denies the version command without any pre-configured scope.","commands":{"allow":[],"deny":["version"]}}},"permission_sets":{},"global_scope_schema":null},"core:event":{"default_permission":{"identifier":"default","description":"Default permissions for the plugin, which enables all commands.","permissions":["allow-listen","allow-unlisten","allow-emit","allow-emit-to"]},"permissions":{"allow-emit":{"identifier":"allow-emit","description":"Enables the emit command without any pre-configured scope.","commands":{"allow":["emit"],"deny":[]}},"allow-emit-to":{"identifier":"allow-emit-to","description":"Enables the emit_to command without any pre-configured scope.","commands":{"allow":["emit_to"],"deny":[]}},"allow-listen":{"identifier":"allow-listen","description":"Enables the listen command without any pre-configured scope.","commands":{"allow":["listen"],"deny":[]}},"allow-unlisten":{"identifier":"allow-unlisten","description":"Enables the unlisten command without any pre-configured scope.","commands":{"allow":["unlisten"],"deny":[]}},"deny-emit":{"identifier":"deny-emit","description":"Denies the emit command without any pre-configured scope.","commands":{"allow":[],"deny":["emit"]}},"deny-emit-to":{"identifier":"deny-emit-to","description":"Denies the emit_to command without any pre-configured scope.","commands":{"allow":[],"deny":["emit_to"]}},"deny-listen":{"identifier":"deny-listen","description":"Denies the listen command without any pre-configured scope.","commands":{"allow":[],"deny":["listen"]}},"deny-unlisten":{"identifier":"deny-unlisten","description":"Denies the unlisten command without any pre-configured scope.","commands":{"allow":[],"deny":["unlisten"]}}},"permission_sets":{},"global_scope_schema":null},"core:image":{"default_permission":{"identifier":"default","description":"Default permissions for the plugin, which enables all commands.","permissions":["allow-new","allow-from-bytes","allow-from-path","allow-rgba","allow-size"]},"permissions":{"allow-from-bytes":{"identifier":"allow-from-bytes","description":"Enables the from_bytes command without any pre-configured scope.","commands":{"allow":["from_bytes"],"deny":[]}},"allow-from-path":{"identifier":"allow-from-path","description":"Enables the from_path command without any pre-configured scope.","commands":{"allow":["from_path"],"deny":[]}},"allow-new":{"identifier":"allow-new","description":"Enables the new command without any pre-configured scope.","commands":{"allow":["new"],"deny":[]}},"allow-rgba":{"identifier":"allow-rgba","description":"Enables the rgba command without any pre-configured scope.","commands":{"allow":["rgba"],"deny":[]}},"allow-size":{"identifier":"allow-size","description":"Enables the size command without any pre-configured scope.","commands":{"allow":["size"],"deny":[]}},"deny-from-bytes":{"identifier":"deny-from-bytes","description":"Denies the from_bytes command without any pre-configured scope.","commands":{"allow":[],"deny":["from_bytes"]}},"deny-from-path":{"identifier":"deny-from-path","description":"Denies the from_path command without any pre-configured scope.","commands":{"allow":[],"deny":["from_path"]}},"deny-new":{"identifier":"deny-new","description":"Denies the new command without any pre-configured scope.","commands":{"allow":[],"deny":["new"]}},"deny-rgba":{"identifier":"deny-rgba","description":"Denies the rgba command without any pre-configured scope.","commands":{"allow":[],"deny":["rgba"]}},"deny-size":{"identifier":"deny-size","description":"Denies the size command without any pre-configured scope.","commands":{"allow":[],"deny":["size"]}}},"permission_sets":{},"global_scope_schema":null},"core:menu":{"default_permission":{"identifier":"default","description":"Default permissions for the plugin, which enables all commands.","permissions":["allow-new","allow-append","allow-prepend","allow-insert","allow-remove","allow-remove-at","allow-items","allow-get","allow-popup","allow-create-default","allow-set-as-app-menu","allow-set-as-window-menu","allow-text","allow-set-text","allow-is-enabled","allow-set-enabled","allow-set-accelerator","allow-set-as-windows-menu-for-nsapp","allow-set-as-help-menu-for-nsapp","allow-is-checked","allow-set-checked","allow-set-icon"]},"permissions":{"allow-append":{"identifier":"allow-append","description":"Enables the append command without any pre-configured scope.","commands":{"allow":["append"],"deny":[]}},"allow-create-default":{"identifier":"allow-create-default","description":"Enables the create_default command without any pre-configured scope.","commands":{"allow":["create_default"],"deny":[]}},"allow-get":{"identifier":"allow-get","description":"Enables the get command without any pre-configured scope.","commands":{"allow":["get"],"deny":[]}},"allow-insert":{"identifier":"allow-insert","description":"Enables the insert command without any pre-configured scope.","commands":{"allow":["insert"],"deny":[]}},"allow-is-checked":{"identifier":"allow-is-checked","description":"Enables the is_checked command without any pre-configured scope.","commands":{"allow":["is_checked"],"deny":[]}},"allow-is-enabled":{"identifier":"allow-is-enabled","description":"Enables the is_enabled command without any pre-configured scope.","commands":{"allow":["is_enabled"],"deny":[]}},"allow-items":{"identifier":"allow-items","description":"Enables the items command without any pre-configured scope.","commands":{"allow":["items"],"deny":[]}},"allow-new":{"identifier":"allow-new","description":"Enables the new command without any pre-configured scope.","commands":{"allow":["new"],"deny":[]}},"allow-popup":{"identifier":"allow-popup","description":"Enables the popup command without any pre-configured scope.","commands":{"allow":["popup"],"deny":[]}},"allow-prepend":{"identifier":"allow-prepend","description":"Enables the prepend command without any pre-configured scope.","commands":{"allow":["prepend"],"deny":[]}},"allow-remove":{"identifier":"allow-remove","description":"Enables the remove command without any pre-configured scope.","commands":{"allow":["remove"],"deny":[]}},"allow-remove-at":{"identifier":"allow-remove-at","description":"Enables the remove_at command without any pre-configured scope.","commands":{"allow":["remove_at"],"deny":[]}},"allow-set-accelerator":{"identifier":"allow-set-accelerator","description":"Enables the set_accelerator command without any pre-configured scope.","commands":{"allow":["set_accelerator"],"deny":[]}},"allow-set-as-app-menu":{"identifier":"allow-set-as-app-menu","description":"Enables the set_as_app_menu command without any pre-configured scope.","commands":{"allow":["set_as_app_menu"],"deny":[]}},"allow-set-as-help-menu-for-nsapp":{"identifier":"allow-set-as-help-menu-for-nsapp","description":"Enables the set_as_help_menu_for_nsapp command without any pre-configured scope.","commands":{"allow":["set_as_help_menu_for_nsapp"],"deny":[]}},"allow-set-as-window-menu":{"identifier":"allow-set-as-window-menu","description":"Enables the set_as_window_menu command without any pre-configured scope.","commands":{"allow":["set_as_window_menu"],"deny":[]}},"allow-set-as-windows-menu-for-nsapp":{"identifier":"allow-set-as-windows-menu-for-nsapp","description":"Enables the set_as_windows_menu_for_nsapp command without any pre-configured scope.","commands":{"allow":["set_as_windows_menu_for_nsapp"],"deny":[]}},"allow-set-checked":{"identifier":"allow-set-checked","description":"Enables the set_checked command without any pre-configured scope.","commands":{"allow":["set_checked"],"deny":[]}},"allow-set-enabled":{"identifier":"allow-set-enabled","description":"Enables the set_enabled command without any pre-configured scope.","commands":{"allow":["set_enabled"],"deny":[]}},"allow-set-icon":{"identifier":"allow-set-icon","description":"Enables the set_icon command without any pre-configured scope.","commands":{"allow":["set_icon"],"deny":[]}},"allow-set-text":{"identifier":"allow-set-text","description":"Enables the set_text command without any pre-configured scope.","commands":{"allow":["set_text"],"deny":[]}},"allow-text":{"identifier":"allow-text","description":"Enables the text command without any pre-configured scope.","commands":{"allow":["text"],"deny":[]}},"deny-append":{"identifier":"deny-append","description":"Denies the append command without any pre-configured scope.","commands":{"allow":[],"deny":["append"]}},"deny-create-default":{"identifier":"deny-create-default","description":"Denies the create_default command without any pre-configured scope.","commands":{"allow":[],"deny":["create_default"]}},"deny-get":{"identifier":"deny-get","description":"Denies the get command without any pre-configured scope.","commands":{"allow":[],"deny":["get"]}},"deny-insert":{"identifier":"deny-insert","description":"Denies the insert command without any pre-configured scope.","commands":{"allow":[],"deny":["insert"]}},"deny-is-checked":{"identifier":"deny-is-checked","description":"Denies the is_checked command without any pre-configured scope.","commands":{"allow":[],"deny":["is_checked"]}},"deny-is-enabled":{"identifier":"deny-is-enabled","description":"Denies the is_enabled command without any pre-configured scope.","commands":{"allow":[],"deny":["is_enabled"]}},"deny-items":{"identifier":"deny-items","description":"Denies the items command without any pre-configured scope.","commands":{"allow":[],"deny":["items"]}},"deny-new":{"identifier":"deny-new","description":"Denies the new command without any pre-configured scope.","commands":{"allow":[],"deny":["new"]}},"deny-popup":{"identifier":"deny-popup","description":"Denies the popup command without any pre-configured scope.","commands":{"allow":[],"deny":["popup"]}},"deny-prepend":{"identifier":"deny-prepend","description":"Denies the prepend command without any pre-configured scope.","commands":{"allow":[],"deny":["prepend"]}},"deny-remove":{"identifier":"deny-remove","description":"Denies the remove command without any pre-configured scope.","commands":{"allow":[],"deny":["remove"]}},"deny-remove-at":{"identifier":"deny-remove-at","description":"Denies the remove_at command without any pre-configured scope.","commands":{"allow":[],"deny":["remove_at"]}},"deny-set-accelerator":{"identifier":"deny-set-accelerator","description":"Denies the set_accelerator command without any pre-configured scope.","commands":{"allow":[],"deny":["set_accelerator"]}},"deny-set-as-app-menu":{"identifier":"deny-set-as-app-menu","description":"Denies the set_as_app_menu command without any pre-configured scope.","commands":{"allow":[],"deny":["set_as_app_menu"]}},"deny-set-as-help-menu-for-nsapp":{"identifier":"deny-set-as-help-menu-for-nsapp","description":"Denies the set_as_help_menu_for_nsapp command without any pre-configured scope.","commands":{"allow":[],"deny":["set_as_help_menu_for_nsapp"]}},"deny-set-as-window-menu":{"identifier":"deny-set-as-window-menu","description":"Denies the set_as_window_menu command without any pre-configured scope.","commands":{"allow":[],"deny":["set_as_window_menu"]}},"deny-set-as-windows-menu-for-nsapp":{"identifier":"deny-set-as-windows-menu-for-nsapp","description":"Denies the set_as_windows_menu_for_nsapp command without any pre-configured scope.","commands":{"allow":[],"deny":["set_as_windows_menu_for_nsapp"]}},"deny-set-checked":{"identifier":"deny-set-checked","description":"Denies the set_checked command without any pre-configured scope.","commands":{"allow":[],"deny":["set_checked"]}},"deny-set-enabled":{"identifier":"deny-set-enabled","description":"Denies the set_enabled command without any pre-configured scope.","commands":{"allow":[],"deny":["set_enabled"]}},"deny-set-icon":{"identifier":"deny-set-icon","description":"Denies the set_icon command without any pre-configured scope.","commands":{"allow":[],"deny":["set_icon"]}},"deny-set-text":{"identifier":"deny-set-text","description":"Denies the set_text command without any pre-configured scope.","commands":{"allow":[],"deny":["set_text"]}},"deny-text":{"identifier":"deny-text","description":"Denies the text command without any pre-configured scope.","commands":{"allow":[],"deny":["text"]}}},"permission_sets":{},"global_scope_schema":null},"core:path":{"default_permission":{"identifier":"default","description":"Default permissions for the plugin, which enables all commands.","permissions":["allow-resolve-directory","allow-resolve","allow-normalize","allow-join","allow-dirname","allow-extname","allow-basename","allow-is-absolute"]},"permissions":{"allow-basename":{"identifier":"allow-basename","description":"Enables the basename command without any pre-configured scope.","commands":{"allow":["basename"],"deny":[]}},"allow-dirname":{"identifier":"allow-dirname","description":"Enables the dirname command without any pre-configured scope.","commands":{"allow":["dirname"],"deny":[]}},"allow-extname":{"identifier":"allow-extname","description":"Enables the extname command without any pre-configured scope.","commands":{"allow":["extname"],"deny":[]}},"allow-is-absolute":{"identifier":"allow-is-absolute","description":"Enables the is_absolute command without any pre-configured scope.","commands":{"allow":["is_absolute"],"deny":[]}},"allow-join":{"identifier":"allow-join","description":"Enables the join command without any pre-configured scope.","commands":{"allow":["join"],"deny":[]}},"allow-normalize":{"identifier":"allow-normalize","description":"Enables the normalize command without any pre-configured scope.","commands":{"allow":["normalize"],"deny":[]}},"allow-resolve":{"identifier":"allow-resolve","description":"Enables the resolve command without any pre-configured scope.","commands":{"allow":["resolve"],"deny":[]}},"allow-resolve-directory":{"identifier":"allow-resolve-directory","description":"Enables the resolve_directory command without any pre-configured scope.","commands":{"allow":["resolve_directory"],"deny":[]}},"deny-basename":{"identifier":"deny-basename","description":"Denies the basename command without any pre-configured scope.","commands":{"allow":[],"deny":["basename"]}},"deny-dirname":{"identifier":"deny-dirname","description":"Denies the dirname command without any pre-configured scope.","commands":{"allow":[],"deny":["dirname"]}},"deny-extname":{"identifier":"deny-extname","description":"Denies the extname command without any pre-configured scope.","commands":{"allow":[],"deny":["extname"]}},"deny-is-absolute":{"identifier":"deny-is-absolute","description":"Denies the is_absolute command without any pre-configured scope.","commands":{"allow":[],"deny":["is_absolute"]}},"deny-join":{"identifier":"deny-join","description":"Denies the join command without any pre-configured scope.","commands":{"allow":[],"deny":["join"]}},"deny-normalize":{"identifier":"deny-normalize","description":"Denies the normalize command without any pre-configured scope.","commands":{"allow":[],"deny":["normalize"]}},"deny-resolve":{"identifier":"deny-resolve","description":"Denies the resolve command without any pre-configured scope.","commands":{"allow":[],"deny":["resolve"]}},"deny-resolve-directory":{"identifier":"deny-resolve-directory","description":"Denies the resolve_directory command without any pre-configured scope.","commands":{"allow":[],"deny":["resolve_directory"]}}},"permission_sets":{},"global_scope_schema":null},"core:resources":{"default_permission":{"identifier":"default","description":"Default permissions for the plugin, which enables all commands.","permissions":["allow-close"]},"permissions":{"allow-close":{"identifier":"allow-close","description":"Enables the close command without any pre-configured scope.","commands":{"allow":["close"],"deny":[]}},"deny-close":{"identifier":"deny-close","description":"Denies the close command without any pre-configured scope.","commands":{"allow":[],"deny":["close"]}}},"permission_sets":{},"global_scope_schema":null},"core:tray":{"default_permission":{"identifier":"default","description":"Default permissions for the plugin, which enables all commands.","permissions":["allow-new","allow-get-by-id","allow-remove-by-id","allow-set-icon","allow-set-menu","allow-set-tooltip","allow-set-title","allow-set-visible","allow-set-temp-dir-path","allow-set-icon-as-template","allow-set-icon-with-as-template","allow-set-show-menu-on-left-click"]},"permissions":{"allow-get-by-id":{"identifier":"allow-get-by-id","description":"Enables the get_by_id command without any pre-configured scope.","commands":{"allow":["get_by_id"],"deny":[]}},"allow-new":{"identifier":"allow-new","description":"Enables the new command without any pre-configured scope.","commands":{"allow":["new"],"deny":[]}},"allow-remove-by-id":{"identifier":"allow-remove-by-id","description":"Enables the remove_by_id command without any pre-configured scope.","commands":{"allow":["remove_by_id"],"deny":[]}},"allow-set-icon":{"identifier":"allow-set-icon","description":"Enables the set_icon command without any pre-configured scope.","commands":{"allow":["set_icon"],"deny":[]}},"allow-set-icon-as-template":{"identifier":"allow-set-icon-as-template","description":"Enables the set_icon_as_template command without any pre-configured scope.","commands":{"allow":["set_icon_as_template"],"deny":[]}},"allow-set-icon-with-as-template":{"identifier":"allow-set-icon-with-as-template","description":"Enables the set_icon_with_as_template command without any pre-configured scope.","commands":{"allow":["set_icon_with_as_template"],"deny":[]}},"allow-set-menu":{"identifier":"allow-set-menu","description":"Enables the set_menu command without any pre-configured scope.","commands":{"allow":["set_menu"],"deny":[]}},"allow-set-show-menu-on-left-click":{"identifier":"allow-set-show-menu-on-left-click","description":"Enables the set_show_menu_on_left_click command without any pre-configured scope.","commands":{"allow":["set_show_menu_on_left_click"],"deny":[]}},"allow-set-temp-dir-path":{"identifier":"allow-set-temp-dir-path","description":"Enables the set_temp_dir_path command without any pre-configured scope.","commands":{"allow":["set_temp_dir_path"],"deny":[]}},"allow-set-title":{"identifier":"allow-set-title","description":"Enables the set_title command without any pre-configured scope.","commands":{"allow":["set_title"],"deny":[]}},"allow-set-tooltip":{"identifier":"allow-set-tooltip","description":"Enables the set_tooltip command without any pre-configured scope.","commands":{"allow":["set_tooltip"],"deny":[]}},"allow-set-visible":{"identifier":"allow-set-visible","description":"Enables the set_visible command without any pre-configured scope.","commands":{"allow":["set_visible"],"deny":[]}},"deny-get-by-id":{"identifier":"deny-get-by-id","description":"Denies the get_by_id command without any pre-configured scope.","commands":{"allow":[],"deny":["get_by_id"]}},"deny-new":{"identifier":"deny-new","description":"Denies the new command without any pre-configured scope.","commands":{"allow":[],"deny":["new"]}},"deny-remove-by-id":{"identifier":"deny-remove-by-id","description":"Denies the remove_by_id command without any pre-configured scope.","commands":{"allow":[],"deny":["remove_by_id"]}},"deny-set-icon":{"identifier":"deny-set-icon","description":"Denies the set_icon command without any pre-configured scope.","commands":{"allow":[],"deny":["set_icon"]}},"deny-set-icon-as-template":{"identifier":"deny-set-icon-as-template","description":"Denies the set_icon_as_template command without any pre-configured scope.","commands":{"allow":[],"deny":["set_icon_as_template"]}},"deny-set-icon-with-as-template":{"identifier":"deny-set-icon-with-as-template","description":"Denies the set_icon_with_as_template command without any pre-configured scope.","commands":{"allow":[],"deny":["set_icon_with_as_template"]}},"deny-set-menu":{"identifier":"deny-set-menu","description":"Denies the set_menu command without any pre-configured scope.","commands":{"allow":[],"deny":["set_menu"]}},"deny-set-show-menu-on-left-click":{"identifier":"deny-set-show-menu-on-left-click","description":"Denies the set_show_menu_on_left_click command without any pre-configured scope.","commands":{"allow":[],"deny":["set_show_menu_on_left_click"]}},"deny-set-temp-dir-path":{"identifier":"deny-set-temp-dir-path","description":"Denies the set_temp_dir_path command without any pre-configured scope.","commands":{"allow":[],"deny":["set_temp_dir_path"]}},"deny-set-title":{"identifier":"deny-set-title","description":"Denies the set_title command without any pre-configured scope.","commands":{"allow":[],"deny":["set_title"]}},"deny-set-tooltip":{"identifier":"deny-set-tooltip","description":"Denies the set_tooltip command without any pre-configured scope.","commands":{"allow":[],"deny":["set_tooltip"]}},"deny-set-visible":{"identifier":"deny-set-visible","description":"Denies the set_visible command without any pre-configured scope.","commands":{"allow":[],"deny":["set_visible"]}}},"permission_sets":{},"global_scope_schema":null},"core:webview":{"default_permission":{"identifier":"default","description":"Default permissions for the plugin.","permissions":["allow-get-all-webviews","allow-webview-position","allow-webview-size","allow-internal-toggle-devtools"]},"permissions":{"allow-clear-all-browsing-data":{"identifier":"allow-clear-all-browsing-data","description":"Enables the clear_all_browsing_data command without any pre-configured scope.","commands":{"allow":["clear_all_browsing_data"],"deny":[]}},"allow-create-webview":{"identifier":"allow-create-webview","description":"Enables the create_webview command without any pre-configured scope.","commands":{"allow":["create_webview"],"deny":[]}},"allow-create-webview-window":{"identifier":"allow-create-webview-window","description":"Enables the create_webview_window command without any pre-configured scope.","commands":{"allow":["create_webview_window"],"deny":[]}},"allow-get-all-webviews":{"identifier":"allow-get-all-webviews","description":"Enables the get_all_webviews command without any pre-configured scope.","commands":{"allow":["get_all_webviews"],"deny":[]}},"allow-internal-toggle-devtools":{"identifier":"allow-internal-toggle-devtools","description":"Enables the internal_toggle_devtools command without any pre-configured scope.","commands":{"allow":["internal_toggle_devtools"],"deny":[]}},"allow-print":{"identifier":"allow-print","description":"Enables the print command without any pre-configured scope.","commands":{"allow":["print"],"deny":[]}},"allow-reparent":{"identifier":"allow-reparent","description":"Enables the reparent command without any pre-configured scope.","commands":{"allow":["reparent"],"deny":[]}},"allow-set-webview-auto-resize":{"identifier":"allow-set-webview-auto-resize","description":"Enables the set_webview_auto_resize command without any pre-configured scope.","commands":{"allow":["set_webview_auto_resize"],"deny":[]}},"allow-set-webview-background-color":{"identifier":"allow-set-webview-background-color","description":"Enables the set_webview_background_color command without any pre-configured scope.","commands":{"allow":["set_webview_background_color"],"deny":[]}},"allow-set-webview-focus":{"identifier":"allow-set-webview-focus","description":"Enables the set_webview_focus command without any pre-configured scope.","commands":{"allow":["set_webview_focus"],"deny":[]}},"allow-set-webview-position":{"identifier":"allow-set-webview-position","description":"Enables the set_webview_position command without any pre-configured scope.","commands":{"allow":["set_webview_position"],"deny":[]}},"allow-set-webview-size":{"identifier":"allow-set-webview-size","description":"Enables the set_webview_size command without any pre-configured scope.","commands":{"allow":["set_webview_size"],"deny":[]}},"allow-set-webview-zoom":{"identifier":"allow-set-webview-zoom","description":"Enables the set_webview_zoom command without any pre-configured scope.","commands":{"allow":["set_webview_zoom"],"deny":[]}},"allow-webview-close":{"identifier":"allow-webview-close","description":"Enables the webview_close command without any pre-configured scope.","commands":{"allow":["webview_close"],"deny":[]}},"allow-webview-hide":{"identifier":"allow-webview-hide","description":"Enables the webview_hide command without any pre-configured scope.","commands":{"allow":["webview_hide"],"deny":[]}},"allow-webview-position":{"identifier":"allow-webview-position","description":"Enables the webview_position command without any pre-configured scope.","commands":{"allow":["webview_position"],"deny":[]}},"allow-webview-show":{"identifier":"allow-webview-show","description":"Enables the webview_show command without any pre-configured scope.","commands":{"allow":["webview_show"],"deny":[]}},"allow-webview-size":{"identifier":"allow-webview-size","description":"Enables the webview_size command without any pre-configured scope.","commands":{"allow":["webview_size"],"deny":[]}},"deny-clear-all-browsing-data":{"identifier":"deny-clear-all-browsing-data","description":"Denies the clear_all_browsing_data command without any pre-configured scope.","commands":{"allow":[],"deny":["clear_all_browsing_data"]}},"deny-create-webview":{"identifier":"deny-create-webview","description":"Denies the create_webview command without any pre-configured scope.","commands":{"allow":[],"deny":["create_webview"]}},"deny-create-webview-window":{"identifier":"deny-create-webview-window","description":"Denies the create_webview_window command without any pre-configured scope.","commands":{"allow":[],"deny":["create_webview_window"]}},"deny-get-all-webviews":{"identifier":"deny-get-all-webviews","description":"Denies the get_all_webviews command without any pre-configured scope.","commands":{"allow":[],"deny":["get_all_webviews"]}},"deny-internal-toggle-devtools":{"identifier":"deny-internal-toggle-devtools","description":"Denies the internal_toggle_devtools command without any pre-configured scope.","commands":{"allow":[],"deny":["internal_toggle_devtools"]}},"deny-print":{"identifier":"deny-print","description":"Denies the print command without any pre-configured scope.","commands":{"allow":[],"deny":["print"]}},"deny-reparent":{"identifier":"deny-reparent","description":"Denies the reparent command without any pre-configured scope.","commands":{"allow":[],"deny":["reparent"]}},"deny-set-webview-auto-resize":{"identifier":"deny-set-webview-auto-resize","description":"Denies the set_webview_auto_resize command without any pre-configured scope.","commands":{"allow":[],"deny":["set_webview_auto_resize"]}},"deny-set-webview-background-color":{"identifier":"deny-set-webview-background-color","description":"Denies the set_webview_background_color command without any pre-configured scope.","commands":{"allow":[],"deny":["set_webview_background_color"]}},"deny-set-webview-focus":{"identifier":"deny-set-webview-focus","description":"Denies the set_webview_focus command without any pre-configured scope.","commands":{"allow":[],"deny":["set_webview_focus"]}},"deny-set-webview-position":{"identifier":"deny-set-webview-position","description":"Denies the set_webview_position command without any pre-configured scope.","commands":{"allow":[],"deny":["set_webview_position"]}},"deny-set-webview-size":{"identifier":"deny-set-webview-size","description":"Denies the set_webview_size command without any pre-configured scope.","commands":{"allow":[],"deny":["set_webview_size"]}},"deny-set-webview-zoom":{"identifier":"deny-set-webview-zoom","description":"Denies the set_webview_zoom command without any pre-configured scope.","commands":{"allow":[],"deny":["set_webview_zoom"]}},"deny-webview-close":{"identifier":"deny-webview-close","description":"Denies the webview_close command without any pre-configured scope.","commands":{"allow":[],"deny":["webview_close"]}},"deny-webview-hide":{"identifier":"deny-webview-hide","description":"Denies the webview_hide command without any pre-configured scope.","commands":{"allow":[],"deny":["webview_hide"]}},"deny-webview-position":{"identifier":"deny-webview-position","description":"Denies the webview_position command without any pre-configured scope.","commands":{"allow":[],"deny":["webview_position"]}},"deny-webview-show":{"identifier":"deny-webview-show","description":"Denies the webview_show command without any pre-configured scope.","commands":{"allow":[],"deny":["webview_show"]}},"deny-webview-size":{"identifier":"deny-webview-size","description":"Denies the webview_size command without any pre-configured scope.","commands":{"allow":[],"deny":["webview_size"]}}},"permission_sets":{},"global_scope_schema":null},"core:window":{"default_permission":{"identifier":"default","description":"Default permissions for the plugin.","permissions":["allow-get-all-windows","allow-scale-factor","allow-inner-position","allow-outer-position","allow-inner-size","allow-outer-size","allow-is-fullscreen","allow-is-minimized","allow-is-maximized","allow-is-focused","allow-is-decorated","allow-is-resizable","allow-is-maximizable","allow-is-minimizable","allow-is-closable","allow-is-visible","allow-is-enabled","allow-title","allow-current-monitor","allow-primary-monitor","allow-monitor-from-point","allow-available-monitors","allow-cursor-position","allow-theme","allow-is-always-on-top","allow-activity-name","allow-scene-identifier","allow-internal-toggle-maximize"]},"permissions":{"allow-activity-name":{"identifier":"allow-activity-name","description":"Enables the activity_name command without any pre-configured scope.","commands":{"allow":["activity_name"],"deny":[]}},"allow-available-monitors":{"identifier":"allow-available-monitors","description":"Enables the available_monitors command without any pre-configured scope.","commands":{"allow":["available_monitors"],"deny":[]}},"allow-center":{"identifier":"allow-center","description":"Enables the center command without any pre-configured scope.","commands":{"allow":["center"],"deny":[]}},"allow-close":{"identifier":"allow-close","description":"Enables the close command without any pre-configured scope.","commands":{"allow":["close"],"deny":[]}},"allow-create":{"identifier":"allow-create","description":"Enables the create command without any pre-configured scope.","commands":{"allow":["create"],"deny":[]}},"allow-current-monitor":{"identifier":"allow-current-monitor","description":"Enables the current_monitor command without any pre-configured scope.","commands":{"allow":["current_monitor"],"deny":[]}},"allow-cursor-position":{"identifier":"allow-cursor-position","description":"Enables the cursor_position command without any pre-configured scope.","commands":{"allow":["cursor_position"],"deny":[]}},"allow-destroy":{"identifier":"allow-destroy","description":"Enables the destroy command without any pre-configured scope.","commands":{"allow":["destroy"],"deny":[]}},"allow-get-all-windows":{"identifier":"allow-get-all-windows","description":"Enables the get_all_windows command without any pre-configured scope.","commands":{"allow":["get_all_windows"],"deny":[]}},"allow-hide":{"identifier":"allow-hide","description":"Enables the hide command without any pre-configured scope.","commands":{"allow":["hide"],"deny":[]}},"allow-inner-position":{"identifier":"allow-inner-position","description":"Enables the inner_position command without any pre-configured scope.","commands":{"allow":["inner_position"],"deny":[]}},"allow-inner-size":{"identifier":"allow-inner-size","description":"Enables the inner_size command without any pre-configured scope.","commands":{"allow":["inner_size"],"deny":[]}},"allow-internal-toggle-maximize":{"identifier":"allow-internal-toggle-maximize","description":"Enables the internal_toggle_maximize command without any pre-configured scope.","commands":{"allow":["internal_toggle_maximize"],"deny":[]}},"allow-is-always-on-top":{"identifier":"allow-is-always-on-top","description":"Enables the is_always_on_top command without any pre-configured scope.","commands":{"allow":["is_always_on_top"],"deny":[]}},"allow-is-closable":{"identifier":"allow-is-closable","description":"Enables the is_closable command without any pre-configured scope.","commands":{"allow":["is_closable"],"deny":[]}},"allow-is-decorated":{"identifier":"allow-is-decorated","description":"Enables the is_decorated command without any pre-configured scope.","commands":{"allow":["is_decorated"],"deny":[]}},"allow-is-enabled":{"identifier":"allow-is-enabled","description":"Enables the is_enabled command without any pre-configured scope.","commands":{"allow":["is_enabled"],"deny":[]}},"allow-is-focused":{"identifier":"allow-is-focused","description":"Enables the is_focused command without any pre-configured scope.","commands":{"allow":["is_focused"],"deny":[]}},"allow-is-fullscreen":{"identifier":"allow-is-fullscreen","description":"Enables the is_fullscreen command without any pre-configured scope.","commands":{"allow":["is_fullscreen"],"deny":[]}},"allow-is-maximizable":{"identifier":"allow-is-maximizable","description":"Enables the is_maximizable command without any pre-configured scope.","commands":{"allow":["is_maximizable"],"deny":[]}},"allow-is-maximized":{"identifier":"allow-is-maximized","description":"Enables the is_maximized command without any pre-configured scope.","commands":{"allow":["is_maximized"],"deny":[]}},"allow-is-minimizable":{"identifier":"allow-is-minimizable","description":"Enables the is_minimizable command without any pre-configured scope.","commands":{"allow":["is_minimizable"],"deny":[]}},"allow-is-minimized":{"identifier":"allow-is-minimized","description":"Enables the is_minimized command without any pre-configured scope.","commands":{"allow":["is_minimized"],"deny":[]}},"allow-is-resizable":{"identifier":"allow-is-resizable","description":"Enables the is_resizable command without any pre-configured scope.","commands":{"allow":["is_resizable"],"deny":[]}},"allow-is-visible":{"identifier":"allow-is-visible","description":"Enables the is_visible command without any pre-configured scope.","commands":{"allow":["is_visible"],"deny":[]}},"allow-maximize":{"identifier":"allow-maximize","description":"Enables the maximize command without any pre-configured scope.","commands":{"allow":["maximize"],"deny":[]}},"allow-minimize":{"identifier":"allow-minimize","description":"Enables the minimize command without any pre-configured scope.","commands":{"allow":["minimize"],"deny":[]}},"allow-monitor-from-point":{"identifier":"allow-monitor-from-point","description":"Enables the monitor_from_point command without any pre-configured scope.","commands":{"allow":["monitor_from_point"],"deny":[]}},"allow-outer-position":{"identifier":"allow-outer-position","description":"Enables the outer_position command without any pre-configured scope.","commands":{"allow":["outer_position"],"deny":[]}},"allow-outer-size":{"identifier":"allow-outer-size","description":"Enables the outer_size command without any pre-configured scope.","commands":{"allow":["outer_size"],"deny":[]}},"allow-primary-monitor":{"identifier":"allow-primary-monitor","description":"Enables the primary_monitor command without any pre-configured scope.","commands":{"allow":["primary_monitor"],"deny":[]}},"allow-request-user-attention":{"identifier":"allow-request-user-attention","description":"Enables the request_user_attention command without any pre-configured scope.","commands":{"allow":["request_user_attention"],"deny":[]}},"allow-scale-factor":{"identifier":"allow-scale-factor","description":"Enables the scale_factor command without any pre-configured scope.","commands":{"allow":["scale_factor"],"deny":[]}},"allow-scene-identifier":{"identifier":"allow-scene-identifier","description":"Enables the scene_identifier command without any pre-configured scope.","commands":{"allow":["scene_identifier"],"deny":[]}},"allow-set-always-on-bottom":{"identifier":"allow-set-always-on-bottom","description":"Enables the set_always_on_bottom command without any pre-configured scope.","commands":{"allow":["set_always_on_bottom"],"deny":[]}},"allow-set-always-on-top":{"identifier":"allow-set-always-on-top","description":"Enables the set_always_on_top command without any pre-configured scope.","commands":{"allow":["set_always_on_top"],"deny":[]}},"allow-set-background-color":{"identifier":"allow-set-background-color","description":"Enables the set_background_color command without any pre-configured scope.","commands":{"allow":["set_background_color"],"deny":[]}},"allow-set-badge-count":{"identifier":"allow-set-badge-count","description":"Enables the set_badge_count command without any pre-configured scope.","commands":{"allow":["set_badge_count"],"deny":[]}},"allow-set-badge-label":{"identifier":"allow-set-badge-label","description":"Enables the set_badge_label command without any pre-configured scope.","commands":{"allow":["set_badge_label"],"deny":[]}},"allow-set-closable":{"identifier":"allow-set-closable","description":"Enables the set_closable command without any pre-configured scope.","commands":{"allow":["set_closable"],"deny":[]}},"allow-set-content-protected":{"identifier":"allow-set-content-protected","description":"Enables the set_content_protected command without any pre-configured scope.","commands":{"allow":["set_content_protected"],"deny":[]}},"allow-set-cursor-grab":{"identifier":"allow-set-cursor-grab","description":"Enables the set_cursor_grab command without any pre-configured scope.","commands":{"allow":["set_cursor_grab"],"deny":[]}},"allow-set-cursor-icon":{"identifier":"allow-set-cursor-icon","description":"Enables the set_cursor_icon command without any pre-configured scope.","commands":{"allow":["set_cursor_icon"],"deny":[]}},"allow-set-cursor-position":{"identifier":"allow-set-cursor-position","description":"Enables the set_cursor_position command without any pre-configured scope.","commands":{"allow":["set_cursor_position"],"deny":[]}},"allow-set-cursor-visible":{"identifier":"allow-set-cursor-visible","description":"Enables the set_cursor_visible command without any pre-configured scope.","commands":{"allow":["set_cursor_visible"],"deny":[]}},"allow-set-decorations":{"identifier":"allow-set-decorations","description":"Enables the set_decorations command without any pre-configured scope.","commands":{"allow":["set_decorations"],"deny":[]}},"allow-set-effects":{"identifier":"allow-set-effects","description":"Enables the set_effects command without any pre-configured scope.","commands":{"allow":["set_effects"],"deny":[]}},"allow-set-enabled":{"identifier":"allow-set-enabled","description":"Enables the set_enabled command without any pre-configured scope.","commands":{"allow":["set_enabled"],"deny":[]}},"allow-set-focus":{"identifier":"allow-set-focus","description":"Enables the set_focus command without any pre-configured scope.","commands":{"allow":["set_focus"],"deny":[]}},"allow-set-focusable":{"identifier":"allow-set-focusable","description":"Enables the set_focusable command without any pre-configured scope.","commands":{"allow":["set_focusable"],"deny":[]}},"allow-set-fullscreen":{"identifier":"allow-set-fullscreen","description":"Enables the set_fullscreen command without any pre-configured scope.","commands":{"allow":["set_fullscreen"],"deny":[]}},"allow-set-fullscreen-on-monitor":{"identifier":"allow-set-fullscreen-on-monitor","description":"Enables the set_fullscreen_on_monitor command without any pre-configured scope.","commands":{"allow":["set_fullscreen_on_monitor"],"deny":[]}},"allow-set-icon":{"identifier":"allow-set-icon","description":"Enables the set_icon command without any pre-configured scope.","commands":{"allow":["set_icon"],"deny":[]}},"allow-set-ignore-cursor-events":{"identifier":"allow-set-ignore-cursor-events","description":"Enables the set_ignore_cursor_events command without any pre-configured scope.","commands":{"allow":["set_ignore_cursor_events"],"deny":[]}},"allow-set-max-size":{"identifier":"allow-set-max-size","description":"Enables the set_max_size command without any pre-configured scope.","commands":{"allow":["set_max_size"],"deny":[]}},"allow-set-maximizable":{"identifier":"allow-set-maximizable","description":"Enables the set_maximizable command without any pre-configured scope.","commands":{"allow":["set_maximizable"],"deny":[]}},"allow-set-min-size":{"identifier":"allow-set-min-size","description":"Enables the set_min_size command without any pre-configured scope.","commands":{"allow":["set_min_size"],"deny":[]}},"allow-set-minimizable":{"identifier":"allow-set-minimizable","description":"Enables the set_minimizable command without any pre-configured scope.","commands":{"allow":["set_minimizable"],"deny":[]}},"allow-set-overlay-icon":{"identifier":"allow-set-overlay-icon","description":"Enables the set_overlay_icon command without any pre-configured scope.","commands":{"allow":["set_overlay_icon"],"deny":[]}},"allow-set-position":{"identifier":"allow-set-position","description":"Enables the set_position command without any pre-configured scope.","commands":{"allow":["set_position"],"deny":[]}},"allow-set-progress-bar":{"identifier":"allow-set-progress-bar","description":"Enables the set_progress_bar command without any pre-configured scope.","commands":{"allow":["set_progress_bar"],"deny":[]}},"allow-set-resizable":{"identifier":"allow-set-resizable","description":"Enables the set_resizable command without any pre-configured scope.","commands":{"allow":["set_resizable"],"deny":[]}},"allow-set-shadow":{"identifier":"allow-set-shadow","description":"Enables the set_shadow command without any pre-configured scope.","commands":{"allow":["set_shadow"],"deny":[]}},"allow-set-simple-fullscreen":{"identifier":"allow-set-simple-fullscreen","description":"Enables the set_simple_fullscreen command without any pre-configured scope.","commands":{"allow":["set_simple_fullscreen"],"deny":[]}},"allow-set-size":{"identifier":"allow-set-size","description":"Enables the set_size command without any pre-configured scope.","commands":{"allow":["set_size"],"deny":[]}},"allow-set-size-constraints":{"identifier":"allow-set-size-constraints","description":"Enables the set_size_constraints command without any pre-configured scope.","commands":{"allow":["set_size_constraints"],"deny":[]}},"allow-set-skip-taskbar":{"identifier":"allow-set-skip-taskbar","description":"Enables the set_skip_taskbar command without any pre-configured scope.","commands":{"allow":["set_skip_taskbar"],"deny":[]}},"allow-set-theme":{"identifier":"allow-set-theme","description":"Enables the set_theme command without any pre-configured scope.","commands":{"allow":["set_theme"],"deny":[]}},"allow-set-title":{"identifier":"allow-set-title","description":"Enables the set_title command without any pre-configured scope.","commands":{"allow":["set_title"],"deny":[]}},"allow-set-title-bar-style":{"identifier":"allow-set-title-bar-style","description":"Enables the set_title_bar_style command without any pre-configured scope.","commands":{"allow":["set_title_bar_style"],"deny":[]}},"allow-set-visible-on-all-workspaces":{"identifier":"allow-set-visible-on-all-workspaces","description":"Enables the set_visible_on_all_workspaces command without any pre-configured scope.","commands":{"allow":["set_visible_on_all_workspaces"],"deny":[]}},"allow-show":{"identifier":"allow-show","description":"Enables the show command without any pre-configured scope.","commands":{"allow":["show"],"deny":[]}},"allow-start-dragging":{"identifier":"allow-start-dragging","description":"Enables the start_dragging command without any pre-configured scope.","commands":{"allow":["start_dragging"],"deny":[]}},"allow-start-resize-dragging":{"identifier":"allow-start-resize-dragging","description":"Enables the start_resize_dragging command without any pre-configured scope.","commands":{"allow":["start_resize_dragging"],"deny":[]}},"allow-theme":{"identifier":"allow-theme","description":"Enables the theme command without any pre-configured scope.","commands":{"allow":["theme"],"deny":[]}},"allow-title":{"identifier":"allow-title","description":"Enables the title command without any pre-configured scope.","commands":{"allow":["title"],"deny":[]}},"allow-toggle-maximize":{"identifier":"allow-toggle-maximize","description":"Enables the toggle_maximize command without any pre-configured scope.","commands":{"allow":["toggle_maximize"],"deny":[]}},"allow-unmaximize":{"identifier":"allow-unmaximize","description":"Enables the unmaximize command without any pre-configured scope.","commands":{"allow":["unmaximize"],"deny":[]}},"allow-unminimize":{"identifier":"allow-unminimize","description":"Enables the unminimize command without any pre-configured scope.","commands":{"allow":["unminimize"],"deny":[]}},"deny-activity-name":{"identifier":"deny-activity-name","description":"Denies the activity_name command without any pre-configured scope.","commands":{"allow":[],"deny":["activity_name"]}},"deny-available-monitors":{"identifier":"deny-available-monitors","description":"Denies the available_monitors command without any pre-configured scope.","commands":{"allow":[],"deny":["available_monitors"]}},"deny-center":{"identifier":"deny-center","description":"Denies the center command without any pre-configured scope.","commands":{"allow":[],"deny":["center"]}},"deny-close":{"identifier":"deny-close","description":"Denies the close command without any pre-configured scope.","commands":{"allow":[],"deny":["close"]}},"deny-create":{"identifier":"deny-create","description":"Denies the create command without any pre-configured scope.","commands":{"allow":[],"deny":["create"]}},"deny-current-monitor":{"identifier":"deny-current-monitor","description":"Denies the current_monitor command without any pre-configured scope.","commands":{"allow":[],"deny":["current_monitor"]}},"deny-cursor-position":{"identifier":"deny-cursor-position","description":"Denies the cursor_position command without any pre-configured scope.","commands":{"allow":[],"deny":["cursor_position"]}},"deny-destroy":{"identifier":"deny-destroy","description":"Denies the destroy command without any pre-configured scope.","commands":{"allow":[],"deny":["destroy"]}},"deny-get-all-windows":{"identifier":"deny-get-all-windows","description":"Denies the get_all_windows command without any pre-configured scope.","commands":{"allow":[],"deny":["get_all_windows"]}},"deny-hide":{"identifier":"deny-hide","description":"Denies the hide command without any pre-configured scope.","commands":{"allow":[],"deny":["hide"]}},"deny-inner-position":{"identifier":"deny-inner-position","description":"Denies the inner_position command without any pre-configured scope.","commands":{"allow":[],"deny":["inner_position"]}},"deny-inner-size":{"identifier":"deny-inner-size","description":"Denies the inner_size command without any pre-configured scope.","commands":{"allow":[],"deny":["inner_size"]}},"deny-internal-toggle-maximize":{"identifier":"deny-internal-toggle-maximize","description":"Denies the internal_toggle_maximize command without any pre-configured scope.","commands":{"allow":[],"deny":["internal_toggle_maximize"]}},"deny-is-always-on-top":{"identifier":"deny-is-always-on-top","description":"Denies the is_always_on_top command without any pre-configured scope.","commands":{"allow":[],"deny":["is_always_on_top"]}},"deny-is-closable":{"identifier":"deny-is-closable","description":"Denies the is_closable command without any pre-configured scope.","commands":{"allow":[],"deny":["is_closable"]}},"deny-is-decorated":{"identifier":"deny-is-decorated","description":"Denies the is_decorated command without any pre-configured scope.","commands":{"allow":[],"deny":["is_decorated"]}},"deny-is-enabled":{"identifier":"deny-is-enabled","description":"Denies the is_enabled command without any pre-configured scope.","commands":{"allow":[],"deny":["is_enabled"]}},"deny-is-focused":{"identifier":"deny-is-focused","description":"Denies the is_focused command without any pre-configured scope.","commands":{"allow":[],"deny":["is_focused"]}},"deny-is-fullscreen":{"identifier":"deny-is-fullscreen","description":"Denies the is_fullscreen command without any pre-configured scope.","commands":{"allow":[],"deny":["is_fullscreen"]}},"deny-is-maximizable":{"identifier":"deny-is-maximizable","description":"Denies the is_maximizable command without any pre-configured scope.","commands":{"allow":[],"deny":["is_maximizable"]}},"deny-is-maximized":{"identifier":"deny-is-maximized","description":"Denies the is_maximized command without any pre-configured scope.","commands":{"allow":[],"deny":["is_maximized"]}},"deny-is-minimizable":{"identifier":"deny-is-minimizable","description":"Denies the is_minimizable command without any pre-configured scope.","commands":{"allow":[],"deny":["is_minimizable"]}},"deny-is-minimized":{"identifier":"deny-is-minimized","description":"Denies the is_minimized command without any pre-configured scope.","commands":{"allow":[],"deny":["is_minimized"]}},"deny-is-resizable":{"identifier":"deny-is-resizable","description":"Denies the is_resizable command without any pre-configured scope.","commands":{"allow":[],"deny":["is_resizable"]}},"deny-is-visible":{"identifier":"deny-is-visible","description":"Denies the is_visible command without any pre-configured scope.","commands":{"allow":[],"deny":["is_visible"]}},"deny-maximize":{"identifier":"deny-maximize","description":"Denies the maximize command without any pre-configured scope.","commands":{"allow":[],"deny":["maximize"]}},"deny-minimize":{"identifier":"deny-minimize","description":"Denies the minimize command without any pre-configured scope.","commands":{"allow":[],"deny":["minimize"]}},"deny-monitor-from-point":{"identifier":"deny-monitor-from-point","description":"Denies the monitor_from_point command without any pre-configured scope.","commands":{"allow":[],"deny":["monitor_from_point"]}},"deny-outer-position":{"identifier":"deny-outer-position","description":"Denies the outer_position command without any pre-configured scope.","commands":{"allow":[],"deny":["outer_position"]}},"deny-outer-size":{"identifier":"deny-outer-size","description":"Denies the outer_size command without any pre-configured scope.","commands":{"allow":[],"deny":["outer_size"]}},"deny-primary-monitor":{"identifier":"deny-primary-monitor","description":"Denies the primary_monitor command without any pre-configured scope.","commands":{"allow":[],"deny":["primary_monitor"]}},"deny-request-user-attention":{"identifier":"deny-request-user-attention","description":"Denies the request_user_attention command without any pre-configured scope.","commands":{"allow":[],"deny":["request_user_attention"]}},"deny-scale-factor":{"identifier":"deny-scale-factor","description":"Denies the scale_factor command without any pre-configured scope.","commands":{"allow":[],"deny":["scale_factor"]}},"deny-scene-identifier":{"identifier":"deny-scene-identifier","description":"Denies the scene_identifier command without any pre-configured scope.","commands":{"allow":[],"deny":["scene_identifier"]}},"deny-set-always-on-bottom":{"identifier":"deny-set-always-on-bottom","description":"Denies the set_always_on_bottom command without any pre-configured scope.","commands":{"allow":[],"deny":["set_always_on_bottom"]}},"deny-set-always-on-top":{"identifier":"deny-set-always-on-top","description":"Denies the set_always_on_top command without any pre-configured scope.","commands":{"allow":[],"deny":["set_always_on_top"]}},"deny-set-background-color":{"identifier":"deny-set-background-color","description":"Denies the set_background_color command without any pre-configured scope.","commands":{"allow":[],"deny":["set_background_color"]}},"deny-set-badge-count":{"identifier":"deny-set-badge-count","description":"Denies the set_badge_count command without any pre-configured scope.","commands":{"allow":[],"deny":["set_badge_count"]}},"deny-set-badge-label":{"identifier":"deny-set-badge-label","description":"Denies the set_badge_label command without any pre-configured scope.","commands":{"allow":[],"deny":["set_badge_label"]}},"deny-set-closable":{"identifier":"deny-set-closable","description":"Denies the set_closable command without any pre-configured scope.","commands":{"allow":[],"deny":["set_closable"]}},"deny-set-content-protected":{"identifier":"deny-set-content-protected","description":"Denies the set_content_protected command without any pre-configured scope.","commands":{"allow":[],"deny":["set_content_protected"]}},"deny-set-cursor-grab":{"identifier":"deny-set-cursor-grab","description":"Denies the set_cursor_grab command without any pre-configured scope.","commands":{"allow":[],"deny":["set_cursor_grab"]}},"deny-set-cursor-icon":{"identifier":"deny-set-cursor-icon","description":"Denies the set_cursor_icon command without any pre-configured scope.","commands":{"allow":[],"deny":["set_cursor_icon"]}},"deny-set-cursor-position":{"identifier":"deny-set-cursor-position","description":"Denies the set_cursor_position command without any pre-configured scope.","commands":{"allow":[],"deny":["set_cursor_position"]}},"deny-set-cursor-visible":{"identifier":"deny-set-cursor-visible","description":"Denies the set_cursor_visible command without any pre-configured scope.","commands":{"allow":[],"deny":["set_cursor_visible"]}},"deny-set-decorations":{"identifier":"deny-set-decorations","description":"Denies the set_decorations command without any pre-configured scope.","commands":{"allow":[],"deny":["set_decorations"]}},"deny-set-effects":{"identifier":"deny-set-effects","description":"Denies the set_effects command without any pre-configured scope.","commands":{"allow":[],"deny":["set_effects"]}},"deny-set-enabled":{"identifier":"deny-set-enabled","description":"Denies the set_enabled command without any pre-configured scope.","commands":{"allow":[],"deny":["set_enabled"]}},"deny-set-focus":{"identifier":"deny-set-focus","description":"Denies the set_focus command without any pre-configured scope.","commands":{"allow":[],"deny":["set_focus"]}},"deny-set-focusable":{"identifier":"deny-set-focusable","description":"Denies the set_focusable command without any pre-configured scope.","commands":{"allow":[],"deny":["set_focusable"]}},"deny-set-fullscreen":{"identifier":"deny-set-fullscreen","description":"Denies the set_fullscreen command without any pre-configured scope.","commands":{"allow":[],"deny":["set_fullscreen"]}},"deny-set-fullscreen-on-monitor":{"identifier":"deny-set-fullscreen-on-monitor","description":"Denies the set_fullscreen_on_monitor command without any pre-configured scope.","commands":{"allow":[],"deny":["set_fullscreen_on_monitor"]}},"deny-set-icon":{"identifier":"deny-set-icon","description":"Denies the set_icon command without any pre-configured scope.","commands":{"allow":[],"deny":["set_icon"]}},"deny-set-ignore-cursor-events":{"identifier":"deny-set-ignore-cursor-events","description":"Denies the set_ignore_cursor_events command without any pre-configured scope.","commands":{"allow":[],"deny":["set_ignore_cursor_events"]}},"deny-set-max-size":{"identifier":"deny-set-max-size","description":"Denies the set_max_size command without any pre-configured scope.","commands":{"allow":[],"deny":["set_max_size"]}},"deny-set-maximizable":{"identifier":"deny-set-maximizable","description":"Denies the set_maximizable command without any pre-configured scope.","commands":{"allow":[],"deny":["set_maximizable"]}},"deny-set-min-size":{"identifier":"deny-set-min-size","description":"Denies the set_min_size command without any pre-configured scope.","commands":{"allow":[],"deny":["set_min_size"]}},"deny-set-minimizable":{"identifier":"deny-set-minimizable","description":"Denies the set_minimizable command without any pre-configured scope.","commands":{"allow":[],"deny":["set_minimizable"]}},"deny-set-overlay-icon":{"identifier":"deny-set-overlay-icon","description":"Denies the set_overlay_icon command without any pre-configured scope.","commands":{"allow":[],"deny":["set_overlay_icon"]}},"deny-set-position":{"identifier":"deny-set-position","description":"Denies the set_position command without any pre-configured scope.","commands":{"allow":[],"deny":["set_position"]}},"deny-set-progress-bar":{"identifier":"deny-set-progress-bar","description":"Denies the set_progress_bar command without any pre-configured scope.","commands":{"allow":[],"deny":["set_progress_bar"]}},"deny-set-resizable":{"identifier":"deny-set-resizable","description":"Denies the set_resizable command without any pre-configured scope.","commands":{"allow":[],"deny":["set_resizable"]}},"deny-set-shadow":{"identifier":"deny-set-shadow","description":"Denies the set_shadow command without any pre-configured scope.","commands":{"allow":[],"deny":["set_shadow"]}},"deny-set-simple-fullscreen":{"identifier":"deny-set-simple-fullscreen","description":"Denies the set_simple_fullscreen command without any pre-configured scope.","commands":{"allow":[],"deny":["set_simple_fullscreen"]}},"deny-set-size":{"identifier":"deny-set-size","description":"Denies the set_size command without any pre-configured scope.","commands":{"allow":[],"deny":["set_size"]}},"deny-set-size-constraints":{"identifier":"deny-set-size-constraints","description":"Denies the set_size_constraints command without any pre-configured scope.","commands":{"allow":[],"deny":["set_size_constraints"]}},"deny-set-skip-taskbar":{"identifier":"deny-set-skip-taskbar","description":"Denies the set_skip_taskbar command without any pre-configured scope.","commands":{"allow":[],"deny":["set_skip_taskbar"]}},"deny-set-theme":{"identifier":"deny-set-theme","description":"Denies the set_theme command without any pre-configured scope.","commands":{"allow":[],"deny":["set_theme"]}},"deny-set-title":{"identifier":"deny-set-title","description":"Denies the set_title command without any pre-configured scope.","commands":{"allow":[],"deny":["set_title"]}},"deny-set-title-bar-style":{"identifier":"deny-set-title-bar-style","description":"Denies the set_title_bar_style command without any pre-configured scope.","commands":{"allow":[],"deny":["set_title_bar_style"]}},"deny-set-visible-on-all-workspaces":{"identifier":"deny-set-visible-on-all-workspaces","description":"Denies the set_visible_on_all_workspaces command without any pre-configured scope.","commands":{"allow":[],"deny":["set_visible_on_all_workspaces"]}},"deny-show":{"identifier":"deny-show","description":"Denies the show command without any pre-configured scope.","commands":{"allow":[],"deny":["show"]}},"deny-start-dragging":{"identifier":"deny-start-dragging","description":"Denies the start_dragging command without any pre-configured scope.","commands":{"allow":[],"deny":["start_dragging"]}},"deny-start-resize-dragging":{"identifier":"deny-start-resize-dragging","description":"Denies the start_resize_dragging command without any pre-configured scope.","commands":{"allow":[],"deny":["start_resize_dragging"]}},"deny-theme":{"identifier":"deny-theme","description":"Denies the theme command without any pre-configured scope.","commands":{"allow":[],"deny":["theme"]}},"deny-title":{"identifier":"deny-title","description":"Denies the title command without any pre-configured scope.","commands":{"allow":[],"deny":["title"]}},"deny-toggle-maximize":{"identifier":"deny-toggle-maximize","description":"Denies the toggle_maximize command without any pre-configured scope.","commands":{"allow":[],"deny":["toggle_maximize"]}},"deny-unmaximize":{"identifier":"deny-unmaximize","description":"Denies the unmaximize command without any pre-configured scope.","commands":{"allow":[],"deny":["unmaximize"]}},"deny-unminimize":{"identifier":"deny-unminimize","description":"Denies the unminimize command without any pre-configured scope.","commands":{"allow":[],"deny":["unminimize"]}}},"permission_sets":{},"global_scope_schema":null},"dialog":{"default_permission":{"identifier":"default","description":"This permission set configures the types of dialogs\navailable from the dialog plugin.\n\n#### Granted Permissions\n\nAll dialog types are enabled.\n\n\n","permissions":["allow-message","allow-save","allow-open"]},"permissions":{"allow-ask":{"identifier":"allow-ask","description":"Enables the ask command without any pre-configured scope. (**DEPRECATED**: This is now an alias to `allow-message` and will be removed in v3)","commands":{"allow":["message"],"deny":[]}},"allow-confirm":{"identifier":"allow-confirm","description":"Enables the confirm command without any pre-configured scope. (**DEPRECATED**: This is now an alias to `allow-message` and will be removed in v3)","commands":{"allow":["message"],"deny":[]}},"allow-message":{"identifier":"allow-message","description":"Enables the message command without any pre-configured scope.","commands":{"allow":["message"],"deny":[]}},"allow-open":{"identifier":"allow-open","description":"Enables the open command without any pre-configured scope.","commands":{"allow":["open"],"deny":[]}},"allow-save":{"identifier":"allow-save","description":"Enables the save command without any pre-configured scope.","commands":{"allow":["save"],"deny":[]}},"deny-ask":{"identifier":"deny-ask","description":"Denies the ask command without any pre-configured scope. (**DEPRECATED**: This is now an alias to `deny-message` and will be removed in v3)","commands":{"allow":[],"deny":["message"]}},"deny-confirm":{"identifier":"deny-confirm","description":"Denies the confirm command without any pre-configured scope. (**DEPRECATED**: This is now an alias to `deny-message` and will be removed in v3)","commands":{"allow":[],"deny":["message"]}},"deny-message":{"identifier":"deny-message","description":"Denies the message command without any pre-configured scope.","commands":{"allow":[],"deny":["message"]}},"deny-open":{"identifier":"deny-open","description":"Denies the open command without any pre-configured scope.","commands":{"allow":[],"deny":["open"]}},"deny-save":{"identifier":"deny-save","description":"Denies the save command without any pre-configured scope.","commands":{"allow":[],"deny":["save"]}}},"permission_sets":{},"global_scope_schema":null}}
````

## File: app/src-tauri/gen/schemas/capabilities.json
````json
{"default":{"identifier":"default","description":"Permisos de la isla y de la ventana de configuración","local":true,"windows":["island","settings"],"permissions":["core:default","core:window:allow-start-dragging","core:window:allow-minimize","core:window:allow-close","core:window:allow-hide","core:window:allow-show","core:window:allow-set-focus","dialog:allow-open"]}}
````

## File: app/src-tauri/gen/schemas/desktop-schema.json
````json
{
  "$schema": "http://json-schema.org/draft-07/schema#",
  "title": "CapabilityFile",
  "description": "Capability formats accepted in a capability file.",
  "anyOf": [
    {
      "description": "A single capability.",
      "allOf": [
        {
          "$ref": "#/definitions/Capability"
        }
      ]
    },
    {
      "description": "A list of capabilities.",
      "type": "array",
      "items": {
        "$ref": "#/definitions/Capability"
      }
    },
    {
      "description": "A list of capabilities.",
      "type": "object",
      "required": [
        "capabilities"
      ],
      "properties": {
        "capabilities": {
          "description": "The list of capabilities.",
          "type": "array",
          "items": {
            "$ref": "#/definitions/Capability"
          }
        }
      }
    }
  ],
  "definitions": {
    "Capability": {
      "description": "A grouping and boundary mechanism developers can use to isolate access to the IPC layer.\n\nIt controls application windows' and webviews' fine grained access to the Tauri core, application, or plugin commands. If a webview or its window is not matching any capability then it has no access to the IPC layer at all.\n\nThis can be done to create groups of windows, based on their required system access, which can reduce impact of frontend vulnerabilities in less privileged windows. Windows can be added to a capability by exact name (e.g. `main-window`) or glob patterns like `*` or `admin-*`. A Window can have none, one, or multiple associated capabilities.\n\n## Example\n\n```json { \"identifier\": \"main-user-files-write\", \"description\": \"This capability allows the `main` window on macOS and Windows access to `filesystem` write related commands and `dialog` commands to enable programmatic access to files selected by the user.\", \"windows\": [ \"main\" ], \"permissions\": [ \"core:default\", \"dialog:open\", { \"identifier\": \"fs:allow-write-text-file\", \"allow\": [{ \"path\": \"$HOME/test.txt\" }] }, ], \"platforms\": [\"macOS\",\"windows\"] } ```",
      "type": "object",
      "required": [
        "identifier",
        "permissions"
      ],
      "properties": {
        "identifier": {
          "description": "Identifier of the capability.\n\n## Example\n\n`main-user-files-write`",
          "type": "string"
        },
        "description": {
          "description": "Description of what the capability is intended to allow on associated windows.\n\nIt should contain a description of what the grouped permissions should allow.\n\n## Example\n\nThis capability allows the `main` window access to `filesystem` write related commands and `dialog` commands to enable programmatic access to files selected by the user.",
          "default": "",
          "type": "string"
        },
        "remote": {
          "description": "Configure remote URLs that can use the capability permissions.\n\nThis setting is optional and defaults to not being set, as our default use case is that the content is served from our local application.\n\n:::caution Make sure you understand the security implications of providing remote sources with local system access. :::\n\n## Example\n\n```json { \"urls\": [\"https://*.mydomain.dev\"] } ```",
          "anyOf": [
            {
              "$ref": "#/definitions/CapabilityRemote"
            },
            {
              "type": "null"
            }
          ]
        },
        "local": {
          "description": "Whether this capability is enabled for local app URLs or not. Defaults to `true`.",
          "default": true,
          "type": "boolean"
        },
        "windows": {
          "description": "List of windows that are affected by this capability. Can be a glob pattern.\n\nIf a window label matches any of the patterns in this list, the capability will be enabled on all the webviews of that window, regardless of the value of [`Self::webviews`].\n\nOn multiwebview windows, prefer specifying [`Self::webviews`] and omitting [`Self::windows`] for a fine grained access control.\n\n## Example\n\n`[\"main\"]`",
          "type": "array",
          "items": {
            "type": "string"
          }
        },
        "webviews": {
          "description": "List of webviews that are affected by this capability. Can be a glob pattern.\n\nThe capability will be enabled on all the webviews whose label matches any of the patterns in this list, regardless of whether the webview's window label matches a pattern in [`Self::windows`].\n\n## Example\n\n`[\"sub-webview-one\", \"sub-webview-two\"]`",
          "type": "array",
          "items": {
            "type": "string"
          }
        },
        "permissions": {
          "description": "List of permissions attached to this capability.\n\nMust include the plugin name as prefix in the form of `${plugin-name}:${permission-name}`. For commands directly implemented in the application itself only `${permission-name}` is required.\n\n## Example\n\n```json [ \"core:default\", \"shell:allow-open\", \"dialog:open\", { \"identifier\": \"fs:allow-write-text-file\", \"allow\": [{ \"path\": \"$HOME/test.txt\" }] } ] ```",
          "type": "array",
          "items": {
            "$ref": "#/definitions/PermissionEntry"
          },
          "uniqueItems": true
        },
        "platforms": {
          "description": "Limit which target platforms this capability applies to.\n\nBy default all platforms are targeted.\n\n## Example\n\n`[\"macOS\",\"windows\"]`",
          "type": [
            "array",
            "null"
          ],
          "items": {
            "$ref": "#/definitions/Target"
          }
        }
      }
    },
    "CapabilityRemote": {
      "description": "Configuration for remote URLs that are associated with the capability.",
      "type": "object",
      "required": [
        "urls"
      ],
      "properties": {
        "urls": {
          "description": "Remote domains this capability refers to using the [URLPattern standard](https://urlpattern.spec.whatwg.org/).\n\n## Examples\n\n- \"https://*.mydomain.dev\": allows subdomains of mydomain.dev - \"https://mydomain.dev/api/*\": allows any subpath of mydomain.dev/api",
          "type": "array",
          "items": {
            "type": "string"
          }
        }
      }
    },
    "PermissionEntry": {
      "description": "An entry for a permission value in a [`Capability`] can be either a raw permission [`Identifier`] or an object that references a permission and extends its scope.",
      "anyOf": [
        {
          "description": "Reference a permission or permission set by identifier.",
          "allOf": [
            {
              "$ref": "#/definitions/Identifier"
            }
          ]
        },
        {
          "description": "Reference a permission or permission set by identifier and extends its scope.",
          "type": "object",
          "allOf": [
            {
              "properties": {
                "identifier": {
                  "description": "Identifier of the permission or permission set.",
                  "allOf": [
                    {
                      "$ref": "#/definitions/Identifier"
                    }
                  ]
                },
                "allow": {
                  "description": "Data that defines what is allowed by the scope.",
                  "type": [
                    "array",
                    "null"
                  ],
                  "items": {
                    "$ref": "#/definitions/Value"
                  }
                },
                "deny": {
                  "description": "Data that defines what is denied by the scope. This should be prioritized by validation logic.",
                  "type": [
                    "array",
                    "null"
                  ],
                  "items": {
                    "$ref": "#/definitions/Value"
                  }
                }
              }
            }
          ],
          "required": [
            "identifier"
          ]
        }
      ]
    },
    "Identifier": {
      "description": "Permission identifier",
      "oneOf": [
        {
          "description": "Default core plugins set.\n#### This default permission set includes:\n\n- `core:path:default`\n- `core:event:default`\n- `core:window:default`\n- `core:webview:default`\n- `core:app:default`\n- `core:image:default`\n- `core:resources:default`\n- `core:menu:default`\n- `core:tray:default`",
          "type": "string",
          "const": "core:default",
          "markdownDescription": "Default core plugins set.\n#### This default permission set includes:\n\n- `core:path:default`\n- `core:event:default`\n- `core:window:default`\n- `core:webview:default`\n- `core:app:default`\n- `core:image:default`\n- `core:resources:default`\n- `core:menu:default`\n- `core:tray:default`"
        },
        {
          "description": "Default permissions for the plugin.\n#### This default permission set includes:\n\n- `allow-version`\n- `allow-name`\n- `allow-tauri-version`\n- `allow-identifier`\n- `allow-bundle-type`\n- `allow-register-listener`\n- `allow-remove-listener`\n- `allow-supports-multiple-windows`",
          "type": "string",
          "const": "core:app:default",
          "markdownDescription": "Default permissions for the plugin.\n#### This default permission set includes:\n\n- `allow-version`\n- `allow-name`\n- `allow-tauri-version`\n- `allow-identifier`\n- `allow-bundle-type`\n- `allow-register-listener`\n- `allow-remove-listener`\n- `allow-supports-multiple-windows`"
        },
        {
          "description": "Enables the app_hide command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-app-hide",
          "markdownDescription": "Enables the app_hide command without any pre-configured scope."
        },
        {
          "description": "Enables the app_show command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-app-show",
          "markdownDescription": "Enables the app_show command without any pre-configured scope."
        },
        {
          "description": "Enables the bundle_type command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-bundle-type",
          "markdownDescription": "Enables the bundle_type command without any pre-configured scope."
        },
        {
          "description": "Enables the default_window_icon command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-default-window-icon",
          "markdownDescription": "Enables the default_window_icon command without any pre-configured scope."
        },
        {
          "description": "Enables the exit command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-exit",
          "markdownDescription": "Enables the exit command without any pre-configured scope."
        },
        {
          "description": "Enables the fetch_data_store_identifiers command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-fetch-data-store-identifiers",
          "markdownDescription": "Enables the fetch_data_store_identifiers command without any pre-configured scope."
        },
        {
          "description": "Enables the identifier command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-identifier",
          "markdownDescription": "Enables the identifier command without any pre-configured scope."
        },
        {
          "description": "Enables the name command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-name",
          "markdownDescription": "Enables the name command without any pre-configured scope."
        },
        {
          "description": "Enables the register_listener command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-register-listener",
          "markdownDescription": "Enables the register_listener command without any pre-configured scope."
        },
        {
          "description": "Enables the remove_data_store command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-remove-data-store",
          "markdownDescription": "Enables the remove_data_store command without any pre-configured scope."
        },
        {
          "description": "Enables the remove_listener command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-remove-listener",
          "markdownDescription": "Enables the remove_listener command without any pre-configured scope."
        },
        {
          "description": "Enables the set_app_theme command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-set-app-theme",
          "markdownDescription": "Enables the set_app_theme command without any pre-configured scope."
        },
        {
          "description": "Enables the set_dock_visibility command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-set-dock-visibility",
          "markdownDescription": "Enables the set_dock_visibility command without any pre-configured scope."
        },
        {
          "description": "Enables the supports_multiple_windows command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-supports-multiple-windows",
          "markdownDescription": "Enables the supports_multiple_windows command without any pre-configured scope."
        },
        {
          "description": "Enables the tauri_version command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-tauri-version",
          "markdownDescription": "Enables the tauri_version command without any pre-configured scope."
        },
        {
          "description": "Enables the version command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-version",
          "markdownDescription": "Enables the version command without any pre-configured scope."
        },
        {
          "description": "Denies the app_hide command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-app-hide",
          "markdownDescription": "Denies the app_hide command without any pre-configured scope."
        },
        {
          "description": "Denies the app_show command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-app-show",
          "markdownDescription": "Denies the app_show command without any pre-configured scope."
        },
        {
          "description": "Denies the bundle_type command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-bundle-type",
          "markdownDescription": "Denies the bundle_type command without any pre-configured scope."
        },
        {
          "description": "Denies the default_window_icon command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-default-window-icon",
          "markdownDescription": "Denies the default_window_icon command without any pre-configured scope."
        },
        {
          "description": "Denies the exit command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-exit",
          "markdownDescription": "Denies the exit command without any pre-configured scope."
        },
        {
          "description": "Denies the fetch_data_store_identifiers command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-fetch-data-store-identifiers",
          "markdownDescription": "Denies the fetch_data_store_identifiers command without any pre-configured scope."
        },
        {
          "description": "Denies the identifier command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-identifier",
          "markdownDescription": "Denies the identifier command without any pre-configured scope."
        },
        {
          "description": "Denies the name command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-name",
          "markdownDescription": "Denies the name command without any pre-configured scope."
        },
        {
          "description": "Denies the register_listener command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-register-listener",
          "markdownDescription": "Denies the register_listener command without any pre-configured scope."
        },
        {
          "description": "Denies the remove_data_store command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-remove-data-store",
          "markdownDescription": "Denies the remove_data_store command without any pre-configured scope."
        },
        {
          "description": "Denies the remove_listener command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-remove-listener",
          "markdownDescription": "Denies the remove_listener command without any pre-configured scope."
        },
        {
          "description": "Denies the set_app_theme command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-set-app-theme",
          "markdownDescription": "Denies the set_app_theme command without any pre-configured scope."
        },
        {
          "description": "Denies the set_dock_visibility command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-set-dock-visibility",
          "markdownDescription": "Denies the set_dock_visibility command without any pre-configured scope."
        },
        {
          "description": "Denies the supports_multiple_windows command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-supports-multiple-windows",
          "markdownDescription": "Denies the supports_multiple_windows command without any pre-configured scope."
        },
        {
          "description": "Denies the tauri_version command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-tauri-version",
          "markdownDescription": "Denies the tauri_version command without any pre-configured scope."
        },
        {
          "description": "Denies the version command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-version",
          "markdownDescription": "Denies the version command without any pre-configured scope."
        },
        {
          "description": "Default permissions for the plugin, which enables all commands.\n#### This default permission set includes:\n\n- `allow-listen`\n- `allow-unlisten`\n- `allow-emit`\n- `allow-emit-to`",
          "type": "string",
          "const": "core:event:default",
          "markdownDescription": "Default permissions for the plugin, which enables all commands.\n#### This default permission set includes:\n\n- `allow-listen`\n- `allow-unlisten`\n- `allow-emit`\n- `allow-emit-to`"
        },
        {
          "description": "Enables the emit command without any pre-configured scope.",
          "type": "string",
          "const": "core:event:allow-emit",
          "markdownDescription": "Enables the emit command without any pre-configured scope."
        },
        {
          "description": "Enables the emit_to command without any pre-configured scope.",
          "type": "string",
          "const": "core:event:allow-emit-to",
          "markdownDescription": "Enables the emit_to command without any pre-configured scope."
        },
        {
          "description": "Enables the listen command without any pre-configured scope.",
          "type": "string",
          "const": "core:event:allow-listen",
          "markdownDescription": "Enables the listen command without any pre-configured scope."
        },
        {
          "description": "Enables the unlisten command without any pre-configured scope.",
          "type": "string",
          "const": "core:event:allow-unlisten",
          "markdownDescription": "Enables the unlisten command without any pre-configured scope."
        },
        {
          "description": "Denies the emit command without any pre-configured scope.",
          "type": "string",
          "const": "core:event:deny-emit",
          "markdownDescription": "Denies the emit command without any pre-configured scope."
        },
        {
          "description": "Denies the emit_to command without any pre-configured scope.",
          "type": "string",
          "const": "core:event:deny-emit-to",
          "markdownDescription": "Denies the emit_to command without any pre-configured scope."
        },
        {
          "description": "Denies the listen command without any pre-configured scope.",
          "type": "string",
          "const": "core:event:deny-listen",
          "markdownDescription": "Denies the listen command without any pre-configured scope."
        },
        {
          "description": "Denies the unlisten command without any pre-configured scope.",
          "type": "string",
          "const": "core:event:deny-unlisten",
          "markdownDescription": "Denies the unlisten command without any pre-configured scope."
        },
        {
          "description": "Default permissions for the plugin, which enables all commands.\n#### This default permission set includes:\n\n- `allow-new`\n- `allow-from-bytes`\n- `allow-from-path`\n- `allow-rgba`\n- `allow-size`",
          "type": "string",
          "const": "core:image:default",
          "markdownDescription": "Default permissions for the plugin, which enables all commands.\n#### This default permission set includes:\n\n- `allow-new`\n- `allow-from-bytes`\n- `allow-from-path`\n- `allow-rgba`\n- `allow-size`"
        },
        {
          "description": "Enables the from_bytes command without any pre-configured scope.",
          "type": "string",
          "const": "core:image:allow-from-bytes",
          "markdownDescription": "Enables the from_bytes command without any pre-configured scope."
        },
        {
          "description": "Enables the from_path command without any pre-configured scope.",
          "type": "string",
          "const": "core:image:allow-from-path",
          "markdownDescription": "Enables the from_path command without any pre-configured scope."
        },
        {
          "description": "Enables the new command without any pre-configured scope.",
          "type": "string",
          "const": "core:image:allow-new",
          "markdownDescription": "Enables the new command without any pre-configured scope."
        },
        {
          "description": "Enables the rgba command without any pre-configured scope.",
          "type": "string",
          "const": "core:image:allow-rgba",
          "markdownDescription": "Enables the rgba command without any pre-configured scope."
        },
        {
          "description": "Enables the size command without any pre-configured scope.",
          "type": "string",
          "const": "core:image:allow-size",
          "markdownDescription": "Enables the size command without any pre-configured scope."
        },
        {
          "description": "Denies the from_bytes command without any pre-configured scope.",
          "type": "string",
          "const": "core:image:deny-from-bytes",
          "markdownDescription": "Denies the from_bytes command without any pre-configured scope."
        },
        {
          "description": "Denies the from_path command without any pre-configured scope.",
          "type": "string",
          "const": "core:image:deny-from-path",
          "markdownDescription": "Denies the from_path command without any pre-configured scope."
        },
        {
          "description": "Denies the new command without any pre-configured scope.",
          "type": "string",
          "const": "core:image:deny-new",
          "markdownDescription": "Denies the new command without any pre-configured scope."
        },
        {
          "description": "Denies the rgba command without any pre-configured scope.",
          "type": "string",
          "const": "core:image:deny-rgba",
          "markdownDescription": "Denies the rgba command without any pre-configured scope."
        },
        {
          "description": "Denies the size command without any pre-configured scope.",
          "type": "string",
          "const": "core:image:deny-size",
          "markdownDescription": "Denies the size command without any pre-configured scope."
        },
        {
          "description": "Default permissions for the plugin, which enables all commands.\n#### This default permission set includes:\n\n- `allow-new`\n- `allow-append`\n- `allow-prepend`\n- `allow-insert`\n- `allow-remove`\n- `allow-remove-at`\n- `allow-items`\n- `allow-get`\n- `allow-popup`\n- `allow-create-default`\n- `allow-set-as-app-menu`\n- `allow-set-as-window-menu`\n- `allow-text`\n- `allow-set-text`\n- `allow-is-enabled`\n- `allow-set-enabled`\n- `allow-set-accelerator`\n- `allow-set-as-windows-menu-for-nsapp`\n- `allow-set-as-help-menu-for-nsapp`\n- `allow-is-checked`\n- `allow-set-checked`\n- `allow-set-icon`",
          "type": "string",
          "const": "core:menu:default",
          "markdownDescription": "Default permissions for the plugin, which enables all commands.\n#### This default permission set includes:\n\n- `allow-new`\n- `allow-append`\n- `allow-prepend`\n- `allow-insert`\n- `allow-remove`\n- `allow-remove-at`\n- `allow-items`\n- `allow-get`\n- `allow-popup`\n- `allow-create-default`\n- `allow-set-as-app-menu`\n- `allow-set-as-window-menu`\n- `allow-text`\n- `allow-set-text`\n- `allow-is-enabled`\n- `allow-set-enabled`\n- `allow-set-accelerator`\n- `allow-set-as-windows-menu-for-nsapp`\n- `allow-set-as-help-menu-for-nsapp`\n- `allow-is-checked`\n- `allow-set-checked`\n- `allow-set-icon`"
        },
        {
          "description": "Enables the append command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-append",
          "markdownDescription": "Enables the append command without any pre-configured scope."
        },
        {
          "description": "Enables the create_default command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-create-default",
          "markdownDescription": "Enables the create_default command without any pre-configured scope."
        },
        {
          "description": "Enables the get command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-get",
          "markdownDescription": "Enables the get command without any pre-configured scope."
        },
        {
          "description": "Enables the insert command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-insert",
          "markdownDescription": "Enables the insert command without any pre-configured scope."
        },
        {
          "description": "Enables the is_checked command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-is-checked",
          "markdownDescription": "Enables the is_checked command without any pre-configured scope."
        },
        {
          "description": "Enables the is_enabled command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-is-enabled",
          "markdownDescription": "Enables the is_enabled command without any pre-configured scope."
        },
        {
          "description": "Enables the items command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-items",
          "markdownDescription": "Enables the items command without any pre-configured scope."
        },
        {
          "description": "Enables the new command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-new",
          "markdownDescription": "Enables the new command without any pre-configured scope."
        },
        {
          "description": "Enables the popup command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-popup",
          "markdownDescription": "Enables the popup command without any pre-configured scope."
        },
        {
          "description": "Enables the prepend command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-prepend",
          "markdownDescription": "Enables the prepend command without any pre-configured scope."
        },
        {
          "description": "Enables the remove command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-remove",
          "markdownDescription": "Enables the remove command without any pre-configured scope."
        },
        {
          "description": "Enables the remove_at command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-remove-at",
          "markdownDescription": "Enables the remove_at command without any pre-configured scope."
        },
        {
          "description": "Enables the set_accelerator command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-set-accelerator",
          "markdownDescription": "Enables the set_accelerator command without any pre-configured scope."
        },
        {
          "description": "Enables the set_as_app_menu command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-set-as-app-menu",
          "markdownDescription": "Enables the set_as_app_menu command without any pre-configured scope."
        },
        {
          "description": "Enables the set_as_help_menu_for_nsapp command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-set-as-help-menu-for-nsapp",
          "markdownDescription": "Enables the set_as_help_menu_for_nsapp command without any pre-configured scope."
        },
        {
          "description": "Enables the set_as_window_menu command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-set-as-window-menu",
          "markdownDescription": "Enables the set_as_window_menu command without any pre-configured scope."
        },
        {
          "description": "Enables the set_as_windows_menu_for_nsapp command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-set-as-windows-menu-for-nsapp",
          "markdownDescription": "Enables the set_as_windows_menu_for_nsapp command without any pre-configured scope."
        },
        {
          "description": "Enables the set_checked command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-set-checked",
          "markdownDescription": "Enables the set_checked command without any pre-configured scope."
        },
        {
          "description": "Enables the set_enabled command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-set-enabled",
          "markdownDescription": "Enables the set_enabled command without any pre-configured scope."
        },
        {
          "description": "Enables the set_icon command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-set-icon",
          "markdownDescription": "Enables the set_icon command without any pre-configured scope."
        },
        {
          "description": "Enables the set_text command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-set-text",
          "markdownDescription": "Enables the set_text command without any pre-configured scope."
        },
        {
          "description": "Enables the text command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-text",
          "markdownDescription": "Enables the text command without any pre-configured scope."
        },
        {
          "description": "Denies the append command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-append",
          "markdownDescription": "Denies the append command without any pre-configured scope."
        },
        {
          "description": "Denies the create_default command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-create-default",
          "markdownDescription": "Denies the create_default command without any pre-configured scope."
        },
        {
          "description": "Denies the get command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-get",
          "markdownDescription": "Denies the get command without any pre-configured scope."
        },
        {
          "description": "Denies the insert command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-insert",
          "markdownDescription": "Denies the insert command without any pre-configured scope."
        },
        {
          "description": "Denies the is_checked command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-is-checked",
          "markdownDescription": "Denies the is_checked command without any pre-configured scope."
        },
        {
          "description": "Denies the is_enabled command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-is-enabled",
          "markdownDescription": "Denies the is_enabled command without any pre-configured scope."
        },
        {
          "description": "Denies the items command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-items",
          "markdownDescription": "Denies the items command without any pre-configured scope."
        },
        {
          "description": "Denies the new command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-new",
          "markdownDescription": "Denies the new command without any pre-configured scope."
        },
        {
          "description": "Denies the popup command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-popup",
          "markdownDescription": "Denies the popup command without any pre-configured scope."
        },
        {
          "description": "Denies the prepend command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-prepend",
          "markdownDescription": "Denies the prepend command without any pre-configured scope."
        },
        {
          "description": "Denies the remove command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-remove",
          "markdownDescription": "Denies the remove command without any pre-configured scope."
        },
        {
          "description": "Denies the remove_at command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-remove-at",
          "markdownDescription": "Denies the remove_at command without any pre-configured scope."
        },
        {
          "description": "Denies the set_accelerator command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-set-accelerator",
          "markdownDescription": "Denies the set_accelerator command without any pre-configured scope."
        },
        {
          "description": "Denies the set_as_app_menu command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-set-as-app-menu",
          "markdownDescription": "Denies the set_as_app_menu command without any pre-configured scope."
        },
        {
          "description": "Denies the set_as_help_menu_for_nsapp command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-set-as-help-menu-for-nsapp",
          "markdownDescription": "Denies the set_as_help_menu_for_nsapp command without any pre-configured scope."
        },
        {
          "description": "Denies the set_as_window_menu command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-set-as-window-menu",
          "markdownDescription": "Denies the set_as_window_menu command without any pre-configured scope."
        },
        {
          "description": "Denies the set_as_windows_menu_for_nsapp command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-set-as-windows-menu-for-nsapp",
          "markdownDescription": "Denies the set_as_windows_menu_for_nsapp command without any pre-configured scope."
        },
        {
          "description": "Denies the set_checked command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-set-checked",
          "markdownDescription": "Denies the set_checked command without any pre-configured scope."
        },
        {
          "description": "Denies the set_enabled command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-set-enabled",
          "markdownDescription": "Denies the set_enabled command without any pre-configured scope."
        },
        {
          "description": "Denies the set_icon command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-set-icon",
          "markdownDescription": "Denies the set_icon command without any pre-configured scope."
        },
        {
          "description": "Denies the set_text command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-set-text",
          "markdownDescription": "Denies the set_text command without any pre-configured scope."
        },
        {
          "description": "Denies the text command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-text",
          "markdownDescription": "Denies the text command without any pre-configured scope."
        },
        {
          "description": "Default permissions for the plugin, which enables all commands.\n#### This default permission set includes:\n\n- `allow-resolve-directory`\n- `allow-resolve`\n- `allow-normalize`\n- `allow-join`\n- `allow-dirname`\n- `allow-extname`\n- `allow-basename`\n- `allow-is-absolute`",
          "type": "string",
          "const": "core:path:default",
          "markdownDescription": "Default permissions for the plugin, which enables all commands.\n#### This default permission set includes:\n\n- `allow-resolve-directory`\n- `allow-resolve`\n- `allow-normalize`\n- `allow-join`\n- `allow-dirname`\n- `allow-extname`\n- `allow-basename`\n- `allow-is-absolute`"
        },
        {
          "description": "Enables the basename command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:allow-basename",
          "markdownDescription": "Enables the basename command without any pre-configured scope."
        },
        {
          "description": "Enables the dirname command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:allow-dirname",
          "markdownDescription": "Enables the dirname command without any pre-configured scope."
        },
        {
          "description": "Enables the extname command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:allow-extname",
          "markdownDescription": "Enables the extname command without any pre-configured scope."
        },
        {
          "description": "Enables the is_absolute command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:allow-is-absolute",
          "markdownDescription": "Enables the is_absolute command without any pre-configured scope."
        },
        {
          "description": "Enables the join command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:allow-join",
          "markdownDescription": "Enables the join command without any pre-configured scope."
        },
        {
          "description": "Enables the normalize command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:allow-normalize",
          "markdownDescription": "Enables the normalize command without any pre-configured scope."
        },
        {
          "description": "Enables the resolve command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:allow-resolve",
          "markdownDescription": "Enables the resolve command without any pre-configured scope."
        },
        {
          "description": "Enables the resolve_directory command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:allow-resolve-directory",
          "markdownDescription": "Enables the resolve_directory command without any pre-configured scope."
        },
        {
          "description": "Denies the basename command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:deny-basename",
          "markdownDescription": "Denies the basename command without any pre-configured scope."
        },
        {
          "description": "Denies the dirname command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:deny-dirname",
          "markdownDescription": "Denies the dirname command without any pre-configured scope."
        },
        {
          "description": "Denies the extname command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:deny-extname",
          "markdownDescription": "Denies the extname command without any pre-configured scope."
        },
        {
          "description": "Denies the is_absolute command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:deny-is-absolute",
          "markdownDescription": "Denies the is_absolute command without any pre-configured scope."
        },
        {
          "description": "Denies the join command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:deny-join",
          "markdownDescription": "Denies the join command without any pre-configured scope."
        },
        {
          "description": "Denies the normalize command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:deny-normalize",
          "markdownDescription": "Denies the normalize command without any pre-configured scope."
        },
        {
          "description": "Denies the resolve command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:deny-resolve",
          "markdownDescription": "Denies the resolve command without any pre-configured scope."
        },
        {
          "description": "Denies the resolve_directory command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:deny-resolve-directory",
          "markdownDescription": "Denies the resolve_directory command without any pre-configured scope."
        },
        {
          "description": "Default permissions for the plugin, which enables all commands.\n#### This default permission set includes:\n\n- `allow-close`",
          "type": "string",
          "const": "core:resources:default",
          "markdownDescription": "Default permissions for the plugin, which enables all commands.\n#### This default permission set includes:\n\n- `allow-close`"
        },
        {
          "description": "Enables the close command without any pre-configured scope.",
          "type": "string",
          "const": "core:resources:allow-close",
          "markdownDescription": "Enables the close command without any pre-configured scope."
        },
        {
          "description": "Denies the close command without any pre-configured scope.",
          "type": "string",
          "const": "core:resources:deny-close",
          "markdownDescription": "Denies the close command without any pre-configured scope."
        },
        {
          "description": "Default permissions for the plugin, which enables all commands.\n#### This default permission set includes:\n\n- `allow-new`\n- `allow-get-by-id`\n- `allow-remove-by-id`\n- `allow-set-icon`\n- `allow-set-menu`\n- `allow-set-tooltip`\n- `allow-set-title`\n- `allow-set-visible`\n- `allow-set-temp-dir-path`\n- `allow-set-icon-as-template`\n- `allow-set-icon-with-as-template`\n- `allow-set-show-menu-on-left-click`",
          "type": "string",
          "const": "core:tray:default",
          "markdownDescription": "Default permissions for the plugin, which enables all commands.\n#### This default permission set includes:\n\n- `allow-new`\n- `allow-get-by-id`\n- `allow-remove-by-id`\n- `allow-set-icon`\n- `allow-set-menu`\n- `allow-set-tooltip`\n- `allow-set-title`\n- `allow-set-visible`\n- `allow-set-temp-dir-path`\n- `allow-set-icon-as-template`\n- `allow-set-icon-with-as-template`\n- `allow-set-show-menu-on-left-click`"
        },
        {
          "description": "Enables the get_by_id command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:allow-get-by-id",
          "markdownDescription": "Enables the get_by_id command without any pre-configured scope."
        },
        {
          "description": "Enables the new command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:allow-new",
          "markdownDescription": "Enables the new command without any pre-configured scope."
        },
        {
          "description": "Enables the remove_by_id command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:allow-remove-by-id",
          "markdownDescription": "Enables the remove_by_id command without any pre-configured scope."
        },
        {
          "description": "Enables the set_icon command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:allow-set-icon",
          "markdownDescription": "Enables the set_icon command without any pre-configured scope."
        },
        {
          "description": "Enables the set_icon_as_template command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:allow-set-icon-as-template",
          "markdownDescription": "Enables the set_icon_as_template command without any pre-configured scope."
        },
        {
          "description": "Enables the set_icon_with_as_template command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:allow-set-icon-with-as-template",
          "markdownDescription": "Enables the set_icon_with_as_template command without any pre-configured scope."
        },
        {
          "description": "Enables the set_menu command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:allow-set-menu",
          "markdownDescription": "Enables the set_menu command without any pre-configured scope."
        },
        {
          "description": "Enables the set_show_menu_on_left_click command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:allow-set-show-menu-on-left-click",
          "markdownDescription": "Enables the set_show_menu_on_left_click command without any pre-configured scope."
        },
        {
          "description": "Enables the set_temp_dir_path command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:allow-set-temp-dir-path",
          "markdownDescription": "Enables the set_temp_dir_path command without any pre-configured scope."
        },
        {
          "description": "Enables the set_title command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:allow-set-title",
          "markdownDescription": "Enables the set_title command without any pre-configured scope."
        },
        {
          "description": "Enables the set_tooltip command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:allow-set-tooltip",
          "markdownDescription": "Enables the set_tooltip command without any pre-configured scope."
        },
        {
          "description": "Enables the set_visible command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:allow-set-visible",
          "markdownDescription": "Enables the set_visible command without any pre-configured scope."
        },
        {
          "description": "Denies the get_by_id command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:deny-get-by-id",
          "markdownDescription": "Denies the get_by_id command without any pre-configured scope."
        },
        {
          "description": "Denies the new command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:deny-new",
          "markdownDescription": "Denies the new command without any pre-configured scope."
        },
        {
          "description": "Denies the remove_by_id command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:deny-remove-by-id",
          "markdownDescription": "Denies the remove_by_id command without any pre-configured scope."
        },
        {
          "description": "Denies the set_icon command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:deny-set-icon",
          "markdownDescription": "Denies the set_icon command without any pre-configured scope."
        },
        {
          "description": "Denies the set_icon_as_template command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:deny-set-icon-as-template",
          "markdownDescription": "Denies the set_icon_as_template command without any pre-configured scope."
        },
        {
          "description": "Denies the set_icon_with_as_template command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:deny-set-icon-with-as-template",
          "markdownDescription": "Denies the set_icon_with_as_template command without any pre-configured scope."
        },
        {
          "description": "Denies the set_menu command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:deny-set-menu",
          "markdownDescription": "Denies the set_menu command without any pre-configured scope."
        },
        {
          "description": "Denies the set_show_menu_on_left_click command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:deny-set-show-menu-on-left-click",
          "markdownDescription": "Denies the set_show_menu_on_left_click command without any pre-configured scope."
        },
        {
          "description": "Denies the set_temp_dir_path command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:deny-set-temp-dir-path",
          "markdownDescription": "Denies the set_temp_dir_path command without any pre-configured scope."
        },
        {
          "description": "Denies the set_title command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:deny-set-title",
          "markdownDescription": "Denies the set_title command without any pre-configured scope."
        },
        {
          "description": "Denies the set_tooltip command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:deny-set-tooltip",
          "markdownDescription": "Denies the set_tooltip command without any pre-configured scope."
        },
        {
          "description": "Denies the set_visible command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:deny-set-visible",
          "markdownDescription": "Denies the set_visible command without any pre-configured scope."
        },
        {
          "description": "Default permissions for the plugin.\n#### This default permission set includes:\n\n- `allow-get-all-webviews`\n- `allow-webview-position`\n- `allow-webview-size`\n- `allow-internal-toggle-devtools`",
          "type": "string",
          "const": "core:webview:default",
          "markdownDescription": "Default permissions for the plugin.\n#### This default permission set includes:\n\n- `allow-get-all-webviews`\n- `allow-webview-position`\n- `allow-webview-size`\n- `allow-internal-toggle-devtools`"
        },
        {
          "description": "Enables the clear_all_browsing_data command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-clear-all-browsing-data",
          "markdownDescription": "Enables the clear_all_browsing_data command without any pre-configured scope."
        },
        {
          "description": "Enables the create_webview command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-create-webview",
          "markdownDescription": "Enables the create_webview command without any pre-configured scope."
        },
        {
          "description": "Enables the create_webview_window command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-create-webview-window",
          "markdownDescription": "Enables the create_webview_window command without any pre-configured scope."
        },
        {
          "description": "Enables the get_all_webviews command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-get-all-webviews",
          "markdownDescription": "Enables the get_all_webviews command without any pre-configured scope."
        },
        {
          "description": "Enables the internal_toggle_devtools command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-internal-toggle-devtools",
          "markdownDescription": "Enables the internal_toggle_devtools command without any pre-configured scope."
        },
        {
          "description": "Enables the print command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-print",
          "markdownDescription": "Enables the print command without any pre-configured scope."
        },
        {
          "description": "Enables the reparent command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-reparent",
          "markdownDescription": "Enables the reparent command without any pre-configured scope."
        },
        {
          "description": "Enables the set_webview_auto_resize command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-set-webview-auto-resize",
          "markdownDescription": "Enables the set_webview_auto_resize command without any pre-configured scope."
        },
        {
          "description": "Enables the set_webview_background_color command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-set-webview-background-color",
          "markdownDescription": "Enables the set_webview_background_color command without any pre-configured scope."
        },
        {
          "description": "Enables the set_webview_focus command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-set-webview-focus",
          "markdownDescription": "Enables the set_webview_focus command without any pre-configured scope."
        },
        {
          "description": "Enables the set_webview_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-set-webview-position",
          "markdownDescription": "Enables the set_webview_position command without any pre-configured scope."
        },
        {
          "description": "Enables the set_webview_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-set-webview-size",
          "markdownDescription": "Enables the set_webview_size command without any pre-configured scope."
        },
        {
          "description": "Enables the set_webview_zoom command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-set-webview-zoom",
          "markdownDescription": "Enables the set_webview_zoom command without any pre-configured scope."
        },
        {
          "description": "Enables the webview_close command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-webview-close",
          "markdownDescription": "Enables the webview_close command without any pre-configured scope."
        },
        {
          "description": "Enables the webview_hide command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-webview-hide",
          "markdownDescription": "Enables the webview_hide command without any pre-configured scope."
        },
        {
          "description": "Enables the webview_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-webview-position",
          "markdownDescription": "Enables the webview_position command without any pre-configured scope."
        },
        {
          "description": "Enables the webview_show command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-webview-show",
          "markdownDescription": "Enables the webview_show command without any pre-configured scope."
        },
        {
          "description": "Enables the webview_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-webview-size",
          "markdownDescription": "Enables the webview_size command without any pre-configured scope."
        },
        {
          "description": "Denies the clear_all_browsing_data command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-clear-all-browsing-data",
          "markdownDescription": "Denies the clear_all_browsing_data command without any pre-configured scope."
        },
        {
          "description": "Denies the create_webview command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-create-webview",
          "markdownDescription": "Denies the create_webview command without any pre-configured scope."
        },
        {
          "description": "Denies the create_webview_window command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-create-webview-window",
          "markdownDescription": "Denies the create_webview_window command without any pre-configured scope."
        },
        {
          "description": "Denies the get_all_webviews command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-get-all-webviews",
          "markdownDescription": "Denies the get_all_webviews command without any pre-configured scope."
        },
        {
          "description": "Denies the internal_toggle_devtools command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-internal-toggle-devtools",
          "markdownDescription": "Denies the internal_toggle_devtools command without any pre-configured scope."
        },
        {
          "description": "Denies the print command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-print",
          "markdownDescription": "Denies the print command without any pre-configured scope."
        },
        {
          "description": "Denies the reparent command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-reparent",
          "markdownDescription": "Denies the reparent command without any pre-configured scope."
        },
        {
          "description": "Denies the set_webview_auto_resize command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-set-webview-auto-resize",
          "markdownDescription": "Denies the set_webview_auto_resize command without any pre-configured scope."
        },
        {
          "description": "Denies the set_webview_background_color command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-set-webview-background-color",
          "markdownDescription": "Denies the set_webview_background_color command without any pre-configured scope."
        },
        {
          "description": "Denies the set_webview_focus command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-set-webview-focus",
          "markdownDescription": "Denies the set_webview_focus command without any pre-configured scope."
        },
        {
          "description": "Denies the set_webview_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-set-webview-position",
          "markdownDescription": "Denies the set_webview_position command without any pre-configured scope."
        },
        {
          "description": "Denies the set_webview_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-set-webview-size",
          "markdownDescription": "Denies the set_webview_size command without any pre-configured scope."
        },
        {
          "description": "Denies the set_webview_zoom command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-set-webview-zoom",
          "markdownDescription": "Denies the set_webview_zoom command without any pre-configured scope."
        },
        {
          "description": "Denies the webview_close command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-webview-close",
          "markdownDescription": "Denies the webview_close command without any pre-configured scope."
        },
        {
          "description": "Denies the webview_hide command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-webview-hide",
          "markdownDescription": "Denies the webview_hide command without any pre-configured scope."
        },
        {
          "description": "Denies the webview_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-webview-position",
          "markdownDescription": "Denies the webview_position command without any pre-configured scope."
        },
        {
          "description": "Denies the webview_show command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-webview-show",
          "markdownDescription": "Denies the webview_show command without any pre-configured scope."
        },
        {
          "description": "Denies the webview_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-webview-size",
          "markdownDescription": "Denies the webview_size command without any pre-configured scope."
        },
        {
          "description": "Default permissions for the plugin.\n#### This default permission set includes:\n\n- `allow-get-all-windows`\n- `allow-scale-factor`\n- `allow-inner-position`\n- `allow-outer-position`\n- `allow-inner-size`\n- `allow-outer-size`\n- `allow-is-fullscreen`\n- `allow-is-minimized`\n- `allow-is-maximized`\n- `allow-is-focused`\n- `allow-is-decorated`\n- `allow-is-resizable`\n- `allow-is-maximizable`\n- `allow-is-minimizable`\n- `allow-is-closable`\n- `allow-is-visible`\n- `allow-is-enabled`\n- `allow-title`\n- `allow-current-monitor`\n- `allow-primary-monitor`\n- `allow-monitor-from-point`\n- `allow-available-monitors`\n- `allow-cursor-position`\n- `allow-theme`\n- `allow-is-always-on-top`\n- `allow-activity-name`\n- `allow-scene-identifier`\n- `allow-internal-toggle-maximize`",
          "type": "string",
          "const": "core:window:default",
          "markdownDescription": "Default permissions for the plugin.\n#### This default permission set includes:\n\n- `allow-get-all-windows`\n- `allow-scale-factor`\n- `allow-inner-position`\n- `allow-outer-position`\n- `allow-inner-size`\n- `allow-outer-size`\n- `allow-is-fullscreen`\n- `allow-is-minimized`\n- `allow-is-maximized`\n- `allow-is-focused`\n- `allow-is-decorated`\n- `allow-is-resizable`\n- `allow-is-maximizable`\n- `allow-is-minimizable`\n- `allow-is-closable`\n- `allow-is-visible`\n- `allow-is-enabled`\n- `allow-title`\n- `allow-current-monitor`\n- `allow-primary-monitor`\n- `allow-monitor-from-point`\n- `allow-available-monitors`\n- `allow-cursor-position`\n- `allow-theme`\n- `allow-is-always-on-top`\n- `allow-activity-name`\n- `allow-scene-identifier`\n- `allow-internal-toggle-maximize`"
        },
        {
          "description": "Enables the activity_name command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-activity-name",
          "markdownDescription": "Enables the activity_name command without any pre-configured scope."
        },
        {
          "description": "Enables the available_monitors command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-available-monitors",
          "markdownDescription": "Enables the available_monitors command without any pre-configured scope."
        },
        {
          "description": "Enables the center command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-center",
          "markdownDescription": "Enables the center command without any pre-configured scope."
        },
        {
          "description": "Enables the close command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-close",
          "markdownDescription": "Enables the close command without any pre-configured scope."
        },
        {
          "description": "Enables the create command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-create",
          "markdownDescription": "Enables the create command without any pre-configured scope."
        },
        {
          "description": "Enables the current_monitor command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-current-monitor",
          "markdownDescription": "Enables the current_monitor command without any pre-configured scope."
        },
        {
          "description": "Enables the cursor_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-cursor-position",
          "markdownDescription": "Enables the cursor_position command without any pre-configured scope."
        },
        {
          "description": "Enables the destroy command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-destroy",
          "markdownDescription": "Enables the destroy command without any pre-configured scope."
        },
        {
          "description": "Enables the get_all_windows command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-get-all-windows",
          "markdownDescription": "Enables the get_all_windows command without any pre-configured scope."
        },
        {
          "description": "Enables the hide command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-hide",
          "markdownDescription": "Enables the hide command without any pre-configured scope."
        },
        {
          "description": "Enables the inner_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-inner-position",
          "markdownDescription": "Enables the inner_position command without any pre-configured scope."
        },
        {
          "description": "Enables the inner_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-inner-size",
          "markdownDescription": "Enables the inner_size command without any pre-configured scope."
        },
        {
          "description": "Enables the internal_toggle_maximize command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-internal-toggle-maximize",
          "markdownDescription": "Enables the internal_toggle_maximize command without any pre-configured scope."
        },
        {
          "description": "Enables the is_always_on_top command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-is-always-on-top",
          "markdownDescription": "Enables the is_always_on_top command without any pre-configured scope."
        },
        {
          "description": "Enables the is_closable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-is-closable",
          "markdownDescription": "Enables the is_closable command without any pre-configured scope."
        },
        {
          "description": "Enables the is_decorated command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-is-decorated",
          "markdownDescription": "Enables the is_decorated command without any pre-configured scope."
        },
        {
          "description": "Enables the is_enabled command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-is-enabled",
          "markdownDescription": "Enables the is_enabled command without any pre-configured scope."
        },
        {
          "description": "Enables the is_focused command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-is-focused",
          "markdownDescription": "Enables the is_focused command without any pre-configured scope."
        },
        {
          "description": "Enables the is_fullscreen command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-is-fullscreen",
          "markdownDescription": "Enables the is_fullscreen command without any pre-configured scope."
        },
        {
          "description": "Enables the is_maximizable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-is-maximizable",
          "markdownDescription": "Enables the is_maximizable command without any pre-configured scope."
        },
        {
          "description": "Enables the is_maximized command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-is-maximized",
          "markdownDescription": "Enables the is_maximized command without any pre-configured scope."
        },
        {
          "description": "Enables the is_minimizable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-is-minimizable",
          "markdownDescription": "Enables the is_minimizable command without any pre-configured scope."
        },
        {
          "description": "Enables the is_minimized command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-is-minimized",
          "markdownDescription": "Enables the is_minimized command without any pre-configured scope."
        },
        {
          "description": "Enables the is_resizable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-is-resizable",
          "markdownDescription": "Enables the is_resizable command without any pre-configured scope."
        },
        {
          "description": "Enables the is_visible command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-is-visible",
          "markdownDescription": "Enables the is_visible command without any pre-configured scope."
        },
        {
          "description": "Enables the maximize command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-maximize",
          "markdownDescription": "Enables the maximize command without any pre-configured scope."
        },
        {
          "description": "Enables the minimize command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-minimize",
          "markdownDescription": "Enables the minimize command without any pre-configured scope."
        },
        {
          "description": "Enables the monitor_from_point command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-monitor-from-point",
          "markdownDescription": "Enables the monitor_from_point command without any pre-configured scope."
        },
        {
          "description": "Enables the outer_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-outer-position",
          "markdownDescription": "Enables the outer_position command without any pre-configured scope."
        },
        {
          "description": "Enables the outer_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-outer-size",
          "markdownDescription": "Enables the outer_size command without any pre-configured scope."
        },
        {
          "description": "Enables the primary_monitor command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-primary-monitor",
          "markdownDescription": "Enables the primary_monitor command without any pre-configured scope."
        },
        {
          "description": "Enables the request_user_attention command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-request-user-attention",
          "markdownDescription": "Enables the request_user_attention command without any pre-configured scope."
        },
        {
          "description": "Enables the scale_factor command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-scale-factor",
          "markdownDescription": "Enables the scale_factor command without any pre-configured scope."
        },
        {
          "description": "Enables the scene_identifier command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-scene-identifier",
          "markdownDescription": "Enables the scene_identifier command without any pre-configured scope."
        },
        {
          "description": "Enables the set_always_on_bottom command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-always-on-bottom",
          "markdownDescription": "Enables the set_always_on_bottom command without any pre-configured scope."
        },
        {
          "description": "Enables the set_always_on_top command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-always-on-top",
          "markdownDescription": "Enables the set_always_on_top command without any pre-configured scope."
        },
        {
          "description": "Enables the set_background_color command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-background-color",
          "markdownDescription": "Enables the set_background_color command without any pre-configured scope."
        },
        {
          "description": "Enables the set_badge_count command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-badge-count",
          "markdownDescription": "Enables the set_badge_count command without any pre-configured scope."
        },
        {
          "description": "Enables the set_badge_label command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-badge-label",
          "markdownDescription": "Enables the set_badge_label command without any pre-configured scope."
        },
        {
          "description": "Enables the set_closable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-closable",
          "markdownDescription": "Enables the set_closable command without any pre-configured scope."
        },
        {
          "description": "Enables the set_content_protected command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-content-protected",
          "markdownDescription": "Enables the set_content_protected command without any pre-configured scope."
        },
        {
          "description": "Enables the set_cursor_grab command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-cursor-grab",
          "markdownDescription": "Enables the set_cursor_grab command without any pre-configured scope."
        },
        {
          "description": "Enables the set_cursor_icon command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-cursor-icon",
          "markdownDescription": "Enables the set_cursor_icon command without any pre-configured scope."
        },
        {
          "description": "Enables the set_cursor_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-cursor-position",
          "markdownDescription": "Enables the set_cursor_position command without any pre-configured scope."
        },
        {
          "description": "Enables the set_cursor_visible command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-cursor-visible",
          "markdownDescription": "Enables the set_cursor_visible command without any pre-configured scope."
        },
        {
          "description": "Enables the set_decorations command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-decorations",
          "markdownDescription": "Enables the set_decorations command without any pre-configured scope."
        },
        {
          "description": "Enables the set_effects command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-effects",
          "markdownDescription": "Enables the set_effects command without any pre-configured scope."
        },
        {
          "description": "Enables the set_enabled command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-enabled",
          "markdownDescription": "Enables the set_enabled command without any pre-configured scope."
        },
        {
          "description": "Enables the set_focus command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-focus",
          "markdownDescription": "Enables the set_focus command without any pre-configured scope."
        },
        {
          "description": "Enables the set_focusable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-focusable",
          "markdownDescription": "Enables the set_focusable command without any pre-configured scope."
        },
        {
          "description": "Enables the set_fullscreen command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-fullscreen",
          "markdownDescription": "Enables the set_fullscreen command without any pre-configured scope."
        },
        {
          "description": "Enables the set_fullscreen_on_monitor command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-fullscreen-on-monitor",
          "markdownDescription": "Enables the set_fullscreen_on_monitor command without any pre-configured scope."
        },
        {
          "description": "Enables the set_icon command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-icon",
          "markdownDescription": "Enables the set_icon command without any pre-configured scope."
        },
        {
          "description": "Enables the set_ignore_cursor_events command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-ignore-cursor-events",
          "markdownDescription": "Enables the set_ignore_cursor_events command without any pre-configured scope."
        },
        {
          "description": "Enables the set_max_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-max-size",
          "markdownDescription": "Enables the set_max_size command without any pre-configured scope."
        },
        {
          "description": "Enables the set_maximizable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-maximizable",
          "markdownDescription": "Enables the set_maximizable command without any pre-configured scope."
        },
        {
          "description": "Enables the set_min_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-min-size",
          "markdownDescription": "Enables the set_min_size command without any pre-configured scope."
        },
        {
          "description": "Enables the set_minimizable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-minimizable",
          "markdownDescription": "Enables the set_minimizable command without any pre-configured scope."
        },
        {
          "description": "Enables the set_overlay_icon command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-overlay-icon",
          "markdownDescription": "Enables the set_overlay_icon command without any pre-configured scope."
        },
        {
          "description": "Enables the set_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-position",
          "markdownDescription": "Enables the set_position command without any pre-configured scope."
        },
        {
          "description": "Enables the set_progress_bar command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-progress-bar",
          "markdownDescription": "Enables the set_progress_bar command without any pre-configured scope."
        },
        {
          "description": "Enables the set_resizable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-resizable",
          "markdownDescription": "Enables the set_resizable command without any pre-configured scope."
        },
        {
          "description": "Enables the set_shadow command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-shadow",
          "markdownDescription": "Enables the set_shadow command without any pre-configured scope."
        },
        {
          "description": "Enables the set_simple_fullscreen command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-simple-fullscreen",
          "markdownDescription": "Enables the set_simple_fullscreen command without any pre-configured scope."
        },
        {
          "description": "Enables the set_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-size",
          "markdownDescription": "Enables the set_size command without any pre-configured scope."
        },
        {
          "description": "Enables the set_size_constraints command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-size-constraints",
          "markdownDescription": "Enables the set_size_constraints command without any pre-configured scope."
        },
        {
          "description": "Enables the set_skip_taskbar command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-skip-taskbar",
          "markdownDescription": "Enables the set_skip_taskbar command without any pre-configured scope."
        },
        {
          "description": "Enables the set_theme command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-theme",
          "markdownDescription": "Enables the set_theme command without any pre-configured scope."
        },
        {
          "description": "Enables the set_title command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-title",
          "markdownDescription": "Enables the set_title command without any pre-configured scope."
        },
        {
          "description": "Enables the set_title_bar_style command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-title-bar-style",
          "markdownDescription": "Enables the set_title_bar_style command without any pre-configured scope."
        },
        {
          "description": "Enables the set_visible_on_all_workspaces command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-visible-on-all-workspaces",
          "markdownDescription": "Enables the set_visible_on_all_workspaces command without any pre-configured scope."
        },
        {
          "description": "Enables the show command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-show",
          "markdownDescription": "Enables the show command without any pre-configured scope."
        },
        {
          "description": "Enables the start_dragging command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-start-dragging",
          "markdownDescription": "Enables the start_dragging command without any pre-configured scope."
        },
        {
          "description": "Enables the start_resize_dragging command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-start-resize-dragging",
          "markdownDescription": "Enables the start_resize_dragging command without any pre-configured scope."
        },
        {
          "description": "Enables the theme command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-theme",
          "markdownDescription": "Enables the theme command without any pre-configured scope."
        },
        {
          "description": "Enables the title command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-title",
          "markdownDescription": "Enables the title command without any pre-configured scope."
        },
        {
          "description": "Enables the toggle_maximize command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-toggle-maximize",
          "markdownDescription": "Enables the toggle_maximize command without any pre-configured scope."
        },
        {
          "description": "Enables the unmaximize command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-unmaximize",
          "markdownDescription": "Enables the unmaximize command without any pre-configured scope."
        },
        {
          "description": "Enables the unminimize command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-unminimize",
          "markdownDescription": "Enables the unminimize command without any pre-configured scope."
        },
        {
          "description": "Denies the activity_name command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-activity-name",
          "markdownDescription": "Denies the activity_name command without any pre-configured scope."
        },
        {
          "description": "Denies the available_monitors command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-available-monitors",
          "markdownDescription": "Denies the available_monitors command without any pre-configured scope."
        },
        {
          "description": "Denies the center command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-center",
          "markdownDescription": "Denies the center command without any pre-configured scope."
        },
        {
          "description": "Denies the close command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-close",
          "markdownDescription": "Denies the close command without any pre-configured scope."
        },
        {
          "description": "Denies the create command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-create",
          "markdownDescription": "Denies the create command without any pre-configured scope."
        },
        {
          "description": "Denies the current_monitor command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-current-monitor",
          "markdownDescription": "Denies the current_monitor command without any pre-configured scope."
        },
        {
          "description": "Denies the cursor_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-cursor-position",
          "markdownDescription": "Denies the cursor_position command without any pre-configured scope."
        },
        {
          "description": "Denies the destroy command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-destroy",
          "markdownDescription": "Denies the destroy command without any pre-configured scope."
        },
        {
          "description": "Denies the get_all_windows command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-get-all-windows",
          "markdownDescription": "Denies the get_all_windows command without any pre-configured scope."
        },
        {
          "description": "Denies the hide command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-hide",
          "markdownDescription": "Denies the hide command without any pre-configured scope."
        },
        {
          "description": "Denies the inner_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-inner-position",
          "markdownDescription": "Denies the inner_position command without any pre-configured scope."
        },
        {
          "description": "Denies the inner_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-inner-size",
          "markdownDescription": "Denies the inner_size command without any pre-configured scope."
        },
        {
          "description": "Denies the internal_toggle_maximize command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-internal-toggle-maximize",
          "markdownDescription": "Denies the internal_toggle_maximize command without any pre-configured scope."
        },
        {
          "description": "Denies the is_always_on_top command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-is-always-on-top",
          "markdownDescription": "Denies the is_always_on_top command without any pre-configured scope."
        },
        {
          "description": "Denies the is_closable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-is-closable",
          "markdownDescription": "Denies the is_closable command without any pre-configured scope."
        },
        {
          "description": "Denies the is_decorated command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-is-decorated",
          "markdownDescription": "Denies the is_decorated command without any pre-configured scope."
        },
        {
          "description": "Denies the is_enabled command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-is-enabled",
          "markdownDescription": "Denies the is_enabled command without any pre-configured scope."
        },
        {
          "description": "Denies the is_focused command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-is-focused",
          "markdownDescription": "Denies the is_focused command without any pre-configured scope."
        },
        {
          "description": "Denies the is_fullscreen command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-is-fullscreen",
          "markdownDescription": "Denies the is_fullscreen command without any pre-configured scope."
        },
        {
          "description": "Denies the is_maximizable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-is-maximizable",
          "markdownDescription": "Denies the is_maximizable command without any pre-configured scope."
        },
        {
          "description": "Denies the is_maximized command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-is-maximized",
          "markdownDescription": "Denies the is_maximized command without any pre-configured scope."
        },
        {
          "description": "Denies the is_minimizable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-is-minimizable",
          "markdownDescription": "Denies the is_minimizable command without any pre-configured scope."
        },
        {
          "description": "Denies the is_minimized command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-is-minimized",
          "markdownDescription": "Denies the is_minimized command without any pre-configured scope."
        },
        {
          "description": "Denies the is_resizable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-is-resizable",
          "markdownDescription": "Denies the is_resizable command without any pre-configured scope."
        },
        {
          "description": "Denies the is_visible command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-is-visible",
          "markdownDescription": "Denies the is_visible command without any pre-configured scope."
        },
        {
          "description": "Denies the maximize command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-maximize",
          "markdownDescription": "Denies the maximize command without any pre-configured scope."
        },
        {
          "description": "Denies the minimize command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-minimize",
          "markdownDescription": "Denies the minimize command without any pre-configured scope."
        },
        {
          "description": "Denies the monitor_from_point command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-monitor-from-point",
          "markdownDescription": "Denies the monitor_from_point command without any pre-configured scope."
        },
        {
          "description": "Denies the outer_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-outer-position",
          "markdownDescription": "Denies the outer_position command without any pre-configured scope."
        },
        {
          "description": "Denies the outer_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-outer-size",
          "markdownDescription": "Denies the outer_size command without any pre-configured scope."
        },
        {
          "description": "Denies the primary_monitor command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-primary-monitor",
          "markdownDescription": "Denies the primary_monitor command without any pre-configured scope."
        },
        {
          "description": "Denies the request_user_attention command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-request-user-attention",
          "markdownDescription": "Denies the request_user_attention command without any pre-configured scope."
        },
        {
          "description": "Denies the scale_factor command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-scale-factor",
          "markdownDescription": "Denies the scale_factor command without any pre-configured scope."
        },
        {
          "description": "Denies the scene_identifier command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-scene-identifier",
          "markdownDescription": "Denies the scene_identifier command without any pre-configured scope."
        },
        {
          "description": "Denies the set_always_on_bottom command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-always-on-bottom",
          "markdownDescription": "Denies the set_always_on_bottom command without any pre-configured scope."
        },
        {
          "description": "Denies the set_always_on_top command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-always-on-top",
          "markdownDescription": "Denies the set_always_on_top command without any pre-configured scope."
        },
        {
          "description": "Denies the set_background_color command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-background-color",
          "markdownDescription": "Denies the set_background_color command without any pre-configured scope."
        },
        {
          "description": "Denies the set_badge_count command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-badge-count",
          "markdownDescription": "Denies the set_badge_count command without any pre-configured scope."
        },
        {
          "description": "Denies the set_badge_label command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-badge-label",
          "markdownDescription": "Denies the set_badge_label command without any pre-configured scope."
        },
        {
          "description": "Denies the set_closable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-closable",
          "markdownDescription": "Denies the set_closable command without any pre-configured scope."
        },
        {
          "description": "Denies the set_content_protected command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-content-protected",
          "markdownDescription": "Denies the set_content_protected command without any pre-configured scope."
        },
        {
          "description": "Denies the set_cursor_grab command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-cursor-grab",
          "markdownDescription": "Denies the set_cursor_grab command without any pre-configured scope."
        },
        {
          "description": "Denies the set_cursor_icon command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-cursor-icon",
          "markdownDescription": "Denies the set_cursor_icon command without any pre-configured scope."
        },
        {
          "description": "Denies the set_cursor_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-cursor-position",
          "markdownDescription": "Denies the set_cursor_position command without any pre-configured scope."
        },
        {
          "description": "Denies the set_cursor_visible command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-cursor-visible",
          "markdownDescription": "Denies the set_cursor_visible command without any pre-configured scope."
        },
        {
          "description": "Denies the set_decorations command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-decorations",
          "markdownDescription": "Denies the set_decorations command without any pre-configured scope."
        },
        {
          "description": "Denies the set_effects command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-effects",
          "markdownDescription": "Denies the set_effects command without any pre-configured scope."
        },
        {
          "description": "Denies the set_enabled command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-enabled",
          "markdownDescription": "Denies the set_enabled command without any pre-configured scope."
        },
        {
          "description": "Denies the set_focus command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-focus",
          "markdownDescription": "Denies the set_focus command without any pre-configured scope."
        },
        {
          "description": "Denies the set_focusable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-focusable",
          "markdownDescription": "Denies the set_focusable command without any pre-configured scope."
        },
        {
          "description": "Denies the set_fullscreen command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-fullscreen",
          "markdownDescription": "Denies the set_fullscreen command without any pre-configured scope."
        },
        {
          "description": "Denies the set_fullscreen_on_monitor command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-fullscreen-on-monitor",
          "markdownDescription": "Denies the set_fullscreen_on_monitor command without any pre-configured scope."
        },
        {
          "description": "Denies the set_icon command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-icon",
          "markdownDescription": "Denies the set_icon command without any pre-configured scope."
        },
        {
          "description": "Denies the set_ignore_cursor_events command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-ignore-cursor-events",
          "markdownDescription": "Denies the set_ignore_cursor_events command without any pre-configured scope."
        },
        {
          "description": "Denies the set_max_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-max-size",
          "markdownDescription": "Denies the set_max_size command without any pre-configured scope."
        },
        {
          "description": "Denies the set_maximizable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-maximizable",
          "markdownDescription": "Denies the set_maximizable command without any pre-configured scope."
        },
        {
          "description": "Denies the set_min_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-min-size",
          "markdownDescription": "Denies the set_min_size command without any pre-configured scope."
        },
        {
          "description": "Denies the set_minimizable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-minimizable",
          "markdownDescription": "Denies the set_minimizable command without any pre-configured scope."
        },
        {
          "description": "Denies the set_overlay_icon command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-overlay-icon",
          "markdownDescription": "Denies the set_overlay_icon command without any pre-configured scope."
        },
        {
          "description": "Denies the set_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-position",
          "markdownDescription": "Denies the set_position command without any pre-configured scope."
        },
        {
          "description": "Denies the set_progress_bar command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-progress-bar",
          "markdownDescription": "Denies the set_progress_bar command without any pre-configured scope."
        },
        {
          "description": "Denies the set_resizable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-resizable",
          "markdownDescription": "Denies the set_resizable command without any pre-configured scope."
        },
        {
          "description": "Denies the set_shadow command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-shadow",
          "markdownDescription": "Denies the set_shadow command without any pre-configured scope."
        },
        {
          "description": "Denies the set_simple_fullscreen command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-simple-fullscreen",
          "markdownDescription": "Denies the set_simple_fullscreen command without any pre-configured scope."
        },
        {
          "description": "Denies the set_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-size",
          "markdownDescription": "Denies the set_size command without any pre-configured scope."
        },
        {
          "description": "Denies the set_size_constraints command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-size-constraints",
          "markdownDescription": "Denies the set_size_constraints command without any pre-configured scope."
        },
        {
          "description": "Denies the set_skip_taskbar command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-skip-taskbar",
          "markdownDescription": "Denies the set_skip_taskbar command without any pre-configured scope."
        },
        {
          "description": "Denies the set_theme command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-theme",
          "markdownDescription": "Denies the set_theme command without any pre-configured scope."
        },
        {
          "description": "Denies the set_title command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-title",
          "markdownDescription": "Denies the set_title command without any pre-configured scope."
        },
        {
          "description": "Denies the set_title_bar_style command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-title-bar-style",
          "markdownDescription": "Denies the set_title_bar_style command without any pre-configured scope."
        },
        {
          "description": "Denies the set_visible_on_all_workspaces command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-visible-on-all-workspaces",
          "markdownDescription": "Denies the set_visible_on_all_workspaces command without any pre-configured scope."
        },
        {
          "description": "Denies the show command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-show",
          "markdownDescription": "Denies the show command without any pre-configured scope."
        },
        {
          "description": "Denies the start_dragging command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-start-dragging",
          "markdownDescription": "Denies the start_dragging command without any pre-configured scope."
        },
        {
          "description": "Denies the start_resize_dragging command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-start-resize-dragging",
          "markdownDescription": "Denies the start_resize_dragging command without any pre-configured scope."
        },
        {
          "description": "Denies the theme command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-theme",
          "markdownDescription": "Denies the theme command without any pre-configured scope."
        },
        {
          "description": "Denies the title command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-title",
          "markdownDescription": "Denies the title command without any pre-configured scope."
        },
        {
          "description": "Denies the toggle_maximize command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-toggle-maximize",
          "markdownDescription": "Denies the toggle_maximize command without any pre-configured scope."
        },
        {
          "description": "Denies the unmaximize command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-unmaximize",
          "markdownDescription": "Denies the unmaximize command without any pre-configured scope."
        },
        {
          "description": "Denies the unminimize command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-unminimize",
          "markdownDescription": "Denies the unminimize command without any pre-configured scope."
        },
        {
          "description": "This permission set configures the types of dialogs\navailable from the dialog plugin.\n\n#### Granted Permissions\n\nAll dialog types are enabled.\n\n\n\n#### This default permission set includes:\n\n- `allow-message`\n- `allow-save`\n- `allow-open`",
          "type": "string",
          "const": "dialog:default",
          "markdownDescription": "This permission set configures the types of dialogs\navailable from the dialog plugin.\n\n#### Granted Permissions\n\nAll dialog types are enabled.\n\n\n\n#### This default permission set includes:\n\n- `allow-message`\n- `allow-save`\n- `allow-open`"
        },
        {
          "description": "Enables the ask command without any pre-configured scope. (**DEPRECATED**: This is now an alias to `allow-message` and will be removed in v3)",
          "type": "string",
          "const": "dialog:allow-ask",
          "markdownDescription": "Enables the ask command without any pre-configured scope. (**DEPRECATED**: This is now an alias to `allow-message` and will be removed in v3)"
        },
        {
          "description": "Enables the confirm command without any pre-configured scope. (**DEPRECATED**: This is now an alias to `allow-message` and will be removed in v3)",
          "type": "string",
          "const": "dialog:allow-confirm",
          "markdownDescription": "Enables the confirm command without any pre-configured scope. (**DEPRECATED**: This is now an alias to `allow-message` and will be removed in v3)"
        },
        {
          "description": "Enables the message command without any pre-configured scope.",
          "type": "string",
          "const": "dialog:allow-message",
          "markdownDescription": "Enables the message command without any pre-configured scope."
        },
        {
          "description": "Enables the open command without any pre-configured scope.",
          "type": "string",
          "const": "dialog:allow-open",
          "markdownDescription": "Enables the open command without any pre-configured scope."
        },
        {
          "description": "Enables the save command without any pre-configured scope.",
          "type": "string",
          "const": "dialog:allow-save",
          "markdownDescription": "Enables the save command without any pre-configured scope."
        },
        {
          "description": "Denies the ask command without any pre-configured scope. (**DEPRECATED**: This is now an alias to `deny-message` and will be removed in v3)",
          "type": "string",
          "const": "dialog:deny-ask",
          "markdownDescription": "Denies the ask command without any pre-configured scope. (**DEPRECATED**: This is now an alias to `deny-message` and will be removed in v3)"
        },
        {
          "description": "Denies the confirm command without any pre-configured scope. (**DEPRECATED**: This is now an alias to `deny-message` and will be removed in v3)",
          "type": "string",
          "const": "dialog:deny-confirm",
          "markdownDescription": "Denies the confirm command without any pre-configured scope. (**DEPRECATED**: This is now an alias to `deny-message` and will be removed in v3)"
        },
        {
          "description": "Denies the message command without any pre-configured scope.",
          "type": "string",
          "const": "dialog:deny-message",
          "markdownDescription": "Denies the message command without any pre-configured scope."
        },
        {
          "description": "Denies the open command without any pre-configured scope.",
          "type": "string",
          "const": "dialog:deny-open",
          "markdownDescription": "Denies the open command without any pre-configured scope."
        },
        {
          "description": "Denies the save command without any pre-configured scope.",
          "type": "string",
          "const": "dialog:deny-save",
          "markdownDescription": "Denies the save command without any pre-configured scope."
        }
      ]
    },
    "Value": {
      "description": "All supported ACL values.",
      "anyOf": [
        {
          "description": "Represents a null JSON value.",
          "type": "null"
        },
        {
          "description": "Represents a [`bool`].",
          "type": "boolean"
        },
        {
          "description": "Represents a valid ACL [`Number`].",
          "allOf": [
            {
              "$ref": "#/definitions/Number"
            }
          ]
        },
        {
          "description": "Represents a [`String`].",
          "type": "string"
        },
        {
          "description": "Represents a list of other [`Value`]s.",
          "type": "array",
          "items": {
            "$ref": "#/definitions/Value"
          }
        },
        {
          "description": "Represents a map of [`String`] keys to [`Value`]s.",
          "type": "object",
          "additionalProperties": {
            "$ref": "#/definitions/Value"
          }
        }
      ]
    },
    "Number": {
      "description": "A valid ACL number.",
      "anyOf": [
        {
          "description": "Represents an [`i64`].",
          "type": "integer",
          "format": "int64"
        },
        {
          "description": "Represents a [`f64`].",
          "type": "number",
          "format": "double"
        }
      ]
    },
    "Target": {
      "description": "Platform target.",
      "oneOf": [
        {
          "description": "MacOS.",
          "type": "string",
          "enum": [
            "macOS"
          ]
        },
        {
          "description": "Windows.",
          "type": "string",
          "enum": [
            "windows"
          ]
        },
        {
          "description": "Linux.",
          "type": "string",
          "enum": [
            "linux"
          ]
        },
        {
          "description": "Android.",
          "type": "string",
          "enum": [
            "android"
          ]
        },
        {
          "description": "iOS.",
          "type": "string",
          "enum": [
            "iOS"
          ]
        }
      ]
    }
  }
}
````

## File: app/src-tauri/gen/schemas/windows-schema.json
````json
{
  "$schema": "http://json-schema.org/draft-07/schema#",
  "title": "CapabilityFile",
  "description": "Capability formats accepted in a capability file.",
  "anyOf": [
    {
      "description": "A single capability.",
      "allOf": [
        {
          "$ref": "#/definitions/Capability"
        }
      ]
    },
    {
      "description": "A list of capabilities.",
      "type": "array",
      "items": {
        "$ref": "#/definitions/Capability"
      }
    },
    {
      "description": "A list of capabilities.",
      "type": "object",
      "required": [
        "capabilities"
      ],
      "properties": {
        "capabilities": {
          "description": "The list of capabilities.",
          "type": "array",
          "items": {
            "$ref": "#/definitions/Capability"
          }
        }
      }
    }
  ],
  "definitions": {
    "Capability": {
      "description": "A grouping and boundary mechanism developers can use to isolate access to the IPC layer.\n\nIt controls application windows' and webviews' fine grained access to the Tauri core, application, or plugin commands. If a webview or its window is not matching any capability then it has no access to the IPC layer at all.\n\nThis can be done to create groups of windows, based on their required system access, which can reduce impact of frontend vulnerabilities in less privileged windows. Windows can be added to a capability by exact name (e.g. `main-window`) or glob patterns like `*` or `admin-*`. A Window can have none, one, or multiple associated capabilities.\n\n## Example\n\n```json { \"identifier\": \"main-user-files-write\", \"description\": \"This capability allows the `main` window on macOS and Windows access to `filesystem` write related commands and `dialog` commands to enable programmatic access to files selected by the user.\", \"windows\": [ \"main\" ], \"permissions\": [ \"core:default\", \"dialog:open\", { \"identifier\": \"fs:allow-write-text-file\", \"allow\": [{ \"path\": \"$HOME/test.txt\" }] }, ], \"platforms\": [\"macOS\",\"windows\"] } ```",
      "type": "object",
      "required": [
        "identifier",
        "permissions"
      ],
      "properties": {
        "identifier": {
          "description": "Identifier of the capability.\n\n## Example\n\n`main-user-files-write`",
          "type": "string"
        },
        "description": {
          "description": "Description of what the capability is intended to allow on associated windows.\n\nIt should contain a description of what the grouped permissions should allow.\n\n## Example\n\nThis capability allows the `main` window access to `filesystem` write related commands and `dialog` commands to enable programmatic access to files selected by the user.",
          "default": "",
          "type": "string"
        },
        "remote": {
          "description": "Configure remote URLs that can use the capability permissions.\n\nThis setting is optional and defaults to not being set, as our default use case is that the content is served from our local application.\n\n:::caution Make sure you understand the security implications of providing remote sources with local system access. :::\n\n## Example\n\n```json { \"urls\": [\"https://*.mydomain.dev\"] } ```",
          "anyOf": [
            {
              "$ref": "#/definitions/CapabilityRemote"
            },
            {
              "type": "null"
            }
          ]
        },
        "local": {
          "description": "Whether this capability is enabled for local app URLs or not. Defaults to `true`.",
          "default": true,
          "type": "boolean"
        },
        "windows": {
          "description": "List of windows that are affected by this capability. Can be a glob pattern.\n\nIf a window label matches any of the patterns in this list, the capability will be enabled on all the webviews of that window, regardless of the value of [`Self::webviews`].\n\nOn multiwebview windows, prefer specifying [`Self::webviews`] and omitting [`Self::windows`] for a fine grained access control.\n\n## Example\n\n`[\"main\"]`",
          "type": "array",
          "items": {
            "type": "string"
          }
        },
        "webviews": {
          "description": "List of webviews that are affected by this capability. Can be a glob pattern.\n\nThe capability will be enabled on all the webviews whose label matches any of the patterns in this list, regardless of whether the webview's window label matches a pattern in [`Self::windows`].\n\n## Example\n\n`[\"sub-webview-one\", \"sub-webview-two\"]`",
          "type": "array",
          "items": {
            "type": "string"
          }
        },
        "permissions": {
          "description": "List of permissions attached to this capability.\n\nMust include the plugin name as prefix in the form of `${plugin-name}:${permission-name}`. For commands directly implemented in the application itself only `${permission-name}` is required.\n\n## Example\n\n```json [ \"core:default\", \"shell:allow-open\", \"dialog:open\", { \"identifier\": \"fs:allow-write-text-file\", \"allow\": [{ \"path\": \"$HOME/test.txt\" }] } ] ```",
          "type": "array",
          "items": {
            "$ref": "#/definitions/PermissionEntry"
          },
          "uniqueItems": true
        },
        "platforms": {
          "description": "Limit which target platforms this capability applies to.\n\nBy default all platforms are targeted.\n\n## Example\n\n`[\"macOS\",\"windows\"]`",
          "type": [
            "array",
            "null"
          ],
          "items": {
            "$ref": "#/definitions/Target"
          }
        }
      }
    },
    "CapabilityRemote": {
      "description": "Configuration for remote URLs that are associated with the capability.",
      "type": "object",
      "required": [
        "urls"
      ],
      "properties": {
        "urls": {
          "description": "Remote domains this capability refers to using the [URLPattern standard](https://urlpattern.spec.whatwg.org/).\n\n## Examples\n\n- \"https://*.mydomain.dev\": allows subdomains of mydomain.dev - \"https://mydomain.dev/api/*\": allows any subpath of mydomain.dev/api",
          "type": "array",
          "items": {
            "type": "string"
          }
        }
      }
    },
    "PermissionEntry": {
      "description": "An entry for a permission value in a [`Capability`] can be either a raw permission [`Identifier`] or an object that references a permission and extends its scope.",
      "anyOf": [
        {
          "description": "Reference a permission or permission set by identifier.",
          "allOf": [
            {
              "$ref": "#/definitions/Identifier"
            }
          ]
        },
        {
          "description": "Reference a permission or permission set by identifier and extends its scope.",
          "type": "object",
          "allOf": [
            {
              "properties": {
                "identifier": {
                  "description": "Identifier of the permission or permission set.",
                  "allOf": [
                    {
                      "$ref": "#/definitions/Identifier"
                    }
                  ]
                },
                "allow": {
                  "description": "Data that defines what is allowed by the scope.",
                  "type": [
                    "array",
                    "null"
                  ],
                  "items": {
                    "$ref": "#/definitions/Value"
                  }
                },
                "deny": {
                  "description": "Data that defines what is denied by the scope. This should be prioritized by validation logic.",
                  "type": [
                    "array",
                    "null"
                  ],
                  "items": {
                    "$ref": "#/definitions/Value"
                  }
                }
              }
            }
          ],
          "required": [
            "identifier"
          ]
        }
      ]
    },
    "Identifier": {
      "description": "Permission identifier",
      "oneOf": [
        {
          "description": "Default core plugins set.\n#### This default permission set includes:\n\n- `core:path:default`\n- `core:event:default`\n- `core:window:default`\n- `core:webview:default`\n- `core:app:default`\n- `core:image:default`\n- `core:resources:default`\n- `core:menu:default`\n- `core:tray:default`",
          "type": "string",
          "const": "core:default",
          "markdownDescription": "Default core plugins set.\n#### This default permission set includes:\n\n- `core:path:default`\n- `core:event:default`\n- `core:window:default`\n- `core:webview:default`\n- `core:app:default`\n- `core:image:default`\n- `core:resources:default`\n- `core:menu:default`\n- `core:tray:default`"
        },
        {
          "description": "Default permissions for the plugin.\n#### This default permission set includes:\n\n- `allow-version`\n- `allow-name`\n- `allow-tauri-version`\n- `allow-identifier`\n- `allow-bundle-type`\n- `allow-register-listener`\n- `allow-remove-listener`\n- `allow-supports-multiple-windows`",
          "type": "string",
          "const": "core:app:default",
          "markdownDescription": "Default permissions for the plugin.\n#### This default permission set includes:\n\n- `allow-version`\n- `allow-name`\n- `allow-tauri-version`\n- `allow-identifier`\n- `allow-bundle-type`\n- `allow-register-listener`\n- `allow-remove-listener`\n- `allow-supports-multiple-windows`"
        },
        {
          "description": "Enables the app_hide command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-app-hide",
          "markdownDescription": "Enables the app_hide command without any pre-configured scope."
        },
        {
          "description": "Enables the app_show command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-app-show",
          "markdownDescription": "Enables the app_show command without any pre-configured scope."
        },
        {
          "description": "Enables the bundle_type command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-bundle-type",
          "markdownDescription": "Enables the bundle_type command without any pre-configured scope."
        },
        {
          "description": "Enables the default_window_icon command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-default-window-icon",
          "markdownDescription": "Enables the default_window_icon command without any pre-configured scope."
        },
        {
          "description": "Enables the exit command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-exit",
          "markdownDescription": "Enables the exit command without any pre-configured scope."
        },
        {
          "description": "Enables the fetch_data_store_identifiers command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-fetch-data-store-identifiers",
          "markdownDescription": "Enables the fetch_data_store_identifiers command without any pre-configured scope."
        },
        {
          "description": "Enables the identifier command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-identifier",
          "markdownDescription": "Enables the identifier command without any pre-configured scope."
        },
        {
          "description": "Enables the name command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-name",
          "markdownDescription": "Enables the name command without any pre-configured scope."
        },
        {
          "description": "Enables the register_listener command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-register-listener",
          "markdownDescription": "Enables the register_listener command without any pre-configured scope."
        },
        {
          "description": "Enables the remove_data_store command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-remove-data-store",
          "markdownDescription": "Enables the remove_data_store command without any pre-configured scope."
        },
        {
          "description": "Enables the remove_listener command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-remove-listener",
          "markdownDescription": "Enables the remove_listener command without any pre-configured scope."
        },
        {
          "description": "Enables the set_app_theme command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-set-app-theme",
          "markdownDescription": "Enables the set_app_theme command without any pre-configured scope."
        },
        {
          "description": "Enables the set_dock_visibility command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-set-dock-visibility",
          "markdownDescription": "Enables the set_dock_visibility command without any pre-configured scope."
        },
        {
          "description": "Enables the supports_multiple_windows command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-supports-multiple-windows",
          "markdownDescription": "Enables the supports_multiple_windows command without any pre-configured scope."
        },
        {
          "description": "Enables the tauri_version command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-tauri-version",
          "markdownDescription": "Enables the tauri_version command without any pre-configured scope."
        },
        {
          "description": "Enables the version command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:allow-version",
          "markdownDescription": "Enables the version command without any pre-configured scope."
        },
        {
          "description": "Denies the app_hide command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-app-hide",
          "markdownDescription": "Denies the app_hide command without any pre-configured scope."
        },
        {
          "description": "Denies the app_show command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-app-show",
          "markdownDescription": "Denies the app_show command without any pre-configured scope."
        },
        {
          "description": "Denies the bundle_type command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-bundle-type",
          "markdownDescription": "Denies the bundle_type command without any pre-configured scope."
        },
        {
          "description": "Denies the default_window_icon command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-default-window-icon",
          "markdownDescription": "Denies the default_window_icon command without any pre-configured scope."
        },
        {
          "description": "Denies the exit command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-exit",
          "markdownDescription": "Denies the exit command without any pre-configured scope."
        },
        {
          "description": "Denies the fetch_data_store_identifiers command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-fetch-data-store-identifiers",
          "markdownDescription": "Denies the fetch_data_store_identifiers command without any pre-configured scope."
        },
        {
          "description": "Denies the identifier command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-identifier",
          "markdownDescription": "Denies the identifier command without any pre-configured scope."
        },
        {
          "description": "Denies the name command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-name",
          "markdownDescription": "Denies the name command without any pre-configured scope."
        },
        {
          "description": "Denies the register_listener command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-register-listener",
          "markdownDescription": "Denies the register_listener command without any pre-configured scope."
        },
        {
          "description": "Denies the remove_data_store command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-remove-data-store",
          "markdownDescription": "Denies the remove_data_store command without any pre-configured scope."
        },
        {
          "description": "Denies the remove_listener command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-remove-listener",
          "markdownDescription": "Denies the remove_listener command without any pre-configured scope."
        },
        {
          "description": "Denies the set_app_theme command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-set-app-theme",
          "markdownDescription": "Denies the set_app_theme command without any pre-configured scope."
        },
        {
          "description": "Denies the set_dock_visibility command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-set-dock-visibility",
          "markdownDescription": "Denies the set_dock_visibility command without any pre-configured scope."
        },
        {
          "description": "Denies the supports_multiple_windows command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-supports-multiple-windows",
          "markdownDescription": "Denies the supports_multiple_windows command without any pre-configured scope."
        },
        {
          "description": "Denies the tauri_version command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-tauri-version",
          "markdownDescription": "Denies the tauri_version command without any pre-configured scope."
        },
        {
          "description": "Denies the version command without any pre-configured scope.",
          "type": "string",
          "const": "core:app:deny-version",
          "markdownDescription": "Denies the version command without any pre-configured scope."
        },
        {
          "description": "Default permissions for the plugin, which enables all commands.\n#### This default permission set includes:\n\n- `allow-listen`\n- `allow-unlisten`\n- `allow-emit`\n- `allow-emit-to`",
          "type": "string",
          "const": "core:event:default",
          "markdownDescription": "Default permissions for the plugin, which enables all commands.\n#### This default permission set includes:\n\n- `allow-listen`\n- `allow-unlisten`\n- `allow-emit`\n- `allow-emit-to`"
        },
        {
          "description": "Enables the emit command without any pre-configured scope.",
          "type": "string",
          "const": "core:event:allow-emit",
          "markdownDescription": "Enables the emit command without any pre-configured scope."
        },
        {
          "description": "Enables the emit_to command without any pre-configured scope.",
          "type": "string",
          "const": "core:event:allow-emit-to",
          "markdownDescription": "Enables the emit_to command without any pre-configured scope."
        },
        {
          "description": "Enables the listen command without any pre-configured scope.",
          "type": "string",
          "const": "core:event:allow-listen",
          "markdownDescription": "Enables the listen command without any pre-configured scope."
        },
        {
          "description": "Enables the unlisten command without any pre-configured scope.",
          "type": "string",
          "const": "core:event:allow-unlisten",
          "markdownDescription": "Enables the unlisten command without any pre-configured scope."
        },
        {
          "description": "Denies the emit command without any pre-configured scope.",
          "type": "string",
          "const": "core:event:deny-emit",
          "markdownDescription": "Denies the emit command without any pre-configured scope."
        },
        {
          "description": "Denies the emit_to command without any pre-configured scope.",
          "type": "string",
          "const": "core:event:deny-emit-to",
          "markdownDescription": "Denies the emit_to command without any pre-configured scope."
        },
        {
          "description": "Denies the listen command without any pre-configured scope.",
          "type": "string",
          "const": "core:event:deny-listen",
          "markdownDescription": "Denies the listen command without any pre-configured scope."
        },
        {
          "description": "Denies the unlisten command without any pre-configured scope.",
          "type": "string",
          "const": "core:event:deny-unlisten",
          "markdownDescription": "Denies the unlisten command without any pre-configured scope."
        },
        {
          "description": "Default permissions for the plugin, which enables all commands.\n#### This default permission set includes:\n\n- `allow-new`\n- `allow-from-bytes`\n- `allow-from-path`\n- `allow-rgba`\n- `allow-size`",
          "type": "string",
          "const": "core:image:default",
          "markdownDescription": "Default permissions for the plugin, which enables all commands.\n#### This default permission set includes:\n\n- `allow-new`\n- `allow-from-bytes`\n- `allow-from-path`\n- `allow-rgba`\n- `allow-size`"
        },
        {
          "description": "Enables the from_bytes command without any pre-configured scope.",
          "type": "string",
          "const": "core:image:allow-from-bytes",
          "markdownDescription": "Enables the from_bytes command without any pre-configured scope."
        },
        {
          "description": "Enables the from_path command without any pre-configured scope.",
          "type": "string",
          "const": "core:image:allow-from-path",
          "markdownDescription": "Enables the from_path command without any pre-configured scope."
        },
        {
          "description": "Enables the new command without any pre-configured scope.",
          "type": "string",
          "const": "core:image:allow-new",
          "markdownDescription": "Enables the new command without any pre-configured scope."
        },
        {
          "description": "Enables the rgba command without any pre-configured scope.",
          "type": "string",
          "const": "core:image:allow-rgba",
          "markdownDescription": "Enables the rgba command without any pre-configured scope."
        },
        {
          "description": "Enables the size command without any pre-configured scope.",
          "type": "string",
          "const": "core:image:allow-size",
          "markdownDescription": "Enables the size command without any pre-configured scope."
        },
        {
          "description": "Denies the from_bytes command without any pre-configured scope.",
          "type": "string",
          "const": "core:image:deny-from-bytes",
          "markdownDescription": "Denies the from_bytes command without any pre-configured scope."
        },
        {
          "description": "Denies the from_path command without any pre-configured scope.",
          "type": "string",
          "const": "core:image:deny-from-path",
          "markdownDescription": "Denies the from_path command without any pre-configured scope."
        },
        {
          "description": "Denies the new command without any pre-configured scope.",
          "type": "string",
          "const": "core:image:deny-new",
          "markdownDescription": "Denies the new command without any pre-configured scope."
        },
        {
          "description": "Denies the rgba command without any pre-configured scope.",
          "type": "string",
          "const": "core:image:deny-rgba",
          "markdownDescription": "Denies the rgba command without any pre-configured scope."
        },
        {
          "description": "Denies the size command without any pre-configured scope.",
          "type": "string",
          "const": "core:image:deny-size",
          "markdownDescription": "Denies the size command without any pre-configured scope."
        },
        {
          "description": "Default permissions for the plugin, which enables all commands.\n#### This default permission set includes:\n\n- `allow-new`\n- `allow-append`\n- `allow-prepend`\n- `allow-insert`\n- `allow-remove`\n- `allow-remove-at`\n- `allow-items`\n- `allow-get`\n- `allow-popup`\n- `allow-create-default`\n- `allow-set-as-app-menu`\n- `allow-set-as-window-menu`\n- `allow-text`\n- `allow-set-text`\n- `allow-is-enabled`\n- `allow-set-enabled`\n- `allow-set-accelerator`\n- `allow-set-as-windows-menu-for-nsapp`\n- `allow-set-as-help-menu-for-nsapp`\n- `allow-is-checked`\n- `allow-set-checked`\n- `allow-set-icon`",
          "type": "string",
          "const": "core:menu:default",
          "markdownDescription": "Default permissions for the plugin, which enables all commands.\n#### This default permission set includes:\n\n- `allow-new`\n- `allow-append`\n- `allow-prepend`\n- `allow-insert`\n- `allow-remove`\n- `allow-remove-at`\n- `allow-items`\n- `allow-get`\n- `allow-popup`\n- `allow-create-default`\n- `allow-set-as-app-menu`\n- `allow-set-as-window-menu`\n- `allow-text`\n- `allow-set-text`\n- `allow-is-enabled`\n- `allow-set-enabled`\n- `allow-set-accelerator`\n- `allow-set-as-windows-menu-for-nsapp`\n- `allow-set-as-help-menu-for-nsapp`\n- `allow-is-checked`\n- `allow-set-checked`\n- `allow-set-icon`"
        },
        {
          "description": "Enables the append command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-append",
          "markdownDescription": "Enables the append command without any pre-configured scope."
        },
        {
          "description": "Enables the create_default command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-create-default",
          "markdownDescription": "Enables the create_default command without any pre-configured scope."
        },
        {
          "description": "Enables the get command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-get",
          "markdownDescription": "Enables the get command without any pre-configured scope."
        },
        {
          "description": "Enables the insert command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-insert",
          "markdownDescription": "Enables the insert command without any pre-configured scope."
        },
        {
          "description": "Enables the is_checked command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-is-checked",
          "markdownDescription": "Enables the is_checked command without any pre-configured scope."
        },
        {
          "description": "Enables the is_enabled command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-is-enabled",
          "markdownDescription": "Enables the is_enabled command without any pre-configured scope."
        },
        {
          "description": "Enables the items command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-items",
          "markdownDescription": "Enables the items command without any pre-configured scope."
        },
        {
          "description": "Enables the new command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-new",
          "markdownDescription": "Enables the new command without any pre-configured scope."
        },
        {
          "description": "Enables the popup command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-popup",
          "markdownDescription": "Enables the popup command without any pre-configured scope."
        },
        {
          "description": "Enables the prepend command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-prepend",
          "markdownDescription": "Enables the prepend command without any pre-configured scope."
        },
        {
          "description": "Enables the remove command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-remove",
          "markdownDescription": "Enables the remove command without any pre-configured scope."
        },
        {
          "description": "Enables the remove_at command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-remove-at",
          "markdownDescription": "Enables the remove_at command without any pre-configured scope."
        },
        {
          "description": "Enables the set_accelerator command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-set-accelerator",
          "markdownDescription": "Enables the set_accelerator command without any pre-configured scope."
        },
        {
          "description": "Enables the set_as_app_menu command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-set-as-app-menu",
          "markdownDescription": "Enables the set_as_app_menu command without any pre-configured scope."
        },
        {
          "description": "Enables the set_as_help_menu_for_nsapp command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-set-as-help-menu-for-nsapp",
          "markdownDescription": "Enables the set_as_help_menu_for_nsapp command without any pre-configured scope."
        },
        {
          "description": "Enables the set_as_window_menu command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-set-as-window-menu",
          "markdownDescription": "Enables the set_as_window_menu command without any pre-configured scope."
        },
        {
          "description": "Enables the set_as_windows_menu_for_nsapp command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-set-as-windows-menu-for-nsapp",
          "markdownDescription": "Enables the set_as_windows_menu_for_nsapp command without any pre-configured scope."
        },
        {
          "description": "Enables the set_checked command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-set-checked",
          "markdownDescription": "Enables the set_checked command without any pre-configured scope."
        },
        {
          "description": "Enables the set_enabled command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-set-enabled",
          "markdownDescription": "Enables the set_enabled command without any pre-configured scope."
        },
        {
          "description": "Enables the set_icon command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-set-icon",
          "markdownDescription": "Enables the set_icon command without any pre-configured scope."
        },
        {
          "description": "Enables the set_text command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-set-text",
          "markdownDescription": "Enables the set_text command without any pre-configured scope."
        },
        {
          "description": "Enables the text command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:allow-text",
          "markdownDescription": "Enables the text command without any pre-configured scope."
        },
        {
          "description": "Denies the append command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-append",
          "markdownDescription": "Denies the append command without any pre-configured scope."
        },
        {
          "description": "Denies the create_default command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-create-default",
          "markdownDescription": "Denies the create_default command without any pre-configured scope."
        },
        {
          "description": "Denies the get command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-get",
          "markdownDescription": "Denies the get command without any pre-configured scope."
        },
        {
          "description": "Denies the insert command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-insert",
          "markdownDescription": "Denies the insert command without any pre-configured scope."
        },
        {
          "description": "Denies the is_checked command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-is-checked",
          "markdownDescription": "Denies the is_checked command without any pre-configured scope."
        },
        {
          "description": "Denies the is_enabled command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-is-enabled",
          "markdownDescription": "Denies the is_enabled command without any pre-configured scope."
        },
        {
          "description": "Denies the items command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-items",
          "markdownDescription": "Denies the items command without any pre-configured scope."
        },
        {
          "description": "Denies the new command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-new",
          "markdownDescription": "Denies the new command without any pre-configured scope."
        },
        {
          "description": "Denies the popup command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-popup",
          "markdownDescription": "Denies the popup command without any pre-configured scope."
        },
        {
          "description": "Denies the prepend command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-prepend",
          "markdownDescription": "Denies the prepend command without any pre-configured scope."
        },
        {
          "description": "Denies the remove command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-remove",
          "markdownDescription": "Denies the remove command without any pre-configured scope."
        },
        {
          "description": "Denies the remove_at command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-remove-at",
          "markdownDescription": "Denies the remove_at command without any pre-configured scope."
        },
        {
          "description": "Denies the set_accelerator command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-set-accelerator",
          "markdownDescription": "Denies the set_accelerator command without any pre-configured scope."
        },
        {
          "description": "Denies the set_as_app_menu command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-set-as-app-menu",
          "markdownDescription": "Denies the set_as_app_menu command without any pre-configured scope."
        },
        {
          "description": "Denies the set_as_help_menu_for_nsapp command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-set-as-help-menu-for-nsapp",
          "markdownDescription": "Denies the set_as_help_menu_for_nsapp command without any pre-configured scope."
        },
        {
          "description": "Denies the set_as_window_menu command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-set-as-window-menu",
          "markdownDescription": "Denies the set_as_window_menu command without any pre-configured scope."
        },
        {
          "description": "Denies the set_as_windows_menu_for_nsapp command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-set-as-windows-menu-for-nsapp",
          "markdownDescription": "Denies the set_as_windows_menu_for_nsapp command without any pre-configured scope."
        },
        {
          "description": "Denies the set_checked command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-set-checked",
          "markdownDescription": "Denies the set_checked command without any pre-configured scope."
        },
        {
          "description": "Denies the set_enabled command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-set-enabled",
          "markdownDescription": "Denies the set_enabled command without any pre-configured scope."
        },
        {
          "description": "Denies the set_icon command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-set-icon",
          "markdownDescription": "Denies the set_icon command without any pre-configured scope."
        },
        {
          "description": "Denies the set_text command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-set-text",
          "markdownDescription": "Denies the set_text command without any pre-configured scope."
        },
        {
          "description": "Denies the text command without any pre-configured scope.",
          "type": "string",
          "const": "core:menu:deny-text",
          "markdownDescription": "Denies the text command without any pre-configured scope."
        },
        {
          "description": "Default permissions for the plugin, which enables all commands.\n#### This default permission set includes:\n\n- `allow-resolve-directory`\n- `allow-resolve`\n- `allow-normalize`\n- `allow-join`\n- `allow-dirname`\n- `allow-extname`\n- `allow-basename`\n- `allow-is-absolute`",
          "type": "string",
          "const": "core:path:default",
          "markdownDescription": "Default permissions for the plugin, which enables all commands.\n#### This default permission set includes:\n\n- `allow-resolve-directory`\n- `allow-resolve`\n- `allow-normalize`\n- `allow-join`\n- `allow-dirname`\n- `allow-extname`\n- `allow-basename`\n- `allow-is-absolute`"
        },
        {
          "description": "Enables the basename command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:allow-basename",
          "markdownDescription": "Enables the basename command without any pre-configured scope."
        },
        {
          "description": "Enables the dirname command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:allow-dirname",
          "markdownDescription": "Enables the dirname command without any pre-configured scope."
        },
        {
          "description": "Enables the extname command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:allow-extname",
          "markdownDescription": "Enables the extname command without any pre-configured scope."
        },
        {
          "description": "Enables the is_absolute command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:allow-is-absolute",
          "markdownDescription": "Enables the is_absolute command without any pre-configured scope."
        },
        {
          "description": "Enables the join command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:allow-join",
          "markdownDescription": "Enables the join command without any pre-configured scope."
        },
        {
          "description": "Enables the normalize command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:allow-normalize",
          "markdownDescription": "Enables the normalize command without any pre-configured scope."
        },
        {
          "description": "Enables the resolve command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:allow-resolve",
          "markdownDescription": "Enables the resolve command without any pre-configured scope."
        },
        {
          "description": "Enables the resolve_directory command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:allow-resolve-directory",
          "markdownDescription": "Enables the resolve_directory command without any pre-configured scope."
        },
        {
          "description": "Denies the basename command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:deny-basename",
          "markdownDescription": "Denies the basename command without any pre-configured scope."
        },
        {
          "description": "Denies the dirname command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:deny-dirname",
          "markdownDescription": "Denies the dirname command without any pre-configured scope."
        },
        {
          "description": "Denies the extname command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:deny-extname",
          "markdownDescription": "Denies the extname command without any pre-configured scope."
        },
        {
          "description": "Denies the is_absolute command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:deny-is-absolute",
          "markdownDescription": "Denies the is_absolute command without any pre-configured scope."
        },
        {
          "description": "Denies the join command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:deny-join",
          "markdownDescription": "Denies the join command without any pre-configured scope."
        },
        {
          "description": "Denies the normalize command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:deny-normalize",
          "markdownDescription": "Denies the normalize command without any pre-configured scope."
        },
        {
          "description": "Denies the resolve command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:deny-resolve",
          "markdownDescription": "Denies the resolve command without any pre-configured scope."
        },
        {
          "description": "Denies the resolve_directory command without any pre-configured scope.",
          "type": "string",
          "const": "core:path:deny-resolve-directory",
          "markdownDescription": "Denies the resolve_directory command without any pre-configured scope."
        },
        {
          "description": "Default permissions for the plugin, which enables all commands.\n#### This default permission set includes:\n\n- `allow-close`",
          "type": "string",
          "const": "core:resources:default",
          "markdownDescription": "Default permissions for the plugin, which enables all commands.\n#### This default permission set includes:\n\n- `allow-close`"
        },
        {
          "description": "Enables the close command without any pre-configured scope.",
          "type": "string",
          "const": "core:resources:allow-close",
          "markdownDescription": "Enables the close command without any pre-configured scope."
        },
        {
          "description": "Denies the close command without any pre-configured scope.",
          "type": "string",
          "const": "core:resources:deny-close",
          "markdownDescription": "Denies the close command without any pre-configured scope."
        },
        {
          "description": "Default permissions for the plugin, which enables all commands.\n#### This default permission set includes:\n\n- `allow-new`\n- `allow-get-by-id`\n- `allow-remove-by-id`\n- `allow-set-icon`\n- `allow-set-menu`\n- `allow-set-tooltip`\n- `allow-set-title`\n- `allow-set-visible`\n- `allow-set-temp-dir-path`\n- `allow-set-icon-as-template`\n- `allow-set-icon-with-as-template`\n- `allow-set-show-menu-on-left-click`",
          "type": "string",
          "const": "core:tray:default",
          "markdownDescription": "Default permissions for the plugin, which enables all commands.\n#### This default permission set includes:\n\n- `allow-new`\n- `allow-get-by-id`\n- `allow-remove-by-id`\n- `allow-set-icon`\n- `allow-set-menu`\n- `allow-set-tooltip`\n- `allow-set-title`\n- `allow-set-visible`\n- `allow-set-temp-dir-path`\n- `allow-set-icon-as-template`\n- `allow-set-icon-with-as-template`\n- `allow-set-show-menu-on-left-click`"
        },
        {
          "description": "Enables the get_by_id command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:allow-get-by-id",
          "markdownDescription": "Enables the get_by_id command without any pre-configured scope."
        },
        {
          "description": "Enables the new command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:allow-new",
          "markdownDescription": "Enables the new command without any pre-configured scope."
        },
        {
          "description": "Enables the remove_by_id command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:allow-remove-by-id",
          "markdownDescription": "Enables the remove_by_id command without any pre-configured scope."
        },
        {
          "description": "Enables the set_icon command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:allow-set-icon",
          "markdownDescription": "Enables the set_icon command without any pre-configured scope."
        },
        {
          "description": "Enables the set_icon_as_template command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:allow-set-icon-as-template",
          "markdownDescription": "Enables the set_icon_as_template command without any pre-configured scope."
        },
        {
          "description": "Enables the set_icon_with_as_template command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:allow-set-icon-with-as-template",
          "markdownDescription": "Enables the set_icon_with_as_template command without any pre-configured scope."
        },
        {
          "description": "Enables the set_menu command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:allow-set-menu",
          "markdownDescription": "Enables the set_menu command without any pre-configured scope."
        },
        {
          "description": "Enables the set_show_menu_on_left_click command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:allow-set-show-menu-on-left-click",
          "markdownDescription": "Enables the set_show_menu_on_left_click command without any pre-configured scope."
        },
        {
          "description": "Enables the set_temp_dir_path command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:allow-set-temp-dir-path",
          "markdownDescription": "Enables the set_temp_dir_path command without any pre-configured scope."
        },
        {
          "description": "Enables the set_title command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:allow-set-title",
          "markdownDescription": "Enables the set_title command without any pre-configured scope."
        },
        {
          "description": "Enables the set_tooltip command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:allow-set-tooltip",
          "markdownDescription": "Enables the set_tooltip command without any pre-configured scope."
        },
        {
          "description": "Enables the set_visible command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:allow-set-visible",
          "markdownDescription": "Enables the set_visible command without any pre-configured scope."
        },
        {
          "description": "Denies the get_by_id command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:deny-get-by-id",
          "markdownDescription": "Denies the get_by_id command without any pre-configured scope."
        },
        {
          "description": "Denies the new command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:deny-new",
          "markdownDescription": "Denies the new command without any pre-configured scope."
        },
        {
          "description": "Denies the remove_by_id command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:deny-remove-by-id",
          "markdownDescription": "Denies the remove_by_id command without any pre-configured scope."
        },
        {
          "description": "Denies the set_icon command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:deny-set-icon",
          "markdownDescription": "Denies the set_icon command without any pre-configured scope."
        },
        {
          "description": "Denies the set_icon_as_template command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:deny-set-icon-as-template",
          "markdownDescription": "Denies the set_icon_as_template command without any pre-configured scope."
        },
        {
          "description": "Denies the set_icon_with_as_template command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:deny-set-icon-with-as-template",
          "markdownDescription": "Denies the set_icon_with_as_template command without any pre-configured scope."
        },
        {
          "description": "Denies the set_menu command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:deny-set-menu",
          "markdownDescription": "Denies the set_menu command without any pre-configured scope."
        },
        {
          "description": "Denies the set_show_menu_on_left_click command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:deny-set-show-menu-on-left-click",
          "markdownDescription": "Denies the set_show_menu_on_left_click command without any pre-configured scope."
        },
        {
          "description": "Denies the set_temp_dir_path command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:deny-set-temp-dir-path",
          "markdownDescription": "Denies the set_temp_dir_path command without any pre-configured scope."
        },
        {
          "description": "Denies the set_title command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:deny-set-title",
          "markdownDescription": "Denies the set_title command without any pre-configured scope."
        },
        {
          "description": "Denies the set_tooltip command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:deny-set-tooltip",
          "markdownDescription": "Denies the set_tooltip command without any pre-configured scope."
        },
        {
          "description": "Denies the set_visible command without any pre-configured scope.",
          "type": "string",
          "const": "core:tray:deny-set-visible",
          "markdownDescription": "Denies the set_visible command without any pre-configured scope."
        },
        {
          "description": "Default permissions for the plugin.\n#### This default permission set includes:\n\n- `allow-get-all-webviews`\n- `allow-webview-position`\n- `allow-webview-size`\n- `allow-internal-toggle-devtools`",
          "type": "string",
          "const": "core:webview:default",
          "markdownDescription": "Default permissions for the plugin.\n#### This default permission set includes:\n\n- `allow-get-all-webviews`\n- `allow-webview-position`\n- `allow-webview-size`\n- `allow-internal-toggle-devtools`"
        },
        {
          "description": "Enables the clear_all_browsing_data command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-clear-all-browsing-data",
          "markdownDescription": "Enables the clear_all_browsing_data command without any pre-configured scope."
        },
        {
          "description": "Enables the create_webview command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-create-webview",
          "markdownDescription": "Enables the create_webview command without any pre-configured scope."
        },
        {
          "description": "Enables the create_webview_window command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-create-webview-window",
          "markdownDescription": "Enables the create_webview_window command without any pre-configured scope."
        },
        {
          "description": "Enables the get_all_webviews command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-get-all-webviews",
          "markdownDescription": "Enables the get_all_webviews command without any pre-configured scope."
        },
        {
          "description": "Enables the internal_toggle_devtools command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-internal-toggle-devtools",
          "markdownDescription": "Enables the internal_toggle_devtools command without any pre-configured scope."
        },
        {
          "description": "Enables the print command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-print",
          "markdownDescription": "Enables the print command without any pre-configured scope."
        },
        {
          "description": "Enables the reparent command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-reparent",
          "markdownDescription": "Enables the reparent command without any pre-configured scope."
        },
        {
          "description": "Enables the set_webview_auto_resize command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-set-webview-auto-resize",
          "markdownDescription": "Enables the set_webview_auto_resize command without any pre-configured scope."
        },
        {
          "description": "Enables the set_webview_background_color command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-set-webview-background-color",
          "markdownDescription": "Enables the set_webview_background_color command without any pre-configured scope."
        },
        {
          "description": "Enables the set_webview_focus command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-set-webview-focus",
          "markdownDescription": "Enables the set_webview_focus command without any pre-configured scope."
        },
        {
          "description": "Enables the set_webview_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-set-webview-position",
          "markdownDescription": "Enables the set_webview_position command without any pre-configured scope."
        },
        {
          "description": "Enables the set_webview_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-set-webview-size",
          "markdownDescription": "Enables the set_webview_size command without any pre-configured scope."
        },
        {
          "description": "Enables the set_webview_zoom command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-set-webview-zoom",
          "markdownDescription": "Enables the set_webview_zoom command without any pre-configured scope."
        },
        {
          "description": "Enables the webview_close command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-webview-close",
          "markdownDescription": "Enables the webview_close command without any pre-configured scope."
        },
        {
          "description": "Enables the webview_hide command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-webview-hide",
          "markdownDescription": "Enables the webview_hide command without any pre-configured scope."
        },
        {
          "description": "Enables the webview_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-webview-position",
          "markdownDescription": "Enables the webview_position command without any pre-configured scope."
        },
        {
          "description": "Enables the webview_show command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-webview-show",
          "markdownDescription": "Enables the webview_show command without any pre-configured scope."
        },
        {
          "description": "Enables the webview_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:allow-webview-size",
          "markdownDescription": "Enables the webview_size command without any pre-configured scope."
        },
        {
          "description": "Denies the clear_all_browsing_data command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-clear-all-browsing-data",
          "markdownDescription": "Denies the clear_all_browsing_data command without any pre-configured scope."
        },
        {
          "description": "Denies the create_webview command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-create-webview",
          "markdownDescription": "Denies the create_webview command without any pre-configured scope."
        },
        {
          "description": "Denies the create_webview_window command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-create-webview-window",
          "markdownDescription": "Denies the create_webview_window command without any pre-configured scope."
        },
        {
          "description": "Denies the get_all_webviews command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-get-all-webviews",
          "markdownDescription": "Denies the get_all_webviews command without any pre-configured scope."
        },
        {
          "description": "Denies the internal_toggle_devtools command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-internal-toggle-devtools",
          "markdownDescription": "Denies the internal_toggle_devtools command without any pre-configured scope."
        },
        {
          "description": "Denies the print command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-print",
          "markdownDescription": "Denies the print command without any pre-configured scope."
        },
        {
          "description": "Denies the reparent command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-reparent",
          "markdownDescription": "Denies the reparent command without any pre-configured scope."
        },
        {
          "description": "Denies the set_webview_auto_resize command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-set-webview-auto-resize",
          "markdownDescription": "Denies the set_webview_auto_resize command without any pre-configured scope."
        },
        {
          "description": "Denies the set_webview_background_color command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-set-webview-background-color",
          "markdownDescription": "Denies the set_webview_background_color command without any pre-configured scope."
        },
        {
          "description": "Denies the set_webview_focus command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-set-webview-focus",
          "markdownDescription": "Denies the set_webview_focus command without any pre-configured scope."
        },
        {
          "description": "Denies the set_webview_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-set-webview-position",
          "markdownDescription": "Denies the set_webview_position command without any pre-configured scope."
        },
        {
          "description": "Denies the set_webview_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-set-webview-size",
          "markdownDescription": "Denies the set_webview_size command without any pre-configured scope."
        },
        {
          "description": "Denies the set_webview_zoom command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-set-webview-zoom",
          "markdownDescription": "Denies the set_webview_zoom command without any pre-configured scope."
        },
        {
          "description": "Denies the webview_close command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-webview-close",
          "markdownDescription": "Denies the webview_close command without any pre-configured scope."
        },
        {
          "description": "Denies the webview_hide command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-webview-hide",
          "markdownDescription": "Denies the webview_hide command without any pre-configured scope."
        },
        {
          "description": "Denies the webview_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-webview-position",
          "markdownDescription": "Denies the webview_position command without any pre-configured scope."
        },
        {
          "description": "Denies the webview_show command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-webview-show",
          "markdownDescription": "Denies the webview_show command without any pre-configured scope."
        },
        {
          "description": "Denies the webview_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:webview:deny-webview-size",
          "markdownDescription": "Denies the webview_size command without any pre-configured scope."
        },
        {
          "description": "Default permissions for the plugin.\n#### This default permission set includes:\n\n- `allow-get-all-windows`\n- `allow-scale-factor`\n- `allow-inner-position`\n- `allow-outer-position`\n- `allow-inner-size`\n- `allow-outer-size`\n- `allow-is-fullscreen`\n- `allow-is-minimized`\n- `allow-is-maximized`\n- `allow-is-focused`\n- `allow-is-decorated`\n- `allow-is-resizable`\n- `allow-is-maximizable`\n- `allow-is-minimizable`\n- `allow-is-closable`\n- `allow-is-visible`\n- `allow-is-enabled`\n- `allow-title`\n- `allow-current-monitor`\n- `allow-primary-monitor`\n- `allow-monitor-from-point`\n- `allow-available-monitors`\n- `allow-cursor-position`\n- `allow-theme`\n- `allow-is-always-on-top`\n- `allow-activity-name`\n- `allow-scene-identifier`\n- `allow-internal-toggle-maximize`",
          "type": "string",
          "const": "core:window:default",
          "markdownDescription": "Default permissions for the plugin.\n#### This default permission set includes:\n\n- `allow-get-all-windows`\n- `allow-scale-factor`\n- `allow-inner-position`\n- `allow-outer-position`\n- `allow-inner-size`\n- `allow-outer-size`\n- `allow-is-fullscreen`\n- `allow-is-minimized`\n- `allow-is-maximized`\n- `allow-is-focused`\n- `allow-is-decorated`\n- `allow-is-resizable`\n- `allow-is-maximizable`\n- `allow-is-minimizable`\n- `allow-is-closable`\n- `allow-is-visible`\n- `allow-is-enabled`\n- `allow-title`\n- `allow-current-monitor`\n- `allow-primary-monitor`\n- `allow-monitor-from-point`\n- `allow-available-monitors`\n- `allow-cursor-position`\n- `allow-theme`\n- `allow-is-always-on-top`\n- `allow-activity-name`\n- `allow-scene-identifier`\n- `allow-internal-toggle-maximize`"
        },
        {
          "description": "Enables the activity_name command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-activity-name",
          "markdownDescription": "Enables the activity_name command without any pre-configured scope."
        },
        {
          "description": "Enables the available_monitors command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-available-monitors",
          "markdownDescription": "Enables the available_monitors command without any pre-configured scope."
        },
        {
          "description": "Enables the center command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-center",
          "markdownDescription": "Enables the center command without any pre-configured scope."
        },
        {
          "description": "Enables the close command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-close",
          "markdownDescription": "Enables the close command without any pre-configured scope."
        },
        {
          "description": "Enables the create command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-create",
          "markdownDescription": "Enables the create command without any pre-configured scope."
        },
        {
          "description": "Enables the current_monitor command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-current-monitor",
          "markdownDescription": "Enables the current_monitor command without any pre-configured scope."
        },
        {
          "description": "Enables the cursor_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-cursor-position",
          "markdownDescription": "Enables the cursor_position command without any pre-configured scope."
        },
        {
          "description": "Enables the destroy command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-destroy",
          "markdownDescription": "Enables the destroy command without any pre-configured scope."
        },
        {
          "description": "Enables the get_all_windows command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-get-all-windows",
          "markdownDescription": "Enables the get_all_windows command without any pre-configured scope."
        },
        {
          "description": "Enables the hide command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-hide",
          "markdownDescription": "Enables the hide command without any pre-configured scope."
        },
        {
          "description": "Enables the inner_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-inner-position",
          "markdownDescription": "Enables the inner_position command without any pre-configured scope."
        },
        {
          "description": "Enables the inner_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-inner-size",
          "markdownDescription": "Enables the inner_size command without any pre-configured scope."
        },
        {
          "description": "Enables the internal_toggle_maximize command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-internal-toggle-maximize",
          "markdownDescription": "Enables the internal_toggle_maximize command without any pre-configured scope."
        },
        {
          "description": "Enables the is_always_on_top command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-is-always-on-top",
          "markdownDescription": "Enables the is_always_on_top command without any pre-configured scope."
        },
        {
          "description": "Enables the is_closable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-is-closable",
          "markdownDescription": "Enables the is_closable command without any pre-configured scope."
        },
        {
          "description": "Enables the is_decorated command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-is-decorated",
          "markdownDescription": "Enables the is_decorated command without any pre-configured scope."
        },
        {
          "description": "Enables the is_enabled command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-is-enabled",
          "markdownDescription": "Enables the is_enabled command without any pre-configured scope."
        },
        {
          "description": "Enables the is_focused command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-is-focused",
          "markdownDescription": "Enables the is_focused command without any pre-configured scope."
        },
        {
          "description": "Enables the is_fullscreen command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-is-fullscreen",
          "markdownDescription": "Enables the is_fullscreen command without any pre-configured scope."
        },
        {
          "description": "Enables the is_maximizable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-is-maximizable",
          "markdownDescription": "Enables the is_maximizable command without any pre-configured scope."
        },
        {
          "description": "Enables the is_maximized command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-is-maximized",
          "markdownDescription": "Enables the is_maximized command without any pre-configured scope."
        },
        {
          "description": "Enables the is_minimizable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-is-minimizable",
          "markdownDescription": "Enables the is_minimizable command without any pre-configured scope."
        },
        {
          "description": "Enables the is_minimized command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-is-minimized",
          "markdownDescription": "Enables the is_minimized command without any pre-configured scope."
        },
        {
          "description": "Enables the is_resizable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-is-resizable",
          "markdownDescription": "Enables the is_resizable command without any pre-configured scope."
        },
        {
          "description": "Enables the is_visible command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-is-visible",
          "markdownDescription": "Enables the is_visible command without any pre-configured scope."
        },
        {
          "description": "Enables the maximize command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-maximize",
          "markdownDescription": "Enables the maximize command without any pre-configured scope."
        },
        {
          "description": "Enables the minimize command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-minimize",
          "markdownDescription": "Enables the minimize command without any pre-configured scope."
        },
        {
          "description": "Enables the monitor_from_point command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-monitor-from-point",
          "markdownDescription": "Enables the monitor_from_point command without any pre-configured scope."
        },
        {
          "description": "Enables the outer_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-outer-position",
          "markdownDescription": "Enables the outer_position command without any pre-configured scope."
        },
        {
          "description": "Enables the outer_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-outer-size",
          "markdownDescription": "Enables the outer_size command without any pre-configured scope."
        },
        {
          "description": "Enables the primary_monitor command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-primary-monitor",
          "markdownDescription": "Enables the primary_monitor command without any pre-configured scope."
        },
        {
          "description": "Enables the request_user_attention command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-request-user-attention",
          "markdownDescription": "Enables the request_user_attention command without any pre-configured scope."
        },
        {
          "description": "Enables the scale_factor command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-scale-factor",
          "markdownDescription": "Enables the scale_factor command without any pre-configured scope."
        },
        {
          "description": "Enables the scene_identifier command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-scene-identifier",
          "markdownDescription": "Enables the scene_identifier command without any pre-configured scope."
        },
        {
          "description": "Enables the set_always_on_bottom command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-always-on-bottom",
          "markdownDescription": "Enables the set_always_on_bottom command without any pre-configured scope."
        },
        {
          "description": "Enables the set_always_on_top command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-always-on-top",
          "markdownDescription": "Enables the set_always_on_top command without any pre-configured scope."
        },
        {
          "description": "Enables the set_background_color command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-background-color",
          "markdownDescription": "Enables the set_background_color command without any pre-configured scope."
        },
        {
          "description": "Enables the set_badge_count command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-badge-count",
          "markdownDescription": "Enables the set_badge_count command without any pre-configured scope."
        },
        {
          "description": "Enables the set_badge_label command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-badge-label",
          "markdownDescription": "Enables the set_badge_label command without any pre-configured scope."
        },
        {
          "description": "Enables the set_closable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-closable",
          "markdownDescription": "Enables the set_closable command without any pre-configured scope."
        },
        {
          "description": "Enables the set_content_protected command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-content-protected",
          "markdownDescription": "Enables the set_content_protected command without any pre-configured scope."
        },
        {
          "description": "Enables the set_cursor_grab command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-cursor-grab",
          "markdownDescription": "Enables the set_cursor_grab command without any pre-configured scope."
        },
        {
          "description": "Enables the set_cursor_icon command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-cursor-icon",
          "markdownDescription": "Enables the set_cursor_icon command without any pre-configured scope."
        },
        {
          "description": "Enables the set_cursor_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-cursor-position",
          "markdownDescription": "Enables the set_cursor_position command without any pre-configured scope."
        },
        {
          "description": "Enables the set_cursor_visible command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-cursor-visible",
          "markdownDescription": "Enables the set_cursor_visible command without any pre-configured scope."
        },
        {
          "description": "Enables the set_decorations command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-decorations",
          "markdownDescription": "Enables the set_decorations command without any pre-configured scope."
        },
        {
          "description": "Enables the set_effects command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-effects",
          "markdownDescription": "Enables the set_effects command without any pre-configured scope."
        },
        {
          "description": "Enables the set_enabled command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-enabled",
          "markdownDescription": "Enables the set_enabled command without any pre-configured scope."
        },
        {
          "description": "Enables the set_focus command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-focus",
          "markdownDescription": "Enables the set_focus command without any pre-configured scope."
        },
        {
          "description": "Enables the set_focusable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-focusable",
          "markdownDescription": "Enables the set_focusable command without any pre-configured scope."
        },
        {
          "description": "Enables the set_fullscreen command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-fullscreen",
          "markdownDescription": "Enables the set_fullscreen command without any pre-configured scope."
        },
        {
          "description": "Enables the set_fullscreen_on_monitor command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-fullscreen-on-monitor",
          "markdownDescription": "Enables the set_fullscreen_on_monitor command without any pre-configured scope."
        },
        {
          "description": "Enables the set_icon command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-icon",
          "markdownDescription": "Enables the set_icon command without any pre-configured scope."
        },
        {
          "description": "Enables the set_ignore_cursor_events command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-ignore-cursor-events",
          "markdownDescription": "Enables the set_ignore_cursor_events command without any pre-configured scope."
        },
        {
          "description": "Enables the set_max_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-max-size",
          "markdownDescription": "Enables the set_max_size command without any pre-configured scope."
        },
        {
          "description": "Enables the set_maximizable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-maximizable",
          "markdownDescription": "Enables the set_maximizable command without any pre-configured scope."
        },
        {
          "description": "Enables the set_min_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-min-size",
          "markdownDescription": "Enables the set_min_size command without any pre-configured scope."
        },
        {
          "description": "Enables the set_minimizable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-minimizable",
          "markdownDescription": "Enables the set_minimizable command without any pre-configured scope."
        },
        {
          "description": "Enables the set_overlay_icon command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-overlay-icon",
          "markdownDescription": "Enables the set_overlay_icon command without any pre-configured scope."
        },
        {
          "description": "Enables the set_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-position",
          "markdownDescription": "Enables the set_position command without any pre-configured scope."
        },
        {
          "description": "Enables the set_progress_bar command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-progress-bar",
          "markdownDescription": "Enables the set_progress_bar command without any pre-configured scope."
        },
        {
          "description": "Enables the set_resizable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-resizable",
          "markdownDescription": "Enables the set_resizable command without any pre-configured scope."
        },
        {
          "description": "Enables the set_shadow command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-shadow",
          "markdownDescription": "Enables the set_shadow command without any pre-configured scope."
        },
        {
          "description": "Enables the set_simple_fullscreen command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-simple-fullscreen",
          "markdownDescription": "Enables the set_simple_fullscreen command without any pre-configured scope."
        },
        {
          "description": "Enables the set_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-size",
          "markdownDescription": "Enables the set_size command without any pre-configured scope."
        },
        {
          "description": "Enables the set_size_constraints command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-size-constraints",
          "markdownDescription": "Enables the set_size_constraints command without any pre-configured scope."
        },
        {
          "description": "Enables the set_skip_taskbar command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-skip-taskbar",
          "markdownDescription": "Enables the set_skip_taskbar command without any pre-configured scope."
        },
        {
          "description": "Enables the set_theme command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-theme",
          "markdownDescription": "Enables the set_theme command without any pre-configured scope."
        },
        {
          "description": "Enables the set_title command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-title",
          "markdownDescription": "Enables the set_title command without any pre-configured scope."
        },
        {
          "description": "Enables the set_title_bar_style command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-title-bar-style",
          "markdownDescription": "Enables the set_title_bar_style command without any pre-configured scope."
        },
        {
          "description": "Enables the set_visible_on_all_workspaces command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-set-visible-on-all-workspaces",
          "markdownDescription": "Enables the set_visible_on_all_workspaces command without any pre-configured scope."
        },
        {
          "description": "Enables the show command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-show",
          "markdownDescription": "Enables the show command without any pre-configured scope."
        },
        {
          "description": "Enables the start_dragging command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-start-dragging",
          "markdownDescription": "Enables the start_dragging command without any pre-configured scope."
        },
        {
          "description": "Enables the start_resize_dragging command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-start-resize-dragging",
          "markdownDescription": "Enables the start_resize_dragging command without any pre-configured scope."
        },
        {
          "description": "Enables the theme command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-theme",
          "markdownDescription": "Enables the theme command without any pre-configured scope."
        },
        {
          "description": "Enables the title command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-title",
          "markdownDescription": "Enables the title command without any pre-configured scope."
        },
        {
          "description": "Enables the toggle_maximize command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-toggle-maximize",
          "markdownDescription": "Enables the toggle_maximize command without any pre-configured scope."
        },
        {
          "description": "Enables the unmaximize command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-unmaximize",
          "markdownDescription": "Enables the unmaximize command without any pre-configured scope."
        },
        {
          "description": "Enables the unminimize command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:allow-unminimize",
          "markdownDescription": "Enables the unminimize command without any pre-configured scope."
        },
        {
          "description": "Denies the activity_name command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-activity-name",
          "markdownDescription": "Denies the activity_name command without any pre-configured scope."
        },
        {
          "description": "Denies the available_monitors command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-available-monitors",
          "markdownDescription": "Denies the available_monitors command without any pre-configured scope."
        },
        {
          "description": "Denies the center command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-center",
          "markdownDescription": "Denies the center command without any pre-configured scope."
        },
        {
          "description": "Denies the close command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-close",
          "markdownDescription": "Denies the close command without any pre-configured scope."
        },
        {
          "description": "Denies the create command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-create",
          "markdownDescription": "Denies the create command without any pre-configured scope."
        },
        {
          "description": "Denies the current_monitor command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-current-monitor",
          "markdownDescription": "Denies the current_monitor command without any pre-configured scope."
        },
        {
          "description": "Denies the cursor_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-cursor-position",
          "markdownDescription": "Denies the cursor_position command without any pre-configured scope."
        },
        {
          "description": "Denies the destroy command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-destroy",
          "markdownDescription": "Denies the destroy command without any pre-configured scope."
        },
        {
          "description": "Denies the get_all_windows command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-get-all-windows",
          "markdownDescription": "Denies the get_all_windows command without any pre-configured scope."
        },
        {
          "description": "Denies the hide command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-hide",
          "markdownDescription": "Denies the hide command without any pre-configured scope."
        },
        {
          "description": "Denies the inner_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-inner-position",
          "markdownDescription": "Denies the inner_position command without any pre-configured scope."
        },
        {
          "description": "Denies the inner_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-inner-size",
          "markdownDescription": "Denies the inner_size command without any pre-configured scope."
        },
        {
          "description": "Denies the internal_toggle_maximize command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-internal-toggle-maximize",
          "markdownDescription": "Denies the internal_toggle_maximize command without any pre-configured scope."
        },
        {
          "description": "Denies the is_always_on_top command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-is-always-on-top",
          "markdownDescription": "Denies the is_always_on_top command without any pre-configured scope."
        },
        {
          "description": "Denies the is_closable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-is-closable",
          "markdownDescription": "Denies the is_closable command without any pre-configured scope."
        },
        {
          "description": "Denies the is_decorated command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-is-decorated",
          "markdownDescription": "Denies the is_decorated command without any pre-configured scope."
        },
        {
          "description": "Denies the is_enabled command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-is-enabled",
          "markdownDescription": "Denies the is_enabled command without any pre-configured scope."
        },
        {
          "description": "Denies the is_focused command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-is-focused",
          "markdownDescription": "Denies the is_focused command without any pre-configured scope."
        },
        {
          "description": "Denies the is_fullscreen command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-is-fullscreen",
          "markdownDescription": "Denies the is_fullscreen command without any pre-configured scope."
        },
        {
          "description": "Denies the is_maximizable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-is-maximizable",
          "markdownDescription": "Denies the is_maximizable command without any pre-configured scope."
        },
        {
          "description": "Denies the is_maximized command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-is-maximized",
          "markdownDescription": "Denies the is_maximized command without any pre-configured scope."
        },
        {
          "description": "Denies the is_minimizable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-is-minimizable",
          "markdownDescription": "Denies the is_minimizable command without any pre-configured scope."
        },
        {
          "description": "Denies the is_minimized command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-is-minimized",
          "markdownDescription": "Denies the is_minimized command without any pre-configured scope."
        },
        {
          "description": "Denies the is_resizable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-is-resizable",
          "markdownDescription": "Denies the is_resizable command without any pre-configured scope."
        },
        {
          "description": "Denies the is_visible command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-is-visible",
          "markdownDescription": "Denies the is_visible command without any pre-configured scope."
        },
        {
          "description": "Denies the maximize command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-maximize",
          "markdownDescription": "Denies the maximize command without any pre-configured scope."
        },
        {
          "description": "Denies the minimize command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-minimize",
          "markdownDescription": "Denies the minimize command without any pre-configured scope."
        },
        {
          "description": "Denies the monitor_from_point command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-monitor-from-point",
          "markdownDescription": "Denies the monitor_from_point command without any pre-configured scope."
        },
        {
          "description": "Denies the outer_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-outer-position",
          "markdownDescription": "Denies the outer_position command without any pre-configured scope."
        },
        {
          "description": "Denies the outer_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-outer-size",
          "markdownDescription": "Denies the outer_size command without any pre-configured scope."
        },
        {
          "description": "Denies the primary_monitor command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-primary-monitor",
          "markdownDescription": "Denies the primary_monitor command without any pre-configured scope."
        },
        {
          "description": "Denies the request_user_attention command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-request-user-attention",
          "markdownDescription": "Denies the request_user_attention command without any pre-configured scope."
        },
        {
          "description": "Denies the scale_factor command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-scale-factor",
          "markdownDescription": "Denies the scale_factor command without any pre-configured scope."
        },
        {
          "description": "Denies the scene_identifier command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-scene-identifier",
          "markdownDescription": "Denies the scene_identifier command without any pre-configured scope."
        },
        {
          "description": "Denies the set_always_on_bottom command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-always-on-bottom",
          "markdownDescription": "Denies the set_always_on_bottom command without any pre-configured scope."
        },
        {
          "description": "Denies the set_always_on_top command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-always-on-top",
          "markdownDescription": "Denies the set_always_on_top command without any pre-configured scope."
        },
        {
          "description": "Denies the set_background_color command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-background-color",
          "markdownDescription": "Denies the set_background_color command without any pre-configured scope."
        },
        {
          "description": "Denies the set_badge_count command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-badge-count",
          "markdownDescription": "Denies the set_badge_count command without any pre-configured scope."
        },
        {
          "description": "Denies the set_badge_label command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-badge-label",
          "markdownDescription": "Denies the set_badge_label command without any pre-configured scope."
        },
        {
          "description": "Denies the set_closable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-closable",
          "markdownDescription": "Denies the set_closable command without any pre-configured scope."
        },
        {
          "description": "Denies the set_content_protected command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-content-protected",
          "markdownDescription": "Denies the set_content_protected command without any pre-configured scope."
        },
        {
          "description": "Denies the set_cursor_grab command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-cursor-grab",
          "markdownDescription": "Denies the set_cursor_grab command without any pre-configured scope."
        },
        {
          "description": "Denies the set_cursor_icon command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-cursor-icon",
          "markdownDescription": "Denies the set_cursor_icon command without any pre-configured scope."
        },
        {
          "description": "Denies the set_cursor_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-cursor-position",
          "markdownDescription": "Denies the set_cursor_position command without any pre-configured scope."
        },
        {
          "description": "Denies the set_cursor_visible command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-cursor-visible",
          "markdownDescription": "Denies the set_cursor_visible command without any pre-configured scope."
        },
        {
          "description": "Denies the set_decorations command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-decorations",
          "markdownDescription": "Denies the set_decorations command without any pre-configured scope."
        },
        {
          "description": "Denies the set_effects command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-effects",
          "markdownDescription": "Denies the set_effects command without any pre-configured scope."
        },
        {
          "description": "Denies the set_enabled command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-enabled",
          "markdownDescription": "Denies the set_enabled command without any pre-configured scope."
        },
        {
          "description": "Denies the set_focus command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-focus",
          "markdownDescription": "Denies the set_focus command without any pre-configured scope."
        },
        {
          "description": "Denies the set_focusable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-focusable",
          "markdownDescription": "Denies the set_focusable command without any pre-configured scope."
        },
        {
          "description": "Denies the set_fullscreen command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-fullscreen",
          "markdownDescription": "Denies the set_fullscreen command without any pre-configured scope."
        },
        {
          "description": "Denies the set_fullscreen_on_monitor command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-fullscreen-on-monitor",
          "markdownDescription": "Denies the set_fullscreen_on_monitor command without any pre-configured scope."
        },
        {
          "description": "Denies the set_icon command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-icon",
          "markdownDescription": "Denies the set_icon command without any pre-configured scope."
        },
        {
          "description": "Denies the set_ignore_cursor_events command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-ignore-cursor-events",
          "markdownDescription": "Denies the set_ignore_cursor_events command without any pre-configured scope."
        },
        {
          "description": "Denies the set_max_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-max-size",
          "markdownDescription": "Denies the set_max_size command without any pre-configured scope."
        },
        {
          "description": "Denies the set_maximizable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-maximizable",
          "markdownDescription": "Denies the set_maximizable command without any pre-configured scope."
        },
        {
          "description": "Denies the set_min_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-min-size",
          "markdownDescription": "Denies the set_min_size command without any pre-configured scope."
        },
        {
          "description": "Denies the set_minimizable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-minimizable",
          "markdownDescription": "Denies the set_minimizable command without any pre-configured scope."
        },
        {
          "description": "Denies the set_overlay_icon command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-overlay-icon",
          "markdownDescription": "Denies the set_overlay_icon command without any pre-configured scope."
        },
        {
          "description": "Denies the set_position command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-position",
          "markdownDescription": "Denies the set_position command without any pre-configured scope."
        },
        {
          "description": "Denies the set_progress_bar command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-progress-bar",
          "markdownDescription": "Denies the set_progress_bar command without any pre-configured scope."
        },
        {
          "description": "Denies the set_resizable command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-resizable",
          "markdownDescription": "Denies the set_resizable command without any pre-configured scope."
        },
        {
          "description": "Denies the set_shadow command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-shadow",
          "markdownDescription": "Denies the set_shadow command without any pre-configured scope."
        },
        {
          "description": "Denies the set_simple_fullscreen command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-simple-fullscreen",
          "markdownDescription": "Denies the set_simple_fullscreen command without any pre-configured scope."
        },
        {
          "description": "Denies the set_size command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-size",
          "markdownDescription": "Denies the set_size command without any pre-configured scope."
        },
        {
          "description": "Denies the set_size_constraints command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-size-constraints",
          "markdownDescription": "Denies the set_size_constraints command without any pre-configured scope."
        },
        {
          "description": "Denies the set_skip_taskbar command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-skip-taskbar",
          "markdownDescription": "Denies the set_skip_taskbar command without any pre-configured scope."
        },
        {
          "description": "Denies the set_theme command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-theme",
          "markdownDescription": "Denies the set_theme command without any pre-configured scope."
        },
        {
          "description": "Denies the set_title command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-title",
          "markdownDescription": "Denies the set_title command without any pre-configured scope."
        },
        {
          "description": "Denies the set_title_bar_style command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-title-bar-style",
          "markdownDescription": "Denies the set_title_bar_style command without any pre-configured scope."
        },
        {
          "description": "Denies the set_visible_on_all_workspaces command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-set-visible-on-all-workspaces",
          "markdownDescription": "Denies the set_visible_on_all_workspaces command without any pre-configured scope."
        },
        {
          "description": "Denies the show command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-show",
          "markdownDescription": "Denies the show command without any pre-configured scope."
        },
        {
          "description": "Denies the start_dragging command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-start-dragging",
          "markdownDescription": "Denies the start_dragging command without any pre-configured scope."
        },
        {
          "description": "Denies the start_resize_dragging command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-start-resize-dragging",
          "markdownDescription": "Denies the start_resize_dragging command without any pre-configured scope."
        },
        {
          "description": "Denies the theme command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-theme",
          "markdownDescription": "Denies the theme command without any pre-configured scope."
        },
        {
          "description": "Denies the title command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-title",
          "markdownDescription": "Denies the title command without any pre-configured scope."
        },
        {
          "description": "Denies the toggle_maximize command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-toggle-maximize",
          "markdownDescription": "Denies the toggle_maximize command without any pre-configured scope."
        },
        {
          "description": "Denies the unmaximize command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-unmaximize",
          "markdownDescription": "Denies the unmaximize command without any pre-configured scope."
        },
        {
          "description": "Denies the unminimize command without any pre-configured scope.",
          "type": "string",
          "const": "core:window:deny-unminimize",
          "markdownDescription": "Denies the unminimize command without any pre-configured scope."
        },
        {
          "description": "This permission set configures the types of dialogs\navailable from the dialog plugin.\n\n#### Granted Permissions\n\nAll dialog types are enabled.\n\n\n\n#### This default permission set includes:\n\n- `allow-message`\n- `allow-save`\n- `allow-open`",
          "type": "string",
          "const": "dialog:default",
          "markdownDescription": "This permission set configures the types of dialogs\navailable from the dialog plugin.\n\n#### Granted Permissions\n\nAll dialog types are enabled.\n\n\n\n#### This default permission set includes:\n\n- `allow-message`\n- `allow-save`\n- `allow-open`"
        },
        {
          "description": "Enables the ask command without any pre-configured scope. (**DEPRECATED**: This is now an alias to `allow-message` and will be removed in v3)",
          "type": "string",
          "const": "dialog:allow-ask",
          "markdownDescription": "Enables the ask command without any pre-configured scope. (**DEPRECATED**: This is now an alias to `allow-message` and will be removed in v3)"
        },
        {
          "description": "Enables the confirm command without any pre-configured scope. (**DEPRECATED**: This is now an alias to `allow-message` and will be removed in v3)",
          "type": "string",
          "const": "dialog:allow-confirm",
          "markdownDescription": "Enables the confirm command without any pre-configured scope. (**DEPRECATED**: This is now an alias to `allow-message` and will be removed in v3)"
        },
        {
          "description": "Enables the message command without any pre-configured scope.",
          "type": "string",
          "const": "dialog:allow-message",
          "markdownDescription": "Enables the message command without any pre-configured scope."
        },
        {
          "description": "Enables the open command without any pre-configured scope.",
          "type": "string",
          "const": "dialog:allow-open",
          "markdownDescription": "Enables the open command without any pre-configured scope."
        },
        {
          "description": "Enables the save command without any pre-configured scope.",
          "type": "string",
          "const": "dialog:allow-save",
          "markdownDescription": "Enables the save command without any pre-configured scope."
        },
        {
          "description": "Denies the ask command without any pre-configured scope. (**DEPRECATED**: This is now an alias to `deny-message` and will be removed in v3)",
          "type": "string",
          "const": "dialog:deny-ask",
          "markdownDescription": "Denies the ask command without any pre-configured scope. (**DEPRECATED**: This is now an alias to `deny-message` and will be removed in v3)"
        },
        {
          "description": "Denies the confirm command without any pre-configured scope. (**DEPRECATED**: This is now an alias to `deny-message` and will be removed in v3)",
          "type": "string",
          "const": "dialog:deny-confirm",
          "markdownDescription": "Denies the confirm command without any pre-configured scope. (**DEPRECATED**: This is now an alias to `deny-message` and will be removed in v3)"
        },
        {
          "description": "Denies the message command without any pre-configured scope.",
          "type": "string",
          "const": "dialog:deny-message",
          "markdownDescription": "Denies the message command without any pre-configured scope."
        },
        {
          "description": "Denies the open command without any pre-configured scope.",
          "type": "string",
          "const": "dialog:deny-open",
          "markdownDescription": "Denies the open command without any pre-configured scope."
        },
        {
          "description": "Denies the save command without any pre-configured scope.",
          "type": "string",
          "const": "dialog:deny-save",
          "markdownDescription": "Denies the save command without any pre-configured scope."
        }
      ]
    },
    "Value": {
      "description": "All supported ACL values.",
      "anyOf": [
        {
          "description": "Represents a null JSON value.",
          "type": "null"
        },
        {
          "description": "Represents a [`bool`].",
          "type": "boolean"
        },
        {
          "description": "Represents a valid ACL [`Number`].",
          "allOf": [
            {
              "$ref": "#/definitions/Number"
            }
          ]
        },
        {
          "description": "Represents a [`String`].",
          "type": "string"
        },
        {
          "description": "Represents a list of other [`Value`]s.",
          "type": "array",
          "items": {
            "$ref": "#/definitions/Value"
          }
        },
        {
          "description": "Represents a map of [`String`] keys to [`Value`]s.",
          "type": "object",
          "additionalProperties": {
            "$ref": "#/definitions/Value"
          }
        }
      ]
    },
    "Number": {
      "description": "A valid ACL number.",
      "anyOf": [
        {
          "description": "Represents an [`i64`].",
          "type": "integer",
          "format": "int64"
        },
        {
          "description": "Represents a [`f64`].",
          "type": "number",
          "format": "double"
        }
      ]
    },
    "Target": {
      "description": "Platform target.",
      "oneOf": [
        {
          "description": "MacOS.",
          "type": "string",
          "enum": [
            "macOS"
          ]
        },
        {
          "description": "Windows.",
          "type": "string",
          "enum": [
            "windows"
          ]
        },
        {
          "description": "Linux.",
          "type": "string",
          "enum": [
            "linux"
          ]
        },
        {
          "description": "Android.",
          "type": "string",
          "enum": [
            "android"
          ]
        },
        {
          "description": "iOS.",
          "type": "string",
          "enum": [
            "iOS"
          ]
        }
      ]
    }
  }
}
````

## File: app/src-tauri/src/apps.rs
````rust
use base64::Engine;
use std::os::windows::process::CommandExt;
use std::process::Command;
use windows::core::PCWSTR;
⋮----
use windows::Win32::UI::Shell::ExtractIconExW;
⋮----
pub struct AppLauncher;
⋮----
impl AppLauncher {
pub fn launch(target: &str) -> Result<(), String> {
if target.is_empty() {
return Ok(());
⋮----
if target.contains(':') && !target.contains('\\') && !target.contains('/') {
⋮----
.args(["/c", "start", "", target])
.creation_flags(CREATE_NO_WINDOW)
.spawn();
⋮----
Ok(())
⋮----
pub fn get_app_entries(slots: &[(&str, &str)]) -> Vec<AppEntry> {
⋮----
.iter()
.map(|(slot, path)| {
let name = get_app_name(path);
let icon = extract_icon(path);
⋮----
slot: slot.to_string(),
path: path.to_string(),
⋮----
.collect()
⋮----
fn get_app_name(path: &str) -> String {
if path.starts_with("ms-settings:") {
return "Configuración".into();
⋮----
if let Some(stem) = p.file_stem() {
return stem.to_string_lossy().to_string();
⋮----
path.to_string()
⋮----
fn extract_icon(path: &str) -> Option<IconData> {
if !path.ends_with(".exe") && !path.ends_with(".ico") {
⋮----
let wide: Vec<u16> = path.encode_utf16().chain(std::iter::once(0)).collect();
⋮----
let num_extracted = ExtractIconExW(PCWSTR(wide.as_ptr()), 0, Some(&mut large_icon), None, 1);
⋮----
if num_extracted == 0 || large_icon.0.is_null() {
⋮----
if GetIconInfo(large_icon, &mut icon_info).is_err() {
let _ = DestroyIcon(large_icon);
⋮----
let hdc = CreateCompatibleDC(Some(HDC::default()));
⋮----
GetObjectW(
HGDIOBJ(icon_info.hbmColor.0),
⋮----
Some(&mut bm as *mut _ as *mut _),
⋮----
let _ = DeleteDC(hdc);
let _ = DeleteObject(HGDIOBJ(icon_info.hbmColor.0));
let _ = DeleteObject(HGDIOBJ(icon_info.hbmMask.0));
⋮----
let mut pixels = vec![0u8; (width * height * 4) as usize];
GetDIBits(
⋮----
Some(pixels.as_mut_ptr() as *mut _),
⋮----
for chunk in pixels.chunks_exact_mut(4) {
chunk.swap(0, 2);
⋮----
let b64 = base64::engine::general_purpose::STANDARD.encode(&png_bytes);
⋮----
Some(IconData {
````

## File: app/src-tauri/src/battery.rs
````rust
use crate::types::BatteryState;
⋮----
use std::sync::Arc;
use std::time::Duration;
⋮----
pub fn get_battery_status() -> Option<BatteryState> {
⋮----
if GetSystemPowerStatus(&mut status).is_ok() {
⋮----
status.BatteryLifePercent.min(100) as i32
⋮----
let low = (0..=20).contains(&percent);
⋮----
return Some(BatteryState {
⋮----
pub fn start_battery_worker(app: AppHandle, running: Arc<AtomicBool>) {
⋮----
while running.load(Ordering::Relaxed) {
if let Some(state) = get_battery_status() {
⋮----
last_state = Some(state.clone());
let _ = app.emit("ln:battery", state);
````

## File: app/src-tauri/src/hardware.rs
````rust
use crate::types::HardwareState;
⋮----
use std::sync::Arc;
use std::time::Duration;
⋮----
use windows::core::PCWSTR;
⋮----
pub struct HardwareMonitor {
⋮----
unsafe impl Send for HardwareMonitor {}
unsafe impl Sync for HardwareMonitor {}
⋮----
impl HardwareMonitor {
pub fn new() -> Option<Self> {
⋮----
if PdhOpenQueryW(PCWSTR::null(), 0, &mut query) != 0 {
⋮----
.encode_utf16()
.collect();
if PdhAddEnglishCounterW(query, PCWSTR(cpu_path.as_ptr()), 0, &mut cpu_counter) != 0 {
let _ = PdhCloseQuery(query);
⋮----
let has_gpu = PdhAddEnglishCounterW(query, PCWSTR(gpu_path.as_ptr()), 0, &mut gpu_counter) == 0;
⋮----
let _ = PdhCollectQueryData(query);
⋮----
Some(Self {
⋮----
gpu_counter: if has_gpu { Some(gpu_counter) } else { None },
⋮----
pub fn read(&self) -> HardwareState {
⋮----
let _ = PdhCollectQueryData(self.query);
⋮----
if PdhGetFormattedCounterValue(self.cpu_counter, PDH_FMT_DOUBLE, None, &mut cpu_val) == 0 {
cpu_load = cpu_val.Anonymous.doubleValue.clamp(0.0, 100.0);
⋮----
if PdhGetFormattedCounterValue(gpu_cnt, PDH_FMT_DOUBLE, None, &mut gpu_val) == 0 {
gpu_load = gpu_val.Anonymous.doubleValue.clamp(0.0, 100.0);
⋮----
cpu: format!("{:.0}%", cpu_load.round()),
gpu: format!("{:.0}%", gpu_load.round()),
cpu_value: Some(cpu_load),
gpu_value: Some(gpu_load),
cpu_kind: "load".into(),
gpu_kind: "load".into(),
cpu_source: "PDH Win32".into(),
gpu_source: "PDH Win32".into(),
⋮----
impl Drop for HardwareMonitor {
fn drop(&mut self) {
⋮----
let _ = PdhCloseQuery(self.query);
⋮----
pub fn start_hardware_worker(
⋮----
let interval = Duration::from_secs(refresh_seconds.max(1) as u64);
⋮----
while running.load(Ordering::Relaxed) {
⋮----
let state = monitor.read();
let _ = app.emit("ln:hardware", state);
````

## File: app/src-tauri/src/lib.rs
````rust
mod apps;
mod battery;
mod downloads;
mod hardware;
mod media;
mod native_host;
mod notifications;
mod radios;
mod settings;
mod types;
mod usb;
mod weather;
mod window;
⋮----
pub struct AppState {
⋮----
async fn get_snapshot(state: State<'_, AppState>) -> Result<Snapshot, String> {
let settings = state.settings.lock().unwrap().load();
⋮----
let downloads = state.downloads.get_active_downloads();
⋮----
Ok(Snapshot {
⋮----
fn set_hit_rect(rect: HitRect, state: State<'_, AppState>) -> Result<(), String> {
let mgr_lock = state.window_mgr.lock().unwrap();
⋮----
mgr.update_hit_rect(rect);
⋮----
Ok(())
⋮----
fn set_view_hint(_expanded: bool, _page: String) -> Result<(), String> {
⋮----
async fn media_control(action: String, state: State<'_, AppState>) -> Result<(), String> {
match action.as_str() {
"play" => state.media.play().await,
"pause" => state.media.pause().await,
"next" => state.media.next().await,
"previous" => state.media.previous().await,
_ => Ok(()),
⋮----
fn launch_app(slot: String, state: State<'_, AppState>) -> Result<(), String> {
let s = state.settings.lock().unwrap().load();
let target = match slot.as_str() {
⋮----
other => other.to_string(),
⋮----
fn get_app_entries(state: State<'_, AppState>) -> Result<Vec<AppEntry>, String> {
⋮----
("1", s.app_launcher1.as_str()),
("2", s.app_launcher2.as_str()),
("3", s.app_launcher3.as_str()),
("4", s.app_launcher4.as_str()),
("5", s.app_launcher5.as_str()),
⋮----
Ok(apps::AppLauncher::get_app_entries(&slots))
⋮----
async fn set_wifi(enabled: bool) -> Result<bool, String> {
Ok(radios::RadioManager::set_wifi(enabled).await)
⋮----
async fn set_bluetooth(enabled: bool) -> Result<bool, String> {
Ok(radios::RadioManager::set_bluetooth(enabled).await)
⋮----
fn set_dark_mode(enabled: bool) -> Result<bool, String> {
Ok(radios::RadioManager::set_dark_mode(enabled))
⋮----
fn set_dnd(enabled: bool) -> Result<bool, String> {
Ok(radios::RadioManager::set_dnd(enabled))
⋮----
fn get_settings(state: State<'_, AppState>) -> Result<Settings, String> {
Ok(state.settings.lock().unwrap().load())
⋮----
fn save_settings(settings: Settings, state: State<'_, AppState>, app: AppHandle) -> Result<Settings, String> {
state.settings.lock().unwrap().save(&settings)?;
state.hide_fullscreen_setting.store(settings.hide_in_fullscreen, Ordering::Relaxed);
let _ = app.emit("ln:settings", &settings);
Ok(settings)
⋮----
async fn detect_location_ip(state: State<'_, AppState>) -> Result<(f64, f64), String> {
⋮----
.map_err(|e| e.to_string())?;
let http = windows::Web::Http::HttpClient::new().map_err(|e| e.to_string())?;
let resp = http.GetStringAsync(&uri).map_err(|e| e.to_string())?.get().map_err(|e| e.to_string())?;
let json: serde_json::Value = serde_json::from_str(&resp.to_string()).map_err(|e| e.to_string())?;
⋮----
let lat = json.get("lat").and_then(|v| v.as_f64()).ok_or("Sin latitud")?;
let lon = json.get("lon").and_then(|v| v.as_f64()).ok_or("Sin longitud")?;
⋮----
let mut current = state.settings.lock().unwrap().load();
⋮----
let _ = state.settings.lock().unwrap().save(&current);
⋮----
Ok((lat, lon))
⋮----
async fn request_extension_location(state: State<'_, AppState>) -> Result<bool, String> {
Ok(state.downloads.request_location_update().await)
⋮----
fn get_integration_status(state: State<'_, AppState>) -> Result<IntegrationStatus, String> {
⋮----
let extension_connected = state.downloads.has_active_downloads();
let notification_access = state.notifications.access_status();
⋮----
.map(|p| p.to_string_lossy().to_string())
.unwrap_or_default();
let settings_path = state.settings.lock().unwrap().get_path();
⋮----
Ok(IntegrationStatus {
⋮----
cpu_source: "Win32 PDH".into(),
gpu_source: "Win32 PDH".into(),
version: "2.0.0".into(),
⋮----
fn register_native_host() -> Result<bool, String> {
⋮----
fn open_settings(app: AppHandle) -> Result<(), String> {
if let Some(win) = app.get_webview_window("settings") {
let _ = win.show();
let _ = win.set_focus();
return Ok(());
⋮----
let _ = WebviewWindowBuilder::new(&app, "settings", WebviewUrl::App("settings.html".into()))
.title("Configuración de LockNotch")
.inner_size(680.0, 520.0)
.resizable(false)
.center()
.decorations(false)
.build();
⋮----
fn open_external(target: String) -> Result<(), String> {
⋮----
fn quit_app(app: AppHandle, state: State<'_, AppState>) {
state.running.store(false, Ordering::Relaxed);
app.exit(0);
⋮----
pub fn run() {
⋮----
.plugin(tauri_plugin_dialog::init())
.setup(|app| {
⋮----
let initial_settings = settings_mgr.lock().unwrap().load();
⋮----
let island_win = app.get_webview_window("island").expect("island window missing");
⋮----
Ok(m) => Arc::new(Mutex::new(Some(m))),
⋮----
let _ = island_win.show();
⋮----
let app_handle = app.handle().clone();
let downloads_clone = downloads.clone();
let running_clone = running.clone();
downloads::start_download_pipe_server(app_handle.clone(), downloads_clone, running_clone);
⋮----
tokio::runtime::Handle::current().block_on(media::MediaService::new())
⋮----
media_service.start_tracking(app_handle.clone());
⋮----
tokio::runtime::Handle::current().block_on(notifications::NotificationService::new())
⋮----
let _ = notif_service.start_listening(app_handle.clone());
⋮----
battery::start_battery_worker(app_handle.clone(), running.clone());
⋮----
app_handle.clone(),
running.clone(),
⋮----
usb::start_usb_monitor(app_handle.clone(), running.clone());
window::start_fullscreen_detector(app_handle.clone(), hide_fullscreen.clone());
⋮----
let weather_service = Arc::new(weather::WeatherService::new().unwrap());
⋮----
let win_mgr_click = win_mgr.clone();
let running_click = running.clone();
⋮----
use windows::Win32::UI::WindowsAndMessaging::GetCursorPos;
use windows::Win32::Foundation::POINT;
⋮----
while running_click.load(Ordering::Relaxed) {
⋮----
if GetCursorPos(&mut pt).is_ok() {
let mgr_lock = win_mgr_click.lock().unwrap();
⋮----
let hovered = mgr.check_cursor_hover(pt);
⋮----
mgr.set_click_through(!hovered);
⋮----
let app_handle_tray = app.handle().clone();
⋮----
.icon(app.default_window_icon().unwrap().clone())
.menu(&menu)
.show_menu_on_left_click(false)
.on_menu_event(move |_app, event| match event.id.as_ref() {
⋮----
let _ = open_settings(app_handle_tray.clone());
⋮----
app_handle_tray.exit(0);
⋮----
.on_tray_icon_event(|tray, event| {
⋮----
let _ = open_settings(tray.app_handle().clone());
⋮----
.build(app)?;
⋮----
app.manage(AppState {
⋮----
.invoke_handler(tauri::generate_handler![
⋮----
.run(tauri::generate_context!())
.expect("error while running tauri application");
````

## File: app/src-tauri/src/main.rs
````rust
fn main() {
⋮----
let _ = std::fs::write("panic.txt", format!("{}", info));
⋮----
let _ = std::fs::OpenOptions::new().create(true).append(true).open("run_log.txt")
.and_then(|mut f| { use std::io::Write; writeln!(f, "{}", s) });
⋮----
log("main started");
let rt = tokio::runtime::Runtime::new().expect("Failed to create Tokio runtime");
let _guard = rt.enter();
log("calling locknotch_lib::run");
⋮----
log("locknotch_lib::run returned");
````

## File: app/src-tauri/src/media.rs
````rust
use base64::Engine;
⋮----
use windows::Foundation::TypedEventHandler;
⋮----
pub struct MediaService {
⋮----
impl MediaService {
pub async fn new() -> Self {
⋮----
op.get().ok()
⋮----
pub fn start_tracking(&self, app: AppHandle) {
⋮----
let current_session_arc = self.current_session.clone();
let last_track_arc = self.last_track_key.clone();
let cache_thumb_arc = self.cached_thumbnail.clone();
let app_clone = app.clone();
⋮----
*current_session_arc.lock().unwrap() = Some(sess.clone());
let sess_clone = sess.clone();
let app_c = app.clone();
let last_c = last_track_arc.clone();
let thumb_c = cache_thumb_arc.clone();
⋮----
let app_for_event = app_clone.clone();
let sess_for_event = current_session_arc.clone();
let last_for_event = last_track_arc.clone();
let thumb_for_event = cache_thumb_arc.clone();
⋮----
if let Some(m) = mgr.as_ref() {
⋮----
*sess_for_event.lock().unwrap() = best.clone();
⋮----
let s = sess.clone();
let a = app_for_event.clone();
let l = last_for_event.clone();
let t = thumb_for_event.clone();
⋮----
let _ = app_for_event.emit("ln:media", MediaEvent {
⋮----
Ok(())
⋮----
let _ = manager.CurrentSessionChanged(&handler);
⋮----
fn select_best_session(
⋮----
let sessions = match manager.GetSessions() {
⋮----
Err(_) => return manager.GetCurrentSession().ok(),
⋮----
let count = sessions.Size().unwrap_or(0);
⋮----
return manager.GetCurrentSession().ok();
⋮----
if let Ok(sess) = sessions.GetAt(i) {
if let Ok(info) = sess.GetPlaybackInfo() {
if let Ok(status) = info.PlaybackStatus() {
⋮----
playing_sessions.push(sess);
⋮----
paused_sessions.push(sess);
⋮----
if let Ok(id) = sess.SourceAppUserModelId() {
let id_lower = id.to_string().to_lowercase();
if known_apps.iter().any(|k| id_lower.contains(k)) {
return Some(sess.clone());
⋮----
if let Some(first_playing) = playing_sessions.into_iter().next() {
return Some(first_playing);
⋮----
manager.GetCurrentSession().ok()
⋮----
pub async fn refresh_session(
⋮----
let props_op = match session.TryGetMediaPropertiesAsync() {
⋮----
let props = match props_op.get() {
⋮----
let title = props.Title().map(|s| s.to_string()).unwrap_or_default();
let artist = props.Artist().map(|s| s.to_string()).unwrap_or_default();
let album = props.AlbumTitle().map(|s| s.to_string()).unwrap_or_default();
⋮----
.SourceAppUserModelId()
.map(|s| s.to_string())
.unwrap_or_default();
⋮----
if title.is_empty() {
⋮----
.GetPlaybackInfo()
.and_then(|info| info.PlaybackStatus())
.map(|s| s == GlobalSystemMediaTransportControlsSessionPlaybackStatus::Playing)
.unwrap_or(false);
⋮----
let timeline = session.GetTimelineProperties().ok();
⋮----
.as_ref()
.and_then(|t| t.Position().ok())
.map(|d| d.Duration / 10000)
.unwrap_or(0);
⋮----
.and_then(|t| t.EndTime().ok())
⋮----
let track_key = format!("{}|{}|{}", source_app, title, artist);
⋮----
let mut last = last_track.lock().unwrap();
⋮----
*last = track_key.clone();
⋮----
let mut thumbnail = cache_thumb.lock().unwrap().clone();
if track_changed || thumbnail.is_none() {
if let Ok(thumb_ref) = props.Thumbnail() {
if let Ok(stream_op) = thumb_ref.OpenReadAsync() {
if let Ok(stream) = stream_op.get() {
if let Ok(size) = stream.Size() {
⋮----
if let Ok(load_op) = reader.LoadAsync(size as u32) {
if load_op.get().is_ok() {
let mut bytes = vec![0u8; size as usize];
if reader.ReadBytes(&mut bytes).is_ok() {
let b64 = base64::engine::general_purpose::STANDARD.encode(&bytes);
let data_url = format!("data:image/jpeg;base64,{}", b64);
thumbnail = Some(data_url.clone());
*cache_thumb.lock().unwrap() = Some(data_url);
⋮----
last_updated_ms: chrono::Utc::now().timestamp_millis(),
track_key: track_key.clone(),
art_key: Some(track_key),
⋮----
let _ = app.emit("ln:media", MediaEvent {
media: Some(media_state),
⋮----
pub async fn play(&self) -> Result<(), String> {
let sess_opt = self.current_session.lock().unwrap().clone();
⋮----
let op = sess.TryPlayAsync().map_err(|e| e.to_string())?;
let _ = op.get();
⋮----
pub async fn pause(&self) -> Result<(), String> {
⋮----
let op = sess.TryPauseAsync().map_err(|e| e.to_string())?;
⋮----
pub async fn next(&self) -> Result<(), String> {
⋮----
let op = sess.TrySkipNextAsync().map_err(|e| e.to_string())?;
⋮----
pub async fn previous(&self) -> Result<(), String> {
⋮----
let op = sess.TrySkipPreviousAsync().map_err(|e| e.to_string())?;
````

## File: app/src-tauri/src/native_host.rs
````rust
use std::path::PathBuf;
⋮----
use winreg::RegKey;
⋮----
pub fn is_native_host_call() -> bool {
std::env::args().any(|arg| arg.starts_with("chrome-extension://"))
⋮----
pub fn run_native_host_proxy() {
⋮----
.enable_all()
.build()
⋮----
rt.block_on(async {
⋮----
use tokio::net::windows::named_pipe::ClientOptions;
⋮----
let client = match ClientOptions::new().open(PIPE_NAME) {
⋮----
while pipe_read.read_exact(&mut len_buf).await.is_ok() {
⋮----
let mut data = vec![0u8; len];
if pipe_read.read_exact(&mut data).await.is_ok() {
let _ = stdout.write_all(&len_buf);
let _ = stdout.write_all(&data);
let _ = stdout.flush();
⋮----
while stdin.read_exact(&mut len_buf).is_ok() {
⋮----
if stdin.read_exact(&mut data).is_ok() {
if pipe_write.write_all(&len_buf).await.is_err() {
⋮----
if pipe_write.write_all(&data).await.is_err() {
⋮----
let _ = pipe_write.flush().await;
⋮----
pub fn register_native_host() -> Result<bool, String> {
let current_exe = std::env::current_exe().map_err(|e| e.to_string())?;
⋮----
.parent()
.unwrap_or_else(|| std::path::Path::new("C:\\"));
let manifest_path = manifest_dir.join(format!("{}.json", HOST_NAME));
⋮----
let json_str = serde_json::to_string_pretty(&manifest_content).map_err(|e| e.to_string())?;
std::fs::write(&manifest_path, json_str).map_err(|e| e.to_string())?;
⋮----
let key_path = format!(r"Software\Google\Chrome\NativeMessagingHosts\{}", HOST_NAME);
let (key, _) = hkcu.create_subkey(&key_path).map_err(|e| e.to_string())?;
key.set_value("", &manifest_path.to_string_lossy().to_string())
.map_err(|e| e.to_string())?;
⋮----
Ok(true)
⋮----
pub fn is_native_host_registered() -> bool {
⋮----
if let Ok(key) = hkcu.open_subkey(&key_path) {
⋮----
return PathBuf::from(path).exists();
````

## File: app/src-tauri/src/radios.rs
````rust
use crate::types::Toggles;
use std::os::windows::process::CommandExt;
use std::process::Command;
⋮----
use winreg::RegKey;
⋮----
pub struct RadioManager;
⋮----
impl RadioManager {
pub async fn get_initial_toggles() -> Toggles {
⋮----
async fn query_radios() -> (bool, bool) {
⋮----
if let Ok(radios) = op.get() {
if let Ok(count) = radios.Size() {
⋮----
if let Ok(radio) = radios.GetAt(i) {
if let (Ok(kind), Ok(state)) = (radio.Kind(), radio.State()) {
⋮----
pub async fn set_wifi(enabled: bool) -> bool {
⋮----
if let Ok(kind) = radio.Kind() {
⋮----
if let Ok(set_op) = radio.SetStateAsync(target_state) {
let _ = set_op.get();
⋮----
.args(["interface", "set", "interface", "Wi-Fi", state_str])
.creation_flags(CREATE_NO_WINDOW)
.status();
⋮----
pub async fn set_bluetooth(enabled: bool) -> bool {
⋮----
.args(["-NoProfile", "-WindowStyle", "Hidden", "-Command", cmd])
⋮----
pub fn set_dark_mode(enabled: bool) -> bool {
⋮----
if let Ok(key) = hkcu.open_subkey_with_flags(path, KEY_SET_VALUE) {
⋮----
let _ = key.set_value("AppsUseLightTheme", &val);
let _ = key.set_value("SystemUsesLightTheme", &val);
⋮----
pub fn is_dark_mode_enabled() -> bool {
⋮----
if let Ok(key) = hkcu.open_subkey(path) {
⋮----
pub fn set_dnd(enabled: bool) -> bool {
⋮----
if let Ok((key, _)) = hkcu.create_subkey(path) {
⋮----
let _ = key.set_value("EnableQuietHours", &val);
⋮----
pub fn is_dnd_enabled() -> bool {
````

## File: app/src-tauri/src/settings.rs
````rust
use crate::types::Settings;
use std::fs;
use std::path::PathBuf;
⋮----
use winreg::RegKey;
⋮----
pub struct SettingsManager {
⋮----
impl SettingsManager {
pub fn new() -> Self {
let app_data = std::env::var("APPDATA").unwrap_or_else(|_| "C:\\".into());
let path = PathBuf::from(app_data).join("LockNotch").join("settings.json");
⋮----
pub fn load(&self) -> Settings {
if self.path.exists() {
⋮----
let _ = self.save(&default_settings);
⋮----
pub fn save(&self, settings: &Settings) -> Result<(), String> {
if let Some(parent) = self.path.parent() {
⋮----
.map_err(|e| format!("Serialization error: {}", e))?;
fs::write(&self.path, json).map_err(|e| format!("Write error: {}", e))?;
⋮----
Ok(())
⋮----
pub fn get_path(&self) -> String {
self.path.to_string_lossy().to_string()
⋮----
pub fn apply_startup_registry(enable: bool) -> Result<(), String> {
⋮----
let (key, _) = hkcu.create_subkey(path).map_err(|e| e.to_string())?;
⋮----
let val = format!("\"{}\"", exe_path.to_string_lossy());
let _ = key.set_value("LockNotch", &val);
⋮----
let _ = key.delete_value("LockNotch");
````

## File: app/src-tauri/src/types.rs
````rust
pub struct MediaState {
⋮----
pub struct MediaEvent {
⋮----
pub struct BatteryState {
⋮----
pub struct WeatherState {
⋮----
pub struct HardwareState {
⋮----
pub struct UsbEvent {
⋮----
pub struct WinNotification {
⋮----
pub struct DownloadItem {
⋮----
pub struct Toggles {
⋮----
pub struct Settings {
⋮----
impl Default for Settings {
fn default() -> Self {
⋮----
app_launcher1: "ms-settings:".into(),
app_launcher2: "explorer.exe".into(),
app_launcher3: "calc.exe".into(),
app_launcher4: "notepad.exe".into(),
app_launcher5: "mspaint.exe".into(),
⋮----
theme: "Dark".into(),
font_family: "Normal".into(),
⋮----
pub struct Snapshot {
⋮----
pub struct IconData {
⋮----
pub struct AppEntry {
⋮----
pub struct HitRect {
⋮----
pub struct IntegrationStatus {
````

## File: app/src-tauri/src/usb.rs
````rust
use crate::types::UsbEvent;
use std::collections::HashSet;
⋮----
use std::sync::Arc;
use std::time::Duration;
⋮----
use windows::core::PCWSTR;
⋮----
pub fn start_usb_monitor(app: AppHandle, running: Arc<AtomicBool>) {
⋮----
let mut known_drives = get_connected_removable_drives();
⋮----
while running.load(Ordering::Relaxed) {
⋮----
let current_drives = get_connected_removable_drives();
⋮----
for drive in known_drives.difference(&current_drives) {
let _ = app.emit(
⋮----
device_id: drive.clone(),
friendly_name: format!("Unidad USB ({})", drive),
kind: "Almacenamiento".into(),
details: "Desconectado".into(),
glyph: "\u{E8EB}".into(),
⋮----
for drive in current_drives.difference(&known_drives) {
let volume_name = get_drive_label(drive);
let name = if volume_name.is_empty() {
format!("Unidad USB ({})", drive)
⋮----
format!("{} ({})", volume_name, drive)
⋮----
kind: "Almacenamiento USB".into(),
details: "Conectado".into(),
⋮----
fn get_connected_removable_drives() -> HashSet<String> {
⋮----
let len = GetLogicalDriveStringsW(Some(&mut buf));
⋮----
let drive_wide: Vec<u16> = drive_str.encode_utf16().chain(std::iter::once(0)).collect();
if GetDriveTypeW(PCWSTR(drive_wide.as_ptr())) == DRIVE_REMOVABLE {
set.insert(drive_str.trim_end_matches('\\').to_string());
⋮----
fn get_drive_label(drive: &str) -> String {
⋮----
let drive_path = format!("{}\\\0", drive);
let wide: Vec<u16> = drive_path.encode_utf16().collect();
⋮----
if GetVolumeInformationW(
PCWSTR(wide.as_ptr()),
Some(&mut vol_name),
⋮----
.is_ok()
⋮----
let len = vol_name.iter().position(|&c| c == 0).unwrap_or(vol_name.len());
⋮----
"".to_string()
````

## File: app/src-tauri/src/weather.rs
````rust
use crate::types::WeatherState;
⋮----
use std::sync::Arc;
use std::time::Duration;
⋮----
use windows::core::HSTRING;
use windows::Foundation::Uri;
use windows::Web::Http::HttpClient;
⋮----
pub struct WeatherService {
⋮----
impl WeatherService {
pub fn new() -> Result<Self, String> {
let http = HttpClient::new().map_err(|e| e.to_string())?;
Ok(Self { http })
⋮----
pub async fn fetch_weather(
⋮----
let url_str = format!(
⋮----
Ok(WeatherState {
⋮----
pub fn get_glyph_for_code(code: i32) -> &'static str {
⋮----
pub fn start_weather_worker(
⋮----
while running.load(Ordering::Relaxed) {
if let Ok(weather) = service.fetch_weather(lat, lon, fahrenheit).await {
let _ = app.emit("ln:weather", weather);
````

## File: app/src-tauri/build.rs
````rust
fn main() {
````

## File: app/src-tauri/Cargo.toml
````toml
[package]
name = "locknotch"
version = "2.0.0"
description = "LockNotch — Dynamic Island nativa para Windows"
authors = ["vC3sar"]
edition = "2021"
rust-version = "1.80"

[lib]
name = "locknotch_lib"
crate-type = ["staticlib", "cdylib", "rlib"]

[build-dependencies]
tauri-build = { version = "2", features = [] }

[dependencies]
tauri = { version = "2", features = ["tray-icon", "image-png"] }
tauri-plugin-single-instance = "2"
tauri-plugin-dialog = "2"
serde = { version = "1", features = ["derive"] }
serde_json = "1"
tokio = { version = "1", features = ["rt", "rt-multi-thread", "macros", "io-util", "net", "sync", "time"] }
base64 = "0.22"
winreg = "0.55"
libloading = "0.8"
wmi = "0.17"
log = "0.4"
image = "0.25"
chrono = "0.4"

[dependencies.windows]
version = "0.61"
features = [
  "Foundation",
  "Foundation_Collections",
  "Media_Control",
  "Storage_Streams",
  "Devices_Radios",
  "Web_Http",
  "UI_Notifications",
  "UI_Notifications_Management",
  "ApplicationModel",
  "Win32_Foundation",
  "Win32_Graphics_Gdi",
  "Win32_Security",
  "Win32_Security_Authorization",
  "Win32_Storage_FileSystem",
  "Win32_System_Com",
  "Win32_System_Performance",
  "Win32_System_Power",
  "Win32_System_Threading",
  "Win32_System_WinRT",
  "Win32_System_LibraryLoader",
  "Win32_Devices_DeviceAndDriverInstallation",
  "Win32_UI_HiDpi",
  "Win32_UI_Shell",
  "Win32_UI_WindowsAndMessaging",
]

[profile.release]
codegen-units = 1
lto = true
opt-level = "s"
panic = "abort"
strip = true

[profile.dev.package."*"]
opt-level = 1
````

## File: app/src-tauri/tauri.conf.json
````json
{
  "$schema": "https://schema.tauri.app/config/2",
  "productName": "LockNotch",
  "version": "2.0.0",
  "identifier": "com.locknotch.app",
  "build": {
    "beforeDevCommand": "npm run dev",
    "devUrl": "http://localhost:1420",
    "beforeBuildCommand": "npm run build",
    "frontendDist": "../dist"
  },
  "app": {
    "withGlobalTauri": false,
    "windows": [
      {
        "label": "island",
        "title": "LockNotch",
        "url": "index.html",
        "width": 640,
        "height": 360,
        "x": 0,
        "y": 0,
        "decorations": false,
        "transparent": true,
        "shadow": false,
        "alwaysOnTop": true,
        "skipTaskbar": true,
        "resizable": false,
        "maximizable": false,
        "minimizable": false,
        "closable": false,
        "focus": false,
        "visible": true,
        "dragDropEnabled": false
      }
    ],
    "security": {
      "csp": "default-src 'self' ipc: http://ipc.localhost; img-src 'self' data: blob:; style-src 'self' 'unsafe-inline'; font-src 'self' data:; script-src 'self'"
    }
  },
  "bundle": {
    "active": true,
    "targets": ["nsis"],
    "publisher": "vC3sar",
    "shortDescription": "Dynamic Island para Windows",
    "longDescription": "LockNotch convierte la parte superior de tu pantalla en una isla dinámica con música, notificaciones, descargas, clima, batería, telemetría y centro de control.",
    "category": "Utility",
    "copyright": "vC3sar",
    "icon": [
      "icons/32x32.png",
      "icons/128x128.png",
      "icons/128x128@2x.png",
      "icons/icon.ico"
    ],
    "windows": {
      "nsis": {
        "installMode": "currentUser",
        "languages": ["Spanish", "English"],
        "displayLanguageSelector": false
      }
    }
  }
}
````

## File: app/tests/format.test.ts
````typescript
import { describe, it, expect } from 'vitest';
import {
  formatDuration,
  batteryGlyph,
  batteryText,
  speedText,
  summarizeDownloads,
  gaugeFraction,
  gaugeLevel,
  notificationGlyph,
  notificationText,
  truncate
} from '../src/lib/format';
import type { DownloadItem } from '../src/lib/types';
````

## File: app/tests/islandController.test.ts
````typescript
import { describe, it, expect } from 'vitest';
import { IslandController, Pager, WheelPager } from '../src/lib/islandController';
````

## File: app/tests/spring.test.ts
````typescript
import { describe, it, expect } from 'vitest';
import { NotificationQueue, type NotificationRequest } from '../src/lib/notificationQueue';
import { Spring, SpringPresets } from '../src/lib/spring';
````

## File: app/index.html
````html
<!DOCTYPE html>
<html lang="es">
<head>
  <meta charset="UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <title>LockNotch</title>
  <link rel="stylesheet" href="/src/styles/island.css" />
</head>
<body>
  <div id="overlay-root">
    <div id="vinyl-disc-host">
      <div class="vinyl-disc" id="vinyl-disc">
        <div class="vinyl-grooves"></div>
        <div class="vinyl-sheen"></div>
        <div class="vinyl-label" id="vinyl-label">
          <img id="vinyl-art-img" src="" alt="Album Art" />
        </div>
        <div class="vinyl-spindle"></div>
      </div>
    </div>
    <div id="island">
      <svg class="concave-ear left" viewBox="0 0 12 12">
        <path d="M0,0 L12,0 L12,12 A12,12 0 0 0 0,0 Z" />
      </svg>
      <svg class="concave-ear right" viewBox="0 0 12 12">
        <path d="M12,0 L0,0 L0,12 A12,12 0 0 1 12,0 Z" />
      </svg>
      <div id="view-compact" class="island-view">
        <div id="compact-default" class="compact-subview">
          <div class="compact-left">
            <span id="compact-time" class="compact-time">--:--</span>
            <span id="compact-weather-glyph" class="compact-weather-glyph">☀️</span>
            <span id="compact-weather-temp" class="compact-weather-temp">--°</span>
          </div>
          <div class="compact-right">
            <span id="compact-battery-text" class="compact-battery-text">--%</span>
            <span id="compact-battery-icon" class="compact-battery-icon">&#xE83F;</span>
          </div>
        </div>
        <div id="compact-media" class="compact-subview hidden">
          <div class="compact-left">
            <img id="compact-media-thumb" class="media-thumb-mini" src="" alt="Thumbnail" />
            <span id="compact-media-title" class="compact-time" style="max-width: 130px; white-space: nowrap; overflow: hidden; text-overflow: ellipsis;">--</span>
          </div>
          <div class="compact-right">
            <div id="compact-visualizer" class="equalizer-bars">
              <div class="eq-bar"></div>
              <div class="eq-bar"></div>
              <div class="eq-bar"></div>
              <div class="eq-bar"></div>
            </div>
          </div>
        </div>
        <div id="compact-downloads" class="compact-subview hidden">
          <div class="compact-left">
            <span style="font-family: var(--font-icon); font-size: 15px; color: var(--accent);">&#xE896;</span>
            <span id="compact-downloads-text" class="compact-time">Descargando...</span>
          </div>
          <div class="compact-right">
            <span id="compact-downloads-speed" class="compact-battery-text" style="color: var(--accent);">-- MB/s</span>
          </div>
        </div>
      </div>
      <div id="view-notification" class="island-view hidden">
        <span id="notif-icon" class="notif-icon">&#xE7E7;</span>
        <span id="notif-text" class="notif-text">Nueva notificación</span>
      </div>
      <div id="view-expanded" class="island-view hidden">
        <div class="carousel-viewport">
          <div id="page-media" class="carousel-page page-active">
            <div class="media-meta">
              <span id="exp-artist" class="media-artist">Abre Spotify o YouTube Music</span>
              <span id="exp-title" class="media-title">Nada reproduciéndose</span>
            </div>
            <div>
              <div id="media-progress-track" class="media-progress-bar">
                <div id="media-progress-fill" class="media-progress-fill"></div>
              </div>
              <div class="media-times">
                <span id="media-time-pos">0:00</span>
                <span id="media-time-end">0:00</span>
              </div>
            </div>
            <div class="media-controls">
              <button id="btn-prev" class="media-btn" title="Anterior">&#xE892;</button>
              <button id="btn-play-pause" class="media-btn play-pause" title="Reproducir / Pausar">&#xE768;</button>
              <button id="btn-next" class="media-btn" title="Siguiente">&#xE893;</button>
            </div>
          </div>
          <div id="page-apps" class="carousel-page page-right">
            <div class="page-title">Accesos Rápidos</div>
            <div class="apps-grid">
              <div class="app-slot" data-slot="1" title="App 1">
                <span class="app-fallback-icon">&#xE718;</span>
              </div>
              <div class="app-slot" data-slot="2" title="App 2">
                <span class="app-fallback-icon">&#xE756;</span>
              </div>
              <div class="app-slot" data-slot="3" title="App 3">
                <span class="app-fallback-icon">&#xE753;</span>
              </div>
              <div class="app-slot" data-slot="4" title="App 4">
                <span class="app-fallback-icon">&#xE72D;</span>
              </div>
              <div class="app-slot" data-slot="5" title="App 5">
                <span class="app-fallback-icon">&#xE713;</span>
              </div>
            </div>
          </div>
          <div id="page-control" class="carousel-page page-right">
            <div class="page-title">Centro de Control</div>
            <div class="control-grid">
              <div class="control-toggle" id="toggle-wifi">
                <span class="toggle-icon">&#xE701;</span>
                <span class="toggle-label">Wi-Fi</span>
              </div>
              <div class="control-toggle" id="toggle-bt">
                <span class="toggle-icon">&#xE702;</span>
                <span class="toggle-label">Bluetooth</span>
              </div>
              <div class="control-toggle" id="toggle-dark">
                <span class="toggle-icon">&#xE708;</span>
                <span class="toggle-label">Oscuro</span>
              </div>
              <div class="control-toggle" id="toggle-dnd">
                <span class="toggle-icon">&#xE7ED;</span>
                <span class="toggle-label">No molestar</span>
              </div>
            </div>
          </div>
          <div id="page-hardware" class="carousel-page page-right">
            <div class="page-title">Telemetría del Sistema</div>
            <div class="hardware-cards">
              <div class="telemetry-card">
                <div class="telemetry-header">
                  <span class="telemetry-label">CPU</span>
                  <span id="hw-cpu-source" style="font-size: 10px; color: var(--text-tertiary);">Load</span>
                </div>
                <div id="hw-cpu-val" class="telemetry-value">--%</div>
                <div class="telemetry-bar-bg">
                  <div id="hw-cpu-bar" class="telemetry-bar-fill"></div>
                </div>
              </div>
              <div class="telemetry-card">
                <div class="telemetry-header">
                  <span class="telemetry-label">GPU</span>
                  <span id="hw-gpu-source" style="font-size: 10px; color: var(--text-tertiary);">Load</span>
                </div>
                <div id="hw-gpu-val" class="telemetry-value">--%</div>
                <div class="telemetry-bar-bg">
                  <div id="hw-gpu-bar" class="telemetry-bar-fill"></div>
                </div>
              </div>
            </div>
          </div>
          <div id="page-downloads" class="carousel-page page-right">
            <div class="page-title">Descargas de Chrome</div>
            <div class="downloads-list" id="exp-downloads-list">
            </div>
          </div>
        </div>
        <div class="paginator" id="paginator">
          <div class="paginator-thumb" id="paginator-thumb"></div>
        </div>
      </div>
    </div>
  </div>
  <script type="module" src="/src/island.ts"></script>
</body>
</html>
````

## File: app/package.json
````json
{
  "name": "locknotch",
  "private": true,
  "version": "2.0.0",
  "description": "LockNotch — Dynamic Island nativa para Windows (Tauri + Rust)",
  "type": "module",
  "scripts": {
    "dev": "vite",
    "build": "tsc --noEmit && vite build",
    "typecheck": "tsc --noEmit",
    "test": "vitest run",
    "tauri": "tauri",
    "app:dev": "tauri dev",
    "app:build": "tauri build",
    "app:exe": "tauri build --no-bundle"
  },
  "dependencies": {
    "@tauri-apps/api": "^2.12.4",
    "@tauri-apps/plugin-dialog": "^2.8.3"
  },
  "devDependencies": {
    "@tauri-apps/cli": "^2.12.4",
    "jsdom": "^25.0.1",
    "typescript": "^5.9.3",
    "vite": "^6.4.5",
    "vitest": "^3.2.7"
  }
}
````

## File: app/settings.html
````html
<!DOCTYPE html>
<html lang="es">
<head>
  <meta charset="UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <title>Configuración de LockNotch</title>
  <link rel="stylesheet" href="/src/styles/settings.css" />
</head>
<body>
  <div class="titlebar">
    <div class="titlebar-brand">
      <img src="/src-tauri/icons/32x32.png" alt="LockNotch" />
      <span>Configuración de LockNotch</span>
    </div>
    <div class="titlebar-controls">
      <button class="titlebar-btn close" id="btn-window-close">&#xE8BB;</button>
    </div>
  </div>
  <div class="settings-layout">
    <div class="sidebar">
      <div class="nav-item active" data-tab="tab-general">
        <span class="nav-icon">&#xE713;</span>
        <span>General</span>
      </div>
      <div class="nav-item" data-tab="tab-apps">
        <span class="nav-icon">&#xE71D;</span>
        <span>Accesos Rápidos</span>
      </div>
      <div class="nav-item" data-tab="tab-weather">
        <span class="nav-icon">&#xE753;</span>
        <span>Clima y Telemetría</span>
      </div>
      <div class="nav-item" data-tab="tab-appearance">
        <span class="nav-icon">&#xE790;</span>
        <span>Apariencia</span>
      </div>
      <div class="nav-item" data-tab="tab-about">
        <span class="nav-icon">&#xE946;</span>
        <span>Acerca de y Estado</span>
      </div>
    </div>
    <div class="content-area">
      <div class="tab-content active" id="tab-general">
        <div class="section-header">
          <h2>Comportamiento General</h2>
          <p>Ajustes de inicio del sistema y visibilidad en pantalla completa</p>
        </div>
        <div class="setting-card">
          <div class="setting-row">
            <div class="setting-info">
              <span class="setting-title">Iniciar con Windows</span>
              <span class="setting-desc">Ejecuta LockNotch automáticamente al iniciar sesión</span>
            </div>
            <label class="toggle-switch">
              <input type="checkbox" id="chk-start-windows" />
              <span class="slider"></span>
            </label>
          </div>
          <div class="setting-row">
            <div class="setting-info">
              <span class="setting-title">Ocultar en Pantalla Completa</span>
              <span class="setting-desc">Oculta la isla cuando juegas o ves vídeos en pantalla completa</span>
            </div>
            <label class="toggle-switch">
              <input type="checkbox" id="chk-hide-fullscreen" />
              <span class="slider"></span>
            </label>
          </div>
          <div class="setting-row">
            <div class="setting-info">
              <span class="setting-title">Brillo Ambiental Dinámico</span>
              <span class="setting-desc">Resplandor sutil debajo de la isla según el tema</span>
            </div>
            <label class="toggle-switch">
              <input type="checkbox" id="chk-ambient-glow" />
              <span class="slider"></span>
            </label>
          </div>
        </div>
      </div>
      <div class="tab-content" id="tab-apps">
        <div class="section-header">
          <h2>Lanzador de Apps</h2>
          <p>Configura las 5 aplicaciones del panel de accesos rápidos</p>
        </div>
        <div class="setting-card">
          <div class="setting-row stacked">
            <div class="setting-info">
              <span class="setting-title">Slot 1</span>
              <span class="setting-desc">Ruta de ejecutable, URI de Windows (ej: ms-settings:) o comando</span>
            </div>
            <div class="input-group">
              <input type="text" class="input-text" id="txt-app1" placeholder="ms-settings:" />
              <button class="btn browse-btn" data-target="txt-app1">Examinar</button>
            </div>
          </div>
          <div class="setting-row stacked">
            <div class="setting-info">
              <span class="setting-title">Slot 2</span>
            </div>
            <div class="input-group">
              <input type="text" class="input-text" id="txt-app2" placeholder="explorer.exe" />
              <button class="btn browse-btn" data-target="txt-app2">Examinar</button>
            </div>
          </div>
          <div class="setting-row stacked">
            <div class="setting-info">
              <span class="setting-title">Slot 3</span>
            </div>
            <div class="input-group">
              <input type="text" class="input-text" id="txt-app3" placeholder="calc.exe" />
              <button class="btn browse-btn" data-target="txt-app3">Examinar</button>
            </div>
          </div>
          <div class="setting-row stacked">
            <div class="setting-info">
              <span class="setting-title">Slot 4</span>
            </div>
            <div class="input-group">
              <input type="text" class="input-text" id="txt-app4" placeholder="notepad.exe" />
              <button class="btn browse-btn" data-target="txt-app4">Examinar</button>
            </div>
          </div>
          <div class="setting-row stacked">
            <div class="setting-info">
              <span class="setting-title">Slot 5</span>
            </div>
            <div class="input-group">
              <input type="text" class="input-text" id="txt-app5" placeholder="mspaint.exe" />
              <button class="btn browse-btn" data-target="txt-app5">Examinar</button>
            </div>
          </div>
        </div>
      </div>
      <div class="tab-content" id="tab-weather">
        <div class="section-header">
          <h2>Clima y Ubicación</h2>
          <p>Configura las coordenadas meteorológicas y telemetría de rendimiento</p>
        </div>
        <div class="setting-card">
          <div class="setting-row">
            <div class="setting-info">
              <span class="setting-title">Latitud</span>
            </div>
            <input type="number" step="0.0001" class="input-text" id="txt-lat" style="max-width: 140px;" />
          </div>
          <div class="setting-row">
            <div class="setting-info">
              <span class="setting-title">Longitud</span>
            </div>
            <input type="number" step="0.0001" class="input-text" id="txt-lon" style="max-width: 140px;" />
          </div>
          <div class="setting-row">
            <div class="setting-info">
              <span class="setting-title">Detección Automática</span>
              <span class="setting-desc">Obtén tus coordenadas geográficas al instante</span>
            </div>
            <div style="display: flex; gap: 8px;">
              <button class="btn" id="btn-detect-ip">Detectar por IP</button>
              <button class="btn" id="btn-detect-chrome">GPS de Chrome</button>
            </div>
          </div>
          <div class="setting-row">
            <div class="setting-info">
              <span class="setting-title">Unidad en Fahrenheit</span>
              <span class="setting-desc">Muestra la temperatura en °F en vez de °C</span>
            </div>
            <label class="toggle-switch">
              <input type="checkbox" id="chk-fahrenheit" />
              <span class="slider"></span>
            </label>
          </div>
          <div class="setting-row">
            <div class="setting-info">
              <span class="setting-title">Notificación Periódica de Ubicación</span>
              <span class="setting-desc">Muestra la ciudad al iniciar y periódicamente</span>
            </div>
            <label class="toggle-switch">
              <input type="checkbox" id="chk-periodic-location" />
              <span class="slider"></span>
            </label>
          </div>
          <div class="setting-row">
            <div class="setting-info">
              <span class="setting-title">Intervalo de Refresco de Hardware (segundos)</span>
              <span class="setting-desc">Frecuencia de lectura de sensores de CPU y GPU</span>
            </div>
            <input type="number" min="1" max="60" class="input-text" id="txt-refresh-hw" style="max-width: 90px;" />
          </div>
        </div>
      </div>
      <div class="tab-content" id="tab-appearance">
        <div class="section-header">
          <h2>Personalización Visual</h2>
          <p>Adapta LockNotch a tu estilo y escritorio</p>
        </div>
        <div class="setting-card">
          <div class="setting-row">
            <div class="setting-info">
              <span class="setting-title">Tema Visual</span>
              <span class="setting-desc">Paleta de colores para la isla y la configuración</span>
            </div>
            <select class="select-dropdown" id="sel-theme">
              <option value="Dark">Oscuro Profundo (OLED)</option>
              <option value="Light">Claro Alto Contraste</option>
            </select>
          </div>
          <div class="setting-row">
            <div class="setting-info">
              <span class="setting-title">Tipografía del Sistema</span>
              <span class="setting-desc">Fuentes tipográficas premium incluidas</span>
            </div>
            <select class="select-dropdown" id="sel-font">
              <option value="Normal">Segoe UI Variable (Windows 11)</option>
              <option value="Nothing">Nothing Font (Puntos 5x7)</option>
              <option value="SpaceGrotesk">Space Grotesk</option>
              <option value="Manrope">Manrope</option>
              <option value="Audiowide">Audiowide (Futurista)</option>
              <option value="Oxanium">Oxanium (Cyberpunk)</option>
            </select>
          </div>
        </div>
      </div>
      <div class="tab-content" id="tab-about">
        <div class="section-header">
          <h2>Estado del Sistema e Integraciones</h2>
          <p>Información del host nativo y telemetría de Windows</p>
        </div>
        <div class="setting-card">
          <div class="setting-row">
            <div class="setting-info">
              <span class="setting-title">Versión de LockNotch</span>
              <span class="setting-desc" id="info-version">2.0.0 (Tauri + Rust Nativo)</span>
            </div>
            <span class="status-badge ok">Activo</span>
          </div>
          <div class="setting-row">
            <div class="setting-info">
              <span class="setting-title">Chrome Native Messaging Host</span>
              <span class="setting-desc">Puente para descargas y geolocalización de Chrome</span>
            </div>
            <div style="display: flex; align-items: center; gap: 8px;">
              <span class="status-badge ok" id="badge-native-host">Registrado</span>
              <button class="btn" id="btn-register-host">Re-registrar</button>
            </div>
          </div>
          <div class="setting-row">
            <div class="setting-info">
              <span class="setting-title">Telemetría CPU / GPU</span>
              <span class="setting-desc" id="info-telemetry-source">PDH PerfCounters / WMI</span>
            </div>
            <span class="status-badge ok">Disponible</span>
          </div>
          <div class="setting-row">
            <div class="setting-info">
              <span class="setting-title">Escucha de Notificaciones de Windows</span>
              <span class="setting-desc">Acceso a notificaciones de apps (UserNotificationListener)</span>
            </div>
            <span class="status-badge ok" id="badge-notif-access">Permitido</span>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="footer-bar">
    <button class="btn" id="btn-cancel">Cancelar</button>
    <button class="btn primary" id="btn-save">Guardar Cambios</button>
  </div>
  <script type="module" src="/src/settings.ts"></script>
</body>
</html>
````

## File: app/tsconfig.json
````json
{
  "compilerOptions": {
    "target": "ES2022",
    "useDefineForClassFields": true,
    "module": "ESNext",
    "lib": ["ES2022", "DOM", "DOM.Iterable"],
    "moduleResolution": "bundler",
    "resolveJsonModule": true,
    "isolatedModules": true,
    "noEmit": true,
    "strict": true,
    "noUnusedLocals": true,
    "noUnusedParameters": true,
    "noFallthroughCasesInSwitch": true,
    "noImplicitOverride": true,
    "skipLibCheck": true,
    "types": ["vite/client"]
  },
  "include": ["src", "tests"]
}
````

## File: app/vite.config.ts
````typescript
import { resolve } from "node:path";
import { defineConfig } from "vite";
````

## File: KeyGen/KeyGen.csproj
````
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>
````

## File: KeyGen/Program.cs
````csharp
var rsa = RSA.Create(2048);
byte[] pubKey = rsa.ExportSubjectPublicKeyInfo();
string base64 = Convert.ToBase64String(pubKey);
⋮----
using var sha256 = SHA256.Create();
byte[] hash = sha256.ComputeHash(pubKey);
string hex = BitConverter.ToString(hash).Replace("-", "").ToLower().Substring(0, 32);
⋮----
int val = Convert.ToInt32(c.ToString(), 16);
⋮----
Console.WriteLine($"KEY:{base64}");
Console.WriteLine($"ID:{id}");
````

## File: lockNotch_chrome/com.locknotch.downloads.json
````json
{
  "name": "com.locknotch.downloads",
  "description": "LockNotch Native Messaging Host",
  "path": "C:\\Users\\vCesar\\OneDrive\\Escritorio\\Proyectos\\LockNotch\\app\\src-tauri\\target\\release\\locknotch.exe",
  "type": "stdio",
  "allowed_origins": [
    "chrome-extension://kfmdodbaimfnbghpklkhlcgleffjikge/"
  ]
}
````

## File: lockNotch_chrome/install_host.bat
````batch
@echo off
setlocal

:: Obtener la ruta absoluta del LockNotch.exe
set "EXE_PATH=%~dp0..\src\LockNotch\bin\x64\Debug\net8.0-windows10.0.19041.0\win-x64\LockNotch.exe"
:: Reemplazar slashes para JSON
set "EXE_PATH=%EXE_PATH:\=\\%"

:: Generar el JSON dinamicamente
echo {> "%~dp0com.locknotch.downloads.json"
echo   "name": "com.locknotch.downloads",>> "%~dp0com.locknotch.downloads.json"
echo   "description": "LockNotch Native Messaging Host",>> "%~dp0com.locknotch.downloads.json"
echo   "path": "%EXE_PATH%",>> "%~dp0com.locknotch.downloads.json"
echo   "type": "stdio",>> "%~dp0com.locknotch.downloads.json"
echo   "allowed_origins": [>> "%~dp0com.locknotch.downloads.json"
echo     "chrome-extension://*/">> "%~dp0com.locknotch.downloads.json"
echo   ]>> "%~dp0com.locknotch.downloads.json"
echo }>> "%~dp0com.locknotch.downloads.json"

:: Wait, wildcards in allowed_origins are NOT supported by Chrome for extensions, only for apps (chrome-extension://* is sometimes invalid). 
:: The user MUST replace the extension ID in the file manually or we must know it. 
:: Actually, let's keep the user replacement for now.

echo Registro del host de Chrome Native Messaging...
reg add "HKCU\Software\Google\Chrome\NativeMessagingHosts\com.locknotch.downloads" /ve /t REG_SZ /d "%~dp0com.locknotch.downloads.json" /f

echo.
echo Listo. No olvides cargar la extension sin empaquetar en Chrome,
echo copiar el ID de la extension, y pegarlo en el archivo:
echo com.locknotch.downloads.json
echo en la propiedad "allowed_origins" (por ejemplo: "chrome-extension://abcdefghijklmnop/").
echo Despues reinicia Chrome o la extension.
pause
````

## File: lockNotch_chrome/manifest.json
````json
{
  "name": "LockNotch Downloads",
  "description": "Envia eventos de descargas de Chrome a LockNotch",
  "version": "1.0",
  "manifest_version": 3,
  "key": "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAuV3PWlVBbwnfLDYdxcE7WskeIRpS1QkI6Vu0Jic8tvRnfovyzdm8M8VXmzcCpJSExAjPGX46QJXNNcKScw/dtuU6mQD9iMIo2heg2z66zuKGwBWUvL+S4HNPINf+mOPQcmR0QTB+iVJZo3BQPlqeWVun0JXilZhHHwwq3NTncKiGMQ6+u4FPrPxs6rHBW4upRS53LdPnV9QJ8yNLXvyJMHxL0zAY6MLzdDfAICYAYUYl40VTe7n1lNnNchh2QTjrH7CW8M6kU3jte23AV7GKaNK/D+L+x1g4qT7PDmZMNRrYllqcciLSWwhtr+xY++RguPlFd8bmwpUQDNopzh3fMQIDAQAB",
  "background": {
    "service_worker": "background.js"
  },
  "permissions": [
    "downloads",
    "nativeMessaging",
    "offscreen",
    "geolocation"
  ],
  "host_permissions": [
    "<all_urls>"
  ]
}
````

## File: lockNotch_chrome/offscreen.html
````html
<!DOCTYPE html>
<html>
<head>
  <title>Offscreen Geolocation</title>
</head>
<body>
  <script src="offscreen.js"></script>
</body>
</html>
````

## File: lockNotch_chrome/offscreen.js
````javascript

````

## File: src/LockNotch/Converters/BoolToStringConverter.cs
````csharp
public sealed class BoolToStringConverter : IValueConverter
⋮----
public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
⋮----
public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
throw new NotSupportedException();
````

## File: src/LockNotch/Helpers/AppearanceManager.cs
````csharp
public static class AppearanceManager
⋮----
public static void Apply(string? theme, string? font)
⋮----
public static void ApplyTheme(string? theme)
⋮----
string name = string.Equals(theme, "Light", StringComparison.OrdinalIgnoreCase) ? "Light" : "Dark";
var dict = new ResourceDictionary
⋮----
Source = new Uri($"pack://application:,,,/LockNotch;component/Resources/Themes/{name}.xaml")
⋮----
if (src != null && src.Contains("Themes/", StringComparison.OrdinalIgnoreCase))
merged.RemoveAt(i);
⋮----
merged.Insert(0, dict);
⋮----
public static void ApplyFont(string? fontTag)
⋮----
var uri = new Uri(FontsPack);
if (string.Equals(fontTag, "Nothing", StringComparison.OrdinalIgnoreCase))
⋮----
else if (string.Equals(fontTag, "SpaceGrotesk", StringComparison.OrdinalIgnoreCase))
⋮----
else if (string.Equals(fontTag, "Manrope", StringComparison.OrdinalIgnoreCase))
⋮----
else if (string.Equals(fontTag, "Audiowide", StringComparison.OrdinalIgnoreCase))
⋮----
else if (string.Equals(fontTag, "Oxanium", StringComparison.OrdinalIgnoreCase))
````

## File: src/LockNotch/Helpers/NotchHelper.cs
````csharp
public static class NotchHelper
⋮----
public static readonly DependencyProperty BottomRadiusProperty =
DependencyProperty.RegisterAttached(
⋮----
new PropertyMetadata(0d, OnBottomRadiusChanged));
⋮----
public static double GetBottomRadius(DependencyObject obj) => (double)obj.GetValue(BottomRadiusProperty);
public static void SetBottomRadius(DependencyObject obj, double value) => obj.SetValue(BottomRadiusProperty, value);
⋮----
private static void OnBottomRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
⋮----
double r = Math.Max(0, (double)e.NewValue);
border.CornerRadius = new CornerRadius(0, 0, r, r);
````

## File: src/LockNotch/Helpers/ProgressBarExtensions.cs
````csharp
public static class ProgressBarExtensions
⋮----
public static readonly DependencyProperty SmoothValueProperty =
DependencyProperty.RegisterAttached(
⋮----
new PropertyMetadata(0.0, OnSmoothValueChanged));
⋮----
public static double GetSmoothValue(DependencyObject obj) => (double)obj.GetValue(SmoothValueProperty);
public static void SetSmoothValue(DependencyObject obj, double value) => obj.SetValue(SmoothValueProperty, value);
⋮----
private static void OnSmoothValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
⋮----
double targetValue = Convert.ToDouble(e.NewValue);
var anim = new DoubleAnimation(targetValue, TimeSpan.FromMilliseconds(300))
⋮----
EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
⋮----
progressBar.BeginAnimation(RangeBase.ValueProperty, anim);
````

## File: src/LockNotch/Interop/WindowHelper.cs
````csharp
internal static class WindowHelper
⋮----
public static void MakeNoActivateToolWindow(IntPtr hwnd)
⋮----
long ex = GetWindowLongPtr(hwnd, GWL_EXSTYLE).ToInt64();
⋮----
SetWindowLongPtr(hwnd, GWL_EXSTYLE, new IntPtr(ex));
⋮----
public static void PositionTopCenterOnPrimary(IntPtr hwnd)
````

## File: src/LockNotch/Services/BatteryService.cs
````csharp
public sealed class BatteryService : IBatteryService, IDisposable
⋮----
public void Start()
⋮----
_timer ??= new System.Threading.Timer(_ => Check(), null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
⋮----
private void Check()
⋮----
public void Dispose()
````

## File: src/LockNotch/Services/ClockService.cs
````csharp
public sealed class ClockService : IClockService, IDisposable
⋮----
public void Start()
⋮----
_timer ??= new System.Threading.Timer(_ => Tick(), null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
⋮----
private void Tick()
⋮----
public void Dispose() => _timer?.Dispose();
````

## File: src/LockNotch/Services/IBatteryService.cs
````csharp
public interface IBatteryService
⋮----
void Start();
````

## File: src/LockNotch/Services/IClockService.cs
````csharp
public interface IClockService
⋮----
void Start();
````

## File: src/LockNotch/Services/IFullscreenService.cs
````csharp
public interface IFullscreenService
⋮----
void Start();
````

## File: src/LockNotch/Services/IHardwareService.cs
````csharp
public interface IHardwareService
⋮----
void Start();
````

## File: src/LockNotch/Services/IMediaService.cs
````csharp
public interface IMediaService
⋮----
Task InitializeAsync();
Task PlayAsync();
Task PauseAsync();
Task NextAsync();
Task PreviousAsync();
````

## File: src/LockNotch/Services/INotificationService.cs
````csharp
public interface INotificationService
⋮----
Task InitializeAsync();
````

## File: src/LockNotch/Services/IWeatherService.cs
````csharp
public interface IWeatherService
⋮----
void Start();
````

## File: src/nothing-font-5x7.otf/license.txt
````
FontStruct Commercial Software End User License Agreement 1.0

This software end user license agreement (hereinafter “Agreement”) is made
between you or, if you represent a legal entity, that legal entity (hereinafter
“You”) and “CTFonts”, the copyright holder and designer (hereinafter
“Font Designer”) of the font software named “Nothing Font (5x7)”
(hereinafter “Font Software”).

1. BINDING AGREEMENT
This Agreement is a binding agreement between You and the Font Designer and
shall set forth the terms and conditions under which You can use the Font
Software. For the purposes of this Agreement, Font Software shall mean the Font
Software, and any parts or copies of it.

2. GRANT OF NON-EXCLUSIVE AND LICENCE
The Font Designer hereby grants You a non-exclusive, non-transferable licence to
use the Font Software provided that You comply with all terms and conditions of
this Agreement.

2.1 PERSONAL AND BUSINESS USE
You may use the Font Software for any personal or business purposes and for Your
Work. For the purposes of this Agreement, “Work” shall mean any work,
result or product created by You with the Font Software, including
commercial/profit-making Work. For the avoidance of doubt, this Agreement does
not permit any use of the Font Software as a web-font (see Section 2.5).

2.2 NO SALE OR DISTRIBUTION
You may not sell, rent, license, sublicense, distribute, redistribute, give-away
or make available (in any other way) the Font Software alone or as part of any
collection, product or service to any third party. 

2.3 NO MODIFICATION
You may not modify, adapt, rename, translate, reverse engineer, decompile,
disassemble, alter, or attempt to discover the source code of the Font Software.


2.4 EMBEDDING
You may embed the Font Software in documents, applications or devices either as
a rasterized representation of the Font Software (e.g., a GIF or JPEG) or as a
subset of the Font Software as long as the document, application or device is
distributed in a secure format that permits only the viewing and printing but
not the editing of the text.

2.5 NO USE AS A WEBFONT
You may not make the Font Software accessible on a web server in order to enable
a web browser to render the content of Websites using the respective Font
Software.

2.6 BACKUP
You may make backup copies of the Font Software for archival purposes only,
provided that You retain exclusive custody and control over such copies. Any
backup copy of the Font Software must contain the same copyright, trademark, and
other proprietary information as the original.

2.7 PRINT SHOPS AND SERVICE BUREAUS
You may provide a copy of the Font Software used in Your Work to a print shop,
service bureau, etc. for printing or otherwise outputting Your Work. Afterwards,
the print shop, service bureau, etc. must destroy and/or delete all copies of
the Font Software.

3. ATTRIBUTION
If You make Your Work publicly accessible in any form, You must keep intact all
copyright notices for the Font Software and display, reasonable to the medium or
means You are utilizing for Your Work, an attribution notice (hereinafter “The
Attribution Notice”) comprising of:

the name of the Font Designer or pseudonym, if applicable (“CTFonts”);
the title of the Font Software (“Nothing Font (5x7)”); and
to the extent reasonably practicable, the URI
(https://fontstruct.com/fontstructions/show/2095104) from where the Font was
originally downloaded.

The Attribution Notice may be implemented in any reasonable manner. For the
avoidance of doubt, You may only use the credit required by this Section for the
purpose of attribution in the manner set out above and, by exercising Your
rights under this License, You may not implicitly or explicitly assert or imply
any connection with, sponsorship or endorsement by the Font Designer of You or
Your Work.

4. YOUR REPRESENTATIONS AND INDEMNIFICATIONS
You represent, warrant, and covenant that You shall use the Font Software only
in accordance with the terms of this Agreement and keep the Font Designer fully
indemnified against all actions, claims, costs, proceedings and damages
whatsoever incurred by any breach of any of the aforesaid representations,
warranties of this Agreement.

5. OWNERSHIP AND RIGHTS TO THE FONT SOFTWARE
The Font Designer reserves all rights not expressly granted to You in this
Agreement (hereinafter “Reserved Rights”). The Font Software is protected by
copyright and other intellectual property laws and treaties. You acknowledge
that the Font Designer shall be the sole owner of the Font Software, of the
copyright and all Reserved Rights of whatever kind and nature in the Font
Software.

6. WARRANTY
The Font Software is made available “as is” WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO ANY WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT OF
COPYRIGHT, PATENT, TRADEMARK, OR OTHER RIGHT.THE FONT DESIGNER DOES NOT AND
CANNOT GUARANTEE THE PERFORMANCE OR RESULTS YOU MAY OBTAIN BY USING THE FONT
SOFTWARE.
THE FONT DESIGNER SHALL IN NO EVENT BE LIABLE TO YOU FOR ANY CONSEQUENTIAL,
INCIDENTAL OR SPECIAL DAMAGES, INCLUDING ANY LOST PROFITS OR LOST SAVINGS, OR
FOR ANY CLAIM BY ANY THIRD PARTY, ARISING OUT OF THE USE OR INABILITY TO USE THE
FONT SOFTWARE.

7. TERMINATION / CONDITION PRECEDENT
You may use the Font Software only as set forth in this Agreement. Any use of
the Font Software that is not expressly allowed in this Agreement will
automatically terminate Your rights to use the Font Software under this
Agreement.  If Your right to use the Font Software is terminated You shall
immediately stop using the Font Software, call back or change any Work that is
used in the public, and delete/destroy any copies of the Font Software. The
termination of the Agreement shall not affect the right of the Font Designer to
claim damages or other rights.

8. WAIVER
A waiver by either Party of any term or condition of this Agreement shall not be
deemed or construed to be a waiver of such term or condition for the future or
any subsequent breach thereof.  All remedies, rights, undertakings, obligations
and agreements contained in this Agreement shall be cumulative and none of them
shall be in limitation of any other remedy, right, undertaking, obligation or
agreement of either Party.

9. GOVERNING LAW, PLACE OF JURISDICTION
This Agreement shall be construed and shall take effect in accordance with the
laws of the Federal Republic of Germany excluding the United Nations Convention
on Contracts for the International Sales of Goods (CISG).  To the extent
permitted by law, the Courts of Berlin, Germany shall have exclusive
jurisdiction to resolve any dispute which may arise.

10. ENTIRE AGREEMENT
This Agreement contains the entire agreement between the Parties. No variation
of any of the terms or conditions of this Agreement may be made unless such
variation is agreed in writing and signed by You and the Font Designer.

11. SEVERABILITY
If any provision of this Agreement is adjudged by a court to be invalid or
unenforceable, such provision shall in no way affect any other provision of this
Agreement, the application of such provision in any other circumstance or the
validity or enforceability of this Agreement.  The Parties will negotiate in
good faith about a provision that will replace the invalid or unenforceable
provision.

FontStruct Notice: FontStruct no Party to this Agreement

Please note that FontStruct (www.fontstruct.com) is not a party to this
Agreement between You and the Font Designer.

FontStruct makes no warranty of any kind in connection with the Font Software,
its use or the results that You may obtain by using the Font Software.

FontStruct shall in no event be liable to You or any third party for any damages
whatsoever arising out of or relating to this Agreement or the use of the Font
Software, including but not limited to incidental, special damages, any lost
profits or lost savings or any claim made by a third party.
````

## File: src/nothing-font-5x7.otf/readme.txt
````
The font file in this archive was created using Fontstruct the free, online
font-building tool.
This font was created by “CTFonts”.
This font has a homepage where this archive and other versions may be found:
https://fontstruct.com/fontstructions/show/2095104
[ancestry]
Try Fontstruct at https://fontstruct.com
It’s easy and it’s fun.

Fontstruct is copyright ©2022-2026 Rob Meek

LEGAL NOTICE:
In using this font you must comply with the licensing terms described in the
file “license.txt” included with this archive.
If you redistribute the font file in this archive, it must be accompanied by all
the other files from this archive, including this one.
````

## File: arreglar_locknotch.bat
````batch
@echo off
:: Pedir permisos de Administrador para poder matar la app trabada
NET SESSION >nul 2>&1
if %errorLevel% neq 0 (
    echo Solicitando permisos para cerrar LockNotch...
    powershell -Command "Start-Process '%~f0' -Verb RunAs"
    exit /b
)

echo Cerrando LockNotch forzosamente...
taskkill /F /IM LockNotch.exe >nul 2>&1

echo.
echo Esperando a que el archivo se libere...
timeout /t 2 /nobreak >nul

echo.
echo Compilando la nueva version con el parche de seguridad...
cd /d "%~dp0src\LockNotch"
dotnet build

echo.
echo Iniciando LockNotch...
start "" "%~dp0src\LockNotch\bin\x64\Debug\net8.0-windows10.0.19041.0\win-x64\LockNotch.exe"

echo.
echo Listo. Ya puedes probar Chrome.
pause
````

## File: automatizar.bat
````batch
@echo off
:: Pedir permisos de Administrador automáticamente
NET SESSION >nul 2>&1
if %errorLevel% neq 0 (
    echo Solicitando permisos de administrador para poder cerrar LockNotch...
    powershell -Command "Start-Process '%~f0' -Verb RunAs"
    exit /b
)

echo Cerrando LockNotch...
taskkill /IM LockNotch.exe /F >nul 2>&1

echo.
echo Compilando nueva version...
cd "%~dp0src\LockNotch"
dotnet build

echo.
echo Abriendo Chrome e instalando extension automaticamente...
set "EXT_PATH=%~dp0lockNotch_chrome"
start "" "chrome.exe" "--load-extension=%EXT_PATH%"

echo.
echo Esperando a que Chrome registre la extension...
timeout /t 5 /nobreak >nul

echo.
echo Extrayendo ID de Chrome y configurando Native Messaging...
powershell -Command "$prefsPath = \"$env:LOCALAPPDATA\Google\Chrome\User Data\Default\Preferences\"; $extPath = '%EXT_PATH%'; if (Test-Path $prefsPath) { $prefs = Get-Content $prefsPath -Raw | ConvertFrom-Json; $extId = $null; $settings = $prefs.extensions.settings; foreach ($prop in $settings.psobject.properties) { $path = $prop.Value.path; if ($path -ne $null -and $path -replace '\\\\', '\' -eq $extPath) { $extId = $prop.Name; break } }; if ($extId) { Write-Output 'ID de Extension encontrado: ' $extId; $jsonPath = \"$extPath\com.locknotch.downloads.json\"; $exePath = \"$extPath\..\src\LockNotch\bin\x64\Debug\net8.0-windows10.0.19041.0\win-x64\LockNotch.exe\"; $exePath = (Resolve-Path $exePath).Path -replace '\\', '\\\\'; $jsonObj = @{ name = 'com.locknotch.downloads'; description = 'LockNotch Native Messaging Host'; path = $exePath; type = 'stdio'; allowed_origins = @(\"chrome-extension://$extId/\") }; $jsonObj | ConvertTo-Json -Depth 10 | Set-Content $jsonPath; $regPath = 'HKCU:\Software\Google\Chrome\NativeMessagingHosts\com.locknotch.downloads'; if (!(Test-Path $regPath)) { New-Item -Path $regPath -Force | Out-Null }; Set-ItemProperty -Path $regPath -Name '(default)' -Value $jsonPath; Write-Output 'Host registrado con exito.' } else { Write-Output 'No se pudo detectar el ID. Verifica en Chrome.' } } else { Write-Output 'No se encontro Chrome Preferences.' }"

echo.
echo Iniciando LockNotch...
start "" "%~dp0src\LockNotch\bin\x64\Debug\net8.0-windows10.0.19041.0\win-x64\LockNotch.exe"

echo.
echo Proceso finalizado. Ve a Chrome, haz una descarga y prueba.
pause
````

## File: Directory.Build.props
````
<Project>
  <PropertyGroup>
    <!-- Fuerza x64 también cuando se compila/ejecuta el proyecto directamente (dotnet run). -->
    <Platform Condition="'$(Platform)' == '' Or '$(Platform)' == 'AnyCPU'">x64</Platform>
  </PropertyGroup>
</Project>
````

## File: find_iscc.ps1
````powershell
$paths = @(
    "C:\Program Files (x86)\Inno Setup 6\ISCC.exe",
    "C:\Program Files\Inno Setup 6\ISCC.exe",
    "$env:LOCALAPPDATA\Programs\Inno Setup 6\ISCC.exe"
)
foreach ($p in $paths) {
    if (Test-Path $p) {
        Write-Host "FOUND: $p"
        & $p Setup.iss
        exit 0
    }
}
Write-Host "NOT FOUND"
````

## File: Instalar_LockNotch.bat
````batch
@echo off
color 0b
echo ===================================================
echo              Instalador de LockNotch
echo ===================================================
echo.
echo Iniciando el proceso de instalacion...

powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0Instalar_LockNotch.ps1"

pause
````

## File: Instalar_LockNotch.ps1
````powershell
$ErrorActionPreference = "Stop"

$installDir = "$env:LOCALAPPDATA\LockNotch"
$sourceExe = "src\LockNotch\bin\x64\Release\net8.0-windows10.0.19041.0\win-x64\publish\LockNotch.exe"
$sourceExt = "lockNotch_chrome"

Write-Host "Instalando LockNotch..." -ForegroundColor Cyan

# 1. Crear carpeta
if (!(Test-Path $installDir)) {
    New-Item -ItemType Directory -Path $installDir | Out-Null
}

# 2. Cerrar si está ejecutándose
Get-Process "LockNotch" -ErrorAction SilentlyContinue | Stop-Process -Force

# 3. Copiar ejecutable
Write-Host "Copiando aplicación..."
Copy-Item -Path $sourceExe -Destination "$installDir\LockNotch.exe" -Force

# 4. Copiar extensión
Write-Host "Copiando extensión de Chrome..."
$extDir = "$installDir\Extension"
if (!(Test-Path $extDir)) {
    New-Item -ItemType Directory -Path $extDir | Out-Null
}
Copy-Item -Path "$sourceExt\*" -Destination $extDir -Recurse -Force

# 5. Crear accesos directos
Write-Host "Creando accesos directos..."
$WshShell = New-Object -comObject WScript.Shell

# Desktop
$desktopShortcut = $WshShell.CreateShortcut("$env:USERPROFILE\Desktop\LockNotch.lnk")
$desktopShortcut.TargetPath = "$installDir\LockNotch.exe"
$desktopShortcut.WorkingDirectory = $installDir
$desktopShortcut.Save()

# Start Menu
$startMenuDir = "$env:APPDATA\Microsoft\Windows\Start Menu\Programs\LockNotch"
if (!(Test-Path $startMenuDir)) {
    New-Item -ItemType Directory -Path $startMenuDir | Out-Null
}
$startMenuShortcut = $WshShell.CreateShortcut("$startMenuDir\LockNotch.lnk")
$startMenuShortcut.TargetPath = "$installDir\LockNotch.exe"
$startMenuShortcut.WorkingDirectory = $installDir
$startMenuShortcut.Save()

# 6. Auto-Inicio (Startup)
$registryPath = "HKCU:\Software\Microsoft\Windows\CurrentVersion\Run"
Set-ItemProperty -Path $registryPath -Name "LockNotch" -Value "$installDir\LockNotch.exe"

# 7. Ejecutar LockNotch
Start-Process "$installDir\LockNotch.exe"

# 8. Mensaje y abrir Chrome
Add-Type -AssemblyName PresentationFramework
$msg = "¡LockNotch se ha instalado correctamente!`n`nSe ha creado un icono en tu Escritorio y se abrirá automáticamente al iniciar Windows.`n`nPara activar la sincronización con Spotify/YouTube:`n1. Se abrirá la pestaña de Extensiones de Chrome.`n2. Activa el 'Modo Desarrollador' (arriba a la derecha).`n3. Dale a 'Cargar Descomprimida'.`n4. Selecciona la carpeta que hemos preparado en:`n   $extDir"
[System.Windows.MessageBox]::Show($msg, "LockNotch - Instalación Completa", "OK", "Information")

Start-Process "chrome.exe" "chrome://extensions/"

Write-Host "¡Instalación Completada!" -ForegroundColor Green
````

## File: LockNotch.sln
````
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.0.31903.59
MinimumVisualStudioVersion = 10.0.40219.1
Project("{2150E333-8FDC-42A3-9474-1A3956D46DE8}") = "src", "src", "{038B395E-04EE-49FF-9F4C-516772F3CB94}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "LockNotch", "src\LockNotch\LockNotch.csproj", "{5BC25300-8548-4032-BD8C-2CA05B790445}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{5BC25300-8548-4032-BD8C-2CA05B790445}.Debug|Any CPU.ActiveCfg = Debug|x64
		{5BC25300-8548-4032-BD8C-2CA05B790445}.Debug|Any CPU.Build.0 = Debug|x64
		{5BC25300-8548-4032-BD8C-2CA05B790445}.Release|Any CPU.ActiveCfg = Release|x64
		{5BC25300-8548-4032-BD8C-2CA05B790445}.Release|Any CPU.Build.0 = Release|x64
	EndGlobalSection
	GlobalSection(NestedProjects) = preSolution
		{5BC25300-8548-4032-BD8C-2CA05B790445} = {038B395E-04EE-49FF-9F4C-516772F3CB94}
	EndGlobalSection
EndGlobal
````

## File: README.md
````markdown
# LockNotch

Una "Dynamic Island" / notch estilo iPhone para Windows 11, anclada en el borde superior central de la pantalla.

## Requisitos

- Windows 10 2004 (19041) o superior; diseñado para Windows 11
- .NET 8 SDK x64

## Ejecutar

```powershell
dotnet run --project src/LockNotch
```

Compilar:

```powershell
dotnet build
```

## Uso

| Acción | Resultado |
|---|---|
| Pasar el ratón por encima / clic | Expande la isla |
| Sacar el ratón | Se contrae tras ~400 ms |
| Clic derecho | Menú → **Salir de LockNotch** |
| App en pantalla completa (monitor principal) | La isla se oculta |

La ventana no roba el foco, no aparece en la barra de tareas ni en Alt+Tab, y se recoloca automáticamente si cambia la resolución o la escala (DPI).

## Estado

- [x] Fase 1: notch, hora, animación expandir/contraer, pantalla completa, DPI
- [x] Fase 2: música (GlobalSystemMediaTransportControls) con portada, controles y auto-expansión al cambiar de canción
- [ ] Fase 3: batería y clima (Open-Meteo)
- [ ] Fase 4: temperaturas CPU/GPU (requiere administrador)

## Estructura

```
src/LockNotch/
├─ Interop/       P/Invoke y helpers de ventana
├─ Services/      Reloj, pantalla completa (y más en próximas fases)
├─ Models/
├─ ViewModels/    IslandViewModel
├─ Helpers/       Propiedad adjunta animable para el radio del notch
├─ Converters/
└─ Resources/     Estilos
```
````

## File: Setup.iss
````
[Setup]
AppName=LockNotch
AppVersion=1.0
DefaultDirName={autopf}\LockNotch
DefaultGroupName=LockNotch
UninstallDisplayIcon={app}\LockNotch.exe
Compression=lzma2
SolidCompression=yes
OutputDir=.\Instalador
OutputBaseFilename=LockNotch_Setup
ArchitecturesInstallIn64BitMode=x64
PrivilegesRequired=lowest

[Tasks]
Name: "desktopicon"; Description: "Crear acceso directo en el escritorio"; GroupDescription: "Iconos adicionales:"
Name: "startup"; Description: "Iniciar LockNotch automáticamente al encender Windows"; GroupDescription: "Inicio automático:"

[Files]
; Ejecutable principal auto-contenido
Source: "src\LockNotch\bin\x64\Release\net8.0-windows10.0.19041.0\win-x64\publish\LockNotch.exe"; DestDir: "{app}"; Flags: ignoreversion
; Extensión de Chrome
Source: "lockNotch_chrome\*"; DestDir: "{app}\Extension"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\LockNotch"; Filename: "{app}\LockNotch.exe"
Name: "{autodesktop}\LockNotch"; Filename: "{app}\LockNotch.exe"; Tasks: desktopicon
Name: "{userstartup}\LockNotch"; Filename: "{app}\LockNotch.exe"; Tasks: startup

[Run]
Filename: "{app}\LockNotch.exe"; Description: "Lanzar LockNotch ahora"; Flags: nowait postinstall skipifsilent
Filename: "chrome.exe"; Parameters: "chrome://extensions/"; Description: "Abrir Extensiones de Chrome (Para instalar la extensión)"; Flags: postinstall shellexec runasoriginaluser

[Code]
procedure CurStepChanged(CurStep: TSetupStep);
var
  ExtPath: String;
begin
  if CurStep = ssPostInstall then
  begin
    ExtPath := ExpandConstant('{app}\Extension');
    MsgBox('¡LockNotch se ha instalado correctamente!' + #13#10 + #13#10 + 
           'Para que funcione la música con Chrome, se abrirá la pestaña de Extensiones.' + #13#10 + #13#10 +
           '1. Activa el "Modo Desarrollador" (arriba a la derecha).' + #13#10 +
           '2. Haz clic en "Cargar Descomprimida".' + #13#10 +
           '3. Selecciona esta carpeta: ' + ExtPath, mbInformation, MB_OK);
  end;
end;
````

## File: app/src-tauri/src/downloads.rs
````rust
use crate::types::DownloadItem;
use std::collections::HashMap;
⋮----
use std::time::Duration;
⋮----
pub struct DownloadManager {
⋮----
impl DownloadManager {
pub fn new() -> Self {
⋮----
pub fn get_active_downloads(&self) -> Vec<DownloadItem> {
let map = self.active_downloads.lock().unwrap();
let mut list: Vec<DownloadItem> = map.values().cloned().collect();
list.sort_by(|a, b| b.timestamp.cmp(&a.timestamp));
⋮----
pub fn has_active_downloads(&self) -> bool {
⋮----
!map.is_empty()
⋮----
pub async fn request_location_update(&self) -> bool {
let writer_opt = self.current_pipe_writer.lock().unwrap().clone();
⋮----
let msg = b"{\"type\":\"request_location\"}".to_vec();
return writer.send(msg).await.is_ok();
⋮----
pub fn start_download_pipe_server(
⋮----
while running.load(Ordering::Relaxed) {
⋮----
opts.first_pipe_instance(true);
⋮----
let server_res = opts.create(PIPE_NAME);
⋮----
if server.connect().await.is_err() {
⋮----
let mut current = manager.current_pipe_writer.lock().unwrap();
*current = Some(tx);
⋮----
// Tarea de escritura de salida hacia la extensión de Chrome
⋮----
while let Some(msg) = rx.recv().await {
let len = (msg.len() as u32).to_le_bytes();
if writer.write_all(&len).await.is_err() {
⋮----
if writer.write_all(&msg).await.is_err() {
⋮----
let _ = writer.flush().await;
⋮----
// Bucle de lectura de mensajes entrantes
⋮----
if reader.read_exact(&mut len_buf).await.is_err() {
break; // Extensión desconectada
⋮----
let mut data_buf = vec![0u8; length];
if reader.read_exact(&mut data_buf).await.is_err() {
⋮----
handle_incoming_json(&app, &manager, &json_str);
⋮----
fn handle_incoming_json(app: &AppHandle, manager: &Arc<DownloadManager>, json: &str) {
⋮----
if let Some(msg_type) = doc.get("type").and_then(|v| v.as_str()) {
⋮----
doc.get("latitude").and_then(|v| v.as_f64()),
doc.get("longitude").and_then(|v| v.as_f64()),
⋮----
let loc_str = format!("{},{}", lat, lon);
let _ = app.emit("ln:location", loc_str);
⋮----
let mut map = manager.active_downloads.lock().unwrap();
map.insert(download_id, item);
⋮----
emit_downloads(app, manager);
⋮----
let mgr_clone = manager.clone();
let app_clone = app.clone();
⋮----
let mut map = mgr_clone.active_downloads.lock().unwrap();
map.remove(&download_id);
⋮----
emit_downloads(&app_clone, &mgr_clone);
⋮----
fn emit_downloads(app: &AppHandle, manager: &Arc<DownloadManager>) {
let list = manager.get_active_downloads();
let _ = app.emit("ln:downloads", list);
````

## File: app/src-tauri/src/notifications.rs
````rust
use crate::types::WinNotification;
⋮----
use std::sync::Arc;
⋮----
use windows::Foundation::TypedEventHandler;
⋮----
pub struct NotificationService {
⋮----
impl NotificationService {
pub async fn new() -> Self {
⋮----
let access_op = listener.RequestAccessAsync();
⋮----
if let Ok(status) = access.get() {
⋮----
UserNotificationListenerAccessStatus::Allowed => "Permitido".to_string(),
UserNotificationListenerAccessStatus::Denied => "Denegado".to_string(),
_ => "No especificado".to_string(),
⋮----
listener: Some(listener),
⋮----
access_status: "No disponible".to_string(),
⋮----
pub fn access_status(&self) -> String {
self.access_status.clone()
⋮----
pub fn start_listening(&self, app: AppHandle) -> Result<(), String> {
⋮----
let app_clone = app.clone();
let listener_clone = listener.clone();
⋮----
if let Some(ev) = args.as_ref() {
if let Ok(kind) = ev.ChangeKind() {
⋮----
if let Ok(id) = ev.UserNotificationId() {
if let Ok(notif) = listener_clone.GetNotification(id) {
let mut app_name = "Windows".to_string();
if let Ok(app_info) = notif.AppInfo() {
if let Ok(disp_info) = app_info.DisplayInfo() {
if let Ok(name) = disp_info.DisplayName() {
app_name = name.to_string();
⋮----
if let Ok(visual) = notif.Notification().and_then(|n| n.Visual()) {
if let Ok(binding) = visual.GetBinding(&KnownNotificationBindings::ToastGeneric().unwrap_or_default()) {
if let Ok(texts) = binding.GetTextElements() {
let mut title = "".to_string();
let mut body = "".to_string();
⋮----
if let Ok(first) = texts.GetAt(0) {
if let Ok(t) = first.Text() {
title = t.to_string();
⋮----
if let Ok(second) = texts.GetAt(1) {
if let Ok(b) = second.Text() {
body = b.to_string();
⋮----
if !title.is_empty() {
let _ = app_clone.emit("ln:notification", WinNotification {
⋮----
Ok(())
⋮----
listener.NotificationChanged(&handler).map_err(|e| e.to_string())?;
````

## File: app/src-tauri/src/window.rs
````rust
use crate::types::HitRect;
⋮----
use windows::core::PWSTR;
⋮----
use windows::Win32::UI::HiDpi::GetDpiForWindow;
⋮----
pub struct WindowManager {
⋮----
unsafe impl Send for WindowManager {}
unsafe impl Sync for WindowManager {}
⋮----
impl WindowManager {
pub fn new(window: &WebviewWindow) -> Result<Self, String> {
⋮----
.hwnd()
.map_err(|e| format!("Error getting HWND: {}", e))?;
let hwnd = HWND(hwnd_raw.0 as _);
⋮----
mgr.apply_tool_window_styles();
mgr.position_top_center();
⋮----
Ok(mgr)
⋮----
pub fn apply_tool_window_styles(&self) {
⋮----
let style = GetWindowLongPtrW(self.hwnd, GWL_EXSTYLE);
⋮----
SetWindowLongPtrW(self.hwnd, GWL_EXSTYLE, new_style);
⋮----
pub fn position_top_center(&self) {
⋮----
let primary_mon = MonitorFromPoint(POINT { x: 0, y: 0 }, MONITOR_DEFAULTTOPRIMARY);
⋮----
if GetMonitorInfoW(primary_mon, &mut mi).as_bool() {
⋮----
let _ = GetWindowRect(self.hwnd, &mut win_rect);
⋮----
let _ = SetWindowPos(
⋮----
Some(HWND_TOPMOST),
⋮----
pub fn update_hit_rect(&self, rect: HitRect) {
let mut hr = self.hit_rect.lock().unwrap();
*hr = Some(rect);
⋮----
pub fn set_click_through(&self, pass_through: bool) {
let currently = self.is_transparent.load(Ordering::Relaxed);
⋮----
Some(HWND::default()),
⋮----
self.is_transparent.store(pass_through, Ordering::Relaxed);
⋮----
pub fn check_cursor_hover(&self, cursor_pt: POINT) -> bool {
let hr_opt = self.hit_rect.lock().unwrap();
⋮----
if GetWindowRect(self.hwnd, &mut win_rect).is_ok() {
let dpi = GetDpiForWindow(self.hwnd);
⋮----
let phys_x = win_rect.left + (r.x as f64 * scale).round() as i32;
let phys_y = win_rect.top + (r.y as f64 * scale).round() as i32;
let phys_w = (r.width as f64 * scale).round() as i32;
let phys_h = (r.height as f64 * scale).round() as i32;
⋮----
pub fn start_fullscreen_detector(app: AppHandle, is_hidden_setting: Arc<AtomicBool>) {
⋮----
if !is_hidden_setting.load(Ordering::Relaxed) {
⋮----
let _ = app.emit("ln:hidden", false);
⋮----
let is_fullscreen = detect_fullscreen();
⋮----
let _ = app.emit("ln:hidden", is_fullscreen);
⋮----
fn detect_fullscreen() -> bool {
⋮----
if let Ok(state) = SHQueryUserNotificationState() {
⋮----
let hwnd = GetForegroundWindow();
if hwnd.0.is_null() {
⋮----
let len = RealGetWindowClassW(hwnd, &mut class_buf);
⋮----
let mon = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);
if mon.0.is_null() {
⋮----
if !GetMonitorInfoW(mon, &mut mi).as_bool() {
⋮----
if !GetClientRect(hwnd, &mut client_rect).is_ok() {
````

## File: lockNotch_chrome/background.js
````javascript
function connect()
⋮----
async function setupOffscreenDocument(path)
⋮----
async function updateLocation()
⋮----
function onNativeMessage(message)
⋮----
function sendToLockNotch(message)
⋮----
function startPolling()
⋮----
function stopPolling()
⋮----
function pollDownloads()
⋮----
function getFilename(fullPath)
````

## File: src/LockNotch/Interop/NativeMethods.cs
````csharp
internal static class NativeMethods
⋮----
public static readonly IntPtr HWND_TOPMOST = new(-1);
⋮----
public RECT rcMonitor;
public RECT rcWork;
⋮----
public static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);
⋮----
public static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
⋮----
public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);
⋮----
public static extern IntPtr MonitorFromPoint(POINT pt, uint dwFlags);
⋮----
public static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);
⋮----
public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);
⋮----
public static extern IntPtr GetForegroundWindow();
⋮----
public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
⋮----
public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
⋮----
public static extern int SHQueryUserNotificationState(out QUERY_USER_NOTIFICATION_STATE pquns);
⋮----
public static IntPtr GetPrimaryMonitor() =>
MonitorFromPoint(new POINT { X = 0, Y = 0 }, MONITOR_DEFAULTTOPRIMARY);
⋮----
public static bool TryGetMonitorRect(IntPtr monitor, out RECT rect)
⋮----
var mi = new MONITORINFO { cbSize = Marshal.SizeOf<MONITORINFO>() };
⋮----
public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);
⋮----
public static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);
````

## File: src/LockNotch/Models/IslandState.cs
````csharp

````

## File: src/LockNotch/Models/MediaInfo.cs
````csharp
public sealed class MediaChangedEventArgs(MediaInfo? media, bool trackChanged) : EventArgs
````

## File: src/LockNotch/Resources/Themes/Dark.xaml
````
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <!-- ===== PALETA OSCURA: isla negra, elementos blancos ===== -->
    <!-- Isla / Notch -->
    <SolidColorBrush x:Key="IslandBrush" Color="#FF000000" />
    <SolidColorBrush x:Key="TextPrimaryBrush" Color="#FFFFFFFF" />
    <SolidColorBrush x:Key="TextSecondaryBrush" Color="#99FFFFFF" />
    <SolidColorBrush x:Key="TextTertiaryBrush" Color="#66FFFFFF" />
    <SolidColorBrush x:Key="AccentBrush" Color="#FF34D399" />
    <SolidColorBrush x:Key="HoverBrush" Color="#1FFFFFFF" />
    <SolidColorBrush x:Key="PressedBrush" Color="#33FFFFFF" />
    <SolidColorBrush x:Key="DangerBrush" Color="#FFEF4444" />
    <SolidColorBrush x:Key="VisualizerBrush" Color="#FFFFFFFF" />
    <LinearGradientBrush x:Key="ArtPlaceholderBrush" StartPoint="0,0" EndPoint="1,1">
        <GradientStop Color="#FF3A3A3A" Offset="0" />
        <GradientStop Color="#FF0F0F0F" Offset="1" />
    </LinearGradientBrush>
    <SolidColorBrush x:Key="ProgressFillBrush" Color="#FFFFFFFF" />
    <SolidColorBrush x:Key="ProgressTrackBrush" Color="#33FFFFFF" />
    <SolidColorBrush x:Key="CardBrush" Color="#26FFFFFF" />
    <SolidColorBrush x:Key="CardLabelBrush" Color="#B3FFFFFF" />
    <SolidColorBrush x:Key="CardValueBrush" Color="#FFFFFFFF" />
    <SolidColorBrush x:Key="VinylBrush" Color="#FF1A1A1A" />
    <SolidColorBrush x:Key="VinylGroove1Brush" Color="#14FFFFFF" />
    <SolidColorBrush x:Key="VinylGroove2Brush" Color="#0FFFFFFF" />
    <SolidColorBrush x:Key="VinylHubBrush" Color="#FF000000" />
    <SolidColorBrush x:Key="VinylHubRimBrush" Color="#40FFFFFF" />
    <SolidColorBrush x:Key="VinylOuterRimBrush" Color="#26FFFFFF" />
    <LinearGradientBrush x:Key="VinylSheenBrush" StartPoint="0,0" EndPoint="1,1">
        <GradientStop Color="#40FFFFFF" Offset="0.1" />
        <GradientStop Color="#00FFFFFF" Offset="0.3" />
        <GradientStop Color="#00FFFFFF" Offset="0.7" />
        <GradientStop Color="#40FFFFFF" Offset="0.9" />
    </LinearGradientBrush>

    <!-- Ventana de ajustes -->
    <SolidColorBrush x:Key="SettingsBg" Color="#FF111111" />
    <SolidColorBrush x:Key="SettingsBorder" Color="#33FFFFFF" />
    <SolidColorBrush x:Key="SettingsContentBg" Color="#FF1A1A1A" />
    <SolidColorBrush x:Key="SettingsCardBg" Color="#FF222222" />
    <SolidColorBrush x:Key="SettingsText" Color="#FFFFFFFF" />
    <SolidColorBrush x:Key="SettingsMutedText" Color="#FF9A9A9A" />
    <SolidColorBrush x:Key="SettingsInputBg" Color="#FF252525" />
    <SolidColorBrush x:Key="SettingsInputBorder" Color="#FF404040" />
    <SolidColorBrush x:Key="SettingsSwitchBorder" Color="#FF555555" />
    <SolidColorBrush x:Key="SettingsHoverBg" Color="#1AFFFFFF" />
    <SolidColorBrush x:Key="SettingsSelectedBg" Color="#26FFFFFF" />
    <SolidColorBrush x:Key="SettingsPopupBg" Color="#FF252525" />
    <SolidColorBrush x:Key="SettingsPopupHoverBg" Color="#FF3A3A3A" />
</ResourceDictionary>
````

## File: src/LockNotch/Resources/Themes/Light.xaml
````
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <!-- ===== PALETA CLARA (alto contraste): isla blanca, TODOS los elementos negros ===== -->
    <!-- Isla / Notch -->
    <SolidColorBrush x:Key="IslandBrush" Color="#FFFFFFFF" />
    <SolidColorBrush x:Key="TextPrimaryBrush" Color="#FF000000" />
    <SolidColorBrush x:Key="TextSecondaryBrush" Color="#FF262626" />
    <SolidColorBrush x:Key="TextTertiaryBrush" Color="#FF4D4D4D" />
    <SolidColorBrush x:Key="AccentBrush" Color="#FF000000" />
    <SolidColorBrush x:Key="HoverBrush" Color="#33000000" />
    <SolidColorBrush x:Key="PressedBrush" Color="#4D000000" />
    <SolidColorBrush x:Key="DangerBrush" Color="#FFB71C1C" />
    <SolidColorBrush x:Key="VisualizerBrush" Color="#FF000000" />
    <LinearGradientBrush x:Key="ArtPlaceholderBrush" StartPoint="0,0" EndPoint="1,1">
        <GradientStop Color="#FFFFFFFF" Offset="0" />
        <GradientStop Color="#FFBDBDBD" Offset="1" />
    </LinearGradientBrush>
    <SolidColorBrush x:Key="ProgressFillBrush" Color="#FF000000" />
    <SolidColorBrush x:Key="ProgressTrackBrush" Color="#FFCFCFCF" />
    <SolidColorBrush x:Key="CardBrush" Color="#FFE8E8E8" />
    <SolidColorBrush x:Key="CardLabelBrush" Color="#FF333333" />
    <SolidColorBrush x:Key="CardValueBrush" Color="#FF000000" />
    <SolidColorBrush x:Key="VinylBrush" Color="#FFC4C4C4" />
    <SolidColorBrush x:Key="VinylGroove1Brush" Color="#59000000" />
    <SolidColorBrush x:Key="VinylGroove2Brush" Color="#40000000" />
    <SolidColorBrush x:Key="VinylHubBrush" Color="#FFFFFFFF" />
    <SolidColorBrush x:Key="VinylHubRimBrush" Color="#FF000000" />
    <SolidColorBrush x:Key="VinylOuterRimBrush" Color="#80000000" />
    <LinearGradientBrush x:Key="VinylSheenBrush" StartPoint="0,0" EndPoint="1,1">
        <GradientStop Color="#33000000" Offset="0.1" />
        <GradientStop Color="#00000000" Offset="0.3" />
        <GradientStop Color="#00000000" Offset="0.7" />
        <GradientStop Color="#33000000" Offset="0.9" />
    </LinearGradientBrush>

    <!-- Ventana de ajustes -->
    <SolidColorBrush x:Key="SettingsBg" Color="#FFDADADA" />
    <SolidColorBrush x:Key="SettingsBorder" Color="#FF000000" />
    <SolidColorBrush x:Key="SettingsContentBg" Color="#FFFFFFFF" />
    <SolidColorBrush x:Key="SettingsCardBg" Color="#FFE8E8E8" />
    <SolidColorBrush x:Key="SettingsText" Color="#FF000000" />
    <SolidColorBrush x:Key="SettingsMutedText" Color="#FF333333" />
    <SolidColorBrush x:Key="SettingsInputBg" Color="#FFFFFFFF" />
    <SolidColorBrush x:Key="SettingsInputBorder" Color="#FF4D4D4D" />
    <SolidColorBrush x:Key="SettingsSwitchBorder" Color="#FF000000" />
    <SolidColorBrush x:Key="SettingsHoverBg" Color="#33000000" />
    <SolidColorBrush x:Key="SettingsSelectedBg" Color="#59000000" />
    <SolidColorBrush x:Key="SettingsPopupBg" Color="#FFFFFFFF" />
    <SolidColorBrush x:Key="SettingsPopupHoverBg" Color="#FFD0D0D0" />
</ResourceDictionary>
````

## File: src/LockNotch/Services/DownloadService.cs
````csharp
public class DownloadEvent
⋮----
public interface IDownloadService
⋮----
IEnumerable<DownloadEvent> GetActiveDownloads();
⋮----
DownloadEvent? GetLatestDownload();
void Start();
void RequestLocationUpdate();
⋮----
public class DownloadService : IDownloadService, IDisposable
⋮----
public IEnumerable<DownloadEvent> GetActiveDownloads() =>
_activeDownloads.Values.OrderByDescending(d => d.timestamp);
⋮----
public DownloadEvent? GetLatestDownload() =>
GetActiveDownloads().FirstOrDefault();
⋮----
public void Start()
⋮----
_cts = new CancellationTokenSource();
_serverTask = Task.Run(() => ServerLoop(_cts.Token));
⋮----
private async Task ServerLoop(CancellationToken token)
⋮----
pipeSecurity.AddAccessRule(new System.IO.Pipes.PipeAccessRule(sid, System.IO.Pipes.PipeAccessRights.ReadWrite, System.Security.AccessControl.AccessControlType.Allow));
⋮----
using var pipeServer = System.IO.Pipes.NamedPipeServerStreamAcl.Create("LockNotchDownloadsPipe", PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous, 0, 0, pipeSecurity);
⋮----
await pipeServer.WaitForConnectionAsync(token);
⋮----
int r = await pipeServer.ReadAsync(lengthBuffer, bytesRead, 4 - bytesRead, token);
⋮----
int length = BitConverter.ToInt32(lengthBuffer, 0);
⋮----
int r = await pipeServer.ReadAsync(dataBuffer, bytesRead, length - bytesRead, token);
⋮----
string json = Encoding.UTF8.GetString(dataBuffer, 0, length);
⋮----
using var doc = JsonDocument.Parse(json);
⋮----
if (root.TryGetProperty("type", out var typeProp) && typeProp.GetString() == "update_location")
⋮----
if (root.TryGetProperty("latitude", out var latProp) && root.TryGetProperty("longitude", out var lonProp))
⋮----
LocationUpdated?.Invoke(this, (latProp.GetDouble(), lonProp.GetDouble()));
⋮----
await Task.Delay(1000, token);
⋮----
public void RequestLocationUpdate()
⋮----
var jsonBytes = Encoding.UTF8.GetBytes("{\"type\":\"request_location\"}");
var lenBytes = BitConverter.GetBytes(jsonBytes.Length);
pipe.Write(lenBytes, 0, 4);
pipe.Write(jsonBytes, 0, jsonBytes.Length);
pipe.Flush();
⋮----
private void HandleEvent(DownloadEvent ev)
⋮----
Task.Run(async () =>
⋮----
await Task.Delay(3500);
if (_activeDownloads.TryRemove(ev.downloadId, out _))
⋮----
if (_activeDownloads.TryGetValue(ev.downloadId, out var existing))
⋮----
public void Dispose()
````

## File: src/LockNotch/Services/FullscreenService.cs
````csharp
public sealed class FullscreenService : IFullscreenService, IDisposable
⋮----
public void Start()
⋮----
_timer ??= new System.Threading.Timer(_ => Check(), null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
⋮----
private void Check()
⋮----
if (Interlocked.Exchange(ref _checking, 1) == 1) return;
⋮----
Volatile.Write(ref _checking, 0);
⋮----
private static bool DetectFullscreen()
⋮----
var sb = new StringBuilder(256);
if (GetClassName(hwnd, sb, sb.Capacity) > 0 && IgnoredClasses.Contains(sb.ToString()))
⋮----
var topLeft = new POINT { X = clientRect.Left, Y = clientRect.Top };
var bottomRight = new POINT { X = clientRect.Right, Y = clientRect.Bottom };
⋮----
public void Dispose() => _timer?.Dispose();
````

## File: src/LockNotch/Services/HardwareService.cs
````csharp
public sealed class HardwareService : IHardwareService, IDisposable
⋮----
private readonly Computer _computer;
⋮----
private readonly SettingsService _settings;
⋮----
_computer = new Computer
⋮----
public void Start()
⋮----
_computer.Open();
⋮----
_timer = new System.Threading.Timer(UpdateHardware, null, TimeSpan.Zero, TimeSpan.FromSeconds(refresh));
⋮----
private void UpdateHardware(object? state)
⋮----
hardware.Update();
⋮----
var tempSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature && s.Name.Contains("Package", StringComparison.OrdinalIgnoreCase))
?? hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature && s.Name.Contains("Core Average", StringComparison.OrdinalIgnoreCase))
?? hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature && s.Name.Contains("Tdie", StringComparison.OrdinalIgnoreCase))
?? hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature && s.Name.Contains("Tctl", StringComparison.OrdinalIgnoreCase))
?? hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature && s.Name.Contains("Core", StringComparison.OrdinalIgnoreCase))
?? hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature);
⋮----
_cpuTemp = $"{Math.Round(tempSensor.Value.Value)}°";
⋮----
var loadSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Load && s.Name.Contains("Total", StringComparison.OrdinalIgnoreCase))
?? hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Load);
⋮----
if (loadSensor?.Value.HasValue == true) _cpuTemp = $"{Math.Round(loadSensor.Value.Value)}%";
⋮----
var tempSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature && s.Name.Contains("GPU Core", StringComparison.OrdinalIgnoreCase))
⋮----
_gpuTemp = $"{Math.Round(tempSensor.Value.Value)}°";
⋮----
var loadSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Load && s.Name.Contains("D3D 3D", StringComparison.OrdinalIgnoreCase))
?? hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Load && s.Name.Contains("GPU Core", StringComparison.OrdinalIgnoreCase))
⋮----
if (loadSensor?.Value.HasValue == true) _gpuTemp = $"{Math.Round(loadSensor.Value.Value)}%";
⋮----
HardwareChanged?.Invoke(this, new HardwareChangedEventArgs(_cpuTemp, _gpuTemp));
⋮----
public void Dispose()
⋮----
try { _computer.Close(); } catch { }
````

## File: src/LockNotch/Services/IUsbService.cs
````csharp
public interface IUsbService
⋮----
void Start();
⋮----
public class UsbDeviceEventArgs : EventArgs
````

## File: src/LockNotch/Services/NativeMessagingProxy.cs
````csharp
public static class NativeMessagingProxy
⋮----
public static void Run()
⋮----
Stream stdin = Console.OpenStandardInput();
⋮----
Task.Run(SenderLoop);
⋮----
int r = stdin.Read(lengthBytes, bytesRead, 4 - bytesRead);
⋮----
int length = BitConverter.ToInt32(lengthBytes, 0);
⋮----
int r = stdin.Read(buffer, bytesRead, length - bytesRead);
⋮----
string json = Encoding.UTF8.GetString(buffer, 0, length);
byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
⋮----
if (_messageQueue.Count >= 90) _messageQueue.TryTake(out _);
_messageQueue.Add(jsonBytes);
⋮----
private static void SenderLoop()
⋮----
using var pipeClient = new NamedPipeClientStream(".", "LockNotchDownloadsPipe", PipeDirection.InOut, PipeOptions.Asynchronous);
pipeClient.Connect(2000);
⋮----
var cts = new CancellationTokenSource();
var readerTask = Task.Run(() => ReaderLoop(pipeClient, cts.Token));
⋮----
if (_messageQueue.TryTake(out byte[]? jsonBytes, 1000))
⋮----
byte[] outLengthBytes = BitConverter.GetBytes(jsonBytes.Length);
pipeClient.Write(outLengthBytes, 0, 4);
pipeClient.Write(jsonBytes, 0, jsonBytes.Length);
pipeClient.Flush();
⋮----
cts.Cancel();
⋮----
Thread.Sleep(2000);
⋮----
private static async Task ReaderLoop(NamedPipeClientStream pipe, CancellationToken token)
⋮----
Stream stdout = Console.OpenStandardOutput();
⋮----
int r = await pipe.ReadAsync(lengthBuffer, bytesRead, 4 - bytesRead, token);
⋮----
int length = BitConverter.ToInt32(lengthBuffer, 0);
⋮----
int r = await pipe.ReadAsync(dataBuffer, bytesRead, length - bytesRead, token);
⋮----
stdout.Write(lengthBuffer, 0, 4);
stdout.Write(dataBuffer, 0, length);
stdout.Flush();
````

## File: src/LockNotch/Services/NotificationService.cs
````csharp
public sealed class NotificationService : INotificationService, IDisposable
⋮----
public async Task InitializeAsync()
⋮----
var access = await _listener.RequestAccessAsync();
⋮----
private void OnNotificationChanged(UserNotificationListener sender, UserNotificationChangedEventArgs args)
⋮----
var bindings = notification.Notification.Visual.GetBinding(KnownNotificationBindings.ToastGeneric);
⋮----
var texts = bindings.GetTextElements();
⋮----
NotificationReceived?.Invoke(this, new NotificationEventArgs(appName, title, body));
⋮----
public void Dispose()
⋮----
public class NotificationEventArgs : EventArgs
````

## File: src/LockNotch/App.xaml
````
<Application x:Class="LockNotch.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             ShutdownMode="OnExplicitShutdown">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="Resources/Themes/Dark.xaml" />
                <ResourceDictionary Source="Resources/Styles.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
````

## File: dev.bat
````batch
@echo off
title LockNotch Dev Manager

:: Solicitar elevacion a Administrador si no se tiene
net session >nul 2>&1
if %errorLevel% neq 0 (
    echo Solicitando privilegios de administrador...
    powershell -Command "Start-Process -FilePath '%0' -Verb RunAs"
    exit /b
)

:: Asegurar que el directorio de trabajo sea la carpeta del script (soluciona error de ruta en modo admin)
cd /d "%~dp0"

:loop
cls
echo ====================================
echo   Control de Desarrollo - LockNotch
echo ====================================
echo.
echo  [ENTER] o cualquier tecla para compilar/reiniciar.
echo  [CTRL + C] para salir.
echo.
pause >nul

echo [1/2] Cerrando instancia previa...
taskkill /F /IM LockNotch.exe /T >nul 2>&1
:: Pequeña pausa para asegurar que el archivo se libere
timeout /t 1 /nobreak >nul

echo [2/2] Lanzando LockNotch en un subproceso...
:: 'start' abre un subproceso independiente y no bloquea este .bat
start "LockNotch Background" /MIN dotnet run --project src/LockNotch

goto loop
````

## File: src/LockNotch/Controls/MarqueeTextBlock.cs
````csharp
public class MarqueeTextBlock : System.Windows.Controls.UserControl
⋮----
private readonly TextBlock _textBlock;
private readonly ScrollViewer _scroll;
⋮----
_textBlock = new TextBlock
⋮----
_textBlock.RenderTransform = new TranslateTransform();
⋮----
_scroll = new ScrollViewer
⋮----
_textBlock.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding("Text") { Source = this });
_textBlock.SetBinding(TextBlock.ForegroundProperty, new System.Windows.Data.Binding("Foreground") { Source = this });
_textBlock.SetBinding(TextBlock.FontSizeProperty, new System.Windows.Data.Binding("FontSize") { Source = this });
_textBlock.SetBinding(TextBlock.FontFamilyProperty, new System.Windows.Data.Binding("FontFamily") { Source = this });
_textBlock.SetBinding(TextBlock.FontWeightProperty, new System.Windows.Data.Binding("FontWeight") { Source = this });
⋮----
public static readonly DependencyProperty TextProperty =
DependencyProperty.Register("Text", typeof(string), typeof(MarqueeTextBlock), new PropertyMetadata("", (d, e) => ((MarqueeTextBlock)d).UpdateAnimation()));
⋮----
public static readonly DependencyProperty IsMarqueeEnabledProperty =
DependencyProperty.Register("IsMarqueeEnabled", typeof(bool), typeof(MarqueeTextBlock), new PropertyMetadata(true, (d, e) => ((MarqueeTextBlock)d).UpdateAnimation()));
⋮----
private void UpdateAnimation()
⋮----
_textBlock.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));
⋮----
double seconds = Math.Max(2.0, diff / 25.0);
⋮----
var anim = new DoubleAnimation
⋮----
Duration = TimeSpan.FromSeconds(seconds),
⋮----
BeginTime = TimeSpan.FromSeconds(1)
⋮----
transform.BeginAnimation(TranslateTransform.XProperty, anim);
⋮----
transform.BeginAnimation(TranslateTransform.XProperty, null);
````

## File: src/LockNotch/Models/NotificationRequest.cs
````csharp

````

## File: src/LockNotch/Services/MediaService.cs
````csharp
public sealed class MediaService : IMediaService, IDisposable
⋮----
private readonly SemaphoreSlim _refreshLock = new(1, 1);
⋮----
public async Task InitializeAsync()
⋮----
_manager = await GsmtcManager.RequestAsync();
⋮----
Debug.WriteLine($"[MediaService] Init failed: {ex}");
⋮----
private async void OnCurrentSessionChanged(GsmtcManager sender, CurrentSessionChangedEventArgs args) =>
⋮----
private async void OnSessionsChanged(GsmtcManager sender, SessionsChangedEventArgs args)
⋮----
private GsmtcSession? GetBestSession(GsmtcManager manager)
⋮----
var sessions = manager.GetSessions();
⋮----
var playingSessions = sessions.Where(s =>
s.GetPlaybackInfo()?.PlaybackStatus == GlobalSystemMediaTransportControlsSessionPlaybackStatus.Playing).ToList();
⋮----
var bestPlaying = playingSessions.FirstOrDefault(s =>
knownApps.Any(app => s.SourceAppUserModelId?.Contains(app, StringComparison.OrdinalIgnoreCase) == true));
⋮----
return bestPlaying ?? playingSessions.First();
⋮----
var bestPausedKnown = sessions.FirstOrDefault(s =>
⋮----
return manager.GetCurrentSession() ?? sessions.FirstOrDefault();
⋮----
private async Task AttachSessionAsync(GsmtcSession? session)
⋮----
private async void OnMediaPropertiesChanged(GsmtcSession sender, MediaPropertiesChangedEventArgs args) =>
⋮----
private async void OnPlaybackInfoChanged(GsmtcSession sender, PlaybackInfoChangedEventArgs args) =>
⋮----
private async void OnTimelinePropertiesChanged(GsmtcSession sender, TimelinePropertiesChangedEventArgs args) =>
⋮----
private async Task RefreshAsync()
⋮----
await _refreshLock.WaitAsync();
⋮----
var props = await session.TryGetMediaPropertiesAsync();
var playback = session.GetPlaybackInfo();
⋮----
if (string.IsNullOrWhiteSpace(title) && Current is not null &&
⋮----
if (string.IsNullOrWhiteSpace(title))
⋮----
var timeline = session.GetTimelineProperties();
var info = new MediaInfo(
⋮----
Debug.WriteLine($"[MediaService] Refresh failed: {ex.Message}");
⋮----
_refreshLock.Release();
⋮----
private async Task<byte[]?> GetThumbnailAsync(string trackKey, Windows.Storage.Streams.IRandomAccessStreamReference? reference)
⋮----
using var winStream = await reference.OpenReadAsync();
using var stream = winStream.AsStreamForRead();
using var ms = new MemoryStream();
await stream.CopyToAsync(ms);
⋮----
_thumbnail = ms.ToArray();
⋮----
Debug.WriteLine($"[MediaService] Thumbnail failed: {ex.Message}");
⋮----
private void Publish(MediaInfo? info)
⋮----
MediaChanged?.Invoke(this, new MediaChangedEventArgs(info, trackChanged));
⋮----
public Task PlayAsync() => RunAsync(s => s.TryPlayAsync().AsTask());
public Task PauseAsync() => RunAsync(s => s.TryPauseAsync().AsTask());
public Task NextAsync() => RunAsync(s => s.TrySkipNextAsync().AsTask());
public Task PreviousAsync() => RunAsync(s => s.TrySkipPreviousAsync().AsTask());
⋮----
private async Task RunAsync(Func<GsmtcSession, Task<bool>> action)
⋮----
Debug.WriteLine($"[MediaService] Control failed: {ex.Message}");
⋮----
public void Dispose()
⋮----
_refreshLock.Dispose();
````

## File: src/LockNotch/Services/UsbService.cs
````csharp
public sealed class UsbService : IUsbService, IDisposable
⋮----
private DateTime _lastUsbCheck = DateTime.MinValue;
⋮----
public void Start()
⋮----
var parameters = new HwndSourceParameters("UsbServiceWindow", 0, 0)
⋮----
_source = new HwndSource(parameters);
_source.AddHook(WndProc);
⋮----
var dbi = new DEV_BROADCAST_DEVICEINTERFACE
⋮----
dbcc_size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(DEV_BROADCAST_DEVICEINTERFACE)),
⋮----
dbcc_classguid = new Guid("A5DCBF10-6530-11D2-901F-00C04FB951ED")
⋮----
IntPtr buffer = System.Runtime.InteropServices.Marshal.AllocHGlobal(dbi.dbcc_size);
System.Runtime.InteropServices.Marshal.StructureToPtr(dbi, buffer, false);
⋮----
System.Runtime.InteropServices.Marshal.FreeHGlobal(buffer);
⋮----
Task.Run(() => ProcessUsbChangesAsync(true));
⋮----
private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
⋮----
int eventType = wParam.ToInt32();
⋮----
Task.Run(() => ProcessUsbChangesAsync(false));
⋮----
private async Task ProcessUsbChangesAsync(bool silent)
⋮----
if (!silent) await Task.Delay(500);
⋮----
using var volSearcher = new ManagementObjectSearcher("Select DeviceID, VolumeName, Size, FreeSpace from Win32_LogicalDisk Where DriveType=2");
foreach (var vol in volSearcher.Get())
⋮----
if (string.IsNullOrWhiteSpace(label)) label = "USB Drive";
⋮----
ulong.TryParse(vol["Size"]?.ToString(), out size);
ulong.TryParse(vol["FreeSpace"]?.ToString(), out free);
⋮----
string capStr = size > 0 ? $"{Math.Round(size / 1073741824.0, 1)} GB" : "";
string freeStr = free > 0 ? $"{Math.Round(free / 1073741824.0, 1)} GB libres" : "";
⋮----
currentDevices[letter] = new UsbDeviceInfo(letter, label, "USB Storage", detStr, "\xE8EB", true);
⋮----
using var pnpSearcher = new ManagementObjectSearcher("Select PNPDeviceID, Name, Description, Caption from Win32_PnPEntity Where PNPDeviceID Like 'USB\\\\%'");
foreach (var mbo in pnpSearcher.Get())
⋮----
// Skip hubs and mass storage (handled by LogicalDisk)
if (name.Contains("Hub") || desc.Contains("Hub") || id.Contains("ROOT_HUB") || rawDesc.Contains("Hub")) continue;
if (name.Contains("Mass Storage") || desc.Contains("Mass Storage") || name.Contains("Almacenamiento masivo") || desc.Contains("Almacenamiento masivo") || rawDesc.Contains("Mass Storage") || rawDesc.Contains("Almacenamiento masivo")) continue;
⋮----
if (desc.Contains("MTP") || desc.Contains("Portable") || name.Contains("Phone") || name.Contains("Mobile"))
⋮----
else if (desc.Contains("HID") || desc.Contains("Input") || desc.Contains("Keyboard") || desc.Contains("Mouse"))
⋮----
else if (name.Contains("Xbox") || desc.Contains("Controller") || desc.Contains("Gamepad"))
⋮----
int vidIdx = id.IndexOf("VID_", StringComparison.OrdinalIgnoreCase);
int pidIdx = id.IndexOf("PID_", StringComparison.OrdinalIgnoreCase);
⋮----
int endIdx = id.IndexOf('&', pidIdx + 4);
⋮----
hardwareGroupId = id.Substring(vidIdx, endIdx - vidIdx);
⋮----
hardwareGroupId = id.Substring(vidIdx, Math.Min(17, id.Length - vidIdx));
⋮----
if (currentDevices.TryGetValue(hardwareGroupId, out var existing))
⋮----
if (name.Length > existing.FriendlyName.Length && !name.Equals("MTP", StringComparison.OrdinalIgnoreCase))
⋮----
currentDevices[hardwareGroupId] = new UsbDeviceInfo(hardwareGroupId, name, type, "USB", glyph, true);
⋮----
if (!currentDevices.ContainsKey(known.DeviceId))
⋮----
UsbDeviceChanged?.Invoke(this, new UsbDeviceEventArgs(copy));
⋮----
if (!_knownDevices.ContainsKey(current.DeviceId))
⋮----
UsbDeviceChanged?.Invoke(this, new UsbDeviceEventArgs(current));
⋮----
private string NormalizeUsbName(string raw)
⋮----
if (string.IsNullOrWhiteSpace(raw)) return raw;
string current = raw.Trim();
⋮----
if (current.EndsWith(word, StringComparison.OrdinalIgnoreCase))
⋮----
current = current.Substring(0, current.Length - word.Length).Trim();
⋮----
return string.IsNullOrWhiteSpace(current) ? raw : current;
⋮----
private static extern IntPtr RegisterDeviceNotification(IntPtr recipient, IntPtr notificationFilter, int flags);
⋮----
private static extern bool UnregisterDeviceNotification(IntPtr handle);
⋮----
public Guid dbcc_classguid;
⋮----
private IntPtr _notificationHandle;
⋮----
public void Dispose()
````

## File: src/LockNotch/app.manifest
````
<?xml version="1.0" encoding="utf-8"?>
<assembly manifestVersion="1.0" xmlns="urn:schemas-microsoft-com:asm.v1">
  <assemblyIdentity version="1.0.0.0" name="LockNotch.app"/>

  <trustInfo xmlns="urn:schemas-microsoft-com:asm.v2">
    <security>
      <requestedPrivileges xmlns="urn:schemas-microsoft-com:asm.v3">
        <requestedExecutionLevel level="asInvoker" uiAccess="false" />
      </requestedPrivileges>
    </security>
  </trustInfo>

  <compatibility xmlns="urn:schemas-microsoft-com:compatibility.v1">
    <application>
      <!-- Windows 10 / 11 -->
      <supportedOS Id="{8e0f7a12-bfb3-4fe8-b9a5-48fd50a15a9a}" />
    </application>
  </compatibility>

</assembly>
````

## File: .gitignore
````
bin/
obj/
.vs/
*.user
*node_modules*
app/node_modules
app/dist
app/src-tauri/target
Instalador/
````

## File: src/LockNotch/Resources/Styles.xaml
````
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">

    <!-- Paleta se carga dinámicamente desde Themes/Dark.xaml o Themes/Light.xaml -->
    
    <!-- Tipografía -->
    <FontFamily x:Key="UiFont">Segoe UI Variable Display, Segoe UI</FontFamily>
    <FontFamily x:Key="IconFont">Segoe Fluent Icons, Segoe MDL2 Assets</FontFamily>

    <!-- Popups/tooltips no heredan la fuente de la ventana: se fuerza aquí -->
    <Style TargetType="ToolTip">
        <Setter Property="FontFamily" Value="{DynamicResource UiFont}" />
    </Style>
    <Style TargetType="ContextMenu">
        <Setter Property="FontFamily" Value="{DynamicResource UiFont}" />
    </Style>
    <Style TargetType="MenuItem">
        <Setter Property="FontFamily" Value="{DynamicResource UiFont}" />
    </Style>

    <Style x:Key="IconText" TargetType="TextBlock">
        <Setter Property="FontFamily" Value="{DynamicResource IconFont}" />
        <Setter Property="Foreground" Value="{DynamicResource TextPrimaryBrush}" />
        <Setter Property="HorizontalAlignment" Value="Center" />
        <Setter Property="VerticalAlignment" Value="Center" />
    </Style>

    <!-- Botón circular de control multimedia -->
    <Style x:Key="MediaButton" TargetType="Button">
        <Setter Property="Width" Value="40" />
        <Setter Property="Height" Value="40" />
        <Setter Property="FontSize" Value="16" />
        <Setter Property="Focusable" Value="False" />
        <Setter Property="Cursor" Value="Hand" />
        <Setter Property="Foreground" Value="{DynamicResource TextPrimaryBrush}" />
        <Setter Property="FontFamily" Value="{DynamicResource IconFont}" />
        <Setter Property="RenderTransformOrigin" Value="0.5,0.5" />
        <Setter Property="RenderTransform">
            <Setter.Value>
                <ScaleTransform ScaleX="1" ScaleY="1" />
            </Setter.Value>
        </Setter>
        <Setter Property="Template">
            <Setter.Value>
                <ControlTemplate TargetType="Button">
                    <Border x:Name="Bg"
                            Background="Transparent"
                            CornerRadius="{Binding ActualHeight, RelativeSource={RelativeSource Self}}">
                        <ContentPresenter HorizontalAlignment="Center" VerticalAlignment="Center" />
                    </Border>
                    <ControlTemplate.Triggers>
                        <Trigger Property="IsMouseOver" Value="True">
                            <Setter TargetName="Bg" Property="Background" Value="{DynamicResource HoverBrush}" />
                        </Trigger>
                        <Trigger Property="IsPressed" Value="True">
                            <Setter TargetName="Bg" Property="Background" Value="{DynamicResource PressedBrush}" />
                            <Setter Property="RenderTransform">
                                <Setter.Value>
                                    <ScaleTransform ScaleX="0.9" ScaleY="0.9" />
                                </Setter.Value>
                            </Setter>
                        </Trigger>
                        <Trigger Property="IsEnabled" Value="False">
                            <Setter Property="Opacity" Value="0.35" />
                            <Setter Property="Cursor" Value="Arrow" />
                        </Trigger>
                    </ControlTemplate.Triggers>
                </ControlTemplate>
            </Setter.Value>
        </Setter>
    </Style>

    <Style x:Key="PlayButton" TargetType="Button" BasedOn="{StaticResource MediaButton}">
        <Setter Property="Width" Value="50" />
        <Setter Property="Height" Value="50" />
        <Setter Property="FontSize" Value="22" />
    </Style>

    <Style x:Key="ControlCenterToggle" TargetType="ToggleButton">
        <Setter Property="Width" Value="48" />
        <Setter Property="Height" Value="48" />
        <Setter Property="FontSize" Value="18" />
        <Setter Property="Focusable" Value="False" />
        <Setter Property="Cursor" Value="Hand" />
        <Setter Property="Foreground" Value="{DynamicResource TextPrimaryBrush}" />
        <Setter Property="FontFamily" Value="{DynamicResource IconFont}" />
        <Setter Property="RenderTransformOrigin" Value="0.5,0.5" />
        <Setter Property="RenderTransform">
            <Setter.Value>
                <ScaleTransform ScaleX="1" ScaleY="1" />
            </Setter.Value>
        </Setter>
        <Setter Property="Template">
            <Setter.Value>
                <ControlTemplate TargetType="ToggleButton">
                    <Border x:Name="Bg"
                            Background="Transparent"
                            CornerRadius="{Binding ActualHeight, RelativeSource={RelativeSource Self}}">
                        <ContentPresenter HorizontalAlignment="Center" VerticalAlignment="Center" />
                    </Border>
                    <ControlTemplate.Triggers>
                        <!-- Checked state -->
                        <Trigger Property="IsChecked" Value="True">
                            <Setter TargetName="Bg" Property="Background" Value="{DynamicResource TextPrimaryBrush}" />
                            <Setter Property="Foreground" Value="{DynamicResource IslandBrush}" />
                        </Trigger>
                        <!-- Hover states -->
                        <MultiTrigger>
                            <MultiTrigger.Conditions>
                                <Condition Property="IsMouseOver" Value="True" />
                                <Condition Property="IsChecked" Value="False" />
                            </MultiTrigger.Conditions>
                            <Setter TargetName="Bg" Property="Background" Value="{DynamicResource HoverBrush}" />
                        </MultiTrigger>
                        <MultiTrigger>
                            <MultiTrigger.Conditions>
                                <Condition Property="IsMouseOver" Value="True" />
                                <Condition Property="IsChecked" Value="True" />
                            </MultiTrigger.Conditions>
                            <Setter TargetName="Bg" Property="Opacity" Value="0.85" />
                        </MultiTrigger>
                        <!-- Pressed state -->
                        <Trigger Property="IsPressed" Value="True">
                            <Setter Property="RenderTransform">
                                <Setter.Value>
                                    <ScaleTransform ScaleX="0.9" ScaleY="0.9" />
                                </Setter.Value>
                            </Setter>
                        </Trigger>
                    </ControlTemplate.Triggers>
                </ControlTemplate>
            </Setter.Value>
        </Setter>
    </Style>

</ResourceDictionary>
````

## File: src/LockNotch/Services/SettingsService.cs
````csharp
public class AppSettings
⋮----
public class SettingsService
⋮----
private static readonly string ConfigPath = Path.Combine(
Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
⋮----
Current = new AppSettings();
⋮----
public void Load()
⋮----
if (File.Exists(ConfigPath))
⋮----
var json = File.ReadAllText(ConfigPath);
⋮----
public void Save()
⋮----
var dir = Path.GetDirectoryName(ConfigPath);
if (dir != null && !Directory.Exists(dir))
⋮----
Directory.CreateDirectory(dir);
⋮----
var json = JsonSerializer.Serialize(Current, new JsonSerializerOptions { WriteIndented = true });
File.WriteAllText(ConfigPath, json);
````

## File: src/LockNotch/SettingsWindow.xaml
````
<Window x:Class="LockNotch.SettingsWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Centro de Control - LockNotch" Height="550" Width="750"
        WindowStartupLocation="CenterScreen"
        Background="Transparent" Foreground="{DynamicResource SettingsText}"
        FontFamily="{DynamicResource UiFont}"
        WindowStyle="None" AllowsTransparency="True" ResizeMode="NoResize">

    <Window.Resources>
        <!-- Estilo de TextBlock -->
        <Style TargetType="TextBlock">
            <Setter Property="FontFamily" Value="{DynamicResource UiFont}"/>
            <Setter Property="Foreground" Value="{DynamicResource SettingsText}"/>
        </Style>

        <!-- ComboBox totalmente temático (el de WPF ignora Foreground/Background en el toggle y el popup) -->
        <Style TargetType="ComboBoxItem">
            <Setter Property="FontFamily" Value="{DynamicResource UiFont}"/>
            <Setter Property="Foreground" Value="{DynamicResource SettingsText}"/>
            <Setter Property="Background" Value="Transparent"/>
            <Setter Property="Padding" Value="12,8"/>
            <Setter Property="Cursor" Value="Hand"/>
            <Setter Property="Template">
                <Setter.Value>
                    <ControlTemplate TargetType="ComboBoxItem">
                        <Border x:Name="Bd" Background="{TemplateBinding Background}" Padding="{TemplateBinding Padding}" CornerRadius="4">
                            <ContentPresenter TextElement.Foreground="{TemplateBinding Foreground}"/>
                        </Border>
                        <ControlTemplate.Triggers>
                            <Trigger Property="IsHighlighted" Value="True">
                                <Setter TargetName="Bd" Property="Background" Value="{DynamicResource SettingsPopupHoverBg}"/>
                            </Trigger>
                            <Trigger Property="IsSelected" Value="True">
                                <Setter TargetName="Bd" Property="Background" Value="{DynamicResource SettingsSelectedBg}"/>
                            </Trigger>
                        </ControlTemplate.Triggers>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
        </Style>

        <Style TargetType="ComboBox">
            <Setter Property="FontFamily" Value="{DynamicResource UiFont}"/>
            <Setter Property="Foreground" Value="{DynamicResource SettingsText}"/>
            <Setter Property="Background" Value="{DynamicResource SettingsInputBg}"/>
            <Setter Property="BorderBrush" Value="{DynamicResource SettingsInputBorder}"/>
            <Setter Property="BorderThickness" Value="1"/>
            <Setter Property="Padding" Value="10,7"/>
            <Setter Property="Cursor" Value="Hand"/>
            <Setter Property="Template">
                <Setter.Value>
                    <ControlTemplate TargetType="ComboBox">
                        <Grid>
                            <ToggleButton x:Name="Toggle" Focusable="False" ClickMode="Press"
                                          IsChecked="{Binding IsDropDownOpen, Mode=TwoWay, RelativeSource={RelativeSource TemplatedParent}}">
                                <ToggleButton.Template>
                                    <ControlTemplate TargetType="ToggleButton">
                                        <Border Background="{DynamicResource SettingsInputBg}" BorderBrush="{DynamicResource SettingsInputBorder}" BorderThickness="1" CornerRadius="4"/>
                                    </ControlTemplate>
                                </ToggleButton.Template>
                            </ToggleButton>
                            <ContentPresenter IsHitTestVisible="False" Margin="{TemplateBinding Padding}"
                                              VerticalAlignment="Center" HorizontalAlignment="Left"
                                              Content="{TemplateBinding SelectionBoxItem}"
                                              ContentTemplate="{TemplateBinding SelectionBoxItemTemplate}"
                                              TextElement.Foreground="{DynamicResource SettingsText}"/>
                            <TextBlock Text="&#xE70D;" FontFamily="Segoe Fluent Icons, Segoe MDL2 Assets" FontSize="11"
                                       Foreground="{DynamicResource SettingsText}" IsHitTestVisible="False"
                                       HorizontalAlignment="Right" VerticalAlignment="Center" Margin="0,0,10,0"/>
                            <Popup x:Name="PART_Popup" Placement="Bottom" AllowsTransparency="True" Focusable="False"
                                   IsOpen="{TemplateBinding IsDropDownOpen}" PopupAnimation="Fade">
                                <Border MinWidth="{TemplateBinding ActualWidth}" Margin="0,4,0,8" Padding="4" CornerRadius="6"
                                        Background="{DynamicResource SettingsPopupBg}"
                                        BorderBrush="{DynamicResource SettingsInputBorder}" BorderThickness="1">
                                    <Border.Effect>
                                        <DropShadowEffect Color="Black" BlurRadius="12" ShadowDepth="2" Opacity="0.3"/>
                                    </Border.Effect>
                                    <StackPanel IsItemsHost="True" KeyboardNavigation.DirectionalNavigation="Contained"/>
                                </Border>
                            </Popup>
                        </Grid>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
        </Style>

        <Style TargetType="RadioButton">
            <Setter Property="FontFamily" Value="{DynamicResource UiFont}"/>
            <Setter Property="Foreground" Value="{DynamicResource SettingsText}"/>
            <Setter Property="Cursor" Value="Hand"/>
            <Setter Property="Template">
                <Setter.Value>
                    <ControlTemplate TargetType="RadioButton">
                        <StackPanel Orientation="Horizontal" Background="Transparent">
                            <Grid Width="18" Height="18" VerticalAlignment="Center">
                                <Ellipse Fill="{DynamicResource SettingsInputBg}" Stroke="{DynamicResource SettingsSwitchBorder}" StrokeThickness="1.5"/>
                                <Ellipse x:Name="Dot" Width="8" Height="8" Fill="{DynamicResource AccentBrush}" Visibility="Collapsed"/>
                            </Grid>
                            <ContentPresenter Margin="8,0,0,0" VerticalAlignment="Center" TextElement.Foreground="{TemplateBinding Foreground}"/>
                        </StackPanel>
                        <ControlTemplate.Triggers>
                            <Trigger Property="IsChecked" Value="True">
                                <Setter TargetName="Dot" Property="Visibility" Value="Visible"/>
                            </Trigger>
                        </ControlTemplate.Triggers>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
        </Style>

        <!-- Estilo de TextBox Moderno -->
        <Style TargetType="TextBox">
            <Setter Property="FontFamily" Value="{DynamicResource UiFont}"/>
            <Setter Property="Background" Value="{DynamicResource SettingsInputBg}"/>
            <Setter Property="CaretBrush" Value="{DynamicResource SettingsText}"/>
            <Setter Property="Foreground" Value="{DynamicResource SettingsText}"/>
            <Setter Property="BorderBrush" Value="{DynamicResource SettingsInputBorder}"/>
            <Setter Property="BorderThickness" Value="1"/>
            <Setter Property="Padding" Value="8,6"/>
            <Setter Property="Margin" Value="0,0,0,16"/>
            <Setter Property="Template">
                <Setter.Value>
                    <ControlTemplate TargetType="TextBox">
                        <Border Background="{TemplateBinding Background}" BorderBrush="{TemplateBinding BorderBrush}" BorderThickness="{TemplateBinding BorderThickness}" CornerRadius="4">
                            <ScrollViewer x:Name="PART_ContentHost" Margin="0"/>
                        </Border>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
        </Style>

        <!-- Estilo de ToggleButton (Interruptor) -->
        <Style TargetType="CheckBox" x:Key="ToggleSwitchStyle">
            <Setter Property="FontFamily" Value="{DynamicResource UiFont}"/>
            <Setter Property="Margin" Value="0,0,0,16"/>
            <Setter Property="Template">
                <Setter.Value>
                    <ControlTemplate TargetType="CheckBox">
                        <StackPanel Orientation="Horizontal" Cursor="Hand">
                            <Border x:Name="Switch" Width="40" Height="20" CornerRadius="10" Background="{DynamicResource SettingsInputBorder}" BorderBrush="{DynamicResource SettingsSwitchBorder}" BorderThickness="1">
                                <Ellipse x:Name="Thumb" Width="14" Height="14" Fill="{DynamicResource SettingsMutedText}" HorizontalAlignment="Left" Margin="2,0,0,0"/>
                            </Border>
                            <ContentPresenter Margin="12,0,0,0" VerticalAlignment="Center"/>
                        </StackPanel>
                        <ControlTemplate.Triggers>
                            <Trigger Property="IsChecked" Value="True">
                                <Setter TargetName="Switch" Property="Background" Value="{DynamicResource AccentBrush}"/>
                                <Setter TargetName="Switch" Property="BorderBrush" Value="{DynamicResource AccentBrush}"/>
                                <Setter TargetName="Thumb" Property="Fill" Value="{DynamicResource IslandBrush}"/>
                                <Setter TargetName="Thumb" Property="HorizontalAlignment" Value="Right"/>
                                <Setter TargetName="Thumb" Property="Margin" Value="0,0,2,0"/>
                            </Trigger>
                        </ControlTemplate.Triggers>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
        </Style>

        <!-- Estilos de Botones -->
        <Style TargetType="Button" x:Key="PrimaryButton">
            <Setter Property="FontFamily" Value="{DynamicResource UiFont}"/>
            <Setter Property="Background" Value="{DynamicResource AccentBrush}"/>
            <Setter Property="Foreground" Value="{DynamicResource IslandBrush}"/>
            <Setter Property="FontWeight" Value="SemiBold"/>
            <Setter Property="Padding" Value="16,8"/>
            <Setter Property="BorderThickness" Value="0"/>
            <Setter Property="Cursor" Value="Hand"/>
            <Setter Property="Template">
                <Setter.Value>
                    <ControlTemplate TargetType="Button">
                        <Border Background="{TemplateBinding Background}" CornerRadius="4">
                            <ContentPresenter HorizontalAlignment="Center" VerticalAlignment="Center"/>
                        </Border>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
        </Style>
        <Style TargetType="Button" x:Key="SecondaryButton">
            <Setter Property="FontFamily" Value="{DynamicResource UiFont}"/>
            <Setter Property="Background" Value="{DynamicResource SettingsInputBorder}"/>
            <Setter Property="Foreground" Value="{DynamicResource SettingsText}"/>
            <Setter Property="Padding" Value="16,8"/>
            <Setter Property="BorderThickness" Value="0"/>
            <Setter Property="Cursor" Value="Hand"/>
            <Setter Property="Template">
                <Setter.Value>
                    <ControlTemplate TargetType="Button">
                        <Border Background="{TemplateBinding Background}" CornerRadius="4">
                            <ContentPresenter HorizontalAlignment="Center" VerticalAlignment="Center"/>
                        </Border>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
        </Style>

        <!-- Estilo de Item de Menú Lateral -->
        <Style TargetType="ListBoxItem">
            <Setter Property="FontFamily" Value="{DynamicResource UiFont}"/>
            <Setter Property="Foreground" Value="{DynamicResource SettingsMutedText}"/>
            <Setter Property="Background" Value="Transparent"/>
            <Setter Property="Padding" Value="16,12"/>
            <Setter Property="Margin" Value="8,4"/>
            <Setter Property="Cursor" Value="Hand"/>
            <Setter Property="Template">
                <Setter.Value>
                    <ControlTemplate TargetType="ListBoxItem">
                        <Border x:Name="Bd" Background="{TemplateBinding Background}" CornerRadius="6">
                            <ContentPresenter HorizontalAlignment="Left" VerticalAlignment="Center"/>
                        </Border>
                        <ControlTemplate.Triggers>
                            <Trigger Property="IsMouseOver" Value="True">
                                <Setter TargetName="Bd" Property="Background" Value="{DynamicResource SettingsHoverBg}"/>
                            </Trigger>
                            <Trigger Property="IsSelected" Value="True">
                                <Setter TargetName="Bd" Property="Background" Value="{DynamicResource SettingsSelectedBg}"/>
                                <Setter Property="Foreground" Value="{DynamicResource SettingsText}"/>
                                <Setter Property="FontWeight" Value="SemiBold"/>
                            </Trigger>
                        </ControlTemplate.Triggers>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
        </Style>
    </Window.Resources>

    <!-- Borde Principal (Glassmorphism Oscuro) -->
    <Border Background="{DynamicResource SettingsBg}" BorderBrush="{DynamicResource SettingsBorder}" BorderThickness="1" CornerRadius="12" ClipToBounds="True">
        <Grid>
            <!-- Barra de Título Arrastrable -->
            <Border Height="36" Background="Transparent" VerticalAlignment="Top" MouseLeftButtonDown="TitleBar_MouseLeftButtonDown">
                <TextBlock Text="Ajustes de LockNotch" FontWeight="SemiBold" FontSize="13" Foreground="{DynamicResource SettingsMutedText}" VerticalAlignment="Center" Margin="16,0"/>
            </Border>

            <!-- Layout Central -->
            <Grid Margin="0,36,0,70">
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="220" />
                    <ColumnDefinition Width="*" />
                </Grid.ColumnDefinitions>

                <!-- Barra Lateral -->
                <Border BorderBrush="{DynamicResource SettingsBorder}" BorderThickness="0,1,1,0" Background="{DynamicResource SettingsBg}">
                    <ListBox x:Name="NavMenu" Background="Transparent" BorderThickness="0" Foreground="{DynamicResource SettingsMutedText}" SelectionChanged="NavMenu_SelectionChanged" SelectedIndex="0" ScrollViewer.HorizontalScrollBarVisibility="Disabled">
                        <ListBoxItem>
                            <StackPanel Orientation="Horizontal">
                                <TextBlock Text="&#xE713;" FontFamily="Segoe Fluent Icons, Segoe MDL2 Assets" FontSize="16" Margin="0,0,12,0"/>
                                <TextBlock Text="General" FontSize="15"/>
                            </StackPanel>
                        </ListBoxItem>
                        <ListBoxItem>
                            <StackPanel Orientation="Horizontal">
                                <TextBlock Text="&#xE71C;" FontFamily="Segoe Fluent Icons, Segoe MDL2 Assets" FontSize="16" Margin="0,0,12,0"/>
                                <TextBlock Text="App Launcher" FontSize="15"/>
                            </StackPanel>
                        </ListBoxItem>
                        <ListBoxItem>
                            <StackPanel Orientation="Horizontal">
                                <TextBlock Text="&#xE706;" FontFamily="Segoe Fluent Icons, Segoe MDL2 Assets" FontSize="16" Margin="0,0,12,0"/>
                                <TextBlock Text="Clima y Hardware" FontSize="15"/>
                            </StackPanel>
                        </ListBoxItem>
                        <ListBoxItem>
                            <StackPanel Orientation="Horizontal">
                                <TextBlock Text="&#xE771;" FontFamily="Segoe Fluent Icons, Segoe MDL2 Assets" FontSize="16" Margin="0,0,12,0"/>
                                <TextBlock Text="Apariencia" FontSize="15"/>
                            </StackPanel>
                        </ListBoxItem>
                        <ListBoxItem>
                            <StackPanel Orientation="Horizontal">
                                <TextBlock Text="&#xE946;" FontFamily="Segoe Fluent Icons, Segoe MDL2 Assets" FontSize="16" Margin="0,0,12,0"/>
                                <TextBlock Text="Acerca de" FontSize="15"/>
                            </StackPanel>
                        </ListBoxItem>
                    </ListBox>
                </Border>

                <!-- Contenido Dinámico -->
                <Border Grid.Column="1" BorderBrush="{DynamicResource SettingsBorder}" BorderThickness="0,1,0,0" Background="{DynamicResource SettingsContentBg}" Padding="32">
                    <Grid>
                        <!-- TAB GENERAL -->
                        <StackPanel x:Name="TabGeneral" Visibility="Visible">
                            <TextBlock Text="Configuración General" FontSize="22" FontWeight="Bold" Foreground="{DynamicResource SettingsText}" Margin="0,0,0,24"/>
                            
                            <TextBlock Text="Arranque del Sistema" FontSize="16" FontWeight="SemiBold" Foreground="{DynamicResource AccentBrush}" Margin="0,0,0,8"/>
                            <CheckBox x:Name="ChkStartWindows" Content="Iniciar LockNotch automáticamente al encender Windows" Style="{StaticResource ToggleSwitchStyle}" Foreground="{DynamicResource SettingsText}"/>
                        </StackPanel>

                        <!-- TAB APP LAUNCHER -->
                        <StackPanel x:Name="TabApps" Visibility="Collapsed">
                            <TextBlock Text="Accesos Rápidos" FontSize="22" FontWeight="Bold" Foreground="{DynamicResource SettingsText}" Margin="0,0,0,8"/>
                            <TextBlock Text="Configura los comandos o rutas de las aplicaciones del carrusel." Foreground="{DynamicResource SettingsMutedText}" Margin="0,0,0,24"/>
                            
                            <ScrollViewer VerticalScrollBarVisibility="Auto" Height="300">
                                <StackPanel Margin="0,0,12,0">
                                    <TextBlock Text="1. Aplicación Principal" FontWeight="SemiBold"/>
                                    <TextBox x:Name="TxtApp1" ToolTip="Ej: chrome.exe o C:\App\app.exe"/>

                                    <TextBlock Text="2. Segunda Aplicación" FontWeight="SemiBold"/>
                                    <TextBox x:Name="TxtApp2"/>

                                    <TextBlock Text="3. Tercera Aplicación" FontWeight="SemiBold"/>
                                    <TextBox x:Name="TxtApp3"/>

                                    <TextBlock Text="4. Cuarta Aplicación" FontWeight="SemiBold"/>
                                    <TextBox x:Name="TxtApp4"/>

                                    <TextBlock Text="5. Quinta Aplicación" FontWeight="SemiBold"/>
                                    <TextBox x:Name="TxtApp5"/>
                                </StackPanel>
                            </ScrollViewer>
                        </StackPanel>

                        <!-- TAB CLIMA -->
                        <StackPanel x:Name="TabWeather" Visibility="Collapsed">
                            <TextBlock Text="Clima y Sensores" FontSize="22" FontWeight="Bold" Foreground="{DynamicResource SettingsText}" Margin="0,0,0,8"/>
                            <TextBlock Text="Ajusta tu ubicación y métricas para la pantalla compacta." Foreground="{DynamicResource SettingsMutedText}" Margin="0,0,0,24"/>

                            <TextBlock Text="Coordenadas (Open-Meteo)" FontSize="16" FontWeight="SemiBold" Foreground="{DynamicResource AccentBrush}" Margin="0,0,0,8"/>
                            <Grid>
                                <Grid.ColumnDefinitions>
                                    <ColumnDefinition Width="*"/>
                                    <ColumnDefinition Width="16"/>
                                    <ColumnDefinition Width="*"/>
                                </Grid.ColumnDefinitions>
                                <StackPanel Grid.Column="0">
                                    <TextBlock Text="Latitud"/>
                                    <TextBox x:Name="TxtLat"/>
                                </StackPanel>
                                <StackPanel Grid.Column="2">
                                    <TextBlock Text="Longitud"/>
                                    <TextBox x:Name="TxtLon"/>
                                </StackPanel>
                            </Grid>
                            <StackPanel Orientation="Horizontal" Margin="0,0,0,8">
                                <Button Content="Auto-detectar ubicación (IP)" Style="{StaticResource SecondaryButton}" Click="AutoDetectLocation_Click" Padding="12,6" FontSize="13"/>
                                <Button Content="Detección ultra precisa (Extensión Chrome)" Style="{StaticResource SecondaryButton}" Click="ExtensionLocation_Click" Margin="12,0,0,0" Padding="12,6" FontSize="13"/>
                            </StackPanel>

                            <TextBlock Text="Unidades" FontSize="16" FontWeight="SemiBold" Foreground="{DynamicResource AccentBrush}" Margin="0,20,0,8"/>
                            <CheckBox x:Name="ChkFahrenheit" Content="Usar grados Fahrenheit (°F) en lugar de Celsius (°C)" Style="{StaticResource ToggleSwitchStyle}" Foreground="{DynamicResource SettingsText}"/>

                            <TextBlock Text="Comportamiento del Notch" FontSize="16" FontWeight="SemiBold" Foreground="{DynamicResource AccentBrush}" Margin="0,20,0,8"/>
                            <CheckBox x:Name="ChkShowLocation" Content="Mostrar notificación periódica con mi ubicación" Style="{StaticResource ToggleSwitchStyle}" Foreground="{DynamicResource SettingsText}"/>

                            <TextBlock Text="Intervalo de Refresco Hardware" FontSize="16" FontWeight="SemiBold" Foreground="{DynamicResource AccentBrush}" Margin="0,20,0,8"/>
                            <StackPanel Orientation="Horizontal">
                                <TextBox x:Name="TxtRefresh" Width="60" Margin="0,0,8,0" TextAlignment="Center"/>
                                <TextBlock Text="segundos" VerticalAlignment="Center" Margin="0,0,0,16"/>
                            </StackPanel>
                        </StackPanel>

                        <!-- TAB APARIENCIA -->
                        <StackPanel x:Name="TabAppearance" Visibility="Collapsed">
                            <TextBlock Text="Apariencia y Temas" FontSize="22" FontWeight="Bold" Foreground="{DynamicResource SettingsText}" Margin="0,0,0,8"/>
                            <TextBlock Text="Personaliza los colores del LockNotch." Foreground="{DynamicResource SettingsMutedText}" Margin="0,0,0,24"/>

                            <TextBlock Text="Tema Global" FontSize="16" FontWeight="SemiBold" Foreground="{DynamicResource AccentBrush}" Margin="0,0,0,12"/>
                            
                            <StackPanel Orientation="Horizontal" Margin="0,0,0,16">
                                <RadioButton x:Name="RbThemeDark" Content="Oscuro (Dark)" IsChecked="True" Margin="0,0,24,0" Foreground="{DynamicResource SettingsText}" Cursor="Hand"/>
                                <RadioButton x:Name="RbThemeLight" Content="Claro (Light)" Foreground="{DynamicResource SettingsText}" Cursor="Hand"/>
                            </StackPanel>

                            <TextBlock Text="Tipografía" FontSize="16" FontWeight="SemiBold" Foreground="{DynamicResource AccentBrush}" Margin="0,12,0,12"/>
                            <ComboBox x:Name="CmbFont" Width="200" HorizontalAlignment="Left" Background="{DynamicResource SettingsInputBg}" Foreground="{DynamicResource SettingsText}" BorderBrush="{DynamicResource SettingsInputBorder}">
                                <ComboBoxItem Content="Normal (Segoe UI)" Tag="Normal" />
                                <ComboBoxItem Content="Nothing-dots" Tag="Nothing" />
                                <ComboBoxItem Content="Space Grotesk" Tag="SpaceGrotesk" />
                                <ComboBoxItem Content="Manrope" Tag="Manrope" />
                                <ComboBoxItem Content="Audiowide" Tag="Audiowide" />
                                <ComboBoxItem Content="Oxanium" Tag="Oxanium" />
                            </ComboBox>

                            <TextBlock Text="Comportamiento en Pantalla Completa" FontSize="16" FontWeight="SemiBold" Foreground="{DynamicResource AccentBrush}" Margin="0,20,0,12"/>
                            <CheckBox x:Name="ChkAllowFullscreen" Content="Permitir mostrar el notch sobre juegos y apps a pantalla completa" Style="{StaticResource ToggleSwitchStyle}" Foreground="{DynamicResource SettingsText}"/>
                        </StackPanel>

                        <!-- TAB ACERCA DE -->
                        <StackPanel x:Name="TabAbout" Visibility="Collapsed">
                            <TextBlock Text="Acerca de LockNotch" FontSize="22" FontWeight="Bold" Foreground="{DynamicResource SettingsText}" Margin="0,0,0,24"/>
                            
                            <StackPanel Orientation="Horizontal" Margin="0,0,0,16">
                                <Border Width="64" Height="64" Background="{DynamicResource SettingsInputBorder}" CornerRadius="16" Margin="0,0,16,0">
                                    <TextBlock Text="&#xE74C;" FontFamily="Segoe Fluent Icons, Segoe MDL2 Assets" Foreground="{DynamicResource SettingsText}" FontSize="32" HorizontalAlignment="Center" VerticalAlignment="Center"/>
                                </Border>
                                <StackPanel VerticalAlignment="Center">
                                    <TextBlock Text="LockNotch" FontSize="24" FontWeight="Bold" Foreground="{DynamicResource SettingsText}"/>
                                    <TextBlock Text="Versión 1.0.0" Foreground="{DynamicResource SettingsMutedText}"/>
                                </StackPanel>
                            </StackPanel>

                            <Border Background="{DynamicResource SettingsCardBg}" CornerRadius="8" Padding="16" Margin="0,16">
                                <StackPanel>
                                    <TextBlock Text="Hecho con ❤️ para entusiastas de lo aesthetic y aprecio a lo que Dios ha creado." 
                                               Foreground="{DynamicResource SettingsText}" FontSize="15" TextWrapping="Wrap" LineHeight="24" FontStyle="Italic"/>
                                </StackPanel>
                            </Border>

                            <TextBlock Foreground="{DynamicResource SettingsMutedText}" TextWrapping="Wrap" Margin="0,16,0,0">
                                <Run Text="Un proyecto desarrollado por "/>
                                <Run Text="vC3sar" FontWeight="Bold" Foreground="{DynamicResource SettingsText}"/>
                                <Run Text=" para llevar la experiencia premium al escritorio de Windows."/>
                            </TextBlock>

                            <StackPanel Orientation="Horizontal" Margin="0,16,0,0">
                                <TextBlock Text="&#xE12B;" FontFamily="Segoe Fluent Icons, Segoe MDL2 Assets" Foreground="{DynamicResource SettingsMutedText}" VerticalAlignment="Center" Margin="0,0,8,0"/>
                                <TextBlock Foreground="{DynamicResource AccentBrush}" Cursor="Hand" TextDecorations="Underline" MouseLeftButtonUp="RepoLink_Click" ToolTip="Abrir GitHub" VerticalAlignment="Center">
                                    https://github.com/vC3sar/LockNotch
                                </TextBlock>
                            </StackPanel>
                        </StackPanel>
                    </Grid>
                </Border>
            </Grid>

            <!-- Barra Inferior (Guardar/Cancelar) -->
            <Border VerticalAlignment="Bottom" Height="70" Background="{DynamicResource SettingsBg}" BorderBrush="{DynamicResource SettingsBorder}" BorderThickness="0,1,0,0">
                <StackPanel Orientation="Horizontal" HorizontalAlignment="Right" VerticalAlignment="Center" Margin="24,0">
                    <Button Content="Cancelar" Style="{StaticResource SecondaryButton}" Click="Cancel_Click" Margin="0,0,12,0"/>
                    <Button Content="Guardar y Aplicar" Style="{StaticResource PrimaryButton}" Click="Save_Click"/>
                </StackPanel>
            </Border>
        </Grid>
    </Border>
</Window>
````

## File: src/LockNotch/SettingsWindow.xaml.cs
````csharp
public partial class SettingsWindow : Window
⋮----
private readonly SettingsService _settings;
private readonly IDownloadService _downloads;
⋮----
TxtLat.Text = _settings.Current.WeatherLatitude.ToString(CultureInfo.InvariantCulture);
TxtLon.Text = _settings.Current.WeatherLongitude.ToString(CultureInfo.InvariantCulture);
⋮----
TxtRefresh.Text = _settings.Current.HardwareRefreshIntervalSeconds.ToString();
⋮----
if (item.Tag.ToString() == _settings.Current.FontFamily)
⋮----
private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
⋮----
private void NavMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
⋮----
private async void AutoDetectLocation_Click(object sender, RoutedEventArgs e)
⋮----
string originalContent = button.Content.ToString() ?? "";
⋮----
using var doc = System.Text.Json.JsonDocument.Parse(response);
⋮----
double lat = doc.RootElement.GetProperty("lat").GetDouble();
double lon = doc.RootElement.GetProperty("lon").GetDouble();
⋮----
TxtLat.Text = lat.ToString(CultureInfo.InvariantCulture);
TxtLon.Text = lon.ToString(CultureInfo.InvariantCulture);
⋮----
await System.Threading.Tasks.Task.Delay(1500);
⋮----
private void ExtensionLocation_Click(object sender, RoutedEventArgs e)
⋮----
_downloads.RequestLocationUpdate();
⋮----
System.Threading.Tasks.Task.Delay(1500).ContinueWith(_ =>
⋮----
Dispatcher.Invoke(() =>
⋮----
private void RepoLink_Click(object sender, MouseButtonEventArgs e)
⋮----
System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
⋮----
private void Save_Click(object sender, RoutedEventArgs e)
⋮----
if (double.TryParse(TxtLat.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double lat))
⋮----
if (double.TryParse(TxtLon.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double lon))
⋮----
if (int.TryParse(TxtRefresh.Text, out int refresh) && refresh > 0)
⋮----
_settings.Current.FontFamily = selectedFont.Tag.ToString() ?? "Normal";
⋮----
_settings.Save();
⋮----
LockNotch.Helpers.AppearanceManager.Apply(_settings.Current.Theme, _settings.Current.FontFamily);
⋮----
private void Cancel_Click(object sender, RoutedEventArgs e)
⋮----
private void ApplyStartupRegistry(bool enable)
⋮----
using var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
⋮----
string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName ?? "";
if (!string.IsNullOrEmpty(exePath))
⋮----
key.SetValue("LockNotch", $"\"{exePath}\"");
⋮----
key.DeleteValue("LockNotch", false);
````

## File: src/LockNotch/LockNotch.csproj
````
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net8.0-windows10.0.19041.0</TargetFramework>
    <SupportedOSPlatformVersion>10.0.19041.0</SupportedOSPlatformVersion>
    <UseWPF>true</UseWPF>
    <UseWindowsForms>true</UseWindowsForms>
    <ApplicationIcon>Resources\icon.ico</ApplicationIcon>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <RootNamespace>LockNotch</RootNamespace>
    <AssemblyName>LockNotch</AssemblyName>
    <Platforms>x64</Platforms>
    <PlatformTarget>x64</PlatformTarget>
    <RuntimeIdentifier>win-x64</RuntimeIdentifier>
    <SelfContained>false</SelfContained>
    <ApplicationManifest>app.manifest</ApplicationManifest>
    <ApplicationHighDpiMode>PerMonitorV2</ApplicationHighDpiMode>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="CommunityToolkit.Mvvm" Version="8.3.2" />
    <PackageReference Include="LibreHardwareMonitorLib" Version="0.9.6" />
    <PackageReference Include="MahApps.Metro.IconPacks.Material" Version="6.2.1" />
    <PackageReference Include="System.Management" Version="10.0.12" />
  </ItemGroup>

  <ItemGroup>
    <Resource Include="Resources\Fonts\**\*.otf" />
    <Resource Include="Resources\Fonts\**\*.ttf" />
    <Resource Include="Resources\icon.ico" />
  </ItemGroup>

</Project>
````

## File: src/LockNotch/Services/WeatherService.cs
````csharp
public sealed class WeatherService : IWeatherService, IDisposable
⋮----
private readonly HttpClient _http = new();
private readonly SettingsService _settings;
⋮----
_http.DefaultRequestHeaders.Add("User-Agent", "LockNotch/1.0");
⋮----
public void Start()
⋮----
_timer ??= new System.Threading.Timer(_ => _ = FetchWeatherAsync(), null, TimeSpan.Zero, TimeSpan.FromMinutes(30));
⋮----
public void Refresh()
⋮----
private async Task FetchWeatherAsync()
⋮----
var response = await _http.GetStringAsync(url);
using var doc = JsonDocument.Parse(response);
⋮----
var current = doc.RootElement.GetProperty("current_weather");
double temp = current.GetProperty("temperature").GetDouble();
int code = current.GetProperty("weathercode").GetInt32();
⋮----
_temperature = $"{Math.Round(temp)}°";
⋮----
private static string GetGlyphForCode(int code)
⋮----
public void Dispose()
⋮----
_http.Dispose();
````

## File: src/LockNotch/ViewModels/IslandViewModel.cs
````csharp
public partial class IslandViewModel : ObservableObject, IDisposable
⋮----
private static readonly TimeSpan TrackChangeExpandDuration = TimeSpan.FromSeconds(2);
⋮----
private readonly IClockService _clock;
private readonly IFullscreenService _fullscreen;
private readonly IMediaService _media;
private readonly IBatteryService _battery;
private readonly IWeatherService _weather;
private readonly IUsbService _usb;
private readonly IHardwareService _hardware;
private readonly INotificationService _notifications;
private readonly SettingsService _settings;
private readonly IDownloadService _downloads;
private readonly SynchronizationContext _ui;
⋮----
public TimeSpan CollapseDelay { get; set; } = TimeSpan.FromMilliseconds(400);
⋮----
public void NextPage() => CurrentPageIndex = (CurrentPageIndex + 1) % TotalPages;
public void PreviousPage() => CurrentPageIndex = (CurrentPageIndex - 1 + TotalPages) % TotalPages;
⋮----
_ui = SynchronizationContext.Current ?? new SynchronizationContext();
⋮----
TimeText = _clock.Now.ToString("h:mm tt");
⋮----
_periodicLocationTimer = new System.Threading.Timer(OnPeriodicLocationTick, null, TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(10));
⋮----
public async Task StartAsync()
⋮----
_clock.Start();
_fullscreen.Start();
_battery.Start();
_weather.Start();
_usb.Start();
_hardware.Start();
await _media.InitializeAsync();
⋮----
_ = Task.Run(async () =>
⋮----
if (_settings.Current.WeatherShowPeriodicLocation && !string.IsNullOrEmpty(_cachedLocationName))
⋮----
_ui.Post(_ =>
⋮----
EnqueueNotification(new NotificationRequest("location", "\xE81D", _cachedLocationName, brush, TimeSpan.FromSeconds(4)));
⋮----
private async void OnLocationUpdated(object? sender, (double lat, double lon) loc)
⋮----
private async Task FetchLocationNameAsync(double lat, double lon)
⋮----
var url = $"https://api.bigdatacloud.net/data/reverse-geocode-client?latitude={lat.ToString(System.Globalization.CultureInfo.InvariantCulture)}&longitude={lon.ToString(System.Globalization.CultureInfo.InvariantCulture)}&localityLanguage=es";
var json = await http.GetStringAsync(url);
⋮----
using var doc = System.Text.Json.JsonDocument.Parse(json);
⋮----
if (root.TryGetProperty("city", out var cityProp) && !string.IsNullOrWhiteSpace(cityProp.GetString()))
locName = cityProp.GetString()!;
else if (root.TryGetProperty("locality", out var locProp) && !string.IsNullOrWhiteSpace(locProp.GetString()))
locName = locProp.GetString()!;
else if (root.TryGetProperty("principalSubdivision", out var subProp) && !string.IsNullOrWhiteSpace(subProp.GetString()))
locName = subProp.GetString()!;
⋮----
if (!string.IsNullOrEmpty(locName))
⋮----
private void OnPeriodicLocationTick(object? state)
⋮----
if (!string.IsNullOrEmpty(_cachedLocationName))
⋮----
EnqueueNotification(new NotificationRequest("location", "\xE81D", _cachedLocationName, brush, TimeSpan.FromSeconds(5)));
⋮----
public void PointerEntered()
⋮----
public void PointerExited()
⋮----
private void Expand()
⋮----
if (!_isPointerOver) ScheduleCollapse(TimeSpan.FromSeconds(3));
⋮----
public void ExpandTemporarily(TimeSpan duration)
⋮----
private async void ScheduleCollapse(TimeSpan delay)
⋮----
var cts = new CancellationTokenSource();
⋮----
await Task.Delay(delay, cts.Token);
⋮----
private void CancelPendingCollapse()
⋮----
private void LaunchApp(string? appId)
⋮----
if (string.IsNullOrWhiteSpace(appId)) return;
⋮----
if (string.IsNullOrWhiteSpace(appPath)) return;
⋮----
System.Diagnostics.Process.Start(psi);
⋮----
private DateTime _suppressUsbNotificationsUntil = DateTime.MinValue;
⋮----
private async void InitializeToggles()
⋮----
var radios = await Windows.Devices.Radios.Radio.GetRadiosAsync();
var wifi = radios.FirstOrDefault(r => r.Kind == Windows.Devices.Radios.RadioKind.WiFi);
var bt = radios.FirstOrDefault(r => r.Kind == Windows.Devices.Radios.RadioKind.Bluetooth);
⋮----
using var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
if (key != null && key.GetValue("AppsUseLightTheme") is int val) IsDarkModeEnabled = val == 0;
⋮----
using var qhKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Policies\Microsoft\Windows\CurrentVersion\QuietHours");
if (qhKey != null && qhKey.GetValue("EnableQuietHours") is int qh) IsDndEnabled = qh == 1;
⋮----
private async Task ToggleWifi()
⋮----
_suppressUsbNotificationsUntil = DateTime.Now.AddSeconds(5);
⋮----
await wifi.SetStateAsync(targetState);
⋮----
System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
⋮----
private async Task ToggleBluetooth()
⋮----
await bt.SetStateAsync(targetState);
⋮----
private void ToggleDarkMode()
⋮----
using var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(keyPath, true);
⋮----
key.SetValue("AppsUseLightTheme", newValue, Microsoft.Win32.RegistryValueKind.DWord);
key.SetValue("SystemUsesLightTheme", newValue, Microsoft.Win32.RegistryValueKind.DWord);
⋮----
private void ToggleDnd()
⋮----
using var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(qhPath, true);
⋮----
key.SetValue("EnableQuietHours", IsDndEnabled ? 1 : 0, Microsoft.Win32.RegistryValueKind.DWord);
⋮----
public void EnqueueNotification(NotificationRequest req)
⋮----
_notificationCts = new CancellationTokenSource();
⋮----
var list = _notificationQueue.ToList();
var idx = list.FindIndex(n => n.Id == req.Id);
⋮----
_notificationQueue.Clear();
foreach (var item in list) _notificationQueue.Enqueue(item);
⋮----
_notificationQueue.Enqueue(req);
⋮----
private async Task RunNotificationTimerAsync(TimeSpan duration, CancellationToken token)
⋮----
await Task.Delay(duration, token);
⋮----
await Task.Delay(200, token);
⋮----
private void ProcessNextNotification()
⋮----
var req = _notificationQueue.Dequeue();
⋮----
private Task PreviousAsync() => _media.PreviousAsync();
⋮----
private Task PlayPauseAsync()
⋮----
return wasPlaying ? _media.PauseAsync() : _media.PlayAsync();
⋮----
private Task NextAsync() => _media.NextAsync();
⋮----
private void OnMediaChanged(object? sender, MediaChangedEventArgs e) =>
_ui.Post(_ => ApplyMedia(e.Media, e.TrackChanged), null);
⋮----
private void ApplyMedia(MediaInfo? media, bool trackChanged)
⋮----
Artist = media is null ? EmptyArtist : (string.IsNullOrWhiteSpace(media.Artist) ? media.AlbumTitle : media.Artist);
⋮----
_progressTimer.Change(500, 500);
⋮----
_progressTimer.Change(Timeout.Infinite, Timeout.Infinite);
⋮----
private void OnProgressTick(object? state) => _ui.Post(_ => UpdateProgress(), null);
⋮----
private void UpdateProgress()
⋮----
private static string FormatTimeSpan(TimeSpan t) =>
⋮----
private static ImageSource? CreateBitmap(byte[]? bytes)
⋮----
var bmp = new BitmapImage();
bmp.BeginInit();
⋮----
bmp.StreamSource = new MemoryStream(bytes);
bmp.EndInit();
bmp.Freeze();
⋮----
private void OnMinuteChanged(object? sender, DateTime now) =>
_ui.Post(_ => TimeText = now.ToString("h:mm tt"), null);
⋮----
private void OnFullscreenChanged(object? sender, bool isFullscreen) =>
⋮----
private void OnBatteryChanged(object? sender, EventArgs e) =>
⋮----
EnqueueNotification(new NotificationRequest("battery", BatteryGlyph, text, brush, TimeSpan.FromSeconds(2.5)));
⋮----
private void OnWeatherChanged(object? sender, EventArgs e) =>
⋮----
private void OnUsbDeviceChanged(object? sender, UsbDeviceEventArgs e) =>
⋮----
System.Diagnostics.Debug.WriteLine($"[USB] event received: {info.FriendlyName} ({info.IsConnected})");
⋮----
text = string.IsNullOrWhiteSpace(info.Details) ? $"{devName}  {info.Type}" : $"{devName}  {info.Details}";
⋮----
System.Diagnostics.Debug.WriteLine($"[USB] Notification requested");
EnqueueNotification(new NotificationRequest("usb_" + info.DeviceId, info.Glyph, text, brush, TimeSpan.FromSeconds(3.5)));
⋮----
private void OnDownloadsChanged(object? sender, EventArgs e) =>
⋮----
List<DownloadEvent> active = _downloads.GetActiveDownloads().ToList();
HasDownloads = active.Any();
⋮----
ActiveDownloadsList.Clear();
foreach(var d in active) ActiveDownloadsList.Add(d);
⋮----
EnqueueNotification(new NotificationRequest("download_complete", "\xE896", $"✓ {d.filename}", (System.Windows.Media.Brush)System.Windows.Application.Current.Resources["AccentBrush"], TimeSpan.FromSeconds(3)));
⋮----
EnqueueNotification(new NotificationRequest("download_error", "\xE896", $"✗ {d.filename}", (System.Windows.Media.Brush)System.Windows.Application.Current.Resources["DangerBrush"], TimeSpan.FromSeconds(3)));
⋮----
int totalProgress = (int)active.Average(x => x.progress);
⋮----
EnqueueNotification(new NotificationRequest("download_start", "\xE896", "↓ Descargando...", brush, TimeSpan.FromSeconds(2.5)));
⋮----
private void OnHardwareChanged(object? sender, HardwareChangedEventArgs e) =>
⋮----
private void OnNotificationReceived(object? sender, NotificationEventArgs e) =>
⋮----
if (e.AppName.Contains("Discord", StringComparison.OrdinalIgnoreCase)) glyph = "\xE909";
if (e.AppName.Contains("WhatsApp", StringComparison.OrdinalIgnoreCase)) glyph = "\xE8F3";
⋮----
string text = string.IsNullOrWhiteSpace(e.Body) ? e.Title : $"{e.Title}: {e.Body}";
⋮----
if (text.Length > 40) text = text.Substring(0, 37) + "...";
⋮----
EnqueueNotification(new NotificationRequest("win_notif_" + e.AppName, glyph, text, brush, TimeSpan.FromSeconds(4)));
⋮----
public void Dispose()
````

## File: src/LockNotch/App.xaml.cs
````csharp
public partial class App : System.Windows.Application
⋮----
protected override async void OnStartup(StartupEventArgs e)
⋮----
if (e.Args.Any(arg => arg.StartsWith("chrome-extension://")))
⋮----
NativeMessagingProxy.Run();
⋮----
_singleInstance = new Mutex(true, "LockNotch.SingleInstance", out bool isFirst);
⋮----
base.OnStartup(e);
⋮----
_settings = new SettingsService();
⋮----
_clock = new ClockService();
_fullscreen = new FullscreenService();
_media = new MediaService();
_battery = new BatteryService();
_weather = new WeatherService(_settings);
_usb = new UsbService();
_hardware = new HardwareService(_settings);
_notifications = new NotificationService();
_downloads = new DownloadService();
_viewModel = new IslandViewModel(_clock, _fullscreen, _media, _battery, _weather, _usb, _hardware, _notifications, _settings, _downloads);
⋮----
LockNotch.Helpers.AppearanceManager.Apply(_settings.Current.Theme, _settings.Current.FontFamily);
⋮----
var window = new MainWindow(_viewModel);
⋮----
window.Show();
⋮----
await _viewModel.StartAsync();
await _notifications.InitializeAsync();
⋮----
_settings.Save();
⋮----
_downloads.Start();
⋮----
private void InitializeTrayIcon()
⋮----
var iconUri = new Uri("pack://application:,,,/Resources/icon.ico");
var streamInfo = System.Windows.Application.GetResourceStream(iconUri);
⋮----
contextMenu.Items.Add(settingsItem);
contextMenu.Items.Add("-");
contextMenu.Items.Add(exitItem);
⋮----
private void ShowSettingsWindow()
⋮----
w.Activate();
⋮----
var sw = new SettingsWindow(_settings, _downloads);
sw.Show();
⋮----
protected override void OnExit(ExitEventArgs e)
⋮----
_notifyIcon.Dispose();
⋮----
base.OnExit(e);
````

## File: src/LockNotch/MainWindow.xaml.cs
````csharp
public partial class MainWindow : Window
⋮----
private readonly IslandViewModel _vm;
private readonly Storyboard _expand;
private readonly Storyboard _collapse;
private readonly Storyboard _notify;
private readonly Storyboard _discSpin;
private readonly Storyboard _visualizerAnim;
⋮----
private IntPtr _hwnd;
⋮----
var spinAnim = new DoubleAnimation(0, 360, TimeSpan.FromSeconds(6.7)) { RepeatBehavior = RepeatBehavior.Forever };
Storyboard.SetTargetName(spinAnim, "DiscRotation");
Storyboard.SetTargetProperty(spinAnim, new PropertyPath("Angle"));
_discSpin = new Storyboard();
_discSpin.Children.Add(spinAnim);
⋮----
_visualizerAnim = new Storyboard();
var rnd = new Random();
⋮----
var anim = new DoubleAnimation
⋮----
To = rnd.Next(10, 16),
Duration = TimeSpan.FromMilliseconds(rnd.Next(250, 450)),
⋮----
EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
⋮----
Storyboard.SetTargetName(anim, $"Bar{i}");
Storyboard.SetTargetProperty(anim, new PropertyPath("Height"));
_visualizerAnim.Children.Add(anim);
⋮----
private void OnSourceInitialized(object? sender, EventArgs e)
⋮----
_hwnd = new WindowInteropHelper(this).Handle;
WindowHelper.MakeNoActivateToolWindow(_hwnd);
HwndSource.FromHwnd(_hwnd)?.AddHook(WndProc);
WindowHelper.PositionTopCenterOnPrimary(_hwnd);
⋮----
private DateTime _lastPageTurn = DateTime.MinValue;
private readonly TimeSpan _pageTurnCooldown = TimeSpan.FromMilliseconds(350);
⋮----
private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
⋮----
return new IntPtr(NativeMethods.MA_NOACTIVATE);
⋮----
if (Math.Abs(_scrollAccumulator) >= ScrollThreshold)
⋮----
if (_scrollAccumulator > 0) _vm.NextPage();
else _vm.PreviousPage();
⋮----
if (_scrollAccumulator < 0) _vm.NextPage();
⋮----
private void SchedulePosition() =>
Dispatcher.BeginInvoke(() => WindowHelper.PositionTopCenterOnPrimary(_hwnd), DispatcherPriority.Background);
⋮----
private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
⋮----
Storyboard sb = _vm.State switch
⋮----
sb.Begin(this, HandoffBehavior.SnapshotAndReplace);
⋮----
_discSpin.Begin(this, true);
_visualizerAnim.Begin(this, true);
⋮----
_discSpin.Resume(this);
_visualizerAnim.Resume(this);
⋮----
_discSpin.Pause(this);
_visualizerAnim.Pause(this);
⋮----
private void SetHidden(bool hidden)
⋮----
var fadeOut = new DoubleAnimation(0, TimeSpan.FromMilliseconds(200));
⋮----
Root.BeginAnimation(OpacityProperty, fadeOut);
⋮----
Root.BeginAnimation(OpacityProperty, new DoubleAnimation(1, TimeSpan.FromMilliseconds(250)));
⋮----
private void Island_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e) => _vm.PointerEntered();
⋮----
private void Island_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e) => _vm.PointerExited();
⋮----
private void Island_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) => _vm.ExpandCommand.Execute(null);
⋮----
private void Exit_Click(object sender, RoutedEventArgs e) => System.Windows.Application.Current.Shutdown();
⋮----
private void UpdateCarousel()
⋮----
DiscHost.BeginAnimation(OpacityProperty, new DoubleAnimation(targetOpacity, TimeSpan.FromMilliseconds(250)));
⋮----
var animThumb = new DoubleAnimation(_vm.CurrentPageIndex * thumbWidth, TimeSpan.FromMilliseconds(300))
⋮----
EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
⋮----
thumbTransform.BeginAnimation(TranslateTransform.XProperty, animThumb);
⋮----
private void UpdateExpandedSize()
⋮----
var widthAnim = new DoubleAnimation(targetWidth, TimeSpan.FromMilliseconds(450)) { EasingFunction = new BackEase { EasingMode = EasingMode.EaseOut, Amplitude = 0.28 } };
var heightAnim = new DoubleAnimation(targetHeight, TimeSpan.FromMilliseconds(450)) { EasingFunction = new BackEase { EasingMode = EasingMode.EaseOut, Amplitude = 0.28 } };
var radiusAnim = new DoubleAnimation(targetRadius, TimeSpan.FromMilliseconds(450)) { EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut } };
var marginAnim = new ThicknessAnimation(new Thickness(0, targetDiscY, 0, 0), TimeSpan.FromMilliseconds(450)) { EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut } };
⋮----
Island.BeginAnimation(WidthProperty, widthAnim);
Island.BeginAnimation(HeightProperty, heightAnim);
IslandBorder.BeginAnimation(Helpers.NotchHelper.BottomRadiusProperty, radiusAnim);
DiscHost.BeginAnimation(MarginProperty, marginAnim);
⋮----
private void AnimatePanel(UIElement panel, int panelIndex)
⋮----
var anim = new DoubleAnimation(targetX, TimeSpan.FromMilliseconds(350))
⋮----
transform.BeginAnimation(TranslateTransform.XProperty, anim);
````

## File: src/LockNotch/MainWindow.xaml
````
<Window x:Class="LockNotch.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:vm="clr-namespace:LockNotch.ViewModels"
        xmlns:helpers="clr-namespace:LockNotch.Helpers"
        xmlns:controls="clr-namespace:LockNotch.Controls"
        xmlns:conv="clr-namespace:LockNotch.Converters"
        xmlns:iconPacks="http://metro.mahapps.com/winfx/xaml/iconpacks"
        mc:Ignorable="d"
        d:DataContext="{d:DesignInstance Type=vm:IslandViewModel}"
        Title="LockNotch"
        Width="600" Height="350"
        WindowStyle="None"
        AllowsTransparency="True"
        Background="Transparent"
        Topmost="True"
        ShowInTaskbar="False"
        ResizeMode="NoResize"
        ShowActivated="False"
        Focusable="False"
        WindowStartupLocation="Manual"
        Left="0" Top="0"
        UseLayoutRounding="True"
        FontFamily="{DynamicResource UiFont}">

    <Window.Resources>
        <conv:BoolToStringConverter x:Key="PlayPauseGlyph" TrueValue="&#xE769;" FalseValue="&#xE768;" />

        <!-- Compacto -> Expandido: rebote suave estilo iOS -->
        <Storyboard x:Key="ExpandStoryboard">
            <DoubleAnimation Storyboard.TargetName="Island" Storyboard.TargetProperty="Width"
                             To="420" Duration="0:0:0.55">
                <DoubleAnimation.EasingFunction>
                    <BackEase EasingMode="EaseOut" Amplitude="0.28" />
                </DoubleAnimation.EasingFunction>
            </DoubleAnimation>
            <DoubleAnimation Storyboard.TargetName="Island" Storyboard.TargetProperty="Height"
                             To="170" Duration="0:0:0.55">
                <DoubleAnimation.EasingFunction>
                    <BackEase EasingMode="EaseOut" Amplitude="0.28" />
                </DoubleAnimation.EasingFunction>
            </DoubleAnimation>
            <DoubleAnimation Storyboard.TargetName="IslandBorder"
                             Storyboard.TargetProperty="(helpers:NotchHelper.BottomRadius)"
                             To="36" Duration="0:0:0.45">
                <DoubleAnimation.EasingFunction>
                    <CubicEase EasingMode="EaseOut" />
                </DoubleAnimation.EasingFunction>
            </DoubleAnimation>
            <DoubleAnimation Storyboard.TargetName="CompactContent" Storyboard.TargetProperty="Opacity"
                             To="0" Duration="0:0:0.12" />
            <DoubleAnimation Storyboard.TargetName="NotificationContent" Storyboard.TargetProperty="Opacity"
                             To="0" Duration="0:0:0.12" />
            <DoubleAnimation Storyboard.TargetName="ExpandedContent" Storyboard.TargetProperty="Opacity"
                             To="1" BeginTime="0:0:0.15" Duration="0:0:0.3" />
            <DoubleAnimation Storyboard.TargetName="ExpandedContent"
                             Storyboard.TargetProperty="(UIElement.RenderTransform).(ScaleTransform.ScaleX)"
                             To="1" BeginTime="0:0:0.08" Duration="0:0:0.45">
                <DoubleAnimation.EasingFunction>
                    <BackEase EasingMode="EaseOut" Amplitude="0.3" />
                </DoubleAnimation.EasingFunction>
            </DoubleAnimation>
            <DoubleAnimation Storyboard.TargetName="ExpandedContent"
                             Storyboard.TargetProperty="(UIElement.RenderTransform).(ScaleTransform.ScaleY)"
                             To="1" BeginTime="0:0:0.08" Duration="0:0:0.45">
                <DoubleAnimation.EasingFunction>
                    <BackEase EasingMode="EaseOut" Amplitude="0.3" />
                </DoubleAnimation.EasingFunction>
            </DoubleAnimation>
            <!-- El vinilo se desliza desde el borde superior -->
            <DoubleAnimation Storyboard.TargetName="DiscHost"
                             Storyboard.TargetProperty="(UIElement.RenderTransform).(TranslateTransform.Y)"
                             To="0" BeginTime="0:0:0.12" Duration="0:0:0.6">
                <DoubleAnimation.EasingFunction>
                    <CubicEase EasingMode="EaseOut" />
                </DoubleAnimation.EasingFunction>
            </DoubleAnimation>
        </Storyboard>

        <!-- Expandido -> Compacto -->
        <Storyboard x:Key="CollapseStoryboard">
            <DoubleAnimation Storyboard.TargetName="Island" Storyboard.TargetProperty="Width"
                             To="240" Duration="0:0:0.4">
                <DoubleAnimation.EasingFunction>
                    <CubicEase EasingMode="EaseInOut" />
                </DoubleAnimation.EasingFunction>
            </DoubleAnimation>
            <DoubleAnimation Storyboard.TargetName="Island" Storyboard.TargetProperty="Height"
                             To="38" Duration="0:0:0.4">
                <DoubleAnimation.EasingFunction>
                    <CubicEase EasingMode="EaseInOut" />
                </DoubleAnimation.EasingFunction>
            </DoubleAnimation>
            <DoubleAnimation Storyboard.TargetName="IslandBorder"
                             Storyboard.TargetProperty="(helpers:NotchHelper.BottomRadius)"
                             To="19" Duration="0:0:0.4">
                <DoubleAnimation.EasingFunction>
                    <CubicEase EasingMode="EaseInOut" />
                </DoubleAnimation.EasingFunction>
            </DoubleAnimation>
            <DoubleAnimation Storyboard.TargetName="ExpandedContent" Storyboard.TargetProperty="Opacity"
                             To="0" Duration="0:0:0.12" />
            <DoubleAnimation Storyboard.TargetName="ExpandedContent"
                             Storyboard.TargetProperty="(UIElement.RenderTransform).(ScaleTransform.ScaleX)"
                             To="0.9" Duration="0:0:0.2" />
            <DoubleAnimation Storyboard.TargetName="ExpandedContent"
                             Storyboard.TargetProperty="(UIElement.RenderTransform).(ScaleTransform.ScaleY)"
                             To="0.9" Duration="0:0:0.2" />
            <DoubleAnimation Storyboard.TargetName="DiscHost"
                             Storyboard.TargetProperty="(UIElement.RenderTransform).(TranslateTransform.Y)"
                             To="-115" Duration="0:0:0.25" />
            <DoubleAnimation Storyboard.TargetName="CompactContent" Storyboard.TargetProperty="Opacity"
                             To="1" BeginTime="0:0:0.22" Duration="0:0:0.2" />
            <DoubleAnimation Storyboard.TargetName="NotificationContent" Storyboard.TargetProperty="Opacity"
                             To="0" Duration="0:0:0.12" />
        </Storyboard>

        <Storyboard x:Key="NotifyStoryboard">
            <DoubleAnimation Storyboard.TargetName="Island" Storyboard.TargetProperty="Width"
                             To="280" Duration="0:0:0.3">
                <DoubleAnimation.EasingFunction>
                    <CubicEase EasingMode="EaseOut" />
                </DoubleAnimation.EasingFunction>
            </DoubleAnimation>
            <DoubleAnimation Storyboard.TargetName="Island" Storyboard.TargetProperty="Height"
                             To="38" Duration="0:0:0.3">
                <DoubleAnimation.EasingFunction>
                    <CubicEase EasingMode="EaseOut" />
                </DoubleAnimation.EasingFunction>
            </DoubleAnimation>
            <DoubleAnimation Storyboard.TargetName="IslandBorder"
                             Storyboard.TargetProperty="(helpers:NotchHelper.BottomRadius)"
                             To="19" Duration="0:0:0.3">
                <DoubleAnimation.EasingFunction>
                    <CubicEase EasingMode="EaseOut" />
                </DoubleAnimation.EasingFunction>
            </DoubleAnimation>
            <DoubleAnimation Storyboard.TargetName="CompactContent" Storyboard.TargetProperty="Opacity"
                             To="0" Duration="0:0:0.12" />
            <DoubleAnimation Storyboard.TargetName="ExpandedContent" Storyboard.TargetProperty="Opacity"
                             To="0" Duration="0:0:0.12" />
            <DoubleAnimation Storyboard.TargetName="NotificationContent" Storyboard.TargetProperty="Opacity"
                             To="1" BeginTime="0:0:0.15" Duration="0:0:0.2" />
        </Storyboard>
    </Window.Resources>

    <!-- Root: área transparente (los clics la atraviesan); solo la isla es interactiva -->
    <Grid x:Name="Root">
        <Grid x:Name="Island"
              Width="240" Height="38"
              HorizontalAlignment="Center" VerticalAlignment="Top"
              MouseEnter="Island_MouseEnter"
              MouseLeave="Island_MouseLeave"
              MouseLeftButtonUp="Island_MouseLeftButtonUp">

            <Grid.ContextMenu>
                <ContextMenu>
                    <MenuItem Header="Salir de LockNotch" Click="Exit_Click" />
                </ContextMenu>
            </Grid.ContextMenu>

            <!-- "Orejas" cóncavas que funden el notch con el borde superior de la pantalla -->
            <Path HorizontalAlignment="Left" VerticalAlignment="Top"
                  Margin="-12,0,0,0" Width="12" Height="12"
                  Fill="{DynamicResource IslandBrush}"
                  Data="M0,0 L12,0 L12,12 A12,12 0 0 0 0,0 Z" />
            <Path HorizontalAlignment="Right" VerticalAlignment="Top"
                  Margin="0,0,-12,0" Width="12" Height="12"
                  Fill="{DynamicResource IslandBrush}"
                  Data="M12,0 L0,0 L0,12 A12,12 0 0 1 12,0 Z" />

            <!-- Sombra en capa separada para no rasterizar el contenido -->
            <Border Background="{DynamicResource IslandBrush}"
                    CornerRadius="{Binding CornerRadius, ElementName=IslandBorder}">
                <Border.Effect>
                    <DropShadowEffect Color="Black" BlurRadius="28" ShadowDepth="6"
                                      Direction="270" Opacity="0.55" />
                </Border.Effect>
            </Border>

            <Border x:Name="IslandBorder"
                    Background="{DynamicResource IslandBrush}"
                    helpers:NotchHelper.BottomRadius="19">
                <Grid>
                    <!-- ===== Compacto ===== -->
                    <Grid x:Name="CompactContent" Height="38" VerticalAlignment="Top" Margin="18,0">
                        
                        <!-- Vista Default (Reloj, Clima, Batería) -->
                        <Grid>
                            <Grid.Style>
                                <Style TargetType="Grid">
                                    <Style.Triggers>
                                        <MultiDataTrigger>
                                            <MultiDataTrigger.Conditions>
                                                <Condition Binding="{Binding HasMedia}" Value="True" />
                                                <Condition Binding="{Binding IsPlaying}" Value="True" />
                                            </MultiDataTrigger.Conditions>
                                            <Setter Property="Visibility" Value="Collapsed" />
                                        </MultiDataTrigger>
                                        <DataTrigger Binding="{Binding HasDownloads}" Value="True">
                                            <Setter Property="Visibility" Value="Collapsed" />
                                        </DataTrigger>
                                    </Style.Triggers>
                                </Style>
                            </Grid.Style>
                            
                            <Grid.ColumnDefinitions>
                                <ColumnDefinition Width="Auto" />
                                <ColumnDefinition Width="*" />
                                <ColumnDefinition Width="Auto" />
                            </Grid.ColumnDefinitions>

                            <Viewbox Grid.Column="0" Stretch="Uniform" StretchDirection="DownOnly" MaxWidth="110" HorizontalAlignment="Left">
                                <StackPanel Orientation="Horizontal" VerticalAlignment="Center">
                                    <TextBlock Text="{Binding TimeText}"
                                               Foreground="{DynamicResource TextPrimaryBrush}"
                                               FontSize="14" FontWeight="SemiBold" Margin="0,0,10,0"/>
                                               
                                    <!-- Clima -->
                                    <TextBlock Text="{Binding WeatherGlyph}"
                                               FontFamily="Segoe UI Emoji"
                                               Foreground="{DynamicResource TextPrimaryBrush}"
                                               FontSize="14" Margin="0,0,6,0"/>
                                    <TextBlock Text="{Binding WeatherText}"
                                               Foreground="{DynamicResource TextSecondaryBrush}"
                                               FontSize="13" FontWeight="SemiBold"/>
                                </StackPanel>
                            </Viewbox>

                            <!-- Espaciador flexible para asegurar que nunca se toquen -->
                            <Border Grid.Column="1" MinWidth="10" />

                            <Viewbox Grid.Column="2" Stretch="Uniform" StretchDirection="DownOnly" MaxWidth="70" HorizontalAlignment="Right">
                                <StackPanel Orientation="Horizontal" VerticalAlignment="Center">
                                    <TextBlock Text="{Binding BatteryText}"
                                               Foreground="{DynamicResource TextPrimaryBrush}"
                                               FontSize="13" FontWeight="SemiBold"
                                               VerticalAlignment="Center" Margin="0,0,6,0" />
                                    <TextBlock Text="{Binding BatteryGlyph}" FontSize="16">
                                        <TextBlock.Style>
                                            <Style TargetType="TextBlock" BasedOn="{StaticResource IconText}">
                                                <Style.Triggers>
                                                    <DataTrigger Binding="{Binding IsBatteryLow}" Value="True">
                                                        <Setter Property="Foreground" Value="{DynamicResource DangerBrush}" />
                                                    </DataTrigger>
                                                    <DataTrigger Binding="{Binding IsCharging}" Value="True">
                                                        <Setter Property="Foreground" Value="{DynamicResource AccentBrush}" />
                                                    </DataTrigger>
                                                </Style.Triggers>
                                            </Style>
                                        </TextBlock.Style>
                                    </TextBlock>
                                </StackPanel>
                            </Viewbox>
                        </Grid>

                        <!-- Vista Media (Portada a la izquierda, Animación a la derecha) -->
                        <Grid>
                            <Grid.Style>
                                <Style TargetType="Grid">
                                    <Setter Property="Visibility" Value="Collapsed" />
                                    <Style.Triggers>
                                        <MultiDataTrigger>
                                            <MultiDataTrigger.Conditions>
                                                <Condition Binding="{Binding HasMedia}" Value="True" />
                                                <Condition Binding="{Binding IsPlaying}" Value="True" />
                                            </MultiDataTrigger.Conditions>
                                            <Setter Property="Visibility" Value="Visible" />
                                        </MultiDataTrigger>
                                    </Style.Triggers>
                                </Style>
                            </Grid.Style>

                            <!-- Portada a la izquierda -->
                            <Border Width="24" Height="24" CornerRadius="12" HorizontalAlignment="Left" VerticalAlignment="Center">
                                <Border.Background>
                                    <ImageBrush ImageSource="{Binding Thumbnail}" Stretch="UniformToFill" />
                                </Border.Background>
                                <Border.Style>
                                    <Style TargetType="Border">
                                        <Style.Triggers>
                                            <DataTrigger Binding="{Binding Thumbnail}" Value="{x:Null}">
                                                <Setter Property="Background" Value="{DynamicResource ArtPlaceholderBrush}" />
                                            </DataTrigger>
                                        </Style.Triggers>
                                    </Style>
                                </Border.Style>
                            </Border>

                            <!-- Animación a la derecha (Ecualizador) -->
                            <StackPanel x:Name="Visualizer" Orientation="Horizontal" HorizontalAlignment="Right" VerticalAlignment="Center" Height="14">
                                <!-- Barras animadas -->
                                <Border x:Name="Bar1" Width="3" Height="6" Background="{DynamicResource VisualizerBrush}" CornerRadius="1.5" VerticalAlignment="Center" />
                                <Border x:Name="Bar2" Width="3" Height="10" Background="{DynamicResource VisualizerBrush}" CornerRadius="1.5" Margin="3,0" VerticalAlignment="Center" />
                                <Border x:Name="Bar3" Width="3" Height="14" Background="{DynamicResource VisualizerBrush}" CornerRadius="1.5" VerticalAlignment="Center" />
                                <Border x:Name="Bar4" Width="3" Height="8" Background="{DynamicResource VisualizerBrush}" CornerRadius="1.5" Margin="3,0,0,0" VerticalAlignment="Center" />
                            </StackPanel>
                        </Grid>

                        <!-- Vista Descargas -->
                        <Grid>
                            <Grid.Style>
                                <Style TargetType="Grid">
                                    <Setter Property="Visibility" Value="Collapsed" />
                                    <Style.Triggers>
                                        <MultiDataTrigger>
                                            <MultiDataTrigger.Conditions>
                                                <Condition Binding="{Binding HasDownloads}" Value="True" />
                                            </MultiDataTrigger.Conditions>
                                            <Setter Property="Visibility" Value="Visible" />
                                        </MultiDataTrigger>
                                        <MultiDataTrigger>
                                            <MultiDataTrigger.Conditions>
                                                <Condition Binding="{Binding HasDownloads}" Value="True" />
                                                <Condition Binding="{Binding HasMedia}" Value="True" />
                                                <Condition Binding="{Binding IsPlaying}" Value="True" />
                                            </MultiDataTrigger.Conditions>
                                            <Setter Property="Visibility" Value="Collapsed" />
                                        </MultiDataTrigger>
                                    </Style.Triggers>
                                </Style>
                            </Grid.Style>

                            <StackPanel Orientation="Horizontal" HorizontalAlignment="Left" VerticalAlignment="Center">
                                <TextBlock Text="&#xE896;" FontFamily="{DynamicResource IconFont}" Foreground="{DynamicResource AccentBrush}" FontSize="16" Margin="0,0,8,0" VerticalAlignment="Center" />
                                <TextBlock Text="{Binding DownloadsCompactText}" Foreground="{DynamicResource TextPrimaryBrush}" FontSize="13" FontWeight="SemiBold" VerticalAlignment="Center" />
                            </StackPanel>

                            <ProgressBar HorizontalAlignment="Right" VerticalAlignment="Center" Width="60" Height="4"
                                         helpers:ProgressBarExtensions.SmoothValue="{Binding DownloadsProgressValue}" Maximum="100" BorderThickness="0"
                                         Foreground="{DynamicResource ProgressFillBrush}" Background="{DynamicResource ProgressTrackBrush}">
                                <ProgressBar.Template>
                                    <ControlTemplate TargetType="ProgressBar">
                                        <Grid x:Name="TemplateRoot">
                                            <Border Background="{TemplateBinding Background}" CornerRadius="2" />
                                            <Border x:Name="PART_Track">
                                                <Border x:Name="PART_Indicator" Background="{TemplateBinding Foreground}" CornerRadius="2" HorizontalAlignment="Left" />
                                            </Border>
                                        </Grid>
                                    </ControlTemplate>
                                </ProgressBar.Template>
                            </ProgressBar>
                        </Grid>
                    </Grid>

                    <!-- ===== Notificación ===== -->
                    <Grid x:Name="NotificationContent" Height="38" VerticalAlignment="Top" Margin="18,0" Opacity="0" IsHitTestVisible="False">
                        <Grid HorizontalAlignment="Center" VerticalAlignment="Center">
                            <Grid.ColumnDefinitions>
                                <ColumnDefinition Width="Auto" />
                                <ColumnDefinition Width="*" MaxWidth="210" />
                            </Grid.ColumnDefinitions>
                            <Grid Grid.Column="0" Margin="0,0,8,0" VerticalAlignment="Center">
                                <TextBlock Text="{Binding CurrentNotification.Glyph}"
                                           Foreground="{Binding CurrentNotification.Foreground}"
                                           FontSize="16">
                                    <TextBlock.Style>
                                        <Style TargetType="TextBlock" BasedOn="{StaticResource IconText}">
                                            <Setter Property="Visibility" Value="Visible" />
                                            <Style.Triggers>
                                                <DataTrigger Binding="{Binding CurrentNotification.MaterialIcon}" Value="{x:Null}">
                                                    <Setter Property="Visibility" Value="Visible" />
                                                </DataTrigger>
                                                <DataTrigger Binding="{Binding CurrentNotification.MaterialIcon, FallbackValue=''}" Value="">
                                                    <Setter Property="Visibility" Value="Visible" />
                                                </DataTrigger>
                                            </Style.Triggers>
                                        </Style>
                                    </TextBlock.Style>
                                </TextBlock>
                                <iconPacks:PackIconMaterial Kind="{Binding CurrentNotification.MaterialIcon, FallbackValue=None}"
                                                            Foreground="{Binding CurrentNotification.Foreground}"
                                                            Width="16" Height="16">
                                    <iconPacks:PackIconMaterial.Style>
                                        <Style TargetType="iconPacks:PackIconMaterial">
                                            <Setter Property="Visibility" Value="Visible" />
                                            <Style.Triggers>
                                                <DataTrigger Binding="{Binding CurrentNotification.MaterialIcon}" Value="{x:Null}">
                                                    <Setter Property="Visibility" Value="Collapsed" />
                                                </DataTrigger>
                                            </Style.Triggers>
                                        </Style>
                                    </iconPacks:PackIconMaterial.Style>
                                </iconPacks:PackIconMaterial>
                            </Grid>
                            <controls:MarqueeTextBlock Grid.Column="1"
                                       Text="{Binding CurrentNotification.Text}"
                                       IsMarqueeEnabled="{Binding CurrentNotification.UseMarquee}"
                                       Foreground="{DynamicResource TextPrimaryBrush}"
                                       FontSize="14" FontWeight="SemiBold" VerticalAlignment="Center" />
                        </Grid>
                    </Grid>

                    <!-- ===== Expandido ===== -->
                    <Grid x:Name="ExpandedContent"
                          VerticalAlignment="Stretch"
                          Opacity="0"
                          IsHitTestVisible="{Binding IsExpanded}"
                          RenderTransformOrigin="0.5,0">
                        <Grid.RenderTransform>
                            <ScaleTransform ScaleX="0.9" ScaleY="0.9" />
                        </Grid.RenderTransform>

                        <Grid x:Name="DiscHost" Width="260" Height="260"
                              HorizontalAlignment="Center" VerticalAlignment="Top"
                              Margin="0,-156,0,0" IsHitTestVisible="False">
                            <Grid.RenderTransform>
                                <TranslateTransform Y="-115" />
                            </Grid.RenderTransform>

                            <Grid RenderTransformOrigin="0.5,0.5">
                                <Grid.RenderTransform>
                                    <RotateTransform x:Name="DiscRotation" />
                                </Grid.RenderTransform>

                                <Ellipse Fill="{DynamicResource VinylBrush}" />
                                <!-- Surcos -->
                                <Ellipse Width="246" Height="246" Stroke="{DynamicResource VinylGroove1Brush}" StrokeThickness="0.8" />
                                <Ellipse Width="232" Height="232" Stroke="{DynamicResource VinylGroove2Brush}" StrokeThickness="0.8" />

                                <!-- Etiqueta con la portada -->
                                <Ellipse Width="232" Height="232" Fill="{DynamicResource ArtPlaceholderBrush}" />
                                <Ellipse Width="232" Height="232">
                                    <Ellipse.Fill>
                                        <ImageBrush ImageSource="{Binding Thumbnail}" Stretch="UniformToFill" />
                                    </Ellipse.Fill>
                                    <Ellipse.Style>
                                        <Style TargetType="Ellipse">
                                            <Style.Triggers>
                                                <DataTrigger Binding="{Binding Thumbnail}" Value="{x:Null}">
                                                    <Setter Property="Visibility" Value="Collapsed" />
                                                </DataTrigger>
                                            </Style.Triggers>
                                        </Style>
                                    </Ellipse.Style>
                                </Ellipse>
                                <Ellipse Width="232" Height="232" Stroke="{DynamicResource VinylOuterRimBrush}" StrokeThickness="2" />
                                <Ellipse Width="18" Height="18" Fill="{DynamicResource VinylHubBrush}" Stroke="{DynamicResource VinylHubRimBrush}" StrokeThickness="1" />
                            </Grid>

                            <!-- Reflejo fijo -->
                            <Ellipse Fill="{DynamicResource VinylSheenBrush}" />
                            <Ellipse Stroke="{DynamicResource VinylOuterRimBrush}" StrokeThickness="1" />
                        </Grid>

                        <!-- Zona de contenido deslizable (Con Clip) -->
                        <Grid ClipToBounds="True">
                            
                            <!-- Panel 0: Media Text -->
                            <Grid x:Name="PanelMedia">
                                <Grid.RenderTransform>
                                    <TranslateTransform X="0" />
                                </Grid.RenderTransform>

                                <StackPanel Margin="16,0,16,14" VerticalAlignment="Bottom">
                                    <!-- Artist & Title -->
                                    <TextBlock Text="{Binding Artist}"
                                               Foreground="{DynamicResource TextSecondaryBrush}"
                                               FontSize="12" Margin="0,0,0,2"
                                               TextTrimming="CharacterEllipsis" TextAlignment="Center" />
                                    <TextBlock Text="{Binding Title}"
                                               Foreground="{DynamicResource TextPrimaryBrush}"
                                               FontSize="15" FontWeight="Bold"
                                               TextTrimming="CharacterEllipsis" TextAlignment="Center" />

                                    <!-- Progress Bar & Times -->
                                    <Grid Margin="16,12,16,8">
                                        <Grid.Style>
                                            <Style TargetType="Grid">
                                                <Style.Triggers>
                                                    <DataTrigger Binding="{Binding HasProgress}" Value="False">
                                                        <Setter Property="Visibility" Value="Collapsed" />
                                                    </DataTrigger>
                                                </Style.Triggers>
                                            </Style>
                                        </Grid.Style>
                                        <Grid.ColumnDefinitions>
                                            <ColumnDefinition Width="Auto" />
                                            <ColumnDefinition Width="*" />
                                            <ColumnDefinition Width="Auto" />
                                        </Grid.ColumnDefinitions>

                                        <TextBlock Grid.Column="0" Text="{Binding PositionText}" 
                                                   Foreground="{DynamicResource TextTertiaryBrush}" 
                                                   FontSize="10" Margin="0,0,8,0" VerticalAlignment="Center" />

                                        <ProgressBar Grid.Column="1" Value="{Binding ProgressPercentage}" 
                                                     Maximum="100" Height="4" BorderThickness="0" VerticalAlignment="Center"
                                                     Foreground="{DynamicResource ProgressFillBrush}"
                                                     Background="{DynamicResource ProgressTrackBrush}">
                                            <ProgressBar.Template>
                                                <ControlTemplate TargetType="ProgressBar">
                                                    <Grid x:Name="TemplateRoot">
                                                        <Border Background="{TemplateBinding Background}" CornerRadius="2" />
                                                        <Border x:Name="PART_Track">
                                                            <Border x:Name="PART_Indicator" Background="{TemplateBinding Foreground}" CornerRadius="2" HorizontalAlignment="Left" />
                                                        </Border>
                                                    </Grid>
                                                </ControlTemplate>
                                            </ProgressBar.Template>
                                        </ProgressBar>

                                        <TextBlock Grid.Column="2" Text="{Binding DurationText}" 
                                                   Foreground="{DynamicResource TextTertiaryBrush}" 
                                                   FontSize="10" Margin="8,0,0,0" VerticalAlignment="Center" />
                                    </Grid>

                                    <!-- Minimalist Controls -->
                                    <StackPanel Orientation="Horizontal" HorizontalAlignment="Center" Margin="0,4,0,0">
                                        <Button Style="{DynamicResource MediaButton}" Content="&#xE892;"
                                                Command="{Binding PreviousCommand}" ToolTip="Anterior" 
                                                FontSize="14" Width="32" Height="32" Padding="0"/>
                                                
                                        <Button Style="{DynamicResource PlayButton}" Margin="24,0"
                                                Content="{Binding IsPlaying, Converter={StaticResource PlayPauseGlyph}}"
                                                Command="{Binding PlayPauseCommand}" ToolTip="Reproducir / Pausar" 
                                                FontSize="16" Width="40" Height="40" Padding="0"/>
                                                
                                        <Button Style="{DynamicResource MediaButton}" Content="&#xE893;"
                                                Command="{Binding NextCommand}" ToolTip="Siguiente" 
                                                FontSize="14" Width="32" Height="32" Padding="0"/>
                                    </StackPanel>
                                </StackPanel>
                            </Grid>


                            <!-- Panel 1: App Launcher -->
                            <Grid x:Name="PanelAppLauncher" Background="Transparent">
                                <Grid.RenderTransform>
                                    <TranslateTransform X="420" />
                                </Grid.RenderTransform>
                                <StackPanel VerticalAlignment="Center" HorizontalAlignment="Center">
                                    <TextBlock Text="Accesos Rápidos" Foreground="{DynamicResource TextPrimaryBrush}" FontSize="14" FontWeight="SemiBold" HorizontalAlignment="Center" Margin="0,0,0,16" />
                                    <StackPanel Orientation="Horizontal" HorizontalAlignment="Center">
                                        <Button Style="{DynamicResource MediaButton}" Content="&#xE718;" ToolTip="App 1" Command="{Binding LaunchAppCommand}" CommandParameter="1" Margin="0,0,16,0" />
                                        <Button Style="{DynamicResource MediaButton}" Content="&#xE756;" ToolTip="App 2" Command="{Binding LaunchAppCommand}" CommandParameter="2" Margin="0,0,16,0" />
                                        <Button Style="{DynamicResource MediaButton}" Content="&#xE753;" ToolTip="App 3" Command="{Binding LaunchAppCommand}" CommandParameter="3" Margin="0,0,16,0" />
                                        <Button Style="{DynamicResource MediaButton}" Content="&#xE72D;" ToolTip="App 4" Command="{Binding LaunchAppCommand}" CommandParameter="4" Margin="0,0,16,0" />
                                        <Button Style="{DynamicResource MediaButton}" Content="&#xE713;" ToolTip="App 5" Command="{Binding LaunchAppCommand}" CommandParameter="5" />
                                    </StackPanel>
                                </StackPanel>
                            </Grid>

                            <!-- Panel 2: Control Center -->
                            <Grid x:Name="PanelControlCenter" Background="Transparent">
                                <Grid.RenderTransform>
                                    <TranslateTransform X="840" />
                                </Grid.RenderTransform>
                                <StackPanel VerticalAlignment="Center" HorizontalAlignment="Center">
                                    <TextBlock Text="Centro de Control" Foreground="{DynamicResource TextPrimaryBrush}" FontSize="14" FontWeight="SemiBold" HorizontalAlignment="Center" Margin="0,0,0,16" />
                                    <StackPanel Orientation="Horizontal" HorizontalAlignment="Center">
                                        <ToggleButton Style="{DynamicResource ControlCenterToggle}" Content="&#xE704;" ToolTip="Wi-Fi" Command="{Binding ToggleWifiCommand}" IsChecked="{Binding IsWifiEnabled}" Margin="0,0,16,0" />
                                        <ToggleButton Style="{DynamicResource ControlCenterToggle}" Content="&#xE702;" ToolTip="Bluetooth" Command="{Binding ToggleBluetoothCommand}" IsChecked="{Binding IsBluetoothEnabled}" Margin="0,0,16,0" />
                                        <ToggleButton Style="{DynamicResource ControlCenterToggle}" Content="&#xE790;" ToolTip="Modo Oscuro" Command="{Binding ToggleDarkModeCommand}" IsChecked="{Binding IsDarkModeEnabled}" Margin="0,0,16,0" />
                                        <ToggleButton Style="{DynamicResource ControlCenterToggle}" Content="&#xE708;" ToolTip="No Molestar" Command="{Binding ToggleDndCommand}" IsChecked="{Binding IsDndEnabled}" />
                                    </StackPanel>
                                </StackPanel>
                            </Grid>

                            <!-- Panel 3: Hardware -->
                            <Grid x:Name="PanelHardware" Background="Transparent">
                                <Grid.RenderTransform>
                                    <TranslateTransform X="1260" />
                                </Grid.RenderTransform>
                                <StackPanel VerticalAlignment="Center" HorizontalAlignment="Center">
                                    <TextBlock Text="Telemetría del Sistema" Foreground="{DynamicResource TextPrimaryBrush}" FontSize="14" FontWeight="SemiBold" HorizontalAlignment="Center" Margin="0,0,0,16" />
                                    
                                    <StackPanel Orientation="Horizontal" HorizontalAlignment="Center">
                                        <!-- CPU -->
                                        <Border Background="{DynamicResource CardBrush}" CornerRadius="12" Padding="16,12" Margin="0,0,12,0" MinWidth="120">
                                            <StackPanel>
                                                <TextBlock Text="CPU Core" Foreground="{DynamicResource CardLabelBrush}" FontSize="12" />
                                                <TextBlock Text="{Binding CpuTemp}" Foreground="{DynamicResource CardValueBrush}" FontSize="26" FontWeight="Bold" Margin="0,4,0,0" />
                                            </StackPanel>
                                        </Border>
                                        <!-- GPU -->
                                        <Border Background="{DynamicResource CardBrush}" CornerRadius="12" Padding="16,12" MinWidth="120">
                                            <StackPanel>
                                                <TextBlock Text="GPU Core" Foreground="{DynamicResource CardLabelBrush}" FontSize="12" />
                                                <TextBlock Text="{Binding GpuTemp}" Foreground="{DynamicResource CardValueBrush}" FontSize="26" FontWeight="Bold" Margin="0,4,0,0" />
                                            </StackPanel>
                                        </Border>
                                    </StackPanel>
                                </StackPanel>
                            </Grid>
                        </Grid>

                            <!-- Panel 4: Descargas -->
                            <Grid x:Name="PanelDownloads" Background="Transparent">
                                <Grid.Style>
                                    <Style TargetType="Grid">
                                        <Setter Property="Visibility" Value="Collapsed" />
                                        <Style.Triggers>
                                            <DataTrigger Binding="{Binding HasDownloads}" Value="True">
                                                <Setter Property="Visibility" Value="Visible" />
                                            </DataTrigger>
                                        </Style.Triggers>
                                    </Style>
                                </Grid.Style>
                                <Grid.RenderTransform>
                                    <TranslateTransform X="1680" />
                                </Grid.RenderTransform>
                                <StackPanel VerticalAlignment="Center" HorizontalAlignment="Center" Margin="20,0">
                                    <TextBlock Text="Descargas de Chrome" Foreground="{DynamicResource TextPrimaryBrush}" FontSize="14" FontWeight="SemiBold" HorizontalAlignment="Center" Margin="0,0,0,12" />
                                    
                                    <!-- Use a fixed height or max height for list to avoid pushing layout -->
                                    <ItemsControl ItemsSource="{Binding ActiveDownloadsList}" MaxHeight="100" ScrollViewer.VerticalScrollBarVisibility="Hidden">
                                        <ItemsControl.Template>
                                            <ControlTemplate>
                                                <ScrollViewer VerticalScrollBarVisibility="Hidden">
                                                    <ItemsPresenter />
                                                </ScrollViewer>
                                            </ControlTemplate>
                                        </ItemsControl.Template>
                                        <ItemsControl.ItemTemplate>
                                            <DataTemplate>
                                                <Grid Margin="0,0,0,8" Width="360">
                                                    <Grid.ColumnDefinitions>
                                                        <ColumnDefinition Width="*" />
                                                        <ColumnDefinition Width="Auto" />
                                                    </Grid.ColumnDefinitions>
                                                    <Grid.RowDefinitions>
                                                        <RowDefinition Height="Auto" />
                                                        <RowDefinition Height="Auto" />
                                                    <RowDefinition Height="Auto" /></Grid.RowDefinitions><TextBlock Grid.Row="2" Grid.ColumnSpan="2" Text="{Binding DetailText}" Foreground="{DynamicResource TextSecondaryBrush}" FontSize="10" Margin="0,3,0,0" />
                                                    
                                                    <controls:MarqueeTextBlock Grid.Row="0" Grid.Column="0" Text="{Binding filename}" Foreground="{DynamicResource TextPrimaryBrush}" FontSize="12" Margin="0,0,8,0" />
                                                    <TextBlock Grid.Row="0" Grid.Column="1" Text="{Binding progress, StringFormat={}{0}%}" Foreground="{DynamicResource TextSecondaryBrush}" FontSize="12" />
                                                    
                                                    <ProgressBar Grid.Row="1" Grid.ColumnSpan="2" helpers:ProgressBarExtensions.SmoothValue="{Binding progress}" Maximum="100" Height="4" Margin="0,4,0,0"
                                                                 Foreground="{DynamicResource ProgressFillBrush}" Background="{DynamicResource ProgressTrackBrush}" BorderThickness="0">
                                                        <ProgressBar.Template>
                                                            <ControlTemplate TargetType="ProgressBar">
                                                                <Grid>
                                                                    <Border Background="{TemplateBinding Background}" CornerRadius="2" />
                                                                    <Border x:Name="PART_Track">
                                                                        <Border x:Name="PART_Indicator" Background="{TemplateBinding Foreground}" CornerRadius="2" HorizontalAlignment="Left" />
                                                                    </Border>
                                                                </Grid>
                                                            </ControlTemplate>
                                                        </ProgressBar.Template>
                                                    </ProgressBar>
                                                </Grid>
                                            </DataTemplate>
                                        </ItemsControl.ItemTemplate>
                                    </ItemsControl>
                                </StackPanel>
                            </Grid>

                        <!-- Paginator 4-Dots -->
                        <Border Width="72" Height="6" Background="{DynamicResource ProgressTrackBrush}" CornerRadius="3" HorizontalAlignment="Center" VerticalAlignment="Bottom" Margin="0,0,0,4">
                            <Border x:Name="PaginatorThumb" Width="18" Height="6" Background="{DynamicResource TextPrimaryBrush}" CornerRadius="3" HorizontalAlignment="Left">
                                <Border.RenderTransform>
                                    <TranslateTransform X="0" />
                                </Border.RenderTransform>
                            </Border>
                        </Border>
                    </Grid>
                </Grid>
            </Border>
        </Grid>
    </Grid>
</Window>
````
