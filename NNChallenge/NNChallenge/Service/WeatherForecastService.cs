using Newtonsoft.Json;
using NNChallenge.Constants;
using NNChallenge.Interfaces;
using NNChallenge.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NNChallenge.Service
{
    public class WeatherForecastService : IWeatherForecastService
    {
        public WeatherForecastVO ParseApiResponse(string responseBody)
        {
            var response = JsonConvert.DeserializeObject<DTO>(responseBody);

            WeatherForecastVO weatherForecast = new WeatherForecastVO
            {
                City = response.location.name,
                HourForecast = new IHourWeatherForecastVO[72],
            };

            List<HourWeatherForecastVO> hours = new List<HourWeatherForecastVO>();


            foreach (var hour in response.forecast.forecastday)
            {
                foreach (var h in hour.hour)
                {
                    hours.Add(new HourWeatherForecastVO
                    {
                        Date = h.time,
                        TeperatureCelcius = h.temp_c,
                        TeperatureFahrenheit = h.temp_f,
                        ForecastPitureURL = h.condition.icon
                    });
                }
            }

            weatherForecast.HourForecast = hours.ToArray();
            return weatherForecast;
        }
        public async Task<WeatherForecastVO> GetForecastData(string selectedLocation)
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = UrlConstants.GetUrl(selectedLocation);

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    WeatherForecastVO weatherData = ParseApiResponse(responseBody);

                    return weatherData;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return null;
                }
            }
        }
    }
}
