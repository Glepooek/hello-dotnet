using Abt.Controls.SciChart.Model.DataSeries;
using Abt.Controls.SciChart.Visuals.PointMarkers;
using System.Windows;
using System.Windows.Media;

namespace Digihail.Controls
{
    /// <summary>
    /// 多重线图中的线单位
    /// </summary>
    public class LineSeries : FrameworkElement
    {
        #region Constructor

        public LineSeries()
        {
            SetCurrentValue(DatasColorProperty, Colors.Red);
        }

        #endregion

        #region Fields & Properties

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

        public static readonly DependencyProperty DatasProperty = DependencyProperty.Register("Datas", typeof(IDataSeries), typeof(LineSeries));

        /// <summary>
        /// 线颜色
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

        public static readonly DependencyProperty DatasColorProperty = DependencyProperty.Register("DatasColor", typeof(Color), typeof(LineSeries));

        /// <summary>
        /// 节点标记
        /// </summary>
        public BasePointMarker DatasPointMarker
        {
            get
            {
                return (BasePointMarker)GetValue(DatasPointMarkerProperty);
            }
            set
            {
                SetValue(DatasPointMarkerProperty, value);
            }
        }

        public static readonly DependencyProperty DatasPointMarkerProperty = DependencyProperty.Register("DatasPointMarker", typeof(BasePointMarker), typeof(LineSeries));

        #endregion
    }
}
