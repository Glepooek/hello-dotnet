using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Digihail.Controls
{
    /// <summary>
    /// 热点图刻度尺
    /// </summary>
    public class FastColorScaleBar : Control, INotifyPropertyChanged
    {
        #region Constructor

        static FastColorScaleBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FastColorScaleBar), new FrameworkPropertyMetadata(typeof(FastColorScaleBar)));
        }

        public FastColorScaleBar()
        {
            SetCurrentValue(MaxValueProperty, 100.0);
            SetCurrentValue(MinValueProperty, 0.0);
            SetCurrentValue(MainTickValueProperty, 0.0);
            SetCurrentValue(ScaleBarWidthProperty, 10.0);
            SetCurrentValue(ScaleBarHeightProperty, 10.0);

            // 默认竖向并将刻度显示在右侧
            SetCurrentValue(ScaleOrientationProperty, Orientation.Vertical); 
            SetCurrentValue(TickPlacementProperty, TickPlacement.BottomRight);
        }

        #endregion

        #region Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (ScaleOrientation == Orientation.Vertical)
            {
                scaleBody = (Grid)GetTemplateChild("VerticalScale");

                scaleBody.SizeChanged += (sender, e) =>
                {
                    if (e.NewSize.Height != e.PreviousSize.Height)
                    {
                        actualHeight = e.NewSize.Height;
                        SetScale();
                    }
                };
            }

            if (ScaleOrientation == Orientation.Horizontal)
            {
                scaleBody = (Grid)GetTemplateChild("HorizontalScale");

                scaleBody.SizeChanged += (sender, e) =>
                {
                    if (e.NewSize.Width != e.PreviousSize.Width)
                    {
                        actualWidth = e.NewSize.Width;
                        SetScale();
                    }
                };
            }
        }

        public static void OnMaxValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((FastColorScaleBar)o).SetScale();
        }

        public static void OnMinValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((FastColorScaleBar)o).SetScale();
        }

        public static void OnMainTickValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((FastColorScaleBar)o).SetScale();
        }

        public static void OnScaleOrientationChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((FastColorScaleBar)o).SetScale();
        }

        private void SetScale()
        {
            scaleValueList = new ObservableCollection<FastScale>();

            var temp = (int)((MaxValue - MinValue) / MainTickValue);
            var ticksCount = (MaxValue - MinValue) % MainTickValue == 0 ? temp + 1 : temp + 2;
            string value;
            double width;
            double height;
            HorizontalAlignment horizontalAlignment;
            HorizontalAlignment containerHorizontalAlignment;
            VerticalAlignment verticalAlignment;
            VerticalAlignment containerVerticalAlignment;

            if (ScaleOrientation == Orientation.Horizontal)
            {
                #region horizontal scale

                for (int i = 0; i < ticksCount; i++)
                {
                    if (i == 0)
                    {
                        width = actualWidth;
                        value = MinValue.ToString();
                        containerHorizontalAlignment = HorizontalAlignment.Left;
                        horizontalAlignment = HorizontalAlignment.Left;
                    }
                    else if (i == ticksCount - 1)
                    {
                        width = actualWidth;
                        value = MaxValue.ToString();
                        containerHorizontalAlignment = HorizontalAlignment.Right;
                        horizontalAlignment = HorizontalAlignment.Right;
                    }
                    else
                    {
                        value = ((MainTickValue * i) + MinValue).ToString();
                        horizontalAlignment = HorizontalAlignment.Center;

                        if (i <= (ticksCount - 1) / 2)
                        {
                            width = (actualWidth / (MaxValue - MinValue)) * MainTickValue * i * 2;
                            containerHorizontalAlignment = HorizontalAlignment.Left;
                        }
                        else
                        {
                            width = (actualWidth / (MaxValue - MinValue)) * ((MaxValue - MinValue) - MainTickValue * i) * 2;
                            containerHorizontalAlignment = HorizontalAlignment.Right;
                        }
                    }

                    scaleValueList.Add(new FastScale()
                    {
                        Label = value,
                        Size = width,
                        LabelHorizontalAlignment = horizontalAlignment,
                        ContainerHorizontalAlignment = containerHorizontalAlignment
                    });
                }

                #endregion
            }
            else if (ScaleOrientation == Orientation.Vertical)
            {
                #region vertical scale

                for (int i = 0; i < ticksCount; i++)
                {
                    if (i == 0)
                    {
                        value = MinValue.ToString();
                        height = actualHeight;
                        verticalAlignment = VerticalAlignment.Bottom;
                        containerVerticalAlignment = VerticalAlignment.Bottom;
                    }
                    else if (i == ticksCount - 1)
                    {
                        value = MaxValue.ToString();
                        height = actualHeight;
                        verticalAlignment = VerticalAlignment.Top;
                        containerVerticalAlignment = VerticalAlignment.Top;
                    }
                    else
                    {
                        value = ((MainTickValue * i) + MinValue).ToString();
                        verticalAlignment = VerticalAlignment.Center;

                        if (i <= (ticksCount - 1) / 2)
                        {
                            height = (actualHeight / (MaxValue - MinValue)) * MainTickValue * i * 2;
                            containerVerticalAlignment = VerticalAlignment.Bottom;
                        }
                        else
                        {
                            height = (actualHeight / (MaxValue - MinValue)) * ((MaxValue - MinValue) - MainTickValue * i) * 2;
                            containerVerticalAlignment = VerticalAlignment.Top;
                        }
                    }

                    scaleValueList.Add(new FastScale()
                    {
                        Label = value,
                        Size = height,
                        LabelVerticalAlignment = verticalAlignment,
                        ContainerVerticalAlignment = containerVerticalAlignment
                    });
                }

                #endregion
            }

            RaisePropertyChanged(() => ScaleValueList);
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

        #endregion

        #region Fields & Properties

        /// <summary>
        /// 自定义刻度面板
        /// </summary>
        public ItemsPanelTemplate CustomAxisItemsPanel
        {
            get
            {
                return (ItemsPanelTemplate)GetValue(CustomAxisItemsPanelProperty);
            }
            set
            {
                SetValue(CustomAxisItemsPanelProperty, value);
            }
        }

        public static readonly DependencyProperty CustomAxisItemsPanelProperty = DependencyProperty.Register("CustomAxisItemsPanel", typeof(ItemsPanelTemplate), typeof(FastColorScaleBar));

        /// <summary>
        /// 刻度尺高度
        /// </summary>
        public double ScaleBarHeight
        {
            get
            {
                return (double)GetValue(ScaleBarHeightProperty);
            }
            set
            {
                SetValue(ScaleBarHeightProperty, value);
            }
        }

        public static readonly DependencyProperty ScaleBarHeightProperty = DependencyProperty.Register("ScaleBarHeight", typeof(double), typeof(FastColorScaleBar));

        /// <summary>
        /// 刻度尺宽度
        /// </summary>
        public double ScaleBarWidth
        {
            get
            {
                return (double)GetValue(ScaleBarWidthProperty);
            }
            set
            {
                SetValue(ScaleBarWidthProperty, value);
            }
        }

        public static readonly DependencyProperty ScaleBarWidthProperty = DependencyProperty.Register("ScaleBarWidth", typeof(double), typeof(FastColorScaleBar));

        private ObservableCollection<FastScale> scaleValueList;
        public ObservableCollection<FastScale> ScaleValueList
        {
            get
            {
                return scaleValueList;
            }
        }

        /// <summary>
        /// 最大值
        /// </summary>
        public double MaxValue
        {
            get
            {
                return (double)GetValue(MaxValueProperty);
            }
            set
            {
                SetValue(MaxValueProperty, value);
            }
        }

        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(double), typeof(FastColorScaleBar), new PropertyMetadata(OnMaxValueChanged));

        /// <summary>
        /// 最小值
        /// </summary>
        public double MinValue
        {
            get
            {
                return (double)GetValue(MinValueProperty);
            }
            set
            {
                SetValue(MinValueProperty, value);
            }
        }

        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register("MinValue", typeof(double), typeof(FastColorScaleBar), new PropertyMetadata(OnMinValueChanged));

        /// <summary>
        /// 主刻度单位
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

        public static readonly DependencyProperty MainTickValueProperty = DependencyProperty.Register("MainTickValue", typeof(double), typeof(FastColorScaleBar), new PropertyMetadata(OnMainTickValueChanged));

        /// <summary>
        /// 刻度尺颜色
        /// </summary>
        public Brush ScaleColor
        {
            get
            {
                return (Brush)GetValue(ScaleColorProperty);
            }
            set
            {
                SetValue(ScaleColorProperty, value);
            }
        }

        public static readonly DependencyProperty ScaleColorProperty = DependencyProperty.Register("ScaleColor", typeof(Brush), typeof(FastColorScaleBar));

        /// <summary>
        /// 自定义刻度样式
        /// </summary>
        public Style CustomScaleStyle
        { 
            get
            {
                return (Style)GetValue(CustomScaleStyleProperty);
            }
            set
            {
                SetValue(CustomScaleStyleProperty, value);
            }
        }

        public static readonly DependencyProperty CustomScaleStyleProperty = DependencyProperty.Register("CustomScaleStyle", typeof(Style), typeof(FastColorScaleBar));

        /// <summary>
        /// 自定义刻度标记样式
        /// </summary>
        public Style CustomLabelStyle
        {
            get
            {
                return (Style)GetValue(CustomLabelStyleProperty);
            }
            set
            {
                SetValue(CustomLabelStyleProperty, value);
            }
        }

        public static readonly DependencyProperty CustomLabelStyleProperty = DependencyProperty.Register("CustomLabelStyle", typeof(Style), typeof(FastColorScaleBar));

        /// <summary>
        /// 刻度尺方向
        /// </summary>
        public Orientation ScaleOrientation
        {
            get
            {
                return (Orientation)GetValue(ScaleOrientationProperty);
            }
            set
            {
                SetValue(ScaleOrientationProperty, value);
            }
        }

        public static readonly DependencyProperty ScaleOrientationProperty = DependencyProperty.Register("ScaleOrientation", typeof(Orientation), typeof(FastColorScaleBar), new PropertyMetadata(OnScaleOrientationChanged));

        /// <summary>
        /// 刻度位置
        /// </summary>
        public TickPlacement TickPlacement
        {
            get
            {
                return (TickPlacement)GetValue(TickPlacementProperty);
            }
            set
            {
                SetValue(TickPlacementProperty, value);
            }
        }

        public static readonly DependencyProperty TickPlacementProperty = DependencyProperty.Register("TickPlacement", typeof(TickPlacement), typeof(FastColorScaleBar));

        public event PropertyChangedEventHandler PropertyChanged;

        private Grid scaleBody;

        private double actualHeight;

        private double actualWidth;

        #endregion
    }
}
