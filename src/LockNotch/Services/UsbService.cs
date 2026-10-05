using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Management;

namespace LockNotch.Services;

public sealed class UsbService : IUsbService, IDisposable
{
    public event EventHandler<UsbDeviceEventArgs>? UsbDeviceChanged;

    private const int WM_DEVICECHANGE = 0x0219;
    private const int DBT_DEVICEARRIVAL = 0x8000;
    private const int DBT_DEVICEREMOVECOMPLETE = 0x8004;

    private HwndSource? _source;
    private Dictionary<string, UsbDeviceInfo> _knownDevices = new();
    private DateTime _lastUsbCheck = DateTime.MinValue;

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
        
        _notificationHandle = RegisterDeviceNotification(_source.Handle, buffer, 0);
        
        System.Runtime.InteropServices.Marshal.FreeHGlobal(buffer);

        // Initial fetch silently
        Task.Run(() => ProcessUsbChangesAsync(true));
    }

    private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        if (msg == WM_DEVICECHANGE)
        {
            int eventType = wParam.ToInt32();
            if (eventType == DBT_DEVICEARRIVAL || eventType == DBT_DEVICEREMOVECOMPLETE)
            {
                if (lParam != IntPtr.Zero)
                {
                    var hdr = System.Runtime.InteropServices.Marshal.PtrToStructure<DEV_BROADCAST_HDR>(lParam);
                    if (hdr.dbch_devicetype == 0x05 || hdr.dbch_devicetype == 0x02) // DEVICEINTERFACE or VOLUME
                    {
                        Task.Run(() => ProcessUsbChangesAsync(false));
                    }
                }
            }
        }
        return IntPtr.Zero;
    }

    private async Task ProcessUsbChangesAsync(bool silent)
    {
        if ((DateTime.Now - _lastUsbCheck).TotalMilliseconds < 500) return;
        _lastUsbCheck = DateTime.Now;

        if (!silent) await Task.Delay(500); // Give Windows time to mount volumes

        var currentDevices = new Dictionary<string, UsbDeviceInfo>();

        try
        {
            // 1. Logical Disks (USB Drives)
            using var volSearcher = new ManagementObjectSearcher("Select DeviceID, VolumeName, Size, FreeSpace from Win32_LogicalDisk Where DriveType=2");
            foreach (var vol in volSearcher.Get())
            {
                string letter = vol["DeviceID"]?.ToString() ?? "";
                string label = vol["VolumeName"]?.ToString() ?? "";
                if (string.IsNullOrWhiteSpace(label)) label = "USB Drive";
                
                ulong size = 0, free = 0;
                ulong.TryParse(vol["Size"]?.ToString(), out size);
                ulong.TryParse(vol["FreeSpace"]?.ToString(), out free);

                string capStr = size > 0 ? $"{Math.Round(size / 1073741824.0, 1)} GB" : "";
                string freeStr = free > 0 ? $"{Math.Round(free / 1073741824.0, 1)} GB libres" : "";
                string detStr = size > 0 ? $"{letter} • {capStr}" : letter;

                currentDevices[letter] = new UsbDeviceInfo(letter, label, "USB Storage", detStr, "\xE8EB", true);
            }

            // 2. PnP Entities (Phones, HID, etc)
            using var pnpSearcher = new ManagementObjectSearcher("Select PNPDeviceID, Name, Description, Caption from Win32_PnPEntity Where PNPDeviceID Like 'USB\\\\%'");
            foreach (var mbo in pnpSearcher.Get())
            {
                string id = mbo["PNPDeviceID"]?.ToString() ?? "";
                string name = NormalizeUsbName(mbo["Name"]?.ToString() ?? mbo["Caption"]?.ToString() ?? "Unknown USB Device");
                string rawDesc = mbo["Description"]?.ToString() ?? "";
                string desc = NormalizeUsbName(rawDesc);

                // Skip hubs and mass storage (handled by LogicalDisk)
                if (name.Contains("Hub") || desc.Contains("Hub") || id.Contains("ROOT_HUB") || rawDesc.Contains("Hub")) continue;
                if (name.Contains("Mass Storage") || desc.Contains("Mass Storage") || name.Contains("Almacenamiento masivo") || desc.Contains("Almacenamiento masivo") || rawDesc.Contains("Mass Storage") || rawDesc.Contains("Almacenamiento masivo")) continue;

                string glyph = "\xE88E"; // Generic USB
                string type = "USB";

                if (desc.Contains("MTP") || desc.Contains("Portable") || name.Contains("Phone") || name.Contains("Mobile"))
                {
                    glyph = "\xE8EA"; // Phone
                    type = "MTP Device";
                }
                else if (desc.Contains("HID") || desc.Contains("Input") || desc.Contains("Keyboard") || desc.Contains("Mouse"))
                {
                    glyph = "\xE961"; // Generic HID
                    type = "HID Device";
                }
                else if (name.Contains("Xbox") || desc.Contains("Controller") || desc.Contains("Gamepad"))
                {
                    glyph = "\xE7FC"; // Generic Gamepad
                    type = "Controller";
                }

                // Extract VID and PID to group multiple interfaces of the same physical device
                string hardwareGroupId = id;
                int vidIdx = id.IndexOf("VID_", StringComparison.OrdinalIgnoreCase);
                int pidIdx = id.IndexOf("PID_", StringComparison.OrdinalIgnoreCase);
                if (vidIdx >= 0 && pidIdx >= 0)
                {
                    // e.g. "USB\VID_04E8&PID_6860&MI_00\5&1B2F..." -> "VID_04E8&PID_6860"
                    int endIdx = id.IndexOf('&', pidIdx + 4);
                    if (endIdx > 0)
                        hardwareGroupId = id.Substring(vidIdx, endIdx - vidIdx);
                    else
                        hardwareGroupId = id.Substring(vidIdx, Math.Min(17, id.Length - vidIdx));
                }

                // Avoid overwriting a good name like "Samsung Mobile" with a generic one like "MTP"
                if (currentDevices.TryGetValue(hardwareGroupId, out var existing))
                {
                    // Si ya existe en el grupo, nos quedamos con el nombre más largo/descriptivo
                    if (name.Length > existing.FriendlyName.Length && !name.Equals("MTP", StringComparison.OrdinalIgnoreCase))
                    {
                        currentDevices[hardwareGroupId] = new UsbDeviceInfo(hardwareGroupId, name, type, "USB", glyph, true);
                    }
                }
                else
                {
                    currentDevices[hardwareGroupId] = new UsbDeviceInfo(hardwareGroupId, name, type, "USB", glyph, true);
                }
            }

            // Compare and emit events
            if (!silent)
            {
                foreach (var known in _knownDevices.Values)
                {
                    if (!currentDevices.ContainsKey(known.DeviceId))
                    {
                        var copy = known with { IsConnected = false };
                        UsbDeviceChanged?.Invoke(this, new UsbDeviceEventArgs(copy));
                    }
                }

                foreach (var current in currentDevices.Values)
                {
                    if (!_knownDevices.ContainsKey(current.DeviceId))
                    {
                        UsbDeviceChanged?.Invoke(this, new UsbDeviceEventArgs(current));
                    }
                }
            }

            _knownDevices = currentDevices;
        }
        catch { }
    }

    private string NormalizeUsbName(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return raw;
        string current = raw.Trim();
        var wordsToRemove = new[] { " USB Device", " USB Composite Device", " Composite Device", " USB", " Device", " Generic", " HID", " MTP", " PTP", " Mass Storage" };
        bool changed = true;
        while(changed && current.Length > 0)
        {
            changed = false;
            foreach (var word in wordsToRemove)
            {
                if (current.EndsWith(word, StringComparison.OrdinalIgnoreCase))
                {
                    current = current.Substring(0, current.Length - word.Length).Trim();
                    changed = true;
                }
            }
        }
        return string.IsNullOrWhiteSpace(current) ? raw : current;
    }

    [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
    private static extern IntPtr RegisterDeviceNotification(IntPtr recipient, IntPtr notificationFilter, int flags);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern bool UnregisterDeviceNotification(IntPtr handle);

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    private struct DEV_BROADCAST_HDR
    {
        public int dbch_size;
        public int dbch_devicetype;
        public int dbch_reserved;
    }

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
