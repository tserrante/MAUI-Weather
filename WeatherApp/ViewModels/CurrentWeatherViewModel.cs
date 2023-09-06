using WeatherApp.Services;
using static System.Net.WebRequestMethods;

namespace WeatherApp.ViewModels
{
    public class CurrentWeatherViewModel : BaseViewModel
    {
        private string zipCode;
        private string cityName;
        private string weatherDescription;
        private string weatherImagePath;

        ApiService apiService;

        public CurrentWeatherViewModel()
        {
            apiService = new ApiService();
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
                QueryApi();
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
            get=> weatherImagePath; 
            set
            {
                SetProperty(ref weatherImagePath, value);
            }
        }

        private async void QueryApi()
        {
            if(zipCode.Length == 5)
            {
                if(int.TryParse(zipCode, out var convertedZipCode))
                {
                    var weatherObject = await apiService.GetCurrentWeather(convertedZipCode);
                    if(weatherObject != null)
                    {
                        CityName = weatherObject.name;
                        WeatherDescription = weatherObject.weather[0].description;
                        string weatherIcon = weatherObject.weather[0].icon;

                        // current weather icon
                        byte[] imageByteArray = await apiService.GetWeatherImageFromIconCode(weatherIcon);
                        WeatherImagePath = FileService.SaveIconToAppData(imageByteArray, weatherIcon);
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
}
