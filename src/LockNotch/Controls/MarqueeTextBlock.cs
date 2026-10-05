using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Data;

namespace LockNotch.Controls;

public class MarqueeTextBlock : System.Windows.Controls.UserControl
{
    private readonly TextBlock _textBlock;
    private readonly Canvas _canvas;

    public MarqueeTextBlock()
    {
        ClipToBounds = true;
        _textBlock = new TextBlock 
        { 
            VerticalAlignment = VerticalAlignment.Center 
        };
        _canvas = new Canvas { ClipToBounds = true };
        _canvas.Children.Add(_textBlock);
        Content = _canvas;

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

    private void UpdateAnimation()
    {
        if (_textBlock == null || _canvas == null || ActualWidth == 0) return;

        _textBlock.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));
        double textWidth = _textBlock.DesiredSize.Width;
        double containerWidth = ActualWidth;

        if (textWidth > containerWidth)
        {
            double diff = textWidth - containerWidth + 10;
            double seconds = Math.Max(1.5, diff / 25.0);

            var anim = new DoubleAnimation
            {
                From = 0,
                To = -diff,
                Duration = TimeSpan.FromSeconds(seconds),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
            _textBlock.BeginAnimation(Canvas.LeftProperty, anim);
        }
        else
        {
            _textBlock.BeginAnimation(Canvas.LeftProperty, null);
            Canvas.SetLeft(_textBlock, 0);
        }
    }
}
