import { describe, it, expect } from 'vitest';
import { IslandController, Pager, WheelPager } from '../src/lib/islandController';

describe('IslandController State Machine', () => {
  it('defaults to compact mode and responds to hover', () => {
    let currentMode = '';
    const ctrl = new IslandController((m) => { currentMode = m; });
    expect(ctrl.mode).toBe('compact');

    ctrl.pointerEntered();
    expect(ctrl.mode).toBe('expanded');
    expect(currentMode).toBe('expanded');

    ctrl.pointerExited();
    // In un-mocked timer, scheduleCollapse takes 400ms. If we collapse explicitly:
    ctrl.collapse();
    expect(ctrl.mode).toBe('compact');
  });

  it('respects priority: hidden > notification > expanded > compact', () => {
    const ctrl = new IslandController(() => {});

    ctrl.pointerEntered(); // expanded
    expect(ctrl.mode).toBe('expanded');

    ctrl.setNotifying(true); // notification beats expanded
    expect(ctrl.mode).toBe('notification');

    ctrl.setHidden(true); // hidden beats notification
    expect(ctrl.mode).toBe('hidden');

    ctrl.setHidden(false);
    expect(ctrl.mode).toBe('notification');

    ctrl.setNotifying(false);
    expect(ctrl.mode).toBe('compact');
  });

  it('pager manages carousel pages and toggles downloads page', () => {
    let curPage = '';
    const pager = new Pager((p) => { curPage = p; });

    expect(pager.pages).toEqual(['media', 'apps', 'control', 'hardware']);
    expect(pager.current).toBe('media');

    pager.next();
    expect(pager.current).toBe('apps');
    expect(curPage).toBe('apps');

    pager.previous();
    expect(pager.current).toBe('media');

    pager.setHasDownloads(true);
    expect(pager.pages).toEqual(['media', 'apps', 'control', 'hardware', 'downloads']);

    pager.goTo('downloads');
    expect(pager.current).toBe('downloads');

    pager.setHasDownloads(false);
    expect(pager.pages).toEqual(['media', 'apps', 'control', 'hardware']);
    expect(pager.current).toBe('hardware'); // clamped
  });

  it('wheel pager filters jitter and enforces cooldown', () => {
    const wheel = new WheelPager(120, 350);
    const now = 1000;

    expect(wheel.feed(60, now)).toBe(0); // below threshold
    expect(wheel.feed(70, now)).toBe(1); // triggered next page

    // Immediate second call should be ignored due to cooldown
    expect(wheel.feed(150, now + 100)).toBe(0);

    // After cooldown
    expect(wheel.feed(-150, now + 400)).toBe(-1); // triggered prev page
  });
});
