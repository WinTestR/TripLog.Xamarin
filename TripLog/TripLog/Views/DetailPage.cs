using System;
using TripLog.Converters;
using TripLog.Services;
using TripLog.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TripLog.Views
{
	public class DetailPage : ContentPage
	{
		private DetailViewModel _vm
		{
			get
			{
				return BindingContext as DetailViewModel;
			}
		}

		private readonly Map _map;

		public DetailPage()
		{
			/* Because the ViewModel is being initialized via the navigation service, it happens
				 * AFTER the the page is contructed and therefore the map does have the data it needs.
				 * Normally this would not be a problem thanks to data binding but since the map controls
				 * does not allow for data binding we need to handle its data differently.
				 * Best way for the page to check when its ViewModel has data for its map control is to handle the
				 * ViewModel's PropertyChanged even.
				 * Note: This must be setup BEFORE BindingContext is instantiated.
				 */
			BindingContextChanged += (sender, args) =>
			{
				if (_vm == null) return;
				_vm.PropertyChanged += (s, e) =>
				{
					if (e.PropertyName == "Entry")
						updateMap();
				};
			};

			//going Ninject, BindingContext = new DetailViewModel(DependencyService.Get<INavService>());

			Title = "Entry Details";

			//Map with a Pin
			_map = new Map();

			var title = new Label { HorizontalOptions = LayoutOptions.Center };
			title.SetBinding(Label.TextProperty, "Entry.Title");

			var date = new Label { HorizontalOptions = LayoutOptions.Center };
			date.SetBinding(Label.TextProperty, "Entry.Date", stringFormat: "{0:M}");

			var rating = new Image { HorizontalOptions = LayoutOptions.Center };
			rating.SetBinding(Image.SourceProperty, "Entry.Rating", converter: new RatingToStarImageNameConverter());

			var notes = new Label { HorizontalOptions = LayoutOptions.Center };
			notes.SetBinding(Label.TextProperty, "Entry.Notes");

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
			mainLayout.Children.Add(_map);
			mainLayout.Children.Add(detailsBg, 0, 1);
			mainLayout.Children.Add(details, 0, 1);
			Grid.SetRowSpan(_map, 3);

			Content = mainLayout;
		}



		private void updateMap()
		{
			if (_vm.Entry == null)
				return;

			var position = new Position(_vm.Entry.Latitute, _vm.Entry.Longitude);
			_map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(0.5)));
			_map.Pins.Add(
				new Pin
				{
					Type = PinType.Place,
					Label = _vm.Entry.Title,
					Position = position
				});

		}
	}
}

