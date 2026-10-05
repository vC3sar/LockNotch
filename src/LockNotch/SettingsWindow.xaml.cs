using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LockNotch.Services;

namespace LockNotch;

public partial class SettingsWindow : Window
{
    private readonly SettingsService _settings;

    public SettingsWindow(SettingsService settings)
    {
        InitializeComponent();
        _settings = settings;

        // Cargar Apps
        TxtApp1.Text = _settings.Current.AppLauncher1;
        TxtApp2.Text = _settings.Current.AppLauncher2;
        TxtApp3.Text = _settings.Current.AppLauncher3;
        TxtApp4.Text = _settings.Current.AppLauncher4;
        TxtApp5.Text = _settings.Current.AppLauncher5;

        // Cargar General
        ChkStartWindows.IsChecked = _settings.Current.StartWithWindows;
        ChkAllowFullscreen.IsChecked = !_settings.Current.HideInFullscreen;

        // Cargar Clima y Hardware
        TxtLat.Text = _settings.Current.WeatherLatitude.ToString(CultureInfo.InvariantCulture);
        TxtLon.Text = _settings.Current.WeatherLongitude.ToString(CultureInfo.InvariantCulture);
        ChkFahrenheit.IsChecked = _settings.Current.WeatherUseFahrenheit;
        TxtRefresh.Text = _settings.Current.HardwareRefreshIntervalSeconds.ToString();

        // Cargar Tema
        if (_settings.Current.Theme == "Light") RbThemeLight.IsChecked = true;
        else RbThemeDark.IsChecked = true;
        
        // Cargar Fuente
        foreach (ComboBoxItem item in CmbFont.Items)
        {
            if (item.Tag.ToString() == _settings.Current.FontFamily)
            {
                CmbFont.SelectedItem = item;
                break;
            }
        }
        if (CmbFont.SelectedItem == null && CmbFont.Items.Count > 0)
            CmbFont.SelectedIndex = 0;
    }

    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            DragMove();
        }
    }

    private void NavMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (TabGeneral == null || TabApps == null || TabWeather == null || TabAbout == null || TabAppearance == null) return;

        TabGeneral.Visibility = Visibility.Collapsed;
        TabApps.Visibility = Visibility.Collapsed;
        TabWeather.Visibility = Visibility.Collapsed;
        TabAppearance.Visibility = Visibility.Collapsed;
        TabAbout.Visibility = Visibility.Collapsed;

        switch (NavMenu.SelectedIndex)
        {
            case 0: TabGeneral.Visibility = Visibility.Visible; break;
            case 1: TabApps.Visibility = Visibility.Visible; break;
            case 2: TabWeather.Visibility = Visibility.Visible; break;
            case 3: TabAppearance.Visibility = Visibility.Visible; break;
            case 4: TabAbout.Visibility = Visibility.Visible; break;
        }
    }

    private async void AutoDetectLocation_Click(object sender, RoutedEventArgs e)
    {
        var button = (System.Windows.Controls.Button)sender;
        string originalContent = button.Content.ToString() ?? "";
        try
        {
            button.IsEnabled = false;
            button.Content = "Detectando...";

            using var http = new System.Net.Http.HttpClient();
            var response = await http.GetStringAsync("http://ip-api.com/json/");
            using var doc = System.Text.Json.JsonDocument.Parse(response);
            
            double lat = doc.RootElement.GetProperty("lat").GetDouble();
            double lon = doc.RootElement.GetProperty("lon").GetDouble();

            TxtLat.Text = lat.ToString(CultureInfo.InvariantCulture);
            TxtLon.Text = lon.ToString(CultureInfo.InvariantCulture);

            button.Content = "¡Detectado!";
            await System.Threading.Tasks.Task.Delay(1500);
        }
        catch
        {
            button.Content = "Error";
            await System.Threading.Tasks.Task.Delay(1500);
        }
        finally
        {
            button.Content = "Auto-detectar ubicación (IP)";
            button.IsEnabled = true;
        }
    }

    private void RepoLink_Click(object sender, MouseButtonEventArgs e)
    {
        try
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://github.com/vC3sar/LockNotch",
                UseShellExecute = true
            });
        }
        catch { }
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        // Guardar Apps
        _settings.Current.AppLauncher1 = TxtApp1.Text;
        _settings.Current.AppLauncher2 = TxtApp2.Text;
        _settings.Current.AppLauncher3 = TxtApp3.Text;
        _settings.Current.AppLauncher4 = TxtApp4.Text;
        _settings.Current.AppLauncher5 = TxtApp5.Text;

        // Guardar General
        _settings.Current.StartWithWindows = ChkStartWindows.IsChecked ?? false;
        _settings.Current.HideInFullscreen = !(ChkAllowFullscreen.IsChecked ?? false);

        // Guardar Clima y Hardware
        if (double.TryParse(TxtLat.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double lat))
            _settings.Current.WeatherLatitude = lat;
        
        if (double.TryParse(TxtLon.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double lon))
            _settings.Current.WeatherLongitude = lon;

        _settings.Current.WeatherUseFahrenheit = ChkFahrenheit.IsChecked ?? false;

        if (int.TryParse(TxtRefresh.Text, out int refresh) && refresh > 0)
            _settings.Current.HardwareRefreshIntervalSeconds = refresh;

        // Guardar Tema y Fuente
        _settings.Current.Theme = RbThemeLight.IsChecked == true ? "Light" : "Dark";
        if (CmbFont.SelectedItem is ComboBoxItem selectedFont)
        {
            _settings.Current.FontFamily = selectedFont.Tag.ToString() ?? "Normal";
        }

        _settings.Save();

        // Aplicar registro de Windows Startup
        ApplyStartupRegistry(_settings.Current.StartWithWindows);

        // Aplicar tema y fuente dinámicamente
        LockNotch.Helpers.AppearanceManager.Apply(_settings.Current.Theme, _settings.Current.FontFamily);

        Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void ApplyStartupRegistry(bool enable)
    {
        try
        {
            using var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            if (key != null)
            {
                if (enable)
                {
                    string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName ?? "";
                    if (!string.IsNullOrEmpty(exePath))
                    {
                        key.SetValue("LockNotch", $"\"{exePath}\"");
                    }
                }
                else
                {
                    key.DeleteValue("LockNotch", false);
                }
            }
        }
        catch { }
    }
}
