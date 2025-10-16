using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace Unipus.Student.Client.WebView2Interop
{
    /// <summary>
    /// 自定义宿主类，用于向网页注册C#对象，供JS调用
    /// </summary>
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class DotnetInterop
    {
        #region Fields

        private readonly Window mWindow;

        #endregion

        #region Constructor

        public DotnetInterop(Window window)
        {
            mWindow = window;
            target = new WindowInteropHelper(mWindow).Handle;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 关闭窗口
        /// </summary>
        public void CloseWindow()
        {
            mWindow.Close();
        }

        /// <summary>
        /// 最小化窗口
        /// </summary>
        public void MinimizeWindow()
        {
            mWindow.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// 最大化、还原窗口
        /// </summary>
        public void MaximizeWindow()
        {
            mWindow.WindowState = mWindow.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        /// <summary>
        /// 拖动窗体
        /// </summary>
        public void MouseDownDrag()
        {
            ReleaseCapture();
            SendMessage(target, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        #endregion

        #region Win32API

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        private readonly IntPtr target;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        #endregion
    }
}
