using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EmployeeManager
{
    public class StateMachineVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string state = value != null ? value.ToString() : string.Empty;
            string targetState = parameter.ToString();

            return state == targetState ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

}
