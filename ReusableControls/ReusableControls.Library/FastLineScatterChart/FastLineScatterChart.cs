using Abt.Controls.SciChart;
using Abt.Controls.SciChart.Model.DataSeries;
using Abt.Controls.SciChart.Visuals;
using Abt.Controls.SciChart.Visuals.Axes;
using Abt.Controls.SciChart.Visuals.PointMarkers;
using System.Windows;
using System.Windows.Media;

namespace Digihail.Controls
{
    [TemplatePart(Name = "PartSciChartSurface", Type = typeof(SciChartSurface))]
    /// <summary>
    /// 线散点图
    /// </summary>
    public class FastLineScatterChart : ChartBase
    {
        #region Constructor

        static FastLineScatterChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FastLineScatterChart), new FrameworkPropertyMetadata(typeof(FastLineScatterChart)));
        }

        public FastLineScatterChart()
        {
            SetValue(YGrowByRangeProperty, new DoubleRange(0, 0.05));
            SetCurrentValue(LineDatasColorProperty, Color.FromRgb(0xAE, 0x3E, 0x34));
            SetValue(XAxisMajorGridLinesProperty, false);
            SetValue(XAxisMinorGridLinesProperty, false);
            SetValue(YAxisMajorGridLinesProperty, false);
            SetValue(YAxisMinorGridLinesProperty, false);
            SetValue(YAutoRangeProperty, AutoRange.Always);
            SetValue(XAutoRangeProperty, AutoRange.Always);

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

        public static void OnLineDatasSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((FastLineScatterChart)o).OnLineDatasChanged(e.NewValue as IDataSeries, e.OldValue as IDataSeries);
        }

        private void OnLineDatasChanged(IDataSeries newDatas, IDataSeries oldDatas)
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

        public Color LineDatasColor
        {
            get { return (Color)GetValue(LineDatasColorProperty); }
            set { SetValue(LineDatasColorProperty, value); }
        }
        
        public static readonly DependencyProperty LineDatasColorProperty = DependencyProperty.Register("LineDatasColor", typeof(Color), typeof(FastLineScatterChart));

        public IPointMarker LineDatasPointMarker
        {
            get { return (IPointMarker)GetValue(LineDatasPointMarkerProperty); }
            set { SetValue(LineDatasPointMarkerProperty, value); }
        }
        
        public static readonly DependencyProperty LineDatasPointMarkerProperty = DependencyProperty.Register("LineDatasPointMarker", typeof(IPointMarker), typeof(FastLineScatterChart));

        public IDataSeries LineDatas
        {
            get { return (IDataSeries)GetValue(LineDatasProperty); }
            set { SetValue(LineDatasProperty, value); }
        }

        public static readonly DependencyProperty LineDatasProperty = DependencyProperty.Register("LineDatas", typeof(IDataSeries), typeof(FastLineScatterChart), new PropertyMetadata(OnLineDatasSourceChanged));

        public IDataSeries ScatterDatas
        {
            get { return (IDataSeries)GetValue(ScatterDatasProperty); }
            set { SetValue(ScatterDatasProperty, value); }
        }
        
        public static readonly DependencyProperty ScatterDatasProperty =
            DependencyProperty.Register("ScatterDatas", typeof(IDataSeries), typeof(FastLineScatterChart));

        public IPointMarker ScatterDatasPointMarker
        {
            get { return (IPointMarker)GetValue(ScatterDatasPointMarkerProperty); }
            set { SetValue(ScatterDatasPointMarkerProperty, value); }
        }

        public static readonly DependencyProperty ScatterDatasPointMarkerProperty =
            DependencyProperty.Register("ScatterDatasPointMarker", typeof(IPointMarker), typeof(FastLineScatterChart));


        #endregion
    }
}
