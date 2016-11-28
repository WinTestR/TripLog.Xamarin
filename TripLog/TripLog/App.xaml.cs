using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Modules;
using TripLog.Modules;
using TripLog.Services;
using TripLog.ViewModels;
using TripLog.Views;
using Xamarin.Forms;

namespace TripLog
{
	public partial class App : Application
	{

		public IKernel Kernel { get; set; }

		public App(params INinjectModule[] platformModules)
		{
			InitializeComponent();

			var mainPage = new NavigationPage(new TripLog.Views.TripLogPage());

			//Register core services
			Kernel = new StandardKernel(
				new TripLogCoreModule(),
				new TripLogNavModule(mainPage.Navigation)
			);

			//Register platform specific services
			Kernel.Load(platformModules);

			//Get the TripLogViewModel from the IoC
			mainPage.BindingContext = Kernel.Get<TripLogViewModel>();

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
