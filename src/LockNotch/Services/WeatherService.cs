using System.Net.Http;
using System.Text.Json;
using System.Threading;

namespace LockNotch.Services;

public sealed class WeatherService : IWeatherService, IDisposable
{
    private System.Threading.Timer? _timer;
    private readonly HttpClient _http = new();
    private readonly SettingsService _settings;
    
    public WeatherService(SettingsService settings)
    {
        _settings = settings;
        _http.DefaultRequestHeaders.Add("User-Agent", "LockNotch/1.0");
    }

    private string _temperature = "--°c";
    private string _conditionGlyph = "\u2600\uFE0F"; // Soleado por defecto

    public event EventHandler? WeatherChanged;

    public string Temperature => _temperature;
    public string ConditionGlyph => _conditionGlyph;

    public void Start()
    {
        // Actualiza cada 30 minutos
        _timer ??= new System.Threading.Timer(_ => _ = FetchWeatherAsync(), null, TimeSpan.Zero, TimeSpan.FromMinutes(30));
    }

    private async Task FetchWeatherAsync()
    {
        try
        {
            var lat = _settings.Current.WeatherLatitude;
            var lon = _settings.Current.WeatherLongitude;
            var unit = _settings.Current.WeatherUseFahrenheit ? "&temperature_unit=fahrenheit" : "";

            var url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true{unit}";
            var response = await _http.GetStringAsync(url);
            using var doc = JsonDocument.Parse(response);
            
            var current = doc.RootElement.GetProperty("current_weather");
            double temp = current.GetProperty("temperature").GetDouble();
            int code = current.GetProperty("weathercode").GetInt32();

            _temperature = $"{Math.Round(temp)}°";
            _conditionGlyph = GetGlyphForCode(code);
            
            WeatherChanged?.Invoke(this, EventArgs.Empty);
        }
        catch
        {
            // Falla silenciosa, mantiene los últimos valores
        }
    }

    private static string GetGlyphForCode(int code)
    {
        // WMO Weather interpretation codes mapped to Emojis (using Unicode escapes to avoid compilation corruption)
        return code switch
        {
            0 => "\u2600\uFE0F", // Despejado (Sunny)
            1 => "\uD83C\uDF24\uFE0F", // Poco nublado
            2 or 3 => "\u2601\uFE0F", // Nubes (Partly cloudy / overcast)
            45 or 48 => "\uD83C\uDF2B\uFE0F", // Niebla (Fog)
            51 or 53 or 55 or 56 or 57 => "\uD83C\uDF26\uFE0F", // Llovizna (Drizzle)
            61 or 63 or 65 or 66 or 67 => "\uD83C\uDF27\uFE0F", // Lluvia (Rain)
            71 or 73 or 75 or 77 => "\u2744\uFE0F", // Nieve (Snow)
            80 or 81 or 82 => "\uD83C\uDF27\uFE0F", // Chubascos (Showers)
            85 or 86 => "\uD83C\uDF28\uFE0F", // Chubascos de nieve (Snow showers)
            95 or 96 or 99 => "\u26C8\uFE0F", // Tormenta (Thunderstorm)
            _ => "\u2600\uFE0F"
        };
    }

    public void Dispose()
    {
        _timer?.Dispose();
        _http.Dispose();
    }
}
