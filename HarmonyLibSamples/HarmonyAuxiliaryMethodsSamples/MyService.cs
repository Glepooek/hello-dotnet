using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonyAuxiliaryMethodsSamples
{
    internal class MyService : IDisposable
    {
        public MyService()
        {
            Console.WriteLine("i'm MyService...");
        }

        public void DoSomething()
        {
            Console.WriteLine($"{DateTime.Now} Doing work...");
        }

        public void Dispose()
        {
            Console.WriteLine($"{DateTime.Now} Disposing MyService");
        }
    }
}
