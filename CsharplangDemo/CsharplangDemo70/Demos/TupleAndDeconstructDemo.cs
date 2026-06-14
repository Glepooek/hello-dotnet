using System;
using System.Collections.Generic;

namespace CsharplangDemo70.Demos
{
    public static class TupleAndDeconstructDemo
    {
        // ── C# 7.0: 值元组（ValueTuple）语法 ─────────────────────────────
        // C# 4.0 的 Tuple<T1,T2> 是引用类型; C# 7.0 的 (T1, T2) 是值类型

        // 多返回值方法
        static (int Min, int Max, double Avg) Analyze(int[] data)
        {
            int min = data[0], max = data[0];
            double sum = 0;
            foreach (int n in data)
            {
                sum += n;
                if (n < min) min = n;
                if (n > max) max = n;
            }
            return (min, max, sum / data.Length);
        }

        // 嵌套元组
        static ((string First, string Last) Name, int Age) CreatePerson(string first, string last, int age)
            => ((first, last), age);

        // ── 可解构的自定义类型 ────────────────────────────────────────────
        struct Point
        {
            public double X, Y;
            public Point(double x, double y) { X = x; Y = y; }
            // Deconstruct 方法使类型支持解构语法
            public void Deconstruct(out double x, out double y) { x = X; y = Y; }
            public override string ToString() => "(" + X + ", " + Y + ")";
        }

        public static void Run()
        {
            // ── 基本元组 ──────────────────────────────────────────────────
            var t = (1, "hello", 3.14);
            Console.WriteLine("  元组: " + t.Item1 + ", " + t.Item2 + ", " + t.Item3);

            // ── 命名元组 ──────────────────────────────────────────────────
            var person = (Name: "Alice", Age: 30, Score: 95.5);
            Console.WriteLine("  命名: " + person.Name + ", " + person.Age + ", " + person.Score);

            // ── 多返回值 ──────────────────────────────────────────────────
            int[] data = { 5, 3, 8, 1, 9, 2, 7 };
            var result = Analyze(data);
            Console.WriteLine("  Analyze: min=" + result.Min + " max=" + result.Max +
                              " avg=" + result.Avg.ToString("F2"));

            // ── 解构元组 ──────────────────────────────────────────────────
            var (min, max, avg) = Analyze(data);
            Console.WriteLine("  解构: min=" + min + " max=" + max + " avg=" + avg.ToString("F2"));

            // ── 解构到已有变量 ─────────────────────────────────────────────
            int a, b;
            (a, b) = (100, 200);
            Console.WriteLine("  赋值解构: a=" + a + ", b=" + b);

            // ── 交换两个变量（无临时变量）─────────────────────────────────
            int x = 10, y = 20;
            (x, y) = (y, x);
            Console.WriteLine("  交换: x=" + x + ", y=" + y);

            // ── 嵌套元组 ──────────────────────────────────────────────────
            var nested = CreatePerson("Bob", "Smith", 25);
            Console.WriteLine("  嵌套: " + nested.Name.First + " " + nested.Name.Last + ", " + nested.Age);

            // ── 自定义类型解构 ────────────────────────────────────────────
            var p = new Point(3.5, 7.2);
            var (px, py) = p;
            Console.WriteLine("  Point 解构: x=" + px + ", y=" + py);

            // ── 元组作为字典键 ─────────────────────────────────────────────
            var cache = new Dictionary<(int, int), int>();
            cache[(1, 2)] = 3;
            cache[(3, 4)] = 7;
            Console.WriteLine("  元组键: (1,2)=" + cache[(1, 2)] + ", (3,4)=" + cache[(3, 4)]);
        }
    }
}
