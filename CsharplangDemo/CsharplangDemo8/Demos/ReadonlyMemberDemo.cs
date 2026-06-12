using System;

namespace CsharplangDemo8.Demos
{
    // ── readonly 成员: struct 方法/属性标记为 readonly ─────────────────────
    // 作用: 告知编译器此成员不会修改 struct 状态
    // 好处: 1. 编译器可避免防御性复制优化  2. 明确文档意图

    public struct Rectangle
    {
        public double Width;
        public double Height;

        public Rectangle(double w, double h) { Width = w; Height = h; }

        // readonly 属性: 不修改字段
        public readonly double Area      => Width * Height;
        public readonly double Perimeter => 2 * (Width + Height);
        public readonly double Diagonal  => Math.Sqrt(Width * Width + Height * Height);

        // readonly 方法: 不修改字段
        public readonly bool IsSquare() => Math.Abs(Width - Height) < 1e-10;
        public readonly Rectangle Scale(double factor)
            => new Rectangle(Width * factor, Height * factor);  // 返回新值，不修改自身

        // readonly override: ToString 不应修改状态
        public readonly override string ToString()
            => $"Rect({Width:F1}×{Height:F1}) Area={Area:F2}";

        // 非 readonly 方法: 可以修改字段
        public void Swap() { double tmp = Width; Width = Height; Height = tmp; }
    }

    // ── readonly 与 in 参数配合 ───────────────────────────────────────────
    public struct Vector3
    {
        public double X, Y, Z;
        public Vector3(double x, double y, double z) { X = x; Y = y; Z = z; }

        // readonly: 编译器知道 in 参数调用此方法不需要防御性复制
        public readonly double Length => Math.Sqrt(X*X + Y*Y + Z*Z);
        public readonly Vector3 Normalize()
        {
            double len = Length;
            return new Vector3(X / len, Y / len, Z / len);
        }
        public readonly override string ToString() => $"({X:F2},{Y:F2},{Z:F2})";
    }

    // 接受 in Vector3: 保证不复制，readonly 方法确保不修改
    public static class ReadonlyMemberDemo
    {
        static double DotProduct(in Vector3 a, in Vector3 b)
            => a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        public static void Run()
        {
            var rect = new Rectangle(4.0, 3.0);
            Console.WriteLine($"  {rect}");
            Console.WriteLine($"  Perimeter: {rect.Perimeter:F2}");
            Console.WriteLine($"  Diagonal:  {rect.Diagonal:F2}");
            Console.WriteLine($"  IsSquare:  {rect.IsSquare()}");

            var scaled = rect.Scale(2.0);
            Console.WriteLine($"  Scale×2:   {scaled}");

            rect.Swap();
            Console.WriteLine($"  Swap后:    {rect}");  // Width 和 Height 互换

            // Vector3 与 in 参数
            var v1 = new Vector3(1, 2, 2);
            var v2 = new Vector3(3, 0, 4);
            Console.WriteLine($"  v1 = {v1}, Length = {v1.Length:F2}");
            Console.WriteLine($"  v1 归一化: {v1.Normalize()}");
            Console.WriteLine($"  v1·v2 点积: {DotProduct(in v1, in v2):F2}");

            Console.WriteLine();
            Console.WriteLine("  readonly 成员的价值:");
            Console.WriteLine("    • 防止意外修改 struct 状态");
            Console.WriteLine("    • 与 in 参数配合，编译器跳过防御性复制");
            Console.WriteLine("    • 明确文档意图：调用方可放心传 in 引用");
        }
    }
}
