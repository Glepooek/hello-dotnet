using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocSamples.Models
{
    public class LoggingSettings
    {
        public const string Logging = "Logging";

        public LogLevel LogLevel { get; set; }
    }

    public class LogLevel
    {
        public string Default { get; set; }
    }

    public class ApplicationSettings
    {
        public const string Application = "Application";

        public string AppName { get; set; }
        public string Version { get; set; }
    }
}