using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Test.CountDown
{
    public class ForegroundConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null)
            {
                if (values[0] is int minuteShi && minuteShi == 0
                    && values[1] is int minuteGe && minuteGe == 0
                    && values[2] is int secondShi && secondShi == 0
                    && values[3] is int secondGe && secondGe <= 5 && secondGe > 0)
                {
                    return new SolidColorBrush(Colors.Red);
                }
            }

            return new SolidColorBrush(Colors.Black);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
