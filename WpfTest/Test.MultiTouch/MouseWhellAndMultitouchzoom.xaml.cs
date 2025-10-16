using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Test.MultiTouch
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MouseWhellAndMultiTouchZoom : Window
    {
        /// <summary>
        /// 当前缩放点
        /// </summary>
        private Point mZoomPosition;
        /// <summary>
        /// 开始移动的点
        /// </summary>
        private Point mStartMovingPosition;
        /// <summary>
        /// 最大缩放比例
        /// </summary>        
        private double maxZoom = 4.0;
        /// <summary>
        /// 最小缩放比例
        /// </summary> 
        private double minZoom = 1.0;
        /// <summary>
        /// 是否正在进行缩放
        /// </summary>
        private bool mIsZooming = false;

        public MouseWhellAndMultiTouchZoom()
        {
            InitializeComponent();
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (matrixTrans == null || mIsZooming)
            {
                Console.WriteLine("正在进行缩放！");
                return;
            }

            ZoomElement(e.NewValue / 100);

            e.Handled = true;
        }

        private void dec_Click(object sender, RoutedEventArgs e)
        {
            // 获取当前鼠标指针的位置
            //_currentPosition = new Point(image.ActualWidth / 2, image.ActualHeight / 2);
            ZoomElement(-1);
        }

        private void inc_Click(object sender, RoutedEventArgs e)
        {
            // 获取当前鼠标指针的位置
            //_currentPosition = new Point(image.ActualWidth / 2, image.ActualHeight / 2);
            ZoomElement(1);
        }

        private void image_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (matrixTrans == null || mIsZooming)
            {
                Console.WriteLine("正在进行缩放！");
                return;
            }

            // 获取当前鼠标指针的位置
            mZoomPosition = e.GetPosition(image);
            ZoomElement(e.Delta);

            e.Handled = true;
        }

        private void image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mStartMovingPosition = e.GetPosition(image);
            image.CaptureMouse();
        }

        private void image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            image.ReleaseMouseCapture();
        }

        private void image_MouseMove(object sender, MouseEventArgs e)
        {
            if (image.IsMouseCaptured)
            {
                var currentPoint = e.GetPosition(image);
                Matrix matrix = matrixTrans.Matrix;
                matrix.Translate(currentPoint.X - mStartMovingPosition.X, currentPoint.Y - mStartMovingPosition.Y);
                matrixTrans.Matrix = matrix;
            }
        }

        private void ZoomElement(int delta)
        {
            mIsZooming = true;
            // 获取鼠标滚轮的缩放比例
            double zoom = delta > 0 ? 1.1 : 0.9;

            // 创建缩放变换矩阵
            Matrix matrix = matrixTrans.Matrix;
            double currentZoom = matrix.M11;
            //Console.WriteLine($"{matrix.M11}, {matrix.M22}");

            // 计算缩放比例
            double newZoom = currentZoom * zoom;
            newZoom = Math.Max(newZoom, minZoom);
            newZoom = Math.Min(newZoom, maxZoom);

            matrix.ScaleAt(newZoom / currentZoom, newZoom / currentZoom, mZoomPosition.X, mZoomPosition.Y);

            // 应用变换矩阵，实现缩放效果
            matrixTrans.Matrix = matrix;
            slider.Value = matrix.M11 * 100;
            mIsZooming = false;
        }

        private void ZoomElement(double zoom)
        {
            mIsZooming = true;
            // 创建缩放变换矩阵
            Matrix matrix = matrixTrans.Matrix;
            double currentZoom = matrix.M11;

            // 计算缩放比例
            double newZoom = zoom;
            newZoom = Math.Max(newZoom, minZoom);
            newZoom = Math.Min(newZoom, maxZoom);

            matrix.ScaleAt(newZoom / currentZoom, newZoom / currentZoom, mZoomPosition.X, mZoomPosition.Y);

            // 应用变换矩阵，实现缩放效果
            matrixTrans.Matrix = matrix;
            mIsZooming = false;
        }

        private void touchPad_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = touchPad;
            e.Mode = ManipulationModes.Scale | ManipulationModes.Translate;
        }

        private void touchPad_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            element.Opacity = 0.5;
            Matrix matrix = matrixTrans.Matrix;

            mIsZooming = true;
            // 获取触控的缩放比例
            double currentZoom = matrix.M11;
            // 计算缩放比例
            double newZoom = currentZoom * e.DeltaManipulation.Scale.X;
            newZoom = Math.Max(newZoom, minZoom);
            newZoom = Math.Min(newZoom, maxZoom);

            matrix.ScaleAt(newZoom / currentZoom, newZoom / currentZoom, e.ManipulationOrigin.X, e.ManipulationOrigin.Y);
            matrix.Translate(e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y); ;
            // 应用变换矩阵，实现缩放效果
            matrixTrans.Matrix = matrix;
            slider.Value = matrix.M11 * 100;
            mIsZooming = false;
        }

        private void touchPad_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            FrameworkElement element = e.Source as FrameworkElement;
            element.Opacity = 1;
        }

        private void touchPad_ManipulationInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
        {

        }
    }
}