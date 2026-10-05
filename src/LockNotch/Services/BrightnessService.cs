using System;
using System.Management;

namespace LockNotch.Services;

public sealed class BrightnessService : IBrightnessService, IDisposable
{
    public event EventHandler<int>? BrightnessChanged;
    private ManagementEventWatcher? _watcher;

    public void Start()
    {
        try
        {
            var scope = new ManagementScope("root\\WMI");
            var query = new WqlEventQuery("SELECT * FROM WmiMonitorBrightnessEvent");
            _watcher = new ManagementEventWatcher(scope, query);
            _watcher.EventArrived += OnEventArrived;
            _watcher.Start();
        }
        catch
        {
            // Silently fail if not supported (e.g. desktop monitors)
        }
    }

    private void OnEventArrived(object sender, EventArrivedEventArgs e)
    {
        try
        {
            int brightness = Convert.ToInt32(e.NewEvent.Properties["Brightness"].Value);
            BrightnessChanged?.Invoke(this, brightness);
        }
        catch { }
    }

    public void Dispose()
    {
        if (_watcher != null)
        {
            _watcher.Stop();
            _watcher.EventArrived -= OnEventArrived;
            _watcher.Dispose();
        }
    }
}
