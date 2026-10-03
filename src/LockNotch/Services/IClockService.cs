namespace LockNotch.Services;

public interface IClockService
{
    /// <summary>Se dispara (en un hilo de fondo) cada vez que cambia el minuto.</summary>
    event EventHandler<DateTime>? MinuteChanged;
    DateTime Now { get; }
    void Start();
}
