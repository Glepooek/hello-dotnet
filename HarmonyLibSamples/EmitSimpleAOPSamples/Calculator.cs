using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmitSimpleAOPSamples
{
    // 2. 目标实现类
    public class Calculator : ICalculator
    {
        public int Add(int a, int b)
        {
            Console.WriteLine($"  [业务逻辑] 执行 Add({a}, {b})");
            return a + b;
        }

        public int Subtract(int a, int b)
        {
            Console.WriteLine($"  [业务逻辑] 执行 Subtract({a}, {b})");
            return a - b;
        }
    }
}
