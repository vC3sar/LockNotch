namespace LockNotch.Services;

public interface IFullscreenService
{
    /// <summary>Se dispara (en un hilo de fondo) cuando entra/sale una app en pantalla completa en el monitor principal.</summary>
    event EventHandler<bool>? FullscreenChanged;
    bool IsFullscreen { get; }
    void Start();
}
