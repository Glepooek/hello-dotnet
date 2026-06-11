namespace CsharplangDemo13.Demos;

public class TimerRemaining
{
    public int[] Buffer { get; set; } = new int[10];
}

public static class ImplicitIndexDemo
{
    public static void Run()
    {
        // C# 13 前: 对象初始化器中不能使用 ^ (从末端) 运算符
        // C# 13 起: 对象初始化器支持隐式索引访问

        var countdown = new TimerRemaining
        {
            Buffer =
            {
                [^1] = 0,   // index 9
                [^2] = 1,   // index 8
                [^3] = 2,   // index 7
                [^4] = 3,
                [^5] = 4,
                [^6] = 5,
                [^7] = 6,
                [^8] = 7,
                [^9] = 8,
                [^10] = 9,  // index 0
            }
        };

        Console.Write("  倒计时: ");
        foreach (var n in countdown.Buffer) Console.Write($"{n} ");
        Console.WriteLine();

        // 对比旧写法: 必须手动计算正向索引
        // Buffer = { [0] = 9, [1] = 8, ..., [9] = 0 }
        // 新写法语义更清晰: "最后一个是 0, 倒数第二个是 1..."
    }
}
