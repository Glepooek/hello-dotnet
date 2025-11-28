using System;
using System.Threading.Tasks;

namespace _02_TaskStatus
{
    class Program
    {
        static void Main(string[] args)
        {

            Task task = new Task(()=>
            {
                Console.WriteLine("Begin");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("Finish");
            });
            Console.WriteLine($"Before start: {task.Status}");
            task.Start();
            Console.WriteLine($"After start: {task.Status}");
            task.Wait();
            Console.WriteLine($"After Finish: {task.Status}");

            Console.ReadLine();
        }
    }
}
