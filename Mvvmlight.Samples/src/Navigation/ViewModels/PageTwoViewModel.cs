using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Navigation.Services;
using System.Windows.Input;

namespace Navigation.ViewModels
{
    public class PageTwoViewModel : ViewModelBase
    {
        #region Constructor

        public PageTwoViewModel(IFrameNavigationService navigationService)
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
                mNavigationService.NavigateTo(PageKeyConstant.PageThree, 56);
            });
            ViewLoadedCommand = new RelayCommand(() =>
            {
                var temp = mNavigationService.Parameter;
            });
        }

        #endregion
    }
}
