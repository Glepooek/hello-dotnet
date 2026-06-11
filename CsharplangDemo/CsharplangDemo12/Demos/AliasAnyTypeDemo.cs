// 任意类型别名必须在文件顶部（全局 using 或文件顶部 using）
using Point2D    = (double X, double Y);
using Point3D    = (double X, double Y, double Z);
using Matrix2x2  = int[][];
using StringMap  = System.Collections.Generic.Dictionary<string, string>;
using IntPair    = (int First, int Second);
// 指针类型别名 (unsafe 场景, 仅声明, 不运行时使用)
// using IntPtr = int*;  // 需要 unsafe 上下文

namespace CsharplangDemo12.Demos;

public static class AliasAnyTypeDemo
{
    // ── C# 12 前: using 只能别名命名类型 ─────────────────────────────
    // using MyList = System.Collections.Generic.List<int>;  // ✅ 命名类型
    // using Point = (double, double);                       // ❌ 元组不支持

    // ── C# 12 起: 任意类型均可别名 ───────────────────────────────────

    static double Distance(Point2D a, Point2D b) =>
        Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));

    static double Magnitude(Point3D p) =>
        Math.Sqrt(p.X * p.X + p.Y * p.Y + p.Z * p.Z);

    static int MatrixTrace(Matrix2x2 m) =>
        m[0][0] + m[1][1];   // 对角线之和

    public static void Run()
    {
        // 元组类型别名: 具名字段让代码更可读
        Point2D origin = (0.0, 0.0);
        Point2D point  = (3.0, 4.0);
        Console.WriteLine($"  Point2D: {point} (命名字段: X={point.X}, Y={point.Y})");
        Console.WriteLine($"  Distance: {Distance(origin, point):F2}");

        // 3D 点
        Point3D p3 = (1.0, 2.0, 2.0);
        Console.WriteLine($"  Point3D Magnitude: {Magnitude(p3):F2}");

        // 数组类型别名
        Matrix2x2 mat = [[1, 2], [3, 4]];
        Console.WriteLine($"  Matrix2x2 Trace: {MatrixTrace(mat)}");

        // 泛型类型别名
        StringMap config = new()
        {
            ["host"] = "localhost",
            ["port"] = "8080",
            ["env"]  = "development"
        };
        Console.WriteLine($"  StringMap: host={config["host"]}, port={config["port"]}");

        // 元组解构仍然有效
        IntPair pair = (42, 99);
        var (first, second) = pair;
        Console.WriteLine($"  IntPair 解构: first={first}, second={second}");

        Console.WriteLine();
        Console.WriteLine("  C# 12 前: using 别名不支持元组/数组/指针类型");
        Console.WriteLine("  C# 12 起: 任意类型均可创建语义别名，增强可读性");
    }
}
