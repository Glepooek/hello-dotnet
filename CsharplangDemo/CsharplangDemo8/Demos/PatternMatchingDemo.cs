using System;

namespace CsharplangDemo8.Demos
{
    public static class PatternMatchingDemo
    {
        // ── 属性模式: 直接匹配对象的属性值 ──────────────────────────────
        static string ClassifyString(object? obj) => obj switch
        {
            string { Length: 0 }         => "空字符串",
            string { Length: 1 }         => "单字符",
            string { Length: var n } s   => $"\"{s[..Math.Min(8, n)]}\" (长度={n})",
            null                         => "null",
            _                            => obj.GetType().Name
        };

        // ── 元组模式: 同时匹配多个值 ─────────────────────────────────────
        static string RockPaperScissors(string a, string b)
            => (a, b) switch
            {
                ("rock",     "scissors") => $"{a} 胜",
                ("scissors", "paper")    => $"{a} 胜",
                ("paper",    "rock")     => $"{a} 胜",
                var (x, y) when x == y  => "平局",
                _                        => $"{b} 胜"
            };

        // 用元组模式表达状态机转换
        enum TrafficLight { Red, Yellow, Green }
        static TrafficLight Next(TrafficLight light) => light switch
        {
            TrafficLight.Red    => TrafficLight.Green,
            TrafficLight.Green  => TrafficLight.Yellow,
            TrafficLight.Yellow => TrafficLight.Red,
            _                   => throw new InvalidOperationException()
        };

        // ── 位置模式: 通过解构匹配 ────────────────────────────────────────
        // C# 8: 主构造函数不可用，使用传统构造函数
        readonly struct Point
        {
            public int X { get; }
            public int Y { get; }
            public Point(int x, int y) { X = x; Y = y; }
            public void Deconstruct(out int x, out int y) { x = X; y = Y; }
        }

        static string Quadrant(Point p)
        {
            // C# 8 位置模式不支持关系子模式 (> 0 等)，用 when 守卫替代
            var (x, y) = p;
            return (x, y) switch
            {
                (0,  0)                        => "原点",
                var (a, b) when a > 0 && b > 0 => "第一象限",
                var (a, b) when a < 0 && b > 0 => "第二象限",
                var (a, b) when a < 0 && b < 0 => "第三象限",
                var (a, b) when a > 0 && b < 0 => "第四象限",
                (0,  _)                        => "Y 轴",
                (_,  0)                        => "X 轴",
                _                              => "未知"
            };
        }

        // ── 嵌套属性模式 ─────────────────────────────────────────────────
        // C# 8: record 不可用，使用普通类
        class Order
        {
            public string  Status { get; }
            public decimal Amount { get; }
            public Order(string status, decimal amount) { Status = status; Amount = amount; }
        }

        static string ProcessOrder(Order o) => o switch
        {
            { Status: "pending" } when o.Amount > 1000 => "大额待处理订单，需要审批",
            { Status: "pending"  }                      => "普通待处理订单",
            { Status: "shipped"  }                      => "已发货",
            { Status: "complete" }                      => "已完成",
            _                                           => $"未知状态: {o.Status}"
        };

        public static void Run()
        {
            // 属性模式
            Console.WriteLine("  属性模式:");
            foreach (object? o in new object?[] { null, "", "a", "hello world" })
                Console.WriteLine($"    {ClassifyString(o)}");

            // 元组模式
            Console.WriteLine("  石头剪刀布:");
            Console.WriteLine($"    rock vs scissors → {RockPaperScissors("rock", "scissors")}");
            Console.WriteLine($"    paper vs paper   → {RockPaperScissors("paper", "paper")}");
            Console.WriteLine($"    scissors vs rock → {RockPaperScissors("scissors", "rock")}");

            // 状态机
            Console.WriteLine("  红绿灯:");
            var light = TrafficLight.Red;
            for (int i = 0; i < 4; i++)
            {
                Console.Write($"    {light} → ");
                light = Next(light);
                Console.WriteLine(light);
            }

            // 位置模式
            Console.WriteLine("  象限:");
            foreach (var p in new[] {
                new Point(0, 0), new Point(3, 4), new Point(-2, 5), new Point(0, 3) })
                Console.WriteLine($"    ({p.X},{p.Y}) → {Quadrant(p)}");

            // 嵌套属性模式
            Console.WriteLine("  订单处理:");
            foreach (var o in new[] {
                new Order("pending", 1500m),
                new Order("pending", 200m),
                new Order("shipped", 0m) })
                Console.WriteLine($"    {o.Status}/{o.Amount} → {ProcessOrder(o)}");
        }
    }
}
