using System;
using System.Collections.Generic;
using System.Text;

namespace HarmonyPatch.Shared
{
    public class Person : Animal
    {
        public string Name;
        public int Age;

        public Person() { }
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public string SayHello(string prefix)
        {
            return $"{prefix}, 我是{Name}, {Age}岁了！";
        }

        public override void Speak()
        {
            //base.Speak();
            Console.WriteLine("Person speak");
        }
    }
}
