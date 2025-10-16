using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Test.TransformDemo
{
    //θ = 35°
    //原始宽度 = 100
    //原始高度 = 30

    //新宽度 = 100 * |cos(35°)| + 30 * |sin(35°)|
    //新高度 = 30 * |cos(35°)| + 100 * |sin(35°)|

    //Δx = (新宽度 - 100) / 2
    //Δy = (新高度 - 30) / 2

    public class OffsetConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length != 4)
            {
                return new Thickness(0);
            }

            if (values[0] is not double width
                || values[1] is not double height
                || values[2] is not double angle
                || values[3] is not Thickness region)
            {
                return new Thickness(0);
            }

            if (angle == 0)
            {
                return region;
            }

            // 将角度转换为弧度
            double angleInRadians = angle * (Math.PI / 180);

            // 计算旋转后的宽度和高度
            double cosTheta = Math.Abs(Math.Cos(angleInRadians));
            double sinTheta = Math.Abs(Math.Sin(angleInRadians));
            double newWidth = width * cosTheta + height * sinTheta;
            double newHeight = height * cosTheta + width * sinTheta;

            // 计算偏移量
            double offsetX = (newWidth - width) / 2;
            double offsetY = (newHeight - height) / 2;

            return new Thickness(region.Left - offsetX, region.Top - offsetY, region.Right, region.Bottom);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
