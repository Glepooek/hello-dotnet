using System;
using System.Threading;
using System.Threading.Tasks;

namespace _04_CancelTask
{
    class Program
    {
        static async Task Main(string[] args)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            token.Register(() =>
            {
                Console.WriteLine("tokenSource canceled");
            });

            // 取消
            //tokenSource.Cancel();

            // 延迟取消
            //tokenSource.CancelAfter(3000);

            // 关联取消
            CancellationTokenSource tokenSource1 =
                CancellationTokenSource.CreateLinkedTokenSource(token);
            tokenSource1.Token.Register(() =>
            {
                Console.WriteLine("tokenSource1 canceled");
            });

            tokenSource.CancelAfter(3000);

            // 判断取消
            while (!tokenSource1.IsCancellationRequested)
            {
                Console.WriteLine("tokenSource1 not cancel, 一直执行");
                await Task.Delay(1000);
            }

            //try
            //{
            //    while (true)
            //    {
            //        //如果操作被取消则直接抛出异常
            //        tokenSource1.Token.ThrowIfCancellationRequested();
            //        System.Console.WriteLine("一直在执行...");
            //        await Task.Delay(1000);
            //    }
            //}
            //catch (Exception ex)
            //{
            //}

            Console.ReadLine();
        }
    }
}
