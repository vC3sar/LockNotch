namespace LockNotch.Models;

/// <summary>Instantánea de la sesión multimedia actual. Thumbnail en bytes para mantener el modelo independiente de WPF.</summary>
public sealed record MediaInfo(
    string Title,
    string Artist,
    string AlbumTitle,
    bool IsPlaying,
    byte[]? Thumbnail,
    string SourceAppId,
    TimeSpan Position,
    TimeSpan EndTime,
    DateTimeOffset LastUpdatedTime)
{
    public string TrackKey => $"{SourceAppId}|{Title}|{Artist}";
}

public sealed class MediaChangedEventArgs(MediaInfo? media, bool trackChanged) : EventArgs
{
    /// <summary>null cuando no hay ninguna sesión reproduciendo.</summary>
    public MediaInfo? Media { get; } = media;

    /// <summary>true si cambió la canción (no solo el estado play/pausa).</summary>
    public bool TrackChanged { get; } = trackChanged;
}
