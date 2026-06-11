namespace CsharplangDemo12.Demos;

public static class RefReadonlyDemo
{
    // ── 三种引用参数修饰符对比 ─────────────────────────────────────
    //
    //  in          接受变量或右值, 承诺不修改, 调用方无需加修饰符
    //  ref         接受变量,       可以修改,   调用方必须加 ref
    //  ref readonly 接受变量,      承诺不修改, 调用方加 ref 或 in (C# 12 新增)
    //
    // ref readonly 的语义: "我需要真实的内存地址（不接受临时副本），但我保证不修改它"
    // 适用场景: 大型结构体只读传递、P/Invoke 互操作

    static void ReadWithIn(in int value)
    {
        Console.WriteLine($"  in:          value = {value}");
        // value = 99;  // 编译错误: in 参数只读
    }

    static void ReadWithRefReadonly(ref readonly int value)
    {
        Console.WriteLine($"  ref readonly: value = {value}");
        // value = 99;  // 编译错误: ref readonly 参数只读
    }

    // 大型结构体场景: 避免复制开销, 同时明确不修改
    readonly struct BigData(double x, double y, double z, double w)
    {
        public double X = x; public double Y = y;
        public double Z = z; public double W = w;
        public double Magnitude => Math.Sqrt(X*X + Y*Y + Z*Z + W*W);
    }

    static double ComputeMagnitude(ref readonly BigData data) => data.Magnitude;

    public static void Run()
    {
        int x = 42;

        // in: 可传变量或右值（字面量/表达式）
        ReadWithIn(x);        // 传变量
        ReadWithIn(100);      // 传右值 ✅

        // ref readonly: 必须传具名变量（不接受右值/临时值）
        ReadWithRefReadonly(ref x);   // 传变量, 加 ref
        ReadWithRefReadonly(in x);    // 传变量, 加 in (C# 12 允许)
        // ReadWithRefReadonly(ref readonly 100);  // 编译错误: 不接受右值

        Console.WriteLine();

        // 大型结构体: 零拷贝只读传递
        var data = new BigData(1.0, 2.0, 3.0, 4.0);
        double mag = ComputeMagnitude(ref data);
        Console.WriteLine($"  BigData.Magnitude = {mag:F4}");

        Console.WriteLine();
        Console.WriteLine("  ref readonly 填补了语义空白:");
        Console.WriteLine("    in    = 可接受右值的只读引用 (有时会创建隐式副本)");
        Console.WriteLine("    ref readonly = 必须是变量的只读引用 (保证无副本)");
    }
}
