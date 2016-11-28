using System;
using Ninject.Modules;
using TripLog.Services;

namespace TripLog.iOS
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
