using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FlyoutFooterRepro.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    bool _isBannerVisible;      // starts hidden => footer starts SHORT
    string _status = "Footer banner: hidden";

    public MainViewModel()
    {
        ToggleBannerCommand = new Command(ToggleBanner);
        ToggleBannerDelayedCommand = new Command(async () =>
        {
            Status = "Toggling in 4 s - open the flyout now...";
            await Task.Delay(4000);
            ToggleBanner();
        });
    }

    public bool IsBannerVisible
    {
        get => _isBannerVisible;
        set { if (_isBannerVisible != value) { _isBannerVisible = value; OnPropertyChanged(); } }
    }

    public string Status
    {
        get => _status;
        set { if (_status != value) { _status = value; OnPropertyChanged(); } }
    }

    public ICommand ToggleBannerCommand { get; }
    public ICommand ToggleBannerDelayedCommand { get; }

    void ToggleBanner()
    {
        IsBannerVisible = !IsBannerVisible;
        Status = $"Footer banner: {(IsBannerVisible ? "VISIBLE (footer taller)" : "hidden (footer shorter)")}";
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
