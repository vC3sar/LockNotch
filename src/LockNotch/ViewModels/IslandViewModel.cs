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
    private readonly IUsbService _usb;
    private readonly IVolumeService _volume;
    private readonly IHardwareService _hardware;
    private readonly SynchronizationContext _ui;

    private byte[]? _thumbnailBytes;

    private CancellationTokenSource? _collapseCts;
    private bool _isPointerOver;
    private readonly System.Threading.Timer _progressTimer;
    private MediaInfo? _currentMedia;

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
    private bool _isNotifying;

    [ObservableProperty]
    private NotificationRequest? _currentNotification;

    private readonly Queue<NotificationRequest> _notificationQueue = new();
    private CancellationTokenSource? _notificationCts;

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

    [ObservableProperty]
    private double _progressPercentage;

    [ObservableProperty]
    private bool _hasProgress;

    [ObservableProperty]
    private string _positionText = "0:00";

    [ObservableProperty]
    private string _durationText = "0:00";

    [ObservableProperty]
    private string _cpuTemp = "--°";

    [ObservableProperty]
    private string _gpuTemp = "--°";

    [ObservableProperty]
    private bool _showHardwarePanel;

    public IslandState State =>
        IsHiddenForFullscreen ? IslandState.Hidden :
        IsNotifying ? IslandState.Notification :
        IsExpanded ? IslandState.Expanded : IslandState.Compact;

    public IslandViewModel(IClockService clock, IFullscreenService fullscreen, IMediaService media, IBatteryService battery, IWeatherService weather, IUsbService usb, IVolumeService volume, IHardwareService hardware)
    {
        _clock = clock;
        _fullscreen = fullscreen;
        _media = media;
        _battery = battery;
        _weather = weather;
        _usb = usb;
        _volume = volume;
        _hardware = hardware;
        _ui = SynchronizationContext.Current ?? new SynchronizationContext();

        TimeText = _clock.Now.ToString("h:mm tt");

        _clock.MinuteChanged += OnMinuteChanged;
        _fullscreen.FullscreenChanged += OnFullscreenChanged;
        _media.MediaChanged += OnMediaChanged;
        _battery.BatteryChanged += OnBatteryChanged;
        _weather.WeatherChanged += OnWeatherChanged;
        _usb.UsbDeviceChanged += OnUsbDeviceChanged;
        _volume.VolumeChanged += OnVolumeChanged;
        _hardware.HardwareChanged += OnHardwareChanged;

        _progressTimer = new System.Threading.Timer(OnProgressTick, null, Timeout.Infinite, Timeout.Infinite);
    }

    public async Task StartAsync()
    {
        _clock.Start();
        _fullscreen.Start();
        _battery.Start();
        _weather.Start();
        _usb.Start();
        _volume.Start();
        _hardware.Start();
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

    #region Notifications

    public void EnqueueNotification(NotificationRequest req)
    {
        _ui.Post(_ =>
        {
            _notificationQueue.Enqueue(req);
            if (!IsNotifying) ProcessNextNotification();
        }, null);
    }

    private async void ProcessNextNotification()
    {
        if (_notificationQueue.Count == 0)
        {
            IsNotifying = false;
            return;
        }

        var req = _notificationQueue.Dequeue();
        CurrentNotification = req;
        IsNotifying = true;

        _notificationCts?.Cancel();
        _notificationCts?.Dispose();
        _notificationCts = new CancellationTokenSource();
        var token = _notificationCts.Token;

        try
        {
            await Task.Delay(req.Duration, token);
            if (!token.IsCancellationRequested)
            {
                IsNotifying = false;
                await Task.Delay(200, token); // Small gap for collapse animation
                if (!token.IsCancellationRequested) ProcessNextNotification();
            }
        }
        catch (TaskCanceledException) { }
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

        _currentMedia = media;
        HasProgress = media?.EndTime > TimeSpan.Zero;
        
        if (HasProgress)
        {
            UpdateProgress();
            if (IsPlaying)
                _progressTimer.Change(500, 500);
            else
                _progressTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }
        else
        {
            _progressTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        if (trackChanged) ExpandTemporarily(TrackChangeExpandDuration);
    }

    private void OnProgressTick(object? state) => _ui.Post(_ => UpdateProgress(), null);

    private void UpdateProgress()
    {
        var media = _currentMedia;
        if (media is null || media.EndTime <= TimeSpan.Zero) return;

        var position = media.Position;
        if (media.IsPlaying)
        {
            position += DateTimeOffset.Now - media.LastUpdatedTime;
        }

        if (position > media.EndTime) position = media.EndTime;
        if (position < TimeSpan.Zero) position = TimeSpan.Zero;

        ProgressPercentage = position.TotalSeconds / media.EndTime.TotalSeconds * 100;
        PositionText = FormatTimeSpan(position);
        DurationText = FormatTimeSpan(media.EndTime);
    }

    private static string FormatTimeSpan(TimeSpan t) =>
        t.TotalHours >= 1 ? $"{(int)t.TotalHours}:{t.Minutes:D2}:{t.Seconds:D2}" : $"{t.Minutes}:{t.Seconds:D2}";

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
        _ui.Post(_ => TimeText = now.ToString("h:mm tt"), null);

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

    private bool? _wasCharging;

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
            else if (charge >= 80) BatteryGlyph = "\xE850";
            else if (charge >= 70) BatteryGlyph = "\xE858";
            else if (charge >= 60) BatteryGlyph = "\xE857";
            else if (charge >= 50) BatteryGlyph = "\xE856";
            else if (charge >= 40) BatteryGlyph = "\xE855";
            else if (charge >= 30) BatteryGlyph = "\xE854";
            else if (charge >= 20) BatteryGlyph = "\xE853";
            else if (charge >= 10) BatteryGlyph = "\xE852";
            else BatteryGlyph = "\xE850"; // Batería baja

            if (_wasCharging.HasValue && _wasCharging.Value != charging)
            {
                var brush = (System.Windows.Media.Brush)System.Windows.Application.Current.Resources[charging ? "AccentBrush" : "TextPrimaryBrush"];
                var text = charging ? $"Cargando: {BatteryText}" : "Batería en uso";
                EnqueueNotification(new NotificationRequest(BatteryGlyph, text, brush, TimeSpan.FromSeconds(2.5)));
            }
            _wasCharging = charging;

        }, null);

    private void OnWeatherChanged(object? sender, EventArgs e) =>
        _ui.Post(_ =>
        {
            WeatherText = _weather.Temperature;
            WeatherGlyph = _weather.ConditionGlyph;
        }, null);

    private void OnUsbDeviceChanged(object? sender, bool connected) =>
        _ui.Post(_ =>
        {
            var brush = (System.Windows.Media.Brush)System.Windows.Application.Current.Resources[connected ? "AccentBrush" : "TextSecondaryBrush"];
            var text = connected ? "Dispositivo USB conectado" : "Dispositivo USB desconectado";
            EnqueueNotification(new NotificationRequest("\xE88E", text, brush, TimeSpan.FromSeconds(2.5)));
        }, null);

    private void OnVolumeChanged(object? sender, VolumeChangedEventArgs e) =>
        _ui.Post(_ =>
        {
            var brush = (System.Windows.Media.Brush)System.Windows.Application.Current.Resources[e.IsMuted ? "DangerBrush" : "TextPrimaryBrush"];
            var glyph = e.IsMuted ? "\xE74F" : (e.VolumePercent >= 50 ? "\xE995" : "\xE993");
            var text = e.IsMuted ? "Silenciado" : $"Volumen: {Math.Round(e.VolumePercent)}%";
            EnqueueNotification(new NotificationRequest(glyph, text, brush, TimeSpan.FromSeconds(2)));
        }, null);

    private void OnHardwareChanged(object? sender, HardwareChangedEventArgs e) =>
        _ui.Post(_ =>
        {
            CpuTemp = e.CpuTemp;
            GpuTemp = e.GpuTemp;
        }, null);

    public void Dispose()
    {
        _clock.MinuteChanged -= OnMinuteChanged;
        _fullscreen.FullscreenChanged -= OnFullscreenChanged;
        _media.MediaChanged -= OnMediaChanged;
        _battery.BatteryChanged -= OnBatteryChanged;
        _weather.WeatherChanged -= OnWeatherChanged;
        _usb.UsbDeviceChanged -= OnUsbDeviceChanged;
        _volume.VolumeChanged -= OnVolumeChanged;
        _hardware.HardwareChanged -= OnHardwareChanged;
        CancelPendingCollapse();
        _notificationCts?.Dispose();
        _progressTimer?.Dispose();
    }
}
