using NNChallenge.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NNChallenge.Models
{
    public class HourWeatherForecastVO : IHourWeatherForecastVO
    {
        public DateTime Date { get; set; }
        public float TeperatureCelcius { get; set; }
        public float TeperatureFahrenheit { get; set; }
        public string ForecastPitureURL { get; set; }
    }

    public class WeatherForecastVO : IWeatherForcastVO
    {
        public string City { get; set; }
        public IHourWeatherForecastVO[] HourForecast { get; set; }
    }
}
