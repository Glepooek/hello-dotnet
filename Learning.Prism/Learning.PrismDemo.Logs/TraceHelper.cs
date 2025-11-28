using log4net;
using System;
using System.IO;

namespace Learning.PrismDemo.Logs
{
    /// <summary>
    /// 记录日志帮助类
    /// </summary>
    /// <remarks>
    /// 1、log4net.config中决定记录等级和显示等级
    /// 2、加载log4net.config是在AssemblyInfo.cs文件中配置
    /// </remarks>
    public class TraceHelper
    {
        #region Constructor
        /// <summary>
        /// 构造日志记录对象
        /// </summary>
        /// <param name="loggerName">日志名称</param>
        public TraceHelper(string loggerName)
        {
            string path = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "log4net.config");
            log4net.Config.XmlConfigurator.Configure(new FileInfo(path));
            mLogger = LogManager.GetLogger(loggerName);
        }
        #endregion

        #region Private Fields
        /// <summary>
        /// log4net日志记录
        /// </summary>
        private ILog mLogger;
        #endregion

        #region Public Methods
        /// <summary>
        /// 记录Debug等级的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        public void Debug(string message)
        {
            if (mLogger.IsDebugEnabled)
            {
                mLogger.Debug(message);
            }
        }

        /// <summary>
        /// 记录Debug等级的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常信息</param>
        public void Debug(string message, Exception exception)
        {
            if (mLogger.IsDebugEnabled)
            {
                mLogger.Debug(message, exception);
            }
        }

        /// <summary>
        /// 记录Info等级的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        public void Info(string message)
        {
            if (mLogger.IsInfoEnabled)
            {
                mLogger.Info(message);
            }
        }

        /// <summary>
        /// 记录Info等级的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常信息</param>
        public void Info(string message, Exception exception)
        {
            if (mLogger.IsInfoEnabled)
            {
                mLogger.Info(message, exception);
            }
        }

        /// <summary>
        /// 记录Warn等级的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        public void Warn(string message)
        {
            if (mLogger.IsWarnEnabled)
            {
                mLogger.Warn(message);
            }
        }

        /// <summary>
        /// 记录Warn等级的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常信息</param>
        public void Warn(string message, Exception exception)
        {
            if (mLogger.IsWarnEnabled)
            {
                mLogger.Warn(message, exception);
            }
        }

        /// <summary>
        /// 记录Error等级的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        public void Error(string message)
        {
            if (mLogger.IsErrorEnabled)
            {
                mLogger.Error(message);
            }
        }

        /// <summary>
        /// 记录Error等级的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常信息</param>
        public void Error(string message, Exception exception)
        {
            if (mLogger.IsErrorEnabled)
            {
                mLogger.Error(message, exception);
            }
        }

        /// <summary>
        /// 记录Fatal等级的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        public void Fatal(string message)
        {
            if (mLogger.IsFatalEnabled)
            {
                mLogger.Fatal(message);
            }
        }

        /// <summary>
        /// 记录Fatal等级的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">异常信息</param>
        public void Fatal(string message, Exception exception)
        {
            if (mLogger.IsFatalEnabled)
            {
                mLogger.Fatal(message, exception);
            }
        }
        #endregion
    }
}
