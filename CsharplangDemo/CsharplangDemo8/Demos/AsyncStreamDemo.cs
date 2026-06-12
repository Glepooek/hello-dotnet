using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace CsharplangDemo8.Demos
{
    public static class AsyncStreamDemo
    {
        // ── IAsyncEnumerable<T>: 异步生产者 ──────────────────────────────
        // C# 8 前: 只能一次性返回 Task<IEnumerable<T>>，必须等全部生成完
        // C# 8 起: 逐条异步 yield，消费者可以边生产边消费

        static async IAsyncEnumerable<int> CountAsync(
            int from, int to, int delayMs = 50,
            [EnumeratorCancellation] CancellationToken ct = default)
        {
            for (int i = from; i <= to; i++)
            {
                ct.ThrowIfCancellationRequested();
                await Task.Delay(delayMs, ct);
                yield return i;
            }
        }

        // ── 模拟分页 API: 每次请求一页数据 ──────────────────────────────
        static async IAsyncEnumerable<string> FetchPagesAsync(int totalPages)
        {
            for (int page = 1; page <= totalPages; page++)
            {
                await Task.Delay(20);  // 模拟网络请求
                yield return $"Page-{page}: [item{page*2-1}, item{page*2}]";
            }
        }

        // ── 异步 LINQ (ToListAsync 手动实现) ─────────────────────────────
        static async Task<List<T>> ToListAsync<T>(IAsyncEnumerable<T> source)
        {
            var result = new List<T>();
            await foreach (var item in source)
                result.Add(item);
            return result;
        }

        public static async Task RunAsync()
        {
            // 1. 基本 await foreach
            Console.Write("  CountAsync(1..5): ");
            await foreach (var n in CountAsync(1, 5))
                Console.Write($"{n} ");
            Console.WriteLine();

            // 2. 取消令牌
            using var cts = new CancellationTokenSource();
            cts.CancelAfter(120);  // 120ms 后取消

            Console.Write("  带取消: ");
            try
            {
                await foreach (var n in CountAsync(1, 20, 50, cts.Token))
                    Console.Write($"{n} ");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("(已取消)");
            }

            // 3. 分页 API 场景
            Console.WriteLine("  分页数据:");
            await foreach (var page in FetchPagesAsync(3))
                Console.WriteLine($"    {page}");

            // 4. 收集所有结果
            var all = await ToListAsync(CountAsync(10, 15));
            Console.WriteLine($"  ToListAsync: [{string.Join(", ", all)}]");

            Console.WriteLine();
            Console.WriteLine("  IAsyncEnumerable<T> 填补了 IEnumerable<T> 与 Task<T> 之间的空白:");
            Console.WriteLine("    IEnumerable<T>          = 同步序列");
            Console.WriteLine("    Task<IEnumerable<T>>    = 异步批量（需等全部完成）");
            Console.WriteLine("    IAsyncEnumerable<T>     = 异步流式序列（边产生边消费）");
        }
    }
}
