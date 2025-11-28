using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Learning.PrismDemo.Utilities
{
	/// <summary>
	/// 
	/// </summary>
	public class BoolToVisibilityConverter : IValueConverter
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var param = parameter == null ? string.Empty : parameter.ToString();
			param = param.ToLower();
			if (value is bool)
			{
				if ((bool)value)
				{
					return Visibility.Visible;
				}
				else
				{
					if (param == "hidden")
					{
						return Visibility.Hidden;
					}
					return Visibility.Collapsed;
				}
			}
			else
			{
				if (param == "hidden")
				{
					return Visibility.Hidden;
				}
				return Visibility.Collapsed;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class BoolToVisibilityTakebackConverter : IValueConverter
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var param = parameter == null ? string.Empty : parameter.ToString();
			param = param.ToLower();
			if (value is bool)
			{
				if ((bool)value)
				{
					if (param == "hidden")
					{
						return Visibility.Hidden;
					}
					return Visibility.Collapsed;
				}
				else
				{
					return Visibility.Visible;
				}
			}
			else
			{
				if (param == "hidden")
				{
					return Visibility.Hidden;
				}
				return Visibility.Collapsed;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class VisibilityTakebackConverter : IValueConverter
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Visibility)
			{
				var visible = (Visibility)value;
				if (visible == Visibility.Visible)
				{
					if (parameter == null || parameter.ToString().ToLower() == "hidden")
					{
						return Visibility.Hidden;
					}
					else
					{
						return Visibility.Collapsed;
					}
				}
				else
				{
					return Visibility.Visible;
				}
			}
			return Visibility.Collapsed;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class BoolTakebackConverter : IValueConverter
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool)
			{
				return !(bool)value;
			}
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class VisibilityToBoolConverter : IValueConverter
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Visibility)
			{
				return (Visibility)value == Visibility.Visible;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}