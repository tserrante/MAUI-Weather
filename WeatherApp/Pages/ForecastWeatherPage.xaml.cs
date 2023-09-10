using WeatherApp.ViewModels;

namespace WeatherApp.Pages;

public partial class ForecastWeatherPage : ContentPage
{
	public ForecastWeatherPage(ForecastWeatherViewModel forecastWeatherViewModel)
	{
		InitializeComponent();

        forecastWeatherViewModel = new ForecastWeatherViewModel();
		BindingContext = forecastWeatherViewModel;
	}
}