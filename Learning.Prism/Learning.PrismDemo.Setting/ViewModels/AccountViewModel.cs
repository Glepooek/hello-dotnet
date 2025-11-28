using Learning.PrismDemo.Controls.DialogServices;
using Learning.PrismDemo.Messages;
using Learning.PrismDemo.Models;
using Learning.PrismDemo.Utilities;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace Learning.PrismDemo.Setting.ViewModels
{
    public class AccountViewModel : ViewModelBase
    {
        #region Fields

        #endregion

        #region Commands

        public DelegateCommand LoginoutCommand { get; private set; }

        protected override void InitCommands()
        {
            base.InitCommands();

            LoginoutCommand = new DelegateCommand(OnLoginout);
        }

        private void OnLoginout()
        {
            mDialogService.ShowConfirm("您确定退出程序吗？", result =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    mEventAggregator.GetEvent<ViewStateChangedMessage>().Publish(ViewState.Login);
                    mRegionManager.RequestNavigate(RegionNameConstant.CONTENT_REGION_NAME, ViewNameConstant.LOGIN_VIEW_NAME);
                }
                else
                {

                }
            });
        }

        #endregion

        #region Constructor

        public AccountViewModel(IContainerExtension containerExtension,
                                                            IEventAggregator eventAggregator,
                                                            IDialogService dialogService,
                                                            IRegionManager regionManager)
            : base(containerExtension, eventAggregator, regionManager, dialogService)
        {

        }

        #endregion

        #region Methods


        #endregion
    }
}
