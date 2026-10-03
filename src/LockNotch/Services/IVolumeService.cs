using System;

namespace LockNotch.Services;

public interface IVolumeService
{
    event EventHandler<VolumeChangedEventArgs>? VolumeChanged;
    void Start();
}

public sealed record VolumeChangedEventArgs(float VolumePercent, bool IsMuted);
