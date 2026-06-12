using System;

namespace CsharplangDemo70.Demos
{
    public static class LiteralDemo
    {
        public static void Run()
        {
            // ── C# 7.0: 二进制文本（0b 前缀）────────────────────────────
            int flags   = 0b1010_1010;
            int allBits = 0b1111_1111;
            int mask    = 0b0000_1111;

            Console.WriteLine("  二进制字面量:");
            Console.WriteLine("    0b1010_1010 = " + flags + " (0x" + flags.ToString("X2") + ")");
            Console.WriteLine("    0b1111_1111 = " + allBits);
            Console.WriteLine("    flags & mask = " + (flags & mask));

            // ── 数字分隔符（_ 中间分隔，提高可读性）────────────────────
            long population   = 1_400_000_000L;   // 14亿
            double lightSpeed = 299_792_458.0;     // 光速
            decimal price     = 1_299.99m;
            int hexColor      = 0xFF_EC_D8;

            Console.WriteLine("  数字分隔符:");
            Console.WriteLine("    人口:   " + population);
            Console.WriteLine("    光速:   " + lightSpeed + " m/s");
            Console.WriteLine("    价格:   " + price);
            Console.WriteLine("    颜色:   0x" + hexColor.ToString("X6"));

            // ── 二进制 + 分隔符组合 ────────────────────────────────────
            // Unix 权限位: rwxr-xr-x = 755
            int permission = 0b111_101_101;
            Console.WriteLine("  权限 rwxr-xr-x: " + Convert.ToString(permission, 8) + " (八进制)");

            // CPU 指令操作码演示
            byte opcode = 0b1000_0001;  // ADD r/m64, r64
            Console.WriteLine("  指令码: 0b" + Convert.ToString(opcode, 2).PadLeft(8, '0') +
                              " = 0x" + opcode.ToString("X2"));

            Console.WriteLine();
            Console.WriteLine("  _ 分隔符规则: 只能在数字之间，不能在首尾或前缀之后");
            Console.WriteLine("  (前缀后 _ 是 C# 7.3 的前导下划线特性)");
        }
    }
}
