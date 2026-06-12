namespace CsharplangDemo10.Demos;

// ── sealed override ToString in record ───────────────────────────────
// C# 10: 在 sealed 类或 record 中重写 ToString 可加 sealed 修饰符
// 防止派生类更改格式，保证输出一致性

public record Shape(string Name)
{
    // 加 sealed: 派生记录无法再重写 ToString
    public sealed override string ToString() => $"[Shape: {Name}]";
}

public record Circle(double Radius) : Shape("Circle")
{
    // ToString() 被 sealed, 无法重写
    // public override string ToString() => ...; // 编译错误
    public double Area => Math.PI * Radius * Radius;
}

// sealed class 中的 sealed override ToString
public sealed class Configuration
{
    public string Key   { get; init; } = string.Empty;
    public string Value { get; init; } = string.Empty;

    // sealed class 本身已不可继承, sealed override 是防御性写法
    public sealed override string ToString() => $"Config({Key}={Value})";
}

public static class SealedToStringDemo
{
    public static void Run()
    {
        var shape  = new Shape("Generic");
        var circle = new Circle(5.0);
        var cfg    = new Configuration { Key = "timeout", Value = "30s" };

        Console.WriteLine($"  Shape:   {shape}");
        Console.WriteLine($"  Circle:  {circle}");           // 使用 Shape 的 sealed ToString
        Console.WriteLine($"  Config:  {cfg}");

        // 验证 Circle 使用的是 Shape 的 ToString (sealed 防止重写)
        Console.WriteLine($"  Circle.Area: {circle.Area:F2}");
        Console.WriteLine();
        Console.WriteLine("  sealed override ToString 用途:");
        Console.WriteLine("    • 保证序列化/日志格式在整个继承体系中一致");
        Console.WriteLine("    • 防止派生类意外破坏父类的字符串格式约定");
    }
}
