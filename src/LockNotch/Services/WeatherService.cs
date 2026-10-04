using System.Net.Http;
using System.Text.Json;
using System.Threading;

namespace LockNotch.Services;

public sealed class WeatherService : IWeatherService, IDisposable
{
    private System.Threading.Timer? _timer;
    private readonly HttpClient _http = new();
    
    public WeatherService()
    {
        _http.DefaultRequestHeaders.Add("User-Agent", "LockNotch/1.0");
    }

    private double? _lat;
    private double? _lon;

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
            if (_lat is null || _lon is null)
            {
                try
                {
                    // Intentar obtener ubicación precisa usando la API nativa de Windows 10/11
                    var accessStatus = await Windows.Devices.Geolocation.Geolocator.RequestAccessAsync();
                    if (accessStatus == Windows.Devices.Geolocation.GeolocationAccessStatus.Allowed)
                    {
                        var geolocator = new Windows.Devices.Geolocation.Geolocator { DesiredAccuracyInMeters = 5000 };
                        var pos = await geolocator.GetGeopositionAsync(TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(10));
                        _lat = pos.Coordinate.Point.Position.Latitude;
                        _lon = pos.Coordinate.Point.Position.Longitude;
                    }
                    else
                    {
                        throw new UnauthorizedAccessException("Geolocator access denied.");
                    }
                }
                catch
                {
                    // Fallback a geolocalización por IP si el usuario deniega el permiso GPS o falla
                    try
                    {
                        var ipResponse = await _http.GetStringAsync("http://ip-api.com/json/");
                        using var ipDoc = JsonDocument.Parse(ipResponse);
                        _lat = ipDoc.RootElement.GetProperty("lat").GetDouble();
                        _lon = ipDoc.RootElement.GetProperty("lon").GetDouble();
                    }
                    catch
                    {
                        _lat = 40.4165; // Fallback extremo: Madrid
                        _lon = -3.7026;
                    }
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
