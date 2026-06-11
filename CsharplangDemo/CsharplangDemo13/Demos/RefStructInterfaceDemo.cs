namespace CsharplangDemo13.Demos;

// ── 演示 8: ref struct 实现接口 ──────────────────────────────────────────

public interface IPrintable
{
    void Print();
}

// C# 13 前: ref struct 不允许实现接口
// C# 13 起: 可以实现, 但不能装箱转换为接口类型
public ref struct StackBuffer : IPrintable
{
    private int _value;

    public StackBuffer(int value) => _value = value;

    public void Print() => Console.WriteLine($"  StackBuffer.Print(): {_value}");
}

// ── 演示 7: allows ref struct 泛型反约束 ──────────────────────────────────

// allows ref struct 允许 ref struct 类型作为泛型类型参数
public static class GenericProcessor<T> where T : IPrintable, allows ref struct
{
    // scoped 确保 T 的实例不会逃逸出方法
    public static void Process(scoped T item)
    {
        item.Print();
    }
}

public static class RefStructInterfaceDemo
{
    public static void Run()
    {
        var buf = new StackBuffer(42);

        // 直接调用接口方法 (无装箱)
        buf.Print();

        // 通过 allows ref struct 泛型约束调用 (无装箱)
        GenericProcessor<StackBuffer>.Process(buf);

        // 注意: 以下代码会导致编译错误 (装箱 ref struct 到接口)
        // IPrintable p = buf;  // 错误: cannot convert 'ref struct' to interface
    }
}
