using System;
using System.Collections.Generic;

namespace CsharplangDemo8.Demos
{
    public static class StaticLocalFunctionDemo
    {
        // ── 静态本地函数: 禁止捕获外部变量 ──────────────────────────────
        // C# 7 引入本地函数，C# 8 新增 static 修饰符
        // 与 static Lambda 同理: 明确表达"不依赖外部状态"

        static int ProcessData(int[] data)
        {
            // 非静态本地函数: 可以捕获 data
            int NonStaticSum() => Sum(data);  // 捕获了 data

            // 静态本地函数: 禁止捕获外部变量，必须通过参数传入
            static int Sum(int[] arr)
            {
                int total = 0;
                foreach (var n in arr) total += n;
                return total;
                // 不能访问 data / ProcessData 的任何局部变量
            }

            static double Average(int[] arr, int count)
                => count > 0 ? (double)Sum(arr) / count : 0;

            static int Max(int[] arr)
            {
                if (arr.Length == 0) throw new ArgumentException("Empty array");
                int max = arr[0];
                foreach (var n in arr) if (n > max) max = n;
                return max;
            }

            Console.WriteLine($"  Sum:     {Sum(data)}");
            Console.WriteLine($"  Average: {Average(data, data.Length):F2}");
            Console.WriteLine($"  Max:     {Max(data)}");

            return NonStaticSum();
        }

        // ── 递归静态本地函数 ─────────────────────────────────────────────
        static long ComputeFactorial(int n)
        {
            return Factorial(n);

            static long Factorial(int x) => x <= 1 ? 1 : x * Factorial(x - 1);
        }

        // ── 泛型静态本地函数 ─────────────────────────────────────────────
        static List<T> BuildList<T>(params T[] items)
        {
            return CreateList(items);

            static List<T> CreateList(T[] arr)
            {
                var list = new List<T>(arr.Length);
                foreach (var item in arr) list.Add(item);
                return list;
            }
        }

        public static void Run()
        {
            int[] data = { 5, 3, 8, 1, 9, 2, 7, 4, 6 };
            Console.WriteLine("  数组分析:");
            ProcessData(data);

            Console.WriteLine("  阶乘:");
            for (int i = 1; i <= 6; i++)
                Console.WriteLine($"    {i}! = {ComputeFactorial(i)}");

            Console.WriteLine("  泛型静态本地函数:");
            var list = BuildList(10, 20, 30, 40);
            Console.WriteLine($"    BuildList(10,20,30,40) = [{string.Join(", ", list)}]");

            Console.WriteLine();
            Console.WriteLine("  static 本地函数 vs 普通本地函数:");
            Console.WriteLine("    普通: 可以捕获外部变量（可能产生闭包分配）");
            Console.WriteLine("    static: 禁止捕获，编译器保证无闭包，明确意图");
        }
    }
}
