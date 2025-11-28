using Stylet.Logging;

namespace Stylet.Samples.Logging
{
    public class Bootstrapper : Bootstrapper<ShellViewModel>
    {
        protected override void OnStart()
        {
            //// 启用Stylet日志
            //// 内部默认使用TraceLogger记录日志，将日志输出到输出窗口
            //LogManager.Enabled = true;

            // 配置并启用自定义的MyLogger
            LogManager.LoggerFactory = name => new MyLogger(name);
            LogManager.Enabled = true;
        }
    }
}
