using System;

namespace LockNotch.Services;

public interface IHardwareService
{
    event EventHandler<HardwareChangedEventArgs>? HardwareChanged;
    void Start();
}

public sealed record HardwareChangedEventArgs(string CpuTemp, string GpuTemp);
