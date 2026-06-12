using System;

namespace CsharplangDemo9.Demos
{
    public static class PatternMatchingDemo
    {
        // ── 关系模式: <, >, <=, >= ────────────────────────────────────────
        static string GradeScore(int score) => score switch
        {
            >= 90 => "优秀",
            >= 80 => "良好",
            >= 60 => "及格",
            _     => "不及格"
        };

        // ── 逻辑模式: and / or / not ──────────────────────────────────────
        static bool IsLetter(char c) =>
            c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z');

        static bool IsDigit(char c) => c is >= '0' and <= '9';

        static bool IsAlphanumeric(char c) => c is
            (>= 'a' and <= 'z') or
            (>= 'A' and <= 'Z') or
            (>= '0' and <= '9');

        // ── not 模式 ──────────────────────────────────────────────────────
        static string DescribeObject(object? obj) => obj switch
        {
            null     => "null 对象",
            not null => $"非 null: {obj.GetType().Name}"
        };

        // ── 与类型模式组合 ────────────────────────────────────────────────
        static string Classify(object value) => value switch
        {
            int n when n > 0      => $"正整数 {n}",
            int n and < 0         => $"负整数 {n}",
            int                   => "零",
            string { Length: 0 }  => "空字符串",
            string s              => $"字符串 \"{s}\"",
            _                     => "其他类型"
        };

        static bool IsWeekend(DayOfWeek d) =>
            d is DayOfWeek.Saturday or DayOfWeek.Sunday;

        static bool IsWorkHour(int hour) =>
            hour is (>= 9 and <= 12) or (>= 14 and <= 18);

        public static void Run()
        {
            Console.WriteLine("  成绩等级:");
            foreach (var s in new[] { 95, 82, 70, 45 })
                Console.WriteLine($"    {s} 分 → {GradeScore(s)}");

            Console.WriteLine("  字符判断:");
            Console.WriteLine($"    'a' IsLetter:      {IsLetter('a')}");
            Console.WriteLine($"    '3' IsDigit:       {IsDigit('3')}");
            Console.WriteLine($"    '$' IsAlphanumeric:{IsAlphanumeric('$')}");

            Console.WriteLine("  not null 模式:");
            Console.WriteLine($"    null  → {DescribeObject(null)}");
            Console.WriteLine($"    \"hi\" → {DescribeObject("hi")}");

            Console.WriteLine("  分类:");
            foreach (var v in new object[] { 42, -5, 0, "", "hello" })
                Console.WriteLine($"    {v.GetType().Name}({v}) → {Classify(v)}");

            Console.WriteLine("  日期:");
            Console.WriteLine($"    周六 IsWeekend: {IsWeekend(DayOfWeek.Saturday)}");
            Console.WriteLine($"    周一 IsWeekend: {IsWeekend(DayOfWeek.Monday)}");

            Console.WriteLine("  工作时间:");
            Console.WriteLine($"    10点 IsWorkHour: {IsWorkHour(10)}");
            Console.WriteLine($"    13点 IsWorkHour: {IsWorkHour(13)}");
            Console.WriteLine($"    15点 IsWorkHour: {IsWorkHour(15)}");
        }
    }
}
