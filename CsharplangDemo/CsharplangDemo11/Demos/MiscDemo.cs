namespace CsharplangDemo11.Demos;

public static class MiscDemo
{
    // ── 1. 扩展 nameof 范围: 可在特性参数中使用方法参数名 ─────────────
    // C# 11 前: 特性内无法直接引用参数名, 需硬编码字符串
    static void OldValidate(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("不能为空", "name");  // 硬编码字符串
    }

    // C# 11 起: 特性内 nameof(param) 合法，重构安全
    static void Validate(string name, [System.Runtime.CompilerServices.CallerArgumentExpression(nameof(name))] string? expr = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException($"不能为空: {expr}", nameof(name));  // 使用 nameof
    }

    // ── 2. nint / nuint 关键字 (平台原生整数) ─────────────────────────
    // C# 11: nint/nuint 正式成为关键字（别名 IntPtr/UIntPtr）
    static void NativeIntDemo()
    {
        nint  ni = 100;         // System.IntPtr  的别名, 32位=4字节, 64位=8字节
        nuint nu = 200u;        // System.UIntPtr 的别名

        nint sum = ni + (nint)50;
        Console.WriteLine($"  nint:  {ni}, sum={sum}, 大小={nint.Size} 字节");
        Console.WriteLine($"  nuint: {nu}, 最大值={nuint.MaxValue}");

        // nint 支持算术运算符（C# 11 前 IntPtr 不直接支持 + - * /）
        nint product = ni * 3;
        Console.WriteLine($"  nint 算术: {ni} * 3 = {product}");
    }

    // ── 3. Span<char>/ReadOnlySpan<char> 的 string 常量模式匹配 ────────
    static void SpanPatternDemo()
    {
        // C# 11 前: 必须 ToString() 后才能模式匹配
        // C# 11 起: 直接对 Span<char> 使用字符串常量模式
        ReadOnlySpan<char> span = "hello".AsSpan();

        bool isHello = span is "hello";    // 直接匹配, 无需转 string
        bool isWorld = span is "world";
        Console.WriteLine($"  Span \"hello\" is \"hello\": {isHello}");
        Console.WriteLine($"  Span \"hello\" is \"world\": {isWorld}");

        // switch 中的 Span 模式
        string result = span switch
        {
            "hello"   => "问候语",
            "goodbye" => "告别语",
            _         => "其他"
        };
        Console.WriteLine($"  switch Span 结果: {result}");
    }

    public static void Run()
    {
        // nameof 扩展
        Console.WriteLine("  扩展 nameof:");
        try { Validate(""); }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"    捕获: {ex.Message}");
        }
        Validate("Alice");
        Console.WriteLine("    Validate(\"Alice\") 通过");

        Console.WriteLine("  nint/nuint:");
        NativeIntDemo();

        Console.WriteLine("  Span<char> 模式匹配:");
        SpanPatternDemo();

        Console.WriteLine();
        Console.WriteLine("  方法组委托缓存（编译器优化，无代码变化）:");
        Console.WriteLine("    C# 11 编译器对静态方法的委托转换会缓存实例，减少堆分配");
    }
}
