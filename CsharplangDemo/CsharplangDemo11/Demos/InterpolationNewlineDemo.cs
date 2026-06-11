namespace CsharplangDemo11.Demos;

public static class InterpolationNewlineDemo
{
    public static void Run()
    {
        int x = 42;
        string[] tags = new[] { "csharp", "dotnet", "demo" };

        // ── C# 11 前: 内插 {} 内不能换行 ─────────────────────────────
        // 复杂表达式必须提取到临时变量
        string tagStr = string.Join(", ", tags);
        string old = $"x={x}, tags=[{tagStr}]";
        Console.WriteLine($"  旧写法: {old}");

        // ── C# 11 起: {} 内可以换行 ──────────────────────────────────
        string result = $"x={x}, tags=[{string.Join(
            ", ",
            tags)}]";
        Console.WriteLine($"  新写法: {result}");

        // 与三元运算符组合: 更易读
        string label = $"状态: {(x > 0
            ? "正数"
            : x < 0
                ? "负数"
                : "零")}";
        Console.WriteLine($"  三元: {label}");

        // 与 LINQ 结合: 不再需要临时变量
        int[] nums = new[] { 5, 3, 8, 1, 9, 2 };
        string report = $"""
            数组报告:
              最大值: {nums.Max()}
              最小值: {nums.Min()}
              排序后: {string.Join(", ", System.Linq.Enumerable.OrderBy(nums, n => n))}
            """;
        Console.WriteLine("  内插原始字符串 + 换行:");
        Console.WriteLine("  " + report.Replace("\n", "\n  "));

        Console.WriteLine();
        Console.WriteLine("  C# 11 前: {} 内一行写完，复杂逻辑需临时变量");
        Console.WriteLine("  C# 11 起: {} 内可换行，复杂表达式直接内联");
    }
}
