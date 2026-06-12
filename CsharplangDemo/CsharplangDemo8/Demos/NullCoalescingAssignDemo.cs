using System;
using System.Collections.Generic;

namespace CsharplangDemo8.Demos
{
    public static class NullCoalescingAssignDemo
    {
        public static void Run()
        {
            // ── 基本用法: 仅当左侧为 null 时赋值 ──────────────────────
            string? name = null;

            // C# 8 前的写法
            if (name == null) name = "Guest";
            Console.WriteLine($"  旧写法 if-null: {name}");

            name = null;
            name = name ?? "Guest2";
            Console.WriteLine($"  旧写法 ??: {name}");

            // C# 8 起: ??= 合并赋值
            name = null;
            name ??= "Guest3";
            Console.WriteLine($"  ??= 写法: {name}");

            // 已有值时不赋值
            name = "Alice";
            name ??= "Guest";  // 不执行赋值
            Console.WriteLine($"  已有值时 ??= : {name}");

            // ── 链式 ??= ──────────────────────────────────────────────
            string? a = null;
            string? b = null;
            string? c = "found";

            a ??= b ?? c;   // a = b ?? c = c = "found"
            Console.WriteLine($"  链式: a = {a}");

            // ── 惰性初始化场景 ───────────────────────────────────────
            Console.WriteLine("  惰性初始化:");
            var cache = new LazyCache();
            Console.WriteLine($"  首次获取: {cache.GetData()}");
            Console.WriteLine($"  再次获取: {cache.GetData()}");

            // ── 集合的惰性初始化 ─────────────────────────────────────
            List<string>? items = null;
            items ??= new List<string>();
            items.Add("a");
            items.Add("b");
            Console.WriteLine($"  集合: [{string.Join(", ", items)}]");

            // ── 与属性结合 ────────────────────────────────────────────
            var config = new Config();
            config.Title ??= "默认标题";
            Console.WriteLine($"  Config.Title: {config.Title}");
            config.Title ??= "不会覆盖";
            Console.WriteLine($"  Config.Title (不变): {config.Title}");
        }

        class LazyCache
        {
            private string? _data;

            public string GetData()
            {
                _data ??= ComputeExpensive();  // 只在首次调用时计算
                return _data;
            }

            private static string ComputeExpensive()
            {
                Console.Write("    (计算中...) ");
                return "expensive-result";
            }
        }

        class Config
        {
            public string? Title { get; set; }
        }
    }
}
