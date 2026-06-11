namespace CsharplangDemo14.Demos;

public static class LambdaModifierDemo
{
    delegate bool TryParse<T>(string text, out T result);
    delegate void Modifier<T>(ref T value);
    delegate void ReadOnly<T>(in T value);
    delegate void ScopedConsumer<T>(scoped T value) where T : allows ref struct;

    public static void Run()
    {
        // ── out 修饰符: 无需写类型 ───────────────────────────────────
        // C# 14 前: 必须写全部类型
        TryParse<int> old = (string text, out int result) => int.TryParse(text, out result);

        // C# 14 起: 只写修饰符, 省略类型
        TryParse<int> newer = (text, out result) => int.TryParse(text, out result);

        Console.WriteLine($"  TryParse(旧): {TryResult(old, "42")}");
        Console.WriteLine($"  TryParse(新): {TryResult(newer, "42")}");
        Console.WriteLine($"  TryParse(新, 非数字): {TryResult(newer, "abc")}");

        // ── ref 修饰符 ───────────────────────────────────────────────
        Modifier<int> doubler = (ref x) => x *= 2;
        int val = 10;
        doubler(ref val);
        Console.WriteLine($"  ref 修饰: doubler(ref 10) = {val}");

        // ── in 修饰符 (只读引用) ─────────────────────────────────────
        ReadOnly<int> printer = (in x) => Console.WriteLine($"  in 修饰: 读取值 = {x}");
        int readVal = 99;
        printer(in readVal);

        // ── scoped 修饰符 (ref struct 参数不逃逸) ────────────────────
        ScopedConsumer<ReadOnlySpan<char>> spanPrinter =
            (scoped span) => Console.WriteLine($"  scoped span: '{span.ToString()}'");
        spanPrinter("hello span");

        Console.WriteLine();
        Console.WriteLine("  注: params 修饰符仍需显式类型 (编译器无法推断集合元素类型)");
    }

    static string TryResult(TryParse<int> parse, string input) =>
        parse(input, out var r) ? $"成功={r}" : "失败";
}
