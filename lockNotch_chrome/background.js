const HOST_NAME = "com.locknotch.downloads";
let port = null;

// Throttling state
let progressThrottles = {};
let lastBytes = {};
let lastSpeed = {};
const THROTTLE_MS = 200;

function connect() {
  if (port) return;
  port = chrome.runtime.connectNative(HOST_NAME);
  port.onMessage.addListener(onNativeMessage);
  port.onDisconnect.addListener(() => {
    console.log("Disconnected from LockNotch native host.");
    port = null;
    // Attempt to reconnect after a delay if needed, 
    // but usually we connect lazily when a download happens.
  });
}

// -- GEOLOCATION LOGIC --
let creatingOffscreen;

async function setupOffscreenDocument(path) {
  if (await chrome.offscreen.hasDocument()) return;

  if (creatingOffscreen) {
    await creatingOffscreen;
  } else {
    creatingOffscreen = chrome.offscreen.createDocument({
      url: path,
      reasons: [chrome.offscreen.Reason.GEOLOCATION || 'DOM_SCRAPING'],
      justification: 'Need to get precise location for LockNotch weather feature'
    });
    await creatingOffscreen;
    creatingOffscreen = null;
  }
}

async function updateLocation() {
  try {
    await setupOffscreenDocument('offscreen.html');
    chrome.runtime.sendMessage({ type: 'get_geolocation' });
  } catch (e) {
    console.error("Failed to setup offscreen document for location", e);
  }
}

chrome.runtime.onMessage.addListener((message) => {
  if (message.type === 'geolocation_result') {
    console.log("Got location:", message.latitude, message.longitude);
    sendToLockNotch({
      type: "update_location",
      latitude: message.latitude,
      longitude: message.longitude
    });
    // Cerrar el documento offscreen una vez obtenida la ubicación para ahorrar recursos
    chrome.offscreen.closeDocument().catch(console.error);
  } else if (message.type === 'geolocation_error') {
    console.error("Geolocation error from offscreen:", message.error);
    chrome.offscreen.closeDocument().catch(console.error);
  }
});

// Obtener la ubicación cuando la extensión se inicia (o se recarga el service worker)
updateLocation();
// También intentar actualizar cada 30 minutos (1800000 ms)
setInterval(updateLocation, 30 * 60 * 1000);
// ------------------------

function onNativeMessage(message) {
  console.log("Received from LockNotch:", message);
}

function sendToLockNotch(message) {
  if (!port) {
    connect();
  }
  if (port) {
    try {
      port.postMessage(message);
    } catch (e) {
      console.error("Failed to send message", e);
      port = null;
    }
  }
}

chrome.downloads.onCreated.addListener((downloadItem) => {
  const msg = {
    type: "download_started",
    downloadId: downloadItem.id,
    filename: getFilename(downloadItem.filename),
    state: downloadItem.state,
    totalBytes: downloadItem.totalBytes,
    bytesReceived: downloadItem.bytesReceived,
    url: downloadItem.url,
    timestamp: Date.now()
  };
  sendToLockNotch(msg);
  startPolling();
});

let pollTimer = null;
const POLL_MS = 400;

function startPolling() {
  if (pollTimer) return;
  pollTimer = setInterval(pollDownloads, POLL_MS);
}

function stopPolling() {
  if (pollTimer) clearInterval(pollTimer);
  pollTimer = null;
}

function pollDownloads() {
  chrome.downloads.search({ state: "in_progress" }, (items) => {
    if (!items || items.length === 0) {
      stopPolling();
      return;
    }
    const now = Date.now();
    for (const item of items) {
      if (item.paused) continue;
      const last = progressThrottles[item.id] || 0;
      let speed = 0;
      if (last > 0 && now > last) {
        const diffBytes = item.bytesReceived - (lastBytes[item.id] || 0);
        speed = Math.max(0, Math.floor((diffBytes / (now - last)) * 1000));
        // Suavizado de la velocidad
        const prev = lastSpeed[item.id];
        if (prev !== undefined) speed = Math.floor(prev * 0.5 + speed * 0.5);
      }
      progressThrottles[item.id] = now;
      lastBytes[item.id] = item.bytesReceived;
      lastSpeed[item.id] = speed;

      let progress = 0;
      if (item.totalBytes > 0) {
        progress = Math.min(100, Math.floor((item.bytesReceived / item.totalBytes) * 100));
      }

      sendToLockNotch({
        type: "download_progress",
        downloadId: item.id,
        filename: getFilename(item.filename),
        state: item.state,
        totalBytes: item.totalBytes,
        bytesReceived: item.bytesReceived,
        progress: progress,
        speed: speed,
        url: item.url,
        timestamp: now
      });
    }
  });
}

// Reanudar polling si el service worker se reinicia con descargas activas
chrome.downloads.search({ state: "in_progress" }, (items) => {
  if (items && items.length > 0) startPolling();
});

chrome.downloads.onChanged.addListener((delta) => {
  chrome.downloads.search({ id: delta.id }, (results) => {
    if (!results || results.length === 0) return;
    const item = results[0];

    // Check state changes
    if (delta.state) {
      let type = "download_progress";
      if (delta.state.current === "complete") type = "download_completed";
      else if (delta.state.current === "interrupted") type = "download_interrupted";

      sendToLockNotch({
        type: type,
        downloadId: item.id,
        filename: getFilename(item.filename),
        state: item.state,
        totalBytes: item.totalBytes,
        bytesReceived: item.bytesReceived,
        url: item.url,
        timestamp: Date.now()
      });
      return;
    }

    // El progreso se envía desde pollDownloads()
    if (item.state === "in_progress") startPolling();
  });
});

chrome.downloads.onErased.addListener((downloadId) => {
  sendToLockNotch({
    type: "download_cancelled",
    downloadId: downloadId,
    timestamp: Date.now()
  });
  delete progressThrottles[downloadId];
});

function getFilename(fullPath) {
  if (!fullPath) return "Desconocido";
  const parts = fullPath.split(/[\/\\]/);
  return parts[parts.length - 1] || fullPath;
}
