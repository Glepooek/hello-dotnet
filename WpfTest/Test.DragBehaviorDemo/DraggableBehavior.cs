using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Test.DragBehaviorDemo
{
    public class DraggableBehavior : Behavior<FrameworkElement>
    {
        private Point mouseOffset;

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseLeftButtonDown += MouseLeftButtonDown;
            this.AssociatedObject.MouseMove += MouseMove;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.MouseLeftButtonDown -= MouseLeftButtonDown;
            this.AssociatedObject.MouseMove -= MouseMove;
        }

        private void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.mouseOffset = e.GetPosition(this.AssociatedObject);
            this.AssociatedObject.CaptureMouse();
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var point = e.GetPosition(null);
                this.AssociatedObject.Margin = new Thickness(
                    point.X - this.mouseOffset.X,
                    point.Y - this.mouseOffset.Y,
                    0,
                    0);
            }
            else
            {
                this.AssociatedObject.ReleaseMouseCapture();
            }
        }
    }
}
