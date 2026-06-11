using System.Runtime.CompilerServices;

namespace CsharplangDemo13.Demos;

public static class OverloadPriorityDemo
{
    // 场景: 库作者新增了一个基于 ReadOnlySpan<T> 的高性能重载
    // 使用 [OverloadResolutionPriority] 使新重载优先, 同时保留旧重载兼容性

    // 旧重载 (保留兼容性, 优先级默认为 0)
    public static int Sum(int[] values)
    {
        Console.Write("  [旧 int[] 重载] ");
        int sum = 0;
        foreach (var v in values) sum += v;
        return sum;
    }

    // 新重载 (高性能: 零分配, 优先级 1 > 0, 编译器优先选择)
    [OverloadResolutionPriority(1)]
    public static int Sum(ReadOnlySpan<int> values)
    {
        Console.Write("  [新 ReadOnlySpan 重载] ");
        int sum = 0;
        foreach (var v in values) sum += v;
        return sum;
    }

    public static void Run()
    {
        int[] arr = [1, 2, 3, 4, 5];

        // 传入数组: 两个重载都适用, 但编译器因 Priority=1 优先选 Span 版本
        Console.WriteLine($"Sum(arr) = {Sum(arr)}");

        // 传入 Span: 精确匹配 Span 重载
        Console.WriteLine($"Sum(span) = {Sum(arr.AsSpan())}");

        // 若需强制调用旧重载, 显式强转
        Console.WriteLine($"Sum((int[])arr) = {Sum((int[])arr)}");
        // 注: 强转并不能绕过优先级, 仍需使用 (IEnumerable<int>) 或命名参数等方式
        // 实际场景中通常由库维护者而非调用方关心此特性
    }
}
