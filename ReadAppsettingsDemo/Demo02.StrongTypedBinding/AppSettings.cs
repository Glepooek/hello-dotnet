namespace Demo02.StrongTypedBinding;

public class ApplicationSettings
{
    public string Name { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public int MaxConnections { get; set; }
    public bool EnableCache { get; set; }
    public DatabaseSettings Database { get; set; } = new();
}

public class DatabaseSettings
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string Name { get; set; } = string.Empty;
    public CredentialSettings Credentials { get; set; } = new();
}

public class CredentialSettings
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class FeatureFlagSettings
{
    public bool DarkMode { get; set; }
    public bool BetaApi { get; set; }
    public int MaxUploadMb { get; set; }
}
