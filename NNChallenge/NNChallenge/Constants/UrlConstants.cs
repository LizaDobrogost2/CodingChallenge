using System;
using System.Collections.Generic;
using System.Text;

namespace NNChallenge.Constants
{
    public static class UrlConstants
    {
        public static string GetUrl(string selectedLocation)
        {
            return $"https://api.weatherapi.com/v1/forecast.json?key=898147f83a734b7dbaa95705211612&q={selectedLocation}&days=3&aqi=no&alerts=no";
        }
    }
}
