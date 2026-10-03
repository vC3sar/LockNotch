using System.Windows.Forms; // Usado para SystemInformation.PowerStatus si Windows.Devices.Power es más complejo en TFM antiguo

namespace LockNotch.Services;

public sealed class BatteryService : IBatteryService, IDisposable
{
    private System.Threading.Timer? _timer;
    private int _chargePercent = -1;
    private bool _isCharging;
    
    public event EventHandler? BatteryChanged;

    public int ChargePercent => _chargePercent;
    public bool IsCharging => _isCharging;
    public bool IsBatteryLow => _chargePercent <= 20;

    public void Start()
    {
        _timer ??= new System.Threading.Timer(_ => Check(), null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
    }

    private void Check()
    {
        var status = SystemInformation.PowerStatus;
        int charge = (int)(status.BatteryLifePercent * 100);
        bool charging = status.PowerLineStatus == PowerLineStatus.Online;
        
        // Si no hay batería (sobremesa), LifePercent es 255.
        if (charge > 100) charge = 100; 

        if (charge == _chargePercent && charging == _isCharging) return;

        _chargePercent = charge;
        _isCharging = charging;
        BatteryChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
