using System;
using System.Threading.Tasks;
using TripLog.Models;
using TripLog.Services;
using Xamarin.Forms;
using Xamarin.Geolocation;

namespace TripLog.Droid
{
	public class LocationService : ILocationService
	{
		public async Task<GeoCoords> GetGeoCoordinatesAsync()
		{
			//the difference with iOS is that Android requires Geolocator to be instantiated with the current Android Activity (=Forms.Contex)
			var locator = new Geolocator(Forms.Context)
			{
				DesiredAccuracy = 30
			};

			var position = await locator.GetPositionAsync(30000);
			return new GeoCoords
			{
				Latitude = position.Latitude,
				Longitude = position.Longitude
			};
		}
	}
}
