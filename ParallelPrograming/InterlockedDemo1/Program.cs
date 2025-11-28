using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

// https://www.cnblogs.com/whuanle/p/12724371.html

namespace InterlockedDemo1
{
    class Program
    {
        private static int sum = 0;
        private static object obj = new object();

        static void Main(string[] args)
        {
            // 同步执行
            //AddOne();
            //AddOne();
            //AddOne();
            //AddOne();
            //AddOne();
            // 结果是5000000
            //Console.WriteLine("sum = " + sum);

            // 多线程执行
            Task[] tasks = new Task[5];
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 5; i++)
            {
                var task = Task.Run(()=>
                {
                    AddOne();
                });

                tasks[i] = task;
            }

            Task.WaitAll(tasks);
            Console.WriteLine("sum = " + sum);

            stopwatch.Stop();
            Console.WriteLine("耗费时间(ms)：" + stopwatch.ElapsedMilliseconds);

            Console.ReadLine();
        }

        public static void AddOne()
        {
            for (int i = 0; i < 100_0000; i++)
            {
                //sum += 1;

                //lock (obj)
                //{
                //    sum += 1;
                //}

                // 对简单值类型进行原子操作
                Interlocked.Increment(ref sum);
                //Interlocked.Add(ref sum, 1);
            }
        }
    }
}
