using System;
using Ninject.Modules;
using TripLog.Droid;
using TripLog.Services;

namespace TripLog.Android
{
	/// <summary>
	/// Module with platform specific services.
	/// </summary>
	public class TripLogPlatformModule : NinjectModule
	{
		public override void Load()
		{
			Bind<ILocationService>().To<LocationService>().InSingletonScope();
		}
	}
}
