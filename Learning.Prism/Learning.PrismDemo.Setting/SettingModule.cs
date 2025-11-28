using Learning.PrismDemo.Setting.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace Learning.PrismDemo.Setting
{
	[Module(ModuleName = "Setting", OnDemand = true)]
	public class SettingModule : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
		{

		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<SettingView>();
		}
	}
}
