using Microsoft.Extensions.DependencyInjection;
using RefitDemo.Common.Modules;
using RefitDemo.Modules;
using System;
using System.Windows;

namespace RefitDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ServiceCollection serviceCollection = new ServiceCollection();

            // Initialize modules
            ModuleBase.InitializeModules(serviceCollection, new[] { typeof(ExampleMainModule) });

            // 注册 MainWindowViewModel
            serviceCollection.AddSingleton<MainWindowViewModel>();
            // 注册 MainWindow
            serviceCollection.AddSingleton<MainWindow>();

            _serviceProvider = serviceCollection.BuildServiceProvider();

            // 启动主窗口并注入依赖
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
