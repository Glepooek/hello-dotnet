using Abt.Controls.SciChart;
using Abt.Controls.SciChart.ChartModifiers;
using Abt.Controls.SciChart.Model.DataSeries;
using Abt.Controls.SciChart.Visuals;
using Abt.Controls.SciChart.Visuals.Annotations;
using Abt.Controls.SciChart.Visuals.Axes;
using Abt.Controls.SciChart.Visuals.PointMarkers;
using Digihail.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ReusableControls.Library.FastLineChart
{
    [TemplatePart(Name = "PartSciChartSurface", Type = typeof(SciChartSurface))]
    /// <summary>
    /// 线图
    /// </summary>
    public class FastLineChart : ChartBase
    {
        #region Constructor

        static FastLineChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FastLineChart), new FrameworkPropertyMetadata(typeof(FastLineChart)));
        }

        public FastLineChart() : base()
        {
            SetCurrentValue(SeriesThicknessProperty, 1);
            SetCurrentValue(YGrowByRangeProperty, new DoubleRange(0.05, 0.05));
            SetCurrentValue(DatasColorProperty, Color.FromRgb(0x2E, 0x8F, 0xD1));
            SetCurrentValue(XAxisMajorGridLinesProperty, false);
            SetCurrentValue(XAxisMinorGridLinesProperty, false);
            SetCurrentValue(YAxisMajorGridLinesProperty, false);
            SetCurrentValue(YAxisMinorGridLinesProperty, false);
            SetCurrentValue(YAutoRangeProperty, AutoRange.Always);
            SetCurrentValue(DataSelectionLineVisibilityProperty, Visibility.Collapsed);
            SetCurrentValue(IsEnableRolloverModifierProperty, false);

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
            dataSelectionLine = (VerticalLineAnnotation)GetTemplateChild("PartVerticalLine");
            rolloverModifier = (RolloverModifier)GetTemplateChild("PartRolloverModifier");

            ChartSurface.PreviewMouseLeftButtonDown += (sender, e) =>
            {
                if (!IsEnableLineAnnotation || rolloverModifier == null || rolloverModifier.SeriesData == null || rolloverModifier.SeriesData.SeriesInfo == null || !(rolloverModifier.SeriesData.SeriesInfo.Count > 0) || rolloverModifier.SeriesData.SeriesInfo[0] == null)
                {
                    return;
                }

                if (dataSelectionLine != null)
                {
                    dataSelectionLine.X1 = rolloverModifier.SeriesData.SeriesInfo[0].XValue;
                    SelectedDataX = dataSelectionLine.X1;
                }
            };
        }

        public static void OnDatasSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((FastLineChart)o).OnDatasChanged(e.NewValue as IDataSeries, e.OldValue as IDataSeries);
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

        public static void OnSelectedDataXSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((FastLineChart)o).OnSelectedDataXChanged(e.NewValue as IComparable, e.OldValue as IComparable);
        }

        private void OnSelectedDataXChanged(IComparable newX, IComparable oldX)
        {
            if(dataSelectionLine != null && newX != oldX)
            {
                dataSelectionLine.X1 = newX;
            }
        }

        #endregion

        #region Fields & Properties

        /// <summary>
        /// 是否启用对数据集点击标记的功能
        /// </summary>
        public bool IsEnableLineAnnotation
        {
            get { return (bool)GetValue(IsEnableLineAnnotationProperty); }
            set { SetValue(IsEnableLineAnnotationProperty, value); }
        }

        public static readonly DependencyProperty IsEnableLineAnnotationProperty = DependencyProperty.Register("IsEnableLineAnnotation", typeof(bool), typeof(FastLineChart), new PropertyMetadata(true));

        /// <summary>
        /// 是否启用标记
        /// </summary>
        public bool IsEnableRolloverModifier
        {
            get { return (bool)GetValue(IsEnableRolloverModifierProperty); }
            set { SetValue(IsEnableRolloverModifierProperty, value); }
        }

        public static readonly DependencyProperty IsEnableRolloverModifierProperty = DependencyProperty.Register("IsEnableRolloverModifier", typeof(bool), typeof(FastLineChart));


        /// <summary>
        /// rollover数据标记模板
        /// </summary>
        public ControlTemplate RolloverMarkerTemplate
        {
            get
            {
                return (ControlTemplate)GetValue(RolloverMarkerTemplateProperty);
            }
            set
            {
                SetValue(RolloverMarkerTemplateProperty, value);
            }
        }

        public static readonly DependencyProperty RolloverMarkerTemplateProperty = DependencyProperty.Register("RolloverMarkerTemplate", typeof(ControlTemplate), typeof(FastLineChart));

        /// <summary>
        /// 提示信息模板
        /// </summary>
        public ControlTemplate TooltipLabelTemplate
        {
            get
            {
                return (ControlTemplate)GetValue(TooltipLabelTemplateProperty);
            }
            set
            {
                SetValue(TooltipLabelTemplateProperty, value);
            }
        }

        public static readonly DependencyProperty TooltipLabelTemplateProperty = DependencyProperty.Register("TooltipLabelTemplate", typeof(ControlTemplate), typeof(FastLineChart));

        /// <summary>
        /// 数据集粗度
        /// </summary>
        public int SeriesThickness
        {
            get { return (int)GetValue(SeriesThicknessProperty); }
            set { SetValue(SeriesThicknessProperty, value); }
        }
        
        public static readonly DependencyProperty SeriesThicknessProperty = DependencyProperty.Register("SeriesThickness", typeof(int), typeof(FastLineChart));

        /// <summary>
        /// 数据选中标记的显示方式
        /// </summary>
        public Visibility DataSelectionLineVisibility
        {
            get { return (Visibility)GetValue(DataSelectionLineVisibilityProperty); }
            set { SetValue(DataSelectionLineVisibilityProperty, value); }
        }

        public static readonly DependencyProperty DataSelectionLineVisibilityProperty =
            DependencyProperty.Register("DataSelectionLineVisibility", typeof(Visibility), typeof(FastLineChart));

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

        public static readonly DependencyProperty DatasProperty = DependencyProperty.Register("Datas", typeof(IDataSeries), typeof(FastLineChart), new PropertyMetadata(OnDatasSourceChanged));

        /// <summary>
        /// 数据集颜色
        /// </summary>
        public Color DatasColor
        {
            get
            {
                return (Color)GetValue(DatasColorProperty);
            }
            set
            {
                SetValue(DatasColorProperty, value);
            }
        }

        public static readonly DependencyProperty DatasColorProperty = DependencyProperty.Register("DatasColor", typeof(Color), typeof(FastLineChart));

        /// <summary>
        /// 节点标记
        /// </summary>
        public IPointMarker DatasPointMarker
        {
            get
            {
                return (IPointMarker)GetValue(DatasPointMarkerProperty);
            }
            set
            {
                SetValue(DatasPointMarkerProperty, value);
            }
        }

        public static readonly DependencyProperty DatasPointMarkerProperty = DependencyProperty.Register("DatasPointMarker", typeof(IPointMarker), typeof(FastLineChart));

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

        public static readonly DependencyProperty XAxisTextFormatProperty = DependencyProperty.Register("XAxisTextFormat", typeof(string), typeof(FastLineChart));

        /// <summary>
        /// 数据标记线的X轴值
        /// </summary>
        public IComparable SelectedDataX
        {
            get { return (IComparable)GetValue(SelectedDataXProperty); }
            set { SetValue(SelectedDataXProperty, value); }
        }

        public static readonly DependencyProperty SelectedDataXProperty = DependencyProperty.Register("SelectedDataX", typeof(IComparable), typeof(FastLineChart), new PropertyMetadata(OnSelectedDataXSourceChanged));

        /// <summary>
        /// 数据标记线样式
        /// </summary>
        public Style DataSelectionLineStyle
        {
            get { return (Style)GetValue(DataSelectionLineStyleProperty); }
            set { SetValue(DataSelectionLineStyleProperty, value); }
        }

        public static readonly DependencyProperty DataSelectionLineStyleProperty = DependencyProperty.Register("DataSelectionLineStyle", typeof(Style), typeof(FastLineChart));
        
        private VerticalLineAnnotation dataSelectionLine;

        private RolloverModifier rolloverModifier;

        #endregion
    }
}
