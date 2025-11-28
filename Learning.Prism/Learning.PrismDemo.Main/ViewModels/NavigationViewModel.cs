using Learning.PrismDemo.Utilities;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace Learning.PrismDemo.Main.ViewModels
{
    public class NavigationViewModel : ViewModelBase
    {
        #region Fields


        #endregion

        #region Commands

        public DelegateCommand<object> MenuSelectedCommand { get; private set; }

        protected override void InitCommands()
        {
            base.InitCommands();

            MenuSelectedCommand = new DelegateCommand<object>(OnMenuSelected);
        }

        private void OnMenuSelected(object arg)
        {
            if (arg is string moduleName)
            {
                if (moduleName.Equals(ModuleNameConstant.CALL_MODULE_NAME))
                {
                    mRegionManager.RequestNavigate(RegionNameConstant.CONTENT_REGION_NAME, ViewNameConstant.CALL_VIEW_NAME);
                }
                else if (moduleName.Equals(ModuleNameConstant.LIVE_MODULE_NAME))
                {
                    mRegionManager.RequestNavigate(RegionNameConstant.CONTENT_REGION_NAME, ViewNameConstant.LIVE_VIEW_NAME);
                }
                else if (moduleName.Equals(ModuleNameConstant.ANIMATION_MODULE_NAME))
                {
                    mRegionManager.RequestNavigate(RegionNameConstant.CONTENT_REGION_NAME, ViewNameConstant.ANIMATION_VIEW_NAME);
                }
            }
        }

        #endregion

        #region Constructor

        public NavigationViewModel(IContainerExtension containerExtension,
                                IRegionManager regionManager,
                                IDialogService dialogService)
            : base(containerExtension, regionManager, dialogService)
        {

        }

        #endregion

        #region Methods


        #endregion
    }
}
