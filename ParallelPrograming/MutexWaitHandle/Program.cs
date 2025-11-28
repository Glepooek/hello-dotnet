using System;
using System.Threading;
using System.Threading.Tasks;

// Mutex：互斥体。 基元同步，也可用于进程间同步
// 主要用于实现线程间的互斥，确保同一时刻只有一个线程能够访问共享资源或临界区。

namespace MutexWaitHandle
{
    class Program
    {
        private static Mutex mutex;
        private static int maxThreadNum = 2;
        private static int reult = 0;

        static void Main(string[] args)
        {
            // 防止程序被多次打开
            // Check that there is only one instance of the application running
            mutex = new Mutex(true, "test", out bool isNewInstance);
            if (isNewInstance)
            {
                mutex.ReleaseMutex();
                SimpleTest();
            }
            else
            {

            }
        }

        // 测试进程内同步
        private static void SimpleTest()
        {
            mutex = new Mutex();
            Task[] tasks = new Task[maxThreadNum];

            tasks[0] = Task.Run(() => { Increase(); });
            tasks[1] = Task.Run(() => { Decrease(); });

            Task.WaitAll(tasks);
            Console.WriteLine(reult);
            Console.Read();
        }

        private static void Increase()
        {
            for (int i = 0; i < 100; i++)
            {
                mutex.WaitOne();
                reult += i;
                Console.WriteLine($"++加: {i}, 结果: {reult}++");
                mutex.ReleaseMutex();
            }
        }

        private static void Decrease()
        {
            for (int i = 0; i < 100; i++)
            {
                mutex.WaitOne();
                reult -= i;
                Console.WriteLine($"--减: {i}, 结果: {reult}--");
                mutex.ReleaseMutex();
            }
        }
    }
}
