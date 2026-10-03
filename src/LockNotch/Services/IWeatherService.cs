namespace LockNotch.Services;

public interface IWeatherService
{
    event EventHandler? WeatherChanged;
    
    string Temperature { get; }
    string ConditionGlyph { get; }

    void Start();
}
