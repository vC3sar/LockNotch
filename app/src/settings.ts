import { api } from './lib/ipc';
import type { Settings } from './lib/types';
import { getCurrentWindow } from '@tauri-apps/api/window';
import { open } from '@tauri-apps/plugin-dialog';

// Pestañas
const navItems = document.querySelectorAll('.nav-item');
const tabContents = document.querySelectorAll('.tab-content');

navItems.forEach(item => {
  item.addEventListener('click', () => {
    navItems.forEach(n => n.classList.remove('active'));
    tabContents.forEach(t => t.classList.remove('active'));

    item.classList.add('active');
    const tabId = item.getAttribute('data-tab');
    if (tabId) {
      document.getElementById(tabId)?.classList.add('active');
    }
  });
});

// Campos
const chkStartWindows = document.getElementById('chk-start-windows') as HTMLInputElement;
const chkHideFullscreen = document.getElementById('chk-hide-fullscreen') as HTMLInputElement;
const chkAmbientGlow = document.getElementById('chk-ambient-glow') as HTMLInputElement;

const txtApp1 = document.getElementById('txt-app1') as HTMLInputElement;
const txtApp2 = document.getElementById('txt-app2') as HTMLInputElement;
const txtApp3 = document.getElementById('txt-app3') as HTMLInputElement;
const txtApp4 = document.getElementById('txt-app4') as HTMLInputElement;
const txtApp5 = document.getElementById('txt-app5') as HTMLInputElement;

const txtLat = document.getElementById('txt-lat') as HTMLInputElement;
const txtLon = document.getElementById('txt-lon') as HTMLInputElement;
const chkFahrenheit = document.getElementById('chk-fahrenheit') as HTMLInputElement;
const chkPeriodicLocation = document.getElementById('chk-periodic-location') as HTMLInputElement;
const txtRefreshHw = document.getElementById('txt-refresh-hw') as HTMLInputElement;

const selTheme = document.getElementById('sel-theme') as HTMLSelectElement;
const selFont = document.getElementById('sel-font') as HTMLSelectElement;

const btnDetectIp = document.getElementById('btn-detect-ip') as HTMLButtonElement;
const btnDetectChrome = document.getElementById('btn-detect-chrome') as HTMLButtonElement;
const btnRegisterHost = document.getElementById('btn-register-host') as HTMLButtonElement;
const btnCancel = document.getElementById('btn-cancel') as HTMLButtonElement;
const btnSave = document.getElementById('btn-save') as HTMLButtonElement;
const btnClose = document.getElementById('btn-window-close') as HTMLButtonElement;

// Examinar archivos con el selector de Windows nativo
document.querySelectorAll('.browse-btn').forEach(btn => {
  btn.addEventListener('click', async () => {
    const targetId = btn.getAttribute('data-target');
    if (!targetId) return;
    const inputEl = document.getElementById(targetId) as HTMLInputElement;
    const selected = await open({
      multiple: false,
      directory: false,
      filters: [{ name: 'Ejecutables', extensions: ['exe', 'bat', 'cmd', 'lnk'] }]
    });
    if (selected && typeof selected === 'string') {
      inputEl.value = selected;
    }
  });
});

// Cargar ajustes actuales
async function loadSettings() {
  try {
    const s = await api.getSettings();
    chkStartWindows.checked = s.StartWithWindows;
    chkHideFullscreen.checked = s.HideInFullscreen;
    chkAmbientGlow.checked = s.AmbientGlow;

    txtApp1.value = s.AppLauncher1;
    txtApp2.value = s.AppLauncher2;
    txtApp3.value = s.AppLauncher3;
    txtApp4.value = s.AppLauncher4;
    txtApp5.value = s.AppLauncher5;

    txtLat.value = s.WeatherLatitude.toString();
    txtLon.value = s.WeatherLongitude.toString();
    chkFahrenheit.checked = s.WeatherUseFahrenheit;
    chkPeriodicLocation.checked = s.WeatherShowPeriodicLocation;
    txtRefreshHw.value = s.HardwareRefreshIntervalSeconds.toString();

    selTheme.value = s.Theme || 'Dark';
    selFont.value = s.FontFamily || 'Normal';

    applyTheme(s.Theme);

    // Cargar estado de integraciones
    const status = await api.integrationStatus();
    const badgeHost = document.getElementById('badge-native-host');
    if (badgeHost) {
      badgeHost.textContent = status.nativeHostRegistered ? 'Registrado' : 'No registrado';
      badgeHost.className = `status-badge ${status.nativeHostRegistered ? 'ok' : 'warning'}`;
    }

    const badgeNotif = document.getElementById('badge-notif-access');
    if (badgeNotif) {
      badgeNotif.textContent = status.notificationAccess;
    }

    const infoSource = document.getElementById('info-telemetry-source');
    if (infoSource) {
      infoSource.textContent = `CPU: ${status.cpuSource} | GPU: ${status.gpuSource}`;
    }
  } catch (err) {
    console.error('Error loading settings', err);
  }
}

