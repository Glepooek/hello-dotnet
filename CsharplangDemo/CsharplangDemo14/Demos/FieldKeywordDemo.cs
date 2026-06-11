namespace CsharplangDemo14.Demos;

// C# 13: field 是预览特性, 需要 LangVersion=preview
// C# 14: field 正式稳定, net10.0 默认支持, 无需额外配置

public class Product
{
    // ── 模式 1: 只有 set 有逻辑, get 自动 ──────────────────
    // 无需声明 private string _name;
    public string Name
    {
        get;
        set => field = value?.Trim() ?? throw new ArgumentNullException(nameof(value));
    }

    // ── 模式 2: 两个访问器都有逻辑 ─────────────────────────
    private const decimal MaxPrice = 99_999m;
    public decimal Price
    {
        get => field;
        set => field = value is < 0 or > MaxPrice
            ? throw new ArgumentOutOfRangeException(nameof(value), $"价格须在 0~{MaxPrice}")
            : value;
    }

    // ── 模式 3: init 访问器 ─────────────────────────────────
    public string Category
    {
        get;
        init => field = string.IsNullOrWhiteSpace(value) ? "未分类" : value;
    }

    // ── 模式 4: 惰性初始化 ─────────────────────────────────
    public string DisplayName
    {
        get => field ??= $"[{Category}] {Name} ¥{Price:F2}";
    }

    public Product(string name, decimal price, string category = "")
    {
        Name = name;
        Price = price;
        Category = category;
    }
}

public static class FieldKeywordDemo
{
    public static void Run()
    {
        var p = new Product("  苹果 MacBook Pro  ", 15999m, "  电子产品  ");

        Console.WriteLine($"  Name (已 Trim):    '{p.Name}'");
        Console.WriteLine($"  Price:             {p.Price:F2}");
        Console.WriteLine($"  Category (已清理): '{p.Category}'");
        Console.WriteLine($"  DisplayName (惰性): '{p.DisplayName}'");
        Console.WriteLine($"  DisplayName (缓存): '{p.DisplayName}'");  // 直接返回缓存

        // 测试边界验证
        try
        {
            p.Price = -100;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"  Price=-100 抛出: {ex.ParamName}");
        }

        // C# 14 vs C# 13 对比
        Console.WriteLine();
        Console.WriteLine("  C# 13: field 需要 <LangVersion>preview</LangVersion>");
        Console.WriteLine("  C# 14: field 正式稳定, net10.0 默认可用");
    }
}
