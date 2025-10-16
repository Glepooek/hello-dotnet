using System.Windows;
using System.Windows.Threading;
using Test.DragControl.Helper;
using Test.DragControl.Models;

namespace Test.DragControl
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Constructor

        public App()
        {
            this.DispatcherUnhandledException += OnAppDispatcherUnhandledException;
            mTraceHelper = new TraceHelper(nameof(App));
        }

        #endregion

        #region Fields

        private TraceHelper mTraceHelper;

        #endregion

        #region Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                SqlSugarHelper.GetInstance().InitSqlSugarClient();
                SqlSugarHelper.GetInstance().SqlSugarClient.DbMaintenance
                    .CreateDatabase();
                SqlSugarHelper.GetInstance().SqlSugarClient
                    .CodeFirst.InitTables(typeof(ListeningMaterial));
            }
            catch (System.Exception ex)
            {
                mTraceHelper.Error(ex.Message, ex);
            }
        }

        private void OnAppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs ex)
        {
            mTraceHelper.Error(ex.Exception?.Message, ex.Exception);
        }

        #endregion
    }
}
