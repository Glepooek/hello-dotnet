namespace CsharplangDemo10.Demos;

public static class LambdaImprovementDemo
{
    public static void Run()
    {
        // ── 1. Lambda 自然类型: 编译器推断委托类型 ────────────────────
        // C# 10 前: 必须显式指定 Func<>/Action<> 类型
        Func<string, string> old1 = name => $"Hello, {name}!";

        // C# 10 起: var 推断
        var greet  = (string name) => $"Hello, {name}!";       // Func<string, string>
        var add    = (int x, int y) => x + y;                  // Func<int, int, int>
        var print  = (string s) => Console.WriteLine(s);       // Action<string>

        Console.WriteLine($"  greet:  {greet("Alice")}");
        Console.WriteLine($"  add:    {add(3, 4)}");

        // 自然类型让 Lambda 可以直接赋给 var，更紧凑
        print("  print: 通过 Lambda 调用 Console.WriteLine");

        // ── 2. 显式声明 Lambda 返回类型 ──────────────────────────────
        // 当编译器无法推断返回类型时（如条件分支返回不同类型）可显式声明
        var toInt   = int    (string s)  => int.Parse(s);
        var toFloat = double (string s)  => double.Parse(s);

        Console.WriteLine($"  toInt(\"42\"):    {toInt("42")}");
        Console.WriteLine($"  toFloat(\"3.14\"): {toFloat("3.14")}");

        // 异步 Lambda 也可声明返回类型
        var asyncOp = async Task<int> (int x) =>
        {
            await Task.Delay(0);
            return x * 2;
        };
        Console.WriteLine($"  asyncOp(21) = {asyncOp(21).Result}");

        // ── 3. Lambda 可添加特性 ──────────────────────────────────────
        // C# 10 前: Lambda 不能有特性
        // C# 10 起: 特性可应用于 Lambda 或其参数

        // [Obsolete] 标记的 Lambda
        var obsoleteLambda = [System.Obsolete("请使用新版本")]
            (int x) => x * 2;

        // 参数特性
        var traced = (
            [System.Diagnostics.CodeAnalysis.NotNull] string input)
            => input.ToUpper();

        Console.WriteLine($"  obsoleteLambda(5) = {obsoleteLambda(5)}");
        Console.WriteLine($"  traced(\"hello\")   = {traced("hello")}");

        Console.WriteLine();
        Console.WriteLine("  Lambda 改进让委托推断更简洁，减少显式 Func<>/Action<> 声明");
    }
}
