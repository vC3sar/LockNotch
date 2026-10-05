chrome.runtime.onMessage.addListener((message, sender, sendResponse) => {
  if (message.type === 'get_geolocation') {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(
        (position) => {
          chrome.runtime.sendMessage({
            type: 'geolocation_result',
            latitude: position.coords.latitude,
            longitude: position.coords.longitude
          });
        },
        (error) => {
          console.error("Geolocation error:", error);
          chrome.runtime.sendMessage({
            type: 'geolocation_error',
            error: error.message
          });
        },
        { enableHighAccuracy: true, timeout: 10000, maximumAge: 0 }
      );
    } else {
      chrome.runtime.sendMessage({
        type: 'geolocation_error',
        error: "Geolocation is not supported by this browser."
      });
    }
  }
});
