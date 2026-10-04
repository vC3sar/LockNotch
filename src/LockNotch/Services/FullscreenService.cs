using System.Text;
using static LockNotch.Interop.NativeMethods;

namespace LockNotch.Services;

public sealed class FullscreenService : IFullscreenService, IDisposable
{
    // Ventanas del shell que ocupan todo el monitor pero no son "pantalla completa".
    private static readonly HashSet<string> IgnoredClasses = new(StringComparer.OrdinalIgnoreCase)
    {
        "Progman", "WorkerW", "Shell_TrayWnd", "Shell_SecondaryTrayWnd"
    };

    private System.Threading.Timer? _timer;
    private int _checking;

    public event EventHandler<bool>? FullscreenChanged;

    public bool IsFullscreen { get; private set; }

    public void Start()
    {
        _timer ??= new System.Threading.Timer(_ => Check(), null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
    }

    private void Check()
    {
        if (Interlocked.Exchange(ref _checking, 1) == 1) return;
        try
        {
            bool fs;
            try { fs = DetectFullscreen(); }
            catch { fs = false; }

            if (fs == IsFullscreen) return;
            IsFullscreen = fs;
            FullscreenChanged?.Invoke(this, fs);
        }
        finally
        {
            Volatile.Write(ref _checking, 0);
        }
    }

    private static bool DetectFullscreen()
    {
        if (SHQueryUserNotificationState(out var state) == 0 &&
            state is QUERY_USER_NOTIFICATION_STATE.QUNS_RUNNING_D3D_FULL_SCREEN
                  or QUERY_USER_NOTIFICATION_STATE.QUNS_PRESENTATION_MODE)
        {
            return true;
        }

        var hwnd = GetForegroundWindow();
        if (hwnd == IntPtr.Zero) return false;

        var sb = new StringBuilder(256);
        if (GetClassName(hwnd, sb, sb.Capacity) > 0 && IgnoredClasses.Contains(sb.ToString()))
            return false;

        var monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONULL);
        if (monitor == IntPtr.Zero || monitor != GetPrimaryMonitor()) return false;
        if (!TryGetMonitorRect(monitor, out var mon)) return false;
        
        // Obtener el área de cliente real de la ventana (sin bordes ni barra de título)
        if (!GetClientRect(hwnd, out var clientRect)) return false;

        // Convertir las coordenadas de cliente a coordenadas de pantalla
        var topLeft = new POINT { X = clientRect.Left, Y = clientRect.Top };
        var bottomRight = new POINT { X = clientRect.Right, Y = clientRect.Bottom };
        
        ClientToScreen(hwnd, ref topLeft);
        ClientToScreen(hwnd, ref bottomRight);

        // Una ventana es fullscreen real (como F11 o un juego) si su área cliente cubre TODO el monitor
        return topLeft.X <= mon.Left && topLeft.Y <= mon.Top &&
               bottomRight.X >= mon.Right && bottomRight.Y >= mon.Bottom;
    }

    public void Dispose() => _timer?.Dispose();
}
