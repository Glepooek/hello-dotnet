using System;
using System.Collections.Generic;

namespace CsharplangDemo60.Demos
{
    public static class NullPropagatorDemo
    {
        class Address
        {
            public string City    { get; set; }
            public string Country { get; set; }
            public string ZipCode { get; set; }
        }

        class Person
        {
            public string  Name    { get; set; }
            public Address Address { get; set; }
            public List<string> Phones { get; set; }
        }

        class Company
        {
            public string Name { get; set; }
            public Person CEO  { get; set; }
        }

        public static void Run()
        {
            // ── C# 6.0 前: 层层 null 检查 ───────────────────────────────
            Company company = new Company { Name = "Acme", CEO = null };

            string oldCity = null;
            if (company != null && company.CEO != null && company.CEO.Address != null)
                oldCity = company.CEO.Address.City;
            Console.WriteLine("  旧写法: " + (oldCity ?? "null"));

            // ── C# 6.0 起: ?. 空传播器 ────────────────────────────────
            string city = company?.CEO?.Address?.City;
            Console.WriteLine("  ?.链式: " + (city ?? "null"));

            // 有值时正常访问
            company.CEO = new Person
            {
                Name = "Alice",
                Address = new Address { City = "Beijing", Country = "China" }
            };
            city = company?.CEO?.Address?.City;
            Console.WriteLine("  有值时: " + city);

            // ── ?. 配合方法调用 ──────────────────────────────────────
            int? nameLen = company?.CEO?.Name?.Length;
            Console.WriteLine("  CEO.Name.Length: " + (nameLen.HasValue ? nameLen.ToString() : "null"));

            // ── ?[] 索引器空传播 ─────────────────────────────────────
            company.CEO.Phones = new List<string> { "123", "456" };
            string firstPhone = company?.CEO?.Phones?[0];
            Console.WriteLine("  Phones[0]: " + (firstPhone ?? "null"));

            Person noPerson = null;
            string noPhone = noPerson?.Phones?[0];
            Console.WriteLine("  null?[0]: " + (noPhone ?? "null"));

            // ── ?. 配合 ?? 提供默认值 ────────────────────────────────
            string country = company?.CEO?.Address?.Country ?? "Unknown";
            Console.WriteLine("  Country ?? \"Unknown\": " + country);

            string noCity = company?.CEO?.Address?.ZipCode ?? "No ZIP";
            Console.WriteLine("  ZipCode ?? \"No ZIP\": " + noCity);

            // ── ?. 配合方法链 ────────────────────────────────────────
            string upper = company?.CEO?.Name?.ToUpper();
            Console.WriteLine("  Name.ToUpper(): " + (upper ?? "null"));

            // ── 事件调用中的 ?. ──────────────────────────────────────
            // C# 6.0 前: if (Changed != null) Changed(this, args);
            // C# 6.0 起: Changed?.Invoke(this, args);  线程安全！
            EventHandler handler = null;
            handler?.Invoke(null, EventArgs.Empty);  // 安全，无需 null 检查
            Console.WriteLine("  handler?.Invoke: 安全调用（无异常）");

            Console.WriteLine();
            Console.WriteLine("  ?. 的两大价值:");
            Console.WriteLine("    1. 消除多层 null 检查的样板代码");
            Console.WriteLine("    2. 事件调用线程安全（原子性检查+调用）");
        }
    }
}
