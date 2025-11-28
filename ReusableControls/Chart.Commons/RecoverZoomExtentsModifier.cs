using Abt.Controls.SciChart;
using Abt.Controls.SciChart.ChartModifiers;
using System;
using System.Windows;

namespace Digihail.Controls
{
    /// <summary>
    /// 可恢复原数据可视范围的zoom extents modifier
    /// </summary>
    public class RecoverZoomExtentsModifier : ChartModifierBase
    {
        #region Constructor

        public RecoverZoomExtentsModifier()
        {
            ExecuteOn = ExecuteOn.MouseDoubleClick;
            SetCurrentValue(DurationProperty, TimeSpan.FromMilliseconds(155));
        }

        #endregion

        #region Methods

        public override void OnModifierDoubleClick(ModifierMouseArgs e)
        {
            if (YAxis != null && InitialYRange != null)
            {
                YAxis.AnimateVisibleRangeTo(InitialYRange, Duration);
            }

            if (XAxis != null && InitialXRange != null)
            {
                XAxis.AnimateVisibleRangeTo(InitialXRange, Duration);
            }
        }

        #endregion

        #region Fields & Properties

        /// <summary>
        /// 缩放动画时间
        /// </summary>
        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(RecoverZoomExtentsModifier));

        /// <summary>
        /// 横坐标初始可视范围
        /// </summary>
        public IRange InitialXRange
        {
            get { return (IRange)GetValue(InitialXRangeProperty); }
            set { SetValue(InitialXRangeProperty, value); }
        }

        public static readonly DependencyProperty InitialXRangeProperty = DependencyProperty.Register("InitialXRange", typeof(IRange), typeof(RecoverZoomExtentsModifier));

        /// <summary>
        /// 纵坐标初始可视范围
        /// </summary>
        public IRange InitialYRange
        {
            get { return (IRange)GetValue(InitialYRangeProperty); }
            set { SetValue(InitialYRangeProperty, value); }
        }

        public static readonly DependencyProperty InitialYRangeProperty = DependencyProperty.Register("InitialYRange", typeof(IRange), typeof(RecoverZoomExtentsModifier));

        #endregion
    }
}
