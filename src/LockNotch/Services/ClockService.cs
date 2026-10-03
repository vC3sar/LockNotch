namespace LockNotch.Services;

public sealed class ClockService : IClockService, IDisposable
{
    private System.Threading.Timer? _timer;
    private int _lastMinute = -1;

    public event EventHandler<DateTime>? MinuteChanged;

    public DateTime Now => DateTime.Now;

    public void Start()
    {
        _timer ??= new System.Threading.Timer(_ => Tick(), null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
    }

    private void Tick()
    {
        var now = DateTime.Now;
        int minuteKey = now.Hour * 60 + now.Minute;
        if (minuteKey == _lastMinute) return;
        _lastMinute = minuteKey;
        MinuteChanged?.Invoke(this, now);
    }

    public void Dispose() => _timer?.Dispose();
}
