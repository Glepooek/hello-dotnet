using CommunityToolkit.Mvvm.DependencyInjection;
using IocSamples.Models;
using IocSamples.Services;
using IocSamples.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Windows;

namespace IocSamples
{
    public partial class App : Application
    {
        #region Constructor

        public App()
        {

        }

        #endregion

        #region Fields

        private IServiceCollection mServices;

        #endregion

        #region Methods

        /// <summary>
        /// 配置应用程序服务
        /// </summary>
        /// <returns></returns>
        private App ConfigureServices()
        {
            mServices.AddSingleton<IFilesService, FilesService>();
            return this;
        }

        /// <summary>
        /// 配置应用程序视图模型
        /// </summary>
        /// <returns></returns>
        private App ConfigureViewModels()
        {
            mServices.AddSingleton<MainViewModel>();
            return this;
        }

        private App ConfigureConfiguration()
        {
            IConfigurationRoot? config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            mServices.AddSingleton<IOptions<LoggingSettings>>((p) =>
            {
                return Options.Create(config.GetSection(LoggingSettings.Logging).Get<LoggingSettings>());
            });
            mServices.AddSingleton<IOptions<ApplicationSettings>>((p) =>
            {
                return Options.Create(config.GetSection(ApplicationSettings.Application).Get<ApplicationSettings>());
            });
            return this;
        }

        private App ConfigureIocProvider()
        {
            IServiceProvider serviceProvider = mServices.BuildServiceProvider();
            Ioc.Default.ConfigureServices(serviceProvider);
            return this;
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            // 初始化 ServiceCollection 并配置服务、ViewModels、IoC提供程序
            mServices = new ServiceCollection();
            this.ConfigureServices()
                .ConfigureViewModels()
                .ConfigureConfiguration()
                .ConfigureIocProvider();
        }

        #endregion
    }
}
