using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NNChallenge.Models;

namespace NNChallenge.Interfaces
{
    public interface IWeatherForecastService
    {
        WeatherForecastVO ParseApiResponse(string responseBody);
        Task<WeatherForecastVO> GetForecastData(string selectedLocation);
    }
}
