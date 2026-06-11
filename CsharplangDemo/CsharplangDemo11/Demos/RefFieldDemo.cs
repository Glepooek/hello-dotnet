namespace CsharplangDemo11.Demos;

// ── ref 字段: ref struct 内部可以存储对外部变量的引用 ─────────────────
// C# 11 前: ref struct 不能有 ref 字段
// C# 11 起: 可以声明 ref 字段，持有对托管堆/栈变量的引用

public ref struct ValueRef<T>
{
    private ref T _value;  // ref 字段: C# 11 新增

    public ValueRef(ref T value) => _value = ref value;

    // 通过 ref 字段读写原始变量（零拷贝）
    public T Value
    {
        get => _value;
        set => _value = value;
    }

    // ref return: 返回对字段的引用
    public ref T GetRef() => ref _value;
}

// 实际场景: 类似 Span<T> 的自定义轻量视图
public ref struct SliceView
{
    private ref int _start;   // ref 字段指向数组首元素
    private int _length;

    public SliceView(ref int start, int length)
    {
        _start = ref start;
        _length = length;
    }

    public int this[int index]
    {
        get
        {
            if ((uint)index >= (uint)_length)
                throw new IndexOutOfRangeException();
            // 通过 ref 字段 + Unsafe 偏移访问连续内存（简化演示）
            return System.Runtime.CompilerServices.Unsafe.Add(ref _start, index);
        }
    }

    public int Length => _length;
}

public static class RefFieldDemo
{
    // scoped ref: 限制引用不能逃逸出方法作用域，编译器可省略安全检查
    static void IncrementScoped(scoped ref int value)
    {
        value++;
        // 不能将 value 存储到字段或返回 —— scoped 保证了这一点
    }

    // scoped 参数: 防止 ref struct 参数逃逸
    static int SumScoped(scoped ref int first, scoped ref int second) =>
        first + second;

    public static void Run()
    {
        // ── ref 字段基本用法 ──────────────────────────────────────────
        int original = 42;
        var vref = new ValueRef<int>(ref original);

        Console.WriteLine($"  original = {original}, vref.Value = {vref.Value}");
        vref.Value = 100;   // 通过 ref 字段修改原始变量
        Console.WriteLine($"  vref.Value = 100 → original = {original}");

        ref int directRef = ref vref.GetRef();
        directRef = 999;    // 通过 ref return 修改
        Console.WriteLine($"  directRef = 999  → original = {original}");

        // ── SliceView: ref 字段实现零拷贝切片 ───────────────────────
        int[] arr = new int[] { 10, 20, 30, 40, 50 };
        var view = new SliceView(ref arr[1], 3);  // 视图: arr[1..4]
        Console.Write("  SliceView [1..4]: ");
        for (int i = 0; i < view.Length; i++) Console.Write($"{view[i]} ");
        Console.WriteLine();

        // ── scoped ref ────────────────────────────────────────────────
        int counter = 10;
        IncrementScoped(ref counter);
        Console.WriteLine($"  IncrementScoped: counter = {counter}");

        int a = 3, b = 7;
        int sum = SumScoped(ref a, ref b);
        Console.WriteLine($"  SumScoped(3, 7) = {sum}");

        Console.WriteLine();
        Console.WriteLine("  ref 字段 = ref struct 内持有外部变量引用（零拷贝）");
        Console.WriteLine("  scoped   = 限制引用不逃逸，编译器省略安全检查开销");
    }
}
