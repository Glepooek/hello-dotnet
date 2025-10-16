// See https://aka.ms/new-console-template for more information
using ConfigurationJsonDemo;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

Console.WriteLine("Hello, World!");

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); ;

IConfigurationRoot config = builder.Build();
var connectionString = config["ConnectionString"];

string appName = config["ApplicationSettings:AppName"];

Debug.WriteLine(connectionString);
Debug.WriteLine(appName);


// 绑定配置到对象
AppSettings appSettings = new AppSettings
{
    Logging = new LoggingSettings(),
    Application = new ApplicationSettings()
};
config.GetSection("Logging").Bind(appSettings.Logging);
config.GetSection("ApplicationSettings").Bind(appSettings.Application);

Debug.WriteLine($"Log Level: {appSettings.Logging.LogLevel.Default}");
Debug.WriteLine($"App Name: {appSettings.Application.AppName}");
Debug.WriteLine($"App Version: {appSettings.Application.Version}");

Console.ReadLine();