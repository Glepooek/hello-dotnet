using Microsoft.Extensions.Configuration;

// -----------------------------------------------------------------------
// Demo06: User Secrets
//
// Problem: Passwords and API keys must NOT be committed to source control.
// Solution: dotnet user-secrets stores them outside the repo in:
//   Windows: %APPDATA%\Microsoft\UserSecrets\<UserSecretsId>\secrets.json
//   Linux/Mac: ~/.microsoft/usersecrets/<UserSecretsId>/secrets.json
//
// The UserSecretsId GUID in the .csproj links the project to its secret store.
// AddUserSecrets<T>() loads secrets.json and overlays it on top of JSON.
//
// Setup commands (already run for this demo):
//   dotnet user-secrets init
//   dotnet user-secrets set "Database:Password" "super-secret-password"
//   dotnet user-secrets set "ApiKey" "sk-demo-abc123xyz"
//   dotnet user-secrets list
//   dotnet user-secrets remove "ApiKey"
//   dotnet user-secrets clear
//
// IMPORTANT: User Secrets is a development-only feature.
//   In production, use environment variables or a secrets manager (Azure Key Vault, etc.)
// -----------------------------------------------------------------------

bool isDevelopment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") != "Production";

IConfigurationBuilder builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false);

if (isDevelopment)
{
    // AddUserSecrets<Program>() reads UserSecretsId from the assembly that
    // contains the type argument — here we use the top-level Program class.
    builder.AddUserSecrets<Program>();
}

IConfiguration config = builder.Build();

Console.WriteLine("=== Demo06: User Secrets ===");
Console.WriteLine($"Environment : {(isDevelopment ? "Development" : "Production")}");
Console.WriteLine();

Console.WriteLine("--- Values from appsettings.json (safe to commit) ---");
Console.WriteLine($"AppName          : {config["AppName"]}");
Console.WriteLine($"Database:Host    : {config["Database:Host"]}");
Console.WriteLine($"Database:Name    : {config["Database:Name"]}");
Console.WriteLine($"Database:Username: {config["Database:Username"]}");

Console.WriteLine();
Console.WriteLine("--- Values from User Secrets store (NOT in repo) ---");
string? password = config["Database:Password"];
string? apiKey = config["ApiKey"];

// Mask secrets in output — never print raw secrets in real apps
Console.WriteLine($"Database:Password: {MaskSecret(password)}");
Console.WriteLine($"ApiKey           : {MaskSecret(apiKey)}");

Console.WriteLine();
Console.WriteLine("--- Secret store location ---");
string secretsId = "4288c148-7b12-4f78-aa5e-42cdb1e4f1e9";
string secretsPath = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
    "Microsoft", "UserSecrets", secretsId, "secrets.json");
Console.WriteLine($"secrets.json at: {secretsPath}");
Console.WriteLine($"File exists    : {File.Exists(secretsPath)}");

Console.ReadLine();

static string MaskSecret(string? value)
{
    if (string.IsNullOrEmpty(value))
        return "(not set)";
    if (value.Length <= 4)
        return "****";
    return value[..2] + new string('*', value.Length - 4) + value[^2..];
}
