export type Tone = "accent" | "primary" | "secondary" | "danger";

export interface NotificationRequest {
  /** Notificaciones con el mismo id se actualizan en vivo en lugar de encolarse. */
  id: string;
  glyph: string;
  text: string;
  tone: Tone;
  durationMs: number;
  /** Imagen opcional (p. ej. portada) en lugar del glifo. */
  image?: string | null;
}

export interface TimerApi {
  set(fn: () => void, ms: number): unknown;
  clear(handle: unknown): void;
}

const defaultTimers: TimerApi = {
  set: (fn, ms) => setTimeout(fn, ms),
  clear: (h) => clearTimeout(h as ReturnType<typeof setTimeout>),
};

/**
 * Cola de notificaciones de la isla. Misma semántica que EnqueueNotification del
 * original: si llega una con el mismo id que la visible se actualiza y se reinicia
 * su temporizador; si hay una en cola con el mismo id se reemplaza.
 */
export class NotificationQueue {
  private queue: NotificationRequest[] = [];
  private current: NotificationRequest | null = null;
  private timer: unknown = null;

  constructor(
    private readonly onChange: (current: NotificationRequest | null) => void,
    private readonly timers: TimerApi = defaultTimers,
    private readonly gapMs = 200,
  ) {}

  get visible(): NotificationRequest | null {
    return this.current;
  }

  get pending(): readonly NotificationRequest[] {
    return this.queue;
  }

  enqueue(req: NotificationRequest): void {
    if (this.current && this.current.id === req.id) {
      this.current = req;
      this.onChange(req);
      this.arm(req.durationMs, () => this.finishCurrent());
      return;
    }
    const idx = this.queue.findIndex((n) => n.id === req.id);
    if (idx >= 0) {
      this.queue[idx] = req;
      return;
    }
    this.queue.push(req);
    if (!this.current) this.next();
  }

  /** Descarta la notificación visible (p. ej. al expandir la isla manualmente). */
  dismiss(): void {
    if (this.current) this.finishCurrent();
  }

  clear(): void {
    this.queue = [];
    this.disarm();
    if (this.current) {
      this.current = null;
      this.onChange(null);
    }
  }

  private next(): void {
    this.disarm();
    const req = this.queue.shift() ?? null;
    this.current = req;
    this.onChange(req);
    if (req) this.arm(req.durationMs, () => this.finishCurrent());
  }

  private finishCurrent(): void {
    this.current = null;
    this.onChange(null);
    // Pausa breve para que termine la animación de salida antes de la siguiente.
    this.arm(this.gapMs, () => this.next());
  }

  private arm(ms: number, fn: () => void): void {
    this.disarm();
    this.timer = this.timers.set(() => {
      this.timer = null;
      fn();
    }, ms);
  }

  private disarm(): void {
    if (this.timer !== null) {
      this.timers.clear(this.timer);
      this.timer = null;
    }
  }
}
