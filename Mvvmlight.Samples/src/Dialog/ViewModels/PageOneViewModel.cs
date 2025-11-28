using Dialog.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Windows;
using System.Windows.Input;

namespace Dialog.ViewModels
{
    public class PageOneViewModel : ViewModelBase
    {
        #region Constructor

        public PageOneViewModel(IFrameNavigationService navigationService,
           MvvmDialogs.IDialogService dialogService)
        {
            mNavigationService = navigationService;
            mDialogService = dialogService;

            InitCommands();
        }

        #endregion

        #region Fields

        private INavigationService mNavigationService;
        private MvvmDialogs.IDialogService mDialogService;

        #endregion

        #region Properties

        private string confirmation;
        public string Confirmation
        {
            get => confirmation;
            private set { Set(() => Confirmation, ref confirmation, value); }
        }

        #endregion

        #region Commands

        public ICommand GoForwardCommand { get; private set; }
        public ICommand ShowMessageBoxWithMessageCommand { get; private set; }
        public ICommand ShowMessageBoxWithCaptionCommand { get; private set; }
        public ICommand ShowMessageBoxWithButtonCommand { get; private set; }
        public ICommand ShowMessageBoxWithIconCommand { get; private set; }
        public ICommand ShowMessageBoxWithDefaultResultCommand { get; private set; }

        private void InitCommands()
        {
            GoForwardCommand = new RelayCommand(() =>
            {
                mNavigationService.NavigateTo(PageKeyConstant.PageTwo, new string[] { "I", "love", "you" });
            });

            ShowMessageBoxWithMessageCommand = new RelayCommand(() =>
            {
                MessageBoxResult result = mDialogService.ShowMessageBox(
                    this,
                    "this is the text");
                UpdateResult(result);
            });

            ShowMessageBoxWithCaptionCommand = new RelayCommand(() =>
            {
                MessageBoxResult result = mDialogService.ShowMessageBox(
                    this,
                    "this is the text",
                    "This Is The Caption");
                UpdateResult(result);
            });

            ShowMessageBoxWithButtonCommand = new RelayCommand(() =>
            {
                MessageBoxResult result = mDialogService.ShowMessageBox(
                    this,
                    "This is the text.",
                    "This Is The Caption",
                    MessageBoxButton.OKCancel);

                UpdateResult(result);
            });

            ShowMessageBoxWithIconCommand = new RelayCommand(() =>
            {
                MessageBoxResult result = mDialogService.ShowMessageBox(
                    this,
                    "This is the text.",
                    "This Is The Caption",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Information);

                UpdateResult(result);
            });

            ShowMessageBoxWithDefaultResultCommand = new RelayCommand(() =>
            {
                MessageBoxResult result = mDialogService.ShowMessageBox(
                    this,
                    "This is the text.",
                    "This Is The Caption",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Information,
                    MessageBoxResult.Cancel);

                UpdateResult(result);
            });
        }

        #endregion

        #region Methods

        private void UpdateResult(MessageBoxResult result)
        {
            switch (result)
            {
                case MessageBoxResult.OK:
                    Confirmation = "We got confirmation to continue!";
                    break;

                case MessageBoxResult.Cancel:
                    Confirmation = string.Empty;
                    break;

                default:
                    throw new NotSupportedException($"{confirmation} is not supported.");
            }
        }

        #endregion
    }
}
