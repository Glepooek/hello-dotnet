using System;

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

        // ══════════════════════════════════════════════════════════════
        // 异常处理三种方式对比（开发指导）
        // ══════════════════════════════════════════════════════════════
        //
        // 【方式一】catch 后记录日志，不再向上抛出
        //   适用: 当前层可以完全处理异常，调用方无需感知
        //   特点: 日志信息最全，异常在此终止传播
        //
        //     catch (Exception ex)
        //     {
        //         logger.Error(ex);          // StackTrace 完整
        //         // 降级处理或返回默认值
        //     }
        //
        // ──────────────────────────────────────────────────────────────
        //
        // 【方式二】catch 后用 throw 重新抛出（推荐）
        //   适用: 记录日志后仍需让调用方感知异常
        //   特点: 保留原始调用栈，根因位置不丢失
        //
        //     catch (Exception ex)
        //     {
        //         logger.Error(ex);
        //         throw;                     // ✅ 栈从原始出错位置开始
        //     }
        //
        // ──────────────────────────────────────────────────────────────
        //
        // 【方式三】catch 后用 throw ex 抛出（❌ 反模式，避免使用）
        //   问题: 重置 StackTrace，原始出错位置永久丢失
        //         调试时只能看到 catch 所在行，无法追溯根因
        //
        //     catch (Exception ex)
        //     {
        //         logger.Error(ex);
        //         throw ex;                  // ❌ 栈从此行重置，根因丢失
        //     }
        //
        // ──────────────────────────────────────────────────────────────
        //
        // 【方式四】跨层边界包装后抛出（推荐）
        //   适用: 跨架构层（DAL→BLL→Controller）时，用业务语言描述错误
        //   特点: 外层异常描述"业务上发生了什么"
        //         InnerException 保留原始技术细节（栈、类型、消息）
        //         日志框架会递归展开 InnerException 链，信息不丢失
        //
        //     catch (SqlException ex)
        //     {
        //         // InnerException.StackTrace 仍然完整
        //         throw new DataAccessException("查询订单失败", ex);  // ✅
        //     }
        //
        // ──────────────────────────────────────────────────────────────
        //
        // 【方式五】when 筛选器日志模式（C# 6.0，本文件重点）
        //   适用: 需要记录日志但不捕获，让异常原样传播（栈完全不受影响）
        //
        //     catch (Exception ex) when (Log(ex))  // Log 返回 false
        //     {
        //         // 永远不执行，异常继续向上传播
        //     }
        //
        // ══════════════════════════════════════════════════════════════
        // 推荐决策树:
        //   能处理 → 方式一（吃掉异常，记录日志）
        //   不能处理，同层 → 方式二（throw 重抛，栈完整）
        //   跨架构层 → 方式四（包装为业务异常，InnerException 保留原始栈）
        //   只记录不捕获 → 方式五（when 筛选器）
        //   方式三（throw ex）：永远不应使用
        // ══════════════════════════════════════════════════════════════

        static void ThrowStackTraceDemo()
        {
            // 演示 throw vs throw ex 的 StackTrace 差异
            Console.WriteLine("  ── throw vs throw ex 调用栈对比 ──");

            // throw: 保留原始栈
            try
            {
                try
                {
                    int x = 10, y = 0;
                    int _ = x / y;           // DivideByZeroException 从这里抛出
                }
                catch (Exception)
                {
                    throw;                   // 保留原始 StackTrace
                }
            }
            catch (Exception ex)
            {
                // StackTrace 第一帧指向 x / y 所在行
                string firstFrame = ex.StackTrace != null
                    ? ex.StackTrace.Split('\n')[0].Trim()
                    : "(无)";
                Console.WriteLine("  throw    首帧: " + firstFrame);
            }

            // throw ex: 栈从 catch 行重置
            try
            {
                try
                {
                    int x = 10, y = 0;
                    int _ = x / y;
                }
                catch (Exception ex)
                {
#pragma warning disable CA2200 // intentional anti-pattern demo: throw ex resets StackTrace
                    throw ex;                // StackTrace 从此行重新开始
#pragma warning restore CA2200
                }
            }
            catch (Exception ex)
            {
                string firstFrame = ex.StackTrace != null
                    ? ex.StackTrace.Split('\n')[0].Trim()
                    : "(无)";
                Console.WriteLine("  throw ex 首帧: " + firstFrame);
            }

            Console.WriteLine("  → throw 首帧更深（指向真实出错位置），throw ex 首帧更浅（指向 catch 行）");
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

            Console.WriteLine();
            ThrowStackTraceDemo();
        }
    }
}
