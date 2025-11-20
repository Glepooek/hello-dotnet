using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonyPatch.Shared
{
    public class OriginalCalculator
    {
        public virtual int Add(int a, int b)
        {
            Console.WriteLine("OriginalCalculator.Add called");
            return a + b;
        }

        public string SpecialCalculation(string original, int n)
        {
            string[] parts = original.Split('-');
            string str = string.Join("", parts) + n;
            return str + "Prolog";
        }
    }
}
