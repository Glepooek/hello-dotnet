using System;
using System.Collections.Generic;

namespace CsharplangDemo70.Demos
{
    public static class OutVariableDemo
    {
        public static void Run()
        {
            // ── C# 7.0 前: out 参数必须先声明变量 ──────────────────────
            int oldResult;
            bool oldOk = int.TryParse("42", out oldResult);
            Console.WriteLine("  旧写法: ok=" + oldOk + ", result=" + oldResult);

            // ── C# 7.0 起: out 变量内联声明 ─────────────────────────────
            // 声明和使用合并在一处，变量作用域延伸到当前块
            bool ok1 = int.TryParse("42", out int parsed);
            Console.WriteLine("  out int: ok=" + ok1 + ", parsed=" + parsed);

            bool ok2 = int.TryParse("abc", out int failed);
            Console.WriteLine("  失败时:  ok=" + ok2 + ", failed=" + failed); // 0

            // ── 在 if 条件中直接使用 ─────────────────────────────────────
            if (int.TryParse("123", out int n))
                Console.WriteLine("  if 内联: n=" + n);

            // ── out var: 让编译器推断类型 ─────────────────────────────────
            if (int.TryParse("99", out var v))
                Console.WriteLine("  out var: v=" + v + " (" + v.GetType().Name + ")");

            // ── 配合 Dictionary.TryGetValue ──────────────────────────────
            var dict = new Dictionary<string, int>
            {
                { "x", 10 }, 
                { "y", 20 }
            };

            if (dict.TryGetValue("x", out int xVal))
                Console.WriteLine("  TryGetValue: x=" + xVal);

            if (!dict.TryGetValue("z", out int zVal))
                Console.WriteLine("  TryGetValue miss: zVal=" + zVal); // 0

            // ── 多个 out 参数 ─────────────────────────────────────────────
            void GetMinMax(int[] arr, out int min, out int max)
            {
                min = max = arr[0];
                foreach (int x in arr)
                {
                    if (x < min) min = x;
                    if (x > max) max = x;
                }
            }

            GetMinMax(new[] { 3, 1, 4, 1, 5, 9, 2, 6 }, out int lo, out int hi);
            Console.WriteLine("  min=" + lo + ", max=" + hi);
        }
    }
}
