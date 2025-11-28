using System;
using System.Windows;

namespace UsingObservableObject
{
	public class RegionManager
    {
        /// <summary>
        /// Identifies the RegionContext attached property.
        /// </summary>
        public static readonly DependencyProperty RegionContextProperty =
            DependencyProperty.RegisterAttached("RegionContext", typeof(object), typeof(RegionManager), new PropertyMetadata(defaultValue: null, propertyChangedCallback: OnRegionContextChanged));

        private static void OnRegionContextChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            var context = RegionContext.GetObservableContext(depObj);
            if (context.Value != e.NewValue)
            {
                context.Value = e.NewValue;
            }
        }

        /// <summary>
        /// Gets the value of the <see cref="RegionContextProperty"/> attached property.
        /// </summary>
        /// <param name="target">The target element.</param>
        /// <returns>The region context to pass to the contained views.</returns>
        public static object GetRegionContext(DependencyObject target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            return target.GetValue(RegionContextProperty);
        }

        /// <summary>
        /// Sets the <see cref="RegionContextProperty"/> attached property.
        /// </summary>
        /// <param name="target">The target element.</param>
        /// <param name="value">The value.</param>
        public static void SetRegionContext(DependencyObject target, object value)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            target.SetValue(RegionContextProperty, value);
        }
    }
}
