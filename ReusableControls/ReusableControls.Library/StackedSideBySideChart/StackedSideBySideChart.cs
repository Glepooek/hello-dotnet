using Abt.Controls.SciChart;
using Abt.Controls.SciChart.Model.DataSeries;
using Abt.Controls.SciChart.Themes;
using Abt.Controls.SciChart.Visuals;
using Abt.Controls.SciChart.Visuals.Axes;
using Abt.Controls.SciChart.Visuals.RenderableSeries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Digihail.Controls
{
    /// <summary>
    /// 横向簇图
    /// </summary>
    [TemplatePart(Name = "PartSciChartSurface", Type = typeof(SciChartSurface))]
    public class StackedSideBySideChart : ChartBase
    {
        #region Constructor

        static StackedSideBySideChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StackedSideBySideChart), new FrameworkPropertyMetadata(typeof(StackedSideBySideChart)));
        }

        public StackedSideBySideChart() : base()
        {
            SetCurrentValue(ThumbCollectionsProperty, new ObservableCollection<SSSDataSeries>());
            SetCurrentValue(XLabelAlignmentProperty, AxisLabelAlignment.Center);
            SetCurrentValue(XAxisMajorDeltaProperty, 1.0);
            SetCurrentValue(XAxisMinorDeltaProperty, 0.2);
            SetCurrentValue(YGrowByRangeProperty, new DoubleRange(0, 0.2));
            SetCurrentValue(ChartBorderThicknessProperty, new Thickness(1, 1, 1, 1));

            Initialized += (sender, e) =>
            {
                if (ThumbCollections == null)
                {
                    return;
                }

                foreach (var line in ThumbCollections)
                {
                    AddVisualChild(line);
                }
            };

            Loaded += (sender, e) =>
            {
                OnThumbCollectionChanged();
            };
        }

        #endregion

        #region Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ChartSurface = (SciChartSurface)GetTemplateChild("PartSciChartSurface");

            OnThumbCollectionChanged();
        }

        public static void OnThumbCollectionSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((StackedSideBySideChart)o).OnThumbCollectionChanged();
        }

        private void OnThumbCollectionChanged()
        {
            if(XLabels == null || ChartSurface == null)
            {
                return;
            }

            if (ThumbCollections == null)
            {
                ChartSurface.RenderableSeries = null;

                return;
            }

            SetAxesAndDatas();
            WeakEventManager<ObservableCollection<SSSDataSeries>, NotifyCollectionChangedEventArgs>.RemoveHandler(ThumbCollections, "CollectionChanged", ThumbCollectionsChanged);
            WeakEventManager<ObservableCollection<SSSDataSeries>, NotifyCollectionChangedEventArgs>.AddHandler(ThumbCollections, "CollectionChanged", ThumbCollectionsChanged);
        }

        private void ThumbCollectionsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SetAxesAndDatas();
        }

        private void SetAxesAndDatas()
        {
            var thumbShift = 1.0 / (ThumbCollections.Count + 1);
            var thumbXPositions = new double[XLabels.Length];

            if (XLabelAlignment == AxisLabelAlignment.Center)
            {
                WeakEventManager<SciChartSurface, EventArgs>.RemoveHandler(ChartSurface, "Rendered", SetAxisTickLabelToMiddle);
                WeakEventManager<SciChartSurface, EventArgs>.AddHandler(ChartSurface, "Rendered", SetAxisTickLabelToMiddle);

                xRange = new DoubleRange(0, XLabels.Length);
                thumbXPositions = Enumerable.Range(0, XLabels.Length).Cast<int>().Select(x => (double)x).ToArray();
            }
            else if (XLabelAlignment == AxisLabelAlignment.TickMark)
            {
                xRange = new DoubleRange(-0.5, XLabels.Length - 0.5);
                thumbXPositions = Enumerable.Range(0, XLabels.Length).Cast<int>().Select(x => (double)x - 0.5).ToArray();
            }

            RaisePropertyChanged(() => XRange);
            ChartSurface.RenderableSeries = new ObservableCollection<IRenderableSeries>();

            for (int i = 0; i < ThumbCollections.Count; i++)
            {
                if (ThumbCollections[i] != null)
                {
                    var renderableSeries = new StackedColumnRenderableSeries();
                    renderableSeries.SetBinding(StackedColumnRenderableSeries.DataPointWidthProperty, ConfigureBinding("DataWidth", ThumbCollections[i]));
                    renderableSeries.SetBinding(StackedColumnRenderableSeries.FillBrushProperty, ConfigureBinding("DataBrush", ThumbCollections[i]));
                    renderableSeries.SetBinding(StackedColumnRenderableSeries.OpacityProperty, ConfigureBinding("DataOpacity", ThumbCollections[i]));
                    renderableSeries.SetBinding(StackedColumnRenderableSeries.DataSeriesProperty, ConfigureBinding("XySeries", ThumbCollections[i]));
                    renderableSeries.StackedGroupId = i.ToString();
                    ThumbCollections[i].Positions = thumbXPositions.Select(p => p = p + thumbShift * (i + 1)).ToArray<double>();

                    if (ThumbCollections[i].Datas != null)
                    {
                        if (ChartUtil.GetUnderlyingType(ThumbCollections[i].Datas.GetType()) == typeof(int))
                        {
                            ThumbCollections[i].XySeries = new XyDataSeries<double, int>();
                            ((XyDataSeries<double, int>)ThumbCollections[i].XySeries).Append(ThumbCollections[i].Positions, (IEnumerable<int>)ThumbCollections[i].Datas);
                        }
                        else if (ChartUtil.GetUnderlyingType(ThumbCollections[i].Datas.GetType()) == typeof(double))
                        {
                            ThumbCollections[i].XySeries = new XyDataSeries<double, double>();
                            ((XyDataSeries<double, double>)ThumbCollections[i].XySeries).Append(ThumbCollections[i].Positions, (IEnumerable<double>)ThumbCollections[i].Datas);
                        }
                    }

                    ChartSurface.RenderableSeries.Add(renderableSeries);
                }
            }
        }

        private void SetAxisTickLabelToMiddle(object sender, EventArgs e)
        {
            var canvasList = new List<TickLabelAxisCanvas>();
            List<TickLabelAxisCanvas> TickLabelAxisCanvasList = SearchVisualTree<TickLabelAxisCanvas>(ChartSurface, canvasList);

            foreach (var canvas in TickLabelAxisCanvasList)
            {
                var oldXValue = 0.0;
                var oldYValue = 0.0;
                var isFirst = true;
                var averageXValue = 0.0;
                var averageYValue = 0.0;
                NumericTickLabel FirstNumericTickLabel = null; 

                foreach (NumericTickLabel child in canvas.Children)
                {
                    child.Visibility = Visibility.Visible;
                    var newXValue = child.Position.X;
                    var newYValue = child.Position.Y;

                    if (isFirst)
                    {
                        FirstNumericTickLabel = child;
                        isFirst = false;
                    }
                    else
                    {
                        if (XLabelAlignment == AxisLabelAlignment.Center)
                        {
                            averageXValue = (oldXValue - newXValue) / 2;
                        }

                        if (YLabelAlignment == AxisLabelAlignment.Center)
                        {
                            averageYValue = (oldYValue - newYValue) / 2;
                        }

                        child.Position = new Point(child.Position.X - averageXValue, child.Position.Y - averageYValue);
                    }

                    oldXValue = newXValue;
                    oldYValue = newYValue;
                }

                if (FirstNumericTickLabel != null)
                {
                    FirstNumericTickLabel.Position = new Point(FirstNumericTickLabel.Position.X - averageXValue, FirstNumericTickLabel.Position.Y - averageYValue);

                    if (YLabelAlignment == AxisLabelAlignment.Center)
                    {
                        canvas.Children[canvas.Children.Count - 1].Visibility = Visibility.Collapsed;
                    }

                    //  需要判断canvas类型来决定最后一个刻度的显隐
                }
            }
        }

        private List<T> SearchVisualTree<T>(DependencyObject tarElem, List<T> result) where T : DependencyObject
        {
            var count = VisualTreeHelper.GetChildrenCount(tarElem);

            if (count == 0)
            {
                return null;
            }

            for (int i = 0; i < count; ++i)
            {
                var child = VisualTreeHelper.GetChild(tarElem, i);

                if (child != null && child is T)
                {
                    result.Add((T)child);
                }
                else
                {
                    SearchVisualTree<T>(child, result);
                }
            }

            return result;
        }

        private static void OnXLabelsSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((StackedSideBySideChart)o).OnXLabelsChanged();
        }

        private void OnXLabelsChanged()
        {
            if (XLabels != null)
            {
                labelFormatter = new CustomLabelProvider<string>(XLabels);
                RaisePropertyChanged(() => LabelFormatter);
                OnThumbCollectionChanged();
            }
        }

        #endregion

        #region Fields & Properties

        public static readonly DependencyProperty ThumbCollectionsProperty = DependencyProperty.Register("ThumbCollections", typeof(ObservableCollection<SSSDataSeries>), typeof(StackedSideBySideChart), new PropertyMetadata(OnThumbCollectionSourceChanged));

        /// <summary>
        /// 簇集合
        /// </summary>
        public ObservableCollection<SSSDataSeries> ThumbCollections
        {
            get
            {
                return (ObservableCollection<SSSDataSeries>)GetValue(ThumbCollectionsProperty);
            }
            set
            {
                SetValue(ThumbCollectionsProperty, value);
            }
        }

        /// <summary>
        /// 横坐标标签
        /// </summary>
        public string[] XLabels
        {
            get
            {
                return (string[])GetValue(XLabelsProperty);
            }
            set
            {
                SetValue(XLabelsProperty, value);
            }
        }

        public static readonly DependencyProperty XLabelsProperty = DependencyProperty.Register("XLabels", typeof(string[]), typeof(StackedSideBySideChart), new PropertyMetadata(OnXLabelsSourceChanged));

        private IRange xRange;
        /// <summary>
        /// 横坐标显示范围
        /// </summary>
        public IRange XRange
        {
            get
            {
                return xRange;
            }
        }

        private ILabelProvider labelFormatter;
        /// <summary>
        /// 横坐标标签格式化器
        /// </summary>
        public ILabelProvider LabelFormatter
        {
            get
            {
                return labelFormatter;
            }
        }

        #endregion
    }
}

