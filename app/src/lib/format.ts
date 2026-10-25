import type { DownloadItem, MediaState } from "./types";

// Glifos de Segoe Fluent Icons / MDL2 (los mismos códigos que usaba la versión WPF).
export const Glyph = {
  play: "\uE768",
  pause: "\uE769",
  previous: "\uE892",
  next: "\uE893",
  download: "\uE896",
  location: "\uE81D",
  wifi: "\uE701",
  bluetooth: "\uE702",
  darkMode: "\uE708",
  dnd: "\uE7ED",
  settings: "\uE713",
  power: "\uE7E8",
  message: "\uE7E7",
  chat: "\uE8BD",
  phone: "\uE8EA",
  check: "\uE73E",
  error: "\uE783",
  cpu: "\uE950",
  gpu: "\uE7F8",
  app: "\uE71D",
  music: "\uE8D6",
} as const;

/** "m:ss" o "h:mm:ss" como FormatTimeSpan del original. */
export function formatDuration(ms: number): string {
  const total = Math.max(0, Math.floor(ms / 1000));
  const h = Math.floor(total / 3600);
  const m = Math.floor((total % 3600) / 60);
  const s = total % 60;
  const ss = s.toString().padStart(2, "0");
  return h >= 1 ? `${h}:${m.toString().padStart(2, "0")}:${ss}` : `${m}:${ss}`;
}

/** Posición actual extrapolada desde la última medición de Windows. */
export function currentPosition(media: MediaState, now: number = Date.now()): number {
  if (media.endMs <= 0) return 0;
  let pos = media.positionMs;
  if (media.isPlaying && media.lastUpdatedMs > 0) pos += now - media.lastUpdatedMs;
  return Math.min(media.endMs, Math.max(0, pos));
}

export function progressFraction(media: MediaState, now: number = Date.now()): number {
  return media.endMs > 0 ? currentPosition(media, now) / media.endMs : 0;
}

/** Mismo escalonado de iconos de batería que la versión original. */
export function batteryGlyph(percent: number, charging: boolean): string {
  if (charging) return "\uE83E";
  if (percent >= 95) return "\uE83F";
  if (percent >= 80) return "\uE850";
  if (percent >= 70) return "\uE858";
  if (percent >= 60) return "\uE857";
  if (percent >= 50) return "\uE856";
  if (percent >= 40) return "\uE855";
  if (percent >= 30) return "\uE854";
  if (percent >= 20) return "\uE853";
  if (percent >= 10) return "\uE852";
  return "\uE850";
}

export function batteryText(percent: number): string {
  return percent < 0 ? "--%" : `${percent}%`;
}

export interface ClockParts {
  time: string;
  period: string;
}

/** Hora en formato de 12 h (equivalente a "h:mm tt") respetando el idioma del sistema. */
export function clockParts(date: Date, locale?: string): ClockParts {
  const fmt = new Intl.DateTimeFormat(locale, { hour: "numeric", minute: "2-digit", hour12: true });
  let time = "";
  let period = "";
  for (const p of fmt.formatToParts(date)) {
    if (p.type === "dayPeriod") period += p.value;
    else if (p.type === "hour" || p.type === "minute" || (p.type === "literal" && p.value.includes(":"))) time += p.value;
  }
  return { time: time.trim(), period: period.trim() };
}

export const MB = 1048576;

export function speedText(speed: number): string {
  return speed >= MB ? `${(speed / MB).toFixed(1)} MB/s` : `${(speed / 1024).toFixed(0)} KB/s`;
}

export function isTerminal(d: DownloadItem): boolean {
  return d.type === "download_completed" || d.type === "download_interrupted" || d.type === "download_cancelled";
}

export function downloadDetail(d: DownloadItem): string {
  const active = d.state === "in_progress" || d.type === "download_progress" || d.type === "download_started";
  if (!active) return d.state === "complete" ? "Completado" : d.state === "interrupted" ? "Error" : "";
  return d.totalBytes > 0
    ? `${(d.bytesReceived / MB).toFixed(1)} / ${(d.totalBytes / MB).toFixed(1)} MB  ·  ${speedText(d.speed)}`
    : `${(d.bytesReceived / MB).toFixed(1)} MB  ·  ${speedText(d.speed)}`;
}

export interface DownloadSummary {
  compactText: string;
  title: string;
  progress: number;
  progressText: string;
}

/** Réplica de OnDownloadsChanged del ViewModel original. */
export function summarizeDownloads(active: DownloadItem[]): DownloadSummary {
  if (active.length === 0) {
    return { compactText: "Descargando...", title: "0 descargas", progress: 0, progressText: "" };
  }
  if (active.length === 1) {
    const d = active[0];
    const compactText =
      d.state === "complete"
        ? `✓ ${d.filename}`
        : d.totalBytes > 0
          ? `↓ ${d.progress}% · ${speedText(d.speed)}`
          : `↓ ${(d.bytesReceived / MB).toFixed(1)} MB · ${speedText(d.speed)}`;
    let progress = d.progress;
    let progressText =
      d.totalBytes > 0
        ? `${(d.bytesReceived / MB).toFixed(1)} MB / ${(d.totalBytes / MB).toFixed(1)} MB - ${(d.speed / MB).toFixed(1)} MB/s`
        : `${(d.bytesReceived / MB).toFixed(1)} MB - ${(d.speed / MB).toFixed(1)} MB/s`;
    if (d.state === "complete") {
      progress = 100;
      progressText = "Completado";
    } else if (d.state === "interrupted") {
      progressText = "Error";
    }
    return { compactText, title: d.filename, progress, progressText };
  }
  const avg = Math.floor(active.reduce((acc, x) => acc + (x.progress || 0), 0) / active.length);
  return {
    compactText: `↓ ${active.length} descargas`,
    title: `${active.length} descargas`,
    progress: avg,
    progressText: "Descargando múltiples archivos...",
  };
}

/** Recorta como el original (40 caracteres → 37 + "..."). */
export function truncate(text: string, max = 40): string {
  return text.length > max ? `${text.slice(0, max - 3)}...` : text;
}

export function notificationGlyph(appName: string): string {
  const n = appName.toLowerCase();
  if (n.includes("discord")) return Glyph.chat;
  if (n.includes("whatsapp")) return Glyph.phone;
  return Glyph.message;
}

export function notificationText(title: string, body: string): string {
  return truncate(body.trim() ? `${title}: ${body}` : title);
}

/** Convierte un valor de sensor en una fracción 0..1 para los medidores. */
export function gaugeFraction(value: number | null, kind: "temp" | "load" | "none"): number {
  if (value == null || kind === "none") return 0;
  if (kind === "load") return Math.min(1, Math.max(0, value / 100));
  // Temperatura: 30 °C → 0, 100 °C → 1
  return Math.min(1, Math.max(0, (value - 30) / 70));
}

/** Nivel semántico de un sensor para colorear el medidor. */
export function gaugeLevel(value: number | null, kind: "temp" | "load" | "none"): "ok" | "warm" | "hot" {
  if (value == null || kind === "none") return "ok";
  const [warm, hot] = kind === "temp" ? [70, 85] : [70, 90];
  return value >= hot ? "hot" : value >= warm ? "warm" : "ok";
}
