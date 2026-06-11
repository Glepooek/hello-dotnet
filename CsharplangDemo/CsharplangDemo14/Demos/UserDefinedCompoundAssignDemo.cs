namespace CsharplangDemo14.Demos;

// 自定义向量类型，演示用户定义复合赋值运算符
public struct Vector2D(double x, double y)
{
    public double X { get; set; } = x;
    public double Y { get; set; } = y;

    // 基础 + 运算符 (C# 一直支持)
    public static Vector2D operator +(Vector2D a, Vector2D b) =>
        new(a.X + b.X, a.Y + b.Y);

    public static Vector2D operator -(Vector2D a, Vector2D b) =>
        new(a.X - b.X, a.Y - b.Y);

    public static Vector2D operator *(Vector2D v, double scalar) =>
        new(v.X * scalar, v.Y * scalar);

    // C# 14 新增: 用户定义复合赋值运算符
    // 直接定义 += 避免 a = a + b 的中间对象（对可变引用类型尤为重要）
    public static Vector2D operator +=(Vector2D a, Vector2D b)
    {
        Console.Write($"  [自定义 +=] ");
        return new(a.X + b.X, a.Y + b.Y);
    }

    public static Vector2D operator -=(Vector2D a, Vector2D b)
    {
        Console.Write($"  [自定义 -=] ");
        return new(a.X - b.X, a.Y - b.Y);
    }

    public static Vector2D operator *=(Vector2D v, double scalar)
    {
        Console.Write($"  [自定义 *=] ");
        return new(v.X * scalar, v.Y * scalar);
    }

    public override string ToString() => $"({X:F1}, {Y:F1})";
}

public static class UserDefinedCompoundAssignDemo
{
    public static void Run()
    {
        var v = new Vector2D(1, 2);
        var delta = new Vector2D(3, 4);

        Console.WriteLine($"  初始:   v = {v}");

        // 直接调用自定义的 operator +=, 不再是 v = v + delta
        v += delta;
        Console.WriteLine($"  v += {delta} → {v}");

        v -= new Vector2D(1, 1);
        Console.WriteLine($"  v -= (1,1)  → {v}");

        v *= 2.0;
        Console.WriteLine($"  v *= 2.0    → {v}");

        Console.WriteLine();
        Console.WriteLine("  对比: C# 14 前 v += delta 等价于 v = v + delta");
        Console.WriteLine("        C# 14 起 v += delta 直接调用 operator +=");
        Console.WriteLine("        对可变引用类型可避免创建中间对象");
    }
}
