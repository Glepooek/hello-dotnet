using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using System.IO;
using System.Threading.Tasks;

// https://zhuanlan.zhihu.com/p/344370027
// https://www.cnblogs.com/uoyo/p/12509871.html
// https://www.cnblogs.com/jionsoft/p/12249326.html
// CancellationChangeToken
// CompositeChangeToken

namespace ChangeTokenExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string filePath = "d:\\test";
            string fileName = "test.txt";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var fileProvider = new PhysicalFileProvider(filePath);
            // 通知一次
            //IChangeToken changeToken = fileProvider.Watch(fileName);
            //changeToken.RegisterChangeCallback(_ =>
            //{
            //    Logger.ToConsole("file changed!", ConsoleColor.DarkGreen);
            //}, "");

            // 通知多次
            //ChangeToken.OnChange(
            //        () => fileProvider.Watch(fileName),
            //        () => Logger.ToConsole("file changed!", ConsoleColor.DarkGreen)
            //    );

            //for (int i = 0; i < 3; i++)
            //{
            //    Logger.ToConsole("add text to file", ConsoleColor.DarkGray);
            //    await File.AppendAllTextAsync(@"d:\test\test.txt", DateTime.Now.ToString());
            //    await Task.Delay(2000);
            //}

            Console.WriteLine("start adding books...");
            Bookstore store = new Bookstore();
            for (int i = 1; i <= 5; i++)
            {
                store.AddBook(i.ToString(), $"书{i}");
                await Task.Delay(1000);
            }

            Console.ReadKey();
        }
    }
}
