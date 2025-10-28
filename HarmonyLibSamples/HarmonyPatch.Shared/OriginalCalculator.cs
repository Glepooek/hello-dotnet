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
            return a + b;
        }

        public string SpecialCalculation(string original, int n)
        {
            var parts = original.Split('-');
            var str = string.Join("", parts) + n;
            return str + "Prolog";
        }
    }
}
