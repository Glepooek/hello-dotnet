using System;
using System.Collections.Generic;

namespace CsharplangDemo60.Demos
{
    public static class GetterOnlyDefaultDemo
    {
        // ── C# 6.0: 仅限 getter 属性的默认值 ─────────────────────────────
        // C# 6.0 前: 只读属性（只有 get）必须通过构造函数赋值
        // C# 6.0 起: 只读自动属性可以在声明处设置初始值
        //            只能在声明时或构造函数中赋值，之后真正只读

        class AppSettings
        {
            // C# 6.0: 只读自动属性 + 默认值（只有 getter，无 setter）
            public string AppName    { get; } = "MyApp";
            public string Version    { get; } = "1.0.0";
            public int    MaxRetries { get; } = 3;
            public bool   Debug      { get; } = false;

            // 集合类型的只读属性
            public IReadOnlyList<string> AllowedHosts { get; }
                = new List<string> { "localhost", "127.0.0.1" };

            // 构造函数可以覆盖默认值
            public AppSettings() { }
            public AppSettings(string name, string version)
            {
                AppName = name;
                Version = version;
            }

            // 注意: 构造函数之外不能修改
            // public void Change() { AppName = "X"; }  // 编译错误
        }

        // 不可变数据对象: 所有属性只读，通过构造函数初始化
        class Point
        {
            public double X { get; }
            public double Y { get; }
            public double Z { get; }

            public Point(double x, double y, double z)
            {
                X = x; Y = y; Z = z;
            }

            // 计算属性（纯 getter，无后备字段）
            public double Length => Math.Sqrt(X * X + Y * Y + Z * Z);

            // 创建修改版本（返回新实例，保持不可变）
            public Point WithX(double x) => new Point(x, Y, Z);
            public Point WithY(double y) => new Point(X, y, Z);

            public override string ToString()
                => $"({X:F2}, {Y:F2}, {Z:F2})";
        }

        public static void Run()
        {
            // 默认值
            var settings1 = new AppSettings();
            Console.WriteLine("  默认设置:");
            Console.WriteLine("    AppName=" + settings1.AppName + " v" + settings1.Version);
            Console.WriteLine("    MaxRetries=" + settings1.MaxRetries + " Debug=" + settings1.Debug);
            Console.WriteLine("    Hosts=[" + string.Join(", ", settings1.AllowedHosts) + "]");

            // 构造函数覆盖
            var settings2 = new AppSettings("ProductionApp", "2.1.0");
            Console.WriteLine("  自定义设置: " + settings2.AppName + " v" + settings2.Version);

            // 不可变 Point
            var p1 = new Point(1, 2, 3);
            Console.WriteLine("  p1 = " + p1 + " Length=" + p1.Length.ToString("F2"));

            var p2 = p1.WithX(99);  // 创建新实例，p1 不变
            Console.WriteLine("  p2(WithX=99) = " + p2);
            Console.WriteLine("  p1 不变: " + p1);

            Console.WriteLine();
            Console.WriteLine("  只读属性默认值 vs 其他方式:");
            Console.WriteLine("    只读属性+默认值: { get; } = value  — 声明时设定，构造后不可改");
            Console.WriteLine("    自动属性+默认值: { get; set; } = value — 可以在任何地方修改");
            Console.WriteLine("    readonly 字段:   private readonly T _f = v — 更底层，不是属性");
        }
    }
}
