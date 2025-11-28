using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Learning.PrismDemo.Main.Converters
{
	internal class ViewStateConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (parameter == null)
			{
				throw new ArgumentNullException($"参数{nameof(parameter)}不能为null");
			}
			if (value == null)
			{
				throw new ArgumentNullException($"参数{nameof(value)}不能为null");
			}
			return value.ToString() == parameter.ToString() ? Visibility.Visible : Visibility.Hidden;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
