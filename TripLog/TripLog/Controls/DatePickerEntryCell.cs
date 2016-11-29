using System;
using Xamarin.Forms;

namespace TripLog.Controls
{
	public class DatePickerEntryCell : EntryCell
	{
		public static readonly BindableProperty DateProperty = BindableProperty.Create(
			nameof(Date), typeof(DateTime), typeof(DatePickerEntryCell), DateTime.Now, propertyChanged: new BindableProperty.BindingPropertyChangedDelegate(datePropertyChanged));

		public DateTime Date
		{
			get { return (DateTime)GetValue(DateProperty); }
			set { SetValue(DateProperty, value); }
		}

		static void datePropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var @this = (DatePickerEntryCell)bindable;
			if (@this.Completed != null)
				@this.Completed(bindable, new EventArgs());
		}

		public new event EventHandler Completed;

	}
}
	
