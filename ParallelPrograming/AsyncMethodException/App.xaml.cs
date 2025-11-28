using System;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncMethodException
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            App.Current.Dispatcher.UnhandledException += Dispatcher_UnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }


        /// <summary>
        /// 用于捕获Task调度器中的未处理的异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            ShowMessageBox("Task Scheduler catches an unobserved task exception. Exception: " + e.Exception.Message);
            e.SetObserved();
            try
            {
                Environment.Exit(0);
            }
            catch { }
        }


        /// <summary>
        /// 用于捕获工作线程未处理的异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception exception)
            {
                ShowMessageBox($"Current Domain catches an unhandled exception. Exception: " + exception.Message);
                try
                {
                    Environment.Exit(0);
                }
                catch { }
            }
        }

        /// <summary>
        /// 用于捕获UI线程未处理的异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            ShowMessageBox($"Application dispatcher catches an unhandled exception. Exception: " + e.Exception.Message);
            e.Handled = true;
            try
            {
                Environment.Exit(0);
            }
            catch { }
        }

        private void ShowMessageBox(string msg)
        {
            MessageBox.Show(msg);
        }
    }
}
