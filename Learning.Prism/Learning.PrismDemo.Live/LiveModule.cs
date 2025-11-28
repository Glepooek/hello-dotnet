using Learning.PrismDemo.Live.Views;
using Learning.PrismDemo.Utilities;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Learning.PrismDemo.Live
{
	public class LiveModule : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
		{
			var regionManager = containerProvider.Resolve<IRegionManager>();
			regionManager.RegisterViewWithRegion(RegionNameConstant.MENU_REGION_NAME, typeof(LiveMenuView));
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<LiveView>();
		}
	}
}
