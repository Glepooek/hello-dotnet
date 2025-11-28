using Abt.Controls.SciChart;
using Abt.Controls.SciChart.Model.DataSeries;
using Abt.Controls.SciChart.Visuals;
using Abt.Controls.SciChart.Visuals.Axes;
using System.Windows;
using System.Windows.Media;

namespace Digihail.Controls
{
    [TemplatePart(Name = "PartSciChartSurface", Type = typeof(SciChartSurface))]
    /// <summary>
    /// 面积图
    /// </summary>
    public class FastMountainChart : ChartBase
    {
        #region Constructor

        static FastMountainChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FastMountainChart), new FrameworkPropertyMetadata(typeof(FastMountainChart)));
        }

        public FastMountainChart()
        {
            SetValue(XAutoRangeProperty, AutoRange.Always);
            SetValue(YAutoRangeProperty, AutoRange.Always);
            SetValue(YGrowByRangeProperty, new DoubleRange(0, 0.1));

            setXAxis = axis =>
            {
                axis.SetBinding(AxisBase.AutoRangeProperty, ConfigureBinding("XAutoRange", this));
                axis.SetBinding(AxisBase.VisibilityProperty, ConfigureBinding("XAxisVisibility", this));
                axis.SetBinding(AxisBase.GrowByProperty, ConfigureBinding("XGrowByRange", this));
                axis.SetBinding(AxisBase.DrawMinorGridLinesProperty, ConfigureBinding("XAxisMinorGridLines", this));
                axis.SetBinding(AxisBase.DrawMajorGridLinesProperty, ConfigureBinding("XAxisMajorGridLines", this));
                axis.SetBinding(AxisBase.DrawMinorTicksProperty, ConfigureBinding("XDrawMinorTicks", this));
                axis.SetBinding(AxisBase.BackgroundProperty, ConfigureBinding("XAxisBackground", this));
                axis.SetBinding(AxisBase.DrawMajorBandsProperty, ConfigureBinding("XAxisMajorBands", this));
                axis.SetBinding(AxisBase.MajorDeltaProperty, ConfigureBinding("XAxisMajorDelta", this));
                axis.SetBinding(AxisBase.MinorDeltaProperty, ConfigureBinding("XAxisMinorDelta", this));
                axis.SetBinding(AxisBase.TickTextBrushProperty, ConfigureBinding("XTickTextBrush", this));
                axis.SetBinding(AxisBase.TextFormattingProperty, ConfigureBinding("XAxisTextFormat", this));

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
            ((FastMountainChart)o).OnDatasChanged(e.NewValue as IDataSeries, e.OldValue as IDataSeries);
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

        public static readonly DependencyProperty DatasProperty = DependencyProperty.Register("Datas", typeof(IDataSeries), typeof(FastMountainChart), new PropertyMetadata(OnDatasSourceChanged));

        /// <summary>
        /// 数据集颜色
        /// </summary>
        public Color PeakColor
        {
            get
            {
                return (Color)GetValue(PeakColorProperty);
            }
            set
            {
                SetValue(PeakColorProperty, value);
            }
        }

        public static readonly DependencyProperty PeakColorProperty = DependencyProperty.Register("PeakColor", typeof(Color), typeof(FastMountainChart));

        /// <summary>
        /// 面积画刷
        /// </summary>
        public Brush MountainBrush
        {
            get
            {
                return (Brush)GetValue(MountainBrushProperty);
            }
            set
            {
                SetValue(MountainBrushProperty, value);
            }
        }

        public static readonly DependencyProperty MountainBrushProperty = DependencyProperty.Register("MountainBrush", typeof(Brush), typeof(FastMountainChart));

        #endregion
    }
}
