using System;
using System.IO;
using System.Threading.Tasks;

namespace CsharplangDemo60.Demos
{
    public static class AwaitInCatchDemo
    {
        // ── C# 6.0: Catch/Finally 块中可以使用 await ─────────────────────
        // C# 5.0: await 只能在 try 块中，catch/finally 不支持
        // C# 6.0: catch 和 finally 块中也可以 await

        static async Task<string> LoadDataAsync(string source)
        {
            await Task.Delay(10);
            if (source == "bad") throw new IOException("数据源不可用: " + source);
            return "来自 " + source + " 的数据";
        }

        static async Task LogAsync(string message)
        {
            await Task.Delay(1);  // 模拟异步日志写入
            Console.WriteLine("  [AsyncLog] " + message);
        }

        static async Task CleanupAsync(string resource)
        {
            await Task.Delay(1);  // 模拟异步清理
            Console.WriteLine("  [AsyncCleanup] 释放: " + resource);
        }

        // C# 6.0: 在 catch 块中 await
        static async Task<string> LoadWithFallbackAsync(string primary, string fallback)
        {
            try
            {
                return await LoadDataAsync(primary);
            }
            catch (IOException ex)
            {
                // C# 6.0 前: 不允许在 catch 中 await
                // C# 6.0 起: 可以直接 await
                await LogAsync("主源失败: " + ex.Message + "，切换到备用源");
                return await LoadDataAsync(fallback);  // await in catch ✅
            }
        }

        // C# 6.0: 在 finally 块中 await
        static async Task ProcessWithCleanupAsync(string resource)
        {
            Console.WriteLine("  处理开始: " + resource);
            try
            {
                await LoadDataAsync(resource);
                Console.WriteLine("  处理成功");
            }
            finally
            {
                // C# 6.0 前: finally 中不允许 await
                // C# 6.0 起: 可以 await 异步清理
                await CleanupAsync(resource);  // await in finally ✅
            }
        }

        public static void Run()
        {
            // await in catch: 失败时切换备用源
            Console.WriteLine("  await in catch (主源失败 → 备用源):");
            // GetAwaiter().GetResult() 仅用于同步 Main 入口调用异步方法，生产代码应 await 到顶层
            string result = LoadWithFallbackAsync("bad", "backup").GetAwaiter().GetResult();
            Console.WriteLine("  结果: " + result);

            Console.WriteLine();

            // await in finally: 确保异步清理
            Console.WriteLine("  await in finally (确保异步资源释放):");
            ProcessWithCleanupAsync("primary").GetAwaiter().GetResult();

            Console.WriteLine();
            Console.WriteLine("  C# 5.0: catch/finally 中不允许 await");
            Console.WriteLine("  C# 6.0: 可以在 catch/finally 中 await，支持异步日志/清理/回滚");
        }
    }
}
