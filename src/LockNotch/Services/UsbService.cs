using System;
using System.Windows.Interop;

namespace LockNotch.Services;

public sealed class UsbService : IUsbService, IDisposable
{
    public event EventHandler<bool>? UsbDeviceChanged;

    private const int WM_DEVICECHANGE = 0x0219;
    private const int DBT_DEVICEARRIVAL = 0x8000;
    private const int DBT_DEVICEREMOVECOMPLETE = 0x8004;

    private HwndSource? _source;

    public void Start()
    {
        var parameters = new HwndSourceParameters("UsbServiceWindow", 0, 0)
        {
            WindowStyle = 0,
            ExtendedWindowStyle = 0,
            ParentWindow = new IntPtr(-3) // HWND_MESSAGE
        };

        _source = new HwndSource(parameters);
        _source.AddHook(WndProc);
    }

    private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        if (msg == WM_DEVICECHANGE)
        {
            int eventType = wParam.ToInt32();
            if (eventType == DBT_DEVICEARRIVAL)
            {
                UsbDeviceChanged?.Invoke(this, true);
            }
            else if (eventType == DBT_DEVICEREMOVECOMPLETE)
            {
                UsbDeviceChanged?.Invoke(this, false);
            }
        }
        return IntPtr.Zero;
    }

    public void Dispose()
    {
        _source?.RemoveHook(WndProc);
        _source?.Dispose();
    }
}
