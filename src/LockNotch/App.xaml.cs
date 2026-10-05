using System.Linq;
using System.Windows;
using LockNotch.Services;
using LockNotch.ViewModels;

namespace LockNotch;

public partial class App : System.Windows.Application
{
    private Mutex? _singleInstance;
    private ClockService? _clock;
    private FullscreenService? _fullscreen;
    private MediaService? _media;
    private BatteryService? _battery;
    private WeatherService? _weather;
    private UsbService? _usb;
    private VolumeService? _volume;
    private BrightnessService? _brightness;
    private HardwareService? _hardware;
    private NotificationService? _notifications;
    private SettingsService? _settings;
    private DownloadService? _downloads;
    private IslandViewModel? _viewModel;
    private System.Windows.Forms.NotifyIcon? _notifyIcon;

    protected override async void OnStartup(StartupEventArgs e)
    {
        // Chrome Native Messaging Host interception
        if (e.Args.Any(arg => arg.StartsWith("chrome-extension://")))
        {
            NativeMessagingProxy.Run();
            Shutdown();
            return;
        }

        _singleInstance = new Mutex(true, "LockNotch.SingleInstance", out bool isFirst);
        if (!isFirst)
        {
            Shutdown();
            return;
        }

        base.OnStartup(e);

        _settings = new SettingsService();

        // Composición manual de dependencias.
        _clock = new ClockService();
        _fullscreen = new FullscreenService();
        _media = new MediaService();
        _battery = new BatteryService();
        _weather = new WeatherService(_settings);
        _usb = new UsbService();
        _volume = new VolumeService();
        _brightness = new BrightnessService();
        _hardware = new HardwareService(_settings);
        _notifications = new NotificationService();
        _downloads = new DownloadService();
        _viewModel = new IslandViewModel(_clock, _fullscreen, _media, _battery, _weather, _usb, _volume, _brightness, _hardware, _notifications, _settings, _downloads);

        LockNotch.Helpers.AppearanceManager.Apply(_settings.Current.Theme, _settings.Current.FontFamily);

        var window = new MainWindow(_viewModel);
        MainWindow = window;
        window.Show();

        InitializeTrayIcon();

        await _viewModel.StartAsync();
        await _notifications.InitializeAsync();
        _downloads.LocationUpdated += (s, loc) =>
        {
            if (_settings != null)
            {
                _settings.Current.WeatherLatitude = loc.lat;
                _settings.Current.WeatherLongitude = loc.lon;
                _settings.Save();
                _weather?.Refresh();
            }
        };
        _downloads.Start();
    }

    private void InitializeTrayIcon()
    {
        _notifyIcon = new System.Windows.Forms.NotifyIcon
        {
            Icon = System.Drawing.SystemIcons.Application,
            Visible = true,
            Text = "LockNotch"
        };

        var contextMenu = new System.Windows.Forms.ContextMenuStrip();
        
        var settingsItem = new System.Windows.Forms.ToolStripMenuItem("Configuración");
        settingsItem.Click += (s, e) => ShowSettingsWindow();
        
        var exitItem = new System.Windows.Forms.ToolStripMenuItem("Salir");
        exitItem.Click += (s, e) => Shutdown();

        contextMenu.Items.Add(settingsItem);
        contextMenu.Items.Add("-");
        contextMenu.Items.Add(exitItem);

        _notifyIcon.ContextMenuStrip = contextMenu;
        
        _notifyIcon.DoubleClick += (s, e) => ShowSettingsWindow();
    }

    private void ShowSettingsWindow()
    {
        // Si ya hay una ventana de configuración abierta, la traemos al frente
        foreach (Window w in Windows)
        {
            if (w is SettingsWindow)
            {
                w.Activate();
                return;
            }
        }

        if (_settings != null && _downloads != null)
        {
            var sw = new SettingsWindow(_settings, _downloads);
            sw.Show();
        }
    }

    protected override void OnExit(ExitEventArgs e)
    {
        if (_notifyIcon != null)
        {
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
        }

        _viewModel?.Dispose();
        _clock?.Dispose();
        _fullscreen?.Dispose();
        _media?.Dispose();
        _battery?.Dispose();
        _weather?.Dispose();
        _usb?.Dispose();
        _volume?.Dispose();
        _brightness?.Dispose();
        _hardware?.Dispose();
        _notifications?.Dispose();
        _downloads?.Dispose();
        _singleInstance?.Dispose();
        base.OnExit(e);
    }
}
