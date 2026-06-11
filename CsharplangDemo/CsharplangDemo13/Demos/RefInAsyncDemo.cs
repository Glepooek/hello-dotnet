namespace CsharplangDemo13.Demos;

public static class RefInAsyncDemo
{
    public static async Task RunAsync()
    {
        // C# 13 前: async 方法和迭代器中不允许 ref 局部变量
        // C# 13 起: 可以声明 ref 局部变量 / ref struct 类型局部变量,
        //           但不能跨越 await / yield return 边界使用

        int[] data = [10, 20, 30, 40, 50];

        // 在 await 前使用 ref 局部变量
        ref int first = ref data[0];
        first = 99;  // 通过 ref 修改原数组
        Console.WriteLine($"  ref 修改 data[0]: {data[0]}");  // 99

        // ReadOnlySpan<T> 可在 await 前/后的独立安全区域内使用
        ReadOnlySpan<int> span = data.AsSpan(1, 3);
        int sum = 0;
        foreach (var n in span) sum += n;
        Console.WriteLine($"  Span 求和 (await 前): {sum}");

        await Task.Delay(0);  // 跨越 await 边界

        // await 之后 ref/span 变量不可访问 (编译器强制保证安全)
        // 可以重新声明
        ReadOnlySpan<int> spanAfter = data;
        Console.WriteLine($"  await 后重新声明 Span, 长度: {spanAfter.Length}");

        // 迭代器示例: 展示 unsafe 上下文可在迭代器中使用
        foreach (var v in CountDown(3))
            Console.Write($"  {v}");
        Console.WriteLine();
    }

    // C# 13: 迭代器方法中可使用 unsafe 上下文 (yield 语句本身仍需在安全上下文)
    static IEnumerable<int> CountDown(int from)
    {
        for (int i = from; i >= 0; i--)
        {
            unsafe
            {
                int local = i;
                int* ptr = &local;  // unsafe 块内使用指针
                yield return *ptr;  // yield 在安全上下文, 指针在 yield 前已释放
            }
        }
    }
}
