/**
 * Motor de muelles (spring physics) para la geometría de la isla.
 * Permite re-dirigir la animación a mitad de recorrido conservando la velocidad,
 * lo que da la sensación "líquida" de la Dynamic Island. El bucle rAF se detiene
 * por completo cuando todos los muelles están en reposo (0 % CPU en idle).
 */

export interface SpringConfig {
  stiffness: number;
  damping: number;
  mass: number;
}

export const SpringPresets = {
  /** Expansión con ligero rebote. */
  bouncy: { stiffness: 380, damping: 26, mass: 1 },
  /** Contracción firme, sin rebote apreciable. */
  snappy: { stiffness: 520, damping: 44, mass: 1 },
  /** Notificaciones. */
  gentle: { stiffness: 300, damping: 28, mass: 1 },
} satisfies Record<string, SpringConfig>;

export class Spring {
  value: number;
  velocity = 0;
  target: number;
  config: SpringConfig;

  constructor(initial: number, config: SpringConfig = SpringPresets.bouncy) {
    this.value = initial;
    this.target = initial;
    this.config = config;
  }

  /** Integra `dt` segundos con sub-pasos fijos (estable con frames irregulares). */
  step(dt: number): void {
    const h = 1 / 240;
    let remaining = Math.min(dt, 0.064);
    const { stiffness: k, damping: c, mass: m } = this.config;
    while (remaining > 0) {
      const s = Math.min(h, remaining);
      const force = -k * (this.value - this.target) - c * this.velocity;
      this.velocity += (force / m) * s;
      this.value += this.velocity * s;
      remaining -= s;
    }
  }

  get settled(): boolean {
    return Math.abs(this.velocity) < 0.5 && Math.abs(this.value - this.target) < 0.25;
  }

  snap(): void {
    this.value = this.target;
    this.velocity = 0;
  }
}

export type FrameScheduler = {
  request(cb: (t: number) => void): number;
  cancel(id: number): void;
};

const rafScheduler: FrameScheduler = {
  request: (cb) => requestAnimationFrame(cb),
  cancel: (id) => cancelAnimationFrame(id),
};

export class SpringGroup<K extends string> {
  readonly springs: Record<K, Spring>;
  private frame: number | null = null;
  private lastTime: number | null = null;

  constructor(
    initial: Record<K, number>,
    private readonly onFrame: (values: Record<K, number>) => void,
    private readonly onSettle: () => void = () => {},
    private readonly scheduler: FrameScheduler = rafScheduler,
  ) {
    const springs = {} as Record<K, Spring>;
    for (const key of Object.keys(initial) as K[]) springs[key] = new Spring(initial[key]);
    this.springs = springs;
  }

  get animating(): boolean {
    return this.frame !== null;
  }

  values(): Record<K, number> {
    const out = {} as Record<K, number>;
    for (const key of Object.keys(this.springs) as K[]) out[key] = this.springs[key].value;
    return out;
  }

  targets(): Record<K, number> {
    const out = {} as Record<K, number>;
    for (const key of Object.keys(this.springs) as K[]) out[key] = this.springs[key].target;
    return out;
  }

  animateTo(targets: Partial<Record<K, number>>, config: SpringConfig, immediate = false): void {
    for (const key of Object.keys(targets) as K[]) {
      const s = this.springs[key];
      s.target = targets[key] as number;
      s.config = config;
      if (immediate) s.snap();
    }
    if (immediate) {
      this.stop();
      this.onFrame(this.values());
      this.onSettle();
      return;
    }
    if (this.frame === null) {
      this.lastTime = null;
      this.frame = this.scheduler.request(this.tick);
    }
  }

  stop(): void {
    if (this.frame !== null) this.scheduler.cancel(this.frame);
    this.frame = null;
    this.lastTime = null;
  }

  private tick = (time: number): void => {
    const dt = this.lastTime === null ? 1 / 60 : (time - this.lastTime) / 1000;
    this.lastTime = time;
    let allSettled = true;
    for (const key of Object.keys(this.springs) as K[]) {
      const s = this.springs[key];
      s.step(dt);
      if (s.settled) s.snap();
      else allSettled = false;
    }
    this.onFrame(this.values());
    if (allSettled) {
      this.frame = null;
      this.lastTime = null;
      this.onSettle();
    } else {
      this.frame = this.scheduler.request(this.tick);
    }
  };
}
