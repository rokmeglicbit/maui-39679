using FlyoutFooterRepro.ViewModels;

namespace FlyoutFooterRepro;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        // Flyout templates inherit the Shell's BindingContext.
        BindingContext = new MainViewModel();
    }
}
