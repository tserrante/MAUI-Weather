using WeatherApp.Services;
using WeatherApp.Models.ForecastWeatherModel;
using ForecastData = WeatherApp.Models.WeatherDataObject;
using System.Collections.ObjectModel;

namespace WeatherApp.ViewModels
{
    public class ForecastWeatherViewModel : BaseViewModel
    {

        private string zipCode;
        private string cityName;

        private Root weatherForecastRoot;

        public ObservableCollection<ForecastData> WeatherForecast { get; private set; } = new ObservableCollection<ForecastData>();
        
        public ForecastWeatherViewModel()
        {
            weatherForecastRoot = new Root();
            WeatherForecast = new ObservableCollection<ForecastData>();
            zipCode = string.Empty;
            cityName = "Please Enter Zip Code";
        }

        public string ZipCode
        {
            get => zipCode;
            set
            {
                SetProperty(ref zipCode, value);
                if (zipCode.Length == 5)
                {
                    QueryApi();
                }
            }
        }
        public string CityName
        {
            get => cityName;
            set
            {
                SetProperty(ref cityName, value);
            }
        }
        private async void QueryApi()
        {
            if (int.TryParse(zipCode, out var convertedZipCode))
            {
                weatherForecastRoot = await ApiService.GetWeatherForecast(convertedZipCode);
                if (weatherForecastRoot != null)
                {
                    CityName = weatherForecastRoot.city.name;
                    
                    foreach(var weatherReport in weatherForecastRoot.list)
                    {
                        foreach(var hourlyWeatherReport in weatherReport.weather)
                        {
                            WeatherForecast.Add(new ForecastData(hourlyWeatherReport.description, hourlyWeatherReport.icon));
                        }
                    }
                }
                else
                {
                    CityName = "Cannot locate city/region by zip code";
                }
            }
        }
    }

}
