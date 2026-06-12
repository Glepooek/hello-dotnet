using System;

namespace CsharplangDemo70.Demos
{
    public static class DiscardDemo
    {
        static (int X, int Y, int Z) GetCoordinates() => (1, 2, 3);
        static bool TryProcess(string input, out int result, out string error)
        {
            if (int.TryParse(input, out result)) { error = null; return true; }
            result = 0; error = "无效输入: " + input; return false;
        }

        public static void Run()
        {
            // ── C# 7.0: 弃元 _ ────────────────────────────────────────────
            // 用 _ 明确表示"我知道有这个值，但我不需要它"

            // 1. 解构时忽略部分字段
            var (x, _, z) = GetCoordinates();
            Console.WriteLine("  解构忽略 Y: x=" + x + ", z=" + z);

            // 2. out 参数中忽略不需要的输出
            if (TryProcess("42", out int value, out _))  // 忽略 error
                Console.WriteLine("  out _: value=" + value);

            if (!TryProcess("abc", out _, out string err))  // 忽略 result
                Console.WriteLine("  out _: error=" + err);

            // 3. 模式匹配中忽略
            object obj = "hello world";
            if (obj is string _)  // 只关心类型，不关心值
                Console.WriteLine("  is string _: 是字符串");

            // switch 中的弃元
            int code = 404;
            switch (code)
            {
                case 200: Console.WriteLine("  200 OK"); break;
                case 404: Console.WriteLine("  404 Not Found"); break;
                default:
                    _ = code;  // 明确忽略，也可用于消除"未使用变量"警告
                    Console.WriteLine("  其他状态码");
                    break;
            }

            // 4. 独立弃元（抑制"必须处理返回值"的场景）
            _ = int.TryParse("not-a-number", out _);  // 不关心成功/失败

            Console.WriteLine();
            Console.WriteLine("  弃元 _ 的价值:");
            Console.WriteLine("    • 明确表达意图: 此处我知道有值但不需要");
            Console.WriteLine("    • 避免无意义的临时变量命名");
            Console.WriteLine("    • 解构、out 参数、模式匹配均适用");
        }
    }
}
