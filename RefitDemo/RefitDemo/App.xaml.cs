using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
        

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddOptions<RefitOptions>();
            serviceCollection.ConfigureOptions<RefitConfigureOptions>();

            serviceCollection.AddSingleton<MainWindowViewModel>();
            serviceCollection.AddRefitClient<IJsonPlaceholderApi>()
                .ConfigureHttpClient((sp, c) =>
                {
                    var refitOptions = sp.GetRequiredService<IOptions<RefitOptions>>().Value;
                    c.BaseAddress = refitOptions.BaseUri;
                    c.Timeout = TimeSpan.FromSeconds(refitOptions.TimeoutSeconds);
                });

            IServiceProvider _serviceProvider = serviceCollection.BuildServiceProvider();
            Ioc.Default.ConfigureServices(_serviceProvider);
        }
    }
}
