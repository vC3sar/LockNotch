using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.UI.Notifications.Management;

namespace LockNotch.Services;

public sealed class NotificationService : INotificationService, IDisposable
{
    private UserNotificationListener? _listener;
    private bool _isListening;
    private SynchronizationContext? _ui;

    public event EventHandler<NotificationEventArgs>? NotificationReceived;

    public async Task InitializeAsync()
    {
        _ui = SynchronizationContext.Current;

        try 
        {
            _listener = UserNotificationListener.Current;
            var access = await _listener.RequestAccessAsync();

            if (access == UserNotificationListenerAccessStatus.Allowed)
            {
                _listener.NotificationChanged += OnNotificationChanged;
                _isListening = true;
            }
        } 
        catch { }
    }

    private void OnNotificationChanged(UserNotificationListener sender, UserNotificationChangedEventArgs args)
    {
        if (args.ChangeKind != UserNotificationChangedKind.Added) return;

        try
        {
            var notification = _listener?.GetNotification(args.UserNotificationId);
            if (notification == null) return;

            var appName = notification.AppInfo.DisplayInfo.DisplayName;
            var bindings = notification.Notification.Visual.GetBinding(KnownNotificationBindings.ToastGeneric);
            if (bindings == null) return;

            var texts = bindings.GetTextElements();
            if (texts.Count > 0)
            {
                string title = texts[0].Text;
                string body = texts.Count > 1 ? texts[1].Text : "";

                _ui?.Post(_ =>
                {
                    NotificationReceived?.Invoke(this, new NotificationEventArgs(appName, title, body));
                }, null);
            }
        }
        catch { }
    }

    public void Dispose()
    {
        if (_isListening && _listener != null)
        {
            _listener.NotificationChanged -= OnNotificationChanged;
        }
    }
}

public class NotificationEventArgs : EventArgs
{
    public string AppName { get; }
    public string Title { get; }
    public string Body { get; }

    public NotificationEventArgs(string appName, string title, string body)
    {
        AppName = appName;
        Title = title;
        Body = body;
    }
}
