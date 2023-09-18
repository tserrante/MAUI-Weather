using WeatherApp.ViewModels;

namespace WeatherApp.Views;

public partial class CurrentWeatherView : VerticalStackLayout
{
	CurrentWeatherViewModel currentWeatherViewModel;
	public CurrentWeatherView()
	{
		InitializeComponent();
		currentWeatherViewModel = new CurrentWeatherViewModel();
		BindingContext = currentWeatherViewModel;
	}
}