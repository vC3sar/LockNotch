using System.Diagnostics;
using System.IO;
using LockNotch.Models;
using Windows.Media.Control;
using GsmtcManager = Windows.Media.Control.GlobalSystemMediaTransportControlsSessionManager;
using GsmtcSession = Windows.Media.Control.GlobalSystemMediaTransportControlsSession;

namespace LockNotch.Services;

public sealed class MediaService : IMediaService, IDisposable
{
    private readonly SemaphoreSlim _refreshLock = new(1, 1);

    private GsmtcManager? _manager;
    private GsmtcSession? _session;

    private string? _lastTrackKey;
    private string? _thumbnailKey;
    private byte[]? _thumbnail;

    public event EventHandler<MediaChangedEventArgs>? MediaChanged;

    public MediaInfo? Current { get; private set; }

    public async Task InitializeAsync()
    {
        try
        {
            _manager = await GsmtcManager.RequestAsync();
            _manager.CurrentSessionChanged += OnCurrentSessionChanged;
            _manager.SessionsChanged += OnSessionsChanged;
            await AttachSessionAsync(GetBestSession(_manager));
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[MediaService] Init failed: {ex}");
            Publish(null);
        }
    }

    #region Session tracking

    private async void OnCurrentSessionChanged(GsmtcManager sender, CurrentSessionChangedEventArgs args) =>
        await AttachSessionAsync(GetBestSession(sender));

    // Algunas apps cierran su sesión sin que cambie CurrentSession; se re-evalúa por seguridad.
    private async void OnSessionsChanged(GsmtcManager sender, SessionsChangedEventArgs args)
    {
        var best = GetBestSession(sender);
        if (best?.SourceAppUserModelId != _session?.SourceAppUserModelId || best is null)
            await AttachSessionAsync(best);
    }

    private GsmtcSession? GetBestSession(GsmtcManager manager)
    {
        var sessions = manager.GetSessions();
        if (sessions.Count == 0) return null;

        var knownApps = new[] { "spotify", "chrome", "msedge", "firefox", "brave", "vlc", "music", "opera", "tidal", "apple", "itunes" };

        var best = sessions.FirstOrDefault(s => 
            s.GetPlaybackInfo()?.PlaybackStatus == GlobalSystemMediaTransportControlsSessionPlaybackStatus.Playing &&
            knownApps.Any(app => s.SourceAppUserModelId?.Contains(app, StringComparison.OrdinalIgnoreCase) == true));
            
        if (best != null) return best;

        best = sessions.FirstOrDefault(s => 
            knownApps.Any(app => s.SourceAppUserModelId?.Contains(app, StringComparison.OrdinalIgnoreCase) == true));
            
        if (best != null) return best;

        return manager.GetCurrentSession();
    }

    private async Task AttachSessionAsync(GsmtcSession? session)
    {
        if (_session is not null)
        {
            _session.MediaPropertiesChanged -= OnMediaPropertiesChanged;
            _session.PlaybackInfoChanged -= OnPlaybackInfoChanged;
        }

        _session = session;

        if (_session is not null)
        {
            _session.MediaPropertiesChanged += OnMediaPropertiesChanged;
            _session.PlaybackInfoChanged += OnPlaybackInfoChanged;
            _session.TimelinePropertiesChanged += OnTimelinePropertiesChanged;
        }

        await RefreshAsync();
    }

    private async void OnMediaPropertiesChanged(GsmtcSession sender, MediaPropertiesChangedEventArgs args) =>
        await RefreshAsync();

    private async void OnPlaybackInfoChanged(GsmtcSession sender, PlaybackInfoChangedEventArgs args) =>
        await RefreshAsync();

    private async void OnTimelinePropertiesChanged(GsmtcSession sender, TimelinePropertiesChangedEventArgs args) =>
        await RefreshAsync();

    #endregion

