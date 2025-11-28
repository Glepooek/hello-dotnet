using System;
using System.Windows;
using System.Windows.Data;

namespace Digihail.Controls
{
    public class OverviewMarginConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var yAxisWidth = 0.0;
            var paddingLeft = 0.0;
            var marginLeft = 0.0;

            if(double.TryParse(values[0].ToString(), out yAxisWidth))
            {
                marginLeft = marginLeft + yAxisWidth;
            }

            if(double.TryParse(values[1].ToString(), out paddingLeft))
            {
                marginLeft = marginLeft + paddingLeft;
            }

            return new Thickness(marginLeft, 0, 0, 0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
