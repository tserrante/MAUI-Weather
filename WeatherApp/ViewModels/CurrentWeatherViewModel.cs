﻿using WeatherApp.Models.CurrentWeatherModel;
using WeatherApp.Services;
using static System.Net.WebRequestMethods;

namespace WeatherApp.ViewModels
{
    public class CurrentWeatherViewModel : BaseViewModel
    {
        private string zipCode;
        private string cityName;
        private string weatherDescription;
        private string weatherIcon;
        private string weatherImagePath;

        private Root currentWeather;

        public CurrentWeatherViewModel()
        {
            currentWeather = new Root();
            zipCode = string.Empty;
            cityName = "Please Enter Zip Code";
            weatherDescription = string.Empty;
            weatherIcon = string.Empty;
            weatherImagePath = string.Empty;
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
            if(int.TryParse(zipCode, out var convertedZipCode))
            {
                currentWeather = await ApiService.GetCurrentWeather(convertedZipCode);

                if(currentWeather != null)
                {
                    CityName = currentWeather.name;
                    WeatherDescription = currentWeather.weather[0].description;
                    weatherIcon = currentWeather.weather[0].icon;

                    if(!FileService.isIconDownloaded(weatherIcon)) 
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
