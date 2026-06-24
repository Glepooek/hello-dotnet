using Microsoft.Extensions.Options;

namespace Demo09.OptionsConfiguration;

public class MailOptions
{
    // Named options constant — used as the key for named registrations
    public const string Primary = "Primary";
    public const string Alerts = "Alerts";

    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string From { get; set; } = string.Empty;
    public bool UseSsl { get; set; }   // not in JSON — set via IConfigureOptions
    public string DisplayName { get; set; } = string.Empty; // set via IPostConfigureOptions
}

// -----------------------------------------------------------------------
// IConfigureOptions<T>
// Runs BEFORE the final value is produced. Used to apply code-based
// defaults or enrichments that cannot live in a JSON file (e.g. derived
// from environment, secrets, or other services).
// -----------------------------------------------------------------------
public class MailOptionsSetup : IConfigureOptions<MailOptions>
{
    public void Configure(MailOptions options)
    {
        // Force SSL for port 465 — logic that belongs in code, not config file
        if (options.Port == 465)
            options.UseSsl = true;

        Console.WriteLine($"  [IConfigureOptions] Configure() called — UseSsl set to {options.UseSsl} for port {options.Port}");
    }
}

// IConfigureNamedOptions<T> — same as above but receives the option name
public class MailOptionsNamedSetup : IConfigureNamedOptions<MailOptions>
{
    public void Configure(string? name, MailOptions options)
    {
        if (options.Port == 465)
            options.UseSsl = true;
        Console.WriteLine($"  [IConfigureNamedOptions] Configure(\"{name}\") called");
    }

    public void Configure(MailOptions options) => Configure(Options.DefaultName, options);
}

// -----------------------------------------------------------------------
// IPostConfigureOptions<T>
// Runs AFTER all IConfigureOptions have applied. Used for computed
// properties or overrides that must happen last.
// -----------------------------------------------------------------------
public class MailOptionsPostSetup : IPostConfigureOptions<MailOptions>
{
    public void PostConfigure(string? name, MailOptions options)
    {
        // Build a human-readable display name from the final values
        options.DisplayName = $"{options.From} via {options.Host}:{options.Port}";
        Console.WriteLine($"  [IPostConfigureOptions] PostConfigure(\"{name}\") called — DisplayName set");
    }
}
