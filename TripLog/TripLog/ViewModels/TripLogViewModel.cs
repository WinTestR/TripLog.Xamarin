using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TripLog.Services;
using Xamarin.Forms;

namespace TripLog.ViewModels
{
	public class TripLogViewModel : BaseViewModel
	{

		private Command<TripLogEntry> _viewCommand;
		public Command<TripLogEntry> ViewCommand
		{
			get
			{
				return _viewCommand ?? (_viewCommand = new Command<TripLogEntry>(async (entry) => await viewCommandHandler(entry)));
			}
		}

		private Command _newCommand;
		public Command NewCommand
		{
			get
			{
				return _newCommand ?? (_newCommand = new Command(async () => await newCommandHandler()));
			}
		}

		private async Task newCommandHandler()
		{
			await NavService.NavigateTo<NewEntryViewModel>();
		}

		private async Task viewCommandHandler(TripLogEntry entry)
		{
			await NavService.NavigateTo<DetailViewModel, TripLogEntry>(entry);
		}

		private ObservableCollection<TripLogEntry> _logEntries;
		public ObservableCollection<TripLogEntry> LogEntries
		{
			get { return _logEntries; }
			set
			{
				_logEntries = value;
				OnPropertyChanged();
			}
		}

		public TripLogViewModel(INavService navService) : base (navService)
		{
			LogEntries = new ObservableCollection<TripLogEntry>();
		}

		public override async Task Init()
		{
			await loadEntriesAsync();
		}

		private async Task loadEntriesAsync()
		{
			LogEntries.Clear();

			IsBusy = true;
			//to simulate delay - remove
			await Task.Delay(TimeSpan.FromSeconds(3));

			await Task.Run(() =>
			{
				LogEntries.Add(
					new TripLogEntry
					{
						Title = "Washington Monument",
						Notes = "Amazing!",
						Rating = 3,
						Date = new DateTime(2016, 01, 03),
						Latitute = 38.8895,
						Longitude = -770352
					});
				LogEntries.Add(
					new TripLogEntry
					{
						Title = "Statue of Liberty",
						Notes = "Inspiring",
						Rating = 4,
						Date = new DateTime(2016, 03, 13),
						Latitute = 40.6892,
						Longitude = -74.0444
					});
				LogEntries.Add(
					new TripLogEntry
					{
						Title = "Golden Gate Bridge",
						Notes = "Foggy but beautiful",
						Rating = 5,
						Date = new DateTime(2016, 04, 12),
						Latitute = 37.8268,
						Longitude = -122.4798
					});
			});

			IsBusy = false;
		}
	}
}
