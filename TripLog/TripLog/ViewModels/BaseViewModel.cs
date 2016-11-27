using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TripLog.Services;

namespace TripLog.ViewModels
{
	public abstract class BaseViewModel : INotifyPropertyChanged
	{
		/// <summary>
		/// Provides ViewModels with a way of initializing without using the constructor, 
		/// which is useful when ViewModel needs to be refreshed.
		/// </summary>
		public abstract Task Init();

		protected INavService NavService { get; private set; }

		public BaseViewModel(INavService navService)
		{
			NavService = navService;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	public abstract class BaseViewModel<TParameter> : BaseViewModel
	{
		protected BaseViewModel(INavService navService) : base(navService) { }

		public abstract Task Init(TParameter parameter);

		public override async Task Init()
		{
			await Init(default(TParameter));
		}
	}
}
