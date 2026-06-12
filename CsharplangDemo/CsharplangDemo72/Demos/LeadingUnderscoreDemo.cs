using System;

namespace CsharplangDemo72.Demos
{
    public static class LeadingUnderscoreDemo
    {
        public static void Run()
        {
            // ── C# 7.0 引入数字分隔符 _，C# 7.2 允许前导下划线 ──────────
            // C# 7.0: _ 可在数字中间，但不能紧接在前缀（0b/0x）之后
            // C# 7.2: _ 可以紧接前缀之后（前导下划线）

            // 二进制字面量: 0b_ 前导下划线
            int flags8     = 0b_1010_1010;        // C# 7.2: 前缀后立即 _
            int allOnes    = 0b_1111_1111;
            int pattern    = 0b_1010_0101;

            Console.WriteLine("  二进制字面量:");
            Console.WriteLine("    0b_1010_1010 = " + flags8 + " (0x" + flags8.ToString("X2") + ")");
            Console.WriteLine("    0b_1111_1111 = " + allOnes);
            Console.WriteLine("    0b_1010_0101 = " + pattern);

            // 十六进制字面量: 0x_ 前导下划线
            int color      = 0x_FF_EC_D8;         // RGB 颜色
            long bigHex    = 0x_DEAD_BEEF_CAFE;
            uint ipAddr    = 0x_C0_A8_01_01;      // 192.168.1.1

            Console.WriteLine("  十六进制字面量:");
            Console.WriteLine("    0x_FF_EC_D8   = " + color + " (颜色)");
            Console.WriteLine("    0x_DEAD_BEEF_CAFE = " + bigHex.ToString("X"));
            Console.WriteLine("    0x_C0_A8_01_01 = " + ipAddr + " (IP地址)");

            // 普通十进制（前导 _ 在十进制数字前是语法错误，不是 C# 特性）
            // 十进制分隔符只能在数字之间
            long million   = 1_000_000;
            long billion   = 1_000_000_000L;
            double pi      = 3.141_592_653;

            Console.WriteLine("  十进制分隔符（中间 _）:");
            Console.WriteLine("    1_000_000   = " + million);
            Console.WriteLine("    1_000_000_000 = " + billion);
            Console.WriteLine("    3.141_592_653 = " + pi);

            // 实用场景: 网络掩码、权限位、魔数
            uint netmask   = 0xFF_FF_FF_00U;      // 255.255.255.0
            int permissions = 0b_111_101_101;      // rwxr-xr-x (Unix 权限)
            long fileHeader = 0x_89_50_4E_47;      // PNG 文件头

            Console.WriteLine("  实用场景:");
            Console.WriteLine("    网络掩码: 0x" + netmask.ToString("X8") + " = " + netmask);
            Console.WriteLine("    Unix权限: 0b" + Convert.ToString(permissions, 2).PadLeft(9, '0'));
            Console.WriteLine("    PNG魔数:  0x" + fileHeader.ToString("X8"));

            Console.WriteLine();
            Console.WriteLine("  前导下划线规则:");
            Console.WriteLine("    • 0b_ 和 0x_ 中 _ 紧接前缀（C# 7.2 新增）");
            Console.WriteLine("    • _ 不能放在最前（不能 _123）也不能在最后（不能 123_）");
            Console.WriteLine("    • 纯视觉辅助，不影响数值本身");
        }
    }
}
