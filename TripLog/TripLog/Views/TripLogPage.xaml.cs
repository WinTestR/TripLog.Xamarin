using System;
using System.Collections.Generic;
using TripLog.Converters;
using TripLog.Services;
using TripLog.ViewModels;
using Xamarin.Forms;

namespace TripLog.Views
{
    public partial class TripLogPage : ContentPage
    {

		private TripLogViewModel _vm
		{
			get
			{
				return BindingContext as TripLogViewModel;
			}
		}

		public TripLogPage()
		{
			//going Ninject - BindingContext = new TripLogViewModel(DependencyService.Get<INavService>());
			Title = "TripLog";

			var newButton = new ToolbarItem
			{
				Text = "New"
			};
			newButton.SetBinding(ToolbarItem.CommandProperty, "NewCommand");

			ToolbarItems.Add(newButton);

			var itemTemplate = new DataTemplate(typeof(TextCell));
			itemTemplate.SetBinding(TextCell.TextProperty, "Title");
			itemTemplate.SetBinding(TextCell.DetailProperty, "Notes");

			var listViewEntries = new ListView
			{
				ItemTemplate = itemTemplate
			};

			listViewEntries.SetBinding(ListView.ItemsSourceProperty, "LogEntries");
			listViewEntries.SetBinding(ListView.IsVisibleProperty, "IsBusy", converter: new ReverseBooleanConverter());

			listViewEntries.ItemTapped += (sender, e) =>
			{
				var item = (TripLogEntry)e.Item;
				_vm.ViewCommand.Execute(item);
			};

			var loading = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Children = {
					new ActivityIndicator { IsRunning = true },
					new Label { Text = "Loading Entries..."}
				}
			};
			loading.SetBinding(StackLayout.IsVisibleProperty, "IsBusy");

			var mainLayout = new Grid
			{
				Children = { listViewEntries, loading }
			};

			Content = mainLayout;
        }

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			//initialize main view model - the rest of view models are initialized via Navigation Service, this is the first
			if (_vm != null)
				await _vm.Init();

		}
    }
}
