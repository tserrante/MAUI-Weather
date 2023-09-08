using WeatherApp.ViewModels;

namespace WeatherApp.Pages;

public partial class CurrentWeatherPage : ContentPage
{

    //CurrentWeatherViewModel currentWeatherViewModel;
    public CurrentWeatherPage(CurrentWeatherViewModel currentWeatherViewModel)
    {
        InitializeComponent();
        currentWeatherViewModel = new CurrentWeatherViewModel();
        BindingContext = currentWeatherViewModel;
    }
    
}