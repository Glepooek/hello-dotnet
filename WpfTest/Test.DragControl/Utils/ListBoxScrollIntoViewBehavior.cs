using Microsoft.Xaml.Behaviors;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Controls;

namespace Test.DragControl.Utils
{
    /// <summary>
    /// ListBox自动滚动到具体项行为。
    /// </summary>
    /// <remarks>
    /// 增加时滚动到底部，移动时滚动的移动位置。
    /// </remarks>
    public class ListBoxScrollIntoViewBehavior : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            ((ICollectionView)AssociatedObject.Items).CollectionChanged += OnCollectionChanged;
        }

        protected override void OnDetaching()
        {
            ((ICollectionView)AssociatedObject.Items).CollectionChanged -= OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (AssociatedObject.HasItems)
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    AssociatedObject.ScrollIntoView(AssociatedObject.Items[AssociatedObject.Items.Count - 1]);
                }
                else if (e.Action == NotifyCollectionChangedAction.Move)
                {
                    AssociatedObject.ScrollIntoView(e.NewItems[0]);
                }
            }
        }
    }
}
