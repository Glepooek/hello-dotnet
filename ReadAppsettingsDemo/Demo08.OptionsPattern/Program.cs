using Demo08.OptionsPattern;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

// -----------------------------------------------------------------------
// Demo08: IOptions / IOptionsSnapshot / IOptionsMonitor
//
// All three read from the same AppOptions POCO bound to "App" section,
// but they differ in lifetime and hot-reload awareness:
//
//  IOptions<T>        — Singleton. Value is fixed at container build time.
//                       Never reflects config file changes at runtime.
//
//  IOptionsSnapshot<T>— Scoped. Value is re-read once per DI scope.
//                       Cannot be injected into singletons.
//                       Reflects file changes when a new scope is created.
//
//  IOptionsMonitor<T> — Singleton. CurrentValue always reflects the latest
//                       config after a reload. Supports OnChange callbacks.
//                       Safe to inject into singletons.
// -----------------------------------------------------------------------

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

ServiceCollection services = new();
services.AddSingleton<IConfiguration>(config);

// Bind "App" section to AppOptions — works for all three IOptions variants
services.AddOptions<AppOptions>()
    .Bind(config.GetSection("App"));

services.AddSingleton<ServiceUsingOptions>();
services.AddScoped<ServiceUsingSnapshot>();   // Scoped — matches IOptionsSnapshot lifecycle
services.AddSingleton<ServiceUsingMonitor>();

ServiceProvider sp = services.BuildServiceProvider();

Console.WriteLine("=== Demo08: IOptions / IOptionsSnapshot / IOptionsMonitor ===");
Console.WriteLine();

// -----------------------------------------------------------------------
Console.WriteLine("--- 1. IOptions<T> (singleton snapshot) ---");
sp.GetRequiredService<ServiceUsingOptions>().Print();

// -----------------------------------------------------------------------
Console.WriteLine();
Console.WriteLine("--- 2. IOptionsSnapshot<T> (scoped, re-read per scope) ---");
// Must be resolved inside an explicit scope — IOptionsSnapshot is scoped
using (IServiceScope scope1 = sp.CreateScope())
{
    scope1.ServiceProvider.GetRequiredService<ServiceUsingSnapshot>().Print();
}

// -----------------------------------------------------------------------
Console.WriteLine();
Console.WriteLine("--- 3. IOptionsMonitor<T> (singleton with live reload) ---");
ServiceUsingMonitor monitor = sp.GetRequiredService<ServiceUsingMonitor>();
monitor.Print();

// Register OnChange callback to observe hot reload
using IDisposable changeReg = monitor.RegisterChangeCallback();

// -----------------------------------------------------------------------
Console.WriteLine();
Console.WriteLine("--- 4. Lifecycle comparison summary ---");
Console.WriteLine("  IOptions<T>        : singleton — never changes after startup");
Console.WriteLine("  IOptionsSnapshot<T>: scoped    — changes visible in next scope");
Console.WriteLine("  IOptionsMonitor<T> : singleton — CurrentValue always up-to-date");

Console.WriteLine();
Console.WriteLine("Edit the OUTPUT appsettings.json to see IOptionsMonitor.OnChange fire.");
Console.WriteLine("Press Enter to exit.");
Console.ReadLine();
