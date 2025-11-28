using Learning.PrismDemo.Controls.DialogServices;
using Learning.PrismDemo.Logs;
using Learning.PrismDemo.Messages;
using Learning.PrismDemo.Models;
using Learning.PrismDemo.Utilities;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Windows;
using System.Windows.Input;

namespace Learning.PrismDemo.Main.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private readonly Window mMainWindow;

        #endregion

        #region Properties

        private ViewState m_ViewState;
        public ViewState ViewState
        {
            get
            {
                return m_ViewState;
            }
            set
            {
                if (m_ViewState != value)
                {
                    m_ViewState = value;
                    RaisePropertyChanged(nameof(ViewState));
                }
            }
        }

        private bool m_IsAutomationMaxButton = false;
        /// <summary>
        /// 是否自动触发最大化按钮
        /// </summary>
        public bool IsAutomationMaxButton
        {
            get
            {
                return m_IsAutomationMaxButton;
            }
            set
            {
                if (m_IsAutomationMaxButton != value)
                {
                    m_IsAutomationMaxButton = value;
                    RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region Commands

        public DelegateCommand WindowMinCommand { get; private set; }
        public DelegateCommand WindowMaxCommand { get; private set; }
        public DelegateCommand WindowExitCommand { get; private set; }
        public DelegateCommand<object> MouseDragCommand { get; private set; }
        public DelegateCommand<object> MouseDoubleClickCommand { get; private set; }
        public DelegateCommand SettingCommand { get; private set; }

        /// <summary>
        /// 初始化命令
        /// </summary>
        protected override void InitCommands()
        {
            base.InitCommands();

            WindowMinCommand = new DelegateCommand(OnWindowMin);
            WindowMaxCommand = new DelegateCommand(OnWindowMax);
            WindowExitCommand = new DelegateCommand(OnWindoewExit);
            MouseDragCommand = new DelegateCommand<object>(OnMouseDrag);
            MouseDoubleClickCommand = new DelegateCommand<object>(OnMouseDoubleClick);
            SettingCommand = new DelegateCommand(OnSet);
        }

        private void OnWindowMin()
        {
            mMainWindow.WindowState = WindowState.Minimized;
        }

        private void OnWindowMax()
        {
            if (IsAutomationMaxButton)
            {
                return;
            }

            if (mMainWindow.WindowState == WindowState.Maximized)
            {
                mMainWindow.WindowState = WindowState.Normal;
            }
            else
            {
                mMainWindow.WindowState = WindowState.Maximized;
            }
        }

        private void OnWindoewExit()
        {
            if (m_ViewState != ViewState.Login)
            {
                mDialogService.ShowConfirm(AppTipsConstant.APP_TIPS_EXIT, result =>
                {
                    if (result.Result == ButtonResult.OK)
                    {
                        Application.Current.Shutdown();
                    }
                });
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        private void OnMouseDrag(object arg)
        {
            if (arg is MouseButtonState buttonState
                && buttonState == MouseButtonState.Pressed)
            {
                mMainWindow.DragMove();
            }
        }

        private void OnMouseDoubleClick(object arg)
        {
            if (arg is MouseButtonEventArgs eventArgs
                && eventArgs.LeftButton == MouseButtonState.Pressed
                && eventArgs.ClickCount == 2)
            {
                IsAutomationMaxButton = true;
                mMainWindow.WindowState = (mMainWindow.WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
            }

            IsAutomationMaxButton = false;
        }

        private void OnSet()
        {
            mModuleManager.LoadModule("Setting");
            mRegionManager.RequestNavigate(RegionNameConstant.CONTENT_REGION_NAME, ViewNameConstant.SETTING_VIEW_NAME);
        }

        #endregion

        #region Messages

        protected override void SubscribeMessages()
        {
            mEventAggregator.GetEvent<LoginMessage>().Subscribe(OnLogin);
            mEventAggregator.GetEvent<ViewStateChangedMessage>().Subscribe(OnViewStateChanged);
        }

        private void OnLogin(bool isSuccess)
        {
            if (isSuccess)
            {
                ViewState = ViewState.Navigation;
                mRegionManager.RequestNavigate(RegionNameConstant.CONTENT_REGION_NAME, ViewNameConstant.NAVIGATION_VIEW_NAME);
            }
        }

        private void OnViewStateChanged(ViewState viewState)
        {
            ViewState = viewState;
        }

        #endregion

        #region Costructor

        /// <summary>
        /// 初始化<see cref="MainViewModel"/>类新实例
        /// </summary>
        /// <param name="containerExtension"></param>
        /// <param name="eventAggregator"></param>
        /// <param name="moduleManager"></param>
        /// <param name="regionManager"></param>
        public MainViewModel(IContainerExtension containerExtension,
                            IEventAggregator eventAggregator,
                            IModuleManager moduleManager,
                            IRegionManager regionManager,
                            IDialogService dialogService)
            : base(containerExtension, eventAggregator, moduleManager, regionManager, dialogService)
        {
            mMainWindow = Application.Current.MainWindow;
            mTraceHelper = new TraceHelper(nameof(MainViewModel));

            mModuleManager.LoadModuleCompleted += LoadModuleCompleted;
        }

        #endregion

        #region Methods

        private void LoadModuleCompleted(object? sender, LoadModuleCompletedEventArgs e)
        {
            if (e.ModuleInfo.ModuleName.Equals("ThemeModule"))
            {
                LoadDefaultTheme();
            }

            try
            {
                int x = 1;
                int y = 0;

                int z = x / y;

            }
            catch (System.Exception ex)
            {
                mTraceHelper.Error(ex.Message, ex);
            }
        }

        private void LoadDefaultTheme()
        {
            try
            {
                Application.Current.Resources.MergedDictionaries.Clear();
                var uri = new Uri("/Learning.PrismDemo.DefaultTheme;Component/Index.xaml", UriKind.Relative);
                var component = Application.LoadComponent(uri);
                var resource = (ResourceDictionary)component;
                Application.Current.Resources.MergedDictionaries.Add(resource);

                mTraceHelper.Info("ThemeModule已加载完成");
            }
            catch (Exception ex)
            {
                mTraceHelper?.Error(ex.Message, ex);
            }
        }

        #endregion
    }
}
