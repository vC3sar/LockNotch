using System.Windows.Media;

namespace LockNotch.Models;

public sealed record NotificationRequest(
    string Id,
    string Glyph,
    string Text,
    System.Windows.Media.Brush Foreground,
    TimeSpan Duration,
    bool UseMarquee = true,
    string? MaterialIcon = null
);
