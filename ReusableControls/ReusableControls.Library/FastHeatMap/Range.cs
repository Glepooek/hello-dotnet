using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digihail.Controls
{
    public class Range
    {
        #region Constructor

        public Range(double min, double max)
        {
            Min = min;
            Max = max;
        }

        public Range()
        {
        }

        #endregion

        #region Fields & Properies

        /// <summary>
        /// 最大值，定义域为0-1
        /// </summary>
        public double Min { get; set; }

        /// <summary>
        /// 最小值，定义域为0-1
        /// </summary>
        public double Max { get; set; }

        #endregion
    }
}
