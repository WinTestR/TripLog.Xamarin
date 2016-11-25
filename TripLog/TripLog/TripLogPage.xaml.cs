using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace TripLog
{
    public partial class TripLogPage : ContentPage
    {
        public TripLogPage()
        {
			Title = "TripLog";

			var newButton = new ToolbarItem
			{
				Text = "New"
			};
			newButton.Clicked += (sender, e) => { 
				Navigation.PushAsync(new NewEntryPage());
			};

			ToolbarItems.Add(newButton);

            var items = new List<TripLogEntry>
            {
                new TripLogEntry
                {
                    Title = "Washington Monument",
                    Notes = "Amazing!",
                    Rating = 3,
                    Date = new DateTime(2016,01,03),
                    Latitute = 38.8895,
                    Longitude = -770352
                },
                new TripLogEntry
                {
                    Title = "Statue of Liberty",
                    Notes = "Inspiring",
                    Rating = 4,
                    Date = new DateTime(2016,03,13),
                    Latitute = 40.6892,
                    Longitude = -74.0444
                },
                new TripLogEntry
                {
                    Title = "Golden Gate Bridge",
                    Notes = "Foggy but beautiful",
                    Rating = 5,
                    Date = new DateTime(2016,04,12),
                    Latitute = 37.8268,
                    Longitude = -122.4798
                }
            };

            var itemTemplate = new DataTemplate(typeof(TextCell));
            itemTemplate.SetBinding(TextCell.TextProperty, "Title");
            itemTemplate.SetBinding(TextCell.DetailProperty, "Notes");

            var listViewEntries = new ListView
            {
                ItemsSource = items,
                ItemTemplate = itemTemplate
            };

			listViewEntries.ItemTapped += async (sender, e) =>
			{
				var item = (TripLogEntry)e.Item;
				await Navigation.PushAsync(new DetailPage(item));
			};

            Content = listViewEntries;
        }
    }
}
