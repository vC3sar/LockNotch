using System.Net.Http;
using System.Text.Json;
using System.Threading;

namespace LockNotch.Services;

public sealed class WeatherService : IWeatherService, IDisposable
{
    private System.Threading.Timer? _timer;
    private readonly HttpClient _http = new();
    
    private double? _lat;
    private double? _lon;

    private string _temperature = "--°c";
    private string _conditionGlyph = "\xE706"; // Soleado por defecto

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
            if (_lat is null || _lon is null)
            {
                try
                {
                    var ipResponse = await _http.GetStringAsync("http://ip-api.com/json/");
                    using var ipDoc = JsonDocument.Parse(ipResponse);
                    _lat = ipDoc.RootElement.GetProperty("lat").GetDouble();
                    _lon = ipDoc.RootElement.GetProperty("lon").GetDouble();
                }
                catch
                {
                    _lat = 40.4165; // Fallback Madrid
                    _lon = -3.7026;
                }
            }

            var url = $"https://api.open-meteo.com/v1/forecast?latitude={_lat}&longitude={_lon}&current_weather=true";
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
        // WMO Weather interpretation codes using Segoe Fluent Icons
        return code switch
        {
            0 => "\xE706", // Despejado (Sunny)
            1 => "\xE706", // Poco nublado
            2 or 3 => "\xE753", // Nubes (Partly cloudy / overcast)
            45 or 48 => "\xE753", // Niebla (Fog)
            51 or 53 or 55 or 56 or 57 => "\xE738", // Llovizna (Drizzle)
            61 or 63 or 65 or 66 or 67 => "\xE738", // Lluvia (Rain)
            71 or 73 or 75 or 77 => "\xE9C9", // Nieve (Snow)
            80 or 81 or 82 => "\xE738", // Chubascos (Showers)
            85 or 86 => "\xE9C9", // Chubascos de nieve (Snow showers)
            95 or 96 or 99 => "\xE73A", // Tormenta (Thunderstorm)
            _ => "\xE706"
        };
    }

    public void Dispose()
    {
        _timer?.Dispose();
        _http.Dispose();
    }
}
