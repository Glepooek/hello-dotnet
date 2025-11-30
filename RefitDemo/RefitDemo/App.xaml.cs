using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using RefitDemo.Common.Services;
using System;
using System.Windows;

namespace RefitDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string ApiBaseAddress = "https://jsonplaceholder.typicode.com";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<MainWindowViewModel>();
            serviceCollection.AddRefitClient<IJsonPlaceholderApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(ApiBaseAddress);
                    c.Timeout = TimeSpan.FromSeconds(20);
                    c.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                });

            IServiceProvider _serviceProvider = serviceCollection.BuildServiceProvider();
            Ioc.Default.ConfigureServices(_serviceProvider);
        }
    }
}
