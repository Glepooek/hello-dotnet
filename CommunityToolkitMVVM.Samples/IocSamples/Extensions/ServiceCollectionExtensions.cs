using CommunityToolkit.Diagnostics;
using IocSamples.Models;
using IocSamples.Services;
using IocSamples.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace IocSamples.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 配置应用程序服务
        /// </summary>
        /// <returns></returns>
        public static ServiceCollection ConfigureServices(this ServiceCollection services)
        {
            Guard.IsNotNull(services);

            services.AddSingleton<IFilesService, FilesService>();
            return services;
        }

        /// <summary>
        /// 配置应用程序视图模型
        /// </summary>
        /// <returns></returns>
        public static ServiceCollection ConfigureViewModels(this ServiceCollection services)
        {
            Guard.IsNotNull(services);

            services.AddSingleton<MainViewModel>();
            return services;
        }

        /// <summary>
        /// 配置应用程序配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static ServiceCollection ConfigureConfiguration(this ServiceCollection services, IConfiguration configuration)
        {
            Guard.IsNotNull(configuration);
            Guard.IsNotNull(services);

            //services.AddSingleton<IOptions<LoggingSettings>>((p) =>
            //{
            //    return Options.Create(config.GetSection(LoggingSettings.Logging).Get<LoggingSettings>());
            //});
            //services.AddSingleton<IOptions<ApplicationSettings>>((p) =>
            //{
            //    return Options.Create(config.GetSection(ApplicationSettings.Application).Get<ApplicationSettings>());
            //});

            // 注册 IConfiguration
            services.AddSingleton<IConfiguration>(configuration);

            // 注册配置类，推荐使用 Configure<T>
            services.Configure<LoggingSettings>(configuration.GetSection(LoggingSettings.Logging));
            services.Configure<ApplicationSettings>(configuration.GetSection(ApplicationSettings.Application));

            return services;
        }

        /// <summary>
        /// 配置应用程序日志记录
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static ServiceCollection ConfigureLogger(this ServiceCollection services, IConfiguration configuration)
        {
            Guard.IsNotNull(services);
            // 代码配置 Serilog 日志
            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Debug()
            //    .WriteTo.Debug()
            //    .WriteTo.File("logs/iocexample.log", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
            //    .CreateLogger();

            // 使用 appsettings.json 配置 Serilog 日志
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithMachineName()
                .Enrich.WithThreadName()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            Log.Information("Serilog log initilized");

            services.AddSingleton<ILogger>(Log.Logger);

            return services;
        }
    }
}
