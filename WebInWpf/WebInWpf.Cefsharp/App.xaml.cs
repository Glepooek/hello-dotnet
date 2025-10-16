using CefSharp;
using CefSharp.Wpf;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace Unipus.Student.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Constructor

        public App()
        {
            this.Startup += OnStartup;
            this.DispatcherUnhandledException += OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainUnhandledException;
            TaskScheduler.UnobservedTaskException += OnTaskSchedulerUnobservedTaskException;
        }

        #endregion

        #region EventHandler

        private async void OnStartup(object sender, StartupEventArgs e)
        {
            await InitializeCefAsync();
        }

        private void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {

        }

        private void OnCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {

            }
        }

        private void OnTaskSchedulerUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {

        }

        #endregion

        #region Methods

        [MethodImpl(MethodImplOptions.NoInlining)]
        private async Task<bool> InitializeCefAsync()
        {
            CefSharpSettings.FocusedNodeChangedEnabled = true;
            CefSharpSettings.SubprocessExitIfParentProcessClosed = true;
            CefSharpSettings.ConcurrentTaskExecution = true;
            CefSharpSettings.ShutdownOnExit = true;

            var settings = new CefSettings()
            {
                // 启用日志打印
                LogSeverity = LogSeverity.Warning,
                //LogFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"CefSharp\\{DateTime.Now:yyyy-MM-dd}.log"),
                //UserDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\UserData"),
                // By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
                CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache"),
            };

            // setting a command line argument
            settings.CefCommandLineArgs.Add("enable-media-stream");
            settings.CefCommandLineArgs.Add("use-fake-ui-for-media-stream");
            settings.CefCommandLineArgs.Add("enable-usermedia-screen-capturing");
            settings.CefCommandLineArgs.Add("disable-gpu", "1"); // 禁用gpu,解决闪烁的问题
            settings.CefCommandLineArgs.Add("disable-gpu-compositing", "1");
            settings.CefCommandLineArgs.Add("touch-events", "1");
            settings.CefCommandLineArgs.Add("disable-web-security", "1");// 关闭同源策略，允许跨域调试
            settings.CefCommandLineArgs.Add("no-proxy-server", "1");// 禁用代理

            var result = Cef.IsInitialized;
            if (result.HasValue && !result.Value)
            {
                //Cef.EnableHighDPISupport();
                //Perform dependency check to make sure all relevant resources are in our output directory.
                return await Cef.InitializeAsync(settings, performDependencyCheck: true, browserProcessHandler: null);
            }

            return false;
        }

        #endregion
    }
}
