using GalaSoft.MvvmLight.Command;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Test.DragControl.Utils
{
    public class LoseFocusOnLeftClickBehavior : Behavior<FrameworkElement>
    {
        private readonly MouseBinding mMouseLeftClickBinding;
        private readonly Label mEmptyLabelControl = new Label()
        {
            Focusable = true,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Width = 0
        };

        public LoseFocusOnLeftClickBehavior()
        {
            mMouseLeftClickBinding = new MouseBinding(new RelayCommand(LoseFocus), new MouseGesture(MouseAction.LeftClick));
        }

        protected override void OnAttached()
        {
            AssociatedObject.InputBindings.Add(mMouseLeftClickBinding);
            AssociatedObject.Loaded += OnAssociatedObjectLoaded;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.InputBindings.Remove(mMouseLeftClickBinding);
            AssociatedObject.Loaded -= OnAssociatedObjectLoaded;
        }

        private void OnAssociatedObjectLoaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.Loaded -= OnAssociatedObjectLoaded;
            AttachEmptyControl();
        }

        private void AttachEmptyControl()
        {
            DependencyObject currentElement = AssociatedObject;
            while (!(currentElement is Panel))
            {
                currentElement = VisualTreeHelper.GetChild(currentElement, 0);
            }

            ((Panel)currentElement).Children.Add(mEmptyLabelControl);
        }

        private void LoseFocus()
        {
            mEmptyLabelControl.Focus();
        }
    }
}
