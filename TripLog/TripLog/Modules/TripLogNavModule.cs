using System;
using Ninject.Modules;
using TripLog.Services;
using TripLog.ViewModels;
using TripLog.Views;
using Xamarin.Forms;

namespace TripLog.Modules
{
	public class TripLogNavModule : NinjectModule
	{
		readonly INavigation _navigation;

		public TripLogNavModule(INavigation navigation)
		{
			_navigation = navigation;
		}

		public override void Load()
		{
			var navService = new XamarinFormsNavService();
			navService.NavigationInstance = _navigation;

			navService.RegisterViewMapping(typeof(TripLogViewModel), typeof(TripLogPage));
			navService.RegisterViewMapping(typeof(DetailViewModel), typeof(DetailPage));
			navService.RegisterViewMapping(typeof(NewEntryViewModel), typeof(NewEntryPage));

			Bind<INavService>().ToMethod(x => navService).InSingletonScope();

		}
	}
}
