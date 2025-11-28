using System;
using System.Globalization;
using System.Windows.Data;

namespace Learning.PrismDemo.Login.Converters
{
	internal class NetworkStateConvert : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool isAvailable && isAvailable)
			{
				return "网络已连接";
			}

			return "网络连接不可用";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
