using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Refit;
using RefitDemo.Common.Services;
using System;
using System.IO;
using System.Windows;

namespace RefitDemo
{
    public partial class App : Application
    {
        private IHost _host;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(AppContext.BaseDirectory);
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    // Options pattern for Refit configuration
                    services.AddOptions<RefitOptions>();
                    services.ConfigureOptions<RefitConfigureOptions>();

                    // Token store (replace NoOpTokenStore with real implementation when needed)
                    services.AddSingleton<IAuthTokenStore, NoOpTokenStore>();

                    // DelegatingHandlers for request pipeline
                    services.AddTransient<LoggingHandler>();
                    services.AddTransient<AuthHeaderHandler>();

                    // Refit client with handler chain
                    services.AddRefitClient<IJsonPlaceholderApi>()
                        .AddHttpMessageHandler<LoggingHandler>()
                        .AddHttpMessageHandler<AuthHeaderHandler>()
                        .ConfigureHttpClient((sp, c) =>
                        {
                            var refitOptions = sp.GetRequiredService<IOptions<RefitOptions>>().Value;
                            c.BaseAddress = refitOptions.BaseUri;
                            c.Timeout = TimeSpan.FromSeconds(refitOptions.TimeoutSeconds);
                        });

                    // Service layer
                    services.AddSingleton<IPostService, PostService>();

                    // ViewModels
                    services.AddSingleton<MainWindowViewModel>();
                })
                .Build();

            Ioc.Default.ConfigureServices(_host.Services);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host?.Dispose();
            base.OnExit(e);
        }
    }
}
