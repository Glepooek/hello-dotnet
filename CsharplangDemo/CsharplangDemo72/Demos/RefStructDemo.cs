using System;

namespace CsharplangDemo72.Demos
{
    // ── ref struct: 只能在栈上存活的结构体 ──────────────────────────────
    // 限制: 不能装箱、不能成为类的字段、不能用作泛型参数、
    //       不能在 async 方法/lambda 中用作局部变量
    // 用途: 安全地包装栈内存指针（Span<T> 就是 ref struct）

    ref struct FixedBuffer
    {
        private Span<int> _data;
        private int _count;

        public FixedBuffer(Span<int> storage) { _data = storage; _count = 0; }

        public bool TryAdd(int value)
        {
            if (_count >= _data.Length) return false;
            _data[_count++] = value;
            return true;
        }

        public int Count => _count;

        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                    throw new IndexOutOfRangeException();
                return _data[index];
            }
        }

        public void Print()
        {
            Console.Write("  FixedBuffer[" + _count + "]: ");
            for (int i = 0; i < _count; i++)
                Console.Write(_data[i] + " ");
            Console.WriteLine();
        }
    }

    // ref struct 也可以有自定义 Dispose（C# 8 的 using 声明依赖此）
    ref struct RentedBuffer
    {
        private Span<byte> _span;
        private bool _disposed;

        public RentedBuffer(int size)
        {
            // 演示: 实际场景中可从 ArrayPool 租借
            _span = new byte[size];
            _disposed = false;
            Console.WriteLine("  RentedBuffer 创建，大小=" + size);
        }

        public Span<byte> Span => _span;

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                Console.WriteLine("  RentedBuffer 归还");
            }
        }
    }

    public static class RefStructDemo
    {
        public static void Run()
        {
            // ── 使用 FixedBuffer（ref struct）──────────────────────────
            Span<int> storage = stackalloc int[8];
            var buf = new FixedBuffer(storage);

            buf.TryAdd(10);
            buf.TryAdd(20);
            buf.TryAdd(30);
            buf.Print();
            Console.WriteLine("  buf[1] = " + buf[1]);

            // ref struct 不能装箱
            // object boxed = buf;  // 编译错误

            // ref struct 不能作为类字段
            // class Holder { FixedBuffer _buf; }  // 编译错误

            // ── Span<T> 就是 ref struct ────────────────────────────────
            Span<int> span = stackalloc int[5];
            span[0] = 5; span[1] = 4; span[2] = 3; span[3] = 2; span[4] = 1;
            span.Sort();
            Console.Write("  Span<int> 排序: ");
            foreach (var n in span) Console.Write(n + " ");
            Console.WriteLine();

            // ── 带 Dispose 的 ref struct ──────────────────────────────
            var rented = new RentedBuffer(16);
            rented.Span[0] = 0xAB;
            rented.Span[1] = 0xCD;
            Console.WriteLine("  rented[0]=0x" + rented.Span[0].ToString("X2") +
                              " rented[1]=0x" + rented.Span[1].ToString("X2"));
            rented.Dispose();

            Console.WriteLine();
            Console.WriteLine("  ref struct 的核心约束:");
            Console.WriteLine("    • 只能在栈上存活（不能装箱/不能成为类字段）");
            Console.WriteLine("    • 内部可以安全持有栈内存指针");
            Console.WriteLine("    • Span<T>/ReadOnlySpan<T> 的语言基础");
        }
    }
}
