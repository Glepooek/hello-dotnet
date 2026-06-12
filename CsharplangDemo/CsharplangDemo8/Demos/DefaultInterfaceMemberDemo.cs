using System;
using System.Text;

namespace CsharplangDemo8.Demos
{
    // ── 默认接口成员: 接口可以提供方法的默认实现 ─────────────────────────
    // 用途: 接口版本演化——新增方法时现有实现类无需修改
    // 需要 .NET Core 3.0+（CLR 支持）

    public interface ILogger
    {
        // 抽象方法: 实现类必须提供
        void Log(string message);

        // C# 8: 带默认实现的方法——实现类可以不重写
        void LogError(string message)   => Log($"[ERROR] {message}");
        void LogWarning(string message) => Log($"[WARN]  {message}");
        void LogInfo(string message)    => Log($"[INFO]  {message}");

        // 默认实现也可以有逻辑
        void LogException(Exception ex) =>
            Log($"[EXCEPTION] {ex.GetType().Name}: {ex.Message}");
    }

    // ── 旧实现类: 只实现了 Log，自动获得所有默认方法 ──────────────────
    public class ConsoleLogger : ILogger
    {
        public void Log(string message) => Console.WriteLine($"  {message}");
        // 无需修改，即可通过接口调用 LogError/LogWarning/LogInfo
    }

    // ── 新实现类: 覆盖部分默认实现 ────────────────────────────────────
    public class PrefixLogger : ILogger
    {
        private readonly string _prefix;
        public PrefixLogger(string prefix) { _prefix = prefix; }

        public void Log(string message) =>
            Console.WriteLine($"  [{_prefix}] {message}");

        // 覆盖默认的 LogError，加红色
        public void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Log($"ERROR: {message}");
            Console.ResetColor();
        }
    }

    // ── 带静态成员的接口 (C# 8 也支持接口中的静态方法) ──────────────
    public interface IFormatter
    {
        string Format(string input);

        // 静态工厂方法
        static IFormatter CreateDefault() => new UpperFormatter();
    }

    public class UpperFormatter : IFormatter
    {
        public string Format(string input) => input.ToUpperInvariant();
    }

    public static class DefaultInterfaceMemberDemo
    {
        public static void Run()
        {
            // ── ConsoleLogger 自动获得默认方法 ────────────────────────
            ILogger logger = new ConsoleLogger();
            logger.Log("这是原始 Log");
            logger.LogInfo("这是通过默认实现调用的 LogInfo");
            logger.LogWarning("这是默认 LogWarning");
            logger.LogError("这是默认 LogError");

            Console.WriteLine();

            // ── PrefixLogger 覆盖了 LogError ─────────────────────────
            ILogger prefixed = new PrefixLogger("APP");
            prefixed.Log("原始 Log");
            prefixed.LogInfo("默认 LogInfo（通过 Log 调用）");
            prefixed.LogError("覆盖的 LogError（带颜色）");

            // ── 默认 LogException ────────────────────────────────────
            Console.WriteLine();
            logger.LogException(new InvalidOperationException("演示异常"));

            // ── 接口静态方法 ─────────────────────────────────────────
            Console.WriteLine();
            var fmt = IFormatter.CreateDefault();
            Console.WriteLine($"  IFormatter.CreateDefault().Format(\"hello\") = {fmt.Format("hello")}");

            Console.WriteLine();
            Console.WriteLine("  默认接口成员的价值:");
            Console.WriteLine("    • 接口版本演化: 新增方法不破坏现有实现类");
            Console.WriteLine("    • 类似 Mixin: 通过接口复用行为");
            Console.WriteLine("    • 注意: 只能通过接口类型变量调用默认方法，不能通过具体类型");
        }
    }
}
