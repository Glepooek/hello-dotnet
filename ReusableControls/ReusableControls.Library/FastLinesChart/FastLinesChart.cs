using Abt.Controls.SciChart;
using Abt.Controls.SciChart.Visuals;
using Abt.Controls.SciChart.Visuals.Axes;
using Abt.Controls.SciChart.Visuals.RenderableSeries;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;

namespace Digihail.Controls
{
    [TemplatePart(Name = "PartSciChartSurface", Type = typeof(SciChartSurface))]
    /// <summary>
    /// 多重线图
    /// </summary>
    public class FastLinesChart : ChartBase
    {
        #region Constructor

        static FastLinesChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FastLinesChart), new FrameworkPropertyMetadata(typeof(FastLinesChart)));
        }

        public FastLinesChart()
        {
            SetCurrentValue(LineCollectionProperty, new ObservableCollection<LineSeries>());
            SetCurrentValue(YGrowByRangeProperty, new DoubleRange(0.2, 0.2));
            SetCurrentValue(XAxisMajorGridLinesProperty, true);
            SetCurrentValue(XAxisMinorGridLinesProperty, false);
            SetCurrentValue(YAxisMajorGridLinesProperty, false);
            SetCurrentValue(YAxisMinorGridLinesProperty, false);
            SetCurrentValue(XAutoRangeProperty, AutoRange.Always);
            SetCurrentValue(YAutoRangeProperty, AutoRange.Always);

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

            Initialized += (sender, e) =>
            {
                if (LineCollection == null)
                {
                    return;
                }

                foreach (var line in LineCollection)
                {
                    AddVisualChild(line);
                }
            };

            Loaded += (sender, e) =>
            {
                OnLineCollectionChanged();
            };
        }

        #endregion

        #region Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ChartSurface = (SciChartSurface)GetTemplateChild("PartSciChartSurface");

            OnLineCollectionChanged();
        }

        public static void OnLineCollectionSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((FastLinesChart)o).OnLineCollectionChanged();
        }

        private void OnLineCollectionChanged()
        {
            if (ChartSurface == null)
            {
                return;
            }

            if (LineCollection == null)
            {
                ChartSurface.RenderableSeries = null;

                return;
            }

            SetAxesAndDatas();
            WeakEventManager<ObservableCollection<LineSeries>, NotifyCollectionChangedEventArgs>.RemoveHandler(LineCollection, "CollectionChanged", LineCollectionChanged);
            WeakEventManager<ObservableCollection<LineSeries>, NotifyCollectionChangedEventArgs>.AddHandler(LineCollection, "CollectionChanged", LineCollectionChanged);
        }

        private void LineCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SetAxesAndDatas();
        }

        private void SetAxesAndDatas()
        {
            ChartSurface.RenderableSeries = new ObservableCollection<IRenderableSeries>();

            // 设置坐标轴
            foreach (var line in LineCollection)
            {
                if (line != null && line.Datas != null)
                {
                    SetAxes(line.Datas);
                    break;
                }
            }

            for (int i = 0; i < LineCollection.Count; i++)
            {
                if (LineCollection[i] != null)
                {
                    var renderableSeries = new FastLineRenderableSeries();
                    renderableSeries.SetBinding(FastLineRenderableSeries.DataSeriesProperty, ConfigureBinding("Datas", LineCollection[i]));
                    renderableSeries.SetBinding(FastLineRenderableSeries.SeriesColorProperty, ConfigureBinding("DatasColor", LineCollection[i]));
                    renderableSeries.SetBinding(FastLineRenderableSeries.PointMarkerProperty, ConfigureBinding("DatasPointMarker", LineCollection[i]));

                    ChartSurface.RenderableSeries.Add(renderableSeries);
                }
            }
        }

        #endregion

        #region Fields & Properties

        public ObservableCollection<LineSeries> LineCollection
        {
            get
            {
                return (ObservableCollection<LineSeries>)GetValue(LineCollectionProperty);
            }
            set
            {
                SetValue(LineCollectionProperty, value);
            }
        }

        public static readonly DependencyProperty LineCollectionProperty = DependencyProperty.Register("LineCollection", typeof(ObservableCollection<LineSeries>), typeof(FastLinesChart), new PropertyMetadata(OnLineCollectionSourceChanged));

        /// <summary>
        /// 图表横轴刻度格式
        /// </summary>
        public string XAxisTextFormat
        {
            get
            {
                return (string)GetValue(XAxisTextFormatProperty);
            }
            set
            {
                SetValue(XAxisTextFormatProperty, value);
            }
        }

        public static readonly DependencyProperty XAxisTextFormatProperty = DependencyProperty.Register("XAxisTextFormat", typeof(string), typeof(FastLinesChart));

        #endregion
    }
}
