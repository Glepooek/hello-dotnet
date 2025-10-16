using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using IocSamples.Services;

namespace IocSamples
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Constructor

        public App()
        {
            Services = ConfigureServices();
        }

        #endregion

        #region Properties

        public new static App Current => (App)Application.Current;
        /// <summary>
        /// 用于解析程序服务的IServiceProvider实例
        /// </summary>
        public IServiceProvider Services { get; }

        #endregion

        #region Methods

        /// <summary>
        /// 为应用程序配置服务
        /// </summary>
        /// <returns></returns>
        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IFilesService, FilesService>();
            services.AddTransient<MainViewModel>();

            return services.BuildServiceProvider();
        }

        #endregion
    }
}
