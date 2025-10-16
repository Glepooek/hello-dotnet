using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using static Test.Shared.WindowsMessageHelper;

namespace Test.SubWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (App.IsProcessStarted)
            {
                /**
                 * 发送进程间消息，设置窗口this.ShowInTaskbar = false;时，
                 * 通过进程名发送消息时，进程的MainWindowHandle == IntPtr.Zero，无法通信，
                 * 此时，只能用通过主窗口名发送消息
                 */
                this.ShowInTaskbar = false;
                this.WindowState = WindowState.Normal;
                this.Left = SystemParameters.WorkArea.Width;
            }
            this.Loaded += (s, e) =>
            {
                //this.Hide();
            };
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            if (PresentationSource.FromVisual(this) is HwndSource hwd)
            {
                hwd.AddHook(new HwndSourceHook(this.WndProc));
            }
        }

        IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_COPYDATA)
            {
                var data = Marshal.PtrToStructure(lParam, typeof(CopyDataStruct));
                if (data != null)
                {
                    CopyDataStruct cds = (CopyDataStruct)data;
                    if (!string.IsNullOrEmpty(cds.lpData))
                    {
                        this.Left = 0;
                        this.ShowInTaskbar = true;
                        this.WindowState = WindowState.Maximized;
                        this.Show();
                        MessageBox.Show(cds.lpData);
                    }
                }
            }
            return hwnd;
        }
    }
}
