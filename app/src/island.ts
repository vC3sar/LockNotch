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

// Elementos del DOM
const island = document.getElementById('island') as HTMLDivElement;
const vinylDiscHost = document.getElementById('vinyl-disc-host') as HTMLDivElement;
const vinylDisc = document.getElementById('vinyl-disc') as HTMLDivElement;
const vinylArtImg = document.getElementById('vinyl-art-img') as HTMLImageElement;

const viewCompact = document.getElementById('view-compact') as HTMLDivElement;
const compactDefault = document.getElementById('compact-default') as HTMLDivElement;
const compactMedia = document.getElementById('compact-media') as HTMLDivElement;
const compactDownloads = document.getElementById('compact-downloads') as HTMLDivElement;

const compactTime = document.getElementById('compact-time') as HTMLSpanElement;
const compactWeatherGlyph = document.getElementById('compact-weather-glyph') as HTMLSpanElement;
const compactWeatherTemp = document.getElementById('compact-weather-temp') as HTMLSpanElement;
const compactBatteryText = document.getElementById('compact-battery-text') as HTMLSpanElement;
const compactBatteryIcon = document.getElementById('compact-battery-icon') as HTMLSpanElement;

const compactMediaThumb = document.getElementById('compact-media-thumb') as HTMLImageElement;
const compactMediaTitle = document.getElementById('compact-media-title') as HTMLSpanElement;
const compactVisualizer = document.getElementById('compact-visualizer') as HTMLDivElement;

const compactDownloadsText = document.getElementById('compact-downloads-text') as HTMLSpanElement;
const compactDownloadsSpeed = document.getElementById('compact-downloads-speed') as HTMLSpanElement;

const viewNotification = document.getElementById('view-notification') as HTMLDivElement;
const notifIcon = document.getElementById('notif-icon') as HTMLSpanElement;
const notifText = document.getElementById('notif-text') as HTMLSpanElement;

const viewExpanded = document.getElementById('view-expanded') as HTMLDivElement;
const pageMedia = document.getElementById('page-media') as HTMLDivElement;
const pageApps = document.getElementById('page-apps') as HTMLDivElement;
const pageControl = document.getElementById('page-control') as HTMLDivElement;
const pageHardware = document.getElementById('page-hardware') as HTMLDivElement;
const pageDownloads = document.getElementById('page-downloads') as HTMLDivElement;

const expArtist = document.getElementById('exp-artist') as HTMLSpanElement;
const expTitle = document.getElementById('exp-title') as HTMLSpanElement;
const mediaProgressFill = document.getElementById('media-progress-fill') as HTMLDivElement;
const mediaTimePos = document.getElementById('media-time-pos') as HTMLSpanElement;
const mediaTimeEnd = document.getElementById('media-time-end') as HTMLSpanElement;
const btnPrev = document.getElementById('btn-prev') as HTMLButtonElement;
const btnPlayPause = document.getElementById('btn-play-pause') as HTMLButtonElement;
const btnNext = document.getElementById('btn-next') as HTMLButtonElement;

const toggleWifi = document.getElementById('toggle-wifi') as HTMLDivElement;
const toggleBt = document.getElementById('toggle-bt') as HTMLDivElement;
const toggleDark = document.getElementById('toggle-dark') as HTMLDivElement;
const toggleDnd = document.getElementById('toggle-dnd') as HTMLDivElement;

const hwCpuVal = document.getElementById('hw-cpu-val') as HTMLDivElement;
const hwCpuBar = document.getElementById('hw-cpu-bar') as HTMLDivElement;
const hwCpuSource = document.getElementById('hw-cpu-source') as HTMLSpanElement;
const hwGpuVal = document.getElementById('hw-gpu-val') as HTMLDivElement;
const hwGpuBar = document.getElementById('hw-gpu-bar') as HTMLDivElement;
const hwGpuSource = document.getElementById('hw-gpu-source') as HTMLSpanElement;

const expDownloadsList = document.getElementById('exp-downloads-list') as HTMLDivElement;
const paginatorThumb = document.getElementById('paginator-thumb') as HTMLDivElement;

// Estado local
let currentMedia: MediaState | null = null;
let currentDownloads: DownloadItem[] = [];
let currentToggles: Toggles = { wifi: false, bluetooth: false, darkMode: false, dnd: false };
let appEntries: AppEntry[] = [];

