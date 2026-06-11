namespace CsharplangDemo13.Demos;

public static class NewLockDemo
{
    // C# 13 前: lock 内部使用 System.Threading.Monitor
    // C# 13 起: 当锁对象为 System.Threading.Lock 时, 编译器自动改用
    //           Lock.EnterScope() -- 返回 ref struct, 退出时调用 Dispose()
    private static readonly Lock _lock = new();
    private static int _counter;

    public static void Run()
    {
        var tasks = Enumerable.Range(0, 5)
            .Select(_ => Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    lock (_lock)   // 编译器识别 Lock 类型, 使用新 API
                    {
                        _counter++;
                    }
                }
            }))
            .ToArray();

        Task.WaitAll(tasks);
        Console.WriteLine($"  预期 500, 实际: {_counter}");

        // Lock.EnterScope() 也可直接用于精细控制
        using (_lock.EnterScope())
        {
            Console.WriteLine("  通过 EnterScope() 手动持锁中...");
        }
        Console.WriteLine("  锁已释放");
    }
}
