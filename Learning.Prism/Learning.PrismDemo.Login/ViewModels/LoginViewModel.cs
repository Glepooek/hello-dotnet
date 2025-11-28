using Learning.PrismDemo.Controls.DialogServices;
using Learning.PrismDemo.Logs;
using Learning.PrismDemo.Messages;
using Learning.PrismDemo.Models;
using Learning.PrismDemo.Services;
using Learning.PrismDemo.Utilities;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Services.Dialogs;

namespace Learning.PrismDemo.Login.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        #region Fields

        private readonly IService mService;

        #endregion

        #region Properties

        private UserInfo m_UserInfo;
        public UserInfo UserInfo
        {
            get
            {
                return m_UserInfo;
            }
            set
            {
                m_UserInfo = value;
                RaisePropertyChanged(nameof(UserInfo));
            }
        }

        private bool m_IsNetworkAvailable = false;
        /// <summary>
        /// 网络连接是否可用
        /// </summary>
        public bool IsNetworkAvailable
        {
            get
            {
                return m_IsNetworkAvailable;
            }
            set
            {
                if (m_IsNetworkAvailable != value)
                {
                    m_IsNetworkAvailable = value;
                    RaisePropertyChanged(nameof(IsNetworkAvailable));
                }
            }
        }

        #endregion

        #region Commands

        public DelegateCommand LoginCommand { get; private set; }

        protected override void InitCommands()
        {
            try
            {
                base.InitCommands();

                LoginCommand = new DelegateCommand(OnLogin, CanLogin).ObservesCanExecute(() => IsNetworkAvailable);
            }
            catch (System.Exception ex)
            {
                mTraceHelper.Error(ex.Message, ex);
            }
        }

        private void OnLogin()
        {
            try
            {
                if (mService.LoginIn(UserInfo.UserName, UserInfo.Password, "http://192.168.10.72:29003"))
                {
                    mTraceHelper.Info($"用户{UserInfo.UserName}登录VMServer成功");
                    mEventAggregator.GetEvent<LoginMessage>().Publish(true);
                }
                else
                {
                    string message = $"用户{UserInfo.UserName}登录VMServer失败";
                    mDialogService.ShowAlert(message, result =>
                    {
                        mTraceHelper.Warn(message);
                    });
                }
            }
            catch (System.Exception ex)
            {
                mTraceHelper.Error(ex.Message, ex);
            }
        }

        private bool CanLogin()
        {
            if (!NetworkHelper.Instance.IsNetworkAvailable)
            {
                mDialogService.ShowAlert(AppTipsConstant.APP_TIPS_NETWORK_NOTAVAILABLE);
                return false;
            }

            if (UserInfo == null
                || string.IsNullOrWhiteSpace(UserInfo.UserName)
                || string.IsNullOrWhiteSpace(UserInfo.Password))
            {
                mDialogService.ShowAlert(AppTipsConstant.APP_TIPS_LOGININFO_NOTEMPTY);
                return false;
            }

            return true;
        }

        #endregion

        #region Messages

        #endregion

        #region Constructor

        public LoginViewModel(IContainerExtension containerExtension,
                              IEventAggregator eventAggregator,
                              IDialogService dialogService)
            : base(containerExtension, eventAggregator, dialogService)
        {
            UserInfo = mContainerExtension.Resolve<UserInfo>();
            mService = mContainerExtension.Resolve<InternetService>();
            mTraceHelper = new TraceHelper(nameof(LoginViewModel));

            IsNetworkAvailable = NetworkHelper.Instance.IsNetworkAvailable;
            NetworkHelper.Instance.NetworkStateChangedAction = ChangeNetworkState;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 更新网络可用状态
        /// </summary>
        /// <param name="isAvailable"></param>
        public void ChangeNetworkState(bool isAvailable)
        {
            IsNetworkAvailable = isAvailable;
            //DispatcherHelper.InvokeOnUIThread(() =>
            //{
            //	if (isAvailable)
            //	{
            //		m_DialogService.ShowNotification("网络已连接", null, interval: 1000);
            //	}
            //	else
            //	{
            //		m_DialogService.ShowNotification("网络连接不可用", null, interval: 1000);
            //	}
            //});
        }

        #endregion
    }
}
