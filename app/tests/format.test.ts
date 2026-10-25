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

describe('Formatters and Helpers', () => {
  it('formats duration into mm:ss and hh:mm:ss', () => {
    expect(formatDuration(0)).toBe('0:00');
    expect(formatDuration(45000)).toBe('0:45');
    expect(formatDuration(125000)).toBe('2:05');
    expect(formatDuration(3665000)).toBe('1:01:05');
  });

  it('calculates battery glyphs and texts correctly', () => {
    expect(batteryText(-1)).toBe('--%');
    expect(batteryText(85)).toBe('85%');
    expect(batteryGlyph(100, true)).toBe('\uE83E'); // charging
    expect(batteryGlyph(98, false)).toBe('\uE83F'); // 95+
    expect(batteryGlyph(82, false)).toBe('\uE850'); // 80+
    expect(batteryGlyph(15, false)).toBe('\uE852'); // low
  });

  it('formats network speed nicely', () => {
    expect(speedText(512000)).toBe('500 KB/s');
    expect(speedText(2621440)).toBe('2.5 MB/s');
  });

  it('summarizes single and multi-download lists', () => {
    expect(summarizeDownloads([])).toEqual({
      compactText: 'Descargando...',
      title: '0 descargas',
      progress: 0,
      progressText: ''
    });

    const single: DownloadItem[] = [{
      type: 'download_progress',
      downloadId: 1,
      filename: 'setup.exe',
      state: 'in_progress',
      totalBytes: 104857600,
      bytesReceived: 52428800,
      progress: 50,
      speed: 5242880,
      url: 'https://example.com/setup.exe',
      timestamp: Date.now()
    }];

    const s = summarizeDownloads(single);
    expect(s.title).toBe('setup.exe');
    expect(s.progress).toBe(50);
    expect(s.compactText).toContain('50%');

    const multi: DownloadItem[] = [
      { ...single[0], progress: 20 },
      { ...single[0], downloadId: 2, filename: 'video.mp4', progress: 60 }
    ];
    const m = summarizeDownloads(multi);
    expect(m.title).toBe('2 descargas');
    expect(m.progress).toBe(40);
  });

  it('evaluates telemetry gauge fractions and levels', () => {
    expect(gaugeFraction(null, 'none')).toBe(0);
    expect(gaugeFraction(50, 'load')).toBe(0.5);
    expect(gaugeFraction(65, 'temp')).toBe(0.5); // (65-30)/70 = 0.5
    expect(gaugeLevel(40, 'temp')).toBe('ok');
    expect(gaugeLevel(75, 'temp')).toBe('warm');
    expect(gaugeLevel(90, 'temp')).toBe('hot');
  });

  it('handles notification glyphs and truncates cleanly', () => {
    expect(notificationGlyph('Discord')).toBe('\uE8BD');
    expect(notificationGlyph('WhatsApp')).toBe('\uE8EA');
    expect(notificationGlyph('Slack')).toBe('\uE7E7');
    expect(truncate('Hello world', 20)).toBe('Hello world');
    expect(truncate('A'.repeat(50), 40).length).toBe(40);
    expect(notificationText('Title', 'Body')).toBe('Title: Body');
  });
});
