using System;
using Android.App;
using Android.OS;
using AndroidX.AppCompat.App;
using Android.Widget;
using Android.Content;
using System.Collections.Generic;
using NNChallenge.Constants;
using static AndroidX.RecyclerView.Widget.RecyclerView;
using Square.Picasso;
using Microsoft.Extensions.DependencyInjection;
using NNChallenge.Interfaces;
using NNChallenge.Service;

namespace NNChallenge.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            Startup.ConfigureServices(services =>
            {
                services.AddSingleton<IWeatherForecastService, WeatherForecastService>();
            });

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_location);

            Button buttonForecst = FindViewById<Button>(Resource.Id.button_forecast);
            buttonForecst.Click += OnForecastClick;

            Spinner spinnerLocations = FindViewById<Spinner>(Resource.Id.spinner_location);

            ArrayAdapter<String> adapter = new ArrayAdapter<String>(
                this,
                Android.Resource.Layout.SimpleSpinnerDropDownItem,
                LocationConstants.LOCATIONS
            );

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            spinnerLocations.Adapter = adapter;
        }

        private void OnForecastClick(object sender, EventArgs e)
        {
            Spinner spinnerLocations = FindViewById<Spinner>(Resource.Id.spinner_location);
            string selectedLocation = (string)spinnerLocations.SelectedItem;

            Intent forecastIntent = new Intent(this, typeof(ForecastActivity));

            forecastIntent.PutExtra("SelectedLocation", selectedLocation);

            this.StartActivity(forecastIntent);
        }
    }
}
