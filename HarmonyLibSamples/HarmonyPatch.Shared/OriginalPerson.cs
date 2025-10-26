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

        public int MakeTotalMoney(int money, int personalIncomeTax = 0)
        {
            Console.WriteLine($"In Original MakeTotalMoney，money parameter: {money}, personalIncomeTax parameter: {personalIncomeTax}");
            return Age * money - personalIncomeTax;
        }

        public IEnumerable<int> GetNumbers()
        {
            Console.WriteLine("In Original GetNumbers");
            yield return 1;
            yield return 2;
            yield return 3;
        }

        public double Divide(double a, double b)
        {
            Console.WriteLine($"In Original Divide, a parameter: {a}, b parameter: {b}");
            if (b == 0)
            {
                throw new InvalidOperationException("parameter b can not be zero.");
            }
            return a / b;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}";
        }
    }
}
