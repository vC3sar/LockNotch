namespace LockNotch.Services;

public interface IUsbService
{
    event EventHandler<bool>? UsbDeviceChanged;
    void Start();
}
