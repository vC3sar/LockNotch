import type { TimerApi } from "./notificationQueue";

export type IslandMode = "hidden" | "notification" | "expanded" | "compact";

const defaultTimers: TimerApi = {
  set: (fn, ms) => setTimeout(fn, ms),
  clear: (h) => clearTimeout(h as ReturnType<typeof setTimeout>),
};

/**
 * Máquina de estados de la isla (región "Expand / collapse" del ViewModel original).
 * Prioridad: oculta > notificación > expandida > compacta.
 */
export class IslandController {
  expanded = false;
  notifying = false;
  hidden = false;
  private pointerOver = false;
  private collapseTimer: unknown = null;

  collapseDelayMs = 400;
  clickHoldMs = 3000;

  constructor(
    private readonly onChange: (mode: IslandMode) => void,
    private readonly timers: TimerApi = defaultTimers,
  ) {}

  get mode(): IslandMode {
    if (this.hidden) return "hidden";
    if (this.notifying) return "notification";
    if (this.expanded) return "expanded";
    return "compact";
  }

  get isPointerOver(): boolean {
    return this.pointerOver;
  }

  pointerEntered(): void {
    this.pointerOver = true;
    this.cancelCollapse();
    this.setExpanded(true);
  }

  pointerExited(): void {
    this.pointerOver = false;
    this.scheduleCollapse(this.collapseDelayMs);
  }

  /** Clic sobre la isla: expande y, si el puntero no está encima, contrae a los 3 s. */
  click(): void {
    this.cancelCollapse();
    this.setExpanded(true);
    if (!this.pointerOver) this.scheduleCollapse(this.clickHoldMs);
  }

  /** Expande durante un tiempo (cambio de canción). */
  expandTemporarily(ms: number): void {
    if (this.hidden) return;
    this.setExpanded(true);
    if (!this.pointerOver) this.scheduleCollapse(ms);
  }

  collapse(): void {
    this.cancelCollapse();
    this.setExpanded(false);
  }

  setHidden(hidden: boolean): void {
    const before = this.mode;
    if (hidden) {
      this.cancelCollapse();
      this.expanded = false;
    }
    this.hidden = hidden;
    this.emitIfChanged(before);
  }

  setNotifying(notifying: boolean): void {
    const before = this.mode;
    this.notifying = notifying;
    this.emitIfChanged(before);
  }

  private setExpanded(value: boolean): void {
    const before = this.mode;
    this.expanded = value;
    this.emitIfChanged(before);
  }

  private scheduleCollapse(ms: number): void {
    this.cancelCollapse();
    this.collapseTimer = this.timers.set(() => {
      this.collapseTimer = null;
      if (!this.pointerOver) this.setExpanded(false);
    }, ms);
  }

  private cancelCollapse(): void {
    if (this.collapseTimer !== null) {
      this.timers.clear(this.collapseTimer);
      this.collapseTimer = null;
    }
  }

  private emitIfChanged(before: IslandMode): void {
    const after = this.mode;
    if (after !== before) this.onChange(after);
  }
}

export type PageId = "media" | "apps" | "control" | "hardware" | "downloads";

/** Carrusel de páginas: 4 fijas + "descargas" cuando hay descargas activas. */
export class Pager {
  private index = 0;
  private hasDownloads = false;

  constructor(private readonly onChange: (page: PageId, index: number, count: number) => void) {}

  get pages(): PageId[] {
    const base: PageId[] = ["media", "apps", "control", "hardware"];
    return this.hasDownloads ? [...base, "downloads"] : base;
  }

  get count(): number {
    return this.pages.length;
  }

  get current(): PageId {
    return this.pages[this.index];
  }

  get currentIndex(): number {
    return this.index;
  }

  next(): void {
    this.go((this.index + 1) % this.count);
  }

  previous(): void {
    this.go((this.index - 1 + this.count) % this.count);
  }

  go(index: number): void {
    const clamped = Math.max(0, Math.min(this.count - 1, index));
    if (clamped === this.index) return;
    this.index = clamped;
    this.emit();
  }

  goTo(page: PageId): void {
    const i = this.pages.indexOf(page);
    if (i >= 0) this.go(i);
  }

  setHasDownloads(has: boolean): void {
    if (has === this.hasDownloads) return;
    this.hasDownloads = has;
    if (this.index >= this.count) this.index = this.count - 1;
    this.emit();
  }

  private emit(): void {
    this.onChange(this.current, this.index, this.count);
  }
}

/**
 * Acumulador de rueda con umbral y enfriamiento (equivalente al WndProc original:
 * 120 unidades y 350 ms entre cambios de página).
 */
export class WheelPager {
  private acc = 0;
  private last = -Infinity;

  constructor(
    private readonly threshold = 120,
    private readonly cooldownMs = 350,
  ) {}

  /** Devuelve +1 (siguiente), -1 (anterior) o 0. */
  feed(delta: number, now: number): number {
    this.acc += delta;
    if (Math.abs(this.acc) < this.threshold) return 0;
    const dir = this.acc > 0 ? 1 : -1;
    this.acc = 0;
    if (now - this.last < this.cooldownMs) return 0;
    this.last = now;
    return dir;
  }

  reset(): void {
    this.acc = 0;
  }
}
