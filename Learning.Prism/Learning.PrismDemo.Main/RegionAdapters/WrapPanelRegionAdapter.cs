using Prism.Regions;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace Learning.PrismDemo.Main.RegionAdapters
{
	public class WrapPanelRegionAdapter : RegionAdapterBase<WrapPanel>
	{
		public WrapPanelRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
			: base(regionBehaviorFactory)
		{

		}

		protected override void Adapt(IRegion region, WrapPanel regionTarget)
		{
			region.Views.CollectionChanged += (o, e) =>
			 {
				 if (e.NewItems == null)
				 {
					 return;
				 }

				 if (e.Action == NotifyCollectionChangedAction.Add)
				 {
					 foreach (FrameworkElement element in e.NewItems)
					 {
						 regionTarget.Children.Add(element);
					 }
				 }
				 else if (e.Action == NotifyCollectionChangedAction.Remove)
				 {
					 foreach (FrameworkElement element in e.NewItems)
					 {
						 regionTarget.Children.Remove(element);
					 }
				 }
			 };
		}

		protected override IRegion CreateRegion()
		{
			return new AllActiveRegion();
		}
	}
}
