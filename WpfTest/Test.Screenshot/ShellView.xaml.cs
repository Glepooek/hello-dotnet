using System;
using System.Windows;
using System.Windows.Interop;

namespace Test.Screenshot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();

            this.Loaded += (o, e) =>
             {
                 // 不要在构造函数中获取句柄，此时Visual还没产生
                 // 获取窗口句柄
                 IntPtr handle = new WindowInteropHelper(this).EnsureHandle();

                 // 获取控件句柄
                 IntPtr hwnd = ((HwndSource)PresentationSource.FromVisual(this.btnScreenshot)).Handle;

                 //if (hwnd != IntPtr.Zero)
                 //{
                 //    MessageBox.Show("获取到按钮句柄！！");
                 //}
             };
        }
    }
}
