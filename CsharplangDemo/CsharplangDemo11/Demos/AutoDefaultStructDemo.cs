namespace CsharplangDemo11.Demos;

// ── C# 11 前: struct 构造函数必须显式初始化所有字段 ───────────────────
public struct OldPoint
{
    public int X;
    public int Y;
    public string? Label;

    // C# 11 前若不写下面这行会报错: 字段 Label 未完全赋值
    public OldPoint(int x) { X = x; Y = 0; Label = null; }  // 必须全写
}

// ── C# 11 起: 自动默认结构 — 编译器自动填充未初始化字段 ───────────────
public struct Point3D
{
    public double X;
    public double Y;
    public double Z;
    public string? Label;
    public bool IsOrigin;

    // 只写需要的, 其余字段自动默认 (0 / null / false)
    public Point3D(double x, double y) { X = x; Y = y; }          // Z=0, Label=null, IsOrigin=false
    public Point3D(double x)           { X = x; }                  // Y=Z=0, Label=null
    public Point3D(string label)       { Label = label; }          // X=Y=Z=0

    public override string ToString() =>
        $"({X}, {Y}, {Z}) Label={Label ?? "null"} IsOrigin={IsOrigin}";
}

// ── 与 required 成员的交互 ────────────────────────────────────────────
public struct NamedValue
{
    public required string Name;   // required: 对象初始化器必须赋值
    public double Value;           // 自动默认为 0.0

    public override string ToString() => $"{Name}={Value}";
}

public static class AutoDefaultStructDemo
{
    public static void Run()
    {
        // 旧 struct 需要手动写所有字段
        var old = new OldPoint(5);
        Console.WriteLine($"  OldPoint: X={old.X}, Y={old.Y}, Label={old.Label ?? "null"}");

        // 新 struct: 只写感兴趣的字段
        var p1 = new Point3D(1.0, 2.0);
        var p2 = new Point3D(3.0);
        var p3 = new Point3D("原点") { IsOrigin = true };

        Console.WriteLine($"  Point3D(x,y): {p1}");
        Console.WriteLine($"  Point3D(x):   {p2}");
        Console.WriteLine($"  Point3D(lbl): {p3}");

        // required + 自动默认: 强制赋 Name, Value 自动为 0
        var nv = new NamedValue { Name = "温度" };
        Console.WriteLine($"  NamedValue:   {nv}");

        // default struct: 所有字段自动为默认值
        var def = default(Point3D);
        Console.WriteLine($"  default:      {def}");

        Console.WriteLine();
        Console.WriteLine("  C# 11 前: struct 构造函数必须显式赋值所有字段");
        Console.WriteLine("  C# 11 起: 未赋值字段自动填充为类型默认值，与 class 行为统一");
    }
}
