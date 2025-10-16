using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

// https://www.cnblogs.com/alvinyue/p/3600867.html

namespace Test.ControlAutoHide
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
            this.MouseMove += DockPanel_MouseMove;
        }

        private DispatcherTimer mTimer;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 设置鼠标隐藏、显示
            this.mTimer = new DispatcherTimer();
            this.mTimer.Tick += Timer_MouseMove_Tick;
            this.mTimer.Interval = new TimeSpan(0, 0, 1);
            this.mTimer.Start();
        }

        private void Timer_MouseMove_Tick(object? sender, EventArgs e)
        {
            try
            {
                if (!MouseMonitorHelper.HaveUsedTo())
                {
                    MouseMonitorHelper.CheckCount++;
                    if (MouseMonitorHelper.CheckCount == 3)
                    {
                        MouseMonitorHelper.CheckCount = 0;

                        this.test.Visibility = Visibility.Hidden;
                        Mouse.OverrideCursor = Cursors.None;
                    }
                }
                else
                {
                    MouseMonitorHelper.CheckCount = 0;
                }
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        private void DockPanel_MouseMove(object sender, MouseEventArgs e)
        {
            this.test.Visibility = Visibility.Visible;
            Mouse.OverrideCursor = Cursors.Arrow;
        }
    }
}