    private async Task RefreshAsync()
    {
        await _refreshLock.WaitAsync();
        try
        {
            var session = _session;
            if (session is null)
            {
                Publish(null);
                return;
            }

            var props = await session.TryGetMediaPropertiesAsync();
            var playback = session.GetPlaybackInfo();
            bool isPlaying = playback?.PlaybackStatus == GlobalSystemMediaTransportControlsSessionPlaybackStatus.Playing;

            string title = props?.Title ?? string.Empty;
            string artist = props?.Artist ?? string.Empty;

            // Spotify a veces emite primero metadatos vacíos: se conserva la pista anterior.
            if (string.IsNullOrWhiteSpace(title) && Current is not null &&
                Current.SourceAppId == session.SourceAppUserModelId)
            {
                Publish(Current with { IsPlaying = isPlaying });
                return;
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                Publish(null);
                return;
            }

            var timeline = session.GetTimelineProperties();
            var info = new MediaInfo(
                title,
                artist,
                props?.AlbumTitle ?? string.Empty,
                isPlaying,
                null,
                session.SourceAppUserModelId ?? string.Empty,
                timeline?.Position ?? TimeSpan.Zero,
                timeline?.EndTime ?? TimeSpan.Zero,
                timeline?.LastUpdatedTime ?? DateTimeOffset.Now);

            info = info with { Thumbnail = await GetThumbnailAsync(info.TrackKey, props?.Thumbnail) };
            Publish(info);
        }
        catch (Exception ex)
        {
            // La sesión puede desaparecer en mitad de la lectura (app cerrada).
            Debug.WriteLine($"[MediaService] Refresh failed: {ex.Message}");
        }
        finally
        {
            _refreshLock.Release();
        }
    }

    private async Task<byte[]?> GetThumbnailAsync(string trackKey, Windows.Storage.Streams.IRandomAccessStreamReference? reference)
    {
        if (trackKey == _thumbnailKey && _thumbnail is not null) return _thumbnail;
        if (reference is null) return null;

        try
        {
            using var winStream = await reference.OpenReadAsync();
            using var stream = winStream.AsStreamForRead();
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            if (ms.Length == 0) return null;

            _thumbnailKey = trackKey;
            _thumbnail = ms.ToArray();
            return _thumbnail;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[MediaService] Thumbnail failed: {ex.Message}");
            return null;
        }
    }

    private void Publish(MediaInfo? info)
    {
        string? key = info?.TrackKey;
        bool trackChanged = key is not null && key != _lastTrackKey;
        _lastTrackKey = key;

        if (info == Current && !trackChanged) return;
        Current = info;
        MediaChanged?.Invoke(this, new MediaChangedEventArgs(info, trackChanged));
    }

    #region Controls

    public Task PlayAsync() => RunAsync(s => s.TryPlayAsync().AsTask());
    public Task PauseAsync() => RunAsync(s => s.TryPauseAsync().AsTask());
    public Task NextAsync() => RunAsync(s => s.TrySkipNextAsync().AsTask());
    public Task PreviousAsync() => RunAsync(s => s.TrySkipPreviousAsync().AsTask());

    private async Task RunAsync(Func<GsmtcSession, Task<bool>> action)
    {
        var session = _session;
        if (session is null) return;
        try
        {
            await action(session);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[MediaService] Control failed: {ex.Message}");
        }
    }

    #endregion

    public void Dispose()
    {
        if (_manager is not null)
        {
            _manager.CurrentSessionChanged -= OnCurrentSessionChanged;
            _manager.SessionsChanged -= OnSessionsChanged;
        }
        if (_session is not null)
        {
            _session.MediaPropertiesChanged -= OnMediaPropertiesChanged;
            _session.PlaybackInfoChanged -= OnPlaybackInfoChanged;
            _session.TimelinePropertiesChanged -= OnTimelinePropertiesChanged;
        }
        _refreshLock.Dispose();
    }
}
