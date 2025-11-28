using Abt.Controls.SciChart;
using Abt.Controls.SciChart.Model.DataSeries;
using Abt.Controls.SciChart.Visuals;
using Abt.Controls.SciChart.Visuals.Axes;
using System.Windows;
using System.Windows.Media;

namespace Digihail.Controls
{
    [TemplatePart(Name = "PartSciChartSurface", Type=typeof(SciChartSurface))]
    /// <summary>
    /// 柱形图表
    /// </summary>
    public class FastColumnChart : ChartBase 
    {
        #region Constructor

        static FastColumnChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FastColumnChart), new FrameworkPropertyMetadata(typeof(FastColumnChart)));
        }

        public FastColumnChart() : base()
        {
            SetCurrentValue(UniformWidthProperty, true);
            SetCurrentValue(XAxisMajorGridLinesProperty, false);
            SetCurrentValue(YAxisMajorGridLinesProperty, false);
            SetCurrentValue(YGrowByRangeProperty, new DoubleRange(0.2, 0.1));
            SetCurrentValue(XGrowByRangeProperty, new DoubleRange(0.1, 0.1));
            SetCurrentValue(DataWidthProperty, 0.5);
            SetCurrentValue(DataOpacityProperty, 1.0);
            SetValue(XAutoRangeProperty, AutoRange.Always);
            SetValue(YAutoRangeProperty, AutoRange.Always);
            SetValue(XMaxAutoTicksProperty, 5);

            setXAxis = axis =>
            {
                axis.SetBinding(AxisBase.MaxAutoTicksProperty, ConfigureBinding("XMaxAutoTicks", this));
                axis.SetBinding(AxisBase.AutoTicksProperty, ConfigureBinding("XAutoTicks", this));
                axis.SetBinding(AxisBase.AutoRangeProperty, ConfigureBinding("XAutoRange", this));
                axis.SetBinding(AxisBase.VisibilityProperty, ConfigureBinding("XAxisVisibility", this));
                axis.SetBinding(AxisBase.GrowByProperty, ConfigureBinding("XGrowByRange", this));
                axis.SetBinding(AxisBase.DrawMinorGridLinesProperty, ConfigureBinding("XAxisMinorGridLines", this));
                axis.SetBinding(AxisBase.DrawMinorTicksProperty, ConfigureBinding("XDrawMinorTicks", this));
                axis.SetBinding(AxisBase.BackgroundProperty, ConfigureBinding("XAxisBackground", this));
                axis.SetBinding(AxisBase.DrawMajorBandsProperty, ConfigureBinding("XAxisMajorBands", this));
                axis.SetBinding(AxisBase.MajorDeltaProperty, ConfigureBinding("XAxisMajorDelta", this));
                axis.SetBinding(AxisBase.MinorDeltaProperty, ConfigureBinding("XAxisMinorDelta", this));
                axis.SetBinding(AxisBase.TickTextBrushProperty, ConfigureBinding("XTickTextBrush", this));
                axis.SetBinding(AxisBase.DrawMajorGridLinesProperty, ConfigureBinding("XAxisMajorGridLines", this));

                return axis;
            };

            setYAxis = axis =>
            {
                axis.SetBinding(AxisBase.AutoRangeProperty, ConfigureBinding("YAutoRange", this));
                axis.SetBinding(AxisBase.VisibilityProperty, ConfigureBinding("YAxisVisibility", this));
                axis.SetBinding(AxisBase.GrowByProperty, ConfigureBinding("YGrowByRange", this));
                axis.SetBinding(AxisBase.TickTextBrushProperty, ConfigureBinding("YTickTextBrush", this));
                axis.SetBinding(AxisBase.AxisAlignmentProperty, ConfigureBinding("YAxisAlignment", this));
                axis.SetBinding(AxisBase.BackgroundProperty, ConfigureBinding("YAxisBackground", this));
                axis.SetBinding(AxisBase.DrawMinorGridLinesProperty, ConfigureBinding("YAxisMinorGridLines", this));
                axis.SetBinding(AxisBase.DrawMajorBandsProperty, ConfigureBinding("YAxisMajorBands", this));
                axis.SetBinding(AxisBase.DrawMinorTicksProperty, ConfigureBinding("YDrawMinorTicks", this));
                axis.SetBinding(AxisBase.DrawMajorGridLinesProperty, ConfigureBinding("YAxisMajorGridLines", this));

                return axis;
            };
        }

        #endregion

        #region Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ChartSurface = (SciChartSurface)GetTemplateChild("PartSciChartSurface");
        }

        public static void OnDatasSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((FastColumnChart)o).OnDatasChanged(e.NewValue as IDataSeries, e.OldValue as IDataSeries);
        }

        private void OnDatasChanged(IDataSeries newDatas, IDataSeries oldDatas)
        {
            if (newDatas == null)
            {
                return;
            }

            if (oldDatas != null && oldDatas.XType == newDatas.XType && oldDatas.YType == newDatas.YType)
            {
                return;
            };

            SetAxes(newDatas);
        }

        #endregion

        #region Fields & Properties

        /// <summary>
        /// 图表数据
        /// </summary>
        public IDataSeries Datas
        {
            get
            {
                return (IDataSeries)GetValue(DatasProperty);
            }
            set
            {
                SetValue(DatasProperty, value);
            }
        }

        public static readonly DependencyProperty DatasProperty = DependencyProperty.Register("Datas", typeof(IDataSeries), typeof(FastColumnChart), new PropertyMetadata(OnDatasSourceChanged));

        public static readonly DependencyProperty DataOpacityProperty = DependencyProperty.Register("DataOpacity", typeof(double), typeof(FastColumnChart));

        /// <summary>
        /// 数据集透明度
        /// </summary>
        public double DataOpacity
        {
            get
            {
                return (double)GetValue(DataOpacityProperty);
            }
            set
            {
                SetValue(DataOpacityProperty, value);
            }
        }

        /// <summary>
        /// 是否使用统一宽度
        /// </summary>
        public bool UniformWidth
        {
            get
            {
                return (bool)GetValue(UniformWidthProperty);
            }
            set
            {
                SetValue(UniformWidthProperty, value);
            }
        }

        public static readonly DependencyProperty UniformWidthProperty = DependencyProperty.Register("UniformWidth", typeof(bool), typeof(FastColumnChart));

        public static readonly DependencyProperty DataBrushProperty = DependencyProperty.Register("DataBrush", typeof(Brush), typeof(FastColumnChart));

        /// <summary>
        /// 数据集画刷
        /// </summary>
        public Brush DataBrush
        {
            get
            {
                return (Brush)GetValue(DataBrushProperty);
            }
            set
            {
                SetValue(DataBrushProperty, value);
            }
        }

        public static readonly DependencyProperty DataWidthProperty = DependencyProperty.Register("DataWidth", typeof(double), typeof(FastColumnChart));

        /// <summary>
        /// 数据集宽度, 定义域(0.1,1)
        /// </summary>
        public double DataWidth
        {
            get
            {
                return (double)GetValue(DataWidthProperty);
            }
            set
            {
                SetValue(DataWidthProperty, value);
            }
        }

        #endregion
    }
}