// Muelles de física para dimensiones de la isla y posición del disco
const springGroup = new SpringGroup(
  {
    width: 240,
    height: 38,
    radius: 19,
    discY: -115,
  },
  (vals) => {
    island.style.width = `${vals.width}px`;
    island.style.height = `${vals.height}px`;
    island.style.borderRadius = `0 0 ${vals.radius}px ${vals.radius}px`;
    vinylDiscHost.style.transform = `translateX(-50%) translateY(${vals.discY}px)`;
    
    // Notificar área física de clic al backend de Windows
    reportHitRect();
  }
);

function reportHitRect() {
  const rect = island.getBoundingClientRect();
  api.setHitRect({
    x: Math.round(rect.left),
    y: Math.round(rect.top),
    width: Math.round(rect.width),
    height: Math.round(rect.height)
  }).catch(() => {});
}

// Controlador de Notificaciones
const notifQueue = new NotificationQueue((req) => {
  if (req) {
    notifIcon.textContent = req.glyph;
    notifText.textContent = req.text;
    controller.setNotifying(true);
  } else {
    controller.setNotifying(false);
  }
});

// Paginador y rueda
const pager = new Pager((page, index, count) => {
  updateCarousel(page, index, count);
  if (controller.expanded) {
    applyExpandedGeometry(page);
  }
});

const wheelPager = new WheelPager(120, 350);

// Máquina de estados principal de la isla
const controller = new IslandController((mode: IslandMode) => {
  applyMode(mode);
});

function applyMode(mode: IslandMode) {
  if (mode === 'hidden') {
    island.style.opacity = '0';
    vinylDiscHost.classList.remove('visible');
    return;
  }
  island.style.opacity = '1';

  if (mode === 'notification') {
    viewCompact.classList.add('hidden');
    viewExpanded.classList.add('hidden');
    viewNotification.classList.remove('hidden');
    vinylDiscHost.classList.remove('visible');

    springGroup.animateTo(
      { width: 280, height: 38, radius: 19, discY: -115 },
      SpringPresets.gentle
    );
  } else if (mode === 'expanded') {
    viewCompact.classList.add('hidden');
    viewNotification.classList.add('hidden');
    viewExpanded.classList.remove('hidden');

    applyExpandedGeometry(pager.current);
    api.setViewHint(true, pager.current).catch(() => {});
  } else {
    // Compact
    viewExpanded.classList.add('hidden');
    viewNotification.classList.add('hidden');
    viewCompact.classList.remove('hidden');
    vinylDiscHost.classList.remove('visible');

    updateCompactView();
    springGroup.animateTo(
      { width: 240, height: 38, radius: 19, discY: -115 },
      SpringPresets.snappy
    );
    api.setViewHint(false, pager.current).catch(() => {});
  }
}

function applyExpandedGeometry(page: PageId) {
  const isMedia = page === 'media';
  const targetWidth = isMedia ? 320 : 420;
  const targetHeight = isMedia ? 255 : 180;
  const targetRadius = isMedia ? 48 : 36;
  const targetDiscY = isMedia ? 0 : -115;

  springGroup.animateTo(
    { width: targetWidth, height: targetHeight, radius: targetRadius, discY: targetDiscY },
    SpringPresets.bouncy
  );

  if (isMedia && currentMedia && currentMedia.thumbnail) {
    vinylDiscHost.classList.add('visible');
  } else {
    vinylDiscHost.classList.remove('visible');
  }
}

function updateCarousel(page: PageId, index: number, count: number) {
  const pages: { id: PageId; el: HTMLDivElement }[] = [
    { id: 'media', el: pageMedia },
    { id: 'apps', el: pageApps },
    { id: 'control', el: pageControl },
    { id: 'hardware', el: pageHardware },
    { id: 'downloads', el: pageDownloads }
  ];

  const activeIdx = pages.findIndex(p => p.id === page);

  pages.forEach((p, idx) => {
    p.el.classList.remove('page-left', 'page-right', 'page-active');
    if (idx === activeIdx) {
      p.el.classList.add('page-active');
    } else if (idx < activeIdx) {
      p.el.classList.add('page-left');
    } else {
      p.el.classList.add('page-right');
    }
  });

  // Paginador
  const thumbWidth = 72 / count;
  paginatorThumb.style.width = `${thumbWidth}px`;
  paginatorThumb.style.transform = `translateX(${index * thumbWidth}px)`;
}

