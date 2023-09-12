using WeatherApp.Services;
using WeatherApp.Models.ForecastWeatherModel;
using ForecastData = WeatherApp.Models.WeatherDataObject;
using System.Collections.ObjectModel;
using WeatherApp.Pages;

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
                    
                    List<ForecastData> preparationList = new List<ForecastData>();

                    foreach(var weatherReport in weatherForecastRoot.list)
                    {
                        foreach(var report in weatherReport.weather)
                        {
                            ForecastData weatherDataObject = new ForecastData(report.description, report.icon);

                            if (!FileService.isIconDownloaded(weatherDataObject.WeatherIcon))
                            {
                                byte[] imageByteArray = await ApiService.GetWeatherImageFromIconCode(weatherDataObject.WeatherIcon);
                                weatherDataObject.WeatherImagePath = FileService.SaveIconToAppData(imageByteArray, weatherDataObject.WeatherIcon);
                            }
                            else
                            {
                                weatherDataObject.WeatherImagePath = FileService.GetIconPath(weatherDataObject.WeatherIcon);
                            }

                            preparationList.Add(weatherDataObject);
                        }
                    }
                    WeatherForecast = new ObservableCollection<ForecastData>(preparationList);
                    OnPropertyChanged(nameof(WeatherForecast));
                }
                else
                {
                    CityName = "Cannot locate city/region by zip code";
                }
            }
        }
    }

}
