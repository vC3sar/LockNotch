using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LockNotch.Models;
using LockNotch.Services;

namespace LockNotch.ViewModels;

public partial class IslandViewModel : ObservableObject, IDisposable
{
    private const string EmptyTitle = "Nada reproduciéndose";
    private const string EmptyArtist = "Abre Spotify o YouTube Music";
    private static readonly TimeSpan TrackChangeExpandDuration = TimeSpan.FromSeconds(2);

    private readonly IClockService _clock;
    private readonly IFullscreenService _fullscreen;
    private readonly IMediaService _media;
    private readonly IBatteryService _battery;
    private readonly IWeatherService _weather;
    private readonly SynchronizationContext _ui;

    private byte[]? _thumbnailBytes;

    private CancellationTokenSource? _collapseCts;
    private bool _isPointerOver;

    public TimeSpan CollapseDelay { get; set; } = TimeSpan.FromMilliseconds(400);

    [ObservableProperty]
    private string _timeText = "--:--";

    [ObservableProperty]
    private string _weatherText = "--°";

    [ObservableProperty]
    private string _weatherGlyph = "\xE706";

    [ObservableProperty]
    private string _batteryText = "--%";

    [ObservableProperty]
    private string _batteryGlyph = "\xE83F";

    [ObservableProperty]
    private bool _isCharging;

