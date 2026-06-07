using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RefitDemo.Converters
{
    /// <summary>
    /// Converts a boolean to Visibility: true → Collapsed, false → Visible.
    /// Useful for hiding elements when a busy/loading flag is set.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public sealed class InverseBoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
                return b ? Visibility.Collapsed : Visibility.Visible;
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility v)
                return v != Visibility.Visible;
            return true;
        }
    }
}
