using System;
using System.Threading;
using System.Threading.Tasks;

// TaskCompletionSource
// 1、基于回调的异步API，适配回调为Task
// 2、手动控制任务状态
// 等

namespace _05_ThreadSync
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var result = AwaitByTaskCompleteSource(TestAsync);
            Console.WriteLine($"4.Hello World! result: {result}");

            Console.ReadLine();
        }

        private static async Task<string> TestAsync()
        {
            Console.WriteLine("1.start async task");
            await Task.Delay(2000);
            Console.WriteLine("2.end async task");

            return "2s";
        }

        private static string AwaitByTaskCompleteSource(Func<Task<string>> func)
        {
            var taskCompletionSource = new TaskCompletionSource<string>();
            var task1 = taskCompletionSource.Task;
            Task.Run(async () =>
            {
                var result = await func.Invoke();
                taskCompletionSource.SetResult(result);
            });
            var task1Result = task1.Result;
            Console.WriteLine($"3.AwaitByTaskCompleteSource end:{task1Result}");
            return task1Result;
        }
    }
}
