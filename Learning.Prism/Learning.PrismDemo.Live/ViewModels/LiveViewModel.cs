using Learning.PrismDemo.Utilities;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace Learning.PrismDemo.Live.ViewModels
{
    public class LiveViewModel : ViewModelBase
    {
        #region Fields

        #endregion

        #region Commands

        protected override void InitCommands()
        {
            base.InitCommands();

        }

        #endregion

        #region Constructor

        public LiveViewModel(IContainerExtension containerExtension,
                            IEventAggregator eventAggregator,
                            IRegionManager regionManager,
                            IDialogService dialogService)
            : base(containerExtension, eventAggregator, regionManager, dialogService)
        {

        }

        #endregion

        #region Methods


        #endregion
    }
}
