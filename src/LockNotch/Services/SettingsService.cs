using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LockNotch.Services;

public class AppSettings
{
    public string AppLauncher1 { get; set; } = "ms-settings:";
    public string AppLauncher2 { get; set; } = "explorer.exe";
    public string AppLauncher3 { get; set; } = "calc.exe";
    public string AppLauncher4 { get; set; } = "notepad.exe";
    public string AppLauncher5 { get; set; } = "mspaint.exe";

    public bool StartWithWindows { get; set; } = false;
    public bool HideInFullscreen { get; set; } = true;
    public double WeatherLatitude { get; set; } = 40.4165; // Default Madrid
    public double WeatherLongitude { get; set; } = -3.7026;
    public bool WeatherUseFahrenheit { get; set; } = false;
    public int HardwareRefreshIntervalSeconds { get; set; } = 2;
    public string Theme { get; set; } = "Dark";
    public string FontFamily { get; set; } = "Normal";
}

public class SettingsService
{
    private static readonly string ConfigPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "LockNotch",
        "settings.json");

    public AppSettings Current { get; private set; }

    public SettingsService()
    {
        Current = new AppSettings();
        Load();
    }

    public void Load()
    {
        try
        {
            if (File.Exists(ConfigPath))
            {
                var json = File.ReadAllText(ConfigPath);
                var loaded = JsonSerializer.Deserialize<AppSettings>(json);
                if (loaded != null)
                {
                    Current = loaded;
                }
            }
        }
        catch { }
    }

    public void Save()
    {
        try
        {
            var dir = Path.GetDirectoryName(ConfigPath);
            if (dir != null && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var json = JsonSerializer.Serialize(Current, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ConfigPath, json);
        }
        catch { }
    }
}
