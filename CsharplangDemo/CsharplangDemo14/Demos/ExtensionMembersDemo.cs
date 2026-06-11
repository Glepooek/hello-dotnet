namespace CsharplangDemo14.Demos;

// ── 旧的扩展方法写法（对比用）──────────────────────────────────────
// 旧写法: 只能扩展实例方法, 无法添加属性/静态成员/运算符
public static class OldStringExtensions
{
    public static bool IsNullOrEmpty(this string? s) => string.IsNullOrEmpty(s);
}

// ── C# 14 新语法: extension 块 ──────────────────────────────────────
public static class StringExtensions
{
    // 实例扩展块: 扩展 string? 的实例成员
    extension(string? s)
    {
        // 扩展属性 (旧写法无法做到)
        public bool IsNullOrEmpty => string.IsNullOrEmpty(s);

        // 扩展方法: 和旧写法等效，但结构更清晰
        public string OrDefault(string defaultValue) =>
            string.IsNullOrEmpty(s) ? defaultValue : s!;
    }

    // 静态扩展块: 扩展 string 的静态成员
    extension(string)
    {
        // 静态扩展属性 (旧写法完全无法做到)
        public static string Separator => "─────────────────";

        // 静态扩展方法
        public static string Repeat(string value, int count) =>
            string.Concat(Enumerable.Repeat(value, count));
    }
}

public static class IntExtensions
{
    // 扩展块同时支持泛型约束
    extension(int value)
    {
        public bool IsInRange(int min, int max) => value >= min && value <= max;
        public int Clamp(int min, int max) => Math.Clamp(value, min, max);
    }

    // 静态扩展块: 扩展运算符 (用户定义运算符的扩展形式)
    extension(int)
    {
        // 扩展运算符: 让两个 int 相加并返回字符串（演示用，无实际意义）
        public static string operator %(int left, string format) =>
            left.ToString(format);
    }
}

public static class ExtensionMembersDemo
{
    public static void Run()
    {
        string? empty = null;
        string? name = "  Alice  ";

        // 扩展属性
        Console.WriteLine($"  null.IsNullOrEmpty  = {empty.IsNullOrEmpty}");
        Console.WriteLine($"  \"Alice\".IsNullOrEmpty = {name.IsNullOrEmpty}");

        // 扩展方法
        Console.WriteLine($"  null.OrDefault(\"Guest\") = {empty.OrDefault("Guest")}");

        // 静态扩展属性 (语法: Type.Property)
        Console.WriteLine($"  string.Separator = {string.Separator}");

        // 静态扩展方法
        Console.WriteLine($"  string.Repeat(\"* \", 5) = {string.Repeat("* ", 5)}");

        // int 扩展属性
        int score = 85;
        Console.WriteLine($"  85.IsInRange(0, 100) = {score.IsInRange(0, 100)}");
        Console.WriteLine($"  85.Clamp(0, 80)      = {score.Clamp(0, 80)}");

        // 扩展运算符
        Console.WriteLine($"  42 % \"D4\" = {42 % "D4"}");

        Console.WriteLine();
        Console.WriteLine("  对比旧写法: 旧扩展方法只能调用 OldStringExtensions.IsNullOrEmpty(s)");
        Console.WriteLine("  新写法: s.IsNullOrEmpty (属性语法，更自然)");
    }
}
