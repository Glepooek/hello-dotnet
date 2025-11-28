using System;
using System.Globalization;
using System.Windows.Data;

namespace Digihail.Controls
{
    public class NoRoundingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)System.Convert.ToDouble(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
