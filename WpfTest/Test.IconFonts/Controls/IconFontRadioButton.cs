using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Test.IconFonts.Controls
{
    /// <summary>
    /// 文本相对于Icon的位置
    /// </summary>
    public enum TextLocation
    {
        /// <summary>
        /// 没有文本
        /// </summary>
        None,

        /// <summary>
        /// 文本在Icon右边
        /// </summary>
        Right,

        /// <summary>
        /// 文本在Icon下边
        /// </summary>
        Bottom
    }

    /// <summary>
    /// IconFont单选按钮
    /// </summary>
    public class IconFontRadioButton : RadioButton
    {
        static IconFontRadioButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(IconFontRadioButton),
                new FrameworkPropertyMetadata(typeof(IconFontRadioButton))
            );
        }

        public string IconFontContent
        {
            get { return (string)GetValue(IconFontContentProperty); }
            set { SetValue(IconFontContentProperty, value); }
        }

        public static readonly DependencyProperty IconFontContentProperty =
            DependencyProperty.Register(
                "IconFontContent",
                typeof(string),
                typeof(IconFontRadioButton),
                new PropertyMetadata(string.Empty)
            );

        public double IconFontSize
        {
            get { return (double)GetValue(IconFontSizeProperty); }
            set { SetValue(IconFontSizeProperty, value); }
        }

        public static readonly DependencyProperty IconFontSizeProperty =
            DependencyProperty.Register(
                "IconFontSize",
                typeof(double),
                typeof(IconFontRadioButton),
                new PropertyMetadata(10.0)
            );

        public Brush IconFontForeground
        {
            get { return (Brush)GetValue(IconFontForegroundProperty); }
            set { SetValue(IconFontForegroundProperty, value); }
        }

        public static readonly DependencyProperty IconFontForegroundProperty =
            DependencyProperty.Register(
                "IconFontForeground",
                typeof(Brush),
                typeof(IconFontRadioButton),
                new PropertyMetadata(null)
            );

        public TextLocation TextLocation
        {
            get { return (TextLocation)GetValue(TextLocationProperty); }
            set { SetValue(TextLocationProperty, value); }
        }

        public static readonly DependencyProperty TextLocationProperty =
            DependencyProperty.Register(
                "TextLocation",
                typeof(TextLocation),
                typeof(IconFontRadioButton),
                new PropertyMetadata(TextLocation.None)
            );

        public double IconFontSpacing
        {
            get { return (double)GetValue(IconFontSpacingProperty); }
            set { SetValue(IconFontSpacingProperty, value); }
        }

        public static readonly DependencyProperty IconFontSpacingProperty =
            DependencyProperty.Register(
                "IconFontSpacing",
                typeof(double),
                typeof(IconFontRadioButton),
                new PropertyMetadata(0.0, On)
            );

        private static void On(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is IconFontRadioButton button && e.NewValue is double space)
            {
                if (button.TextLocation == TextLocation.Right)
                {
                    button.IconFontMargin = new Thickness(0, 0, space, 0);
                }
                else if (button.TextLocation == TextLocation.Bottom)
                {
                    button.IconFontMargin = new Thickness(0, 0, 0, space);
                }
            }
        }

        /// <summary>
        /// Icon与文本之间的间距
        /// </summary>
        internal Thickness IconFontMargin
        {
            get { return (Thickness)GetValue(IconFontMarginProperty); }
            set { SetValue(IconFontMarginProperty, value); }
        }

        internal static readonly DependencyProperty IconFontMarginProperty =
            DependencyProperty.Register(
                "IconFontMargin",
                typeof(Thickness),
                typeof(IconFontRadioButton),
                new PropertyMetadata(new Thickness(0))
            );
    }
}
