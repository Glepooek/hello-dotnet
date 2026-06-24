using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

// -----------------------------------------------------------------------
// Demo05: Hot Reload — reloadOnChange: true + IChangeToken
//
// How it works:
//   ConfigurationBuilder wires a FileSystemWatcher to appsettings.json.
//   When the file changes on disk, the provider reloads and all
//   subsequent config reads return the new values — without restarting.
//
//   IChangeToken.RegisterChangeCallback lets you react to a reload event.
//
// How to test:
//   1. Run the program (dotnet run).
//   2. While it is running, open the OUTPUT appsettings.json in
//      bin/Debug/ and change "LogLevel" to "Debug" and save.
//   3. The callback fires and prints the new value within ~1 second.
//   4. Press Enter to exit.
// -----------------------------------------------------------------------

string configPath = Directory.GetCurrentDirectory();

IConfigurationRoot config = new ConfigurationBuilder()
    .SetBasePath(configPath)
    // reloadOnChange: true — enables the FileSystemWatcher
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

int reloadCount = 0;

// RegisterChangeCallback registers a one-shot callback.
// We wrap it in a local method so we can re-register after each reload
// (IChangeToken is single-use — it fires once and becomes stale).
void RegisterReloadCallback()
{
    IChangeToken token = config.GetReloadToken();
    token.RegisterChangeCallback(_ =>
    {
        reloadCount++;
        Console.WriteLine();
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Config reloaded (#{reloadCount})");
        Console.WriteLine($"  LogLevel        : {config["LogLevel"]}");
        Console.WriteLine($"  PollingIntervalMs: {config["PollingIntervalMs"]}");
        Console.WriteLine("Edit appsettings.json again to trigger another reload, or press Enter to exit.");

        // Re-register for the next change — token is single-use
        RegisterReloadCallback();
    }, state: null);
}

Console.WriteLine("=== Demo05: Hot Reload ===");
Console.WriteLine($"Watching: {Path.Combine(configPath, "appsettings.json")}");
Console.WriteLine($"Initial LogLevel         : {config["LogLevel"]}");
Console.WriteLine($"Initial PollingIntervalMs: {config["PollingIntervalMs"]}");
Console.WriteLine();
Console.WriteLine("Edit the OUTPUT appsettings.json while this runs to see hot reload.");
Console.WriteLine("Press Enter to exit.");
Console.WriteLine();

RegisterReloadCallback();

Console.ReadLine();
