using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
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
            serviceCollection.AddSingleton<MainWindowViewModel>();

            _serviceProvider = serviceCollection.BuildServiceProvider();
            Ioc.Default.ConfigureServices(_serviceProvider);
        }
    }
}
