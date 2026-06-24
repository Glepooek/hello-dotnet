using Demo08.OptionsPattern;
using Microsoft.Extensions.Options;

namespace Demo08.OptionsPattern;

// -----------------------------------------------------------------------
// Three services that each depend on a different IOptions variant.
// Injected by the DI container so you can see the lifecycle difference.
// -----------------------------------------------------------------------

// IOptions<T>  — singleton; value is fixed at DI container build time.
public class ServiceUsingOptions(IOptions<AppOptions> options)
{
    public void Print()
    {
        var o = options.Value;   // always the same snapshot
        Console.WriteLine($"[IOptions]         Name={o.Name}  Version={o.Version}  MaxConn={o.MaxConnections}");
    }
}

// IOptionsSnapshot<T> — scoped; re-read once per DI scope (e.g. per HTTP request).
public class ServiceUsingSnapshot(IOptionsSnapshot<AppOptions> options)
{
    public void Print()
    {
        var o = options.Value;   // fresh within this scope, stale across scope boundary
        Console.WriteLine($"[IOptionsSnapshot]  Name={o.Name}  Version={o.Version}  MaxConn={o.MaxConnections}");
    }
}

// IOptionsMonitor<T> — singleton; reflects live reloads via OnChange callback.
public class ServiceUsingMonitor(IOptionsMonitor<AppOptions> monitor)
{
    public void Print()
    {
        var o = monitor.CurrentValue;   // always the latest value after any reload
        Console.WriteLine($"[IOptionsMonitor]   Name={o.Name}  Version={o.Version}  MaxConn={o.MaxConnections}");
    }

    // Register a callback that fires whenever configuration reloads
    public IDisposable RegisterChangeCallback()
        // OnChange returns IDisposable? in .NET 10; null only when the provider
        // does not support change notifications, which cannot happen here.
        => monitor.OnChange(updated =>
            Console.WriteLine($"  >> OnChange fired: Name={updated.Name}  MaxConnections={updated.MaxConnections}"))!;
}
