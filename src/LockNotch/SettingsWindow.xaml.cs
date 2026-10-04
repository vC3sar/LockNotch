using System.Windows;
using LockNotch.Services;

namespace LockNotch;

public partial class SettingsWindow : Window
{
    private readonly SettingsService _settings;

    public SettingsWindow(SettingsService settings)
    {
        InitializeComponent();
        _settings = settings;

        TxtApp1.Text = _settings.Current.AppLauncher1;
        TxtApp2.Text = _settings.Current.AppLauncher2;
        TxtApp3.Text = _settings.Current.AppLauncher3;
        TxtApp4.Text = _settings.Current.AppLauncher4;
        TxtApp5.Text = _settings.Current.AppLauncher5;
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        _settings.Current.AppLauncher1 = TxtApp1.Text;
        _settings.Current.AppLauncher2 = TxtApp2.Text;
        _settings.Current.AppLauncher3 = TxtApp3.Text;
        _settings.Current.AppLauncher4 = TxtApp4.Text;
        _settings.Current.AppLauncher5 = TxtApp5.Text;

        _settings.Save();
        Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
