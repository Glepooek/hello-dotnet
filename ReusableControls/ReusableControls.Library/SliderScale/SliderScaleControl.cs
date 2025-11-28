using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace Digihail.Controls
{
    public class SliderScaleControl : Slider
    {
        private ToolTip _autoToolTip;
        private string _autoToolTipFormat;

        public string AutoToolTipFormat
        {
            get { return _autoToolTipFormat; }
            set { _autoToolTipFormat = value; }
        }

        protected override void OnThumbDragStarted(DragStartedEventArgs e)
        {
            base.OnThumbDragStarted(e);
            this.FormatAutoToolTipContent();
        }

        protected override void OnThumbDragDelta(DragDeltaEventArgs e)
        {
            base.OnThumbDragDelta(e);
            this.FormatAutoToolTipContent();
        }

        /// <summary>
        /// 设置AutoToolTip的样式及内容
        /// </summary>
        private void FormatAutoToolTipContent()
        {
            if (!string.IsNullOrEmpty(this.AutoToolTipFormat))
            {
                this.AutoToolTip.Background = new SolidColorBrush(Color.FromArgb(0x00, 0x00, 0x00, 0x00));
                this.AutoToolTip.BorderThickness = new Thickness(0);
                Border border = new Border();
                border.CornerRadius = new CornerRadius(5);
                border.Background = AutoToolTipBackGround;
                border.BorderThickness = new Thickness(0);
                TextBlock text = new TextBlock();
                text.Padding = new Thickness(10, 5, 10, 5);
                text.Foreground = LeftRepeatButtonColor;
                text.Text = string.Format(
                    this.AutoToolTipFormat,
                    this.AutoToolTip.Content);
                text.HorizontalAlignment = HorizontalAlignment.Center;
                text.VerticalAlignment = VerticalAlignment.Center;
                border.Child = text;
                this.AutoToolTip.Content = border;
                //this.AutoToolTip.Content = string.Format(
                //    this.AutoToolTipFormat,
                //    this.AutoToolTip.Content);
            }
        }

        private ToolTip AutoToolTip
        {
            get
            {
                if (_autoToolTip == null)
                {
                    FieldInfo field = typeof(Slider).GetField(
                        "_autoToolTip",
                        BindingFlags.NonPublic | BindingFlags.Instance);

                    _autoToolTip = field.GetValue(this) as ToolTip;
                }

                return _autoToolTip;
            }
        }

        static SliderScaleControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SliderScaleControl), new FrameworkPropertyMetadata(typeof(SliderScaleControl)));
        }

        /// <summary>
        /// 刻度尺坐标轴文字集合
        /// </summary>
        private ObservableCollection<string> ScaleValueList
        {
            get { return (ObservableCollection<string>)GetValue(ScaleValueListProperty); }
            set { SetValue(ScaleValueListProperty, value); }
        }
        public static readonly DependencyProperty ScaleValueListProperty =
            DependencyProperty.Register("ScaleValueList", typeof(ObservableCollection<string>), typeof(SliderScaleControl), new PropertyMetadata(new ObservableCollection<string>()));

        /// <summary>
        /// Slider左侧RepeatButton颜色
        /// </summary>
        public Brush LeftRepeatButtonColor
        {
            get { return (Brush)GetValue(LeftRepeatButtonColorProperty); }
            set { SetValue(LeftRepeatButtonColorProperty, value); }
        }
        public static readonly DependencyProperty LeftRepeatButtonColorProperty =
            DependencyProperty.Register("LeftRepeatButtonColor", typeof(Brush), typeof(SliderScaleControl), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(0xFF, 0x40, 0xC3, 0xE3))));

        /// <summary>
        ///  Slider右侧RepeatButton颜色
        /// </summary>
        public Brush RightRepeatButtonColor
        {
            get { return (Brush)GetValue(RightRepeatButtonColorProperty); }
            set { SetValue(RightRepeatButtonColorProperty, value); }
        }
        public static readonly DependencyProperty RightRepeatButtonColorProperty =
            DependencyProperty.Register("RightRepeatButtonColor", typeof(Brush), typeof(SliderScaleControl), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(0xFF, 0x66, 0x66, 0x66))));

        /// <summary>
        /// 提示框背景颜色
        /// </summary>
        public Brush AutoToolTipBackGround
        {
            get { return (Brush)GetValue(AutoToolTipBackGroundProperty); }
            set { SetValue(AutoToolTipBackGroundProperty, value); }
        }
        public static readonly DependencyProperty AutoToolTipBackGroundProperty =
            DependencyProperty.Register("AutoToolTipBackGround", typeof(Brush), typeof(SliderScaleControl), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(0xFF, 0x11, 0x11, 0x11))));

        public SliderScaleControl()
        {
            this.Loaded += TimeScaleControl_Loaded;
        }

        void TimeScaleControl_Loaded(object sender, RoutedEventArgs e)
        {
            ScaleValueList = new ObservableCollection<string>();
            double TicksCount = (Maximum - Minimum) / TickFrequency + 1;
            for (int i = 0; i < TicksCount; i++)
            {
                ScaleValueList.Add((i).ToString());
            }
        }
    }

    /// <summary>
    /// 根据索引和数据集合的总数以及控件的宽度计算TickLable的宽度
    /// </summary>
    public class SetTickLableWidthConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double TickLableWidth = 0;
            double Index = double.Parse(values[0].ToString());
            double Count = double.Parse(values[1].ToString());
            double Width = double.Parse(values[2].ToString());
            if (Index != 0 && Index != (Count - 1))
            {
                TickLableWidth = ((Width - 0) / (Count - 1)) * Index * 2;

                if (Index < ((Count - 1) / 2))
                {
                    TickLableWidth += 5;
                }
                else if (Index > ((Count - 1) / 2))
                {
                    TickLableWidth -= 5;
                }
            }
            return TickLableWidth;
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 根据索引和起始值计算TickValue
    /// </summary>
    public class SetTickLableConverter : IMultiValueConverter
    {
        int count = 0;
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double Index = double.Parse(values[0].ToString());
            double Start = double.Parse(values[1].ToString());
            double TickValue = (Index * count) + Start;
            count++;
            return TickValue.ToString();
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
