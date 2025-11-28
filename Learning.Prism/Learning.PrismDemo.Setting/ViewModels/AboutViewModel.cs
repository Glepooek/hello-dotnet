using Learning.PrismDemo.Utilities;
using Prism.Ioc;
using Prism.Services.Dialogs;

namespace Learning.PrismDemo.Setting.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        #region Constructor

        public AboutViewModel(IContainerExtension containerExtension, IDialogService dialogService)
            : base(containerExtension, dialogService)
        {

        }

        #endregion
    }
}
