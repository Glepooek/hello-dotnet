using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Learning.PrismDemo.Controls.Behaviors
{
	/// <summary>
	/// ListBox自动滚动到底部行为
	/// </summary>
	public class ListBoxScrollToBottomBehavior : Behavior<ListBox>
	{
		protected override void OnAttached()
		{
			base.OnAttached();
			// 只有ICollectionView类型才有CollectionChanged事件
			(AssociatedObject.Items as ICollectionView).CollectionChanged += ListBoxScrollToBottomBehavior_CollectionChanged;
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			(AssociatedObject.Items as ICollectionView).CollectionChanged -= ListBoxScrollToBottomBehavior_CollectionChanged;
		}

		private void ListBoxScrollToBottomBehavior_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (AssociatedObject.HasItems)
			{
				// AssociatedObject.ScrollIntoView(AssociatedObject.Items[AssociatedObject.Items.Count - 1]);
				AssociatedObject.ScrollIntoView(AssociatedObject.Items[^1]);
			}
		}
	}
}
