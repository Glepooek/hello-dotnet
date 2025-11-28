using System;
using System.Threading;
using System.Threading.Tasks;

namespace _03_TaskControl
{
    class Program
    {
        static void Main(string[] args)
        {
            Task task = new Task(() =>
            {
                Console.WriteLine($"Hello World! task");
                Thread.Sleep(1000);
            });
            task.Start();

            Task task1 = new Task(() =>
            {
                Console.WriteLine("Hello World! task1");
                Thread.Sleep(1000);
            });
            task1.Start();

            //task.Wait(); // 等待任务完成
            //Task.WaitAll(task, task1); // 等待全部任务完成
            //Task.WaitAny(task, task1);// 等待任一个任务完成
            Task completedTask = Task.WhenAll(task, task1); // 等待全部任务完成
            completedTask.Wait();
            completedTask.ContinueWith(t=> 
            {
                if (t.IsCompleted)
                {
                    Console.WriteLine("所有任务已完成");
                }
            });

            //Task.WhenAny(task, task1);// 等待任一个任务完成

            Console.ReadLine();
        }
    }
}
