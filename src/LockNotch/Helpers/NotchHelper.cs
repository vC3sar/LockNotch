using System.Windows;
using System.Windows.Controls;

namespace LockNotch.Helpers;

/// <summary>
/// CornerRadius no es animable con DoubleAnimation; esta propiedad adjunta (double)
/// sí lo es y se traduce a un CornerRadius con solo las esquinas inferiores redondeadas.
/// </summary>
public static class NotchHelper
{
    public static readonly DependencyProperty BottomRadiusProperty =
        DependencyProperty.RegisterAttached(
            "BottomRadius",
            typeof(double),
            typeof(NotchHelper),
            new PropertyMetadata(0d, OnBottomRadiusChanged));

    public static double GetBottomRadius(DependencyObject obj) => (double)obj.GetValue(BottomRadiusProperty);
    public static void SetBottomRadius(DependencyObject obj, double value) => obj.SetValue(BottomRadiusProperty, value);

    private static void OnBottomRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Border border)
        {
            double r = Math.Max(0, (double)e.NewValue);
            border.CornerRadius = new CornerRadius(0, 0, r, r);
        }
    }
}
