using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;

namespace Test.DragBehaviorDemo
{
    /// <summary>
    /// 控件在父控件或Panel中的拖动行为
    /// </summary>
    public class DragMoveBehavior1 : Behavior<FrameworkElement>
    {
        #region Fields

        private bool isDown;
        private Point prePosition = new Point();
        private Point startPoint = new Point();

        /// <summary>
        /// Occurs when a drag gesture is finished.
        /// </summary>
        public event DragFinishedEventHandler DragFinished;

        #endregion

        #region DependencyProperty

        public FrameworkElement Parent
        {
            get { return (FrameworkElement)GetValue(ParentProperty); }
            set { SetValue(ParentProperty, value); }
        }

        public static readonly DependencyProperty ParentProperty =
            DependencyProperty.Register("Parent", typeof(FrameworkElement), typeof(DragMoveBehavior1), new PropertyMetadata(null));

        #endregion

        #region Protected Methods

        protected override void OnAttached()
        {
            base.OnAttached();

            base.AssociatedObject.MouseLeftButtonDown += OnMouseLeftButtonDown;
            base.AssociatedObject.MouseLeftButtonUp += OnMouseLeftButtonUp;
            base.AssociatedObject.MouseMove += OnMouseMove;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            base.AssociatedObject.MouseLeftButtonDown -= OnMouseLeftButtonDown;
            base.AssociatedObject.MouseLeftButtonUp -= OnMouseLeftButtonUp;
            base.AssociatedObject.MouseMove -= OnMouseMove;
        }

        #endregion

        #region Private Methods

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!isDown)
            {
                return;
            }

            Point currentPosition = GetPosition(e);
            double offsetx = currentPosition.X - prePosition.X;
            double offsety = currentPosition.Y - prePosition.Y;
            double left = AssociatedObject.Margin.Left;
            double top = AssociatedObject.Margin.Top;
            double right = AssociatedObject.Margin.Right;
            double bottom = AssociatedObject.Margin.Bottom;
            double l = double.IsNaN(left) ? 0 : left + offsetx;
            double t = double.IsNaN(top) ? 0 : top + offsety;
            double r = double.IsNaN(left) ? 0 : right - offsetx;
            double b = double.IsNaN(top) ? 0 : bottom - offsety;
            AssociatedObject.Margin = new Thickness(l, t, r, b);
            prePosition = currentPosition;
            e.Handled = true;
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!isDown)
            {
                return;
            }
            isDown = false;
            AssociatedObject.ReleaseMouseCapture();
            e.Handled = true;

            Point endPoint = GetPosition(e);
            if (startPoint != endPoint)
            {
                DragFinished(this, new DragFinishedEventArgs(AssociatedObject.Margin));
            }
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isDown)
            {
                return;
            }

            isDown = true;
            prePosition = GetPosition(e);
            startPoint = GetPosition(e);
            AssociatedObject.CaptureMouse();
            e.Handled = true;
        }

        private Point GetPosition(MouseEventArgs e)
        {
            return e.GetPosition(Parent);
        }

        #endregion
    }

    public delegate void DragFinishedEventHandler(object sender, DragFinishedEventArgs e);
    public class DragFinishedEventArgs : RoutedEventArgs
    {
        public DragFinishedEventArgs(Thickness margin) : base()
        {
            this.Margin = margin;
        }

        public Thickness Margin { get; set; }
    }
}
