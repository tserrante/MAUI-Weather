using Newtonsoft.Json;
using WeatherApp.Models;
using System.Drawing;
using static WeatherApp.API.API;
namespace WeatherApp.Services
{
    public class ApiService
    {
        
        static readonly HttpClient client = new HttpClient();

        public async Task<Root> GetCurrentWeather(int zipCode)
        {
            Root weatherObject = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(string.Format("https://api.openweathermap.org/data/2.5/weather?zip={0}&appid={1}", zipCode, API_KEY));
                response.EnsureSuccessStatusCode();
                weatherObject = JsonConvert.DeserializeObject<Root>(await response.Content.ReadAsStringAsync());
            }
            catch (HttpRequestException e)
            {
                await Console.Out.WriteLineAsync("Exception in ApiService.GetCurrentWeather");
                await Console.Out.WriteLineAsync(e.Message);
            }

            return weatherObject;
        }

        public async Task<byte[]> GetWeatherImageFromIconCode(string iconCode)
        {
            byte[] response = await client.GetByteArrayAsync(string.Format("http://openweathermap.org/img/wn/{0}@2x.png", iconCode));
            // add validation here
            return response;
        }
    }
}
