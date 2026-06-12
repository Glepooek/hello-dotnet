using System;

namespace CsharplangDemo8.Demos
{
    public static class SwitchExpressionDemo
    {
        // ── C# 8 前: switch 语句 ──────────────────────────────────────
        static string OldGrade(int score)
        {
            switch (score / 10)
            {
                case 10:
                case 9:  return "A";
                case 8:  return "B";
                case 7:  return "C";
                case 6:  return "D";
                default: return "F";
            }
        }

        // ── C# 8 起: switch 表达式 ────────────────────────────────────
        // C# 8 的 switch 表达式支持: 常量模式、类型模式、when 守卫
        // 注: 关系模式 (>= 90) 和 or/and 逻辑模式 是 C# 9 特性
        static string Grade(int score)
        {
            // C# 8 兼容: 用 when 守卫实现范围判断
            return score switch
            {
                var s when s >= 90 => "A",
                var s when s >= 80 => "B",
                var s when s >= 70 => "C",
                var s when s >= 60 => "D",
                _                  => "F"
            };
        }

        // ── 枚举的 switch 表达式 ──────────────────────────────────────
        static string DayType(DayOfWeek day) => day switch
        {
            // C# 8: or 模式不可用，用多个 when 或分开写
            DayOfWeek.Saturday => "周末",
            DayOfWeek.Sunday   => "周末",
            DayOfWeek.Monday   => "周一（最艰难）",
            DayOfWeek.Friday   => "周五（最轻松）",
            _                  => "工作日"
        };

        // ── 类型模式 + switch 表达式 ──────────────────────────────────
        static string Describe(object obj) => obj switch
        {
            int n when n < 0    => $"负整数 {n}",
            int n               => $"正整数或零 {n}",
            string s when s.Length == 0 => "空字符串",
            string s            => $"字符串 \"{s}\"",
            null                => "null",
            _                   => $"其他: {obj.GetType().Name}"
        };

        // ── switch 表达式作为方法体 ───────────────────────────────────
        static double Area(string shape, double a, double b) => shape switch
        {
            "rectangle" => a * b,
            "triangle"  => a * b / 2,
            "circle"    => Math.PI * a * a,
            _           => throw new ArgumentException($"未知形状: {shape}")
        };

        public static void Run()
        {
            // 成绩
            Console.WriteLine("  成绩等级:");
            foreach (int s in new[] { 95, 85, 75, 65, 50 })
                Console.WriteLine($"    {s} → {Grade(s)} (旧: {OldGrade(s)})");

            // 星期
            Console.WriteLine("  星期类型:");
            foreach (DayOfWeek d in new[] {
                DayOfWeek.Monday, DayOfWeek.Friday, DayOfWeek.Saturday })
                Console.WriteLine($"    {d} → {DayType(d)}");

            // 类型模式
            Console.WriteLine("  对象描述:");
            foreach (object o in new object[] { -5, 42, "", "hello", null! })
                Console.WriteLine($"    {Describe(o)}");

            // 几何
            Console.WriteLine("  面积:");
            Console.WriteLine($"    rectangle(4,5) = {Area("rectangle", 4, 5):F2}");
            Console.WriteLine($"    triangle(3,6)  = {Area("triangle",  3, 6):F2}");
            Console.WriteLine($"    circle(r=5)    = {Area("circle",    5, 0):F2}");
        }
    }
}
