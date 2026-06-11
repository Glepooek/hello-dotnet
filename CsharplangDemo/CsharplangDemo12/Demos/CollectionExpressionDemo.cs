namespace CsharplangDemo12.Demos;

public static class CollectionExpressionDemo
{
    public static void Run()
    {
        // ── 1. 统一语法创建各种集合类型 ──────────────────────────────
        int[]        array   = [1, 2, 3, 4, 5];
        List<string> list    = ["alpha", "beta", "gamma"];
        Span<char>   span    = ['a', 'b', 'c', 'd'];
        HashSet<int> hashSet = [10, 20, 30, 20, 10];   // 自动去重

        Console.WriteLine($"  int[]:       [{string.Join(", ", array)}]");
        Console.WriteLine($"  List<string>:[{string.Join(", ", list)}]");
        Console.WriteLine($"  Span<char>:  [{string.Join(", ", span.ToArray())}]");
        Console.WriteLine($"  HashSet<int>:[{string.Join(", ", hashSet)}] (去重后)");

        // ── 2. 展开运算符 (..) ────────────────────────────────────────
        int[] first  = [1, 2, 3];
        int[] second = [4, 5, 6];
        int[] extra  = [7, 8, 9];

        // .. 将集合元素内联展开，等效于 Concat 但编译期优化为 MemoryCopy
        int[] merged = [..first, ..second, ..extra];
        Console.WriteLine($"  合并: [{string.Join(", ", merged)}]");

        // 展开 + 追加/前置元素
        int[] padded = [0, ..first, ..second, 10];
        Console.WriteLine($"  填充: [{string.Join(", ", padded)}]");

        // ── 3. 二维交错数组 ────────────────────────────────────────────
        int[][] matrix = [[1, 2, 3], [4, 5, 6], [7, 8, 9]];
        Console.Write("  矩阵: ");
        foreach (var row in matrix)
            Console.Write($"[{string.Join(",", row)}] ");
        Console.WriteLine();

        // ── 4. 用变量构建交错数组 ────────────────────────────────────
        int[] row0 = [1, 0, 0];
        int[] row1 = [0, 1, 0];
        int[] row2 = [0, 0, 1];
        int[][] identity = [row0, row1, row2];
        Console.Write("  单位矩阵: ");
        foreach (var r in identity)
            Console.Write($"[{string.Join(",", r)}] ");
        Console.WriteLine();

        // ── 5. 作为方法参数直接传入 ───────────────────────────────────
        Console.WriteLine($"  Sum([1..5]) = {Sum([1, 2, 3, 4, 5])}");

        // ── 6. 空集合 ─────────────────────────────────────────────────
        int[]        emptyArr  = [];
        List<string> emptyList = [];
        Console.WriteLine($"  空数组长度: {emptyArr.Length}, 空列表: {emptyList.Count}");

        // ── C# 12 前对比 ──────────────────────────────────────────────
        Console.WriteLine();
        Console.WriteLine("  C# 12 前: new int[] { 1, 2, 3 } 或 new List<string> { ... }");
        Console.WriteLine("  C# 12 起: [1, 2, 3] 统一语法, 编译器推断目标类型");
    }

    static int Sum(ReadOnlySpan<int> values)
    {
        int total = 0;
        foreach (var v in values) total += v;
        return total;
    }
}
