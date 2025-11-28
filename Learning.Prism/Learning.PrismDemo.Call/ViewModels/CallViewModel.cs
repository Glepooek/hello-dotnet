using Learning.PrismDemo.Controls.DialogServices;
using Learning.PrismDemo.Utilities;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System;

namespace Learning.PrismDemo.Call.ViewModels
{
    public class CallViewModel : ViewModelBase
    {
        #region Fields


        #endregion

        #region Properties

        private TimeSpan m_SelectedTime;
        public TimeSpan SelectedTime
        {
            get
            {
                return m_SelectedTime;
            }
            set
            {
                if (m_SelectedTime != value)
                {
                    m_SelectedTime = value;
                    RaisePropertyChanged();
                }
            }
        }

        private bool m_IsBusy;
        public bool IsBusy
        {
            get
            {
                return m_IsBusy;
            }
            set
            {
                if (m_IsBusy != value)
                {
                    m_IsBusy = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region Commands

        public DelegateCommand<object> SelectedTimeCommand { get; private set; }
        public DelegateCommand StartCommand { get; private set; }
        public DelegateCommand StopCommand { get; private set; }

        protected override void InitCommands()
        {
            base.InitCommands();

            SelectedTimeCommand = new DelegateCommand<object>(OnSelectedTime);
            StartCommand = new DelegateCommand(OnStart);
            StopCommand = new DelegateCommand(OnStop);
        }

        private void OnStop()
        {
            IsBusy = false;
        }

        private void OnStart()
        {
            IsBusy = true;
        }

        private void OnSelectedTime(object obj)
        {
            mDialogService.ShowNotification(obj.ToString());
        }

        #endregion

        #region Constructor

        public CallViewModel(IContainerExtension containerExtension,
                            IEventAggregator eventAggregator,
                            IDialogService dialogService)
            : base(containerExtension, eventAggregator, dialogService)
        {
        }

        #endregion
    }
}
