using Abt.Controls.SciChart.Model.DataSeries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Media;

namespace Digihail.Controls
{
    /// <summary>
    /// 簇图数据集
    /// </summary>
    public class SSSDataSeries : FrameworkElement, INotifyPropertyChanged 
    {
        #region Constructor

        public SSSDataSeries()
        {
            SetCurrentValue(DataBrushProperty, new SolidColorBrush(Colors.Red));
            SetCurrentValue(DataWidthProperty, 0.1);
            SetCurrentValue(DataOpacityProperty, 1.0);
        }

        #endregion

        #region Methods

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

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private static void OnDatasSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SSSDataSeries)d).OnDatasChanged(); 
        }

        /// <summary>
        /// 初始化结束后当数据产生变化后重绘，位置只有在初始化时才随之初始化
        /// </summary>
        private void OnDatasChanged()
        {
            if (Datas == null)
            {
                XySeries = null;

                return;
            }

            if (ChartUtil.GetUnderlyingType(Datas.GetType()) == typeof(int))
            {
                if (Positions != null)
                {
                    XySeries = new XyDataSeries<double, int>();
                    ((XyDataSeries<double, int>)XySeries).Append(Positions, (IEnumerable<int>)Datas);
                }

                if (Datas.GetType() == typeof(ObservableCollection<int>))
                {
                    WeakEventManager<ObservableCollection<int>, NotifyCollectionChangedEventArgs>.AddHandler((ObservableCollection<int>)Datas, "CollectionChanged", (sender, e) =>
                    {
                        if (Positions != null)
                        {
                            XySeries = new XyDataSeries<double, int>();
                            ((XyDataSeries<double, int>)XySeries).Append(Positions, (IEnumerable<int>)Datas);
                        }
                    });
                }
            }
            else if (ChartUtil.GetUnderlyingType(Datas.GetType()) == typeof(double))
            {
                if (Positions != null)
                {
                    XySeries = new XyDataSeries<double, double>();
                    ((XyDataSeries<double, double>)XySeries).Append(Positions, (IEnumerable<double>)Datas);
                }

                if (Datas.GetType() == typeof(ObservableCollection<double>))
                {
                    WeakEventManager<ObservableCollection<double>, NotifyCollectionChangedEventArgs>.AddHandler((ObservableCollection<double>)Datas, "CollectionChanged", (sender, e) =>
                    {
                        if (Positions != null)
                        {
                            XySeries = new XyDataSeries<double, double>();
                            ((XyDataSeries<double, double>)XySeries).Append(Positions, (IEnumerable<double>)Datas);
                        }
                    });
                }
            }
        }

        #endregion

        #region Fields & Properties

        public static readonly DependencyProperty DatasProperty = DependencyProperty.Register("Datas", typeof(IEnumerable), typeof(SSSDataSeries), new PropertyMetadata(OnDatasSourceChanged));

        /// <summary>
        /// 数据集数据
        /// </summary>
        public IEnumerable Datas
        {
            get
            {
                return (IEnumerable)GetValue(DatasProperty);
            }
            set
            {
                SetValue(DatasProperty, value);
            }
        }

        public static readonly DependencyProperty DataBrushProperty = DependencyProperty.Register("DataBrush", typeof(Brush), typeof(SSSDataSeries));

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

        public static readonly DependencyProperty DataWidthProperty = DependencyProperty.Register("Width", typeof(double), typeof(SSSDataSeries));

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

        public static readonly DependencyProperty DataOpacityProperty = DependencyProperty.Register("DataOpacity", typeof(double), typeof(SSSDataSeries));

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

        private object xySeries;
        /// <summary>
        /// 一维横纵数据集
        /// </summary>
        public object XySeries
        {
            get
            {
                return xySeries;
            }
            set
            {
                if (value != xySeries)
                {
                    xySeries = value;
                    RaisePropertyChanged(() => XySeries);
                }
            }
        }

        /// <summary>
        /// 数据位置集合
        /// </summary>
        public double[] Positions
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
