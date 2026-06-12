using System;

namespace CsharplangDemo60.Demos
{
    public static class StringInterpolationDemo
    {
        class Product
        {
            public string Name  { get; set; }
            public decimal Price { get; set; }
            public int Stock { get; set; }
            public DateTime Expiry { get; set; }
        }

        public static void Run()
        {
            // ── C# 6.0 前: string.Format 或字符串拼接 ────────────────────
            string name = "Alice";
            int age = 30;
            double score = 95.678;
            string oldWay = string.Format("用户: {0}, 年龄: {1}, 分数: {2:F2}", name, age, score);
            Console.WriteLine("  旧 Format: " + oldWay);

            // ── C# 6.0 起: $"" 字符串内插 ────────────────────────────────
            string newWay = $"用户: {name}, 年龄: {age}, 分数: {score:F2}";
            Console.WriteLine("  $\"\"内插: " + newWay);

            // ── 格式化说明符 ──────────────────────────────────────────────
            decimal price = 1299.99m;
            Console.WriteLine($"  货币: {price:C}");
            Console.WriteLine($"  固定2位: {score:F2}");
            Console.WriteLine($"  百分比: {0.856:P1}");
            Console.WriteLine($"  16进制: {255:X4}");
            Console.WriteLine($"  对齐右10: '{name,10}'");
            Console.WriteLine($"  对齐左10: '{name,-10}'");

            // ── 表达式 ────────────────────────────────────────────────────
            int x = 5, y = 3;
            Console.WriteLine($"  表达式: {x} + {y} = {x + y}");
            Console.WriteLine($"  条件: {(age >= 18 ? "成年" : "未成年")}");
            Console.WriteLine($"  方法: {name.ToUpper()}");
            Console.WriteLine($"  长度: {name.Length}");

            // ── 多行（C# 6.0 不支持换行，用 + 拼接）────────────────────
            var product = new Product
            {
                Name = "MacBook Pro",
                Price = 15999m,
                Stock = 5,
                Expiry = new DateTime(2025, 12, 31)
            };

            string report = $"商品: {product.Name}\n" +
                            $"价格: {product.Price:C}\n" +
                            $"库存: {product.Stock} 件\n" +
                            $"有效期至: {product.Expiry:yyyy-MM-dd}";
            Console.WriteLine("  商品报告:\n" + report);

            // ── 嵌套内插 ─────────────────────────────────────────────────
            string label = $"[{(product.Stock > 0 ? "有货" : "缺货")}] {product.Name}";
            Console.WriteLine($"  标签: {label}");

            // ── verbatim 内插字符串 $@"" ──────────────────────────────────
            string path = @"C:\Users\Alice";
            Console.WriteLine($"  路径: {path}");

            Console.WriteLine();
            Console.WriteLine("  $\"\" 本质是编译器糖衣，底层调用 string.Format 或 interpolation handler");
        }
    }
}
