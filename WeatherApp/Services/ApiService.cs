using Newtonsoft.Json;
using CurrentWeatherRoot = WeatherApp.Models.CurrentWeatherModel.Root;
using ForecastWeatherRoot = WeatherApp.Models.ForecastWeatherModel.Root;

using System.Drawing;
using static WeatherApp.API.API;

namespace WeatherApp.Services
{
    public static class ApiService
    {
        static readonly HttpClient client = new HttpClient();
        public static async Task<CurrentWeatherRoot> GetCurrentWeather(int zipCode)
        {
            CurrentWeatherRoot weatherObject = null; 
            try
            {
                HttpResponseMessage response = await client.GetAsync(string.Format("https://api.openweathermap.org/data/2.5/weather?zip={0}&appid={1}", zipCode, API_KEY));
                response.EnsureSuccessStatusCode();
                weatherObject = JsonConvert.DeserializeObject <CurrentWeatherRoot> (await response.Content.ReadAsStringAsync());
            }
            catch (HttpRequestException e)
            {
                await Console.Out.WriteLineAsync("Exception in ApiService.GetCurrentWeather: ");
                await Console.Out.WriteLineAsync(e.Message);
            }

            return weatherObject;
        }

        public static async Task<ForecastWeatherRoot> GetWeatherForecast(int zipCode)
        {
            ForecastWeatherRoot weatherObject = null;

            try
            {
                HttpResponseMessage response = await client.GetAsync(string.Format("https://api.openweathermap.org/data/2.5/forecast?zip={0}&appid={1}", zipCode, API_KEY));
                response.EnsureSuccessStatusCode();
                weatherObject = JsonConvert.DeserializeObject<ForecastWeatherRoot>(await response.Content.ReadAsStringAsync());
            }
            catch (HttpRequestException e)
            {
                await Console.Out.WriteLineAsync("Exception in ApiService.GetCurrentWeather: ");
                await Console.Out.WriteLineAsync(e.Message);
            }

            return weatherObject;
        }

        public static async Task<byte[]> GetWeatherImageFromIconCode(string iconCode)
        {
            byte[] response = await client.GetByteArrayAsync(string.Format("http://openweathermap.org/img/wn/{0}@2x.png", iconCode));
            // add validation here
            return response;
        }
    }
}
