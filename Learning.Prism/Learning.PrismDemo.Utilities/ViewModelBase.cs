using Learning.PrismDemo.Logs;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;

namespace Learning.PrismDemo.Utilities
{
    /// <summary>
    /// ViewModel基类
    /// </summary>
    public class ViewModelBase : BindableBase, INavigationAware, IDisposable
    {
        #region Fields

        protected readonly IContainerExtension mContainerExtension;
        protected readonly IEventAggregator mEventAggregator;
        protected readonly IModuleManager mModuleManager;
        protected readonly IRegionManager mRegionManager;
        protected readonly IDialogService mDialogService;
        protected IRegionNavigationJournal mRegionNavigationJournal;
        /// <summary>
        /// 日志记录
        /// </summary>
        protected TraceHelper mTraceHelper;
        private bool disposedValue;

        #endregion

        #region Commands

        public DelegateCommand GoBackCommand { get; private set; }

        #endregion

        #region Constructor

        ~ViewModelBase()
        {
            // 用于释放未托管资源，替代终结器
            // 不要更改此代码。清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: false);
        }

        protected ViewModelBase(IContainerExtension containerExtension)
        {
            mContainerExtension = containerExtension;

            InitCommands();
        }

        protected ViewModelBase(IContainerExtension containerExtension,
                                                    IDialogService dialogService)
            : this(containerExtension)
        {
            mDialogService = dialogService;
        }

        protected ViewModelBase(IContainerExtension containerExtension,
                                                    IEventAggregator eventAggregator,
                                                    IDialogService dialogService)
            : this(containerExtension, dialogService)
        {
            mEventAggregator = eventAggregator;

            SubscribeMessages();
        }

        protected ViewModelBase(IContainerExtension containerExtension,
                                                    IRegionManager regionManager,
                                                    IDialogService dialogService)
            : this(containerExtension, dialogService)
        {
            mRegionManager = regionManager;
        }

        protected ViewModelBase(IContainerExtension containerExtension,
                                                    IEventAggregator eventAggregator,
                                                    IModuleManager moduleManager,
                                                    IDialogService dialogService)
            : this(containerExtension, eventAggregator, dialogService)
        {
            mModuleManager = moduleManager;
        }

        protected ViewModelBase(IContainerExtension containerExtension,
                                                    IEventAggregator eventAggregator,
                                                    IRegionManager regionManager,
                                                    IDialogService dialogService)
            : this(containerExtension, eventAggregator, dialogService)
        {
            mRegionManager = regionManager;
        }

        protected ViewModelBase(IContainerExtension containerExtension,
                                                    IEventAggregator eventAggregator,
                                                    IModuleManager moduleManager,
                                                    IRegionManager regionManager,
                                                    IDialogService dialogService)
            : this(containerExtension, eventAggregator, moduleManager, dialogService)
        {
            mRegionManager = regionManager;
        }

        #endregion

        #region Methods

        protected virtual void InitCommands()
        {
            GoBackCommand = new DelegateCommand(OnGoBack);
        }

        protected virtual void SubscribeMessages()
        {

        }

        protected virtual void UnsubscribeMessages()
        {

        }

        protected virtual void OnGoBack()
        {
            mRegionNavigationJournal.GoBack();
        }

        #endregion

        #region INavigationAware Members

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
            UnsubscribeMessages();
            Dispose();
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            mRegionNavigationJournal = navigationContext.NavigationService.Journal;
        }

        #endregion

        #region IDisposable Members

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                }

                // TODO: 释放未托管的资源(未托管的对象)并替代终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}