using Learning.PrismDemo.Call.Views;
using Learning.PrismDemo.Utilities;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Learning.PrismDemo.Call
{
	public class CallModule : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
		{
			var regionManager = containerProvider.Resolve<IRegionManager>();
			regionManager.RegisterViewWithRegion(RegionNameConstant.MENU_REGION_NAME, typeof(CallMenuView));
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<CallView>();
		}
	}
}
