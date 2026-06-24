using Demo10.OptionsValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

// -----------------------------------------------------------------------
// Demo10: Options Validation
//
// Three validation mechanisms, applied in order:
//   1. ValidateDataAnnotations() — [Required], [Range], [RegularExpression]…
//   2. Validate(Func<T, bool>)   — inline lambda for quick rules
//   3. IValidateOptions<T>       — class-based, supports cross-property rules
//
// ValidateOnStart() — triggers all validation when the DI container is
// first used, rather than on the first .Value access. Essential for
// "fail fast": catch bad config at startup, not mid-request.
// -----------------------------------------------------------------------

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

// -----------------------------------------------------------------------
Console.WriteLine("=== 1. Valid config — all validators pass ===");
{
    ServiceCollection services = new();
    services.AddOptions<SmtpOptions>()
        .Bind(config.GetSection("Valid"))
        .ValidateDataAnnotations()
        .ValidateOnStart();

    services.AddSingleton<IValidateOptions<SmtpOptions>, SmtpOptionsValidator>();

    ServiceProvider sp = services.BuildServiceProvider();
    // Trigger validation by resolving the options
    SmtpOptions opts = sp.GetRequiredService<IOptions<SmtpOptions>>().Value;
    Console.WriteLine($"  Host           : {opts.Host}");
    Console.WriteLine($"  Port           : {opts.Port}");
    Console.WriteLine($"  TimeoutSeconds : {opts.TimeoutSeconds}");
    Console.WriteLine($"  MaxRetry       : {opts.MaxRetry}");
    Console.WriteLine("  Validation     : PASSED");
}

// -----------------------------------------------------------------------
Console.WriteLine();
Console.WriteLine("=== 2. Invalid config — DataAnnotations catch errors ===");
{
    ServiceCollection services = new();
    services.AddOptions<SmtpOptions>()
        .Bind(config.GetSection("Invalid"))
        .ValidateDataAnnotations()  // [Required], [Range] checked here
        .ValidateOnStart();

    ServiceProvider sp = services.BuildServiceProvider();
    try
    {
        _ = sp.GetRequiredService<IOptions<SmtpOptions>>().Value;
    }
    catch (OptionsValidationException ex)
    {
        Console.WriteLine("  Validation FAILED (OptionsValidationException):");
        foreach (string failure in ex.Failures)
            Console.WriteLine($"    - {failure}");
    }
}

// -----------------------------------------------------------------------
Console.WriteLine();
Console.WriteLine("=== 3. Inline Validate() lambda ===");
{
    ServiceCollection services = new();
    services.AddOptions<SmtpOptions>()
        .Bind(config.GetSection("Valid"))
        // Quick one-liner rule — returns bool; false triggers failure
        .Validate(
            opts => opts.TimeoutSeconds >= opts.MaxRetry,
            "TimeoutSeconds must be >= MaxRetry")
        .ValidateOnStart();

    ServiceProvider sp = services.BuildServiceProvider();
    SmtpOptions opts = sp.GetRequiredService<IOptions<SmtpOptions>>().Value;
    Console.WriteLine($"  TimeoutSeconds={opts.TimeoutSeconds} >= MaxRetry={opts.MaxRetry} : PASSED");
}

// -----------------------------------------------------------------------
Console.WriteLine();
Console.WriteLine("=== 4. IValidateOptions<T> — cross-property custom logic ===");
{
    ServiceCollection services = new();
    // Add a section that triggers the cross-property rule in SmtpOptionsValidator
    services.AddOptions<SmtpOptions>()
        .Configure(o =>
        {
            o.Host = "smtp.gmail.com";
            o.Port = 465;             // triggers the validator's cross-property rule
            o.TimeoutSeconds = 30;
            o.MaxRetry = 3;
        })
        .ValidateOnStart();

    services.AddSingleton<IValidateOptions<SmtpOptions>, SmtpOptionsValidator>();

    ServiceProvider sp = services.BuildServiceProvider();
    try
    {
        _ = sp.GetRequiredService<IOptions<SmtpOptions>>().Value;
    }
    catch (OptionsValidationException ex)
    {
        Console.WriteLine("  IValidateOptions FAILED:");
        foreach (string failure in ex.Failures)
            Console.WriteLine($"    - {failure}");
    }
}

Console.ReadLine();
