namespace CsharplangDemo10.Demos;

record Address(string City, string Country, string? ZipCode = null);
record Person(string Name, int Age, Address Address);
record Order(string Id, Person Customer, Address ShippingAddress);

public static class ExtendedPropertyPatternDemo
{
    public static void Run()
    {
        var alice = new Person("Alice", 28,
            new Address("Beijing", "China", "100000"));
        var order = new Order("ORD-001", alice,
            new Address("Shanghai", "China", "200000"));

        // ── C# 10 前: 嵌套属性模式需多层 {} ─────────────────────────
        bool oldWay1 = alice is { Address: { Country: "China" } };
        bool oldWay2 = order is { Customer: { Address: { Country: "China" } } };

        // ── C# 10 起: 扩展属性模式, 用 . 直接访问嵌套属性 ───────────
        bool newWay1 = alice is { Address.Country: "China" };
        bool newWay2 = order is { Customer.Address.Country: "China" };

        Console.WriteLine($"  旧写法 alice.Address.Country=China: {oldWay1}");
        Console.WriteLine($"  新写法 alice.Address.Country=China: {newWay1}");
        Console.WriteLine($"  三层嵌套 order.Customer.Address.Country: {newWay2}");

        // ── switch 表达式中应用 ──────────────────────────────────────
        static string ClassifyOrder(Order o) => o switch
        {
            { Customer.Address.Country: "China",
              ShippingAddress.City: "Beijing" }  => "北京本地订单",
            { Customer.Address.Country: "China",
              ShippingAddress.City: "Shanghai" } => "上海订单",
            { Customer.Address.Country: "China" } => "中国其他城市",
            _                                      => "海外订单"
        };

        Console.WriteLine($"  订单分类: {ClassifyOrder(order)}");

        // ── 与 null 检查结合 ─────────────────────────────────────────
        // 扩展属性模式在遇到 null 时安全短路
        var personNoZip = alice with
        {
            Address = new Address("Guangzhou", "China")
        };
        bool hasZip = personNoZip is { Address.ZipCode: not null };
        Console.WriteLine($"  Address.ZipCode not null: {hasZip}");  // false

        // ── 与捕获变量结合 ───────────────────────────────────────────
        if (alice is { Address.City: var city, Name: var name })
            Console.WriteLine($"  捕获变量: {name} 在 {city}");
    }
}
