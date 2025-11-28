using Stylet.Logging;

namespace Stylet.Samples.Logging
{
    public class ShellViewModel : Screen
    {
        private readonly ILogger logger;

        public ShellViewModel()
        {
            this.logger = LogManager.GetLogger(nameof(ShellViewModel));
        }

        public void TestLog()
        {
            this.logger.Info("Hello");
        }
    }
}
