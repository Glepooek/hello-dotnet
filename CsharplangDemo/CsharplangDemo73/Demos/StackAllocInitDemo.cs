using System;

namespace CsharplangDemo73.Demos
{
    public static class StackAllocInitDemo
    {
        public static void Run()
        {
            // ── C# 7.3 前: stackalloc 不支持初始化器，必须逐个赋值 ────
            unsafe
            {
                int* old = stackalloc int[3];
                old[0] = 10; old[1] = 20; old[2] = 30;
                Console.WriteLine("  旧写法 (unsafe): " + old[0] + ", " + old[1] + ", " + old[2]);
            }

            // ── C# 7.3 起: stackalloc 支持初始化器 ────────────────────

            // 1. 配合 Span<T>（安全代码，无需 unsafe）
            Span<int> nums = stackalloc int[] { 10, 20, 30, 40, 50 };
            Console.Write("  Span<int> stackalloc: ");
            foreach (var n in nums) Console.Write(n + " ");
            Console.WriteLine();

            // 2. 零初始化
            Span<int> zeros = stackalloc int[5];
            Console.Write("  stackalloc int[5] 默认零: ");
            foreach (var n in zeros) Console.Write(n + " ");
            Console.WriteLine();

            // 3. 字节缓冲区（网络/IO 常见场景）
            Span<byte> header = stackalloc byte[] { 0xFF, 0xFE, 0x00, 0x01 };
            Console.Write("  HTTP header bytes: ");
            foreach (var b in header) Console.Write("0x" + b.ToString("X2") + " ");
            Console.WriteLine();

            // 4. 栈上字符缓冲区
            Span<char> buf = stackalloc char[16];
            "Hello".AsSpan().CopyTo(buf);
            Console.WriteLine("  char buffer: " + buf.Slice(0, 5).ToString());

            // 5. unsafe 中的初始化器
            unsafe
            {
                int* p = stackalloc int[] { 100, 200, 300 };
                Console.WriteLine("  unsafe 初始化: " + p[0] + ", " + p[1] + ", " + p[2]);
            }

            Console.WriteLine();
            Console.WriteLine("  stackalloc + Span<T>: 安全代码实现零堆分配的栈缓冲");
        }
    }
}
