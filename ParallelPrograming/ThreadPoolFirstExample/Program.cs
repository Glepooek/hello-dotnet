using System;
using System.Diagnostics;
using System.Threading;

/*ThreadPool
 * 参考文章
 * 1、https://blog.csdn.net/smooth_tailor/article/details/52460566
 * 2、https://www.cnblogs.com/JeffreyZhao/archive/2009/07/22/thread-pool-1-the-goal-and-the-clr-thread-pool.html
 * 3、http://www.albahari.com/
 * 4、https://www.codeproject.com/Articles/7933/Smart-Thread-Pool
 * 
 * 目标：
 * 1、什么是线程池？
 *		线程池可用于执行任务、发送工作项、处理异步 I/O、代表其他线程等待以及处理计时器。
 *		线程池其实就是一个存放线程对象的“池子(pool)”。
 *		
 * 2、为什么用线程池？
 *		线程池中的线程执行完指定的方法后并不会自动消除，而是以挂起状态返回线程池，如果应用程序再次向线程池发出请求，那么处于挂起状态的线程就会被激活并执行任务，而不会创建新线程，这就节约了很多开销。只有当线程数达到最大线程数量，系统才会自动销毁线程。因此，使用线程池可以避免大量的创建和销毁的开支，具有更好的性能和稳定性，其次，开发人员把线程交给系统管理，可以集中精力处理其他任务。
 * 
 * 3、怎么用线程池？
 * 
 */


namespace ThreadPoolFirstExample
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleTest();

            //DeadLock();

            Console.Read();
        }


        static void SimpleTest()
        {
            ThreadPool.SetMinThreads(5, 5);
            ThreadPool.SetMaxThreads(12, 12);

            Stopwatch watch = new Stopwatch();
            watch.Start();

            void callback(object index)
            {
                Console.WriteLine($"{watch.Elapsed}: Task {index} started");
                Thread.Sleep(10000);
                Console.WriteLine($"{watch.Elapsed}: Task {index} finished");
            }

            for (int i = 0; i < 20; i++)
            {
                ThreadPool.QueueUserWorkItem(callback, i);
            }
        }

        static void WaitCallback(object handle)
        {
            ManualResetEvent waitHandle = (ManualResetEvent)handle;

            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(state =>
                {
                    int index = (int)state;
                    if (index == 9)
                    {
                        waitHandle.Set(); // release all 
                    }
                    else
                    {
                        waitHandle.WaitOne(); // wait 
                    }

                    Console.WriteLine(index);
                }, i);
            }
        }

        // 不要阻塞线程池里的线程
        public static void DeadLock()
        {
            ManualResetEvent waitHandle = new ManualResetEvent(false);

            ThreadPool.SetMaxThreads(5, 5);
            ThreadPool.QueueUserWorkItem(WaitCallback, waitHandle);

            waitHandle.WaitOne();
        }
    }
}
