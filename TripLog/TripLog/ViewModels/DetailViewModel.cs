using System;
using System.Threading.Tasks;
using TripLog.Services;

namespace TripLog.ViewModels
{
	public class DetailViewModel : BaseViewModel<TripLogEntry>
	{

		public override Task Init(TripLogEntry parameter)
		{
			Entry = parameter;
			return Task.FromResult(0);
		}

		private TripLogEntry _entry;

		public TripLogEntry Entry
		{
			get { return _entry; }
			set
			{
				_entry = value;
				OnPropertyChanged();
			}
		}

		public DetailViewModel(INavService navService) : base(navService)
		{
	
		}
	}
}
