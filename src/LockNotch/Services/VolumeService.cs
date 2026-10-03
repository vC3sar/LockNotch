using System;
using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;

namespace LockNotch.Services;

public sealed class VolumeService : IVolumeService, IDisposable
{
    public event EventHandler<VolumeChangedEventArgs>? VolumeChanged;

    private MMDeviceEnumerator? _enumerator;
    private MMDevice? _device;

    public void Start()
    {
        try
        {
            _enumerator = new MMDeviceEnumerator();
            _device = _enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            _device.AudioEndpointVolume.OnVolumeNotification += OnVolumeNotification;
        }
        catch
        {
            // Falla silenciosa si no hay dispositivo de audio activo
        }
    }

    private void OnVolumeNotification(AudioVolumeNotificationData data)
    {
        VolumeChanged?.Invoke(this, new VolumeChangedEventArgs(data.MasterVolume * 100, data.Muted));
    }

    public void Dispose()
    {
        if (_device != null)
        {
            _device.AudioEndpointVolume.OnVolumeNotification -= OnVolumeNotification;
            _device.Dispose();
        }
        _enumerator?.Dispose();
    }
}
