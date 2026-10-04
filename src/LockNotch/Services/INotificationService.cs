using System;
using System.Threading.Tasks;

namespace LockNotch.Services;

public interface INotificationService
{
    event EventHandler<NotificationEventArgs>? NotificationReceived;
    Task InitializeAsync();
}
