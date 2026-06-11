namespace CsharplangDemo14.Demos;

public static class SpanConversionDemo
{
    public static void Run()
    {
        int[] array = [1, 2, 3, 4, 5];

        // ── C# 14 之前已有的转换 ──────────────────────────────────────
        Span<int> span = array;                      // T[] -> Span<T>  (隐式)
        ReadOnlySpan<int> ros = array;               // T[] -> ReadOnlySpan<T> (隐式)
        ReadOnlySpan<int> rosFromSpan = span;        // Span<T> -> ReadOnlySpan<T> (隐式)

        // ── C# 14 新增的隐式转换 ──────────────────────────────────────
        // 1. Span<T> / ReadOnlySpan<T> 可作为扩展方法接收器参与方法解析
        //    编译器在推断泛型类型时能正确处理 Span 系列
        PrintLength(array);      // T[] -> ReadOnlySpan<T> 参与泛型推断
        PrintLength(span);       // Span<T> -> ReadOnlySpan<T> 参与泛型推断
        PrintLength(ros);        // 直接匹配

        // 2. 与其他转换组合: Span<derived> -> ReadOnlySpan<base> 等场景
        string[] words = ["hello", "world", "csharp"];
        ReadOnlySpan<string> wordSpan = words;
        PrintWords(wordSpan);

        // 3. 隐式转换在三元表达式中的应用
        bool useArray = true;
        ReadOnlySpan<int> chosen = useArray ? array : span;  // 统一为 ReadOnlySpan<int>
        Console.WriteLine($"  三元表达式选择的 Span 长度: {chosen.Length}");

        // 4. 传递给接受 ReadOnlySpan<T> 的方法时无需手动转换
        int sum = Sum(array);    // 直接传 int[], 隐式转为 ReadOnlySpan<int>
        Console.WriteLine($"  Sum(int[]) via ReadOnlySpan: {sum}");
    }

    // 接受 ReadOnlySpan<T>: C# 14 让 T[]/Span<T> 都能无缝传入
    static void PrintLength<T>(ReadOnlySpan<T> span) =>
        Console.WriteLine($"  PrintLength<{typeof(T).Name}> 长度: {span.Length}");

    static void PrintWords(ReadOnlySpan<string> words)
    {
        Console.Write("  Words: ");
        foreach (var w in words) Console.Write($"{w} ");
        Console.WriteLine();
    }

    static int Sum(ReadOnlySpan<int> values)
    {
        int total = 0;
        foreach (var v in values) total += v;
        return total;
    }
}
