using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .Build();

Console.WriteLine("=== 1. Direct key reading (string indexer) ===");
string? appName = config["AppName"];
string? version = config["Version"];
Console.WriteLine($"AppName : {appName}");
Console.WriteLine($"Version : {version}");

Console.WriteLine();
Console.WriteLine("=== 2. Nested key reading (colon-separated path) ===");
string? dbHost = config["Database:Host"];
string? dbPort = config["Database:Port"];
Console.WriteLine($"Database:Host : {dbHost}");
Console.WriteLine($"Database:Port : {dbPort}");

Console.WriteLine();
Console.WriteLine("=== 3. GetValue<T> with default fallback ===");
// Key exists — returns the configured value
int maxRetry = config.GetValue<int>("MaxRetryCount", defaultValue: 5);
// Key does not exist — returns the supplied default
int timeout = config.GetValue<int>("TimeoutSeconds", defaultValue: 30);
bool featureX = config.GetValue<bool>("EnableFeatureX", defaultValue: false);
bool featureY = config.GetValue<bool>("EnableFeatureY", defaultValue: false);

Console.WriteLine($"MaxRetryCount   (exists)  : {maxRetry}");
Console.WriteLine($"TimeoutSeconds  (missing) : {timeout}  <-- default value");
Console.WriteLine($"EnableFeatureX  (exists)  : {featureX}");
Console.WriteLine($"EnableFeatureY  (missing) : {featureY}  <-- default value");

Console.WriteLine();
Console.WriteLine("=== 4. GetConnectionString shortcut ===");
// Equivalent to config["ConnectionStrings:DefaultConnection"]
string? connStr = config.GetConnectionString("DefaultConnection");
Console.WriteLine($"DefaultConnection : {connStr}");

Console.ReadLine();
