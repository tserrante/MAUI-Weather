using Microsoft.Extensions.Logging;
using WeatherApp.ViewModels;
using WeatherApp.Views;
namespace WeatherApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		//AddView<CurrentWeatherView, CurrentWeatherViewModel>(builder.Services, "currentweather");
		return builder.Build();
	}

    //private static IServiceCollection AddView<TView, TViewModel>(
    //    IServiceCollection services,
    //    string route)
    //    where TView : View
    //    where TViewModel : BaseViewModel
    //{
    //    services
    //        .AddTransient(typeof(TView))
    //        .AddTransient(typeof(TViewModel));

    //    Routing.RegisterRoute(route, typeof(TView));

    //    return services;
    //}
}
