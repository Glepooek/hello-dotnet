using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace CsharplangDemo9.Demos
{
    // ── 模块初始化器: 程序集加载时第一个执行，早于 Main() ────────────────
    public static class AppRegistry
    {
        public static bool IsInitialized { get; private set; }
        public static List<string> Log { get; } = new List<string>();

        [ModuleInitializer]
        internal static void Initialize()
        {
            IsInitialized = true;
            Log.Add($"[{DateTime.Now:HH:mm:ss.fff}] 模块初始化器执行");
            Log.Add("  → 注册全局异常处理");
            Log.Add("  → 预热缓存");
        }
    }

    public static class PluginRegistry
    {
        public static List<string> Plugins { get; } = new List<string>();

        [ModuleInitializer]
        internal static void RegisterPlugins()
        {
            Plugins.Add("CorePlugin");
            Plugins.Add("LoggingPlugin");
            AppRegistry.Log.Add("  → PluginRegistry 初始化完成");
        }
    }

    public static class ModuleInitializerDemo
    {
        public static void Run()
        {
            Console.WriteLine($"  AppRegistry.IsInitialized: {AppRegistry.IsInitialized}");
            Console.WriteLine("  初始化日志:");
            foreach (var entry in AppRegistry.Log)
                Console.WriteLine($"  {entry}");

            Console.WriteLine($"  已注册插件: [{string.Join(", ", PluginRegistry.Plugins)}]");

            Console.WriteLine();
            Console.WriteLine("  [ModuleInitializer] 规则:");
            Console.WriteLine("    • 方法必须: static, void, 无参数, internal/public");
            Console.WriteLine("    • 同一程序集可有多个，执行顺序不保证");
            Console.WriteLine("    • 早于 Main() 执行，是框架初始化的标准入口");
            Console.WriteLine("    • 源生成器常用此机制注册编译期生成的代码");
        }
    }
}
