using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

// ManualResetEvent and AutoResetEvent
// 主要负责多线程中的线程同步，用于在多线程中通知或阻止线程的执行。
// Set：将一个事件设置为有信号，这样被阻塞的线程就会继续下去
// Reset：将一个事件设置为无信号，这样尝试继续的线程就会被阻塞
// 区别：执行Set后，AutoResetEvent会自动Reset，ManualResetEvent需要手动Reset

namespace ManualAutoEventWaitHandle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource mCancellationTokenSource;
        private ManualResetEvent mManualResetEvent;
        private ManualResetEventSlim mManualResetEventSlim;
        private AutoResetEvent mAutoResetEvent;

        public MainWindow()
        {
            InitializeComponent();

            mManualResetEventSlim = new ManualResetEventSlim(true);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Heart();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            mManualResetEventSlim.Reset();
        }

        private void Resume_Click(object sender, RoutedEventArgs e)
        {
            mManualResetEventSlim.Set();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            mCancellationTokenSource?.Cancel();
            mCancellationTokenSource?.Dispose();

            mManualResetEventSlim.Dispose();
            mManualResetEventSlim = null;

            listbox.Items.Add("task is canceled");
        }

        private void Heart()
        {
            if (mCancellationTokenSource == null || mCancellationTokenSource.IsCancellationRequested)
            {
                mCancellationTokenSource = new CancellationTokenSource();
            }

            mManualResetEventSlim ??= new ManualResetEventSlim(true);

            Task.Run(async () =>
            {
                while (!mCancellationTokenSource.IsCancellationRequested)
                {
                    mManualResetEventSlim.Wait();

                    this.Dispatcher.Invoke(() =>
                    {
                        var item = $"time: {DateTime.Now},  threadId1: {Thread.CurrentThread.ManagedThreadId}";
                        listbox.Items.Add(item);
                        listbox.ScrollIntoView(item);
                    });

                    await Task.Delay(4000);
                }
            }, mCancellationTokenSource.Token);

            Task.Run(async () =>
            {
                while (!mCancellationTokenSource.IsCancellationRequested)
                {
                    mManualResetEventSlim.Wait();

                    this.Dispatcher.Invoke(() =>
                    {
                        var item = $"time: {DateTime.Now},  threadId2: {Thread.CurrentThread.ManagedThreadId}";
                        listbox.Items.Add(item);
                        listbox.ScrollIntoView(item);
                    });

                    await Task.Delay(2000);
                }
            }, mCancellationTokenSource.Token);

        }
    }
}
