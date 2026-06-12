using System;
using System.IO;
using System.Threading.Tasks;

namespace CsharplangDemo60.Demos
{
    public static class ExceptionFilterDemo
    {
        // ── C# 6.0: 异常筛选器 when ──────────────────────────────────────
        // C# 6.0 前: catch 后无法基于条件过滤，只能 catch 后再 if/rethrow
        // C# 6.0 起: catch (ExType e) when (condition) 只在条件为真时捕获

        // 模拟 HTTP 请求异常
        static void SimulateRequest(int statusCode)
        {
            throw new InvalidOperationException("HTTP Error " + statusCode);
        }

        // 旧写法: 全部捕获再根据条件重新抛出
        static void OldWay(int code)
        {
            try
            {
                SimulateRequest(code);
            }
            catch (InvalidOperationException e)
            {
                if (e.Message.Contains("404"))
                    Console.WriteLine("  旧写法 处理404: " + e.Message);
                else
                    throw;  // 重新抛出，但栈信息已被破坏
            }
        }

        // 新写法: when 筛选器 — 条件不满足时异常继续向上传播，栈信息完整保留
        static void NewWay(int code)
        {
            try
            {
                SimulateRequest(code);
            }
            catch (InvalidOperationException e) when (e.Message.Contains("404"))
            {
                Console.WriteLine("  when 处理404: " + e.Message);
            }
            catch (InvalidOperationException e) when (e.Message.Contains("500"))
            {
                Console.WriteLine("  when 处理500: " + e.Message);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("  默认处理: " + e.Message);
            }
        }

        // when 可以用于日志记录（条件始终为 false，仅副作用）
        static bool LogException(Exception e)
        {
            Console.WriteLine("  [日志] " + e.GetType().Name + ": " + e.Message);
            return false;  // 返回 false 表示不捕获，只是记录
        }

        static void LogAndRethrow()
        {
            try
            {
                throw new ArgumentException("测试日志记录");
            }
            catch (Exception e) when (LogException(e))
            {
                // 永远不会执行（LogException 返回 false）
                Console.WriteLine("  不会捕获");
            }
            // 但异常会继续传播
        }

        public static void Run()
        {
            // 基本 when 筛选
            Console.WriteLine("  when 筛选器:");
            NewWay(404);
            NewWay(500);
            NewWay(503);

            // 日志记录模式
            Console.WriteLine("  when 日志模式 (不捕获，只记录):");
            try
            {
                LogAndRethrow();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("  外层捕获: " + e.Message);
            }

            Console.WriteLine();
            Console.WriteLine("  when 的优势:");
            Console.WriteLine("    • 条件不满足时异常原样传播，调用栈完整保留");
            Console.WriteLine("    • when(false) 可用于纯副作用（日志）不中断传播");
            Console.WriteLine("    • 比 catch + if + rethrow 更清晰");
        }
    }
}
