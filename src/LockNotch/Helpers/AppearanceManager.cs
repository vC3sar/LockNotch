using System.Windows;
using System.Windows.Media;

namespace LockNotch.Helpers;

/// <summary>
/// Punto único para aplicar tema (paleta) y tipografía en tiempo de ejecución.
/// Todo el XAML consume los recursos mediante DynamicResource.
/// </summary>
public static class AppearanceManager
{
    private const string DefaultFont = "Segoe UI Variable Display, Segoe UI";
    private const string FontsPack = "pack://application:,,,/LockNotch;component/Resources/Fonts/";

    public static void Apply(string? theme, string? font)
    {
        ApplyTheme(theme);
        ApplyFont(font);
    }

    public static void ApplyTheme(string? theme)
    {
        string name = string.Equals(theme, "Light", StringComparison.OrdinalIgnoreCase) ? "Light" : "Dark";
        var dict = new ResourceDictionary
        {
            Source = new Uri($"pack://application:,,,/LockNotch;component/Resources/Themes/{name}.xaml")
        };

        var merged = System.Windows.Application.Current.Resources.MergedDictionaries;
        for (int i = merged.Count - 1; i >= 0; i--)
        {
            var src = merged[i].Source?.OriginalString;
            if (src != null && src.Contains("Themes/", StringComparison.OrdinalIgnoreCase))
                merged.RemoveAt(i);
        }
        merged.Insert(0, dict);
    }

    public static void ApplyFont(string? fontTag)
    {
        var font = new System.Windows.Media.FontFamily(DefaultFont);
        try
        {
            var uri = new Uri(FontsPack);
            if (string.Equals(fontTag, "Nothing", StringComparison.OrdinalIgnoreCase))
                font = new System.Windows.Media.FontFamily(uri, "./#Nothing Font (5x7)");
            else if (string.Equals(fontTag, "SpaceGrotesk", StringComparison.OrdinalIgnoreCase))
                font = new System.Windows.Media.FontFamily(uri, "./#Space Grotesk");
            else if (string.Equals(fontTag, "Manrope", StringComparison.OrdinalIgnoreCase))
                font = new System.Windows.Media.FontFamily(uri, "./#Manrope");
            else if (string.Equals(fontTag, "Audiowide", StringComparison.OrdinalIgnoreCase))
                font = new System.Windows.Media.FontFamily(uri, "./#Audiowide");
            else if (string.Equals(fontTag, "Oxanium", StringComparison.OrdinalIgnoreCase))
                font = new System.Windows.Media.FontFamily(uri, "./#Oxanium");
        }
        catch { }

        System.Windows.Application.Current.Resources["UiFont"] = font;
    }
}
