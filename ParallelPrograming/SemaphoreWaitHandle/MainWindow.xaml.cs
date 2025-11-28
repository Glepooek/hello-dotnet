using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

// SemaphoreSlim and Semaphore
// 通常用于限制对某一资源或资源池的并发访问数量。
// SemaphoreSlim用于单进程内
// Semaphore可用于单进程内，也可以多进程使用


namespace SemaphoreWaitHandle;

public partial class MainWindow : Window
{
    #region Fields
    private const int StudentCount = 10;
    private const int MaxThrendCount = 4;

    private SemaphoreSlim mSemaphoreSlim;
    private ThreadLocal<Random> mRandom;

    private BlockingCollection<Student> mQueue;
    #endregion

    #region Constructor
    public MainWindow()
    {
        InitializeComponent();

        mRandom = new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode()));
        AddQueue();

        this.Loaded += MainWindow_Loaded;
    }
    #endregion

    #region EventHandlers
    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        //await SemaphoreTestAsync();
        await SemaphoreTestAsync1();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        AddMessage("================================");
        AddQueue();
    }
    #endregion

    #region 方式一
    private async Task SemaphoreTestAsync()
    {
        mSemaphoreSlim = new SemaphoreSlim(MaxThrendCount, MaxThrendCount);
        Task[] eatings = new Task[StudentCount];

        for (int i = 0; i < StudentCount; i++)
        {
            int studentId = i;
            eatings[i] = Task.Run(async () => await EatAsync(mSemaphoreSlim, studentId));
        }

        await Task.WhenAll(eatings);
    }

    private async Task EatAsync(SemaphoreSlim semaphoreSlim, int studentId)
    {
        await semaphoreSlim.WaitAsync();

        try
        {
            AddMessage($"time: {DateTime.Now}, Student {studentId} is eating now! ");
            int num = mRandom.Value.Next(3, 8); // 使用ThreadLocal<Random>的Value属性获取Random实例
            await Task.Delay(num * 1000);
        }
        finally
        {
            AddMessage($"time: {DateTime.Now}, Student {studentId} has finished eating! ");
            semaphoreSlim.Release();
        }
    }
    #endregion

    #region 方式二
    private async Task SemaphoreTestAsync1()
    {
        Task[] eatings = new Task[MaxThrendCount];
        mSemaphoreSlim = new SemaphoreSlim(MaxThrendCount, MaxThrendCount);

        for (int i = 0; i < MaxThrendCount; i++)
        {
            eatings[i] = Task.Run(async () => await EatAsync(mSemaphoreSlim));
        }

        await Task.WhenAll(eatings);
    }

    private async Task EatAsync(SemaphoreSlim semaphoreSlim)
    {
        while (!mQueue.IsCompleted)
        {
            Student s;
            try
            {
                s = mQueue.Take();
            }
            catch (InvalidOperationException)
            {
                break; // Queue completes adding and is empty
            }

            await semaphoreSlim.WaitAsync();

            try
            {
                AddMessage($"time: {DateTime.Now}, Student {s.Id} is eating now! ");
                // 使用ThreadLocal<Random>的Value属性获取Random实例
                int num = mRandom.Value.Next(3, 8);
                // 模拟延时操作
                await Task.Delay(num * 1000);
            }
            finally
            {
                AddMessage($"time: {DateTime.Now}, Student {s.Id} has finished eating! ");
                semaphoreSlim.Release();
            }

            //if (mQueue.TryTake(out Student s)) // 取不到元素时不阻塞线程
            //{
            //    await semaphoreSlim.WaitAsync();

            //    try
            //    {
            //        AddMessage($"time: {DateTime.Now}, Student {s.Id} is eating now! ");
            //        // 使用ThreadLocal<Random>的Value属性获取Random实例
            //        int num = mRandom.Value.Next(3, 8);
            //        // 模拟延时操作
            //        await Task.Delay(num * 1000);
            //    }
            //    finally
            //    {
            //        AddMessage($"time: {DateTime.Now}, Student {s.Id} has finished eating! ");
            //        semaphoreSlim.Release();
            //    }
            //}
        }
    }
    #endregion

    #region CommonMethods
    private void AddMessage(string msg)
    {
        this.Dispatcher.BeginInvoke((Action)delegate ()
        {
            listbox.Items.Add(msg);
        });
    }

    private void AddQueue()
    {
        if (mQueue == null || mQueue.IsCompleted)
        {
            mQueue = new BlockingCollection<Student>();
        }
        for (int i = 0; i < StudentCount; i++)
        {
            mQueue.Add(new Student() { Id = i });
        }
    }
    #endregion
}