function updateCompactView() {
  const hasActiveDownloads = currentDownloads.length > 0;
  const hasMediaPlaying = currentMedia !== null && currentMedia.isPlaying;

  compactDefault.classList.add('hidden');
  compactMedia.classList.add('hidden');
  compactDownloads.classList.add('hidden');

  if (hasActiveDownloads && !hasMediaPlaying) {
    compactDownloads.classList.remove('hidden');
    if (currentDownloads.length === 1) {
      const d = currentDownloads[0];
      compactDownloadsText.textContent = d.state === 'complete'
        ? `✓ ${d.filename}`
        : (d.totalBytes > 0 ? `↓ ${d.progress}%` : `↓ ${(d.bytesReceived / 1048576).toFixed(1)} MB`);
      compactDownloadsSpeed.textContent = d.state === 'complete' ? 'Listo' : speedText(d.speed);
    } else {
      compactDownloadsText.textContent = `↓ ${currentDownloads.length} descargas`;
      const totalSpeed = currentDownloads.reduce((acc, x) => acc + (x.speed || 0), 0);
      compactDownloadsSpeed.textContent = speedText(totalSpeed);
    }
  } else if (hasMediaPlaying) {
    compactMedia.classList.remove('hidden');
    compactMediaTitle.textContent = currentMedia?.title || '--';
    if (currentMedia?.thumbnail) {
      compactMediaThumb.src = currentMedia.thumbnail;
      compactMediaThumb.style.display = 'block';
    } else {
      compactMediaThumb.style.display = 'none';
    }
    compactVisualizer.classList.add('animating');
  } else {
    compactDefault.classList.remove('hidden');
  }
}

// Reloj continuo
function updateClock() {
  const parts = clockParts(new Date());
  compactTime.textContent = parts.time;
}
setInterval(updateClock, 1000);
updateClock();

// Rueda del ratón para pasar páginas
island.addEventListener('wheel', (e) => {
  if (!controller.expanded) return;
  e.preventDefault();
  const dir = wheelPager.feed(e.deltaY, Date.now());
  if (dir > 0) pager.next();
  else if (dir < 0) pager.previous();
});

// Eventos del puntero en la isla
island.addEventListener('mouseenter', () => {
  controller.pointerEntered();
});

island.addEventListener('mouseleave', () => {
  controller.pointerExited();
});

island.addEventListener('click', (e) => {
  // Evitar colapsar si se hizo clic en un botón interactivo dentro de la vista expandida
  const target = e.target as HTMLElement;
  if (target.closest('.media-btn') || target.closest('.control-toggle') || target.closest('.app-slot')) {
    return;
  }
  controller.click();
});

// Clic derecho -> Abrir configuración o menú
island.addEventListener('contextmenu', (e) => {
  e.preventDefault();
  api.openSettings().catch(() => {});
});

// Controles multimedia
btnPlayPause.addEventListener('click', () => {
  if (!currentMedia) return;
  api.media(currentMedia.isPlaying ? 'pause' : 'play').catch(() => {});
});

btnPrev.addEventListener('click', () => {
  api.media('previous').catch(() => {});
});

btnNext.addEventListener('click', () => {
  api.media('next').catch(() => {});
});

// Toggles del Centro de Control
toggleWifi.addEventListener('click', async () => {
  const newState = !currentToggles.wifi;
  toggleWifi.classList.toggle('active', newState);
  await api.setWifi(newState);
});

toggleBt.addEventListener('click', async () => {
  const newState = !currentToggles.bluetooth;
  toggleBt.classList.toggle('active', newState);
  await api.setBluetooth(newState);
});

toggleDark.addEventListener('click', async () => {
  const newState = !currentToggles.darkMode;
  toggleDark.classList.toggle('active', newState);
  await api.setDarkMode(newState);
  applyTheme(newState ? 'Dark' : 'Light');
});

toggleDnd.addEventListener('click', async () => {
  const newState = !currentToggles.dnd;
  toggleDnd.classList.toggle('active', newState);
  await api.setDnd(newState);
});

// Lanzador de apps
document.querySelectorAll('.app-slot').forEach((slotEl) => {
  slotEl.addEventListener('click', () => {
    const slot = slotEl.getAttribute('data-slot');
    if (slot) {
      api.launchApp(slot).catch(() => {});
      controller.collapse();
    }
  });
});

function applyTheme(themeName: string) {
  if (themeName === 'Light') {
    document.documentElement.setAttribute('data-theme', 'light');
  } else {
    document.documentElement.removeAttribute('data-theme');
  }
}

function applyFont(fontName: string) {
  let font = 'Segoe UI Variable Display, Segoe UI, sans-serif';
  if (fontName === 'Nothing') font = "'Nothing Font (5x7)', sans-serif";
  else if (fontName === 'SpaceGrotesk') font = "'Space Grotesk', sans-serif";
  else if (fontName === 'Manrope') font = "'Manrope', sans-serif";
  else if (fontName === 'Audiowide') font = "'Audiowide', cursive";
  else if (fontName === 'Oxanium') font = "'Oxanium', sans-serif";
  
  document.documentElement.style.setProperty('--font-base', font);
}

