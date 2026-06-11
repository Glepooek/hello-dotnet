namespace CsharplangDemo11.Demos;

// ── required 成员: 编译器强制要求对象初始化器设置该属性 ───────────────
public class Order
{
    // 必须在对象初始化器中赋值，否则编译错误
    public required string OrderId  { get; init; }
    public required string Customer { get; init; }
    public required decimal Amount  { get; init; }

    // 可选成员: 不需要强制赋值
    public string? Note    { get; init; }
    public bool    IsUrgent { get; init; }
}

// ── 与构造函数配合: SetsRequiredMembers 特性跳过编译器检查 ────────────
public class Product
{
    public required string Name  { get; set; }
    public required decimal Price { get; set; }
    public string Category { get; set; } = "未分类";

    // 无参构造函数 + SetsRequiredMembers: 告知编译器该构造函数会设置所有 required 成员
    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public Product(string name, decimal price)
    {
        Name  = name;
        Price = price;
    }

    // 无参构造函数不带 SetsRequiredMembers: 使用时仍需对象初始化器设置 required 成员
    public Product() { }
}

public static class RequiredMemberDemo
{
    public static void Run()
    {
        // ✅ 正确: 所有 required 成员都在初始化器中赋值
        var order = new Order
        {
            OrderId  = "ORD-001",
            Customer = "Alice",
            Amount   = 299.99m,
            Note     = "优先配送"  // 可选
        };
        Console.WriteLine($"  Order: {order.OrderId}, {order.Customer}, ¥{order.Amount}");

        // 编译错误示例 (已注释): 缺少 required 成员
        // var bad = new Order { OrderId = "X" };   // ❌ 缺 Customer 和 Amount

        // 通过带 SetsRequiredMembers 的构造函数创建
        var p1 = new Product("MacBook", 12999m);  // 构造函数已设置 required 成员
        Console.WriteLine($"  Product(ctor): {p1.Name}, ¥{p1.Price}");

        // 通过无参构造函数 + 对象初始化器
        var p2 = new Product
        {
            Name  = "iPad",
            Price = 4999m,
            Category = "平板"
        };
        Console.WriteLine($"  Product(init): {p2.Name}, {p2.Category}, ¥{p2.Price}");

        Console.WriteLine();
        Console.WriteLine("  required vs 构造函数参数:");
        Console.WriteLine("    构造函数参数: 强制赋值, 但影响对象初始化器语法");
        Console.WriteLine("    required:    强制赋值 + 保留对象初始化器灵活性");
    }
}
