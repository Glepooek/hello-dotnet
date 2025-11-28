using Abt.Controls.SciChart.Visuals.Axes;
using System;

namespace Digihail.Controls
{
    public class CustomLabelProvider<T> : NumericLabelProvider
    {
        #region Constructor

        public CustomLabelProvider(T[] xLabels)
        {
            _xLabels = xLabels;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called when the label formatted is initialized as it is attached to the parent axis, with the parent axis instance
        /// </summary>
        /// <param name="parentAxis">The parent <see cref="IAxis" /> instance</param>
        public override void Init(IAxis parentAxis)
        {
            // TODO: (Optional) Any initialization when the LabelProvider is attached to the axis
            base.Init(parentAxis);
        }

        /// <summary>
        /// Called at the start of an axis render pass, before any labels are formatted for the current draw operation
        /// </summary>
        public override void OnBeginAxisDraw()
        {
            // TODO: (Optional) Any initialization before each drawing pass
            base.OnBeginAxisDraw();
        }

        /// <summary>
        /// Formats a label for the axis from the specified data-value passed in
        /// </summary>
        /// <param name="dataValue">The data-value to format</param>
        /// <returns>
        /// The formatted label string
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override string FormatLabel(IComparable dataValue)
        {
            int index = (int)Convert.ChangeType(dataValue, typeof(int));
            if (index >= 0 && index < _xLabels.Length)
                return (_xLabels[index]).ToString();

            return index.ToString();
        }

        /// <summary>
        /// Formats a label for the cursor, from the specified data-value passed in
        /// </summary>
        /// <param name="dataValue">The data-value to format</param>
        /// <returns>
        /// The formatted cursor label string
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override string FormatCursorLabel(IComparable dataValue)
        {
            int index = (int)Convert.ChangeType(dataValue, typeof(int));
            if (index >= 0 && index < _xLabels.Length)
                return (_xLabels[index]).ToString();

            return index.ToString();
        }

        #endregion

        #region Fields & Properties

        private readonly T[] _xLabels;

        #endregion
    }
}
