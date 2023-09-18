using WeatherApp.ViewModels;

namespace WeatherApp.Views;

public partial class ForecastWeatherView : VerticalStackLayout
{
	ForecastWeatherViewModel forecastWeatherViewModel;
	public ForecastWeatherView()
	{
		InitializeComponent();

        forecastWeatherViewModel = new ForecastWeatherViewModel();
		BindingContext = forecastWeatherViewModel;
	}
}