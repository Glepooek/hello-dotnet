namespace ConfigurationJsonDemo;

public class AppSettings
{
    public LoggingSettings Logging { get; set; } = new();
    public ApplicationSettings Application { get; set; } = new();
}

public class LoggingSettings
{
    public LogLevelSettings LogLevel { get; set; } = new();
}

public class LogLevelSettings
{
    public string Default { get; set; } = string.Empty;
}

public class ApplicationSettings
{
    public string AppName { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
}
