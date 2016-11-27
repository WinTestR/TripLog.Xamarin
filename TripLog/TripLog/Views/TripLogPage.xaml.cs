using System;
using System.Collections.Generic;
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
			BindingContext = new TripLogViewModel(DependencyService.Get<INavService>());
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

			listViewEntries.ItemTapped += (sender, e) =>
			{
				var item = (TripLogEntry)e.Item;
				_vm.ViewCommand.Execute(item);
			};

            Content = listViewEntries;
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
