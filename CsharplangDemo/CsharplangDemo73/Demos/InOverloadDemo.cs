using System;

namespace CsharplangDemo73.Demos
{
    public static class InOverloadDemo
    {
        // ── C# 7.3: in 参数的重载解析改进 ────────────────────────────────
        // 当同时存在值传递和 in 引用重载时，编译器能更精确地选择

        // 值传递版本
        static string Print(int x)
        {
            return "  值传递: " + x;
        }

        // in 引用版本（只读引用，无复制开销）
        static string Print(in int x)
        {
            return "  in 引用: " + x;
        }

        // ── 大型结构体演示 in 的性能优势 ─────────────────────────────────
        struct BigMatrix
        {
            public double M00, M01, M02, M03;
            public double M10, M11, M12, M13;
            public double M20, M21, M22, M23;
            public double M30, M31, M32, M33;

            public BigMatrix(double diagonal)
            {
                M00 = M11 = M22 = M33 = diagonal;
                M01 = M02 = M03 = M10 = M12 = M13 = 0;
                M20 = M21 = M23 = M30 = M31 = M32 = 0;
            }

            public double Trace() => M00 + M11 + M22 + M33;
        }

        // 值传递: 复制 128 字节
        static double TraceByValue(BigMatrix m) => m.Trace();

        // in 传递: 只传引用（8 字节），零复制
        static double TraceByIn(in BigMatrix m) => m.Trace();

        // ── 演示 in 与 ref/out 的区别 ────────────────────────────────────
        static void ModifyRef(ref int x)   => x *= 2;       // 可读写
        static void ReadIn(in int x)       // 只读，不可修改
        {
            Console.WriteLine("  ReadIn: " + x);
            // x = 99;  // 编译错误: in 参数只读
        }

        public static void Run()
        {
            int val = 42;

            // C# 7.3 改进: 同时存在值传递和 in 重载时不再因歧义报错
            Console.WriteLine(Print(val));         // 选择值传递版本（省略 in 时默认值传递）
            Console.WriteLine(Print(in val));      // 显式选择 in 版本
            Console.WriteLine(Print(100));         // 选择值传递（字面量不能传 in）

            // ── 大型结构体对比 ────────────────────────────────────────
            var matrix = new BigMatrix(1.0);
            Console.WriteLine("  BigMatrix(128字节) Trace by value: " + TraceByValue(matrix));
            Console.WriteLine("  BigMatrix(128字节) Trace by in:    " + TraceByIn(in matrix));

            // ── ref / in / out 三者对比 ───────────────────────────────
            int a = 10;
            ModifyRef(ref a);
            Console.WriteLine("  ref 修改后: " + a);  // 20

            int b = 99;
            ReadIn(in b);  // 只读，不修改

            Console.WriteLine();
            Console.WriteLine("  in 参数三要点:");
            Console.WriteLine("    1. 只读引用（不可修改，不复制）");
            Console.WriteLine("    2. 调用方可省略 in（编译器推断），也可显式写 in val");
            Console.WriteLine("    3. 字面量/右值不能传给 in 参数（需具名变量）");
        }
    }
}
