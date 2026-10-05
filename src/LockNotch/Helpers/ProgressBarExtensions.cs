using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

namespace LockNotch.Helpers;

public static class ProgressBarExtensions
{
    public static readonly DependencyProperty SmoothValueProperty =
        DependencyProperty.RegisterAttached(
            "SmoothValue",
            typeof(double),
            typeof(ProgressBarExtensions),
            new PropertyMetadata(0.0, OnSmoothValueChanged));

    public static double GetSmoothValue(DependencyObject obj) => (double)obj.GetValue(SmoothValueProperty);
    public static void SetSmoothValue(DependencyObject obj, double value) => obj.SetValue(SmoothValueProperty, value);

    private static void OnSmoothValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is RangeBase progressBar && e.NewValue != null)
        {
            double targetValue = Convert.ToDouble(e.NewValue);
            var anim = new DoubleAnimation(targetValue, TimeSpan.FromMilliseconds(300))
            {
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            progressBar.BeginAnimation(RangeBase.ValueProperty, anim);
        }
    }
}
