using WeatherApp.ViewModels;

namespace WeatherApp.Pages;

public partial class WeatherForecastPage : ContentPage
{
	public WeatherForecastPage(WeatherForecastViewModel weatherForecastViewModel)
	{
		InitializeComponent();

		weatherForecastViewModel = new WeatherForecastViewModel();
		BindingContext = weatherForecastViewModel;
	}
}