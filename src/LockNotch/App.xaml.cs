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
    private HardwareService? _hardware;
    private NotificationService? _notifications;
    private SettingsService? _settings;
    private IslandViewModel? _viewModel;
    private System.Windows.Forms.NotifyIcon? _notifyIcon;

    protected override async void OnStartup(StartupEventArgs e)
    {
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
        _hardware = new HardwareService(_settings);
        _notifications = new NotificationService();
        _viewModel = new IslandViewModel(_clock, _fullscreen, _media, _battery, _weather, _usb, _volume, _hardware, _notifications, _settings);

        var window = new MainWindow(_viewModel);
        MainWindow = window;
        window.Show();

        InitializeTrayIcon();

        await _viewModel.StartAsync();
        await _notifications.InitializeAsync();
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

        if (_settings != null)
        {
            var sw = new SettingsWindow(_settings);
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
        _hardware?.Dispose();
        _notifications?.Dispose();
        _singleInstance?.Dispose();
        base.OnExit(e);
    }
}
