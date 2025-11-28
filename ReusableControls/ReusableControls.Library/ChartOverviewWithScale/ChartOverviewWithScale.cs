using Abt.Controls.SciChart.Visuals;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Digihail.Controls
{
    public class ChartOverviewWithScale : Control, INotifyPropertyChanged
    {
        #region Constructor

        static ChartOverviewWithScale()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ChartOverviewWithScale), new FrameworkPropertyMetadata(typeof(ChartOverviewWithScale)));
        }

        public ChartOverviewWithScale()
        {
            SetCurrentValue(MainTickValueProperty, new TimeSpan(5, 0, 0, 0));
            SetCurrentValue(DatasColorProperty, Color.FromRgb(0x2E, 0x8F, 0xD1));

            Loaded += (sender, e) =>
            {
                var overview = (SciChartOverview)GetTemplateChild("Overview");
                var rectRight = (Rectangle)overview.Template.FindName("PART_RightEdge", overview);
                var rectLeft = (Rectangle)overview.Template.FindName("PART_LeftEdge", overview);
                rectLeft.Fill = rectRight.Fill = new SolidColorBrush(Colors.Transparent);
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

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            overviewTicks = (ItemsControl)GetTemplateChild("OverviewTicks");

            overviewTicks.SizeChanged += (sender, e) =>
            {
                if (e.NewSize.Width != e.PreviousSize.Width)
                {
                    actualWidth = e.NewSize.Width;
                    SetScale();
                }
            };
        }

        public static void OnOverviewSciChartChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((ChartOverviewWithScale)o).SetScale();
        }

        public static void OnXAxisTextFormatSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((ChartOverviewWithScale)o).SetScale();
        }

        public static void OnMainTickValueSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((ChartOverviewWithScale)o).SetScale();
        }

        /// <summary>
        /// 暂时只考虑概率横向放置
        /// </summary>
        private void SetScale()
        {
            if (OverviewSciChart != null && OverviewSciChart.RenderableSeries != null && OverviewSciChart.RenderableSeries.Count > 0 && OverviewSciChart.RenderableSeries[0].DataSeries != null)
            {
                scaleValueList = new ObservableCollection<FastScale>();
                dynamic minValue = OverviewSciChart.RenderableSeries[0].DataSeries.XMin;
                dynamic maxValue = OverviewSciChart.RenderableSeries[0].DataSeries.XMax;
                var formant = string.IsNullOrEmpty(XAxisTextFormat) ? "{0}" : XAxisTextFormat;
                var ticksCount = 5;
                var span = (maxValue - minValue) is TimeSpan ? ((TimeSpan)(maxValue - minValue)).TotalMilliseconds : maxValue - minValue;
                var calculableMainTickValue = 0.0;
                object value;
                double width;
                HorizontalAlignment horizontalAlignment;
                HorizontalAlignment containerHorizontalAlignment;

                if (MainTickValue != null)
                {
                    calculableMainTickValue = MainTickValue is TimeSpan ? ((TimeSpan)MainTickValue).TotalMilliseconds : (double)MainTickValue;
                    var temp = (int)(span / calculableMainTickValue);
                    ticksCount = span % calculableMainTickValue == 0 ? temp + 1 : temp + 2;
                }
                else
                {
                    calculableMainTickValue = span / ticksCount;
                }

                for (int i = 0; i < ticksCount; i++)
                {
                    if (i == 0)
                    {
                        width = actualWidth;
                        value = minValue;
                        containerHorizontalAlignment = HorizontalAlignment.Left;
                        horizontalAlignment = HorizontalAlignment.Left;
                    }
                    else if (i == ticksCount - 1)
                    {
                        width = actualWidth;
                        value = maxValue;
                        containerHorizontalAlignment = HorizontalAlignment.Right;
                        horizontalAlignment = HorizontalAlignment.Right;
                    }
                    else
                    {
                        if (!(minValue is DateTime && MainTickValue is TimeSpan))
                        {
                            value = calculableMainTickValue * i + (double)minValue;
                        }
                        else
                        {
                            value = (DateTime)minValue + TimeSpan.FromTicks(((TimeSpan)MainTickValue).Ticks * i);
                        }

                        horizontalAlignment = HorizontalAlignment.Center;

                        if (i <= (ticksCount - 1) / 2)
                        {
                            width = (actualWidth / span) * calculableMainTickValue * i * 2;
                            containerHorizontalAlignment = HorizontalAlignment.Left;
                        }
                        else
                        {
                            width = (actualWidth / span) * (span - calculableMainTickValue * i) * 2;
                            containerHorizontalAlignment = HorizontalAlignment.Right;
                        }
                    }

                    scaleValueList.Add(new FastScale()
                    {
                        Label = string.Format(formant, value),
                        Size = width,
                        LabelHorizontalAlignment = horizontalAlignment,
                        ContainerHorizontalAlignment = containerHorizontalAlignment
                    });
                }

                RaisePropertyChanged(() => ScaleValueList);
            }
        }

        #endregion

        #region Fields & Properties

        /// <summary>
        /// 概览数据集颜色
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

        public static readonly DependencyProperty DatasColorProperty = DependencyProperty.Register("DatasColor", typeof(Color), typeof(ChartOverviewWithScale));

        /// <summary>
        /// 概览横轴刻度格式
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

        public static readonly DependencyProperty XAxisTextFormatProperty = DependencyProperty.Register("XAxisTextFormat", typeof(string), typeof(ChartOverviewWithScale), new PropertyMetadata(OnXAxisTextFormatSourceChanged));

        /// <summary>
        /// 概览坐标轴主刻度单位
        /// </summary>
        public IComparable MainTickValue
        {
            get
            {
                return (IComparable)GetValue(MainTickValueProperty);
            }
            set
            {
                SetValue(MainTickValueProperty, value);
            }
        }

        public static readonly DependencyProperty MainTickValueProperty = DependencyProperty.Register("MainTickValue", typeof(IComparable), typeof(ChartOverviewWithScale), new PropertyMetadata(OnMainTickValueSourceChanged));


        /// <summary>
        /// 概览模板
        /// </summary>
        public ControlTemplate OverviewTemplate
        {
            get
            {
                return (ControlTemplate)GetValue(OverviewTemplateProperty);
            }
            set
            {
                SetValue(OverviewTemplateProperty, value);
            }
        }

        public static readonly DependencyProperty OverviewTemplateProperty = DependencyProperty.Register("OverviewTemplate", typeof(ControlTemplate), typeof(ChartOverviewWithScale));
        
        /// <summary>
        /// 绑定的SciChartSurface
        /// </summary>
        public SciChartSurface OverviewSciChart
        {
            get
            {
                return (SciChartSurface)GetValue(OverviewSciChartProperty);
            }
            set
            {
                SetValue(OverviewSciChartProperty, value);
            }
        }

        public static readonly DependencyProperty OverviewSciChartProperty = DependencyProperty.Register("OverviewSciChart", typeof(SciChartSurface), typeof(ChartOverviewWithScale), new PropertyMetadata(OnOverviewSciChartChanged));

        private ObservableCollection<FastScale> scaleValueList;
        public ObservableCollection<FastScale> ScaleValueList
        {
            get
            {
                return scaleValueList;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private ItemsControl overviewTicks;

        private double actualWidth;

        #endregion
    }
}
