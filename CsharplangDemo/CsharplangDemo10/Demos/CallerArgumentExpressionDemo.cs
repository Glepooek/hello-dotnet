using System.Runtime.CompilerServices;

namespace CsharplangDemo10.Demos;

public static class CallerArgumentExpressionDemo
{
    // ── CallerArgumentExpression: 自动捕获调用方传入的表达式文本 ──────
    // C# 10 前: 断言失败时只知道"失败了", 不知道具体表达式
    // C# 10 起: 编译器自动将参数表达式文本注入 expr 参数

    // 简单断言
    static void Assert(
        bool condition,
        [CallerArgumentExpression(nameof(condition))]
        string? expr = null,
        [CallerMemberName] string? caller = null)
    {
        if (!condition)
            throw new InvalidOperationException(
                $"断言失败 [{caller}]: {expr}");
    }

    // 参数验证工具 (类似 ArgumentNullException.ThrowIfNull 的实现原理)
    static T NotNull<T>(
        T? value,
        [CallerArgumentExpression(nameof(value))] string? expr = null)
        where T : class
    {
        return value ?? throw new ArgumentNullException(expr, $"不能为 null: {expr}");
    }

    // 范围检查
    static void InRange(
        int value, int min, int max,
        [CallerArgumentExpression(nameof(value))] string? expr = null)
    {
        if (value < min || value > max)
            throw new ArgumentOutOfRangeException(
                expr, value, $"{expr} = {value} 不在 [{min}, {max}] 范围内");
    }

    public static void Run()
    {
        // ── 基本用法 ──────────────────────────────────────────────────
        int x = 10;
        Assert(x > 0);           // 通过
        Assert(x * 2 == 20);     // 通过

        // 断言失败时, expr 自动捕获表达式文本
        try
        {
            Assert(x > 100);
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine($"  捕获: {e.Message}");
        }

        // ── NotNull 验证 ─────────────────────────────────────────────
        string? name = "Alice";
        string valid = NotNull(name);       // 通过, expr = "name"
        Console.WriteLine($"  NotNull(\"{name}\"): {valid}");

        try
        {
            string? nullName = null;
            NotNull(nullName);              // expr 自动填充为 "nullName"
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine($"  捕获: {e.Message}");
        }

        // ── 范围检查 ─────────────────────────────────────────────────
        int score = 85;
        InRange(score, 0, 100);  // 通过, expr = "score"

        try
        {
            int age = -1;
            InRange(age, 0, 150);  // expr = "age"
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine($"  捕获: {e.Message.Split('\n')[0]}");
        }

        Console.WriteLine();
        Console.WriteLine("  CallerArgumentExpression 让错误消息自动包含表达式文本");
        Console.WriteLine("  无需手动写 nameof(x) 或字符串描述，重构时自动同步");
    }
}
