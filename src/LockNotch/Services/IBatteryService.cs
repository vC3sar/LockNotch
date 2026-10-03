namespace LockNotch.Services;

public interface IBatteryService
{
    event EventHandler? BatteryChanged;
    
    int ChargePercent { get; }
    bool IsCharging { get; }
    bool IsBatteryLow { get; }

    void Start();
}
