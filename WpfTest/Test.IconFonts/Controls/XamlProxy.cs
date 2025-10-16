using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Test.IconFonts.Controls
{
    public class XamlProxy : DependencyObject
    {
        #region 圆角

        public static CornerRadius GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(XamlProxy), new PropertyMetadata(new CornerRadius(2)));

        #endregion

        #region 图标边框颜色

        public static SolidColorBrush GetIconBrush(DependencyObject obj)
        {
            return (SolidColorBrush)obj.GetValue(IconBrushProperty);
        }

        public static void SetIconBrush(DependencyObject obj, SolidColorBrush value)
        {
            obj.SetValue(IconBrushProperty, value);
        }

        public static readonly DependencyProperty IconBrushProperty =
            DependencyProperty.RegisterAttached("IconBrush", typeof(SolidColorBrush), typeof(XamlProxy), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        #endregion
    }
}
