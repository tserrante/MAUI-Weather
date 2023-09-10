using WeatherApp.Services;
using WeatherApp.Models.ForecastWeatherModel;
using System.Collections.ObjectModel;

namespace WeatherApp.ViewModels
{
    public class ForecastWeatherViewModel : BaseViewModel
    {

        private string zipCode;
        private string cityName;
        private string weatherDescription;
        private string weatherImagePath;

        private Root weatherForecastRoot;
        
        public ObservableCollection<Weather> WeatherForecast;
        
        public ForecastWeatherViewModel()
        {
            weatherForecastRoot = new Root();
            WeatherForecast = new ObservableCollection<Weather>();
            zipCode = string.Empty;
            cityName = "Please Enter Zip Code";
            weatherDescription = string.Empty;
            weatherImagePath = string.Empty;
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

        public string WeatherDescription
        {
            get => weatherDescription;
            set
            {
                SetProperty(ref weatherDescription, value);
            }
        }

        public string WeatherImagePath
        {
            get => weatherImagePath;
            set
            {
                SetProperty(ref weatherImagePath, value);
            }
        }

        private async void QueryApi()
        {
            if (int.TryParse(zipCode, out var convertedZipCode))
            {
                var weatherObject = await ApiService.GetWeatherForecast(convertedZipCode);
                if (weatherObject != null)
                {
                    CityName = weatherObject.city.name;
                    
                    foreach(var weatherReport in weatherObject.list)
                    {
                        foreach(var hourlyWeatherReport in weatherReport.weather)
                        {
                            WeatherForecast.Add(hourlyWeatherReport);
                        }
                    }

                    if (!FileService.isIconDownloaded(weatherIcon))
                    {
                        // current weather icon
                        byte[] imageByteArray = await ApiService.GetWeatherImageFromIconCode(weatherIcon);
                        WeatherImagePath = FileService.SaveIconToAppData(imageByteArray, weatherIcon);
                    }
                    else
                    {
                        WeatherImagePath = FileService.GetIconPath(weatherIcon);
                    }
                }
                else
                {
                    CityName = "Cannot locate city/region by zip code";
                    WeatherDescription = "Enter a correct zip code";
                }
            }
        }
    }

}
