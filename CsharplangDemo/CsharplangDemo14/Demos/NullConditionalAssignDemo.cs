namespace CsharplangDemo14.Demos;

public class Customer
{
    public string Name { get; set; } = string.Empty;
    public Order? CurrentOrder { get; set; }
    public decimal TotalAmount { get; set; }
    public List<string> Tags { get; set; } = [];

    public override string ToString() =>
        $"Customer({Name}, Order={CurrentOrder?.Id ?? "null"}, Total={TotalAmount:F2})";
}

public class Order
{
    public string Id { get; init; } = string.Empty;
    public decimal Amount { get; set; }
    public string? Note { get; set; }
}

public static class NullConditionalAssignDemo
{
    public static void Run()
    {
        // ── 场景 1: 基本空条件赋值 ───────────────────────────────────
        Customer? customer = new Customer { Name = "Alice", TotalAmount = 100m };
        Customer? nullCustomer = null;

        // C# 14 前: 需要 null 检查
        if (customer is not null)
            customer.CurrentOrder = new Order { Id = "ORD-001", Amount = 50m };

        // C# 14 起: 空条件赋值
        customer?.CurrentOrder = new Order { Id = "ORD-002", Amount = 88m };
        Console.WriteLine($"  customer?.Order  = {customer}");

        // null 时赋值被跳过, 右侧表达式不求值
        nullCustomer?.CurrentOrder = new Order { Id = "ORD-NEVER" };
        Console.WriteLine($"  null?.Order      = (跳过, nullCustomer 仍为 null)");

        // ── 场景 2: 复合赋值 ─────────────────────────────────────────
        customer?.TotalAmount += 200m;   // customer 非 null, TotalAmount += 200
        Console.WriteLine($"  customer?.TotalAmount += 200 → {customer?.TotalAmount}");

        nullCustomer?.TotalAmount += 999m;  // null, 跳过
        Console.WriteLine($"  null?.TotalAmount += 999    → (跳过)");

        // ── 场景 3: 索引器空条件赋值 ─────────────────────────────────
        List<string>? tags = ["csharp", "dotnet"];
        List<string>? nullTags = null;

        tags?[0] = "C#14";             // 非 null, 赋值生效
        nullTags?[0] = "never";        // null, 跳过

        Console.WriteLine($"  tags?[0] = \"C#14\" → {tags?[0]}");
        Console.WriteLine($"  nullTags?[0]        → (跳过, nullTags 仍为 null: {nullTags is null})");

        // ── 场景 4: 链式成员访问 ─────────────────────────────────────
        customer?.CurrentOrder?.Note = "优先处理";
        Console.WriteLine($"  customer?.Order?.Note = {customer?.CurrentOrder?.Note}");

        // ── 注意: ++ / -- 不允许用于空条件赋值 ──────────────────────
        // customer?.TotalAmount++;  // 编译错误: ++ 和 -- 不支持空条件
        Console.WriteLine();
        Console.WriteLine("  注: ++ 和 -- 不允许与 ?. 组合使用");
    }
}
