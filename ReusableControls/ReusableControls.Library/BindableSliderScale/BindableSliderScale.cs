using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Digihail.Controls
{
    /// <summary>
    /// 提示信息可绑定的滑动比例尺
    /// </summary>
    public class BindableSliderScale : Slider, INotifyPropertyChanged
    {
        #region Constructor

        static BindableSliderScale()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BindableSliderScale), new FrameworkPropertyMetadata(typeof(BindableSliderScale)));
        }

        public BindableSliderScale()
        {
            SetCurrentValue(LeftRepeatButtonColorProperty, new SolidColorBrush(Color.FromArgb(0xFF, 0x40, 0xC3, 0xE3)));
            SetCurrentValue(RightRepeatButtonColorProperty, new SolidColorBrush(Color.FromArgb(0xFF, 0x66, 0x66, 0x66)));
            SetCurrentValue(AutoToolTipBackGroundProperty, new SolidColorBrush(Color.FromArgb(0xFF, 0x11, 0x11, 0x11)));
            SetCurrentValue(TickPlacementProperty, TickPlacement.TopLeft);
            SetCurrentValue(IsSnapToTickEnabledProperty, false);
            SetCurrentValue(IsTickDisplayDataProperty, false);

            SizeChanged += (sender, e) =>
            {
                if(e.NewSize.Width != e.PreviousSize.Width)
                {
                    currentWidth = e.NewSize.Width;
                    SetScale();
                }
            };
        }

        #endregion

        #region Methods

        public static void OnIsTickDisplayDataSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((BindableSliderScale)o).SetScale();
        }

        public static void OnTickDataFormatSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((BindableSliderScale)o).SetScale();
        }
        
        public static void OnMainTickValueSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((BindableSliderScale)o).SetScale();
        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> action)
        {
            var propertyName = GetPropertyName(action);
            RaisePropertyChanged(propertyName);
        }

        private static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            var propertyName = expression.Member.Name;

            return propertyName;
        }

        protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
            SetScale();
        }

        protected override void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
            SetScale();
        }

        private void SetScale()
        {
            TickFrequency = MainTickValue; // 每次计算时首先对tf赋值
            scaleValueList = new ObservableCollection<FastScale>();

            var temp = (int)((Maximum - Minimum) / TickFrequency);
            var ticksCount = (Maximum - Minimum) % TickFrequency == 0 ? temp + 1 : temp + 2;
            string value;
            double width;
            Thickness margin;
            HorizontalAlignment horizontalAlignment;
            HorizontalAlignment containerHorizontalAlignment;

            if(IsTickDisplayData && BindingDatas != null && BindingDatas.Count > 0)
            {
                Minimum = 0;
                Maximum = BindingDatas.Count;
            }

            for (int i = 0; i < ticksCount; i++)
            {
                if (i == 0)
                {
                    width = currentWidth;
                    horizontalAlignment = HorizontalAlignment.Left;
                    containerHorizontalAlignment = HorizontalAlignment.Left;
                    margin = new Thickness(-4, 2, 0, 2);

                    if(IsTickDisplayData && BindingDatas != null && BindingDatas.Count > 0)
                    {
                        value = BindingDatas[0].ToString();
                    }
                    else
                    {
                        value = Minimum.ToString();
                    }
                }
                else if (i == ticksCount - 1)
                {
                    width = currentWidth - 10;
                    horizontalAlignment = HorizontalAlignment.Right;
                    containerHorizontalAlignment = HorizontalAlignment.Right;
                    margin = new Thickness(0, 2, -4, 2);

                    if (IsTickDisplayData && BindingDatas != null && BindingDatas.Count > 0)
                    {
                        value = BindingDatas[BindingDatas.Count - 1].ToString();
                    }
                    else
                    {
                        value = Maximum.ToString();
                    }
                }
                else
                {
                    if (IsTickDisplayData && BindingDatas != null && BindingDatas.Count > 0)
                    {
                        value = BindingDatas[i].ToString();
                    }
                    else
                    {
                        value = (MainTickValue * i + Minimum).ToString();
                    }

                    horizontalAlignment = HorizontalAlignment.Center;

                    if (i <= (ticksCount - 1) / 2)
                    {
                        width = (currentWidth / (Maximum - Minimum)) * MainTickValue * i * 2; 
                        containerHorizontalAlignment = HorizontalAlignment.Left;
                        margin = new Thickness(0,2,0,2);
                    }
                    else
                    {
                        width = (currentWidth / (Maximum - Minimum)) * ((Maximum - Minimum) - MainTickValue * i) * 2; 
                        containerHorizontalAlignment = HorizontalAlignment.Right;
                        margin = new Thickness(0,2,0,2);
                    }
                }

                if(!string.IsNullOrEmpty(TickDataFormat))
                {
                    value = string.Format(value, TickDataFormat);
                }

                scaleValueList.Add(new FastScale()
                {
                    Label = value,
                    Size = width,
                    LabelHorizontalAlignment = horizontalAlignment,
                    ContainerHorizontalAlignment = containerHorizontalAlignment,
                    Margin = margin
                });
            }

            RaisePropertyChanged(() => ScaleValueList);
        }

        protected override void OnThumbDragStarted(DragStartedEventArgs e)
        {
            base.OnThumbDragStarted(e);
            FormatAutoToolTipContent();
        }

        protected override void OnThumbDragDelta(DragDeltaEventArgs e)
        {
            base.OnThumbDragDelta(e);
            FormatAutoToolTipContent();
        }

        private void FormatAutoToolTipContent()
        {
            if (BSSAutoToolTip != null)
            {
                BSSAutoToolTip.DataContext = this;
                BSSAutoToolTip.Background = new SolidColorBrush(Colors.Transparent);
                BSSAutoToolTip.BorderThickness = new Thickness(0);
                var value = "";
                var index = -1;
                int.TryParse(BSSAutoToolTip.Content.ToString(), out index);

                if (index > -1)
                {
                    index = (int)(index - Minimum);

                    if (BindingDatas != null && BindingDatas.Count > index)
                    {
                        value = BindingDatas[index].ToString();
                    }
                };

                if (!string.IsNullOrEmpty(AutoToolTipFormat))
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        autoToolTipText = string.Format(AutoToolTipFormat, BSSAutoToolTip.Content, value);
                    }
                    else if (AutoToolTipFormat.Contains("{1}"))
                    {
                        autoToolTipText = string.Format(AutoToolTipFormat, BSSAutoToolTip.Content, "");
                    }
                    else
                    {
                        autoToolTipText = string.Format(AutoToolTipFormat, BSSAutoToolTip.Content);
                    }
                }
                else
                {
                    autoToolTipText = index.ToString();
                }

                RaisePropertyChanged(() => AutoToolTipText);
                BSSAutoToolTip.Content = AutoToolTipContent;
            }
        }

        #endregion

        #region Fields & Properties

        private double tickFrequency;
        public new double TickFrequency
        {
            get
            {
                return tickFrequency;
            }
            private set
            {
                if (value != tickFrequency)
                {
                    tickFrequency = value;
                    base.TickFrequency = tickFrequency;
                }
            }
        }

        public string AutoToolTipFormat
        {
            get;
            set;
        }

        private ObservableCollection<FastScale> scaleValueList;
        public ObservableCollection<FastScale> ScaleValueList
        {
            get
            {
                return scaleValueList;
            }
        }

        private ToolTip bssAutoToolTip;
        public ToolTip BSSAutoToolTip
        {
            get
            {
                if (bssAutoToolTip == null)
                {
                    bssAutoToolTip = (typeof(Slider).GetField("_autoToolTip", BindingFlags.NonPublic | BindingFlags.Instance)).GetValue(this) as ToolTip;
                }

                return bssAutoToolTip;
            }
        }

        private string autoToolTipText;
        public string AutoToolTipText
        {
            get
            {
                return autoToolTipText;
            }
        }

        /// <summary>
        /// 提示框样式
        /// </summary>
        public ContentControl AutoToolTipContent
        {
            get
            {
                return (ContentControl)GetValue(AutoToolTipContentProperty);
            }
            set
            {
                SetValue(AutoToolTipContentProperty, value);
            }
        }

        public static readonly DependencyProperty AutoToolTipContentProperty = DependencyProperty.Register("AutoToolTipContent", typeof(ContentControl), typeof(BindableSliderScale));

        /// <summary>
        /// 绑定刻度的数据源（靠刻度作为索引）
        /// </summary>
        public IList BindingDatas
        {
            get
            {
                return (IList)GetValue(BindingDatasProperty);
            }
            set
            {
                SetValue(BindingDatasProperty, value);
            }
        }

        public static readonly DependencyProperty BindingDatasProperty = DependencyProperty.Register("BindingDatas", typeof(IList), typeof(BindableSliderScale));

        /// <summary>
        /// 自定义刻度面板
        /// </summary>
        public ItemsPanelTemplate AxisItemsPanel
        {
            get
            {
                return (ItemsPanelTemplate)GetValue(AxisItemsPanelProperty);
            }
            set
            {
                SetValue(AxisItemsPanelProperty, value);
            }
        }

        public static readonly DependencyProperty AxisItemsPanelProperty = DependencyProperty.Register("AxisItemsPanel", typeof(ItemsPanelTemplate), typeof(BindableSliderScale));

        /// <summary>
        /// 自定义刻度样式
        /// </summary>
        public Style ScaleStyle
        {
            get
            {
                return (Style)GetValue(ScaleStyleProperty);
            }
            set
            {
                SetValue(ScaleStyleProperty, value);
            }
        }

        public static readonly DependencyProperty ScaleStyleProperty = DependencyProperty.Register("ScaleStyle", typeof(Style), typeof(BindableSliderScale));

        /// <summary>
        /// 自定义刻度样式
        /// </summary>
        public Style LabelStyle
        {
            get
            {
                return (Style)GetValue(LabelStyleProperty);
            }
            set
            {
                SetValue(LabelStyleProperty, value);
            }
        }

        public static readonly DependencyProperty LabelStyleProperty = DependencyProperty.Register("LabelStyle", typeof(Style), typeof(BindableSliderScale));

        /// <summary>
        /// 频率
        /// </summary>
        public double MainTickValue
        {
            get
            {
                return (double)GetValue(MainTickValueProperty);
            }
            set
            {
                SetValue(MainTickValueProperty, value);
            }
        }

        public static readonly DependencyProperty MainTickValueProperty = DependencyProperty.Register("MainTickValue", typeof(double), typeof(BindableSliderScale), new PropertyMetadata(OnMainTickValueSourceChanged));

        /// <summary>
        /// Slider左侧RepeatButton颜色
        /// </summary>
        public Brush LeftRepeatButtonColor
        {
            get
            {
                return (Brush)GetValue(LeftRepeatButtonColorProperty);
            }
            set
            {
                SetValue(LeftRepeatButtonColorProperty, value);
            }
        }

        public static readonly DependencyProperty LeftRepeatButtonColorProperty = DependencyProperty.Register("LeftRepeatButtonColor", typeof(Brush), typeof(BindableSliderScale));

        /// <summary>
        ///  Slider右侧RepeatButton颜色
        /// </summary>
        public Brush RightRepeatButtonColor
        {
            get
            {
                return (Brush)GetValue(RightRepeatButtonColorProperty);
            }
            set
            {
                SetValue(RightRepeatButtonColorProperty, value);
            }
        }

        public static readonly DependencyProperty RightRepeatButtonColorProperty = DependencyProperty.Register("RightRepeatButtonColor", typeof(Brush), typeof(BindableSliderScale));

        /// <summary>
        /// 提示框背景颜色
        /// </summary>
        public Brush AutoToolTipBackGround
        {
            get
            {
                return (Brush)GetValue(AutoToolTipBackGroundProperty);
            }
            set
            {
                SetValue(AutoToolTipBackGroundProperty, value);
            }
        }

        public static readonly DependencyProperty AutoToolTipBackGroundProperty = DependencyProperty.Register("AutoToolTipBackGround", typeof(Brush), typeof(BindableSliderScale));

        /// <summary>
        /// 刻度是否显示数据，此属性为true时Minimum与Maximum属性无法更改
        /// </summary>
        public bool IsTickDisplayData
        {
            get { return (bool)GetValue(IsTickDisplayDataProperty); }
            set { SetValue(IsTickDisplayDataProperty, value); }
        }
        
        public static readonly DependencyProperty IsTickDisplayDataProperty = DependencyProperty.Register("IsTickDisplayData", typeof(bool), typeof(BindableSliderScale), new PropertyMetadata(OnIsTickDisplayDataSourceChanged));

        /// <summary>
        /// 刻度字符格式化
        /// </summary>
        public string TickDataFormat
        {
            get { return (string)GetValue(TickDataFormatProperty); }
            set { SetValue(TickDataFormatProperty, value); }
        }

        public static readonly DependencyProperty TickDataFormatProperty = DependencyProperty.Register("TickDataFormat", typeof(string), typeof(BindableSliderScale), new PropertyMetadata(OnTickDataFormatSourceChanged));

        public event PropertyChangedEventHandler PropertyChanged;

        private double currentWidth;

        #endregion
    }
}
