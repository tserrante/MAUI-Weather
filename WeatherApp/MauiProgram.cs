using Microsoft.Extensions.Logging;
using WeatherApp.Pages;
using WeatherApp.ViewModels;
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
		builder.Services.AddTransient<AppShellViewModel>();
		builder.Services.AddTransient<AppShell>();
		
		AddPage<CurrentWeatherPage, CurrentWeatherViewModel>(builder.Services, "currentweather");
		AddPage<WeatherForecastPage, WeatherForecastViewModel>(builder.Services, "weatherforecast");

		return builder.Build();
	}

	private static IServiceCollection AddPage<TPage, TViewModel>(
		IServiceCollection services,
		string route)
		where TPage : Page
		where TViewModel : BaseViewModel
	{
		services
			.AddTransient(typeof(TPage))
			.AddTransient(typeof(TViewModel));

		Routing.RegisterRoute(route, typeof(TPage));

		return services;
	}
}
