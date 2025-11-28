using Abt.Controls.SciChart;
using Abt.Controls.SciChart.Common.Databinding;
using Abt.Controls.SciChart.Model.DataSeries;
using Abt.Controls.SciChart.Visuals;
using Abt.Controls.SciChart.Visuals.Axes;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Digihail.Controls
{
    /// <summary>
    /// 图表基类
    /// </summary>
    public abstract class ChartBase : Control, INotifyPropertyChanged
    {
        #region Constructor

        public ChartBase()
        {
            SetCurrentValue(XFlipCoordinatesProperty, false);
            SetCurrentValue(YFlipCoordinatesProperty, false);
            SetCurrentValue(XAutoTicksProperty, true);
            SetCurrentValue(YAutoTicksProperty, true);
            SetCurrentValue(ChartBorderBrushProperty, new SolidColorBrush(Color.FromRgb(0xA1, 0xA1, 0xA3)));
            SetCurrentValue(XAutoRangeProperty, AutoRange.Once);
            SetCurrentValue(YAutoRangeProperty, AutoRange.Once);
            SetCurrentValue(ChartBorderThicknessProperty, new Thickness(1, 0, 0, 1));
            SetCurrentValue(XLabelAlignmentProperty, AxisLabelAlignment.TickMark);
            SetCurrentValue(YLabelAlignmentProperty, AxisLabelAlignment.TickMark);
            SetCurrentValue(SurfaceBackgroundProperty, new SolidColorBrush(Colors.Transparent));
            SetCurrentValue(XAxisBackgroundProperty, new SolidColorBrush(Colors.Transparent));
            SetCurrentValue(YAxisBackgroundProperty, new SolidColorBrush(Colors.Transparent));
            SetCurrentValue(XAxisVisibilityProperty, Visibility.Visible);
            SetCurrentValue(XAxisMinorGridLinesProperty, false);
            SetCurrentValue(XDrawMinorTicksProperty, false);
            SetCurrentValue(XTickTextBrushProperty, new SolidColorBrush(Color.FromRgb(0x9F, 0x9F, 0xA3)));
            SetCurrentValue(XAxisMajorGridLinesProperty, true);
            SetCurrentValue(YAxisVisibilityProperty, Visibility.Visible);
            SetCurrentValue(YTickTextBrushProperty, new SolidColorBrush(Color.FromRgb(0x9F, 0x9F, 0xA3)));
            SetCurrentValue(YAxisMinorGridLinesProperty, false);
            SetCurrentValue(YAxisMajorBandsProperty, true);
            SetCurrentValue(YDrawMinorTicksProperty, false);
            SetCurrentValue(YAxisMajorGridLinesProperty, true);
            SetCurrentValue(YAxisAlignmentProperty, AxisAlignment.Left);

            Loaded += (sender, e) =>
            {
                if (ChartSurface != null)
                {
                    if (ChartSurface.XAxis != null && XAxisVisibleRange != null)
                    {
                        ChartSurface.XAxis.VisibleRange = XAxisVisibleRange;
                    }

                    if (ChartSurface.YAxis != null && YAxisVisibleRange != null)
                    {
                        ChartSurface.YAxis.VisibleRange = YAxisVisibleRange;
                    }
                }
            };
        }

        #endregion

        #region Methods

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

        /// <summary>
        /// 设置绑定
        /// </summary>
        /// <param name="path">绑定路径</param>
        /// <param name="source">源</param>
        /// <returns>设置完毕的绑定</returns>
        public Binding ConfigureBinding(string path, object source, BindingMode mode = BindingMode.Default)
        {
            var binding = new Binding(path);
            binding.Source = source;
            binding.Mode = mode;

            return binding;
        }

        public static void OnXAxisVisibleRangeSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((ChartBase)o).OnXAxisVisibleRangeChanged();
        }

        private void OnXAxisVisibleRangeChanged()
        {
            if(IsLoaded && ChartSurface != null && ChartSurface.XAxis != null)
            {
                ChartSurface.XAxis.VisibleRange = XAxisVisibleRange;
            }
        }

        public static void OnYAxisVisibleRangeSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((ChartBase)o).OnYAxisVisibleRangeChanged();
        }

        private void OnYAxisVisibleRangeChanged()
        {
            if (IsLoaded && ChartSurface != null && ChartSurface.YAxis != null)
            {
                ChartSurface.YAxis.VisibleRange = YAxisVisibleRange;
            }
        }

        /// <summary>
        /// 设置坐标轴，暂且仅考虑DateTimeAxis与NumericAxis两种坐标轴类型
        /// </summary>
        public void SetAxes(IDataSeries datas)
        {
            if (setXAxis == null || setYAxis == null)
            {
                return;
            }

            if (datas.XType.IsAssignableFrom(typeof(DateTime)) && datas.YType.IsAssignableFrom(typeof(DateTime)))
            {
                chartXAxis = setXAxis(new DateTimeAxis());
                chartYAxis = setYAxis(new DateTimeAxis());
            }
            else if (datas.XType.IsAssignableFrom(typeof(DateTime)))
            {
                chartXAxis = setXAxis(new DateTimeAxis());
                chartYAxis = setYAxis(new NumericAxis());
            }
            else if (datas.YType.IsAssignableFrom(typeof(DateTime)))
            {
                chartXAxis = setXAxis(new NumericAxis());
                chartYAxis = setYAxis(new DateTimeAxis());
            }
            else
            {
                chartXAxis = setXAxis(new NumericAxis());
                chartYAxis = setYAxis(new NumericAxis());
            }

            if (ChartSurface != null)
            {
                if (XAxisVisibleRange != null)
                {
                    chartXAxis.VisibleRange = XAxisVisibleRange;
                }

                if (YAxisVisibleRange != null)
                {
                    chartYAxis.VisibleRange = YAxisVisibleRange;
                }
            }

            RaisePropertyChanged(() => ChartXAxis);
            RaisePropertyChanged(() => ChartYAxis);
        }

        #endregion

        #region Fields & Properties

        /// <summary>
        /// 是否定制MajorTickLineStyle样式
        /// </summary>
        public bool IsCustomMajorTickLineStyle
        {
            get { return (bool)GetValue(IsCustomMajorTickLineStyleProperty); }
            set { SetValue(IsCustomMajorTickLineStyleProperty, value); }
        }

        public static readonly DependencyProperty IsCustomMajorTickLineStyleProperty = DependencyProperty.Register("IsCustomMajorTickLineStyle", typeof(bool), typeof(ChartBase), new PropertyMetadata(false));

        /// <summary>
        /// 横坐标可视数据范围限制
        /// </summary>
        public IRange XRangeLimit
        {
            get { return (IRange)GetValue(XRangeLimitProperty); }
            set { SetValue(XRangeLimitProperty, value); }
        }

        public static readonly DependencyProperty XRangeLimitProperty = DependencyProperty.Register("XRangeLimit", typeof(IRange), typeof(ChartBase));

        /// <summary>
        /// 纵坐标可视数据范围限制
        /// </summary>
        public IRange YRangeLimit
        {
            get { return (IRange)GetValue(YRangeLimitProperty); }
            set { SetValue(YRangeLimitProperty, value); }
        }

        public static readonly DependencyProperty YRangeLimitProperty = DependencyProperty.Register("YRangeLimit", typeof(IRange), typeof(ChartBase));

        /// <summary>
        /// 横坐标是否翻转显示
        /// </summary>
        public bool XFlipCoordinates
        {
            get { return (bool)GetValue(XFlipCoordinatesProperty); }
            set { SetValue(XFlipCoordinatesProperty, value); }
        }

        public static readonly DependencyProperty XFlipCoordinatesProperty = DependencyProperty.Register("XFlipCoordinates", typeof(bool), typeof(ChartBase));

        /// <summary>
        /// 纵坐标是否翻转显示
        /// </summary>
        public bool YFlipCoordinates
        {
            get { return (bool)GetValue(YFlipCoordinatesProperty); }
            set { SetValue(YFlipCoordinatesProperty, value); }
        }

        public static readonly DependencyProperty YFlipCoordinatesProperty = DependencyProperty.Register("YFlipCoordinates", typeof(bool), typeof(ChartBase));


        /// <summary>
        /// 横坐标最大刻度数量
        /// </summary>
        public int XMaxAutoTicks
        {
            get { return (int)GetValue(XMaxAutoTicksProperty); }
            set { SetValue(XMaxAutoTicksProperty, value); }
        }

        public static readonly DependencyProperty XMaxAutoTicksProperty = DependencyProperty.Register("XMaxAutoTicks", typeof(int), typeof(ChartBase));

        /// <summary>
        /// 纵坐标最大刻度数量
        /// </summary>
        public int YMaxAutoTicks
        {
            get { return (int)GetValue(YMaxAutoTicksProperty); }
            set { SetValue(YMaxAutoTicksProperty, value); }
        }

        public static readonly DependencyProperty YMaxAutoTicksProperty = DependencyProperty.Register("YMaxAutoTicks", typeof(int), typeof(ChartBase));

        /// <summary>
        /// 是否自动绘制横坐标
        /// </summary>
        public bool XAutoTicks
        {
            get { return (bool)GetValue(XAutoTicksProperty); }
            set { SetValue(XAutoTicksProperty, value); }
        }
        
        public static readonly DependencyProperty XAutoTicksProperty = DependencyProperty.Register("XAutoTicks", typeof(bool), typeof(ChartBase));

        /// <summary>
        /// 是否自动绘制纵坐标
        /// </summary>
        public bool YAutoTicks
        {
            get { return (bool)GetValue(YAutoTicksProperty); }
            set { SetValue(YAutoTicksProperty, value); }
        }

        public static readonly DependencyProperty YAutoTicksProperty = DependencyProperty.Register("YAutoTicks", typeof(bool), typeof(ChartBase));

        /// <summary>
        /// 横坐标自动区间选项
        /// </summary>
        public AutoRange XAutoRange
        {
            get
            {
                return (AutoRange)GetValue(XAutoRangeProperty);
            }
            set
            {
                SetValue(XAutoRangeProperty, value);
            }
        }

        public static readonly DependencyProperty XAutoRangeProperty = DependencyProperty.Register("XAutoRange", typeof(AutoRange), typeof(ChartBase));

        /// <summary>
        /// 纵坐标自动区间选项
        /// </summary>
        public AutoRange YAutoRange
        {
            get
            {
                return (AutoRange)GetValue(YAutoRangeProperty);
            }
            set
            {
                SetValue(YAutoRangeProperty, value);
            }
        }

        public static readonly DependencyProperty YAutoRangeProperty = DependencyProperty.Register("YAutoRange", typeof(AutoRange), typeof(ChartBase));

        /// <summary>
        /// 横坐标数据可视范围
        /// </summary>
        public IRange XAxisVisibleRange
        {
            get
            {
                return (IRange)GetValue(XAxisVisibleRangeProperty);
            }
            set
            {
                SetValue(XAxisVisibleRangeProperty, value);
            }
        }

        public static readonly DependencyProperty XAxisVisibleRangeProperty = DependencyProperty.Register("XAxisVisibleRange", typeof(IRange), typeof(ChartBase), new PropertyMetadata(OnXAxisVisibleRangeSourceChanged));

        /// <summary>
        /// 横坐标数据可视范围
        /// </summary>
        public IRange YAxisVisibleRange
        {
            get
            {
                return (IRange)GetValue(YAxisVisibleRangeProperty);
            }
            set
            {
                SetValue(YAxisVisibleRangeProperty, value);
            }
        }

        public static readonly DependencyProperty YAxisVisibleRangeProperty = DependencyProperty.Register("YAxisVisibleRange", typeof(IRange), typeof(ChartBase), new PropertyMetadata(OnYAxisVisibleRangeSourceChanged));

        /// <summary>
        /// ScriChart，通过此属性可对表进行更深入定制
        /// </summary>
        public SciChartSurface ChartSurface
        {
            get
            {
                return (SciChartSurface)GetValue(ChartSurfaceProperty);
            }
            set
            {
                SetValue(ChartSurfaceProperty, value);
            }
        }

        public static readonly DependencyProperty ChartSurfaceProperty = DependencyProperty.Register("ChartSurface", typeof(SciChartSurface), typeof(ChartBase));

        /// <summary>
        /// 图表框架样式
        /// </summary>
        public Thickness ChartBorderThickness
        {
            get
            {
                return (Thickness)GetValue(ChartBorderThicknessProperty);
            }
            set
            {
                SetValue(ChartBorderThicknessProperty, value);
            }
        }

        public static readonly DependencyProperty ChartBorderThicknessProperty = DependencyProperty.Register("ChartBorderThickness", typeof(Thickness), typeof(ChartBase));

        /// <summary>
        /// 图表边框画刷
        /// </summary>
        public Brush ChartBorderBrush
        {
            get
            {
                return (Brush)GetValue(ChartBorderBrushProperty);
            }
            set
            {
                SetValue(ChartBorderBrushProperty, value);
            }
        }

        public static readonly DependencyProperty ChartBorderBrushProperty = DependencyProperty.Register("ChartBorderBrush", typeof(Brush), typeof(ChartBase));

        public static readonly DependencyProperty XLabelAlignmentProperty = DependencyProperty.Register("XLabelAlignment", typeof(AxisLabelAlignment), typeof(ChartBase));

        /// <summary>
        /// 横坐标标签对齐方式
        /// </summary>
        public AxisLabelAlignment XLabelAlignment
        {
            get
            {
                return (AxisLabelAlignment)GetValue(XLabelAlignmentProperty);
            }
            set
            {
                SetValue(XLabelAlignmentProperty, value);
            }
        }

        public static readonly DependencyProperty YLabelAlignmentProperty = DependencyProperty.Register("YLabelAlignment", typeof(AxisLabelAlignment), typeof(ChartBase));

        /// <summary>
        /// 竖坐标标签对齐方式
        /// </summary>
        public AxisLabelAlignment YLabelAlignment
        {
            get
            {
                return (AxisLabelAlignment)GetValue(YLabelAlignmentProperty);
            }
            set
            {
                SetValue(YLabelAlignmentProperty, value);
            }
        }

        public static readonly DependencyProperty SurfaceBackgroundProperty = DependencyProperty.Register("SurfaceBackground", typeof(Brush), typeof(ChartBase));

        /// <summary>
        /// 图表背景
        /// </summary>
        public Brush SurfaceBackground
        {
            get
            {
                return (Brush)GetValue(SurfaceBackgroundProperty);
            }
            set
            {
                SetValue(SurfaceBackgroundProperty, value);
            }
        }

        public static readonly DependencyProperty XAxisBackgroundProperty = DependencyProperty.Register("XAxisBackground", typeof(Brush), typeof(ChartBase));

        /// <summary>
        /// 横向背景
        /// </summary>
        public Brush XAxisBackground
        {
            get
            {
                return (Brush)GetValue(XAxisBackgroundProperty);
            }
            set
            {
                SetValue(XAxisBackgroundProperty, value);
            }
        }

        public static readonly DependencyProperty YAxisBackgroundProperty = DependencyProperty.Register("YAxisBackground", typeof(Brush), typeof(ChartBase));

        /// <summary>
        /// 竖向背景
        /// </summary>
        public Brush YAxisBackground
        {
            get
            {
                return (Brush)GetValue(YAxisBackgroundProperty);
            }
            set
            {
                SetValue(YAxisBackgroundProperty, value);
            }
        }

        /// <summary>
        /// 横坐标显示方式
        /// </summary>
        public Visibility XAxisVisibility
        {
            get
            {
                return (Visibility)GetValue(XAxisVisibilityProperty);
            }
            set
            {
                SetValue(XAxisVisibilityProperty, value);
            }
        }

        public static readonly DependencyProperty XAxisVisibilityProperty = DependencyProperty.Register("XAxisVisibility", typeof(Visibility), typeof(ChartBase));

        public static readonly DependencyProperty XAxisMajorGridLinesProperty = DependencyProperty.Register("XAxisMajorGridLines", typeof(bool), typeof(ChartBase));

        /// <summary>
        /// 横坐标MajorGridLines是否显示
        /// </summary>
        public bool XAxisMajorGridLines
        {
            get
            {
                return (bool)GetValue(XAxisMajorGridLinesProperty);
            }
            set
            {
                SetValue(XAxisMajorGridLinesProperty, value);
            }
        }

        public static readonly DependencyProperty XAxisMinorGridLinesProperty = DependencyProperty.Register("XAxisMinorGridLines", typeof(bool), typeof(ChartBase));

        /// <summary>
        /// 横坐标MinorGridLines是否显示
        /// </summary>
        public bool XAxisMinorGridLines
        {
            get
            {
                return (bool)GetValue(XAxisMinorGridLinesProperty);
            }
            set
            {
                SetValue(XAxisMinorGridLinesProperty, value);
            }
        }

        public static readonly DependencyProperty XAxisMajorBandsProperty = DependencyProperty.Register("XAxisMajorBands", typeof(bool), typeof(ChartBase));

        /// <summary>
        /// 横坐标MajorBands是否显示
        /// </summary>
        public bool XAxisMajorBands
        {
            get
            {
                return (bool)GetValue(XAxisMajorBandsProperty);
            }
            set
            {
                SetValue(XAxisMajorBandsProperty, value);
            }
        }

        public static readonly DependencyProperty XAxisMajorDeltaProperty = DependencyProperty.Register("XAxisMajorDelta", typeof(double), typeof(ChartBase));

        /// <summary>
        /// 横坐标主刻度单位
        /// </summary>
        public double XAxisMajorDelta
        {
            get
            {
                return (double)GetValue(XAxisMajorDeltaProperty);
            }
            set
            {
                SetValue(XAxisMajorDeltaProperty, value);
            }
        }

        public static readonly DependencyProperty XAxisMinorDeltaProperty = DependencyProperty.Register("XAxisMinorDelta", typeof(double), typeof(ChartBase));

        /// <summary>
        /// 横坐标副刻度单位
        /// </summary>
        public double XAxisMinorDelta
        {
            get
            {
                return (double)GetValue(XAxisMinorDeltaProperty);
            }
            set
            {
                SetValue(XAxisMinorDeltaProperty, value);
            }
        }

        public static readonly DependencyProperty XTickTextBrushProperty = DependencyProperty.Register("XTickTextBrush", typeof(Brush), typeof(ChartBase));

        /// <summary>
        /// 横坐标刻度画刷
        /// </summary>
        public Brush XTickTextBrush
        {
            get
            {
                return (Brush)GetValue(XTickTextBrushProperty);
            }
            set
            {
                SetValue(XTickTextBrushProperty, value);
            }
        }

        public static readonly DependencyProperty XGrowByRangeProperty = DependencyProperty.Register("XGrowByRange", typeof(IRange<double>), typeof(ChartBase));

        [TypeConverter(typeof(StringToDoubleRangeTypeConverter))]
        /// <summary>
        /// 纵坐标数据刻度富裕程度
        /// </summary>
        public IRange<double> XGrowByRange
        {
            get
            {
                return (IRange<double>)GetValue(XGrowByRangeProperty);
            }
            set
            {
                SetValue(XGrowByRangeProperty, value);
            }
        }

        public static readonly DependencyProperty XDrawMinorTicksProperty = DependencyProperty.Register("XDrawMinorTicks", typeof(bool), typeof(ChartBase));

        /// <summary>
        /// 横坐标副刻度是否显示
        /// </summary>
        public bool XDrawMinorTicks
        {
            get
            {
                return (bool)GetValue(XDrawMinorTicksProperty);
            }
            set
            {
                SetValue(XDrawMinorTicksProperty, value);
            }
        }

        /// <summary>
        /// 纵坐标显示方式
        /// </summary>
        public Visibility YAxisVisibility
        {
            get
            {
                return (Visibility)GetValue(YAxisVisibilityProperty);
            }
            set
            {
                SetValue(YAxisVisibilityProperty, value);
            }
        }

        public static readonly DependencyProperty YAxisVisibilityProperty = DependencyProperty.Register("YAxisVisibility", typeof(Visibility), typeof(ChartBase));

        public static readonly DependencyProperty YAxisMajorGridLinesProperty = DependencyProperty.Register("YAxisMajorGridLines", typeof(bool), typeof(ChartBase));

        /// <summary>
        /// 纵坐标MajorGridLines是否显示
        /// </summary>
        public bool YAxisMajorGridLines
        {
            get
            {
                return (bool)GetValue(YAxisMajorGridLinesProperty);
            }
            set
            {
                SetValue(YAxisMajorGridLinesProperty, value);
            }
        }

        public static readonly DependencyProperty YAxisMinorGridLinesProperty = DependencyProperty.Register("YAxisMinorGridLines", typeof(bool), typeof(ChartBase));

        /// <summary>
        /// 纵坐标MinorGridLines是否显示
        /// </summary>
        public bool YAxisMinorGridLines
        {
            get
            {
                return (bool)GetValue(YAxisMinorGridLinesProperty);
            }
            set
            {
                SetValue(YAxisMinorGridLinesProperty, value);
            }
        }

        public static readonly DependencyProperty YAxisMajorBandsProperty = DependencyProperty.Register("YAxisMajorBands", typeof(bool), typeof(ChartBase));

        /// <summary>
        /// 纵坐标MajorBands是否显示
        /// </summary>
        public bool YAxisMajorBands
        {
            get
            {
                return (bool)GetValue(YAxisMajorBandsProperty);
            }
            set
            {
                SetValue(YAxisMajorBandsProperty, value);
            }
        }

        public static readonly DependencyProperty YDrawMinorTicksProperty = DependencyProperty.Register("YDrawMinorTicks", typeof(bool), typeof(ChartBase));

        /// <summary>
        /// 纵坐标副刻度是否显示
        /// </summary>
        public bool YDrawMinorTicks
        {
            get
            {
                return (bool)GetValue(YDrawMinorTicksProperty);
            }
            set
            {
                SetValue(YDrawMinorTicksProperty, value);
            }
        }

        public static readonly DependencyProperty YAxisAlignmentProperty = DependencyProperty.Register("YAxisAlignment", typeof(AxisAlignment), typeof(ChartBase));

        /// <summary>
        /// 纵坐标对齐方式
        /// </summary>
        public AxisAlignment YAxisAlignment
        {
            get
            {
                return (AxisAlignment)GetValue(YAxisAlignmentProperty);
            }
            set
            {
                SetValue(YAxisAlignmentProperty, value);
            }
        }

        public static readonly DependencyProperty YTickTextBrushProperty = DependencyProperty.Register("YTickTextBrush", typeof(Brush), typeof(ChartBase));

        /// <summary>
        /// 纵坐标刻度画刷
        /// </summary>
        public Brush YTickTextBrush
        {
            get
            {
                return (Brush)GetValue(YTickTextBrushProperty);
            }
            set
            {
                SetValue(YTickTextBrushProperty, value);
            }
        }

        public static readonly DependencyProperty YGrowByRangeProperty = DependencyProperty.Register("YGrowByRange", typeof(IRange<double>), typeof(ChartBase));

        [TypeConverter(typeof(StringToDoubleRangeTypeConverter))]
        /// <summary>
        /// 纵坐标数据刻度富裕程度
        /// </summary>
        public IRange<double> YGrowByRange
        {
            get
            {
                return (IRange<double>)GetValue(YGrowByRangeProperty);
            }
            set
            {
                SetValue(YGrowByRangeProperty, value);
            }
        }

        public static readonly DependencyProperty YAxisMajorDeltaProperty = DependencyProperty.Register("YAxisMajorDelta", typeof(double), typeof(ChartBase));

        /// <summary>
        /// 纵坐标主刻度单位
        /// </summary>
        public double YAxisMajorDelta
        {
            get
            {
                return (double)GetValue(YAxisMajorDeltaProperty);
            }
            set
            {
                SetValue(YAxisMajorDeltaProperty, value);
            }
        }

        public static readonly DependencyProperty YAxisMinorDeltaProperty = DependencyProperty.Register("YAxisMinorDelta", typeof(double), typeof(ChartBase));

        /// <summary>
        /// 纵坐标副刻度单位
        /// </summary>
        public double YAxisMinorDelta
        {
            get
            {
                return (double)GetValue(YAxisMinorDeltaProperty);
            }
            set
            {
                SetValue(YAxisMinorDeltaProperty, value);
            }
        }

        protected IAxis chartXAxis;
        public IAxis ChartXAxis
        {
            get
            {
                return chartXAxis;
            }
        }

        protected AxisBase chartYAxis;
        public AxisBase ChartYAxis
        {
            get
            {
                return chartYAxis;
            }
        }

        protected Func<AxisBase, AxisBase> setXAxis;

        protected Func<AxisBase, AxisBase> setYAxis;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
