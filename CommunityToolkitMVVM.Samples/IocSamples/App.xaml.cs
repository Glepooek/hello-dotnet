using CommunityToolkit.Mvvm.DependencyInjection;
using IocSamples.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using Microsoft.Extensions.Configuration;
using System.IO;

// https://cloud.tencent.com/developer/article/2504831
// https://zhuanlan.zhihu.com/p/1926539875374568187

namespace IocSamples
{
    public partial class App : Application
    {
        #region Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                IConfigurationRoot config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                // 初始化 ServiceCollection 并配置服务、ViewModels、Logger、IoC提供程序
                ServiceCollection mServices = new ServiceCollection()
                    .ConfigureServices()
                    .ConfigureViewModels()
                    .ConfigureConfiguration(config)
                    .ConfigureLogger(config);

                IServiceProvider serviceProvider = mServices.BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true });
                Ioc.Default.ConfigureServices(serviceProvider);
            }
            catch (Exception ex)
            {
                Current.Shutdown();
            }
        }

        #endregion
    }
}
