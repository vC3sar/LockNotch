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
    private readonly INotificationService _notifications;
    private readonly SettingsService _settings;
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
    private string _weatherGlyph = "\u2600\uFE0F";

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
    private int _currentPageIndex;

    [ObservableProperty]
    private int _totalPages = 4;

    public void NextPage() => CurrentPageIndex = (CurrentPageIndex + 1) % TotalPages;
    public void PreviousPage() => CurrentPageIndex = (CurrentPageIndex - 1 + TotalPages) % TotalPages;

    public IslandState State =>
        IsHiddenForFullscreen ? IslandState.Hidden :
        IsNotifying ? IslandState.Notification :
        IsExpanded ? IslandState.Expanded : IslandState.Compact;

    public IslandViewModel(IClockService clock, IFullscreenService fullscreen, IMediaService media, IBatteryService battery, IWeatherService weather, IUsbService usb, IVolumeService volume, IHardwareService hardware, INotificationService notifications, SettingsService settings)
    {
        _clock = clock;
        _fullscreen = fullscreen;
        _media = media;
        _battery = battery;
        _weather = weather;
        _usb = usb;
        _volume = volume;
        _hardware = hardware;
        _notifications = notifications;
        _settings = settings;
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
        _notifications.NotificationReceived += OnNotificationReceived;

        _progressTimer = new System.Threading.Timer(OnProgressTick, null, Timeout.Infinite, Timeout.Infinite);
        
        InitializeToggles();
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

    #region App Launcher

    [RelayCommand]
    private void LaunchApp(string? appId)
    {
        if (string.IsNullOrWhiteSpace(appId)) return;
        try
        {
            string appPath = appId switch
            {
                "1" => _settings.Current.AppLauncher1,
                "2" => _settings.Current.AppLauncher2,
                "3" => _settings.Current.AppLauncher3,
                "4" => _settings.Current.AppLauncher4,
                "5" => _settings.Current.AppLauncher5,
                _ => appId // Fallback: si pasan una ruta directa como ms-settings:network-wifi
            };

            if (string.IsNullOrWhiteSpace(appPath)) return;

            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = appPath,
                UseShellExecute = true
            };
            System.Diagnostics.Process.Start(psi);
            IsExpanded = false;
        }
        catch { }
    }

    #endregion

    #region Control Center Toggles

    [ObservableProperty] private bool _isWifiEnabled;
    [ObservableProperty] private bool _isBluetoothEnabled;
    [ObservableProperty] private bool _isDarkModeEnabled;
    [ObservableProperty] private bool _isDndEnabled;
    
    private DateTime _suppressUsbNotificationsUntil = DateTime.MinValue;

    private async void InitializeToggles()
    {
        try
        {
            var radios = await Windows.Devices.Radios.Radio.GetRadiosAsync();
            var wifi = radios.FirstOrDefault(r => r.Kind == Windows.Devices.Radios.RadioKind.WiFi);
            var bt = radios.FirstOrDefault(r => r.Kind == Windows.Devices.Radios.RadioKind.Bluetooth);
            
            if (wifi != null) IsWifiEnabled = wifi.State == Windows.Devices.Radios.RadioState.On;
            if (bt != null) IsBluetoothEnabled = bt.State == Windows.Devices.Radios.RadioState.On;

            using var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            if (key != null && key.GetValue("AppsUseLightTheme") is int val) IsDarkModeEnabled = val == 0;
            
            // Focus Assist (DND) check via WNF is complex, using generic registry key for QuietHours
            using var qhKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Policies\Microsoft\Windows\CurrentVersion\QuietHours");
            if (qhKey != null && qhKey.GetValue("EnableQuietHours") is int qh) IsDndEnabled = qh == 1;
        }
        catch { }
    }

    [RelayCommand]
    private async Task ToggleWifi()
    {
        _suppressUsbNotificationsUntil = DateTime.Now.AddSeconds(5);
        try
        {
            var radios = await Windows.Devices.Radios.Radio.GetRadiosAsync();
            var wifi = radios.FirstOrDefault(r => r.Kind == Windows.Devices.Radios.RadioKind.WiFi);
            if (wifi != null)
            {
                // WPF ya cambió IsWifiEnabled al nuevo estado deseado al hacer clic.
                var targetState = IsWifiEnabled ? Windows.Devices.Radios.RadioState.On : Windows.Devices.Radios.RadioState.Off;
                await wifi.SetStateAsync(targetState);
                
                // Confirmamos el estado real
                IsWifiEnabled = wifi.State == Windows.Devices.Radios.RadioState.On;
                return;
            }
        }
        catch { }

        // Fallback agresivo vía netsh
        try
        {
            string cmd = IsWifiEnabled ? "interface set interface \"Wi-Fi\" admin=enable" : "interface set interface \"Wi-Fi\" admin=disable";
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "netsh",
                Arguments = cmd,
                CreateNoWindow = true,
                UseShellExecute = false
            });
        }
        catch { }
    }

    [RelayCommand]
    private async Task ToggleBluetooth()
    {
        _suppressUsbNotificationsUntil = DateTime.Now.AddSeconds(5);
        try
        {
            var radios = await Windows.Devices.Radios.Radio.GetRadiosAsync();
            var bt = radios.FirstOrDefault(r => r.Kind == Windows.Devices.Radios.RadioKind.Bluetooth);
            if (bt != null)
            {
                var targetState = IsBluetoothEnabled ? Windows.Devices.Radios.RadioState.On : Windows.Devices.Radios.RadioState.Off;
                await bt.SetStateAsync(targetState);
                IsBluetoothEnabled = bt.State == Windows.Devices.Radios.RadioState.On;
                return;
            }
        }
        catch { }

        // Fallback agresivo vía PowerShell
        try
        {
            string psCmd = IsBluetoothEnabled ? "Start-Service bthserv" : "Stop-Service bthserv -Force";
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "powershell",
                Arguments = $"-Command \"{psCmd}\"",
                CreateNoWindow = true,
                UseShellExecute = false
            });
        }
        catch { }
    }

    [RelayCommand]
    private void ToggleDarkMode()
    {
        try
        {
            string keyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
            using var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(keyPath, true);
            if (key != null)
            {
                // IsDarkModeEnabled ya refleja el nuevo estado deseado gracias al ToggleButton
                int newValue = IsDarkModeEnabled ? 0 : 1;
                key.SetValue("AppsUseLightTheme", newValue, Microsoft.Win32.RegistryValueKind.DWord);
                key.SetValue("SystemUsesLightTheme", newValue, Microsoft.Win32.RegistryValueKind.DWord);
            }
        }
        catch { }
    }

    [RelayCommand]
    private void ToggleDnd()
    {
        try
        {
            // IsDndEnabled ya refleja el nuevo estado deseado gracias al ToggleButton
            
            // Toggle QuietHours global registry key
            string qhPath = @"Software\Policies\Microsoft\Windows\CurrentVersion\QuietHours";
            using var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(qhPath, true);
            if (key != null)
            {
                key.SetValue("EnableQuietHours", IsDndEnabled ? 1 : 0, Microsoft.Win32.RegistryValueKind.DWord);
            }
            
            // No cerramos el panel
        }
        catch { }
    }

    #endregion

    #region Notifications

    public void EnqueueNotification(NotificationRequest req)
    {
        _ui.Post(_ =>
        {
            // Si la notificación actual es del mismo tipo, la actualizamos en vivo
            if (IsNotifying && CurrentNotification?.Id == req.Id)
            {
                CurrentNotification = req;
                
                // Reiniciar el temporizador para prolongar su estancia
                _notificationCts?.Cancel();
                _notificationCts?.Dispose();
                _notificationCts = new CancellationTokenSource();
                
                _ = RunNotificationTimerAsync(req.Duration, _notificationCts.Token);
                return;
            }

            // Si hay una en la cola del mismo tipo, la reemplazamos
            var list = _notificationQueue.ToList();
            var idx = list.FindIndex(n => n.Id == req.Id);
            if (idx >= 0)
            {
                list[idx] = req;
                _notificationQueue.Clear();
                foreach (var item in list) _notificationQueue.Enqueue(item);
                return;
            }

            _notificationQueue.Enqueue(req);
            if (!IsNotifying) ProcessNextNotification();
        }, null);
    }

    private async Task RunNotificationTimerAsync(TimeSpan duration, CancellationToken token)
    {
        try
        {
            await Task.Delay(duration, token);
            if (!token.IsCancellationRequested)
            {
                IsNotifying = false;
                await Task.Delay(200, token); // Pequeña pausa para permitir que termine la animación
                if (!token.IsCancellationRequested) ProcessNextNotification();
            }
        }
        catch (TaskCanceledException) { }
    }

    private void ProcessNextNotification()
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
        
        _ = RunNotificationTimerAsync(req.Duration, _notificationCts.Token);
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
                EnqueueNotification(new NotificationRequest("battery", BatteryGlyph, text, brush, TimeSpan.FromSeconds(2.5)));
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
            if (DateTime.Now < _suppressUsbNotificationsUntil) return;
            var brush = (System.Windows.Media.Brush)System.Windows.Application.Current.Resources[connected ? "AccentBrush" : "TextSecondaryBrush"];
            var text = connected ? "Dispositivo USB conectado" : "Dispositivo USB desconectado";
            EnqueueNotification(new NotificationRequest("usb", "\xE88E", text, brush, TimeSpan.FromSeconds(2.5)));
        }, null);

    private void OnVolumeChanged(object? sender, VolumeChangedEventArgs e) =>
        _ui.Post(_ =>
        {
            var brush = (System.Windows.Media.Brush)System.Windows.Application.Current.Resources[e.IsMuted ? "DangerBrush" : "TextPrimaryBrush"];
            var glyph = e.IsMuted ? "\xE74F" : (e.VolumePercent >= 50 ? "\xE995" : "\xE993");
            var text = e.IsMuted ? "Silenciado" : $"Volumen: {Math.Round(e.VolumePercent)}%";
            EnqueueNotification(new NotificationRequest("volume", glyph, text, brush, TimeSpan.FromSeconds(2)));
        }, null);

    private void OnHardwareChanged(object? sender, HardwareChangedEventArgs e) =>
        _ui.Post(_ =>
        {
            CpuTemp = e.CpuTemp;
            GpuTemp = e.GpuTemp;
        }, null);

    private void OnNotificationReceived(object? sender, NotificationEventArgs e) =>
        _ui.Post(_ =>
        {
            if (IsDndEnabled) return; // Respetar el Modo No Molestar interno

            string glyph = "\xE7E7"; // Default mail/message icon
            if (e.AppName.Contains("Discord", StringComparison.OrdinalIgnoreCase)) glyph = "\xE909"; // Discord/Chat like icon
            if (e.AppName.Contains("WhatsApp", StringComparison.OrdinalIgnoreCase)) glyph = "\xE8F3";

            var brush = (System.Windows.Media.Brush)System.Windows.Application.Current.Resources["AccentBrush"];
            string text = string.IsNullOrWhiteSpace(e.Body) ? e.Title : $"{e.Title}: {e.Body}";
            
            // Si el texto es muy largo, recortarlo
            if (text.Length > 40) text = text.Substring(0, 37) + "...";

            EnqueueNotification(new NotificationRequest("win_notif_" + e.AppName, glyph, text, brush, TimeSpan.FromSeconds(4)));
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
        _notifications.NotificationReceived -= OnNotificationReceived;
        CancelPendingCollapse();
        _notificationCts?.Dispose();
        _progressTimer?.Dispose();
    }
}
