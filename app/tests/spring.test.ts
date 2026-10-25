import { describe, it, expect } from 'vitest';
import { NotificationQueue, type NotificationRequest } from '../src/lib/notificationQueue';
import { Spring, SpringPresets } from '../src/lib/spring';

describe('NotificationQueue', () => {
  it('enqueues notifications and updates existing id in place', () => {
    let current: NotificationRequest | null = null;
    const q = new NotificationQueue((req) => { current = req; });

    q.enqueue({
      id: 'bat',
      glyph: '🔋',
      text: 'Batería 80%',
      tone: 'accent',
      durationMs: 3000
    });

    expect(current).not.toBeNull();
    expect(current?.text).toBe('Batería 80%');

    // Live update same id
    q.enqueue({
      id: 'bat',
      glyph: '⚡',
      text: 'Batería 81%',
      tone: 'accent',
      durationMs: 3000
    });

    expect(current?.text).toBe('Batería 81%');

    // Dismiss
    q.dismiss();
    expect(current).toBeNull();
  });
});

describe('Spring Physics Engine', () => {
  it('steps towards target and settles firmly', () => {
    const s = new Spring(100, SpringPresets.bouncy);
    s.target = 200;

    expect(s.value).toBe(100);
    expect(s.settled).toBe(false);

    // Simulate stepping for 1 second
    for (let i = 0; i < 60; i++) {
      s.step(1 / 60);
    }

    expect(s.value).toBeGreaterThan(180);
    expect(s.value).toBeLessThan(220);

    // After enough steps it must settle exactly at target
    for (let i = 0; i < 180; i++) {
      s.step(1 / 60);
      if (s.settled) s.snap();
    }

    expect(s.settled).toBe(true);
    expect(s.value).toBe(200);
  });
});
