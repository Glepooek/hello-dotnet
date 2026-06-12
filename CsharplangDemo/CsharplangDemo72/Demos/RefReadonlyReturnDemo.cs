using System;

namespace CsharplangDemo72.Demos
{
    // ── ref readonly 返回值: 零拷贝返回，调用方不可修改 ──────────────────
    // C# 7.0 引入 ref return（可读写引用返回）
    // C# 7.2 新增 ref readonly return（只读引用返回）

    readonly struct LargeData
    {
        public readonly double A, B, C, D, E, F, G, H;  // 64 字节

        public LargeData(double seed)
        {
            A = seed; B = seed * 2; C = seed * 3; D = seed * 4;
            E = seed * 5; F = seed * 6; G = seed * 7; H = seed * 8;
        }

        public double Sum() => A + B + C + D + E + F + G + H;
        public override string ToString() => "LargeData(A=" + A + ", Sum=" + Sum() + ")";
    }

    class DataRepository
    {
        private LargeData[] _items;

        public DataRepository()
        {
            _items = new LargeData[]
            {
                new LargeData(1.0),
                new LargeData(2.0),
                new LargeData(3.0)
            };
        }

        // ref readonly return: 零拷贝返回，但调用方不能修改
        public ref readonly LargeData GetItem(int index) => ref _items[index];

        // 对比: 普通返回 —— 复制整个 LargeData（64字节）
        public LargeData GetItemCopy(int index) => _items[index];

        // 对比: ref return —— 可读写引用（调用方可修改原始数据）
        public ref LargeData GetItemRef(int index) => ref _items[index];
    }

    public static class RefReadonlyReturnDemo
    {
        public static void Run()
        {
            var repo = new DataRepository();

            // ── ref readonly: 零拷贝，只读 ───────────────────────────
            ref readonly LargeData item0 = ref repo.GetItem(0);
            Console.WriteLine("  ref readonly item[0]: " + item0.ToString());

            // 不能修改
            // item0.A = 999;  // 编译错误: ref readonly 不可修改

            // ── 普通返回: 每次调用都复制 64 字节 ─────────────────────
            LargeData copy = repo.GetItemCopy(1);
            Console.WriteLine("  copy item[1]: " + copy.ToString());

            // ── ref return: 可修改原始数据 ────────────────────────────
            ref LargeData mutable = ref repo.GetItemRef(2);
            Console.WriteLine("  ref item[2] 修改前: " + mutable.ToString());
            mutable = new LargeData(99.0);  // 替换整个元素（readonly 字段不可单独修改）
            Console.WriteLine("  ref item[2] 修改后: " + repo.GetItem(2).ToString());

            // ── ref readonly 局部变量 ─────────────────────────────────
            // 也可以不声明为 ref 局部变量，直接使用（会复制一次）
            LargeData directCopy = repo.GetItem(0);  // 此处产生一次复制
            Console.WriteLine("  直接赋值（复制）: A=" + directCopy.A);

            Console.WriteLine();
            Console.WriteLine("  三种返回方式对比:");
            Console.WriteLine("    return T:           每次调用复制整个值");
            Console.WriteLine("    ref return T:       零拷贝引用，调用方可修改");
            Console.WriteLine("    ref readonly T:     零拷贝引用，调用方只读");
        }
    }
}
