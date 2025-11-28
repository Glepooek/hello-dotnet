using System;
using System.Globalization;
using System.Windows.Data;

namespace ReusableControls.Demos
{
	public class TimeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var time = (DateTime)value;

			return time.Hour.ToString() + "h" + time.Minute.ToString() + "′" + time.Second + "″";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
