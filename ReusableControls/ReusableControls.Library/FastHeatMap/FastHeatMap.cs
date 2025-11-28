using Abt.Controls.SciChart;
using Abt.Controls.SciChart.ChartModifiers;
using Abt.Controls.SciChart.Model.DataSeries;
using Abt.Controls.SciChart.Themes;
using Abt.Controls.SciChart.Visuals;
using Abt.Controls.SciChart.Visuals.Annotations;
using Abt.Controls.SciChart.Visuals.Axes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Digihail.Controls
{
    /// <summary>
    /// 热点图
    /// </summary>
    [TemplatePart(Name = "PartSciChartSurface", Type = typeof(SciChartSurface))]
    public class FastHeatMap :  ChartBase
    {
        #region Constructor

        static FastHeatMap() 
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FastHeatMap), new FrameworkPropertyMetadata(typeof(FastHeatMap)));
        }

        public FastHeatMap() : base()
        {
            SetCurrentValue(ChartBorderThicknessProperty, new Thickness(1, 1, 1, 1));
            SetCurrentValue(XLabelAlignmentProperty, AxisLabelAlignment.Center);
            SetCurrentValue(YLabelAlignmentProperty, AxisLabelAlignment.Center);
            SetCurrentValue(TooltipVisibilityProperty, Visibility.Visible);
            SetCurrentValue(XMaxAutoTicksProperty, 5);
            SetCurrentValue(YMaxAutoTicksProperty, 5);
            SetCurrentValue(DisplayCellLinesProperty, false);
            SetCurrentValue(DisplayCellTextProperty, false); 
               SetCurrentValue(CanSelectCellProperty, true);
            SetCurrentValue(IsEnableZoomProperty, false); 
             isSetedAxes = false;

            Loaded += (sender, e) =>
             {
                 if(ChartSurface == null)
                 {
                     return;
                 }

                 if (xLabelFormatter != null && ChartSurface.XAxis != null)
                 {
                     ChartSurface.XAxis.LabelProvider = xLabelFormatter;
                 }

                 if (yLabelFormatter != null && ChartSurface.YAxis != null)
                 {
                     ChartSurface.YAxis.LabelProvider = yLabelFormatter;
                 }
             };

            setXAxis = axis =>
            {
                axis.SetBinding(AxisBase.VisibleRangeLimitProperty, ConfigureBinding("XRangeLimit", this));
                axis.SetBinding(AxisBase.FlipCoordinatesProperty, ConfigureBinding("XFlipCoordinates", this));
                axis.SetBinding(AxisBase.AutoTicksProperty, ConfigureBinding("XAutoTicks", this));
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
                axis.SetBinding(AxisBase.TickTextBrushProperty, ConfigureBinding("XTickTextBrush", this));;

                return axis;
            };

            setYAxis = axis =>
            {
                axis.SetBinding(AxisBase.VisibleRangeLimitProperty, ConfigureBinding("YRangeLimit", this));
                axis.SetBinding(AxisBase.FlipCoordinatesProperty, ConfigureBinding("YFlipCoordinates", this));
                axis.SetBinding(AxisBase.AutoTicksProperty, ConfigureBinding("YAutoTicks", this)); 
                axis.SetBinding(AxisBase.MajorDeltaProperty, ConfigureBinding("YAxisMajorDelta", this));
                axis.SetBinding(AxisBase.MinorDeltaProperty, ConfigureBinding("YAxisMinorDelta", this));
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
            rolloverModifier = (RolloverModifier)GetTemplateChild("PartRolloverModifier");
            selectedDataBox = (BoxAnnotation)GetTemplateChild("PartSelectedBox");
            selectedDataBoxThickness = selectedDataBox.BorderThickness;
            selectedDataBox.BorderThickness = new Thickness(0);

            if (Datas != null)
            {
                series = new Heatmap2DArrayDataSeries<int, int, double>(Datas, x => x, y => y);
                RaisePropertyChanged(() => Series);
                SetAxes(Series);
                isSetedAxes = true;
                SetCellLine();
                SetCellText();
            }

            if(initialSelectedDataBoxPosition != null)
            {
                if (CanSelectCell)
                {
                    SetSelectedDataBoxPosition(initialSelectedDataBoxPosition.X, initialSelectedDataBoxPosition.Y);
                }
            }

            if (XLabelAlignment == AxisLabelAlignment.Center || XLabelAlignment == AxisLabelAlignment.Center)
            {
                ChartSurface.Rendered += SetAxisTickLabelToMiddle;
            }

            ChartSurface.PreviewMouseLeftButtonDown += (sender, e) =>
            {
                if(!CanSelectCell || rolloverModifier == null || rolloverModifier.SeriesData == null || rolloverModifier.SeriesData.SeriesInfo == null || !(rolloverModifier.SeriesData.SeriesInfo.Count > 0) || rolloverModifier.SeriesData.SeriesInfo[0] == null)
                {
                    return;
                }
                
                SetSelectedDataBoxPosition(rolloverModifier.SeriesData.SeriesInfo[0].XValue, rolloverModifier.SeriesData.SeriesInfo[0].YValue);

                if (selectedDataBox != null)
                {
                    SelectedDataBoxPosition = new Position(selectedDataBox.X1, selectedDataBox.Y1);
                }
            };

            isAppliedTemplate = true;
        }

        private void SetCellLine()
        {
            if(DisplayCellLines && ChartSurface != null && Datas != null)
            {
                cellLineCollection = new List<LineAnnotation>();

                for (int i = 0; i < Datas.GetLength(1); i++)
                {
                    if (i != 0)
                    {
                        var line = new VerticalLineAnnotation();
                        line.Style = CellLineStyle;
                        line.VerticalAlignment = VerticalAlignment.Stretch;
                        line.X1 = i;
                        ChartSurface.Annotations.Add(line);
                        cellLineCollection.Add(line);
                    }
                }

                for (int i = 0; i < Datas.GetLength(0); i++)
                {
                    if (i != 0)
                    {
                        var line = new HorizontalLineAnnotation();
                        line.Style = CellLineStyle;
                        line.HorizontalAlignment = HorizontalAlignment.Stretch;
                        line.Y1 = i;
                        ChartSurface.Annotations.Add(line);
                        cellLineCollection.Add(line);
                    }
                }
            }
        }

        private void SetCellText()
        {
            if (DisplayCellText && ChartSurface != null && Datas != null)
            {
                var min = Datas[0, 0];
                 var max = Datas[0, 0];
                cellTextCollection = new List<BoxAnnotation>();

                foreach (var data in Datas)
                {
                    if (data < min)
                    {
                        min = data;
                    }

                    if (data > max)
                    {
                        max = data;
                    }
                }

                var range = max - min;

                var figureOutIsBlack = new Func<int, int, bool>((k, j) =>
                 {
                     var isBlack = false;

                     if (TextLightFields != null)
                     {
                         for (int i = 0; i < TextLightFields.Length; i++)
                         {
                             isBlack = Datas[k, j] <= TextLightFields[i].Max * range && Datas[k, j] >= TextLightFields[i].Min * range ? true : isBlack;
                         }
                     } // 在浅域内置黑色，否则置白色

                    return isBlack;
                 });

                foreach (var annotation in ChartSurface.Annotations)
                {
                    if (annotation.GetType() == typeof(BoxAnnotation) && annotation != selectedDataBox)
                    {
                        var textAnnotion = (BoxAnnotation)annotation;
                        var x = Convert.ToInt32(textAnnotion.X1);
                        var y = Convert.ToInt32(textAnnotion.Y1);

                        if (x < Datas.GetLength(1) && y < Datas.GetLength(0))
                        {
                            textAnnotion.Content = Math.Round(Datas[y, x], 1).ToString();
                            textAnnotion.Style = CellTextStyle;
                            textAnnotion.Foreground = figureOutIsBlack(y, x) ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.White);
                            cellTextCollection.Add(textAnnotion);
                        }
                    }
                }

                for (int k = 0; k < Datas.GetLength(0); k++)
                {
                    for (int j = 0; j < Datas.GetLength(1); j++)
                    {
                        var isExist = false;

                        foreach (var t in cellTextCollection)
                        {
                            if (Convert.ToInt32(t.X1) == j && Convert.ToInt32(t.Y1) == k)
                            {
                                isExist = true;
                                break;
                            }
                        }

                        if (!isExist)
                        {
                            var newTextAnnotion = new BoxAnnotation();
                            newTextAnnotion.Style = CellTextStyle;
                            newTextAnnotion.Content = Math.Round(Datas[k, j], 1).ToString();
                            newTextAnnotion.X1 = j;
                            newTextAnnotion.X2 = j + 1;
                            newTextAnnotion.Y1 = k;
                            newTextAnnotion.Y2 = k + 1;
                            newTextAnnotion.Foreground = figureOutIsBlack(k, j) ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.White);
                            ChartSurface.Annotations.Add(newTextAnnotion);
                            cellTextCollection.Add(newTextAnnotion);
                        }
                    }
                }
            }
        }

        public static void OnXLabelsSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((FastHeatMap)o).OnXLabelsChanged();
        }

        private void OnXLabelsChanged()
        {
            if (XLabels == null)
            {
                SetAxes(Series);
                isSetedAxes = true;
            } // 重置
            else
            {
                xLabelFormatter = new CustomLabelProvider<string>(XLabels);

                if (ChartSurface != null && ChartSurface.XAxis != null)
                {
                    ChartSurface.XAxis.LabelProvider = xLabelFormatter;
                }
            }
        }

        public static void OnYLabelsSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((FastHeatMap)o).OnYLabelsChanged();
        }

        private void OnYLabelsChanged()
        {
            if (YLabels == null)
            {
                SetAxes(Series);
                isSetedAxes = true;
            } // 重置
            else
            {
                yLabelFormatter = new CustomLabelProvider<string>(YLabels);

                if (ChartSurface != null && ChartSurface.YAxis != null)
                {
                    ChartSurface.YAxis.LabelProvider = yLabelFormatter;
                }
            }
        }

        public static void OnCellLineStyleSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((FastHeatMap)o).OnCellLineStyleChanged(e.NewValue as Style, e.OldValue as Style);
        }

        private void OnCellLineStyleChanged(Style newValue, Style oldValue)
        {
            if (newValue == oldValue)
            {
                return;
            }

            if (ChartSurface != null && cellLineCollection != null)
            {
                cellLineCollection.ForEach(l => ChartSurface.Annotations.Remove(l));
                SetCellLine();
            }
        }

        public static void OnDisplayCellLinesSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((FastHeatMap)o).OnDisplayCellLinesChanged((bool)e.NewValue, (bool)e.OldValue);
        }

        private void OnDisplayCellLinesChanged(bool newValue, bool oldValue)
        {
            if (newValue == oldValue)
            {
                return;
            }

            if (ChartSurface != null && cellLineCollection != null)
            {
                if (newValue)
                {
                    cellLineCollection.ForEach(l => ChartSurface.Annotations.Remove(l));
                    SetCellLine();
                }
                else
                {
                    cellLineCollection.ForEach(l => ChartSurface.Annotations.Remove(l));
                }
            }
        }

        public static void OnDisplayCellTextSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((FastHeatMap)o).OnDisplayCellTextChanged((bool)e.NewValue, (bool)e.OldValue);
        }

        private void OnDisplayCellTextChanged(bool newValue, bool oldValue)
        {
            if (newValue == oldValue)
            {
                return;
            }

            if (ChartSurface != null && cellTextCollection != null)
            {
                if (newValue)
                {
                    SetCellText();
                }
                else
                {
                    cellTextCollection.ForEach(t => ChartSurface.Annotations.Remove(t));
                }
            }
        }

        public static void OnCellTextStyleSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((FastHeatMap)o).OnCellTextStyleChanged(e.NewValue as Style, e.OldValue as Style);
        }

        private void OnCellTextStyleChanged(Style newValue, Style oldValue)
        {
            if (newValue == oldValue)
            {
                return;
            }

            if (ChartSurface != null && cellTextCollection != null)
            {
                SetCellText();
            }
        }

        public static void OnTextLightFieldsSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((FastHeatMap)o).OnTextLightFieldsChanged(e.NewValue as Range[], e.OldValue as Range[]);
        }
        
        private void OnTextLightFieldsChanged(Range[] newValue, Range[] oldValue)
        {
            if(newValue == oldValue)
            {
                return;
            }

            if(ChartSurface != null && cellTextCollection != null)
            {
                SetCellText();
            }
        }

        private void SetSelectedDataBoxPosition(IComparable xValue, IComparable yValue)
        {
            if(selectedDataBox == null)
            {
                return;
            }

            int x1 = (int)Convert.ToDouble(xValue);
            selectedDataBox.X1 = x1;
            selectedDataBox.X2 = x1 + 1;
            int y1 = (int)Convert.ToDouble(yValue);
            selectedDataBox.Y1 = y1;
            selectedDataBox.Y2 = y1 + 1;
            selectedDataBox.BorderThickness = selectedDataBoxThickness;
        }

        public static void OnSelectedDataBoxStyleSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if(((FastHeatMap)o).selectedDataBox != null)
            {
                ((FastHeatMap)o).selectedDataBoxThickness = ((FastHeatMap)o).selectedDataBox.BorderThickness;
            }
        }

        public static void OnSelectedDataBoxPositionSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((FastHeatMap)o).OnSelectedDataBoxPositionChanged(e.NewValue as Position, e.OldValue as Position);
        }

        private void OnSelectedDataBoxPositionChanged(Position newValue, Position oldValue)
        {
            if (newValue == oldValue)
            {
                return;
            }

            if (newValue != null && CanSelectCell)
            {
                if (!isAppliedTemplate)
                {
                    initialSelectedDataBoxPosition = newValue;
                }

                SetSelectedDataBoxPosition(newValue.X, newValue.Y);
            }
            else
            {
                if(selectedDataBox != null)
                {
                    selectedDataBox.BorderThickness = new Thickness(0); 
                }
            }
        }

        public static void OnDatasSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((FastHeatMap)o).OnDatasChanged(e.NewValue as double[,], e.OldValue as double[,]);
        }

        private void OnDatasChanged(double[,] newDatas, double[,] oldDatas)
        {
            if (newDatas == null || newDatas == oldDatas)
            {
                return;
            }

            dataMax = newDatas[0, 0];

            foreach (var data in Datas)
            {
                if (data > dataMax)
                {
                    dataMax = data;
                }
            }

            RaisePropertyChanged(() => DataMax);
            series = new Heatmap2DArrayDataSeries<int, int, double>(newDatas, x => x, y => y);
            RaisePropertyChanged(() => Series);

            if (ChartSurface != null)
            {
                if (cellLineCollection != null)
                {
                    cellLineCollection.ForEach(l => ChartSurface.Annotations.Remove(l));
                }
            }

            SetCellLine();
            SetCellText();

            if(!isSetedAxes)
            {
                SetAxes(Series);
                isSetedAxes = true;
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

        #endregion

        #region Fields & Properties

        /// <summary>
        /// 横坐标刻度标记集合
        /// </summary>
        public string[] XLabels
        {
            get { return (string[])GetValue(XLabelsProperty); }
            set { SetValue(XLabelsProperty, value); }
        }

        public static readonly DependencyProperty XLabelsProperty = DependencyProperty.Register("XLabels", typeof(string[]), typeof(FastHeatMap), new PropertyMetadata(OnXLabelsSourceChanged));

        /// <summary>
        /// 横坐标刻度标记集合
        /// </summary>
        public string[] YLabels
        {
            get { return (string[])GetValue(YLabelsProperty); }
            set { SetValue(YLabelsProperty, value); }
        }

        public static readonly DependencyProperty YLabelsProperty = DependencyProperty.Register("YLabels", typeof(string[]), typeof(FastHeatMap), new PropertyMetadata(OnYLabelsSourceChanged));

        /// <summary>
        /// 是否启用缩放
        /// </summary>
        public bool IsEnableZoom
        {
            get { return (bool)GetValue(IsEnableZoomProperty); }
            set { SetValue(IsEnableZoomProperty, value); }
        }

        public static readonly DependencyProperty IsEnableZoomProperty = DependencyProperty.Register("IsEnableZoom", typeof(bool), typeof(FastHeatMap));
        
        /// <summary>
        /// 是否显示热区网格线
        /// </summary>
        public bool DisplayCellLines
        {
            get { return (bool)GetValue(DisplayCellLinesProperty); }
            set { SetValue(DisplayCellLinesProperty, value); }
        }

        public static readonly DependencyProperty DisplayCellLinesProperty = DependencyProperty.Register("DisplayCellLines", typeof(bool), typeof(FastHeatMap), new PropertyMetadata(OnDisplayCellLinesSourceChanged));

        /// <summary>
        /// 热区网格线样式
        /// </summary>
        public Style CellLineStyle
        {
            get { return (Style)GetValue(CellLineStyleProperty); }
            set { SetValue(CellLineStyleProperty, value); }
        }

        public static readonly DependencyProperty CellLineStyleProperty = DependencyProperty.Register("CellLineStyle", typeof(Style), typeof(FastHeatMap), new PropertyMetadata(OnCellLineStyleSourceChanged));

        /// <summary>
        /// 是否显示热区文字标记
        /// </summary>
        public bool DisplayCellText
        {
            get { return (bool)GetValue(DisplayCellTextProperty); }
            set { SetValue(DisplayCellTextProperty, value); }
        }

        public static readonly DependencyProperty DisplayCellTextProperty = DependencyProperty.Register("DisplayCellText", typeof(bool), typeof(FastHeatMap), new PropertyMetadata(OnDisplayCellTextSourceChanged));

        /// <summary>
        /// 热区文字样式
        /// </summary>
        public Style CellTextStyle
        {
            get { return (Style)GetValue(CellTextStyleProperty); }
            set { SetValue(CellTextStyleProperty, value); }
        }

        public static readonly DependencyProperty CellTextStyleProperty = DependencyProperty.Register("CellTextStyle", typeof(Style), typeof(FastHeatMap), new PropertyMetadata(OnCellTextStyleSourceChanged));

        /// <summary>
        /// 热区文字标记深浅域
        /// </summary>
        public Range[] TextLightFields
        {
            get { return (Range[])GetValue(TextLightFieldsProperty); }
            set { SetValue(TextLightFieldsProperty, value); }
        }

        public static readonly DependencyProperty TextLightFieldsProperty = DependencyProperty.Register("TextLightFields", typeof(Range[]), typeof(FastHeatMap), new PropertyMetadata(OnTextLightFieldsSourceChanged));

        /// <summary>
        /// 热点图数据
        /// </summary>
        public double[,] Datas
        {
            get { return (double[,])GetValue(DatasProperty); }
            set { SetValue(DatasProperty, value); }
        }

        public static readonly DependencyProperty DatasProperty = DependencyProperty.Register("Datas", typeof(double[,]), typeof(FastHeatMap), new PropertyMetadata(OnDatasSourceChanged));

        /// <summary>
        /// 是否可以选中热区数据
        /// </summary>
        public bool CanSelectCell
        {
            get { return (bool)GetValue(CanSelectCellProperty); }
            set { SetValue(CanSelectCellProperty, value); }
        }

        public static readonly DependencyProperty CanSelectCellProperty = DependencyProperty.Register("CanSelectCell", typeof(bool), typeof(FastHeatMap));

        public Style SelectedDataBoxStyle
        {
            get { return (Style)GetValue(SelectedDataBoxStyleProperty); }
            set { SetValue(SelectedDataBoxStyleProperty, value); }
        }

        public static readonly DependencyProperty SelectedDataBoxStyleProperty = DependencyProperty.Register("SelectedDataBoxStyle", typeof(Style), typeof(FastHeatMap), new PropertyMetadata(OnSelectedDataBoxStyleSourceChanged));

        /// <summary>
        /// 选中数据框位置
        /// </summary>
        public Position SelectedDataBoxPosition
        {
            get { return (Position)GetValue(SelectedDataBoxPositionProperty); }
            set { SetValue(SelectedDataBoxPositionProperty, value); }
        }
        
        public static readonly DependencyProperty SelectedDataBoxPositionProperty = DependencyProperty.Register("SelectedDataBoxPosition", typeof(Position), typeof(FastHeatMap), new PropertyMetadata(OnSelectedDataBoxPositionSourceChanged));

        /// <summary>
        /// 色彩映射，用于从数据来计算数据显示色彩
        /// </summary>
        public LinearGradientBrush DataColorMap
        {
            get
            {
                return (LinearGradientBrush)GetValue(DataColorMapProperty);
            }
            set
            {
                SetValue(DataColorMapProperty, value);
            }
        }

        public static readonly DependencyProperty DataColorMapProperty = DependencyProperty.Register("DataColorMap", typeof(LinearGradientBrush), typeof(FastHeatMap));

        /// <summary>
        /// 自定义提示信息模板
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

        public static readonly DependencyProperty TooltipLabelTemplateProperty = DependencyProperty.Register("TooltipLabelTemplate", typeof(ControlTemplate), typeof(FastHeatMap));

        /// <summary>
        /// 提示信息的显示方式
        /// </summary>
        public Visibility TooltipVisibility
        {
            get
            {
                return (Visibility)GetValue(ToolTipProperty);
            }
            set
            {
                SetValue(ToolTipProperty, value);
            }
        }

        public static readonly DependencyProperty TooltipVisibilityProperty = DependencyProperty.Register("TooltipVisibility", typeof(Visibility), typeof(FastHeatMap));

        private Heatmap2DArrayDataSeries<int, int, double> series;
        public Heatmap2DArrayDataSeries<int, int, double> Series
        {
            get
            {
                return series;
            }
        }

        private double dataMax;
        public double DataMax
        {
            get
            {
                return dataMax;
            }
        }

        private ILabelProvider xLabelFormatter;

        private ILabelProvider yLabelFormatter;

        private List<BoxAnnotation> cellTextCollection;

        private List<LineAnnotation> cellLineCollection;

        private RolloverModifier rolloverModifier;

        private Thickness selectedDataBoxThickness;

        private BoxAnnotation selectedDataBox;

        private bool isAppliedTemplate;

        private bool isSetedAxes;

        private Position initialSelectedDataBoxPosition;

        #endregion
    }
}
