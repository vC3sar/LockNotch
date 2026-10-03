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
            ParentWindow = IntPtr.Zero // HWND_DESKTOP allows receiving broadcast messages
        };

        _source = new HwndSource(parameters);
        _source.AddHook(WndProc);

        var dbi = new DEV_BROADCAST_DEVICEINTERFACE
        {
            dbcc_size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(DEV_BROADCAST_DEVICEINTERFACE)),
            dbcc_devicetype = 0x00000005, // DBT_DEVTYP_DEVICEINTERFACE
            dbcc_classguid = new Guid("A5DCBF10-6530-11D2-901F-00C04FB951ED") // GUID_DEVINTERFACE_USB_DEVICE
        };
        
        IntPtr buffer = System.Runtime.InteropServices.Marshal.AllocHGlobal(dbi.dbcc_size);
        System.Runtime.InteropServices.Marshal.StructureToPtr(dbi, buffer, false);
        
        _notificationHandle = RegisterDeviceNotification(_source.Handle, buffer, 0); // DEVICE_NOTIFY_WINDOW_HANDLE
        
        System.Runtime.InteropServices.Marshal.FreeHGlobal(buffer);
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

    [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
    private static extern IntPtr RegisterDeviceNotification(IntPtr recipient, IntPtr notificationFilter, int flags);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern bool UnregisterDeviceNotification(IntPtr handle);

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    private struct DEV_BROADCAST_DEVICEINTERFACE
    {
        public int dbcc_size;
        public int dbcc_devicetype;
        public int dbcc_reserved;
        public Guid dbcc_classguid;
        public short dbcc_name;
    }

    private IntPtr _notificationHandle;

    public void Dispose()
    {
        if (_notificationHandle != IntPtr.Zero)
        {
            UnregisterDeviceNotification(_notificationHandle);
            _notificationHandle = IntPtr.Zero;
        }
        _source?.RemoveHook(WndProc);
        _source?.Dispose();
    }
}
