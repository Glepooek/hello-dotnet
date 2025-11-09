using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitSimpleAOPSamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ICalculator original = new Calculator();
                ICalculator proxy = AopProxyGenerator.CreateProxy(original);

                Console.WriteLine("=== 测试 Add 方法 ===");
                int addResult = proxy.Add(10, 20);

                Console.WriteLine("\n=== 测试 Subtract 方法 ===");
                int subResult = proxy.Subtract(50, 15);

                Console.WriteLine($"\n最终结果：10+20={addResult}，50-15={subResult}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"测试异常：{ex}");
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
