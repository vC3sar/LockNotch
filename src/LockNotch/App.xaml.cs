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
    private IslandViewModel? _viewModel;

    protected override async void OnStartup(StartupEventArgs e)
    {
        _singleInstance = new Mutex(true, "LockNotch.SingleInstance", out bool isFirst);
        if (!isFirst)
        {
            Shutdown();
            return;
        }

        base.OnStartup(e);

        // Composición manual de dependencias.
        _clock = new ClockService();
        _fullscreen = new FullscreenService();
        _media = new MediaService();
        _battery = new BatteryService();
        _weather = new WeatherService();
        _viewModel = new IslandViewModel(_clock, _fullscreen, _media, _battery, _weather);

        var window = new MainWindow(_viewModel);
        MainWindow = window;
        window.Show();

        await _viewModel.StartAsync();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _viewModel?.Dispose();
        _clock?.Dispose();
        _fullscreen?.Dispose();
        _media?.Dispose();
        _battery?.Dispose();
        _weather?.Dispose();
        _singleInstance?.Dispose();
        base.OnExit(e);
    }
}
