using System;
using LibreHardwareMonitor.Hardware;
using System.Threading;
using System.Linq;

namespace LockNotch.Services;

public sealed class HardwareService : IHardwareService, IDisposable
{
    public event EventHandler<HardwareChangedEventArgs>? HardwareChanged;

    private readonly Computer _computer;
    private System.Threading.Timer? _timer;
    private string _cpuTemp = "--°";
    private string _gpuTemp = "--°";

    public HardwareService()
    {
        _computer = new Computer
        {
            IsCpuEnabled = true,
            IsGpuEnabled = true,
            IsMotherboardEnabled = false,
            IsMemoryEnabled = false,
            IsNetworkEnabled = false,
            IsControllerEnabled = false,
            IsStorageEnabled = false
        };
    }

    public void Start()
    {
        try
        {
            _computer.Open();
            _timer = new System.Threading.Timer(UpdateHardware, null, TimeSpan.Zero, TimeSpan.FromSeconds(2));
        }
        catch
        {
            // Falla si no es administrador (LibreHardwareMonitor requiere admin)
        }
    }

    private void UpdateHardware(object? state)
    {
        try
        {
            foreach (var hardware in _computer.Hardware)
            {
                hardware.Update();

                if (hardware.HardwareType == HardwareType.Cpu)
                {
                    var tempSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature && s.Name.Contains("Package", StringComparison.OrdinalIgnoreCase))
                                  ?? hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature && s.Name.Contains("Core Average", StringComparison.OrdinalIgnoreCase))
                                  ?? hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature && s.Name.Contains("Tdie", StringComparison.OrdinalIgnoreCase))
                                  ?? hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature && s.Name.Contains("Tctl", StringComparison.OrdinalIgnoreCase))
                                  ?? hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature && s.Name.Contains("Core", StringComparison.OrdinalIgnoreCase))
                                  ?? hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature);

                    if (tempSensor?.Value.HasValue == true)
                    {
                        _cpuTemp = $"{Math.Round(tempSensor.Value.Value)}°";
                    }
                    else
                    {
                        // Fallback a Load si no hay temperatura
                        var loadSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Load && s.Name.Contains("Total", StringComparison.OrdinalIgnoreCase))
                                      ?? hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Load);
                        
                        if (loadSensor?.Value.HasValue == true) _cpuTemp = $"{Math.Round(loadSensor.Value.Value)}%";
                    }
                }
                else if (hardware.HardwareType == HardwareType.GpuNvidia || hardware.HardwareType == HardwareType.GpuAmd || hardware.HardwareType == HardwareType.GpuIntel)
                {
                    var tempSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature && s.Name.Contains("GPU Core", StringComparison.OrdinalIgnoreCase))
                                  ?? hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature && s.Name.Contains("Core", StringComparison.OrdinalIgnoreCase))
                                  ?? hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature);

                    if (tempSensor?.Value.HasValue == true)
                    {
                        _gpuTemp = $"{Math.Round(tempSensor.Value.Value)}°";
                    }
                    else
                    {
                        // Fallback a Load si no hay temperatura
                        var loadSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Load && s.Name.Contains("D3D 3D", StringComparison.OrdinalIgnoreCase))
                                      ?? hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Load && s.Name.Contains("GPU Core", StringComparison.OrdinalIgnoreCase))
                                      ?? hardware.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Load);
                        
                        if (loadSensor?.Value.HasValue == true) _gpuTemp = $"{Math.Round(loadSensor.Value.Value)}%";
                    }
                }
            }
            HardwareChanged?.Invoke(this, new HardwareChangedEventArgs(_cpuTemp, _gpuTemp));
        }
        catch { }
    }

    public void Dispose()
    {
        _timer?.Dispose();
        try { _computer.Close(); } catch { }
    }
}
