using Demo09.OptionsConfiguration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

// -----------------------------------------------------------------------
// Demo09: IConfigureOptions / IPostConfigureOptions / Named Options
//
// Configuration pipeline order for a single options instance:
//   1. Bind() / .Bind(section)  — maps JSON values onto the POCO
//   2. IConfigureOptions<T>     — code-based setup / defaults applied on top
//   3. IPostConfigureOptions<T> — final override / computed properties
//
// Named Options: register the same POCO type multiple times under
// different names, each bound to a different config section.
// Resolved via IOptionsMonitor<T>.Get("name").
// -----------------------------------------------------------------------

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

ServiceCollection services = new();
services.AddSingleton<IConfiguration>(config);

// -----------------------------------------------------------------------
Console.WriteLine("=== 1. IConfigureOptions<T> + IPostConfigureOptions<T> ===");
Console.WriteLine("  (configure/postconfigure callbacks fire on first .Value access)");
Console.WriteLine();

services.AddOptions<MailOptions>()
    .Bind(config.GetSection("Mail"));

// IConfigureOptions — runs after Bind, before PostConfigure
services.AddSingleton<IConfigureOptions<MailOptions>, MailOptionsSetup>();
// IPostConfigureOptions — runs last
services.AddSingleton<IPostConfigureOptions<MailOptions>, MailOptionsPostSetup>();

ServiceProvider sp1 = services.BuildServiceProvider();
MailOptions mail = sp1.GetRequiredService<IOptions<MailOptions>>().Value;

Console.WriteLine($"  Host        : {mail.Host}");
Console.WriteLine($"  Port        : {mail.Port}");
Console.WriteLine($"  From        : {mail.From}");
Console.WriteLine($"  UseSsl      : {mail.UseSsl}      <-- set by IConfigureOptions");
Console.WriteLine($"  DisplayName : {mail.DisplayName} <-- set by IPostConfigureOptions");

// -----------------------------------------------------------------------
Console.WriteLine();
Console.WriteLine("=== 2. Named Options ===");
Console.WriteLine("  Same POCO type, two instances, each bound to a different section.");
Console.WriteLine();

ServiceCollection services2 = new();
services2.AddSingleton<IConfiguration>(config);

// Register two named instances of MailOptions
services2.AddOptions<MailOptions>(MailOptions.Primary)
    .Bind(config.GetSection("Mail"));

services2.AddOptions<MailOptions>(MailOptions.Alerts)
    .Bind(config.GetSection("Smtp"));

// IConfigureNamedOptions applies to ALL named instances
services2.AddSingleton<IConfigureNamedOptions<MailOptions>, MailOptionsNamedSetup>();
services2.AddSingleton<IPostConfigureOptions<MailOptions>, MailOptionsPostSetup>();

ServiceProvider sp2 = services2.BuildServiceProvider();

// Named options must be resolved via IOptionsMonitor<T>.Get(name)
IOptionsMonitor<MailOptions> monitor = sp2.GetRequiredService<IOptionsMonitor<MailOptions>>();

MailOptions primary = monitor.Get(MailOptions.Primary);
MailOptions alerts = monitor.Get(MailOptions.Alerts);

Console.WriteLine($"  [{MailOptions.Primary}]");
Console.WriteLine($"    Host        : {primary.Host}");
Console.WriteLine($"    Port        : {primary.Port}  UseSsl={primary.UseSsl}");
Console.WriteLine($"    DisplayName : {primary.DisplayName}");

Console.WriteLine();
Console.WriteLine($"  [{MailOptions.Alerts}]");
Console.WriteLine($"    Host        : {alerts.Host}");
Console.WriteLine($"    Port        : {alerts.Port}  UseSsl={alerts.UseSsl}  <-- IConfigureNamedOptions set SSL for port 465");
Console.WriteLine($"    DisplayName : {alerts.DisplayName}");

Console.ReadLine();
