using LockNotch.Models;

namespace LockNotch.Services;

public interface IMediaService
{
    /// <summary>Se dispara (en un hilo de fondo) al cambiar sesión, metadatos o estado de reproducción.</summary>
    event EventHandler<MediaChangedEventArgs>? MediaChanged;

    MediaInfo? Current { get; }

    Task InitializeAsync();
    Task PlayAsync();
    Task PauseAsync();
    Task NextAsync();
    Task PreviousAsync();
}
