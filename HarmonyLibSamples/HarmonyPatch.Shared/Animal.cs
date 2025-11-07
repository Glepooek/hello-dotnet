using System;
using System.Collections.Generic;
using System.Text;

namespace HarmonyPatch.Shared
{
    public class Animal
    {
        public virtual void Speak()
        {
            Console.WriteLine("动物在叫");
        }

        public virtual void Eat()
        {
            Console.WriteLine("动物在吃饭");
        }
    }
}
