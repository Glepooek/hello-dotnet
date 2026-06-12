using System;

namespace CsharplangDemo72.Demos
{
    // ── in 参数: 按只读引用传递，不复制，不修改 ─────────────────────────
    // 对于大型 struct 有显著性能优势

    // 较大的结构体（32字节），演示 in 的性能价值
    struct Matrix2x2
    {
        public double M00, M01, M10, M11;

        public Matrix2x2(double m00, double m01, double m10, double m11)
        { M00 = m00; M01 = m01; M10 = m10; M11 = m11; }

        public double Determinant() => M00 * M11 - M01 * M10;
        public double Trace()       => M00 + M11;

        public override string ToString()
            => "[" + M00 + " " + M01 + "; " + M10 + " " + M11 + "]";
    }

    public static class InParameterDemo
    {
        // 值传递: 复制整个 Matrix2x2（32字节）
        static double DetByValue(Matrix2x2 m) => m.Determinant();

        // in 传递: 只传指针（8字节），零复制
        static double DetByIn(in Matrix2x2 m) => m.Determinant();
        static double TraceByIn(in Matrix2x2 m) => m.Trace();

        // in 参数: 不能修改
        static void ReadMatrix(in Matrix2x2 m)
        {
            Console.WriteLine("  读取矩阵: " + m.ToString());
            // m.M00 = 99;  // 编译错误: in 参数只读
        }

        // ── in 参数与 ref/out 对比 ────────────────────────────────────
        static void ModifyRef(ref int x) => x *= 2;       // 可读写
        static void ReadIn(in int x)
            => Console.WriteLine("  in int = " + x);     // 只读
        static void WriteOut(out int x) { x = 100; }     // 只写（必须赋值）

        // ── 配合 readonly struct 使用效果最佳 ────────────────────────
        readonly struct Point3D
        {
            public double X { get; }
            public double Y { get; }
            public double Z { get; }
            public Point3D(double x, double y, double z) { X = x; Y = y; Z = z; }
            public double Length => Math.Sqrt(X*X + Y*Y + Z*Z);
        }

        static double Distance(in Point3D a, in Point3D b)
        {
            double dx = a.X - b.X, dy = a.Y - b.Y, dz = a.Z - b.Z;
            return Math.Sqrt(dx*dx + dy*dy + dz*dz);
        }

        public static void Run()
        {
            var mat = new Matrix2x2(1, 2, 3, 4);

            // 值传递 vs in 传递（结果相同，性能不同）
            Console.WriteLine("  DetByValue: " + DetByValue(mat));
            Console.WriteLine("  DetByIn:    " + DetByIn(in mat));   // 显式 in
            Console.WriteLine("  DetByIn:    " + DetByIn(mat));      // 省略 in 也合法

            ReadMatrix(in mat);
            Console.WriteLine("  TraceByIn:  " + TraceByIn(mat));

            // ref / in / out 三者对比
            Console.WriteLine("  ref/in/out 对比:");
            int val = 10;
            ModifyRef(ref val);
            Console.WriteLine("  ref 修改后: " + val);  // 20

            ReadIn(in val);

            int outVal;
            WriteOut(out outVal);
            Console.WriteLine("  out 输出:   " + outVal);

            // 配合 readonly struct
            var p1 = new Point3D(0, 0, 0);
            var p2 = new Point3D(3, 4, 0);
            Console.WriteLine("  Distance(p1, p2) = " + Distance(in p1, in p2).ToString("F2"));

            Console.WriteLine();
            Console.WriteLine("  in 参数的适用场景:");
            Console.WriteLine("    • 大型 struct（>= 16 字节）按引用传递避免拷贝");
            Console.WriteLine("    • 与 readonly struct 配合效果最佳");
            Console.WriteLine("    • 调用方可省略 in 关键字（编译器自动推断）");
        }
    }
}
