using System;
using System.Threading.Tasks;
using NNChallenge.Interfaces;
using NNChallenge.Models;
using UIKit;

namespace NNChallenge.iOS
{
    public partial class ForecastViewController : UIViewController
    {
        private UILabel textCity;
        private UITableView tableViewHourForecast;
        public string SelectedCity { get; set; }

        public ForecastViewController() : base("ForecastViewController", null)
        {
        }

        public override void LoadView()
        {
            base.LoadView();

            textCity = new UILabel
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Tag = 1,
            };

            tableViewHourForecast = new UITableView
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Tag = 2,
            };

            View.AddSubviews(textCity, tableViewHourForecast);

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                textCity.TopAnchor.ConstraintEqualTo(View.TopAnchor, 100),
                textCity.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor),

                tableViewHourForecast.TopAnchor.ConstraintEqualTo(textCity.BottomAnchor, 20),
                tableViewHourForecast.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 20),
                tableViewHourForecast.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, -20),
                tableViewHourForecast.BottomAnchor.ConstraintEqualTo(View.BottomAnchor, -20),
            });
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Forecast";

            Task.Run(async () =>
            {
                var weatherForecastService = Startup.GetService<IWeatherForecastService>();
                string selectedLocation = SelectedCity;

                // Assuming that you have a similar IWeatherForecastService and WeatherForecastVO class
                WeatherForecastVO weatherData =
                    await weatherForecastService.GetForecastData(selectedLocation).ConfigureAwait(false);

                // Use InvokeOnMainThread to update the UI on the main thread
                InvokeOnMainThread(() =>
                {
                    textCity.Text = weatherData.City;

                    var dataSource = new HourForecastDataSource(weatherData.HourForecast);
                    tableViewHourForecast.Source = dataSource;
                    tableViewHourForecast.ReloadData();
                });
            });
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

