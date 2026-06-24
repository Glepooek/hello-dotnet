using Demo07.CustomProvider;
using Microsoft.Extensions.Configuration;

// -----------------------------------------------------------------------
// Demo07: Custom IConfigurationProvider
//
// Goal: read configuration from appsettings.xml using a hand-rolled provider.
//
// Two interfaces to implement:
//   IConfigurationSource  — registered with ConfigurationBuilder; factory role
//   IConfigurationProvider — does the actual loading; inherits ConfigurationProvider
//
// The base class ConfigurationProvider provides:
//   Data (Dictionary<string,string?>) — write your key-value pairs here in Load()
//   Set(), TryGet(), GetChildKeys() — implemented automatically from Data
//
// After Load(), the provider participates in IConfiguration lookups
// just like the built-in JSON/env-var/cmdline providers.
// -----------------------------------------------------------------------

string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.xml");

IConfiguration config = new ConfigurationBuilder()
    // Our custom extension method — registered exactly like AddJsonFile()
    .AddXmlFile(xmlPath)
    .Build();

Console.WriteLine("=== Demo07: Custom XML Configuration Provider ===");
Console.WriteLine($"Source file : {xmlPath}");
Console.WriteLine();

Console.WriteLine("--- Reading flat keys ---");
Console.WriteLine($"AppName : {config["AppName"]}");
Console.WriteLine($"Version : {config["Version"]}");

Console.WriteLine();
Console.WriteLine("--- Reading nested keys (colon path) ---");
Console.WriteLine($"Database:Host : {config["Database:Host"]}");
Console.WriteLine($"Database:Port : {config["Database:Port"]}");

Console.WriteLine();
Console.WriteLine("--- Enumerating all loaded keys ---");
foreach (var pair in config.AsEnumerable().OrderBy(x => x.Key))
{
    Console.WriteLine($"  {pair.Key} = {pair.Value}");
}

Console.WriteLine();
Console.WriteLine("--- Composing with another source (InMemory overrides XML) ---");
IConfiguration composed = new ConfigurationBuilder()
    .AddXmlFile(xmlPath)
    .AddInMemoryCollection(new Dictionary<string, string?>
    {
        ["AppName"] = "OverriddenByMemory"
    })
    .Build();
Console.WriteLine($"AppName (XML only)    : {config["AppName"]}");
Console.WriteLine($"AppName (+ InMemory)  : {composed["AppName"]}  <-- InMemory wins");

Console.ReadLine();
