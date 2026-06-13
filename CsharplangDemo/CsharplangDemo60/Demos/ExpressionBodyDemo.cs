using System;
using System.Collections.Generic;

namespace CsharplangDemo60.Demos
{
    public static class ExpressionBodyDemo
    {
        // ── C# 6.0: 表达式主体成员（方法和属性）─────────────────────────
        // C# 7.0 将其扩展到构造函数/析构函数/setter
        // C# 6.0 支持: 方法、只读属性、运算符

        class Vector
        {
            public double X { get; }
            public double Y { get; }

            public Vector(double x, double y) { X = x; Y = y; }

            // C# 6.0: 方法表达式主体 =>
            public double Length() => Math.Sqrt(X * X + Y * Y);

            // C# 6.0: 只读属性表达式主体 =>
            public double LengthSquared => X * X + Y * Y;
            public bool IsZero => X == 0 && Y == 0;

            // C# 6.0: 运算符表达式主体 =>
            public static Vector operator +(Vector a, Vector b)
                => new Vector(a.X + b.X, a.Y + b.Y);
            public static Vector operator *(Vector v, double scalar)
                => new Vector(v.X * scalar, v.Y * scalar);

            // C# 6.0: 重写 ToString 表达式主体
            public override string ToString()
                => "(" + X.ToString("F2") + ", " + Y.ToString("F2") + ")";
        }

        class Calculator
        {
            private double _memory = 0;

            // 方法表达式主体
            public double Add(double a, double b) => a + b;
            public double Sub(double a, double b) => a - b;
            public double Mul(double a, double b) => a * b;
            public double Div(double a, double b) => b != 0 ? a / b : double.NaN;

            // 只读属性
            public double Memory => _memory;
            public bool HasMemory => _memory != 0;

            // void 方法也可以用表达式主体
            public void StoreMemory(double value) => _memory = value;
            public void ClearMemory() => _memory = 0;
        }

        public static void Run()
        {
            // Vector 演示
            var v1 = new Vector(3, 4);
            var v2 = new Vector(1, 2);
            Console.WriteLine("  v1 = " + v1 + ", Length = " + v1.Length().ToString("F2"));
            Console.WriteLine("  v2 = " + v2);
            Console.WriteLine("  v1 + v2 = " + (v1 + v2));
            Console.WriteLine("  v1 * 2  = " + (v1 * 2));
            Console.WriteLine("  IsZero: " + v1.IsZero);

            // Calculator 演示
            var calc = new Calculator();
            Console.WriteLine("  3 + 4 = " + calc.Add(3, 4));
            Console.WriteLine("  10 / 3 = " + calc.Div(10, 3).ToString("F4"));
            calc.StoreMemory(42);
            Console.WriteLine("  Memory = " + calc.Memory + ", HasMemory = " + calc.HasMemory);

            // 扩展方法演示
            string s = "  Hello World  ";
            string empty = "";
            Console.WriteLine("  \"\" IsNullOrEmpty: " + empty.IsNullOrEmpty());
            Console.WriteLine("  OrDefault: " + empty.OrDefault("默认值"));
            Console.WriteLine("  WordCount: " + s.Trim().WordCount());

            Console.WriteLine();
            Console.WriteLine("  C# 6.0 表达式主体支持: 方法、只读属性、运算符");
            Console.WriteLine("  C# 7.0 进一步扩展到: 构造函数、析构函数、get/set");
        }
    }

    // 扩展方法必须在顶级静态类中（不能是嵌套类）
    static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string s) 
            => string.IsNullOrEmpty(s);
        public static string OrDefault(this string s, string def)
            => string.IsNullOrEmpty(s) ? def : s;
        public static int WordCount(this string s)
            => string.IsNullOrEmpty(s) ? 0 : s.Trim().Split(' ').Length;
    }
}
