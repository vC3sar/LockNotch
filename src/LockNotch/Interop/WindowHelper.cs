using static LockNotch.Interop.NativeMethods;

namespace LockNotch.Interop;

internal static class WindowHelper
{
    /// <summary>
    /// Evita que la ventana tome el foco al hacer clic y la oculta de Alt+Tab.
    /// </summary>
    public static void MakeNoActivateToolWindow(IntPtr hwnd)
    {
        long ex = GetWindowLongPtr(hwnd, GWL_EXSTYLE).ToInt64();
        ex |= WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW;
        ex &= ~WS_EX_APPWINDOW;
        SetWindowLongPtr(hwnd, GWL_EXSTYLE, new IntPtr(ex));
    }

    /// <summary>
    /// Centra la ventana en el borde superior del monitor principal usando píxeles físicos,
    /// lo que la hace independiente de la escala DPI del sistema.
    /// </summary>
    public static void PositionTopCenterOnPrimary(IntPtr hwnd)
    {
        if (hwnd == IntPtr.Zero) return;
        if (!TryGetMonitorRect(GetPrimaryMonitor(), out var mon)) return;
        if (!GetWindowRect(hwnd, out var win)) return;

        int x = mon.Left + (mon.Width - win.Width) / 2;
        int y = mon.Top;
        SetWindowPos(hwnd, HWND_TOPMOST, x, y, 0, 0, SWP_NOSIZE | SWP_NOACTIVATE);
    }
}
