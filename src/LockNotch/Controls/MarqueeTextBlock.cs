using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Data;
using System.Windows.Media;

namespace LockNotch.Controls;

public class MarqueeTextBlock : System.Windows.Controls.UserControl
{
    private readonly TextBlock _textBlock;
    private readonly ScrollViewer _scroll;

    public MarqueeTextBlock()
    {
        ClipToBounds = true;
        _textBlock = new TextBlock 
        { 
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = System.Windows.HorizontalAlignment.Left
        };
        _textBlock.RenderTransform = new TranslateTransform();

        _scroll = new ScrollViewer 
        { 
            HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden,
            VerticalScrollBarVisibility = ScrollBarVisibility.Disabled,
            IsHitTestVisible = false,
            ClipToBounds = true
        };
        _scroll.Content = _textBlock;
        Content = _scroll;

        _textBlock.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding("Text") { Source = this });
        _textBlock.SetBinding(TextBlock.ForegroundProperty, new System.Windows.Data.Binding("Foreground") { Source = this });
        _textBlock.SetBinding(TextBlock.FontSizeProperty, new System.Windows.Data.Binding("FontSize") { Source = this });
        _textBlock.SetBinding(TextBlock.FontFamilyProperty, new System.Windows.Data.Binding("FontFamily") { Source = this });
        _textBlock.SetBinding(TextBlock.FontWeightProperty, new System.Windows.Data.Binding("FontWeight") { Source = this });

        Loaded += (s, e) => UpdateAnimation();
        SizeChanged += (s, e) => UpdateAnimation();
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(MarqueeTextBlock), new PropertyMetadata("", (d, e) => ((MarqueeTextBlock)d).UpdateAnimation()));

    public bool IsMarqueeEnabled
    {
        get => (bool)GetValue(IsMarqueeEnabledProperty);
        set => SetValue(IsMarqueeEnabledProperty, value);
    }

    public static readonly DependencyProperty IsMarqueeEnabledProperty =
        DependencyProperty.Register("IsMarqueeEnabled", typeof(bool), typeof(MarqueeTextBlock), new PropertyMetadata(true, (d, e) => ((MarqueeTextBlock)d).UpdateAnimation()));

    private void UpdateAnimation()
    {
        if (_textBlock == null || _scroll == null || ActualWidth == 0) return;

        _textBlock.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));
        double textWidth = _textBlock.DesiredSize.Width;
        double containerWidth = ActualWidth;

        var transform = _textBlock.RenderTransform as TranslateTransform;
        if (transform == null) return;

        if (textWidth > containerWidth && IsMarqueeEnabled)
        {
            double diff = textWidth - containerWidth + 10;
            double seconds = Math.Max(2.0, diff / 25.0); // Velocidad fluida

            var anim = new DoubleAnimation
            {
                From = 0,
                To = -diff,
                Duration = TimeSpan.FromSeconds(seconds),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever,
                BeginTime = TimeSpan.FromSeconds(1) // Pausa inicial antes de deslizar
            };
            transform.BeginAnimation(TranslateTransform.XProperty, anim);
        }
        else
        {
            transform.BeginAnimation(TranslateTransform.XProperty, null);
            transform.X = 0;
        }
    }
}
