using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace Demo10.OptionsValidation;

public class SmtpOptions
{
    // DataAnnotations — declarative rules, validated by ValidateDataAnnotations()
    [Required(ErrorMessage = "Host is required")]
    public string Host { get; set; } = string.Empty;

    [Range(1, 65535, ErrorMessage = "Port must be between 1 and 65535")]
    public int Port { get; set; }

    [Range(1, 300, ErrorMessage = "TimeoutSeconds must be between 1 and 300")]
    public int TimeoutSeconds { get; set; }

    [Range(0, 5, ErrorMessage = "MaxRetry must be between 0 and 5")]
    public int MaxRetry { get; set; }
}

// -----------------------------------------------------------------------
// IValidateOptions<T> — custom validation logic that cannot be expressed
// with DataAnnotations (cross-property rules, external lookups, etc.)
// -----------------------------------------------------------------------
public class SmtpOptionsValidator : IValidateOptions<SmtpOptions>
{
    public ValidateOptionsResult Validate(string? name, SmtpOptions options)
    {
        var errors = new List<string>();

        // Cross-property rule: port 465 implies SSL, which requires a non-gmail host
        if (options.Port == 465 && options.Host.EndsWith("gmail.com", StringComparison.OrdinalIgnoreCase))
            errors.Add("Port 465 with gmail.com is not supported — use port 587 instead.");

        // Business rule: production hosts should not be localhost
        if (options.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase))
            errors.Add("Localhost is not allowed for SMTP in this environment.");

        return errors.Count > 0
            ? ValidateOptionsResult.Fail(errors)
            : ValidateOptionsResult.Success;
    }
}
