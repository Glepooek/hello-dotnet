namespace CsharplangDemo12.Demos;

// ── C# 12 前: class 只有 record 才支持主构造函数 ─────────────────────
// 旧写法: 手动声明字段 + 构造函数赋值
public class OldService
{
    private readonly string _name;
    private readonly int _timeout;

    public OldService(string name, int timeout)
    {
        _name = name;
        _timeout = timeout;
    }

    public string Describe() => $"OldService({_name}, timeout={_timeout}s)";
}

// ── C# 12: class 主构造函数 ──────────────────────────────────────────
// 参数在整个类体范围内直接可用，编译器不自动生成公共属性（与 record 的区别）
public class HttpClientService(string baseUrl, int timeoutSeconds)
{
    // 直接用主构造函数参数初始化属性
    public string BaseUrl { get; } = baseUrl.TrimEnd('/');

    // 参数也可以在方法体中直接引用
    public string Describe() =>
        $"HttpClientService(url={BaseUrl}, timeout={timeoutSeconds}s)";

    // 显式声明的其他构造函数必须通过 this() 调用主构造函数
    public HttpClientService(string baseUrl) : this(baseUrl, 30) { }
}

// ── C# 12: struct 主构造函数 ─────────────────────────────────────────
public struct Rectangle(double width, double height)
{
    public double Width { get; } = width;
    public double Height { get; } = height;
    public double Area => Width * Height;
    public double Perimeter => 2 * (Width + Height);
}

// ── 依赖注入场景: 主构造函数最典型的用途 ─────────────────────────────
public interface ILogger
{
    void Log(string message);
}

public class ConsoleLogger : ILogger
{
    public void Log(string message) => Console.WriteLine($"  [LOG] {message}");
}

// DI 场景: 直接在主构造函数中注入，无需手动存字段
public class OrderProcessor(ILogger logger, string currency = "CNY")
{
    public void Process(string orderId)
    {
        logger.Log($"处理订单 {orderId}，货币: {currency}");
    }
}

public static class PrimaryConstructorDemo
{
    public static void Run()
    {
        // 旧写法 vs 新写法对比
        var old = new OldService("legacy", 60);
        Console.WriteLine($"  旧写法: {old.Describe()}");

        var svc = new HttpClientService("https://api.example.com", 15);
        Console.WriteLine($"  新写法: {svc.Describe()}");

        // 使用辅助构造函数（默认 timeout）
        var svc2 = new HttpClientService("https://api.example.com/v2");
        Console.WriteLine($"  辅助构造: {svc2.Describe()}");

        // struct 主构造函数
        var rect = new Rectangle(4.0, 3.0);
        Console.WriteLine($"  Rectangle: 面积={rect.Area}, 周长={rect.Perimeter}");

        // DI 场景
        var processor = new OrderProcessor(new ConsoleLogger());
        processor.Process("ORD-2024-001");
    }
}
