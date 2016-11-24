using System;
namespace TripLog
{
	public class TripLogEntry
	{
		public TripLogEntry()
		{
			
		}

		public string Title
		{
			get;
			set;
		}

		public double Latitute
		{
			get;
			set;
		}

		public double Longitude
		{
			get;
			set;
		}

		public DateTime Date
		{
			get;
			set;
		}

		public int Rating
		{
			get;
			set;
		}

		public string Notes
		{
			get;
			set;
		}
	}
}
