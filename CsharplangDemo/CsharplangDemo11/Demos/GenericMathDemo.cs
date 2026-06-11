using System.Numerics;

namespace CsharplangDemo11.Demos;

// ── 核心: 接口中的 static abstract 成员 ─────────────────────────────
// 这是泛型数学的语言基础: 约束泛型类型必须支持某个静态运算符/属性

// 自定义泛型可加接口
public interface IAddable<T> where T : IAddable<T>
{
    static abstract T operator +(T left, T right);
    static abstract T Zero { get; }
}

// 使用标准 BCL 接口 INumber<T> (需 .NET 7+, 已在 net10.0 上可用)

// ── 泛型算法: 只写一次, 适用于所有数值类型 ───────────────────────────
public static class MathAlgorithms
{
    // 对任意 INumber<T> 类型求和
    public static T Sum<T>(IEnumerable<T> values) where T : INumber<T>
    {
        T result = T.Zero;
        foreach (var v in values) result += v;
        return result;
    }

    // 泛型平均值
    public static T Average<T>(IEnumerable<T> values) where T : INumber<T>
    {
        T sum = T.Zero;
        int count = 0;
        foreach (var v in values) { sum += v; count++; }
        return sum / T.CreateChecked(count);
    }

    // 泛型最大值 (利用 IComparisonOperators 约束)
    public static T Max<T>(T a, T b) where T : INumber<T> => a > b ? a : b;

    // 利用 static abstract: 调用类型自己的 Parse
    public static T ParseNumber<T>(string s) where T : INumber<T> =>
        T.Parse(s, null);
}

public static class GenericMathDemo
{
    public static void Run()
    {
        // 同一个 Sum<T> 方法适用于 int / double / decimal
        int[]     ints     = new int[]     { 1, 2, 3, 4, 5 };
        double[]  doubles  = new double[]  { 1.1, 2.2, 3.3 };
        decimal[] decimals = new decimal[] { 10.5m, 20.3m, 5.2m };

        Console.WriteLine($"  Sum<int>:     {MathAlgorithms.Sum(ints)}");
        Console.WriteLine($"  Sum<double>:  {MathAlgorithms.Sum(doubles):F1}");
        Console.WriteLine($"  Sum<decimal>: {MathAlgorithms.Sum(decimals):F1}");

        Console.WriteLine($"  Avg<int>:     {MathAlgorithms.Average(ints)}");
        Console.WriteLine($"  Avg<double>:  {MathAlgorithms.Average(doubles):F2}");

        Console.WriteLine($"  Max(3, 7):    {MathAlgorithms.Max(3, 7)}");
        Console.WriteLine($"  Max(3.5, 2.1):{MathAlgorithms.Max(3.5, 2.1)}");

        // 泛型 Parse
        int    parsed1 = MathAlgorithms.ParseNumber<int>("42");
        double parsed2 = MathAlgorithms.ParseNumber<double>("3.14");
        Console.WriteLine($"  Parse<int>(\"42\")   = {parsed1}");
        Console.WriteLine($"  Parse<double>(\"3.14\") = {parsed2}");

        Console.WriteLine();
        Console.WriteLine("  static abstract 成员是实现泛型数学的语言基础");
        Console.WriteLine("  BCL 中 INumber<T>/IAdditionOperators<T,T,T> 等接口均依赖此特性");
    }
}
