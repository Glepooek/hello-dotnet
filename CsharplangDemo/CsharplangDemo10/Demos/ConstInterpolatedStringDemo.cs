namespace CsharplangDemo10.Demos;

public static class ConstInterpolatedStringDemo
{
    // C# 10: 若所有占位符均为 const 字符串, 内插字符串可以是 const
    private const string AppName    = "DemoApp";
    private const string Version    = "1.0.0";
    private const string Environment = "Production";

    // C# 10 前: 编译错误
    // C# 10 起: 合法, 因为所有 {} 内都是 const string
    private const string FullVersion = $"{AppName} v{Version}";
    private const string Banner      = $"=== {FullVersion} ({Environment}) ===";

    // 可以嵌套 const 内插字符串
    private const string LogPrefix  = $"[{AppName}]";
    private const string StartMsg   = $"{LogPrefix} 启动: {Banner}";

    public static void Run()
    {
        Console.WriteLine($"  FullVersion: {FullVersion}");
        Console.WriteLine($"  Banner:      {Banner}");
        Console.WriteLine($"  StartMsg:    {StartMsg}");

        // const 内插字符串可用于 switch case、特性参数等需要编译期常量的场景
        const string target = "DemoApp v1.0.0";
        bool match = FullVersion == target;
        Console.WriteLine($"  编译期常量比较: {match}");

        // ── 限制: 占位符必须全为 const string ─────────────────────
        // const int num = 42;
        // const string s = $"num={num}";  // 编译错误: int 不是 string
        Console.WriteLine();
        Console.WriteLine("  限制: 占位符只能是 const string, 不能是其他类型");
        Console.WriteLine("  用途: switch case、Attribute 参数、编译期字符串组合");
    }
}
