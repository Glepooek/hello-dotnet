namespace CsharplangDemo11.Demos;

public static class ListPatternDemo
{
    public static void Run()
    {
        // ── 基本列表模式 ──────────────────────────────────────────────
        int[] empty  = new int[] { };
        int[] one    = new int[] { 1 };
        int[] triple = new int[] { 1, 2, 3 };
        int[] data   = new int[] { 1, 2, 3, 4, 5 };

        Console.WriteLine($"  [] 匹配空数组:     {empty  is []}");
        Console.WriteLine($"  [1] 匹配单元素:    {one    is [1]}");
        Console.WriteLine($"  [1,2,3] 精确匹配:  {triple is [1, 2, 3]}");
        Console.WriteLine($"  [1,2,3] 匹配 data: {data   is [1, 2, 3]}");  // false

        // ── 切片模式 (..) ─────────────────────────────────────────────
        Console.WriteLine($"  以 1 开头:         {data is [1, ..]}");
        Console.WriteLine($"  以 5 结尾:         {data is [.., 5]}");
        Console.WriteLine($"  首尾匹配:          {data is [1, .., 5]}");
        Console.WriteLine($"  第二个是 2:        {data is [_, 2, ..]}");

        // ── 切片捕获变量 ─────────────────────────────────────────────
        if (data is [var first, .. var middle, var last])
        {
            Console.WriteLine($"  first={first}, last={last}, middle=[{string.Join(",", middle)}]");
        }

        // ── switch 表达式中的列表模式 ─────────────────────────────────
        static string Describe(int[] arr) => arr switch
        {
            []           => "空",
            [_]          => "单元素",
            [var a, var b] => $"两个元素: {a}, {b}",
            [1, 2, ..]   => "以 1,2 开头",
            [.., 0]      => "以 0 结尾",
            _            => $"其他 ({arr.Length} 个元素)"
        };

        Console.WriteLine($"  switch []:      {Describe(new int[]{})}");
        Console.WriteLine($"  switch [42]:    {Describe(new int[]{42})}");
        Console.WriteLine($"  switch [3,7]:   {Describe(new int[]{3, 7})}");
        Console.WriteLine($"  switch [1,2,3]: {Describe(new int[]{1, 2, 3})}");
        Console.WriteLine($"  switch [9,0]:   {Describe(new int[]{9, 0})}");
        Console.WriteLine($"  switch [1..5]:  {Describe(data)}");

        // ── 嵌套模式 ─────────────────────────────────────────────────
        int[][] matrix = new int[][] {
            new int[] { 1, 0 },
            new int[] { 0, 1 }
        };
        bool isIdentity = matrix is [[1, 0], [0, 1]];
        Console.WriteLine($"  单位矩阵验证:   {isIdentity}");

        // ── 字符串列表模式 (span 模式匹配) ────────────────────────────
        string command = "git commit -m";
        string[] parts = command.Split(' ');
        if (parts is ["git", var subCmd, ..])
            Console.WriteLine($"  git 子命令: {subCmd}");
    }
}
