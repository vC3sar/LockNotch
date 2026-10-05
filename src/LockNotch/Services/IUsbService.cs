namespace LockNotch.Services;

public interface IUsbService
{
    event EventHandler<UsbDeviceEventArgs>? UsbDeviceChanged;
    void Start();
}

public class UsbDeviceEventArgs : EventArgs
{
    public UsbDeviceInfo DeviceInfo { get; }
    public UsbDeviceEventArgs(UsbDeviceInfo info) => DeviceInfo = info;
}

public record UsbDeviceInfo(
    string DeviceId,
    string FriendlyName,
    string Type,
    string Details,
    string Glyph,
    bool IsConnected
);
