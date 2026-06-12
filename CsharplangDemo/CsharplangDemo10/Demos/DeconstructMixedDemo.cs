namespace CsharplangDemo10.Demos;

public static class DeconstructMixedDemo
{
    public static void Run()
    {
        // ── C# 10 前: 解构时不能在同一语句中混合声明和赋值 ───────────
        // 必须分开写:
        int oldX;
        int oldY;
        (oldX, oldY) = (10, 20);  // 赋值给已声明变量
        int newZ;
        (int _, newZ) = (30, 40); // 单独声明 + 赋值

        // ── C# 10 起: 同一解构语句中可以混合 ────────────────────────
        int x = 0;
        (x, int y) = (10, 20);  // x 赋值（已存在），y 新声明
        Console.WriteLine($"  混合解构: x={x}, y={y}");

        // 更复杂的混合场景
        int a = 100;
        (a, int b, int c) = (1, 2, 3);
        Console.WriteLine($"  三元混合: a={a}, b={b}, c={c}");

        // ── 与元组返回值结合 ─────────────────────────────────────────
        static (int Min, int Max, double Avg) Analyze(int[] nums)
        {
            int sum = 0;
            foreach (var n in nums) sum += n;
            return (nums[0], nums[0], (double)sum / nums.Length); // simplified
        }

        int minVal = 0;  // 已存在
        (minVal, int maxVal, double avgVal) = Analyze(new[] { 3, 7, 2, 9, 1 });
        Console.WriteLine($"  分析结果: min={minVal}, max={maxVal}, avg={avgVal}");

        // ── 与 foreach 解构结合 ──────────────────────────────────────
        var points = new (int X, int Y)[] { (1, 2), (3, 4), (5, 6) };
        int sumX = 0;
        foreach (var (px, py) in points)
            sumX += px;
        Console.WriteLine($"  所有点 X 坐标之和: {sumX}");

        Console.WriteLine();
        Console.WriteLine("  C# 10 前: 解构时声明和赋值必须分两步");
        Console.WriteLine("  C# 10 起: 同一解构语句可混合，代码更紧凑");
    }
}
