using Stylet.Logging;
using System;

namespace Stylet.Samples.Logging
{
    public class MyLogger : ILogger
    {
        public MyLogger(string loggerName)
        {
        }

        public void Error(Exception exception, string message = null)
        {
        }

        public void Info(string format, params object[] args)
        {
        }

        public void Warn(string format, params object[] args)
        {
        }
    }
}
