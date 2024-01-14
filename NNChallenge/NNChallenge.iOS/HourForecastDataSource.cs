using System;
using Foundation;
using System.Collections.Generic;
using UIKit;
using NNChallenge.Models;
using NNChallenge.Interfaces;

namespace NNChallenge.iOS
{
    public class HourForecastDataSource : UITableViewSource
    {
        private IHourWeatherForecastVO[] hourForecastData;
        private const string CellIdentifier = "HourForecastCell";

        public HourForecastDataSource(IHourWeatherForecastVO[] data)
        {
            hourForecastData = data ?? Array.Empty<IHourWeatherForecastVO>();
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return hourForecastData.Length;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier) ?? new UITableViewCell(UITableViewCellStyle.Subtitle, CellIdentifier);

            var hourForecast = hourForecastData[indexPath.Row];

            cell.TextLabel.Text = $"{hourForecast.TeperatureCelcius}°C / {hourForecast.TeperatureFahrenheit}\u00b0F";
            cell.DetailTextLabel.Text = hourForecast.Date.ToString("MMM dd, yyyy HH:mm");

            LoadImageAsync($"https:{hourForecast.ForecastPitureURL}", cell.ImageView);

            return cell; 
        }
        
        private async void LoadImageAsync(string imageUrl, UIImageView imageView)
        {
            try
            {
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    using (var url = new NSUrl(imageUrl))
                    using (var data = NSData.FromUrl(url))
                    {
                        var image = UIImage.LoadFromData(data);
                        imageView.Image = image;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading image: {ex.Message}");
            }
        }
    }
}

