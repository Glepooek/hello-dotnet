using System.Runtime.CompilerServices;

namespace CsharplangDemo12.Demos;

// ── 内联数组声明 ──────────────────────────────────────────────────────
// [InlineArray(N)]: struct 中包含 N 个连续元素，存储在栈上
// 只需声明第一个字段，编译器自动扩展为 N 个槽位
[InlineArray(8)]
public struct Buffer8
{
    private int _element0;  // 声明元素类型即可，字段名无关紧要
}

[InlineArray(4)]
public struct FloatBuffer4
{
    private float _e0;
}

// 固定大小的字符缓冲区（替代 unsafe fixed char[N]）
[InlineArray(16)]
public struct CharBuffer16
{
    private char _c0;
}

public static class InlineArrayDemo
{
    public static void Run()
    {
        // ── 基本读写: 与普通数组语法完全一致 ─────────────────────────
        var buf = new Buffer8();
        for (int i = 0; i < 8; i++) buf[i] = i * i;

        Console.Write("  Buffer8 (平方数): ");
        foreach (var v in buf) Console.Write($"{v} ");
        Console.WriteLine();

        // ── 通过 Span<T> 访问: 运行时 API 暴露内联数组的标准方式 ──────
        Span<int> span = buf;
        span[0] = 999;  // 通过 Span 修改原始内联数组
        Console.WriteLine($"  通过 Span 修改 buf[0] = {buf[0]}");

        // ── ReadOnlySpan 只读访问 ─────────────────────────────────────
        ReadOnlySpan<int> ros = buf;
        int sum = 0;
        foreach (var v in ros) sum += v;
        Console.WriteLine($"  ReadOnlySpan 求和: {sum}");

        // ── float 缓冲区 ──────────────────────────────────────────────
        var fbuf = new FloatBuffer4();
        fbuf[0] = 1.1f; fbuf[1] = 2.2f; fbuf[2] = 3.3f; fbuf[3] = 4.4f;
        Console.Write("  FloatBuffer4: ");
        foreach (var v in fbuf) Console.Write($"{v:F1} ");
        Console.WriteLine();

        // ── 字符缓冲区 ────────────────────────────────────────────────
        var cbuf = new CharBuffer16();
        string text = "Hello, C# 12!";
        for (int i = 0; i < Math.Min(text.Length, 16); i++) cbuf[i] = text[i];
        Console.WriteLine($"  CharBuffer16: {new string(((ReadOnlySpan<char>)cbuf).TrimEnd('\0'))}");

        Console.WriteLine();
        Console.WriteLine("  内联数组特点:");
        Console.WriteLine("    • 存储于栈（struct），无堆分配");
        Console.WriteLine("    • 索引语法与普通数组相同");
        Console.WriteLine("    • 通过 Span<T>/ReadOnlySpan<T> 集成到现有 API");
        Console.WriteLine("    • 通常由库/运行时使用，用户透明消费");
    }
}
