
using Android.App;
using Android.OS;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Java.Lang;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NNChallenge.Constants;
using NNChallenge.Interfaces;
using NNChallenge.Models;
using Org.Json;
using Square.Picasso;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NNChallenge.Droid
{
    [Activity(Label = "ForecastActivity")]
    public class ForecastActivity : Activity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_forecast);

            string selectedLocation = Intent.GetStringExtra("SelectedLocation");

            var weatherForecastService = Startup.GetService<IWeatherForecastService>();
            WeatherForecastVO weatherData = await weatherForecastService.GetForecastData(selectedLocation);

            TextView textCity = FindViewById<TextView>(Resource.Id.text_city);

            ListView listViewHourForecast = FindViewById<ListView>(Resource.Id.listViewHourForecast);

            textCity.Text = weatherData.City;

            var adapter = new HourForecastAdapter(this, weatherData.HourForecast);
            listViewHourForecast.Adapter = adapter;
        }
    }
}
