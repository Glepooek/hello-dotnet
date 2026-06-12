using System;

namespace CsharplangDemo9.Demos
{
    public static class StaticAnonymousDemo
    {
        public static void Run()
        {
            int factor = 3;

            // ── 普通 Lambda: 可以捕获外部变量（产生闭包对象分配）─────────
            Func<int, int> capture = x => x * factor;

            // ── static Lambda 不能捕获外部变量 ──────────────────────────
            Func<int, int>    square = static x => x * x;
            Func<int, int>    negate = static x => -x;
            Func<double, double> sqrt = static x => Math.Sqrt(x);

            Console.WriteLine($"  capture(5) = {capture(5)}");
            Console.WriteLine($"  square(5)  = {square(5)}");
            Console.WriteLine($"  negate(5)  = {negate(5)}");
            Console.WriteLine($"  sqrt(16)   = {sqrt(16)}");

            // ── static 匿名方法 ────────────────────────────────────────────
            Func<int, int, int> add = static delegate(int a, int b) { return a + b; };
            Console.WriteLine($"  static delegate(3,4) = {add(3, 4)}");

            // ── 编译错误演示 (注释掉的代码) ─────────────────────────────
            // static Func<int,int> bad = static x => x * factor;  // ❌

            Console.WriteLine();
            Console.WriteLine("  static Lambda/匿名方法 的价值:");
            Console.WriteLine("    • 明确表达意图: 此处不依赖任何外部状态");
            Console.WriteLine("    • 防止意外捕获外部变量导致的意外闭包分配");
            Console.WriteLine("    • 高频调用路径的小优化（避免闭包对象）");
        }
    }
}
