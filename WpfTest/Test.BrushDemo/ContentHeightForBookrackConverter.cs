using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Test.TileBrushDemo
{
    public class ContentHeightForBookrackConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null
                || !double.TryParse(value.ToString(), out double actualHeight))
            {
                return new Rect(0, 0, 1, 1);
            }

            int row = (int)Math.Ceiling(actualHeight / 183.0);
            if (row <= 0) row = 1;

            double height = 1.0 / row;
            return new Rect(0, 0, 1, height);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
