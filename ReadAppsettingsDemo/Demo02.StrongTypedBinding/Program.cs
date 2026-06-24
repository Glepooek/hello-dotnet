using Demo02.StrongTypedBinding;
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .Build();

Console.WriteLine("=== 1. Bind(obj) — mutates an existing instance ===");
var appByBind = new ApplicationSettings();
config.GetSection("Application").Bind(appByBind);
Console.WriteLine($"Name           : {appByBind.Name}");
Console.WriteLine($"Version        : {appByBind.Version}");
Console.WriteLine($"MaxConnections : {appByBind.MaxConnections}");
Console.WriteLine($"EnableCache    : {appByBind.EnableCache}");

Console.WriteLine();
Console.WriteLine("=== 2. Get<T>() — creates and returns a new instance ===");
// Get<T> is more concise than Bind; returns null if the section is missing
ApplicationSettings? appByGet = config.GetSection("Application").Get<ApplicationSettings>();
Console.WriteLine($"Name    : {appByGet?.Name}");
Console.WriteLine($"Version : {appByGet?.Version}");

Console.WriteLine();
Console.WriteLine("=== 3. Nested object binding ===");
// Deeply nested: Application -> Database -> Credentials
DatabaseSettings? db = config.GetSection("Application:Database").Get<DatabaseSettings>();
Console.WriteLine($"DB Host     : {db?.Host}");
Console.WriteLine($"DB Port     : {db?.Port}");
Console.WriteLine($"DB Name     : {db?.Name}");
Console.WriteLine($"DB Username : {db?.Credentials.Username}");

Console.WriteLine();
Console.WriteLine("=== 4. Array / List binding ===");
// JSON array maps directly to string[]
string[]? hosts = config.GetSection("AllowedHosts").Get<string[]>();
List<string>? hostList = config.GetSection("AllowedHosts").Get<List<string>>();
Console.WriteLine($"string[] count  : {hosts?.Length}  values: {string.Join(", ", hosts ?? [])}");
Console.WriteLine($"List<string> count : {hostList?.Count}");

Console.WriteLine();
Console.WriteLine("=== 5. Bind a flat section with mixed types ===");
FeatureFlagSettings? flags = config.GetSection("FeatureFlags").Get<FeatureFlagSettings>();
Console.WriteLine($"DarkMode    : {flags?.DarkMode}");
Console.WriteLine($"BetaApi     : {flags?.BetaApi}");
Console.WriteLine($"MaxUploadMb : {flags?.MaxUploadMb}");

Console.ReadLine();
