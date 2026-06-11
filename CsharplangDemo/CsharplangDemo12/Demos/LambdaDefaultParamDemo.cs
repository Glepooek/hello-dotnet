namespace CsharplangDemo12.Demos;

public static class LambdaDefaultParamDemo
{
    public static void Run()
    {
        // ── C# 12 前: lambda 参数不支持默认值 ────────────────────────
        // 必须通过重载或条件判断模拟默认值

        // ── C# 12 起: lambda 参数可有默认值 ─────────────────────────

        // 1. 基本用法: 字符串参数带默认值
        var greet = (string name, string greeting = "Hello") =>
            $"{greeting}, {name}!";

        Console.WriteLine($"  greet(\"Alice\")          = {greet("Alice")}");
        Console.WriteLine($"  greet(\"Bob\", \"Hi\")      = {greet("Bob", "Hi")}");
        Console.WriteLine($"  greet(\"Carol\", \"你好\")  = {greet("Carol", "你好")}");

        // 2. 数值类型默认值
        Func<int, int, int> add = (x, y = 10) => x + y;
        Console.WriteLine($"  add(5)     = {add(5)}");     // 15
        Console.WriteLine($"  add(5, 3)  = {add(5, 3)}");  // 8

        // 3. 多个默认参数
        var format = (double value, int decimals = 2, string unit = "m") =>
            $"{value.ToString($"F{decimals}")}{unit}";

        Console.WriteLine($"  format(3.14159)          = {format(3.14159)}");
        Console.WriteLine($"  format(3.14159, 4)       = {format(3.14159, 4)}");
        Console.WriteLine($"  format(3.14159, 4, \"km\") = {format(3.14159, 4, "km")}");

        // 4. 与本地函数对比: 规则与普通方法/本地函数完全一致
        static string LocalFormat(double v, int d = 2) => v.ToString($"F{d}");
        var lambdaFormat = (double v, int d = 2) => v.ToString($"F{d}");

        Console.WriteLine($"  本地函数: {LocalFormat(Math.PI)}");
        Console.WriteLine($"  Lambda:   {lambdaFormat(Math.PI)}");

        // 5. 实用场景: 配置回调
        var log = (string msg, ConsoleColor color = ConsoleColor.White) =>
        {
            Console.ForegroundColor = color;
            Console.Write($"  [LOG] {msg}");
            Console.ResetColor();
            Console.WriteLine();
        };

        log("普通消息");
        log("成功消息", ConsoleColor.Green);
        log("警告消息", ConsoleColor.Yellow);
    }
}
