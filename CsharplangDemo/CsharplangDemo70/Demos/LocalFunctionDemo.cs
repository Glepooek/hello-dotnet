using System;
using System.Collections.Generic;

namespace CsharplangDemo70.Demos
{
    public static class LocalFunctionDemo
    {
        public static void Run()
        {
            // ── C# 7.0: 本地函数 ─────────────────────────────────────────
            // 定义在方法体内部的函数，可以访问外部变量
            // 比 Lambda 更高效（无委托对象分配），支持泛型、递归、out 参数

            // 1. 基本本地函数 (C# 7.0: 不支持 static 修饰符，那是 C# 8 特性)
            int Square(int x) => x * x;
            Console.WriteLine("  Square(5) = " + Square(5));

            // 2. 访问外部变量（闭包）
            int multiplier = 3;
            int MultiplyBy(int n) => n * multiplier;  // 捕获 multiplier
            Console.WriteLine("  MultiplyBy(7) = " + MultiplyBy(7));

            // 3. 递归本地函数
            long Factorial(int n) => n <= 1 ? 1 : n * Factorial(n - 1);
            Console.WriteLine("  Factorial(10) = " + Factorial(10));

            // 4. 带 out 参数的本地函数
            bool TryDivide(int a, int b, out double result)
            {
                if (b == 0) { result = 0; return false; }
                result = (double)a / b;
                return true;
            }
            if (TryDivide(10, 3, out double r))
                Console.WriteLine("  10/3 = " + r.ToString("F4"));

            // 5. 泛型本地函数
            T[] Repeat<T>(T value, int count)
            {
                T[] arr = new T[count];
                for (int i = 0; i < count; i++) arr[i] = value;
                return arr;
            }
            Console.WriteLine("  Repeat<int>(7, 4): [" + string.Join(", ", Repeat(7, 4)) + "]");
            Console.WriteLine("  Repeat<string>(\"x\", 3): [" + string.Join(", ", Repeat("x", 3)) + "]");

            // 6. 本地函数用于延迟验证（迭代器前置条件检查）
            IEnumerable<int> GetRange(int start, int count)
            {
                // 本地函数: 验证立即执行（而非迭代时才触发）
                if (count < 0) throw new ArgumentException("count 不能为负");
                return Iterator();

                IEnumerable<int> Iterator()
                {
                    for (int i = 0; i < count; i++)
                        yield return start + i;
                }
            }

            Console.Write("  GetRange(10, 5): ");
            foreach (int n in GetRange(10, 5)) Console.Write(n + " ");
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("  本地函数 vs Lambda 对比:");
            Console.WriteLine("    本地函数: 支持泛型/递归/out参数，无委托对象开销");
            Console.WriteLine("    Lambda:   可赋给变量/传递，适合作为参数传入");
        }
    }
}
