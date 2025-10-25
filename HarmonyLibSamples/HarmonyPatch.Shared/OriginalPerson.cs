using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonyPatch.Shared
{
    public class OriginalPerson
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public OriginalPerson(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        public string GetName()
        {
            Console.WriteLine("In Original GetName");
            return Name;
        }

        public int MakeTotalMoney(int money)
        {
            Console.WriteLine("In Original MakeTotalMoney");
            for (int i = 0; i < 10000; i++)
            {
                
            }
            return Age * money;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}";
        }
    }
}
