using Microsoft.Extensions.Configuration;

// -----------------------------------------------------------------------
// Demo03: Configuration Sources & Priority
//
// Priority (lowest → highest, later source wins):
//   InMemory < JSON file < Environment variables < Command-line args
//
// The same key "AppName" is set in all four sources so you can see
// exactly which source wins at each stage.
// -----------------------------------------------------------------------

Console.WriteLine("=== 1. InMemory only ===");
IConfiguration inMemory = new ConfigurationBuilder()
    .AddInMemoryCollection(new Dictionary<string, string?>
    {
        ["Source"] = "InMemory",
        ["AppName"] = "FromMemory",
        ["Timeout"] = "5"
    })
    .Build();
Console.WriteLine($"Source  : {inMemory["Source"]}");
Console.WriteLine($"AppName : {inMemory["AppName"]}");
Console.WriteLine($"Timeout : {inMemory["Timeout"]}");

Console.WriteLine();
Console.WriteLine("=== 2. InMemory + JSON (JSON overrides InMemory) ===");
IConfiguration withJson = new ConfigurationBuilder()
    .AddInMemoryCollection(new Dictionary<string, string?>
    {
        ["Source"] = "InMemory",
        ["AppName"] = "FromMemory",
        ["Timeout"] = "5"
    })
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .Build();
Console.WriteLine($"Source  : {withJson["Source"]}   <-- JSON wins over InMemory");
Console.WriteLine($"AppName : {withJson["AppName"]}  <-- JSON wins");
Console.WriteLine($"Timeout : {withJson["Timeout"]}  <-- JSON wins (10)");

Console.WriteLine();
Console.WriteLine("=== 3. + Environment variables (env overrides JSON) ===");
// Set a process-level env var to simulate a real environment override.
// In Docker/K8s this would be set externally.
Environment.SetEnvironmentVariable("AppName", "FromEnvVar");
Environment.SetEnvironmentVariable("Timeout", "60");

IConfiguration withEnv = new ConfigurationBuilder()
    .AddInMemoryCollection(new Dictionary<string, string?>
    {
        ["Source"] = "InMemory",
        ["AppName"] = "FromMemory"
    })
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddEnvironmentVariables()   // overrides JSON for matching keys
    .Build();
Console.WriteLine($"AppName : {withEnv["AppName"]}  <-- env var wins over JSON");
Console.WriteLine($"Timeout : {withEnv["Timeout"]}  <-- env var wins (60)");
Console.WriteLine($"Source  : {withEnv["Source"]}   <-- only in JSON, env has no override");

Console.WriteLine();
Console.WriteLine("=== 4. + Command-line args (highest priority) ===");
// Simulate: dotnet run -- AppName=FromCli Timeout=999
string[] simulatedArgs = ["AppName=FromCli", "Timeout=999"];

IConfiguration withCli = new ConfigurationBuilder()
    .AddInMemoryCollection(new Dictionary<string, string?>
    {
        ["Source"] = "InMemory",
        ["AppName"] = "FromMemory"
    })
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddEnvironmentVariables()
    .AddCommandLine(simulatedArgs)   // highest priority — wins over everything
    .Build();
Console.WriteLine($"AppName : {withCli["AppName"]}  <-- CLI wins over env var");
Console.WriteLine($"Timeout : {withCli["Timeout"]}  <-- CLI wins (999)");
Console.WriteLine($"Source  : {withCli["Source"]}   <-- no CLI override, falls through to JSON");

// Clean up env vars set above
Environment.SetEnvironmentVariable("AppName", null);
Environment.SetEnvironmentVariable("Timeout", null);

Console.ReadLine();
