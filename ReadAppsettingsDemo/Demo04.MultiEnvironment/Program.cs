using Microsoft.Extensions.Configuration;

// -----------------------------------------------------------------------
// Demo04: Multi-Environment Configuration
//
// Convention: appsettings.json holds production defaults.
//             appsettings.{DOTNET_ENVIRONMENT}.json patches only the keys
//             that differ per environment — everything else falls through
//             to the base file.
//
// DOTNET_ENVIRONMENT values used in .NET generic host:
//   Development | Staging | Production (default when unset)
// -----------------------------------------------------------------------

static IConfiguration BuildConfig(string environment)
{
    Console.WriteLine($"  [building config for environment: {environment}]");
    return new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false)
        // optional: true — environment-specific file need not exist
        .AddJsonFile($"appsettings.{environment}.json", optional: true)
        .AddEnvironmentVariables()
        .Build();
}

static void PrintConfig(IConfiguration config)
{
    Console.WriteLine($"  AppName              : {config["AppName"]}");
    Console.WriteLine($"  LogLevel             : {config["LogLevel"]}");
    Console.WriteLine($"  Database:Host        : {config["Database:Host"]}");
    Console.WriteLine($"  Database:Name        : {config["Database:Name"]}");
    Console.WriteLine($"  EnableDetailedErrors : {config["EnableDetailedErrors"]}");
}

Console.WriteLine("=== Production (no env-specific file override) ===");
PrintConfig(BuildConfig("Production"));

Console.WriteLine();
Console.WriteLine("=== Development (appsettings.Development.json patches base) ===");
PrintConfig(BuildConfig("Development"));

Console.WriteLine();
Console.WriteLine("=== Staging (appsettings.Staging.json patches base) ===");
PrintConfig(BuildConfig("Staging"));

Console.WriteLine();
Console.WriteLine("=== Key points ===");
Console.WriteLine("  - AppName is only in appsettings.json; all environments inherit it.");
Console.WriteLine("  - LogLevel, Database, EnableDetailedErrors differ per environment.");
Console.WriteLine("  - Missing keys in env file fall through to the base file (not null).");
Console.WriteLine("  - Set DOTNET_ENVIRONMENT=Development at OS level for real deployment.");

Console.ReadLine();
