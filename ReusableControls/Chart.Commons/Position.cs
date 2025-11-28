using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digihail.Controls
{
    /// <summary>
    /// 位置，用于确定标记的位置
    /// </summary>
    public class Position
    {
        public Position(IComparable x, IComparable y)
        {
            X = x;
            Y = y;
        }

        public Position()
        {
        }

        public IComparable X { get; set; }

        public IComparable Y { get; set; }
    }
}
