using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Test.DragBehaviorDemo
{
    /// <summary>
    /// 控件在Canvas中的拖动行为
    /// </summary>
    [Obsolete("DragMoveBehavior已过时，请使用DragMoveBehavior1")]
    public class DragMoveBehavior : Behavior<FrameworkElement>
    {
        #region Fields

        /// <summary>
        /// 控件所属的Canvas
        /// </summary>
        private Canvas? canvas;
        private bool isDragging = false;
        private Point prePosition;

        #endregion

        #region Protected Methods

        protected override void OnAttached()
        {
            base.OnAttached();

            base.AssociatedObject.PreviewMouseLeftButtonDown += MouseLeftButtonDown;
            base.AssociatedObject.PreviewMouseLeftButtonUp += MouseLeftButtonUp;
            base.AssociatedObject.PreviewMouseMove += MouseMove;

            var parent = VisualTreeHelper.GetParent(AssociatedObject);
            if (parent == null)
            {
                // 这种适用于在后台代码中将控件添加到Canvas中
                // 在控件中为静态变量OwnedByCanvas赋值其所属的Canvas
                // 在控件中在调用InitializeComponent方法前，为Tag赋值OwnedByCanvas
                canvas = base.AssociatedObject.Tag as Canvas;
            }
            else
            {
                canvas = parent as Canvas;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            base.AssociatedObject.PreviewMouseLeftButtonDown -= MouseLeftButtonDown;
            base.AssociatedObject.PreviewMouseLeftButtonUp -= MouseLeftButtonUp;
            base.AssociatedObject.PreviewMouseMove -= MouseMove;
        }

        #endregion

        #region Private Methods

        private void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isDragging)
            {
                return;
            }

            isDragging = true;
            prePosition = GetPosition(e);
            AssociatedObject.CaptureMouse();
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDragging)
            {
                return;
            }

            Point currentPosition = GetPosition(e);
            double offsetX = currentPosition.X - prePosition.X;
            double offsetY = currentPosition.Y - prePosition.Y;
            double oldLeft = Canvas.GetLeft(AssociatedObject);
            double oldTop = Canvas.GetTop(AssociatedObject);
            double newLeft = double.IsNaN(oldLeft) ? 0 : oldLeft + offsetX;
            double newTop = double.IsNaN(oldTop) ? 0 : oldTop + offsetY;
            Canvas.SetLeft(base.AssociatedObject, newLeft);
            Canvas.SetTop(base.AssociatedObject, newTop);
            prePosition = currentPosition;
        }

        private void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!isDragging)
            {
                return;
            }

            isDragging = false;
            AssociatedObject.ReleaseMouseCapture();
        }

        private Point GetPosition(MouseEventArgs e)
        {
            return e.GetPosition(canvas);
        }

        #endregion
    }
}
