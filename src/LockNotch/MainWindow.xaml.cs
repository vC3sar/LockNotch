using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using LockNotch.Interop;
using LockNotch.ViewModels;

namespace LockNotch;

public partial class MainWindow : Window
{
    private readonly IslandViewModel _vm;
    private readonly Storyboard _expand;
    private readonly Storyboard _collapse;
    private readonly Storyboard _discSpin;
    private bool _discSpinStarted;
    private IntPtr _hwnd;

    public MainWindow(IslandViewModel viewModel)
    {
        InitializeComponent();

        _vm = viewModel;
        DataContext = viewModel;
        _expand = (Storyboard)FindResource("ExpandStoryboard");
        _collapse = (Storyboard)FindResource("CollapseStoryboard");

        var spinAnim = new DoubleAnimation(0, 360, TimeSpan.FromSeconds(4)) { RepeatBehavior = RepeatBehavior.Forever };
        Storyboard.SetTargetName(spinAnim, "DiscRotation");
        Storyboard.SetTargetProperty(spinAnim, new PropertyPath("Angle"));
        _discSpin = new Storyboard();
        _discSpin.Children.Add(spinAnim);

        _vm.PropertyChanged += OnViewModelPropertyChanged;
        SourceInitialized += OnSourceInitialized;
        DpiChanged += (_, _) => SchedulePosition();
    }

    private void OnSourceInitialized(object? sender, EventArgs e)
    {
        _hwnd = new WindowInteropHelper(this).Handle;
        WindowHelper.MakeNoActivateToolWindow(_hwnd);
        HwndSource.FromHwnd(_hwnd)?.AddHook(WndProc);
        WindowHelper.PositionTopCenterOnPrimary(_hwnd);
    }

    private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        switch (msg)
        {
            case NativeMethods.WM_MOUSEACTIVATE:
                handled = true;
                return new IntPtr(NativeMethods.MA_NOACTIVATE);

            case NativeMethods.WM_DISPLAYCHANGE:
                SchedulePosition();
                break;
        }
        return IntPtr.Zero;
    }

    // Se difiere para que WPF termine primero de aplicar el nuevo DPI/tamaño.
    private void SchedulePosition() =>
        Dispatcher.BeginInvoke(() => WindowHelper.PositionTopCenterOnPrimary(_hwnd), DispatcherPriority.Background);

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(IslandViewModel.IsExpanded):
                var sb = _vm.IsExpanded ? _expand : _collapse;
                sb.Begin(this, HandoffBehavior.SnapshotAndReplace);
                break;

            case nameof(IslandViewModel.IsPlaying):
                if (_vm.IsPlaying)
                {
                    if (!_discSpinStarted)
                    {
                        _discSpin.Begin(this, true);
                        _discSpinStarted = true;
                    }
                    else
                    {
                        _discSpin.Resume(this);
                    }
                }
                else
                {
                    if (_discSpinStarted)
                    {
                        _discSpin.Pause(this);
                    }
                }
                break;

            case nameof(IslandViewModel.IsHiddenForFullscreen):
                SetHidden(_vm.IsHiddenForFullscreen);
                break;
        }
    }

    private void SetHidden(bool hidden)
    {
        if (hidden)
        {
            var fadeOut = new DoubleAnimation(0, TimeSpan.FromMilliseconds(200));
            fadeOut.Completed += (_, _) =>
            {
                if (_vm.IsHiddenForFullscreen) Hide();
            };
            Root.BeginAnimation(OpacityProperty, fadeOut);
        }
        else
        {
            Show();
            WindowHelper.PositionTopCenterOnPrimary(_hwnd);
            Root.BeginAnimation(OpacityProperty, new DoubleAnimation(1, TimeSpan.FromMilliseconds(250)));
        }
    }

    private void Island_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e) => _vm.PointerEntered();

    private void Island_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e) => _vm.PointerExited();

    private void Island_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) => _vm.ExpandCommand.Execute(null);

    private void Exit_Click(object sender, RoutedEventArgs e) => System.Windows.Application.Current.Shutdown();
}
