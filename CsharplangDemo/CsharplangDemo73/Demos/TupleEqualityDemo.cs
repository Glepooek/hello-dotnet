using System;

namespace CsharplangDemo73.Demos
{
    public static class TupleEqualityDemo
    {
        public static void Run()
        {
            // ── C# 7.3: 元组支持 == 和 != 运算符 ─────────────────────────
            // C# 7.0/7.1/7.2: 元组不支持 == 和 !=，只能用 Equals()
            // C# 7.3 起: 按元素逐一比较

            // 基本比较
            var t1 = (1, "hello", true);
            var t2 = (1, "hello", true);
            var t3 = (2, "world", false);

            Console.WriteLine("  t1 = (1, \"hello\", true)");
            Console.WriteLine("  t2 = (1, \"hello\", true)");
            Console.WriteLine("  t3 = (2, \"world\", false)");
            Console.WriteLine("  t1 == t2: " + (t1 == t2));   // true
            Console.WriteLine("  t1 == t3: " + (t1 == t3));   // false
            Console.WriteLine("  t1 != t3: " + (t1 != t3));   // true

            // ── 命名元组同样适用（名称不影响比较）────────────────────────
            var named1 = (Id: 1, Name: "Alice");
            var named2 = (Id: 1, Name: "Alice");
            var anon   = (1, "Alice");              // 无命名
            Console.WriteLine("  named1 == named2: " + (named1 == named2));  // true
            Console.WriteLine("  named1 == anon:   " + (named1 == anon));    // true（名称不参与比较）

            // ── 在 if / while / 集合操作中使用 ───────────────────────────
            var pairs = new (int X, int Y)[]
            {
                (0, 0), (1, 2), (3, 4), (0, 0), (5, 6)
            };

            Console.Write("  原点 (0,0) 出现次数: ");
            int count = 0;
            foreach (var p in pairs)
                if (p == (0, 0)) count++;
            Console.WriteLine(count);

            // ── null 支持: 可空元组比较 ───────────────────────────────────
            (int, string)? nullable1 = (1, "x");
            (int, string)? nullable2 = null;

            Console.WriteLine("  nullable1 == (1,\"x\"): " + (nullable1 == (1, "x")));
            Console.WriteLine("  nullable2 == null:    " + (nullable2 == null));

            // ── 嵌套元组比较 ──────────────────────────────────────────────
            var nested1 = ((1, 2), (3, 4));
            var nested2 = ((1, 2), (3, 4));
            var nested3 = ((1, 2), (3, 5));
            Console.WriteLine("  嵌套元组 nested1==nested2: " + (nested1 == nested2));
            Console.WriteLine("  嵌套元组 nested1==nested3: " + (nested1 == nested3));

            Console.WriteLine();
            Console.WriteLine("  C# 7.2 前: 元组没有 == 运算符，需用 t1.Equals(t2)");
            Console.WriteLine("  C# 7.3 起: == 和 != 按元素逐一比较");
        }
    }
}
