using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TripLog.Services;
using TripLog.ViewModels;
using TripLog.Views;
using Xamarin.Forms;

namespace TripLog
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			var mainPage = new NavigationPage(new TripLog.Views.TripLogPage());

			var navService = DependencyService.Get<INavService>() as XamarinFormsNavService;
			navService.NavigationInstance = mainPage.Navigation;
			navService.RegisterViewMapping(typeof(TripLogViewModel), typeof(TripLogPage));
			navService.RegisterViewMapping(typeof(DetailViewModel), typeof(DetailPage));
			navService.RegisterViewMapping(typeof(NewEntryViewModel), typeof(NewEntryPage));

			MainPage = mainPage;
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
