namespace CsharplangDemo14.Demos;

// ── partial 构造函数 ─────────────────────────────────────────────────
public partial class OrderService
{
    // 声明声明: 只有签名
    public partial OrderService(string connectionString);

    public string ConnectionString { get; private set; } = string.Empty;
    public bool IsInitialized { get; private set; }
}

public partial class OrderService
{
    // 实现声明: 包含主体, 只有实现方可写 : base() / : this()
    public partial OrderService(string connectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);
        ConnectionString = connectionString;
        IsInitialized = true;
        Console.WriteLine($"  OrderService 初始化, 连接串: {connectionString}");
    }
}

// ── partial 事件 ─────────────────────────────────────────────────────
public partial class EventBus
{
    // 声明声明: 类字段事件形式
    public partial event EventHandler<string>? MessageReceived;
}

public partial class EventBus
{
    // 存储订阅者列表
    private EventHandler<string>? _messageReceived;

    // 实现声明: 必须包含 add/remove 访问器
    public partial event EventHandler<string>? MessageReceived
    {
        add
        {
            Console.WriteLine($"  订阅者已注册: {value?.Method.Name}");
            _messageReceived += value;
        }
        remove
        {
            Console.WriteLine($"  订阅者已注销: {value?.Method.Name}");
            _messageReceived -= value;
        }
    }

    // Publish uses the backing field directly — required when event has custom add/remove
    public void Publish(string message) => _messageReceived?.Invoke(this, message);
}

public static class PartialCtorEventDemo
{
    public static void Run()
    {
        // partial 构造函数
        var svc = new OrderService("Server=localhost;Database=Orders");
        Console.WriteLine($"  IsInitialized: {svc.IsInitialized}");

        try
        {
            var bad = new OrderService("  ");  // 空白连接串抛出异常
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"  空白连接串抛出: {ex.GetType().Name}");
        }

        Console.WriteLine();

        // partial 事件
        var bus = new EventBus();
        bus.MessageReceived += OnMessage;          // 触发 add 访问器
        bus.Publish("Hello from EventBus!");
        bus.MessageReceived -= OnMessage;          // 触发 remove 访问器

        Console.WriteLine();
        Console.WriteLine("  典型用途: 源生成器提供实现声明, 用户只写声明声明");
    }

    static void OnMessage(object? sender, string msg) =>
        Console.WriteLine($"  收到消息: {msg}");
}
