using CurrentWeatherRoot = WeatherApp.Models.CurrentWeatherModel.Root;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.ViewModels
{
    public class CurrentWeatherViewModel : BaseViewModel
    {
        private string zipCode;
        private string cityName;
        private string weatherDescription;
        private string iconPath;

        private BaseWeatherObject weatherObject;

        public CurrentWeatherViewModel()
        {
            weatherObject = new BaseWeatherObject(); //= new BaseWeatherObject();
            zipCode = string.Empty;
            cityName = string.Empty;
            weatherDescription = string.Empty;
            iconPath = string.Empty;
        }

        public string ZipCode
        {
            get => zipCode;
            set
            {
                SetProperty(ref zipCode, value);

                if(zipCode.Length == 5)
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
        public BaseWeatherObject WeatherObject
        {
            get => weatherObject;
            set => SetProperty(ref weatherObject, value);
        }
        public string WeatherDescription
        {
            //get => weatherDescription;
            get => weatherObject.WeatherDescription;
            set
            {
                SetProperty(ref weatherDescription, value);
                SetProperty(ref weatherObject.GetWeatherDescriptionByRef(), value);
            }
        }

        public string IconPath
        {
            //get => iconPath;
            get => weatherObject.IconPath;
            set
            {
                //SetProperty(ref iconPath, value);
                SetProperty(ref weatherObject.GetIconPathByRef(), value);
            }
        }


        private async void QueryApi()
        {
            if(int.TryParse(zipCode, out var convertedZipCode))
            {
                var currentWeather = await ApiService.GetCurrentWeather(convertedZipCode);

                if(currentWeather != null)
                {
                    CityName = currentWeather.name;
                    WeatherDescription = currentWeather.weather[0].description;
                    weatherObject.WeatherIcon = currentWeather.weather[0].icon;
                    IconPath = weatherObject.IconPath;
                }
                else
                {
                    CityName = string.Empty;
                    WeatherDescription = string.Empty;
                }
            }
        }
    }
}
