using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NNChallenge.Interfaces;
using NNChallenge.Models;
using Square.Picasso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Xamarin.Essentials;

namespace NNChallenge.Droid
{
    public class HourForecastAdapter : BaseAdapter<HourWeatherForecastVO>
    {
        private readonly Context context;
        private readonly IHourWeatherForecastVO[] hourForecasts;

        public HourForecastAdapter(Context context, IHourWeatherForecastVO[] hourForecasts)
        {
            this.context = context;
            this.hourForecasts = hourForecasts;
        }

        public override HourWeatherForecastVO this[int position] => (HourWeatherForecastVO)hourForecasts[position];

        public override int Count => hourForecasts.Length;

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView ?? LayoutInflater.FromContext(context).Inflate(Resource.Layout.hour_forecast_item, null);

            TextView textDate = view.FindViewById<TextView>(Resource.Id.textDate);
            TextView textTemperature = view.FindViewById<TextView>(Resource.Id.textTemperature);
            ImageView imageViewForecast = view.FindViewById<ImageView>(Resource.Id.imageViewForecast);

            HourWeatherForecastVO currentForecast = (HourWeatherForecastVO)hourForecasts[position];

            textDate.Text = currentForecast.Date.ToString("HH:mm MMM dd,yyyy");
            textTemperature.Text = $"{currentForecast.TeperatureCelcius}°C / {currentForecast.TeperatureFahrenheit}°F";

            Picasso.Get()
                .Load("https://"+currentForecast.ForecastPitureURL)
                .Fit()
                .CenterCrop()
                .NoFade()
                .Into(imageViewForecast);

            return view;
        }
    }
}