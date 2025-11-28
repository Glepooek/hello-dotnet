using System;
using System.Threading.Tasks;

// 实例化Task
namespace _01_InstantiateTask
{
    class Program
    {
        static void Main(string[] args)
        {
            // 创建，需要启动
            Task task = new Task(() =>
            {
                Console.WriteLine("Hello World!");
            });

            task.Start();

            // 创建并启动
            Task task1 = Task.Factory.StartNew(()=>
            {
                Console.WriteLine("Hello World!");
            });


            Console.ReadLine();
        }
    }
}
