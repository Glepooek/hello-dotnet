using System;
using System.Collections.Generic;
using System.Threading;

namespace CsharplangDemo71.Demos
{
    public static class DefaultLiteralDemo
    {
        // ── C# 7.1: default 字面量（无需重复类型名）──────────────────────
        // C# 7.0 前: default(T) 需要写出类型名
        // C# 7.1 起: default 可由编译器从上下文推断类型，省略括号和类型名

        // 方法参数默认值
        static void Process(
            int count = default,           // 等同 default(int) = 0
            string label = default,        // 等同 default(string) = null
            bool verbose = default,        // 等同 default(bool) = false
            CancellationToken ct = default) // 等同 default(CancellationToken)
        {
            Console.WriteLine("  count=" + count + " label=" + (label ?? "null") +
                              " verbose=" + verbose + " ct.CanBeCanceled=" + ct.CanBeCanceled);
        }

        // 泛型中的 default
        static T GetDefault<T>() => default;  // 等同 default(T)

        static T FirstOrDefault<T>(T[] arr) => arr.Length > 0 ? arr[0] : default;

        // 与可空类型配合
        static int? FindIndex<T>(T[] arr, T value) where T : IEquatable<T>
        {
            for (int i = 0; i < arr.Length; i++)
                if (arr[i].Equals(value)) return i;
            return default;  // 等同 default(int?) = null
        }

        public static void Run()
        {
            // ── 变量声明中使用 default ─────────────────────────────────
            // (旧写法 default(int) 与新写法对比)
            int    numNew = default;           // C# 7.1: 从赋值目标类型推断

            string strNew = default;
            bool   boolNew = default;
            double dblNew  = default;

            Console.WriteLine("  变量初始化:");
            Console.WriteLine("    int    default = " + numNew);
            Console.WriteLine("    string default = " + (strNew ?? "null"));
            Console.WriteLine("    bool   default = " + boolNew);
            Console.WriteLine("    double default = " + dblNew);

            // ── 方法参数默认值 ─────────────────────────────────────────
            Console.WriteLine("  方法参数 default:");
            Process();
            Process(count: 5, label: "test");

            // ── 泛型方法 ──────────────────────────────────────────────
            Console.WriteLine("  泛型 default:");
            Console.WriteLine("    GetDefault<int>    = " + GetDefault<int>());
            Console.WriteLine("    GetDefault<string> = " + (GetDefault<string>() ?? "null"));
            Console.WriteLine("    GetDefault<bool>   = " + GetDefault<bool>());

            int[] nums = { 10, 20, 30 };
            Console.WriteLine("    FirstOrDefault<int>([10,20,30]) = " + FirstOrDefault(nums));

            int[] empty = new int[0];
            Console.WriteLine("    FirstOrDefault<int>([]) = " + FirstOrDefault(empty));

            // ── 条件表达式中的 default ────────────────────────────────
            string input = null;
            string result = input != null ? input.ToUpper() : default;
            Console.WriteLine("  条件 default = " + (result ?? "null"));

            // ── 集合元素填充 ──────────────────────────────────────────
            var list = new List<int> { 1, 2, 3 };
            int found = list.Count > 10 ? list[10] : default;
            Console.WriteLine("  集合越界 default = " + found);

            Console.WriteLine();
            Console.WriteLine("  C# 7.0: default(int)、default(string)");
            Console.WriteLine("  C# 7.1: default（从上下文推断类型，省略类型名）");
        }
    }
}
