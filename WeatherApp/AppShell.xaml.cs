using WeatherApp.ViewModels;

namespace WeatherApp;

public partial class AppShell : Shell
{
	public AppShell(AppShellViewModel appShellViewModel)
	{
		InitializeComponent();

        appShellViewModel = new AppShellViewModel();
        BindingContext = appShellViewModel;
    }
}
