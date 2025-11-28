using Learning.PrismDemo.Controls.DialogServices;
using Learning.PrismDemo.Logs;
using Learning.PrismDemo.Main.RegionAdapters;
using Learning.PrismDemo.Main.Views;
using Learning.PrismDemo.Models;
using Learning.PrismDemo.Services;
using Learning.PrismDemo.Utilities;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Learning.PrismDemo.Main
{
    /// <summary>
    /// App处理逻辑
    /// </summary>
    public partial class App : PrismApplication
    {
        #region Constructor

        public App()
        {
            mTraceHelper = new TraceHelper(nameof(App));
        }

        #endregion

        #region Fields

        private TraceHelper mTraceHelper;

        #endregion

        #region Methods

        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<UserInfo>();
            containerRegistry.Register<IService, InternetService>();
            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>();

            containerRegistry.RegisterForNavigation<NavigationView>();

            containerRegistry.RegisterDialog<ConfirmDialog, ConfirmDialogViewModel>();
            containerRegistry.RegisterDialog<AlertDialog, AlertDialogViewModel>();
            containerRegistry.RegisterDialog<NotificationDialog, NotificationDialogViewModel>();
        }

        // 可以通过以下方式存放模块信息并加载：AppConfig、Code、Directory、Xaml
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog()
            {
                ModulePath = @"./Modules"
            };
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());
            regionAdapterMappings.RegisterMapping(typeof(WrapPanel), Container.Resolve<WrapPanelRegionAdapter>());
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.DispatcherUnhandledException += OnDispatcherUnhandledException;
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            mTraceHelper.Fatal(e.Exception.Message, e.Exception);
            e.Handled = true;
        }

        #endregion
    }
}