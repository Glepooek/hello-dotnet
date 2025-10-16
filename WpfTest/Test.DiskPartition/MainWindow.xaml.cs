using System;
using System.Windows;

namespace Test.DiskPartition
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var drives = DiskHelper.MatchDriveLetterWithSerial("3150");
            foreach (var item in drives)
            {
                Console.WriteLine($"{item.Key}{item.Value}");
            }
        }

        //protected override void OnSourceInitialized(EventArgs e)
        //{
        //    base.OnSourceInitialized(e);
        //    HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
        //    source.AddHook(WndProc);
        //}

        //protected IntPtr WndProc(IntPtr hwnd, int m, IntPtr wParam, IntPtr lParam, ref bool handled)
        //{
        //    return IntPtr.Zero;
        //}
    }
}
