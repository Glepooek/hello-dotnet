using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Navigation.Services;
using System.Windows.Input;

namespace Navigation.ViewModels
{
    public class PageThreeViewModel : ViewModelBase
    {
        #region Constructor

        public PageThreeViewModel(IFrameNavigationService navigationService)
        {
            mNavigationService = navigationService;
            InitCommands();
        }

        #endregion

        #region Fields

        private IFrameNavigationService mNavigationService;

        #endregion

        #region Commands

        public ICommand GoBackCommand { get; private set; }
        public ICommand GoForwardCommand { get; private set; }
        public ICommand ViewLoadedCommand { get; private set; }

        private void InitCommands()
        {
            GoBackCommand = new RelayCommand(() =>
            {
                mNavigationService.GoBack();
            });
            GoForwardCommand = new RelayCommand(() =>
            {

            });
            ViewLoadedCommand = new RelayCommand(() =>
            {
                var temp = mNavigationService.Parameter;
            });
        }

        #endregion
    }
}
