namespace CsharplangDemo10.Demos;

// ── C# 10 前: struct 不能有字段初始化器, 不能有无参构造函数 ───────────
public struct OldMeasurement
{
    public double Value;
    public string Unit;
    // 无法写 = "m"; 无法写无参构造函数
    public OldMeasurement(double v, string u) { Value = v; Unit = u; }
}

// ── C# 10 起: struct 支持字段初始化器 + 无参构造函数 ─────────────────
public struct Measurement
{
    // 字段/属性初始化器
    public double Value = 0.0;
    public string Unit = "m";
    public bool IsValid = true;

    // 无参构造函数 (C# 10 新增)
    public Measurement()
    {
        Value = 0.0;
        Unit  = "m";
        IsValid = true;
    }

    public Measurement(double value, string unit)
    {
        Value = value;
        Unit  = unit;
    }

    public override string ToString() => $"{Value} {Unit} (valid={IsValid})";
}

// with 表达式也适用于普通 struct (C# 10)
public struct Temperature
{
    public double Celsius  { get; init; }
    public string Label    { get; init; } = "温度";

    public Temperature() { Celsius = 0.0; Label = "温度"; }
    public Temperature(double c) { Celsius = c; }

    public double Fahrenheit => Celsius * 9.0 / 5.0 + 32;
    public override string ToString() => $"{Label}: {Celsius}°C / {Fahrenheit:F1}°F";
}

public static class StructImprovementDemo
{
    public static void Run()
    {
        // 无参构造函数
        var m1 = new Measurement();
        Console.WriteLine($"  默认构造: {m1}");

        var m2 = new Measurement(36.5, "°C");
        Console.WriteLine($"  有参构造: {m2}");

        // default 与 new(): C# 10 中 default(Measurement) 不调用无参构造函数
        var def = default(Measurement);
        Console.WriteLine($"  default: Value={def.Value}, Unit='{def.Unit}' (初始化器不执行)");
        // 注意: default() 直接清零, 不经过无参构造函数 —— 这是 struct 的固有行为

        // with 表达式用于 struct
        var body = new Temperature(37.0) { Label = "体温" };
        var fever = body with { Celsius = 38.5 };
        Console.WriteLine($"  {body}");
        Console.WriteLine($"  {fever}");
        Console.WriteLine($"  body 不变: {body}");

        Console.WriteLine();
        Console.WriteLine("  C# 10 前: struct 字段必须在有参构造函数中赋值，不能有默认初始化器");
        Console.WriteLine("  C# 10 起: struct 与 class 行为更统一，字段可直接初始化");
    }
}
