using System;
using System.Collections.Generic;
using System.Text;

namespace HarmonyPatch.Shared
{
    public class Person
    {
        public string Name;
        public int Age;

        public Person() { }
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
