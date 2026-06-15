using System;

namespace CsharplangDemo73.Demos
{
    // ── C# 7.3: fixed 支持任意实现了 GetPinnableReference() 的类型 ──────
    // C# 7.3 之前: fixed 只能用于内置支持的类型（数组、字符串等）
    // C# 7.3 起: 任意类型只要有 GetPinnableReference() 方法即可

    // 自定义缓冲区类型
    public unsafe class PinnableBuffer
    {
        private readonly int[] _data;

        public PinnableBuffer(params int[] data) { _data = data; }

        // C# 7.3: 实现此方法使类型支持 fixed 语句
        public ref int GetPinnableReference() => ref _data[0];

        public int Length => _data.Length;
    }

    public static class FixedPatternDemo
    {
        public static unsafe void Run()
        {
            var buffer = new PinnableBuffer(10, 20, 30, 40, 50);

            // C# 7.3: 自定义类型可以直接用 fixed 固定
            fixed (int* ptr = buffer)
            {
                Console.Write("  fixed 自定义类型: ");
                for (int i = 0; i < buffer.Length; i++)
                    Console.Write(*(ptr + i) + " ");
                Console.WriteLine();

                // 通过指针修改数据
                *ptr = 999;
            }

            // 修改已生效（fixed 块结束后内存已解除固定，但修改保留）
            fixed (int* ptr = buffer)
                Console.WriteLine("  修改 ptr[0]=999: " + *ptr);

            // ── 对比: 字符串和数组的传统 fixed ──────────────────────
            string s = "Hello";
            fixed (char* cp = s)
                Console.WriteLine("  fixed string 首字符: " + *cp);

            int[] arr = { 1, 2, 3 };
            fixed (int* ap = arr)
                Console.WriteLine("  fixed array[0]: " + *ap);

            Console.WriteLine();
            Console.WriteLine("  GetPinnableReference() 让任意类型支持 fixed");
            Console.WriteLine("  Span<T>/Memory<T> 也基于此模式实现 fixed 支持");
        }
    }
}
