using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Services;

namespace WeatherApp.Models
{
    public class BaseWeatherObject : IComparable
    {
        private string weatherDescription;
        private string weatherIcon;
        private string iconPath;

        public BaseWeatherObject()
        {
            this.weatherDescription = string.Empty;
            this.weatherIcon = string.Empty;
            this.iconPath = string.Empty;
        }
        public BaseWeatherObject(string weatherDescription, string weatherIcon)
        {
            this.weatherDescription = weatherDescription;
            this.weatherIcon = weatherIcon;
            //this.iconPath = string.Empty;
            //Task.Run(InitializeIconPath).Wait();
            iconPath = Task.Run(InitializeIconPath).Result;
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
                //Task.Run(InitializeIconPath).Wait();
                iconPath = Task.Run(InitializeIconPath).Result;
            }
        }
        public string IconPath
        {
            get => iconPath;
            set => iconPath = value;
        }
        public ref string GetWeatherIconByRef()
        {
            return ref this.weatherIcon;
        }
        public ref string GetIconPathByRef()
        { 
            return ref this.iconPath;
        }
        public ref string GetWeatherDescriptionByRef()
        {
            return ref this.weatherDescription;
        }

        private async Task<string> InitializeIconPath()
        {
            if(!weatherIcon.Equals(string.Empty))
            {
                if (!FileService.IsIconDownloaded(weatherIcon))
                {
                    byte[] imageByteArray = await ApiService.GetWeatherImageFromIconCode(weatherIcon);
                    /*this.iconPath = */
                    return FileService.SaveIconToAppData(imageByteArray, weatherIcon);
                }
                else
                {
                    /*this.iconPath = */return FileService.GetIconPath(weatherIcon);
                }
            }
            return string.Empty;
        }

        public int CompareTo(object obj)
        {
            return weatherDescription.CompareTo(obj);
        }
    }
}
