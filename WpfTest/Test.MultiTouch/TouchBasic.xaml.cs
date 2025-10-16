using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Test.MultiTouch
{
    /// <summary>
    /// TouchBasic.xaml 的交互逻辑
    /// </summary>
    public partial class TouchBasic : Window
    {
        private Dictionary<int, Ellipse> movingEllipses = new Dictionary<int, Ellipse>();
        private Random rd = new Random();

        public TouchBasic()
        {
            InitializeComponent();
        }

        private void touchPad_TouchDown(object sender, TouchEventArgs e)
        {
            // 生成随机颜色的圆
            Ellipse ellipse = new Ellipse
            {
                Width = 30,
                Height = 30,
                Stroke = Brushes.White,
                Fill = new SolidColorBrush(Color.FromRgb((byte)rd.Next(0, 255), (byte)rd.Next(0, 255), (byte)rd.Next(0, 255)))
            };

            // 获取触控点并设置圆的位置
            TouchPoint touchPoint = e.GetTouchPoint(touchPad);
            Canvas.SetTop(ellipse, touchPoint.Bounds.Top);
            Canvas.SetLeft(ellipse, touchPoint.Bounds.Left);

            movingEllipses[e.TouchDevice.Id] = ellipse;

            touchPad.Children.Add(ellipse);
        }

        private void touchPad_TouchUp(object sender, TouchEventArgs e)
        {
            Ellipse ellipse = movingEllipses[e.TouchDevice.Id];
            touchPad.Children.Remove(ellipse);
            movingEllipses.Remove(e.TouchDevice.Id);
        }

        private void touchPad_TouchMove(object sender, TouchEventArgs e)
        {
            Ellipse ellipse = movingEllipses[e.TouchDevice.Id];
            TouchPoint touchPoint = e.GetTouchPoint(touchPad);
            Canvas.SetTop(ellipse, touchPoint.Bounds.Top);
            Canvas.SetLeft(ellipse, touchPoint.Bounds.Left);
        }
    }
}