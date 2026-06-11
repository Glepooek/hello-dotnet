namespace CsharplangDemo13.Demos;

// C# 13 预览特性: field 上下文关键字
// 在属性访问器中直接访问编译器合成的后备字段, 无需手动声明 private 字段
// 需要 LangVersion=preview

public class Temperature
{
    // 无需声明 private double _celsius;
    // `field` 在访问器内引用编译器自动生成的后备字段
    public double Celsius
    {
        get => field;
        set => field = value < -273.15 ? -273.15 : value;  // 不低于绝对零度
    }

    // 同样适用于只读属性: 在 init 中赋值
    public string Label
    {
        get => field;
        init => field = string.IsNullOrWhiteSpace(value) ? "Unknown" : value;
    }

    // 混合: get 自动, set 带逻辑
    public int Precision
    {
        get;
        set => field = value is >= 0 and <= 10 ? value : field;  // 范围外忽略赋值
    }
}

public static class FieldKeywordDemo
{
    public static void Run()
    {
        var t = new Temperature { Label = "  室温  ", Celsius = 25.0, Precision = 2 };

        Console.WriteLine($"  Label:     '{t.Label}'");
        Console.WriteLine($"  Celsius:   {t.Celsius}");
        Console.WriteLine($"  Precision: {t.Precision}");

        // 测试边界: 低于绝对零度被修正
        t.Celsius = -300;
        Console.WriteLine($"  低于绝对零度修正后: {t.Celsius}°C");

        // 测试 Precision: 超范围赋值被忽略
        t.Precision = 99;
        Console.WriteLine($"  超范围 Precision (应保持 2): {t.Precision}");

        Console.WriteLine();
        Console.WriteLine("  注: field 关键字在 C# 13 为预览, C# 14 正式稳定");
    }
}