// Barra de progreso y tiempo de música
function updateMediaProgress() {
  if (!currentMedia || currentMedia.endMs <= 0) return;
  const pos = currentPosition(currentMedia);
  const frac = progressFraction(currentMedia);
  
  mediaProgressFill.style.width = `${(frac * 100).toFixed(1)}%`;
  mediaTimePos.textContent = formatDuration(pos);
  mediaTimeEnd.textContent = formatDuration(currentMedia.endMs);
}
setInterval(updateMediaProgress, 500);

// Sincronización de eventos IPC
async function init() {
  const snap = await api.snapshot();
  applySnapshot(snap);

  // Escuchar eventos
  on('ln:media', (ev) => {
    const prevTrack = currentMedia?.trackKey;
    currentMedia = ev.media;

    if (currentMedia) {
      expTitle.textContent = currentMedia.title;
      expArtist.textContent = currentMedia.artist || currentMedia.album || 'Artista desconocido';
      btnPlayPause.textContent = currentMedia.isPlaying ? Glyph.pause : Glyph.play;
      
      if (currentMedia.thumbnail) {
        vinylArtImg.src = currentMedia.thumbnail;
        compactMediaThumb.src = currentMedia.thumbnail;
      }
      
      if (currentMedia.isPlaying) {
        vinylDisc.classList.add('spinning');
        compactVisualizer.classList.add('animating');
      } else {
        vinylDisc.classList.remove('spinning');
        compactVisualizer.classList.remove('animating');
      }

      updateMediaProgress();

      // Al cambiar de canción expandir temporalmente 2s como en el original
      if (ev.trackChanged || (prevTrack && prevTrack !== currentMedia.trackKey)) {
        controller.expandTemporarily(2000);
      }
    } else {
      expTitle.textContent = 'Nada reproduciéndose';
      expArtist.textContent = 'Abre Spotify o YouTube Music';
      btnPlayPause.textContent = Glyph.play;
      vinylDisc.classList.remove('spinning');
      compactVisualizer.classList.remove('animating');
      vinylDiscHost.classList.remove('visible');
    }
    updateCompactView();
  });

  on('ln:battery', (b) => {
    compactBatteryText.textContent = batteryText(b.percent);
    compactBatteryIcon.textContent = batteryGlyph(b.percent, b.charging);
    compactBatteryIcon.classList.toggle('charging', b.charging);
    compactBatteryIcon.classList.toggle('low', b.low);
  });

  on('ln:weather', (w) => {
    compactWeatherGlyph.textContent = w.glyph;
    compactWeatherTemp.textContent = w.temperature;
  });

  on('ln:hardware', (hw) => {
    hwCpuVal.textContent = hw.cpu;
    hwGpuVal.textContent = hw.gpu;
    hwCpuSource.textContent = hw.cpuKind === 'temp' ? 'Temp' : 'Load';
    hwGpuSource.textContent = hw.gpuKind === 'temp' ? 'Temp' : 'Load';

    const cpuFrac = gaugeFraction(hw.cpuValue, hw.cpuKind);
    const gpuFrac = gaugeFraction(hw.gpuValue, hw.gpuKind);

    hwCpuBar.style.width = `${(cpuFrac * 100).toFixed(0)}%`;
    hwGpuBar.style.width = `${(gpuFrac * 100).toFixed(0)}%`;

    const cpuLvl = gaugeLevel(hw.cpuValue, hw.cpuKind);
    const gpuLvl = gaugeLevel(hw.gpuValue, hw.gpuKind);

    hwCpuBar.className = `telemetry-bar-fill ${cpuLvl}`;
    hwGpuBar.className = `telemetry-bar-fill ${gpuLvl}`;
  });

  on('ln:usb', (u) => {
    const text = u.connected
      ? `${u.friendlyName} ${u.details || u.kind}`
      : `${u.friendlyName} Desconectado`;
    notifQueue.enqueue({
      id: `usb_${u.deviceId}`,
      glyph: u.glyph || Glyph.power,
      text,
      tone: u.connected ? 'accent' : 'secondary',
      durationMs: 3500
    });
  });

  on('ln:notification', (n) => {
    notifQueue.enqueue({
      id: `win_${n.appName}`,
      glyph: notificationGlyph(n.appName),
      text: notificationText(n.title, n.body),
      tone: 'accent',
      durationMs: 4000
    });
  });

  on('ln:downloads', (downloads) => {
    currentDownloads = downloads;
    pager.setHasDownloads(downloads.length > 0);
    renderDownloads(downloads);
    updateCompactView();
  });

  on('ln:hidden', (hidden) => {
    controller.setHidden(hidden);
  });

  on('ln:toggles', (toggles) => {
    currentToggles = toggles;
    toggleWifi.classList.toggle('active', toggles.wifi);
    toggleBt.classList.toggle('active', toggles.bluetooth);
    toggleDark.classList.toggle('active', toggles.darkMode);
    toggleDnd.classList.toggle('active', toggles.dnd);
  });

  on('ln:settings', (settings) => {
    applyTheme(settings.Theme);
    applyFont(settings.FontFamily);
    island.classList.toggle('ambient-glow', settings.AmbientGlow);
  });

  // Cargar iconos reales de apps en los slots
  loadAppEntries();
  reportHitRect();
}

