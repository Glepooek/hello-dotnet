using System;

namespace CsharplangDemo72.Demos
{
    // ── readonly struct: 整个结构体不可变 ────────────────────────────────
    // 所有实例方法/属性隐式为 readonly，编译器可跳过防御性复制
    readonly struct Vector2D
    {
        public double X { get; }
        public double Y { get; }

        public Vector2D(double x, double y) { X = x; Y = y; }

        // 所有方法隐式 readonly，无需显式标注
        public double Length => Math.Sqrt(X * X + Y * Y);
        public Vector2D Add(Vector2D other) => new Vector2D(X + other.X, Y + other.Y);
        public Vector2D Scale(double factor) => new Vector2D(X * factor, Y * factor);
        public double Dot(Vector2D other) => X * other.X + Y * other.Y;

        public override string ToString() => "(" + X.ToString("F2") + ", " + Y.ToString("F2") + ")";
    }

    // ── 对比: 普通 struct（非 readonly）─────────────────────────────────
    struct MutableVector2D
    {
        public double X;
        public double Y;
        public MutableVector2D(double x, double y) { X = x; Y = y; }
        // 普通方法: 编译器对 in 传入的实例会做防御性复制
        public double Length() => Math.Sqrt(X * X + Y * Y);
    }

    public static class ReadonlyStructDemo
    {
        // readonly struct 通过 in 传递: 零防御性复制
        static double GetLength(in Vector2D v) => v.Length;

        // 普通 struct 通过 in 传递: 编译器会做防御性复制（因为方法可能修改状态）
        static double GetMutableLength(in MutableVector2D v) => v.Length();

        public static void Run()
        {
            var v1 = new Vector2D(3, 4);
            var v2 = new Vector2D(1, 2);

            Console.WriteLine("  readonly struct Vector2D:");
            Console.WriteLine("    v1 = " + v1 + ", Length = " + v1.Length.ToString("F2"));
            Console.WriteLine("    v2 = " + v2);
            Console.WriteLine("    v1 + v2 = " + v1.Add(v2));
            Console.WriteLine("    v1 * 2 = " + v1.Scale(2));
            Console.WriteLine("    v1 · v2 = " + v1.Dot(v2).ToString("F2"));

            // 通过 in 传递 readonly struct: 无防御性复制
            Console.WriteLine("    GetLength(in v1) = " + GetLength(in v1).ToString("F2"));

            // readonly struct 的字段/属性不可修改
            // v1.X = 99;  // 编译错误: readonly struct 字段只读

            Console.WriteLine();
            Console.WriteLine("  readonly struct 的优势:");
            Console.WriteLine("    • 所有方法隐式 readonly，无需逐个标注");
            Console.WriteLine("    • 通过 in 参数传递时，编译器无需防御性复制");
            Console.WriteLine("    • 是 Span<T>/Vector<T> 等高性能类型的设计基础");
        }
    }
}
