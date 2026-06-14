// C# 6.0: using static — 静态类的成员可以直接调用，无需类名前缀
using System;
using static System.Math;          // 导入 Math 的所有静态成员
using static System.Console;       // 导入 Console 的所有静态成员
using static System.StringComparison;  // 导入枚举值

namespace CsharplangDemo60.Demos
{
    public static class StaticImportDemo
    {
        public static void Run()
        {
            // ── C# 6.0 前: 必须写完整类名 ───────────────────────────────
            double oldResult = System.Math.Sqrt(System.Math.Pow(3, 2) + System.Math.Pow(4, 2));
            Console.WriteLine("  旧写法: " + oldResult);

            // ── C# 6.0 起: using static Math 后直接调用 ──────────────────
            double hypotenuse = Sqrt(Pow(3, 2) + Pow(4, 2));
            WriteLine("  using static Math: 斜边 = " + hypotenuse);  // 连 Console. 也省了

            // Math 的其他方法直接可用
            double circle = PI * Pow(5, 2);
            WriteLine("  圆面积(r=5): " + circle.ToString("F2"));
            WriteLine("  Abs(-42) = " + Abs(-42));
            WriteLine("  Max(3,7) = " + Max(3, 7));
            WriteLine("  Floor(3.7) = " + Floor(3.7));// 向下取整
            WriteLine("  Ceiling(3.2) = " + Ceiling(3.2)); // 向上取整

            // using static enum 值: 直接写枚举成员名
            string a = "Hello", b = "hello";
            bool eq = string.Equals(a, b, OrdinalIgnoreCase);  // 直接用 OrdinalIgnoreCase
            WriteLine("  OrdinalIgnoreCase 比较: " + eq);

            WriteLine();
            WriteLine("  using static 典型场景:");
            WriteLine("    Math/Console 等频繁使用的工具类");
            WriteLine("    枚举类型的成员（如 StringComparison）");
            WriteLine("    自定义工具类（如 Guard.NotNull → NotNull）");
        }
    }
}
