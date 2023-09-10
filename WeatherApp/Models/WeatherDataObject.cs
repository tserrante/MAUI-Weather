using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Services;

namespace WeatherApp.Models
{
    public class WeatherDataObject
    {
        private string weatherDescription;
        private string weatherIcon;
        private string weatherImagePath;

        public WeatherDataObject()
        {
            weatherDescription = string.Empty;
            weatherIcon = string.Empty;
            weatherImagePath = string.Empty;
        }
        public WeatherDataObject(string weatherDescription, string weatherIcon)
        {
            this.weatherDescription = weatherDescription;
            this.weatherIcon = weatherIcon;
            Task.Run(async () => { WeatherImagePath = await FetchIcon(); });
        }

        public string WeatherDescription
        {
            get => weatherDescription;
            set => weatherDescription = value;
        }
        public string WeatherIcon
        {
            get => weatherIcon;
            set
            {
                weatherIcon = value;
            }
        }
        public string WeatherImagePath
        {
            get => weatherImagePath;
            set => weatherImagePath = value;
        }

        private async Task<string> FetchIcon()
        {
            if (!FileService.isIconDownloaded(weatherIcon))
            {
                byte[] imageByteArray = await ApiService.GetWeatherImageFromIconCode(weatherIcon);
                return FileService.SaveIconToAppData(imageByteArray, weatherIcon);
            }

            return FileService.GetIconPath(weatherIcon);
            
        }
            
    }
}
