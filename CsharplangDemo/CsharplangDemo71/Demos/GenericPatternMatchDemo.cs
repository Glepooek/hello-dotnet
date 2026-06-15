using System;
using System.Collections.Generic;

namespace CsharplangDemo71.Demos
{
    public static class GenericPatternMatchDemo
    {
        // ── C# 7.1: 泛型类型参数的模式匹配 ──────────────────────────────
        // C# 7.1 之前: is/switch 模式匹配不能直接作用于泛型类型参数 T
        // C# 7.1 起: 可以对 T 使用 is 类型模式和常量模式

        // 判断泛型值是否为某种具体类型
        static string Describe<T>(T value)
        {
            // C# 7.1: is 模式匹配可以作用于泛型参数 T
            if (value is string s)  return "字符串: \"" + s + "\" 长度=" + s.Length;
            if (value is int i)     return "整数: " + i;
            if (value is double d)  return "浮点: " + d.ToString("F2");
            if (value is bool b)    return "布尔: " + b;
            // C# 7.1: T is null 需要 C# 8+，用 object.ReferenceEquals 替代
            if (object.ReferenceEquals(value, null)) return "null";
            return "其他: " + typeof(T).Name + " = " + value;
        }

        // switch 模式匹配作用于泛型参数
        static string ClassifyValue<T>(T value)
        {
            switch (value)
            {
                case string s when s.Length == 0: return "空字符串";
                case string s:                    return "字符串(\"" + s + "\")";
                case int n when n < 0:            return "负整数(" + n + ")";
                case int n when n == 0:           return "零";
                case int n:                       return "正整数(" + n + ")";
                // C# 7.1: switch T case null 需要 C# 8+，转为 object 后判断
                default:
                    if (object.ReferenceEquals(value, null)) return "null";
                    return typeof(T).Name + "(" + value + ")";
            }
        }

        // 泛型集合过滤：只保留特定类型的元素
        static List<TResult> OfType<TSource, TResult>(TSource[] items)
        {
            var result = new List<TResult>();
            foreach (TSource item in items)
            {
                if (item is TResult r)  // C# 7.1: T is TResult 模式
                    result.Add(r);
            }
            return result;
        }

        // 泛型方法中的 null 检查
        static bool IsNullOrDefault<T>(T value)
        {
            // C# 7.1: T is null 需要 C# 8+，用 object.ReferenceEquals 替代
            if (object.ReferenceEquals(value, null)) return true;
            return value.Equals(default(T));
        }

        public static void Run()
        {
            // Describe<T> 泛型模式匹配
            Console.WriteLine("  Describe<T>:");
            Console.WriteLine("    " + Describe("hello"));
            Console.WriteLine("    " + Describe(42));
            Console.WriteLine("    " + Describe(3.14));
            Console.WriteLine("    " + Describe(true));
            Console.WriteLine("    " + Describe((object)null));
            Console.WriteLine("    " + Describe(new int[] { 1, 2, 3 }));

            // switch 模式匹配
            Console.WriteLine("  ClassifyValue<T>:");
            Console.WriteLine("    " + ClassifyValue(""));
            Console.WriteLine("    " + ClassifyValue("hello"));
            Console.WriteLine("    " + ClassifyValue(-5));
            Console.WriteLine("    " + ClassifyValue(0));
            Console.WriteLine("    " + ClassifyValue(7));

            // OfType 泛型过滤
            object[] mixed = { 1, "two", 3, "four", 5.0, "six" };
            List<string> strings = OfType<object, string>(mixed);
            Console.Write("  OfType<object,string>: ");
            foreach (string s in strings) Console.Write("\"" + s + "\" ");
            Console.WriteLine();

            List<int> ints = OfType<object, int>(mixed);
            Console.Write("  OfType<object,int>:    ");
            foreach (int n in ints) Console.Write(n + " ");
            Console.WriteLine();

            // IsNullOrDefault
            Console.WriteLine("  IsNullOrDefault:");
            Console.WriteLine("    null:    " + IsNullOrDefault((string)null));
            Console.WriteLine("    \"\": " + IsNullOrDefault(""));
            Console.WriteLine("    0:       " + IsNullOrDefault(0));
            Console.WriteLine("    \"hi\":  " + IsNullOrDefault("hi"));
            Console.WriteLine("    42:      " + IsNullOrDefault(42));

            Console.WriteLine();
            Console.WriteLine("  C# 7.0: 泛型参数 T 不支持 is/switch 模式匹配");
            Console.WriteLine("  C# 7.1: T 可以用 is string s、switch case int n 等模式");
        }
    }
}
