using System;
using System.Collections.Generic;

namespace CsharplangDemo73.Demos
{
    public static class ExpressionVariableDemo
    {
        static bool TryGetValue(string key, out int value)
        {
            var map = new Dictionary<string, int>
            {
                { "x", 42 }, { "y", 100 }
            };
            return map.TryGetValue(key, out value);
        }

        public static void Run()
        {
            // ── C# 7.3: out var 和 is 变量可在更多位置声明和使用 ──────

            // 1. switch 条件中使用表达式变量
            // C# 7.3 前: out var 不能直接嵌在 switch 表达式中
            switch (int.TryParse("42", out int parsed) ? parsed : -1)
            {
                case int n when n > 0:
                    Console.WriteLine("  switch 中 out var: parsed=" + parsed + ", n=" + n);
                    break;
                default:
                    Console.WriteLine("  解析失败");
                    break;
            }

            // 2. while 条件中使用 out var
            var queue = new Queue<string>(new[] { "a", "b", "c" });
            Console.Write("  while out var: ");
            // TryDequeue 在 C# 7.3 时代需要手动实现，这里用 Count>0 + Dequeue
            while (queue.Count > 0)
            {
                string item = queue.Dequeue();
                Console.Write(item + " ");
            }
            Console.WriteLine();

            // 3. is 模式变量在更多上下文中使用
            object obj = "hello world";
            if (obj is string s && s.Length > 5)
                Console.WriteLine("  is string s: \"" + s + "\" 长度=" + s.Length);

            // 4. 在初始化器中使用表达式变量
            // C# 7.3 扩展了表达式变量的适用范围（字段初始化器等）
            Console.WriteLine("  TryGetValue(\"x\"): " +
                (TryGetValue("x", out int xVal) ? "found=" + xVal : "not found"));
            Console.WriteLine("  TryGetValue(\"z\"): " +
                (TryGetValue("z", out int zVal) ? "found=" + zVal : "not found"));

            // 5. 三元表达式中的 out var
            string result = int.TryParse("123", out int num)
                ? "解析成功: " + num
                : "解析失败";
            Console.WriteLine("  三元 out var: " + result);

            // 6. 嵌套 is 模式匹配
            object[] items = { 42, "text", 3.14, (object)null };
            Console.Write("  is 模式筛选 int: ");
            foreach (var item in items)
                if (item is int i) Console.Write(i + " ");
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("  C# 7.3: 表达式变量 (out var / is var) 适用范围扩展");
            Console.WriteLine("  可用于 switch 条件、更复杂的表达式上下文");
        }
    }
}
