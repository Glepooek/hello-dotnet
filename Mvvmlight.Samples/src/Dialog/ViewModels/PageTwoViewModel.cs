using Dialog.DialogViews;
using Dialog.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmDialogs;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Dialog.ViewModels
{
    public class PageTwoViewModel : ViewModelBase
    {
        #region Constructor

        public PageTwoViewModel(IFrameNavigationService navigationService, IDialogService dialogService)
        {
            mNavigationService = navigationService;
            mDialogService = dialogService;

            InitCommands();
        }

        #endregion

        #region Fields

        private readonly IFrameNavigationService mNavigationService;
        private readonly IDialogService mDialogService;

        #endregion

        #region Properties

        public ObservableCollection<string> Texts { get; } = new ObservableCollection<string>();

        #endregion

        #region Commands

        public ICommand GoBackCommand { get; private set; }
        public ICommand GoForwardCommand { get; private set; }
        public ICommand ViewLoadedCommand { get; private set; }
        public ICommand ImplicitShowDialogCommand { get; private set; }
        public ICommand ExplicitShowDialogCommand { get; private set; }

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
            ImplicitShowDialogCommand = new RelayCommand(() =>
            {
                ShowDialog(viewModel => mDialogService.ShowDialog(this, viewModel));
            });
            ExplicitShowDialogCommand = new RelayCommand(() =>
            {
                ShowDialog(viewModel => mDialogService.ShowDialog<AddTextDialog>(this, viewModel));
            });
        }

        #endregion

        #region Methods

        private void ShowDialog(Func<AddTextDialogViewModel, bool?> showDialog)
        {
            var dialogViewModel = new AddTextDialogViewModel();

            bool? success = showDialog(dialogViewModel);
            if (success == true)
            {
                Texts.Add(dialogViewModel.Text);
            }
        }

        #endregion
    }
}
