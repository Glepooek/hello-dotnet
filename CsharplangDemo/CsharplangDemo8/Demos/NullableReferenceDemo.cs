using System;
using System.Collections.Generic;

namespace CsharplangDemo8.Demos
{
    // Nullable=enable 已在 csproj 启用
    // string  = 不可为 null (编译器追踪，若可能为 null 则警告)
    // string? = 可以为 null (需要显式检查才能使用)

    public class UserProfile
    {
        public string  Name    { get; set; }   // 不可为 null
        public string? Email   { get; set; }   // 可以为 null
        public string? Phone   { get; set; }   // 可以为 null
        public Address HomeAddress { get; set; }  // 不可为 null

        public UserProfile(string name, Address address)
        {
            Name = name;
            HomeAddress = address;
        }
    }

    public class Address
    {
        public string City    { get; set; }
        public string Country { get; set; }

        public Address(string city, string country)
        {
            City = city;
            Country = country;
        }
    }

    public static class NullableReferenceDemo
    {
        // 返回 string? 表明可能返回 null
        static string? FindEmail(string name, Dictionary<string, string> map)
            => map.TryGetValue(name, out var email) ? email : null;

        // 参数 string? 表明调用方可传 null
        static int GetLength(string? s)
        {
            // 直接用 s.Length 会触发 CS8602 警告
            // 必须先检查
            if (s == null) return 0;
            return s.Length;  // 此处编译器已知 s 非 null
        }

        // 非空断言 ! ── 向编译器声明"我确认此处非 null"
        static string GetUpper(string? s)
            => s!.ToUpper();  // 若 s 为 null 会运行时崩溃，由开发者负责

        public static void Run()
        {
            var addr = new Address("Beijing", "China");
            var user = new UserProfile("Alice", addr)
            {
                Email = "alice@example.com"
                // Phone 未赋值，保持 null
            };

            // ── 安全访问 null 属性 ────────────────────────────────────
            Console.WriteLine($"  Name:  {user.Name}");
            Console.WriteLine($"  Email: {user.Email ?? "(未设置)"}");
            Console.WriteLine($"  Phone: {user.Phone ?? "(未设置)"}");

            // ── null 条件运算符 ?. ────────────────────────────────────
            int emailLen = user.Email?.Length ?? 0;
            Console.WriteLine($"  Email 长度: {emailLen}");

            // ── FindEmail 返回 string? ────────────────────────────────
            var emailMap = new Dictionary<string, string>
            {
                ["Alice"] = "alice@example.com"
            };
            string? aliceEmail = FindEmail("Alice", emailMap);
            string? bobEmail   = FindEmail("Bob",   emailMap);

            Console.WriteLine($"  Alice email: {aliceEmail ?? "null"}");
            Console.WriteLine($"  Bob email:   {bobEmail   ?? "null"}");

            // ── GetLength 处理 null 参数 ──────────────────────────────
            Console.WriteLine($"  GetLength(null):    {GetLength(null)}");
            Console.WriteLine($"  GetLength(\"hello\"): {GetLength("hello")}");

            Console.WriteLine();
            Console.WriteLine("  Nullable 引用类型是纯编译期静态分析，不影响运行时");
            Console.WriteLine("  在 .csproj 中 <Nullable>enable</Nullable> 启用");
        }
    }
}
