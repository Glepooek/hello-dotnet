using System;
using System.IO;
using System.Text;

namespace CsharplangDemo8.Demos
{
    public static class UsingDeclarationDemo
    {
        // C# 8 前: using 语句需要大括号，多层嵌套时代码向右漂移
        static string OldWay()
        {
            using (var ms = new MemoryStream())
            {
                using (var writer = new StreamWriter(ms))
                {
                    writer.Write("hello");
                    writer.Flush();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }

        // C# 8 起: using 声明，变量在所在作用域结束时 Dispose
        static string NewWay()
        {
            using var ms = new MemoryStream();            // 方法结束时 Dispose
            using var writer = new StreamWriter(ms);      // 方法结束时 Dispose（先于 ms）

            writer.Write("hello");
            writer.Flush();
            return Encoding.UTF8.GetString(ms.ToArray());
        }

        // ── 作用域块内的 using 声明 ───────────────────────────────────
        static void ScopedUsing()
        {
            Console.WriteLine("  进入外部作用域");

            {
                using var inner = new TrackingDisposable("inner");
                Console.WriteLine("  使用 inner");
            }  // inner 在此 Dispose

            Console.WriteLine("  inner 已 Dispose，继续外部逻辑");

            using var outer = new TrackingDisposable("outer");
            Console.WriteLine("  使用 outer");
            // outer 在方法结束时 Dispose
        }

        // ── 多个 using 声明的 Dispose 顺序 ───────────────────────────
        static void MultipleUsings()
        {
            using var first  = new TrackingDisposable("first");
            using var second = new TrackingDisposable("second");
            using var third  = new TrackingDisposable("third");
            Console.WriteLine("  所有资源已就绪，Dispose 顺序与声明顺序相反:");
            // 方法结束时: third → second → first (LIFO 顺序)
        }

        public static void Run()
        {
            Console.WriteLine($"  OldWay: {OldWay()}");
            Console.WriteLine($"  NewWay: {NewWay()}");

            Console.WriteLine();
            Console.WriteLine("  作用域块控制 Dispose 时机:");
            ScopedUsing();

            Console.WriteLine();
            Console.WriteLine("  多个 using 声明 (LIFO Dispose):");
            MultipleUsings();
        }

        // 辅助类: 追踪 Dispose 调用
        class TrackingDisposable : IDisposable
        {
            private readonly string _name;
            public TrackingDisposable(string name)
            {
                _name = name;
                Console.WriteLine($"    创建: {_name}");
            }
            public void Dispose() => Console.WriteLine($"    Dispose: {_name}");
        }
    }
}
