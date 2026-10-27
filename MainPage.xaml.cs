namespace FlyoutFooterRepro;

public partial class MainPage : ContentPage
{
    public MainPage() => InitializeComponent();

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Share the Shell's view-model so both pages and flyout use the same state.
        BindingContext = Shell.Current.BindingContext;
    }
}
