using System;
using System.Collections.Generic;

namespace CsharplangDemo60.Demos
{
    public static class AutoPropertyInitDemo
    {
        // ── C# 6.0: 自动属性初始化器 ─────────────────────────────────────
        // C# 6.0 前: 自动属性只能在构造函数中赋初值
        // C# 6.0 起: 可以直接在属性声明处赋初值

        class ServerConfig
        {
            // C# 6.0 前: 这些属性只能在构造函数中赋值
            // C# 6.0 起: 直接赋初值，更简洁
            public string Host     { get; set; } = "localhost";
            public int    Port     { get; set; } = 8080;
            public bool   UseSsl   { get; set; } = false;
            public string Protocol { get; set; } = "http";

            // 只读自动属性也可以有初始化器（C# 6.0 新特性，见 Demo 10）
            public string FullUrl  { get; } = "http://localhost:8080";

            // 集合类型初始化
            public List<string> Tags { get; set; } = new List<string> { "default" };

            // 构造函数中可以覆盖默认值
            public ServerConfig() { }
            public ServerConfig(string host, int port)
            {
                Host = host;
                Port = port;
                FullUrl = Protocol + "://" + host + ":" + port;
            }

            public override string ToString()
                => Protocol + "://" + Host + ":" + Port;
        }

        class User
        {
            // 混合: 部分有默认值，部分在构造函数中赋值
            public string Name   { get; set; }  = "Anonymous";
            public int    Age    { get; set; }  = 0;
            public bool   Active { get; set; }  = true;
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            public User() { }
            public User(string name, int age)
            {
                Name = name;
                Age = age;
            }
        }

        public static void Run()
        {
            // 使用默认值
            var cfg1 = new ServerConfig();
            Console.WriteLine("  默认配置: " + cfg1);
            Console.WriteLine("  Tags: [" + string.Join(", ", cfg1.Tags) + "]");

            // 构造函数覆盖
            var cfg2 = new ServerConfig("api.example.com", 443);
            Console.WriteLine("  自定义: " + cfg2);
            Console.WriteLine("  FullUrl: " + cfg2.FullUrl);

            // 对象初始化器覆盖默认值
            var cfg3 = new ServerConfig
            {
                Host = "prod.server",
                Port = 443,
                UseSsl = true,
                Protocol = "https"
            };
            Console.WriteLine("  对象初始化器: " + cfg3);

            // User 演示
            var u1 = new User();
            Console.WriteLine("  默认用户: " + u1.Name + ", active=" + u1.Active);

            var u2 = new User("Alice", 30);
            Console.WriteLine("  指定用户: " + u2.Name + ", age=" + u2.Age);

            Console.WriteLine();
            Console.WriteLine("  C# 6.0 前: 自动属性默认值只能写在构造函数里");
            Console.WriteLine("  C# 6.0 起: 直接在属性声明处 = value 设置初始值");
        }
    }
}
