using System;
using System.Collections.Generic;

namespace CsharplangDemo9.Demos
{
    public static class MiscDemo
    {
        // ── 1. nint / nuint: 平台原生大小整数 ────────────────────────────
        static void NativeIntDemo()
        {
            nint  ni = 100;
            nuint nu = 200;
            nint sum     = ni + 50;
            nint product = ni * 3;
            Console.WriteLine($"  nint:  {ni}, sum={sum}, product={product}");
            Console.WriteLine($"  nint.Size = {nint.Size} 字节 (平台相关)");
            Console.WriteLine($"  nuint: {nu}, Max={nuint.MaxValue}");
        }

        // ── 2. 函数指针 delegate*: unsafe, 零分配，比委托更快 ────────────
        static unsafe void FunctionPointerDemo()
        {
            delegate*<int, int, int> add = &Add;
            delegate*<int, int, int> mul = &Multiply;
            Console.WriteLine($"  delegate* add(3,4) = {add(3, 4)}");
            Console.WriteLine($"  delegate* mul(3,4) = {mul(3, 4)}");

            delegate* managed<int, int> square = &Square;
            Console.WriteLine($"  delegate* managed square(5) = {square(5)}");
        }

        static int Add(int a, int b) => a + b;
        static int Multiply(int a, int b) => a * b;
        static int Square(int x) => x * x;

        // ── 3. Lambda 弃元参数 _ ──────────────────────────────────────────
        static void DiscardParamDemo()
        {
            var items = new List<string> { "alpha", "beta", "gamma" };

            items.ForEach(_ => Console.Write("• "));
            Console.WriteLine();

            Func<int, int, int> ignoreSecond = (x, _) => x * 2;
            Console.WriteLine($"  ignoreSecond(5, 99) = {ignoreSecond(5, 99)}");

            Action<object?, EventArgs> handler = (_, _) =>
                Console.WriteLine("  事件触发 (参数已忽略)");
            handler(null, EventArgs.Empty);
        }

        // ── 4. 本地函数可添加特性 ─────────────────────────────────────────
        static void LocalFunctionAttributeDemo()
        {
            [Obsolete("仅供演示")]
            static int OldCompute(int x) => x * 2;

            [System.Diagnostics.Conditional("DEBUG")]
            static void DebugLog(string msg) => Console.WriteLine($"  DEBUG: {msg}");

#pragma warning disable CS0618
            Console.WriteLine($"  OldCompute(5) = {OldCompute(5)}");
#pragma warning restore CS0618
            DebugLog("本地函数特性演示");
        }

        // ── 5. 扩展 GetEnumerator 支持 foreach ───────────────────────────
        static void ExtendedGetEnumeratorDemo()
        {
            var range = new NumberRange(1, 5);
            Console.Write("  foreach NumberRange: ");
            foreach (var n in range)
                Console.Write($"{n} ");
            Console.WriteLine();
        }

        public static void Run()
        {
            Console.WriteLine("  nint/nuint:");
            NativeIntDemo();

            Console.WriteLine("  函数指针 delegate*:");
            FunctionPointerDemo();

            Console.WriteLine("  Lambda 弃元参数 _:");
            DiscardParamDemo();

            Console.WriteLine("  本地函数特性:");
            LocalFunctionAttributeDemo();

            Console.WriteLine("  扩展 GetEnumerator:");
            ExtendedGetEnumeratorDemo();
        }
    }

    // GetEnumerator 扩展方法演示
    public class NumberRange
    {
        public int Start { get; }
        public int End   { get; }
        public NumberRange(int start, int end) { Start = start; End = end; }
    }

    public static class NumberRangeExtensions
    {
        public static RangeEnumerator GetEnumerator(this NumberRange range) =>
            new RangeEnumerator(range.Start, range.End);
    }

    public struct RangeEnumerator
    {
        private int _current;
        private readonly int _end;
        public RangeEnumerator(int start, int end) { _current = start - 1; _end = end; }
        public int Current => _current;
        public bool MoveNext() => ++_current <= _end;
    }
}
