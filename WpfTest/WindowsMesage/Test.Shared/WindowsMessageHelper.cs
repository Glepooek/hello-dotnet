using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Test.Shared
{
    public class WindowsMessageHelper
    {
        public const int WM_COPYDATA = 0x004A;

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="hWnd">接收消息的窗体句柄</param>
        /// <param name="msg">消息类型，WM_COPYDATA</param>
        /// <param name="wParam">自定义数值</param>
        /// <param name="lParam">结构体，携带发送的消息内容</param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref CopyDataStruct lParam);

        /// <summary>
        /// 根据窗体名称查找窗体
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="windowName">接收消息的进程的主窗体名称</param>
        /// <param name="msg">消息内容</param>
        public static void SendMessageByMainWindowName(string windowName, string msg)
        {
            if (string.IsNullOrEmpty(msg))
            {
                return;
            }

            IntPtr hwnd = FindWindow(null, windowName);
            if (hwnd == IntPtr.Zero)
            {
                return;
            }

            CopyDataStruct cds = PackageMsg(msg);
            int result = SendMessage(hwnd, WM_COPYDATA, 0, ref cds);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="processName">接收消息的进程名称</param>
        /// <param name="msg">消息内容</param>
        public static void SendMessageByProcessName(string processName, string msg)
        {
            if (string.IsNullOrEmpty(msg))
            {
                return;
            }

            var processes = Process.GetProcessesByName(processName);
            if (processes == null || processes.Length == 0)
            {
                return;
            }

            var process = processes.FirstOrDefault(p => p.MainWindowHandle != IntPtr.Zero);
            if (process == null)
            {
                return;
            }

            var hwnd = process.MainWindowHandle;
            CopyDataStruct cds = PackageMsg(msg);
            int result = SendMessage(hwnd, WM_COPYDATA, 0, ref cds);
        }

        /// <summary>
        /// 包装消息
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <returns></returns>
        private static CopyDataStruct PackageMsg(string msg)
        {
            CopyDataStruct cds;
            cds.dwData = IntPtr.Zero;
            cds.lpData = msg;
            cds.cbData = Encoding.Default.GetBytes(msg).Length + 1;

            return cds;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CopyDataStruct
        {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }
    }
}