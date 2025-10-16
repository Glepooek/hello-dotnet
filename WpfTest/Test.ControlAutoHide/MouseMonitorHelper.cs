using System.Runtime.InteropServices;
using System.Windows;

namespace Test.ControlAutoHide
{
    public class MouseMonitorHelper
    {
        /// <summary>
        /// 鼠标的当前位置
        /// </summary>
        private static Point mMouseCurrentPosition;
        /// <summary>
        /// 检测鼠标位置的次数
        /// </summary>
        public static int CheckCount { get; set; }

        /// <summary>
        /// 判断鼠标是否移动    
        /// </summary>
        /// <returns></returns>
        public static bool HaveUsedTo()
        {
            Point point = GetMousePoint();
            if (point == mMouseCurrentPosition)
            {
                return false;
            }

            mMouseCurrentPosition = point;
            return true;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MPoint
        {
            public int X;
            public int Y;
            public MPoint(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }
        
        /// <summary>
        /// 获取鼠标在屏幕中的当前位置      
        /// </summary>
        /// <returns></returns>
        public static Point GetMousePoint()
        {
            GetCursorPos(out MPoint mpt);
            Point p = new Point(mpt.X, mpt.Y);
            return p;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool GetCursorPos(out MPoint mpt);
    }
}
