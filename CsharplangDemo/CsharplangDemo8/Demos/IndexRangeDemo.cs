using System;

namespace CsharplangDemo8.Demos
{
    public static class IndexRangeDemo
    {
        public static void Run()
        {
            int[] arr = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            string[] words = { "zero", "one", "two", "three", "four", "five" };

            // ── ^ 从末端索引运算符 (Index) ─────────────────────────────
            Console.WriteLine("  ^ 从末端索引:");
            Console.WriteLine($"    arr[^1] = {arr[^1]}");   // 9
            Console.WriteLine($"    arr[^2] = {arr[^2]}");   // 8
            Console.WriteLine($"    arr[^3] = {arr[^3]}");   // 7

            // ── .. 范围运算符 (Range) ──────────────────────────────────
            Console.WriteLine("  .. 范围切片:");
            int[] s1 = arr[2..5];   // index 2,3,4
            int[] s2 = arr[..3];    // 前3个: 0,1,2
            int[] s3 = arr[7..];    // 从7到末尾: 7,8,9
            int[] s4 = arr[^3..];   // 末尾3个: 7,8,9
            int[] s5 = arr[1..^1];  // 去掉首尾: 1..8

            Console.WriteLine($"    arr[2..5]  = [{string.Join(",", s1)}]");
            Console.WriteLine($"    arr[..3]   = [{string.Join(",", s2)}]");
            Console.WriteLine($"    arr[7..]   = [{string.Join(",", s3)}]");
            Console.WriteLine($"    arr[^3..]  = [{string.Join(",", s4)}]");
            Console.WriteLine($"    arr[1..^1] = [{string.Join(",", s5)}]");

            // ── 字符串切片 ────────────────────────────────────────────
            Console.WriteLine("  字符串切片:");
            string text = "Hello, World!";
            Console.WriteLine($"    \"{text}\"[^6..] = \"{text[^6..]}\"");  // "World!"
            Console.WriteLine($"    \"{text}\"[..5]  = \"{text[..5]}\"");   // "Hello"
            Console.WriteLine($"    \"{text}\"[7..12] = \"{text[7..12]}\""); // "World"

            // ── 保存 Index/Range 到变量 ───────────────────────────────
            Console.WriteLine("  Index/Range 变量:");
            Index lastIndex = ^1;
            Range middleRange = 1..^1;
            Console.WriteLine($"    lastIndex:   arr[^1]   = {arr[lastIndex]}");
            Console.WriteLine($"    middleRange: arr[1..^1]= [{string.Join(",", arr[middleRange])}]");

            // ── 与字符串 words 结合 ───────────────────────────────────
            Console.WriteLine("  words 切片:");
            string[] last3 = words[^3..];
            Console.WriteLine($"    words[^3..] = [{string.Join(", ", last3)}]");
        }
    }
}
