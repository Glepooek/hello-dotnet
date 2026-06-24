using ConfigurationJsonDemo;
using Microsoft.Extensions.Configuration;

IConfigurationRoot? config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

string? version = config["Version"];
string? connectionString = config.GetConnectionString("ConnectionString");
string? appName = config["ApplicationSettings:AppName"];

Console.WriteLine(version);
Console.WriteLine(connectionString);
Console.WriteLine(appName);

// Bind configuration to strongly-typed objects
AppSettings appSettings = new()
{
    Logging = new(),
    Application = new()
};
config.GetSection("Logging").Bind(appSettings.Logging);
config.GetSection("ApplicationSettings").Bind(appSettings.Application);

Console.WriteLine($"Log Level: {appSettings.Logging.LogLevel.Default}");
Console.WriteLine($"App Name: {appSettings.Application.AppName}");
Console.WriteLine($"App Version: {appSettings.Application.Version}");

Console.ReadLine();
