using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Services;
using WeatherApp.Models;
using WeatherApp.Views;

namespace WeatherApp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private string zipCode;
        private string cityName;

        private CurrentWeatherViewModel currentWeatherViewModel;
        private ForecastWeatherViewModel forecastWeatherViewModel;

        public MainPageViewModel() 
        { 
            zipCode = string.Empty;
            cityName = string.Empty;
        }

        public string ZipCode
        { 
            get { return zipCode; } 
            set 
            {
                SetProperty(ref zipCode, value);

                if(zipCode.Length == 5)
                {
                    //QueryApi();
                }
            } 
        }
        public string CityName
        {
            get { return cityName; }
            set 
            { 
                SetProperty(ref cityName, value);
            }
        }
        
    }
}
