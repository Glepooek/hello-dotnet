using System.Runtime.CompilerServices;
using System.Text;

// ── 自定义内插字符串处理程序 ──────────────────────────────────────────
// C# 10 允许库作者实现 InterpolatedStringHandler, 控制内插字符串的构建逻辑
// 典型用途: 日志级别检查, 条件构建, 高性能场景

[InterpolatedStringHandler]
public ref struct ConditionalInterpolatedStringHandler
{
    private StringBuilder? _builder;
    private readonly bool _shouldBuild;

    // 编译器在构造处理程序时调用此构造函数
    // literalLength: 字面量字符总长度估计
    // formattedCount: {} 占位符数量
    // condition: 自定义参数 (通过 InterpolatedStringHandlerArgument 传入)
    public ConditionalInterpolatedStringHandler(
        int literalLength, int formattedCount,
        bool condition, out bool shouldAppend)
    {
        _shouldBuild = condition;
        shouldAppend = condition;
        _builder = condition ? new StringBuilder(literalLength) : null;
    }

    public void AppendLiteral(string s) => _builder?.Append(s);
    public void AppendFormatted<T>(T value) => _builder?.Append(value);
    public void AppendFormatted<T>(T value, string? format)
        where T : IFormattable => _builder?.Append(value?.ToString(format, null));

    public string GetFormattedText() => _builder?.ToString() ?? string.Empty;
    public bool IsEnabled => _shouldBuild;
}

// 使用处理程序的 API
public static class ConditionalLogger
{
    // 编译器识别 InterpolatedStringHandlerArgument: 将 $"..." 转换为处理程序调用
    // 只有 enabled=true 时才构建字符串
    public static void Log(
        bool enabled,
        [InterpolatedStringHandlerArgument(nameof(enabled))]
        ConditionalInterpolatedStringHandler handler)
    {
        if (handler.IsEnabled)
            Console.WriteLine($"  [LOG] {handler.GetFormattedText()}");
    }
}

public static class InterpolatedStringHandlerDemo
{
    public static void Run()
    {
        // ── BCL 内置处理程序: DefaultInterpolatedStringHandler ────────
        // 普通 $"..." 已由编译器转换为 DefaultInterpolatedStringHandler
        // 相比旧的 string.Format 方案, 避免了装箱和 params 数组分配
        string name = "Alice";
        int age = 30;
        string s1 = $"用户: {name}, 年龄: {age}";  // 编译器使用 handler, 非 Format
        Console.WriteLine($"  内插字符串: {s1}");

        // ── 自定义处理程序: 条件构建 ────────────────────────────────
        // enabled=true: 构建字符串并打印
        ConditionalLogger.Log(true,  $"请求完成, 用户={name}, 耗时={42}ms");
        // enabled=false: 完全不构建字符串 (零分配)
        ConditionalLogger.Log(false, $"这段文字不会被构建: {name}, {DateTime.Now}");

        Console.WriteLine();
        Console.WriteLine("  内插字符串处理程序的核心价值:");
        Console.WriteLine("    • 日志场景: 级别未启用时不构建字符串, 零分配");
        Console.WriteLine("    • 编译器生成调用 AppendLiteral/AppendFormatted 序列");
        Console.WriteLine("    • 而非 string.Format(fmt, args) — 无 params 数组, 无装箱");
    }
}
