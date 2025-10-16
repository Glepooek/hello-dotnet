using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationJsonDemo
{
    public class AppSettings
    {
        public LoggingSettings Logging { get; set; }
        public ApplicationSettings Application { get; set; }
    }
    public class LoggingSettings
    {
        public LogLevel LogLevel { get; set; }
    }
    public class LogLevel
    {
        public string Default { get; set; }
    }
    public class ApplicationSettings
    {
        public string AppName { get; set; }
        public string Version { get; set; }
    }
}