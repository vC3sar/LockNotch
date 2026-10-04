using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using LockNotch.Interop;
using LockNotch.Models;
using LockNotch.ViewModels;

namespace LockNotch;

public partial class MainWindow : Window
{
    private readonly IslandViewModel _vm;
    private readonly Storyboard _expand;
    private readonly Storyboard _collapse;
    private readonly Storyboard _notify;
    private readonly Storyboard _discSpin;
    private readonly Storyboard _visualizerAnim;
    private bool _discSpinStarted;
    private IntPtr _hwnd;

    public MainWindow(IslandViewModel viewModel)
    {
        InitializeComponent();

        _vm = viewModel;
        DataContext = viewModel;
        _expand = (Storyboard)FindResource("ExpandStoryboard");
        _collapse = (Storyboard)FindResource("CollapseStoryboard");
        _notify = (Storyboard)FindResource("NotifyStoryboard");

        var spinAnim = new DoubleAnimation(0, 360, TimeSpan.FromSeconds(6.7)) { RepeatBehavior = RepeatBehavior.Forever };
        Storyboard.SetTargetName(spinAnim, "DiscRotation");
        Storyboard.SetTargetProperty(spinAnim, new PropertyPath("Angle"));
        _discSpin = new Storyboard();
        _discSpin.Children.Add(spinAnim);

        // Animación del ecualizador
        _visualizerAnim = new Storyboard();
        var rnd = new Random();
        for (int i = 1; i <= 4; i++)
        {
            var anim = new DoubleAnimation
            {
                From = 4,
                To = rnd.Next(10, 16),
                Duration = TimeSpan.FromMilliseconds(rnd.Next(250, 450)),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever,
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
            };
            Storyboard.SetTargetName(anim, $"Bar{i}");
            Storyboard.SetTargetProperty(anim, new PropertyPath("Height"));
            _visualizerAnim.Children.Add(anim);
        }

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

    private int _scrollAccumulator;
    private const int ScrollThreshold = 120; // Aumentado para mayor precisión y evitar brincos
    private DateTime _lastPageTurn = DateTime.MinValue;
    private readonly TimeSpan _pageTurnCooldown = TimeSpan.FromMilliseconds(350);

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

            case NativeMethods.WM_MOUSEHWHEEL:
                if (_vm.IsExpanded && Island.IsMouseOver)
                {
                    int delta = (short)((ulong)wParam >> 16);
                    _scrollAccumulator += delta;

                    if (Math.Abs(_scrollAccumulator) >= ScrollThreshold)
                    {
                        if (DateTime.Now - _lastPageTurn >= _pageTurnCooldown)
                        {
                            if (_scrollAccumulator > 0) _vm.NextPage();
                            else _vm.PreviousPage();
                            _lastPageTurn = DateTime.Now;
                        }
                        _scrollAccumulator = 0;
                    }
                    handled = true;
                }
                else
                {
                    _scrollAccumulator = 0;
                }
                break;

            case NativeMethods.WM_MOUSEWHEEL:
                if (_vm.IsExpanded && Island.IsMouseOver)
                {
                    int delta = (short)((ulong)wParam >> 16);
                    _scrollAccumulator += delta;

                    if (Math.Abs(_scrollAccumulator) >= ScrollThreshold)
                    {
                        if (DateTime.Now - _lastPageTurn >= _pageTurnCooldown)
                        {
                            if (_scrollAccumulator < 0) _vm.NextPage();
                            else _vm.PreviousPage();
                            _lastPageTurn = DateTime.Now;
                        }
                        _scrollAccumulator = 0;
                    }
                    handled = true;
                }
                else
                {
                    _scrollAccumulator = 0;
                }
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
            case nameof(IslandViewModel.CurrentPageIndex):
                UpdateCarousel();
                break;

            case nameof(IslandViewModel.State):
                Storyboard sb = _vm.State switch
                {
                    IslandState.Expanded => _expand,
                    IslandState.Notification => _notify,
                    _ => _collapse
                };
                sb.Begin(this, HandoffBehavior.SnapshotAndReplace);
                if (_vm.State == IslandState.Expanded) UpdateCarousel();
                break;

            case nameof(IslandViewModel.IsPlaying):
                if (_vm.IsPlaying)
                {
                    if (!_discSpinStarted)
                    {
                        _discSpin.Begin(this, true);
                        _visualizerAnim.Begin(this, true);
                        _discSpinStarted = true;
                    }
                    else
                    {
                        _discSpin.Resume(this);
                        _visualizerAnim.Resume(this);
                    }
                }
                else
                {
                    if (_discSpinStarted)
                    {
                        _discSpin.Pause(this);
                        _visualizerAnim.Pause(this);
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

    private void UpdateCarousel()
    {
        AnimatePanel(PanelMedia, 0);
        AnimatePanel(PanelAppLauncher, 1);
        AnimatePanel(PanelControlCenter, 2);
        AnimatePanel(PanelHardware, 3);

        // Ocultar disco de vinilo suavemente en otras páginas
        double targetOpacity = _vm.CurrentPageIndex == 0 ? 1.0 : 0.0;
        DiscHost.BeginAnimation(OpacityProperty, new DoubleAnimation(targetOpacity, TimeSpan.FromMilliseconds(250)));

        // Animate the paginator dot (each dot is 18px wide)
        var thumbTransform = (TranslateTransform)PaginatorThumb.RenderTransform;
        var animThumb = new DoubleAnimation(_vm.CurrentPageIndex * 18.0, TimeSpan.FromMilliseconds(300))
        {
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };
        thumbTransform.BeginAnimation(TranslateTransform.XProperty, animThumb);

        if (_vm.State == IslandState.Expanded)
        {
            UpdateExpandedSize();
        }
    }

    private void UpdateExpandedSize()
    {
        double targetWidth = _vm.CurrentPageIndex == 0 ? 300.0 : 420.0;
        double targetHeight = _vm.CurrentPageIndex == 0 ? 250.0 : 170.0;
        double targetRadius = _vm.CurrentPageIndex == 0 ? 48.0 : 36.0;
        double targetDiscY = _vm.CurrentPageIndex == 0 ? -156.0 : -260.0; // Disc de 260px: en pág 0 oculta 156px, muestra 104px (40%).

        var widthAnim = new DoubleAnimation(targetWidth, TimeSpan.FromMilliseconds(450)) { EasingFunction = new BackEase { EasingMode = EasingMode.EaseOut, Amplitude = 0.28 } };
        var heightAnim = new DoubleAnimation(targetHeight, TimeSpan.FromMilliseconds(450)) { EasingFunction = new BackEase { EasingMode = EasingMode.EaseOut, Amplitude = 0.28 } };
        var radiusAnim = new DoubleAnimation(targetRadius, TimeSpan.FromMilliseconds(450)) { EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut } };
        var marginAnim = new ThicknessAnimation(new Thickness(0, targetDiscY, 0, 0), TimeSpan.FromMilliseconds(450)) { EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut } };

        Island.BeginAnimation(WidthProperty, widthAnim);
        Island.BeginAnimation(HeightProperty, heightAnim);
        IslandBorder.BeginAnimation(Helpers.NotchHelper.BottomRadiusProperty, radiusAnim);
        DiscHost.BeginAnimation(MarginProperty, marginAnim);
    }

    private void AnimatePanel(UIElement panel, int panelIndex)
    {
        if (panel == null) return;
        var transform = (TranslateTransform)panel.RenderTransform;
        // Si panelIndex == CurrentPageIndex, X = 0
        // Si panelIndex > CurrentPageIndex, X > 0 (A la derecha, esperando entrar)
        // Si panelIndex < CurrentPageIndex, X < 0 (A la izquierda, ya pasó)
        double targetX = (panelIndex - _vm.CurrentPageIndex) * 420.0;

        var anim = new DoubleAnimation(targetX, TimeSpan.FromMilliseconds(350))
        {
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };
        transform.BeginAnimation(TranslateTransform.XProperty, anim);
    }
}
