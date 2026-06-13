namespace CsharplangDemo14.Demos;

// C# 14: 用户定义复合赋值运算符
// 语法: 实例方法，无 static，void 返回类型
//   public void operator +=(T other) { ... }
// 该方法就地修改实例，避免对象分配。
// 主要适用于引用类型 (class)：默认行为
//   x += y  ⟹  x = x + y  每次都会分配新实例。
public class BigCounter
{
    public long Value { get; private set; }

    public BigCounter(long value) => Value = value;

    public static BigCounter operator +(BigCounter a, long b) =>
        new(a.Value + b);  // 旧路径: 分配新实例

    // C# 14: 就地复合赋值 — 无分配
    public void operator +=(long b)
    {
        Console.Write("  [就地 +=, 无分配] ");
        Value += b;
    }

    public void operator -=(long b)
    {
        Console.Write("  [就地 -=, 无分配] ");
        Value -= b;
    }

    public void operator *=(long b)
    {
        Console.Write("  [就地 *=, 无分配] ");
        Value *= b;
    }

    public override string ToString() => $"BigCounter({Value})";
}

public static class UserDefinedCompoundAssignDemo
{
    public static void Run()
    {
        var c = new BigCounter(10);
        Console.WriteLine($"  初始:  {c}");

        c += 5;
        Console.WriteLine($"c += 5  → {c}");

        c -= 3;
        Console.WriteLine($"c -= 3  → {c}");

        c *= 4;
        Console.WriteLine($"c *= 4  → {c}");

        Console.WriteLine();
        Console.WriteLine("  规则: 实例方法, void 返回, 无 static");
        Console.WriteLine("  优势: 就地修改, 引用类型可避免分配新实例");
        Console.WriteLine("  对比: c += 5 若无 operator+=, 等价于 c = c + 5 (调用 operator+, 分配新对象)");
    }
}
