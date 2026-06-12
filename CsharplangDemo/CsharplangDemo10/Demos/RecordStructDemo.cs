// C# 10: 文件范围命名空间 — 整个文件属于此命名空间，无需大括号
namespace CsharplangDemo10.Demos;

// ── C# 9: record class (引用类型, 堆分配) ─────────────────────────────
record class PersonClass(string Name, int Age);

// ── C# 10: record struct (值类型, 栈分配) ────────────────────────────
record struct Point(double X, double Y);

// readonly record struct: 所有属性只读, 禁止 set
readonly record struct Color(byte R, byte G, byte B)
{
    // 自定义方法
    public string Hex => $"#{R:X2}{G:X2}{B:X2}";
}

// record struct 也可手动声明属性 (非位置式)
record struct Range
{
    public double Min { get; init; }
    public double Max { get; init; }
    public double Span => Max - Min;
    public bool Contains(double v) => v >= Min && v <= Max;
}

public static class RecordStructDemo
{
    public static void Run()
    {
        // ── 值语义相等 ────────────────────────────────────────────────
        var p1 = new Point(1.0, 2.0);
        var p2 = new Point(1.0, 2.0);
        var p3 = new Point(3.0, 4.0);

        Console.WriteLine($"  p1 = {p1}");
        Console.WriteLine($"  p1 == p2 (同值): {p1 == p2}");   // true
        Console.WriteLine($"  p1 == p3 (异值): {p1 == p3}");   // false

        // ── with 表达式: 非破坏性修改 ────────────────────────────────
        var p4 = p1 with { X = 99.0 };
        Console.WriteLine($"  p1 with X=99: {p4}");
        Console.WriteLine($"  p1 不变:      {p1}");  // 原始值不变

        // ── readonly record struct ────────────────────────────────────
        var red = new Color(255, 0, 0);
        Console.WriteLine($"  Color: {red}, Hex={red.Hex}");
        // red.R = 100;  // 编译错误: readonly record struct 属性不可修改

        // ── 非位置式 record struct ────────────────────────────────────
        var r = new Range { Min = 0.0, Max = 100.0 };
        Console.WriteLine($"  Range: {r}, Span={r.Span}");
        Console.WriteLine($"  Contains(50): {r.Contains(50)}");

        // ── record struct vs record class 对比 ───────────────────────
        var pc1 = new PersonClass("Alice", 30);
        var pc2 = new PersonClass("Alice", 30);
        Console.WriteLine($"  record class ReferenceEquals: {ReferenceEquals(pc1, pc2)}"); // false
        Console.WriteLine($"  record class == (值相等):     {pc1 == pc2}");  // true
        Console.WriteLine();
        Console.WriteLine("  record struct: 值类型(栈), 适合小型数据如坐标/颜色/金额");
        Console.WriteLine("  record class:  引用类型(堆), 适合含有引用语义的数据");
    }
}
