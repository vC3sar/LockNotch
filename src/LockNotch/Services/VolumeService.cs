using System;
using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;

namespace LockNotch.Services;

public sealed class VolumeService : IVolumeService, IDisposable
{
    public event EventHandler<VolumeChangedEventArgs>? VolumeChanged;

    private MMDeviceEnumerator? _enumerator;
    private MMDevice? _device;
    private AudioEndpointVolume? _endpointVolume;

    public void Start()
    {
        try
        {
            _enumerator = new MMDeviceEnumerator();
            _device = _enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            _endpointVolume = _device.AudioEndpointVolume;
            _endpointVolume.OnVolumeNotification += OnVolumeNotification;
        }
        catch
        {
            // Falla silenciosa si no hay dispositivo de audio activo
        }
    }

    private DateTime _lastVolumeEvent = DateTime.MinValue;

    private void OnVolumeNotification(AudioVolumeNotificationData data)
    {
        var now = DateTime.Now;
        // Removed throttle to allow instant updates from media keys
        _lastVolumeEvent = now;

        string devName = _device?.FriendlyName ?? "Audio Device";
        // Clean up common suffixes like "(Realtek(R) Audio)"
        int parenIdx = devName.IndexOf(" (");
        if (parenIdx > 0) devName = devName.Substring(0, parenIdx);

        VolumeChanged?.Invoke(this, new VolumeChangedEventArgs(data.MasterVolume * 100, data.Muted, devName));
    }

    public void Dispose()
    {
        if (_endpointVolume != null)
        {
            _endpointVolume.OnVolumeNotification -= OnVolumeNotification;
            _endpointVolume.Dispose();
        }
        _device?.Dispose();
        _enumerator?.Dispose();
    }
}
