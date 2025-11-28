using Abt.Controls.SciChart.ChartModifiers;
using Abt.Controls.SciChart.Visuals.Axes;
using System;
using System.Windows;

namespace Digihail.Controls
{
    /// <summary>
    /// 有最小边界的mouse wheel zoom modifier， 可用于numberic axis，设置major delta为边界
    /// </summary>
    public class BoundedMouseWheelZoomModifier : MouseWheelZoomModifier
    {
        #region Constructor
        
        public BoundedMouseWheelZoomModifier()
        {
            canBounded = true;
            isValidated = false;
            SetCurrentValue(MinMainDeltaProperty, 1.0);
        }

        #endregion

        #region Methods

        public override void OnModifierMouseWheel(ModifierMouseArgs e)
        {
            var modifiers = (ModifierGroup)e.Source;

            if (!isValidated)
            {
                if (modifiers.XAxis == null || modifiers.XAxis.GetType() != typeof(NumericAxis))
                {
                    canBounded = false;
                }

                isValidated = true;
            }

            if (canBounded)
            {
                if (Convert.ToDouble(modifiers.XAxis.MajorDelta) <= MinMainDelta &&  e.Delta >= 0)
                {
                    e.Handled = true;
                } // 主刻度小于等于设置值且滚轮继续前进的话不再放大
                else
                {
                    base.OnModifierMouseWheel(e);
                }
            }
            else
            {
                base.OnModifierMouseWheel(e);
            }
        }

        #endregion

        #region Fields && Properties

        /// <summary>
        /// 最小主刻度单位
        /// </summary>
        public double MinMainDelta
        {
            get { return (double)GetValue(MinMainDeltaProperty); }
            set { SetValue(MinMainDeltaProperty, value); }
        }

        public static readonly DependencyProperty MinMainDeltaProperty = 
			DependencyProperty.Register("MinMainDelta", typeof(double), typeof(BoundedMouseWheelZoomModifier));

        private bool canBounded;
        private bool isValidated;

        #endregion
    }
}
