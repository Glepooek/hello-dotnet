using System;
using System.Collections.Generic;

namespace CsharplangDemo60.Demos
{
    public static class IndexInitializerDemo
    {
        // ── C# 6.0: 索引初始化器 ─────────────────────────────────────────
        // C# 6.0 前: Dictionary 初始化只能用 { { key, value } } 写法
        // C# 6.0 起: 可以用 { [key] = value } 的索引语法，更直观

        public static void Run()
        {
            // ── C# 6.0 前: 集合初始化器写法 ──────────────────────────────
            var oldDict = new Dictionary<string, int>
            {
                { "one",   1 },
                { "two",   2 },
                { "three", 3 }
            };
            Console.WriteLine("  旧写法: one=" + oldDict["one"]);

            // ── C# 6.0 起: 索引初始化器写法 ──────────────────────────────
            var newDict = new Dictionary<string, int>
            {
                ["one"]   = 1,
                ["two"]   = 2,
                ["three"] = 3
            };
            Console.WriteLine("  新写法: two=" + newDict["two"]);

            // ── 更复杂的键类型 ────────────────────────────────────────────
            var httpStatus = new Dictionary<int, string>
            {
                [200] = "OK",
                [201] = "Created",
                [400] = "Bad Request",
                [401] = "Unauthorized",
                [403] = "Forbidden",
                [404] = "Not Found",
                [500] = "Internal Server Error"
            };

            Console.WriteLine("  HTTP 状态码:");
            foreach (var code in new[] { 200, 404, 500 })
                Console.WriteLine("    " + code + " = " + httpStatus[code]);

            // ── 自定义索引器也支持 ────────────────────────────────────────
            var grid = new SparseGrid
            {
                [0, 0] = "A",
                [1, 1] = "B",
                [2, 0] = "C"
            };
            Console.WriteLine("  SparseGrid[0,0] = " + grid[0, 0]);
            Console.WriteLine("  SparseGrid[1,1] = " + grid[1, 1]);

            Console.WriteLine();
            Console.WriteLine("  索引初始化器 vs 集合初始化器:");
            Console.WriteLine("    旧: { { key, value } }  — 调用 Add(key, value)");
            Console.WriteLine("    新: { [key] = value }   — 调用索引器 set");
        }

        // 自定义索引器类
        class SparseGrid
        {
            private Dictionary<string, string> _cells = new Dictionary<string, string>();

            public string this[int row, int col]
            {
                get
                {
                    string key = row + "," + col;
                    string result;
                    return _cells.TryGetValue(key, out result) ? result : ".";
                }
                set { _cells[row + "," + col] = value; }
            }
        }
    }
}
