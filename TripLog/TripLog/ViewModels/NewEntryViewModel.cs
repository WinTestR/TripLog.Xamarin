using System;
using System.Threading.Tasks;
using TripLog.Services;
using Xamarin.Forms;

namespace TripLog.ViewModels
{
	public class NewEntryViewModel : BaseViewModel
	{

		public override Task Init()
		{
			//nothing here
			return Task.FromResult(0);
		}

		private string _title;
		public string Title
		{
			get { return _title; }
			set
			{
				_title = value;
				OnPropertyChanged();
				SaveCommand.ChangeCanExecute();
			}
		}

		private double _latitude;
		public double Latitude
		{
			get { return _latitude; }
			set
			{
				_latitude = value;
				OnPropertyChanged();
			}
		}

		private double _longitute;
		public double Longitute
		{
			get { return _longitute; }
			set
			{
				_longitute = value;
				OnPropertyChanged();
			}
		}

		private DateTime _date; 
		public DateTime Date
		{
			get { return _date; }
			set
			{
				_date = value;
				OnPropertyChanged();
			}
		}

		private int _rating;
		public int Rating
		{
			get { return _rating; }
			set
			{
				_rating = value;
				OnPropertyChanged();
			}
		}

		private string _notes;
		public string Notes
		{
			get { return _notes; }
			set
			{
				_notes = value;
				OnPropertyChanged();
			}
		}

		private Command _saveCommand;
		public Command SaveCommand
		{
			get { return _saveCommand ?? (_saveCommand = new Command(async () => await executeSaveCommand(), canSave)); }
		}

		public NewEntryViewModel(INavService navService) : base(navService)
		{
			Date = DateTime.Today;
			Rating = 1;
		}

		private async Task executeSaveCommand()
		{
			var newItem = new TripLogEntry
			{
				Title = this.Title,
				Latitute = this.Latitude,
				Longitude = this.Longitute,
				Date = this.Date,
				Rating = this.Rating,
				Notes = this.Notes
			};
			//TODO: Implement logic to persist newItem

			await NavService.GoBack();
		}

		private bool canSave()
		{
			return !string.IsNullOrWhiteSpace(Title);
		}

	}
}
