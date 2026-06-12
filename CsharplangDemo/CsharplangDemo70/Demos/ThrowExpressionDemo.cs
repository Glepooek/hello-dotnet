using System;
using System.Collections.Generic;

namespace CsharplangDemo70.Demos
{
    public static class ThrowExpressionDemo
    {
        // ── C# 7.0: 抛出表达式（throw expression）────────────────────────
        // C# 7.0 前: throw 只能是语句，不能出现在表达式上下文
        // C# 7.0 起: throw 可以作为表达式，用于条件运算符、空合并等

        // 1. null 合并运算符中抛出
        static string NonNullName(string name)
            => name ?? throw new ArgumentNullException(nameof(name));

        // 2. 条件运算符中抛出
        static int PositiveOrThrow(int value)
            => value > 0 ? value : throw new ArgumentOutOfRangeException(nameof(value), "必须为正数");

        // 3. 属性 setter 中抛出（表达式主体）
        class Config
        {
            private string _host;
            private int _port;

            public string Host
            {
                get => _host;
                set => _host = !string.IsNullOrEmpty(value) ? value
                    : throw new ArgumentException("Host 不能为空");
            }

            public int Port
            {
                get => _port;
                set => _port = value > 0 && value <= 65535 ? value
                    : throw new ArgumentOutOfRangeException(nameof(value), "端口范围 1-65535");
            }

            public Config(string host, int port)
            {
                _host = host;
                _port = port;
            }

            public override string ToString() => _host + ":" + _port;
        }

        // 4. Lambda 中抛出
        static Func<int, int> SafeSqrt = x =>
            x >= 0 ? (int)Math.Sqrt(x) : throw new ArgumentException("不能对负数开方");

        // 5. 字典查找中抛出
        static string GetRequired(Dictionary<string, string> dict, string key)
            => dict.TryGetValue(key, out string val) ? val
               : throw new KeyNotFoundException("必须包含键: " + key);

        public static void Run()
        {
            // null 合并中抛出
            Console.WriteLine("  非空: " + NonNullName("Alice"));
            try { NonNullName(null); }
            catch (ArgumentNullException e) { Console.WriteLine("  null 抛出: " + e.ParamName); }

            // 条件运算符中抛出
            Console.WriteLine("  正数: " + PositiveOrThrow(42));
            try { PositiveOrThrow(-1); }
            catch (ArgumentOutOfRangeException e) { Console.WriteLine("  负数抛出: " + e.ParamName); }

            // 属性 setter 中抛出
            var cfg = new Config("localhost", 8080);
            Console.WriteLine("  Config: " + cfg);
            try { cfg.Port = 99999; }
            catch (ArgumentOutOfRangeException e) { Console.WriteLine("  端口越界: " + e.ParamName); }

            // Lambda 中抛出
            Console.WriteLine("  Sqrt(16) = " + SafeSqrt(16));
            try { SafeSqrt(-1); }
            catch (ArgumentException e) { Console.WriteLine("  负数开方: " + e.Message); }

            // 字典查找中抛出
            var dict = new Dictionary<string, string> { { "key1", "val1" } };
            Console.WriteLine("  GetRequired: " + GetRequired(dict, "key1"));
            try { GetRequired(dict, "missing"); }
            catch (KeyNotFoundException e) { Console.WriteLine("  缺失键: " + e.Message); }

            Console.WriteLine();
            Console.WriteLine("  C# 7.0 前: throw 只能是语句，不能内联在表达式中");
            Console.WriteLine("  C# 7.0 起: throw 可用于 ??、?:、=> 等表达式上下文");
        }
    }
}