function applyTheme(theme: string) {
  if (theme === 'Light') {
    document.documentElement.setAttribute('data-theme', 'light');
  } else {
    document.documentElement.removeAttribute('data-theme');
  }
}

selTheme.addEventListener('change', () => {
  applyTheme(selTheme.value);
});

// Detectar IP
btnDetectIp.addEventListener('click', async () => {
  btnDetectIp.textContent = 'Detectando...';
  btnDetectIp.disabled = true;
  try {
    const [lat, lon] = await api.detectLocationIp();
    txtLat.value = lat.toFixed(4);
    txtLon.value = lon.toFixed(4);
    btnDetectIp.textContent = '¡Listo!';
  } catch {
    btnDetectIp.textContent = 'Error';
  } finally {
    setTimeout(() => {
      btnDetectIp.textContent = 'Detectar por IP';
      btnDetectIp.disabled = false;
    }, 1500);
  }
});

// Detectar Chrome
btnDetectChrome.addEventListener('click', async () => {
  btnDetectChrome.textContent = 'Solicitando...';
  btnDetectChrome.disabled = true;
  try {
    await api.requestExtensionLocation();
    btnDetectChrome.textContent = 'Enviado a Chrome';
  } catch {
    btnDetectChrome.textContent = 'Error';
  } finally {
    setTimeout(() => {
      btnDetectChrome.textContent = 'GPS de Chrome';
      btnDetectChrome.disabled = false;
    }, 1500);
  }
});

// Re-registrar Host Nativo
btnRegisterHost.addEventListener('click', async () => {
  btnRegisterHost.disabled = true;
  try {
    const ok = await api.registerNativeHost();
    const badgeHost = document.getElementById('badge-native-host');
    if (badgeHost) {
      badgeHost.textContent = ok ? 'Registrado' : 'Error';
      badgeHost.className = `status-badge ${ok ? 'ok' : 'warning'}`;
    }
  } finally {
    btnRegisterHost.disabled = false;
  }
});

// Guardar
btnSave.addEventListener('click', async () => {
  const newSettings: Settings = {
    StartWithWindows: chkStartWindows.checked,
    HideInFullscreen: chkHideFullscreen.checked,
    AmbientGlow: chkAmbientGlow.checked,
    AppLauncher1: txtApp1.value,
    AppLauncher2: txtApp2.value,
    AppLauncher3: txtApp3.value,
    AppLauncher4: txtApp4.value,
    AppLauncher5: txtApp5.value,
    WeatherLatitude: parseFloat(txtLat.value) || 40.4165,
    WeatherLongitude: parseFloat(txtLon.value) || -3.7026,
    WeatherUseFahrenheit: chkFahrenheit.checked,
    WeatherShowPeriodicLocation: chkPeriodicLocation.checked,
    HardwareRefreshIntervalSeconds: parseInt(txtRefreshHw.value) || 2,
    Theme: selTheme.value,
    FontFamily: selFont.value,
    ReduceMotion: false,
  };

  await api.saveSettings(newSettings);
  const win = getCurrentWindow();
  await win.close();
});

// Cancelar / Cerrar
btnCancel.addEventListener('click', async () => {
  const win = getCurrentWindow();
  await win.close();
});

btnClose.addEventListener('click', async () => {
  const win = getCurrentWindow();
  await win.close();
});

window.addEventListener('DOMContentLoaded', loadSettings);
