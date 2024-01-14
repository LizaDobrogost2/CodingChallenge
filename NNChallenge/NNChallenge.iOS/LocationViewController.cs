using System;
using Microsoft.Extensions.DependencyInjection;
using NNChallenge.Constants;
using NNChallenge.Interfaces;
using NNChallenge.iOS.ViewModel;
using NNChallenge.Service;
using UIKit;

namespace NNChallenge.iOS
{
    public partial class LocationViewController : UIViewController
    {
        public LocationViewController() : base("LocationViewController", null)
        {
        }       

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            Startup.ConfigureServices(services =>
            {
                services.AddSingleton<IWeatherForecastService, WeatherForecastService>();
            });
            
            Title = "Location";
            _submitButton.TitleLabel.Text = "Submit";
            _contentLabel.Text = "Select your location.";
            _submitButton.TouchUpInside += SubmitButtonTouchUpInside;

            _picker.Model = new LocationPickerModel(LocationConstants.LOCATIONS);
        }

        private void SubmitButtonTouchUpInside(object sender, EventArgs e)
        {
            var selectedCity = LocationConstants.LOCATIONS[_picker.SelectedRowInComponent(0)];
        
            var forecastView = new ForecastViewController();
        
            // Set the selected city before pushing the view controller
            forecastView.SelectedCity = selectedCity;
        
            this.NavigationController.PushViewController(forecastView, true);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

