using System;

namespace CsharplangDemo73.Demos
{
    public static class GenericConstraintDemo
    {
        // ── 1. unmanaged 约束: 非托管类型（无引用类型字段的 struct）────
        // 允许对泛型类型进行 sizeof、指针操作等
        static unsafe int SizeOf<T>() where T : unmanaged => sizeof(T);

        static unsafe void PrintBytes<T>(T value) where T : unmanaged
        {
            byte* ptr = (byte*)&value;
            Console.Write("  " + typeof(T).Name + " bytes: ");
            for (int i = 0; i < sizeof(T); i++)
                Console.Write(ptr[i].ToString("X2") + " ");
            Console.WriteLine();
        }

        // unmanaged 泛型 Span 操作
        static unsafe Span<byte> AsBytes<T>(ref T value) where T : unmanaged
        {
            fixed (T* ptr = &value)
                return new Span<byte>((byte*)ptr, sizeof(T));
        }

        // ── 2. Enum 约束: 泛型方法处理任意枚举 ─────────────────────────
        static string GetEnumNames<T>() where T : Enum
            => string.Join(", ", Enum.GetNames(typeof(T)));

        static bool IsValidEnum<T>(int value) where T : Enum
            => Enum.IsDefined(typeof(T), value);

        static T[] GetEnumValues<T>() where T : Enum
            => (T[])Enum.GetValues(typeof(T));

        // ── 3. Delegate 约束: 泛型方法处理任意委托 ──────────────────────
        static TDelegate Combine<TDelegate>(TDelegate a, TDelegate b)
            where TDelegate : Delegate
            => (TDelegate)Delegate.Combine(a, b);

        enum Color { Red, Green, Blue }
        enum Direction { North, South, East, West }

        public static void Run()
        {
            // unmanaged 约束
            Console.WriteLine("  unmanaged sizeof:");
            Console.WriteLine("    sizeof<int>    = " + SizeOf<int>());
            Console.WriteLine("    sizeof<double> = " + SizeOf<double>());
            Console.WriteLine("    sizeof<bool>   = " + SizeOf<bool>());

            Console.WriteLine("  unmanaged PrintBytes:");
            unsafe
            {
                int iv = 0x01020304;
                PrintBytes(iv);
                double dv = 3.14;
                PrintBytes(dv);
            }

            // Enum 约束
            Console.WriteLine("  Enum 约束:");
            Console.WriteLine("    Color:     " + GetEnumNames<Color>());
            Console.WriteLine("    Direction: " + GetEnumNames<Direction>());
            Console.WriteLine("    IsValid Color(1):  " + IsValidEnum<Color>(1));
            Console.WriteLine("    IsValid Color(99): " + IsValidEnum<Color>(99));
            Console.Write("    Color values: ");
            foreach (var v in GetEnumValues<Color>()) Console.Write(v + " ");
            Console.WriteLine();

            // Delegate 约束
            Console.WriteLine("  Delegate 约束:");
            Action<string> log1 = s => Console.WriteLine("    [log1] " + s);
            Action<string> log2 = s => Console.WriteLine("    [log2] " + s);
            Action<string> combined = Combine(log1, log2);
            combined("Combine 调用");

            Func<int, int> double_ = x => x * 2;
            Func<int, int> addTen  = x => x + 10;
            // 注: Func<int,int> 多播委托链式调用返回最后一个结果
            Func<int, int> chain = Combine(double_, addTen);
            Console.WriteLine("    chain(5): double(5)=10, addTen(5)=15, 结果=" + chain(5));
        }
    }
}
