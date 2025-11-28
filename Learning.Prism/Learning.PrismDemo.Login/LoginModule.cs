using Learning.PrismDemo.Login.Views;
using Learning.PrismDemo.Utilities;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Learning.PrismDemo.Login
{
	public class LoginModule : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
		{
			var regionManager = containerProvider.Resolve<IRegionManager>();
			regionManager.RequestNavigate(RegionNameConstant.CONTENT_REGION_NAME, ViewNameConstant.LOGIN_VIEW_NAME);
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<LoginView>();
		}
	}
}