async function loadAppEntries() {
  try {
    appEntries = await api.appEntries();
    appEntries.forEach(entry => {
      const slotEl = document.querySelector(`.app-slot[data-slot="${entry.slot}"]`);
      if (slotEl) {
        slotEl.setAttribute('title', entry.name || `App ${entry.slot}`);
        if (entry.icon && entry.icon.rgba) {
          slotEl.innerHTML = `<img src="data:image/png;base64,${entry.icon.rgba}" alt="${entry.name}" />`;
        }
      }
    });
  } catch {}
}

function renderDownloads(list: DownloadItem[]) {
  expDownloadsList.innerHTML = '';
  if (list.length === 0) {
    expDownloadsList.innerHTML = '<div style="text-align: center; color: var(--text-tertiary); font-size: 12px; margin-top: 15px;">Sin descargas activas</div>';
    return;
  }

  list.forEach(item => {
    const card = document.createElement('div');
    card.className = 'download-item-card';

    const speed = item.speed >= 1048576 
      ? `${(item.speed / 1048576).toFixed(1)} MB/s` 
      : `${(item.speed / 1024).toFixed(0)} KB/s`;

    card.innerHTML = `
      <div class="download-item-row">
        <span class="download-item-name">${item.filename}</span>
        <span class="download-item-speed">${item.state === 'complete' ? '✓ Listo' : `${item.progress}% · ${speed}`}</span>
      </div>
      <div class="download-item-bar">
        <div class="download-item-progress" style="width: ${item.progress}%"></div>
      </div>
    `;
    expDownloadsList.appendChild(card);
  });
}

function applySnapshot(snap: Snapshot) {
  if (snap.settings) {
    applyTheme(snap.settings.Theme);
    applyFont(snap.settings.FontFamily);
    island.classList.toggle('ambient-glow', snap.settings.AmbientGlow);
  }
  if (snap.toggles) {
    currentToggles = snap.toggles;
    toggleWifi.classList.toggle('active', snap.toggles.wifi);
    toggleBt.classList.toggle('active', snap.toggles.bluetooth);
    toggleDark.classList.toggle('active', snap.toggles.darkMode);
    toggleDnd.classList.toggle('active', snap.toggles.dnd);
  }
  if (snap.media) {
    currentMedia = snap.media;
    expTitle.textContent = snap.media.title;
    expArtist.textContent = snap.media.artist || snap.media.album;
    btnPlayPause.textContent = snap.media.isPlaying ? Glyph.pause : Glyph.play;
    if (snap.media.thumbnail) {
      vinylArtImg.src = snap.media.thumbnail;
      compactMediaThumb.src = snap.media.thumbnail;
    }
    if (snap.media.isPlaying) {
      vinylDisc.classList.add('spinning');
      compactVisualizer.classList.add('animating');
    }
  }
  if (snap.battery) {
    compactBatteryText.textContent = batteryText(snap.battery.percent);
    compactBatteryIcon.textContent = batteryGlyph(snap.battery.percent, snap.battery.charging);
    compactBatteryIcon.classList.toggle('charging', snap.battery.charging);
    compactBatteryIcon.classList.toggle('low', snap.battery.low);
  }
  if (snap.weather) {
    compactWeatherGlyph.textContent = snap.weather.glyph;
    compactWeatherTemp.textContent = snap.weather.temperature;
  }
  if (snap.downloads) {
    currentDownloads = snap.downloads;
    pager.setHasDownloads(snap.downloads.length > 0);
    renderDownloads(snap.downloads);
  }
  updateCompactView();
}

window.addEventListener('DOMContentLoaded', init);
window.addEventListener('resize', reportHitRect);
