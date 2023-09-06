using WeatherApp.Services;

using WeatherApp.ViewModels;

namespace WeatherApp.Views;

public partial class CurrentWeatherView : ContentPage
{
    CurrentWeatherViewModel currentWeatherViewModel;
    public CurrentWeatherView(/*CurrentWeatherViewModel currentWeatherViewModel*/)
	{
		InitializeComponent();
        currentWeatherViewModel = new CurrentWeatherViewModel();
        BindingContext = currentWeatherViewModel;
	}
}

