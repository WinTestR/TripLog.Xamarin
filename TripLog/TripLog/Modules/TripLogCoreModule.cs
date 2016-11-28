using System;
using Ninject.Modules;
using TripLog.ViewModels;

namespace TripLog.Modules
{
	public class TripLogCoreModule : NinjectModule
	{
		public override void Load()
		{
			//register ViewModules
			Bind<TripLogViewModel>().ToSelf();
			Bind<DetailViewModel>().ToSelf();
			Bind<NewEntryViewModel>().ToSelf();
		}
	}
}
