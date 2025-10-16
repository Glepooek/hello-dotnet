using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

// https://www.cnblogs.com/socujun/articles/4896440.html
// http://www.javashuo.com/article/p-qkcsuqwd-en.html
// https://docs.microsoft.com/zh-cn/windows/win32/msi/registry-table?redirectedfrom=MSDN
// https://www.cnblogs.com/alannever/p/12715390.html
// https://blog.csdn.net/bruce135lee/article/details/78299755
// https://www.cnblogs.com/luguangguang/p/15176237.html

namespace Test.MainWpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Mutex mMutex;
        private const string mAppName = "TestMainWpfApp";

        public App()
        {
            this.Startup += (s, e) =>
            {
                if (e.Args != null && e.Args.Length > 0)
                {
                    MessageBox.Show(e.Args[0]);
                }

                // 避免启动多个程序
                mMutex = new Mutex(true, mAppName, out bool createdNew);
                if (!createdNew)
                {
                    //App.Current.Shutdown();
                    Process.GetCurrentProcess().Kill();
                }
                else
                {
                    WriteRegistry();
                }
            };
        }

        private void WriteRegistry()
        {
            // 自定义的文件后缀（扩展名）
            string fileExtensionName = ".cdb";
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            // 获取启动了应用程序的可执行文件的路径及文件名
            // string exeName = Process.GetCurrentProcess().MainModule.FileName;
            string fileName = $"{exePath}\\Test.MainWpfApp.exe";

            // 注册文件后缀类型
            using RegistryKey? extensionOpenKey = Registry.ClassesRoot.OpenSubKey(fileExtensionName);
            if (extensionOpenKey == null)
            {
                using RegistryKey extensionKey = Registry.ClassesRoot.CreateSubKey(fileExtensionName);
                extensionKey.SetValue("", mAppName, RegistryValueKind.String);
            }

            using RegistryKey? appOpenKey = Registry.ClassesRoot.OpenSubKey(mAppName);
            if (appOpenKey == null)
            {
                using RegistryKey appKey = Registry.ClassesRoot.CreateSubKey(mAppName);
                appKey.SetValue("", "测试程序");
                // 设置图标
                appKey.CreateSubKey("DefaultIcon")
                   .SetValue("", $"{exePath}\\AIClass.ico");
                // 设置默认启动程序
                appKey.CreateSubKey(@"Shell\Open\Command")
                   .SetValue("", $"{fileName} \"%1\"", RegistryValueKind.ExpandString);
                SHChangeNotify(0x8000000, 0, IntPtr.Zero, IntPtr.Zero);
            }
        }

        /// <summary>
        /// 自定义文件格式写入注册表后，即时刷新注册表使其生效
        /// </summary>
        /// <param name="wEventId"></param>
        /// <param name="uFlags"></param>
        /// <param name="dwItem1"></param>
        /// <param name="dwItem2"></param>
        [DllImport("shell32.dll")]
        public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);
    }
}