    [ObservableProperty]
    private bool _isBatteryLow;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(State))]
    private bool _isExpanded;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(State))]
    private bool _isHiddenForFullscreen;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(PreviousCommand), nameof(PlayPauseCommand), nameof(NextCommand))]
    private bool _hasMedia;

    [ObservableProperty]
    private string _title = EmptyTitle;

    [ObservableProperty]
    private string _artist = EmptyArtist;

    [ObservableProperty]
    private bool _isPlaying;

    [ObservableProperty]
    private ImageSource? _thumbnail;

    public IslandState State =>
        IsHiddenForFullscreen ? IslandState.Hidden :
        IsExpanded ? IslandState.Expanded : IslandState.Compact;

    public IslandViewModel(IClockService clock, IFullscreenService fullscreen, IMediaService media, IBatteryService battery, IWeatherService weather)
    {
        _clock = clock;
        _fullscreen = fullscreen;
        _media = media;
        _battery = battery;
        _weather = weather;
        _ui = SynchronizationContext.Current ?? new SynchronizationContext();

        TimeText = _clock.Now.ToString("HH:mm");

        _clock.MinuteChanged += OnMinuteChanged;
        _fullscreen.FullscreenChanged += OnFullscreenChanged;
        _media.MediaChanged += OnMediaChanged;
        _battery.BatteryChanged += OnBatteryChanged;
        _weather.WeatherChanged += OnWeatherChanged;
    }

    public async Task StartAsync()
    {
        _clock.Start();
        _fullscreen.Start();
        _battery.Start();
        _weather.Start();
        await _media.InitializeAsync();
    }

    #region Expand / collapse

    public void PointerEntered()
    {
        _isPointerOver = true;
        CancelPendingCollapse();
        IsExpanded = true;
    }

    public void PointerExited()
    {
        _isPointerOver = false;
        ScheduleCollapse(CollapseDelay);
    }

    [RelayCommand]
    private void Expand()
    {
        CancelPendingCollapse();
        IsExpanded = true;
        if (!_isPointerOver) ScheduleCollapse(TimeSpan.FromSeconds(3));
    }

    /// <summary>Expande durante un tiempo y vuelve a compacto (usado al cambiar de canción).</summary>
    public void ExpandTemporarily(TimeSpan duration)
    {
        if (IsHiddenForFullscreen) return;
        IsExpanded = true;
        if (!_isPointerOver) ScheduleCollapse(duration);
    }

    private async void ScheduleCollapse(TimeSpan delay)
    {
        CancelPendingCollapse();
        var cts = new CancellationTokenSource();
        _collapseCts = cts;
        try
        {
            await Task.Delay(delay, cts.Token);
        }
        catch (TaskCanceledException)
        {
            return;
        }
        if (!_isPointerOver) IsExpanded = false;
    }

    private void CancelPendingCollapse()
    {
        _collapseCts?.Cancel();
        _collapseCts?.Dispose();
        _collapseCts = null;
    }

    #endregion

    #region Media

    [RelayCommand(CanExecute = nameof(HasMedia), AllowConcurrentExecutions = true)]
    private Task PreviousAsync() => _media.PreviousAsync();

    [RelayCommand(CanExecute = nameof(HasMedia), AllowConcurrentExecutions = true)]
    private Task PlayPauseAsync()
    {
        bool wasPlaying = IsPlaying;
        IsPlaying = !wasPlaying; // Respuesta inmediata; PlaybackInfoChanged confirmará el estado real.
        return wasPlaying ? _media.PauseAsync() : _media.PlayAsync();
    }

    [RelayCommand(CanExecute = nameof(HasMedia), AllowConcurrentExecutions = true)]
    private Task NextAsync() => _media.NextAsync();

    private void OnMediaChanged(object? sender, MediaChangedEventArgs e) =>
        _ui.Post(_ => ApplyMedia(e.Media, e.TrackChanged), null);

    private void ApplyMedia(MediaInfo? media, bool trackChanged)
    {
        HasMedia = media is not null;
        Title = media?.Title ?? EmptyTitle;
        Artist = media is null ? EmptyArtist : (string.IsNullOrWhiteSpace(media.Artist) ? media.AlbumTitle : media.Artist);
        IsPlaying = media?.IsPlaying ?? false;

        if (!ReferenceEquals(media?.Thumbnail, _thumbnailBytes))
        {
            _thumbnailBytes = media?.Thumbnail;
            Thumbnail = CreateBitmap(_thumbnailBytes);
        }

        if (trackChanged) ExpandTemporarily(TrackChangeExpandDuration);
    }

    private static ImageSource? CreateBitmap(byte[]? bytes)
    {
        if (bytes is null || bytes.Length == 0) return null;
        try
        {
            var bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.CacheOption = BitmapCacheOption.OnLoad;
            bmp.DecodePixelWidth = 128;
            bmp.StreamSource = new MemoryStream(bytes);
            bmp.EndInit();
            bmp.Freeze();
            return bmp;
        }
        catch
        {
            return null;
        }
    }

    #endregion

    private void OnMinuteChanged(object? sender, DateTime now) =>
        _ui.Post(_ => TimeText = now.ToString("HH:mm"), null);

    private void OnFullscreenChanged(object? sender, bool isFullscreen) =>
        _ui.Post(_ =>
        {
            if (isFullscreen)
            {
                CancelPendingCollapse();
                IsExpanded = false;
            }
            IsHiddenForFullscreen = isFullscreen;
        }, null);

    private void OnBatteryChanged(object? sender, EventArgs e) =>
        _ui.Post(_ =>
        {
            int charge = _battery.ChargePercent;
            bool charging = _battery.IsCharging;
            
            BatteryText = charge < 0 ? "--%" : $"{charge}%";
            IsCharging = charging;
            IsBatteryLow = _battery.IsBatteryLow;
            
            if (charging) BatteryGlyph = "\xE83E"; // Batería cargando
            else if (charge >= 95) BatteryGlyph = "\xE83F";
            else if (charge >= 80) BatteryGlyph = "\xE850"; // Batería 8 - aprox 80% (o similar, pero vamos a dejar el E83F para todas, o E850-E859 según la fuente Fluent Icons). Fluent Icons tiene de E850 a E859 para la carga de 0 a 10.
            else if (charge >= 70) BatteryGlyph = "\xE858";
            else if (charge >= 60) BatteryGlyph = "\xE857";
            else if (charge >= 50) BatteryGlyph = "\xE856";
            else if (charge >= 40) BatteryGlyph = "\xE855";
            else if (charge >= 30) BatteryGlyph = "\xE854";
            else if (charge >= 20) BatteryGlyph = "\xE853";
            else if (charge >= 10) BatteryGlyph = "\xE852";
            else BatteryGlyph = "\xE850"; // Batería baja

        }, null);

    private void OnWeatherChanged(object? sender, EventArgs e) =>
        _ui.Post(_ =>
        {
            WeatherText = _weather.Temperature;
            WeatherGlyph = _weather.ConditionGlyph;
        }, null);

    public void Dispose()
    {
        _clock.MinuteChanged -= OnMinuteChanged;
        _fullscreen.FullscreenChanged -= OnFullscreenChanged;
        _media.MediaChanged -= OnMediaChanged;
        _battery.BatteryChanged -= OnBatteryChanged;
        _weather.WeatherChanged -= OnWeatherChanged;
        CancelPendingCollapse();
    }
}
