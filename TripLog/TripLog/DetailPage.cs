using System;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TripLog
{
	public class DetailPage : ContentPage
	{
		public DetailPage(TripLogEntry tripLogEntry)
		{
			Title = "Entry Details";

			//Map with a Pin
			var map = new Map();
			var position = new Position(tripLogEntry.Latitute, tripLogEntry.Longitude);
			map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(0.5)));
			map.Pins.Add(
				new Pin
				{
					Type = PinType.Place,
					Label = tripLogEntry.Title,
					Position = position
				});

			var title = new Label { HorizontalOptions = LayoutOptions.Center, Text = tripLogEntry.Title };

			var date = new Label { HorizontalOptions = LayoutOptions.Center, Text = tripLogEntry.Date.ToString("M") };

			var rating = new Label { HorizontalOptions = LayoutOptions.Center, Text = $"{tripLogEntry.Rating} star rating" };

			var notes = new Label { HorizontalOptions = LayoutOptions.Center, Text = tripLogEntry.Notes };

			var details = new StackLayout
			{
				Padding = 10,
				Children = {
					title, date, rating, notes
				}
			};

			var detailsBg = new BoxView
			{
				BackgroundColor = Color.White,
				Opacity = 0.8
			};

			var mainLayout = new Grid
			{
				RowDefinitions = {
						new RowDefinition { Height = new GridLength(4, GridUnitType.Star)},
						new RowDefinition { Height = GridLength.Auto },
						new RowDefinition { Height = new GridLength(1, GridUnitType.Star)}
					}
			};
			mainLayout.Children.Add(map);
			mainLayout.Children.Add(detailsBg, 0, 1);
			mainLayout.Children.Add(details, 0, 1);
			Grid.SetRowSpan(map, 3);

			Content = mainLayout;
		}
	}
}

