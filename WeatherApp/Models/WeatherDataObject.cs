using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Services;

namespace WeatherApp.Models
{
    public class WeatherDataObject : IComparable
    {
        private string weatherDescription;
        private string weatherIcon;
        private string weatherImagePath;

        public WeatherDataObject()
        {
            WeatherDescription = string.Empty;
            WeatherIcon = string.Empty;
            WeatherImagePath = string.Empty;
        }
        public WeatherDataObject(string weatherDescription, string weatherIcon)
        {
            this.weatherDescription = weatherDescription;
            this.weatherIcon = weatherIcon;
        }

        public string WeatherDescription
        {
            get => weatherDescription;
            set => weatherDescription = value;
        }
        public string WeatherIcon
        {
            get => weatherIcon;
            set => weatherIcon = value;
        }
        public string WeatherImagePath
        {
            get => weatherImagePath;
            set => weatherImagePath = value;
        }

        public int CompareTo(object obj)
        {
            return weatherDescription.CompareTo(obj);
        }
    }
}
