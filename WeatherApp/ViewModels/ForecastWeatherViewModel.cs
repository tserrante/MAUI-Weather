using WeatherApp.Services;
using ForecastWeatherRoot = WeatherApp.Models.ForecastWeatherModel.Root;
using WeatherApp.Models;
using System.Collections.ObjectModel;

namespace WeatherApp.ViewModels
{
    public class ForecastWeatherViewModel : BaseViewModel
    {

        private string zipCode;
        private string cityName;

        public ObservableCollection<BaseWeatherObject> WeatherForecast { get; private set; } = new ObservableCollection<BaseWeatherObject>();
        
        public ForecastWeatherViewModel()
        {
            zipCode = string.Empty;
            cityName = string.Empty;
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
                var weatherForecastRoot = await ApiService.GetWeatherForecast(convertedZipCode);
                
                if (weatherForecastRoot != null)
                {
                    CityName = weatherForecastRoot.city.name;

                    WeatherForecast.Clear();

                    WeatherForecast = new ObservableCollection<BaseWeatherObject>();
                    foreach(var weatherReport in weatherForecastRoot.list)
                    {
                        foreach(var report in weatherReport.weather)
                        {
                            BaseWeatherObject weatherDataObject = new BaseWeatherObject(report.description, report.icon);

                            WeatherForecast.Add(weatherDataObject);
                        }
                    }
                    OnPropertyChanged(nameof(WeatherForecast));
                }
                else
                {
                    ZipCode = string.Empty;
                    CityName = string.Empty;
                }
            }
        }
    }

}
