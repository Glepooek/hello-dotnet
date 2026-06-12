using System;
using System.Collections.Generic;
using System.Text;

namespace CsharplangDemo9.Demos
{
    public static class TargetTypedNewDemo
    {
        class HttpClient2
        {
            public string BaseUrl { get; init; } = "";
            public HttpClient2(string url) { BaseUrl = url; }
            public HttpClient2() { }
        }

        public static void Run()
        {
            // ── C# 9 前: new 后必须重复写类型名 ─────────────────────────
            Dictionary<string, List<int>> old =
                new Dictionary<string, List<int>>();

            // ── C# 9 起: 目标类型可推断时省略类型名 ──────────────────────
            Dictionary<string, List<int>> dict = new();
            List<string>   names  = new() { "Alice", "Bob", "Carol" };
            StringBuilder  sb     = new();
            HttpClient2    client = new("https://api.example.com");

            sb.Append("target-typed new");
            Console.WriteLine($"  dict 类型: {dict.GetType().Name}");
            Console.WriteLine($"  names: [{string.Join(", ", names)}]");
            Console.WriteLine($"  sb:    {sb}");
            Console.WriteLine($"  client BaseUrl: {client.BaseUrl}");

            Console.WriteLine("  嵌套 new():");
            var map = new Dictionary<string, List<int>>
            {
                ["a"] = new() { 1, 2, 3 },
                ["b"] = new() { 4, 5, 6 }
            };
            foreach (var kv in map)
                Console.WriteLine($"    {kv.Key}: [{string.Join(",", kv.Value)}]");

            static void Process(List<int> items)
                => Console.WriteLine($"  Process: {items.Count} 个元素");

            Process(new() { 10, 20, 30 });

            // ── 目标类型条件表达式 (C# 9) ─────────────────────────────────
            bool useLocal = true;
            HttpClient2 c = useLocal
                ? new("localhost")
                : new("https://prod.server.com");
            Console.WriteLine($"  条件 new: {c.BaseUrl}");

            Console.WriteLine();
            Console.WriteLine("  目标类型 new 消除了类型名的重复书写");
            Console.WriteLine("  特别适合泛型集合、长类型名场景");
        }
    }
}
