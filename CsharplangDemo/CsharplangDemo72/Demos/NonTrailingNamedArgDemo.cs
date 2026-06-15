using System;

namespace CsharplangDemo72.Demos
{
    public static class NonTrailingNamedArgDemo
    {
        static void Configure(string host, int port, bool ssl, int timeout, string protocol)
        {
            Console.WriteLine("  " + protocol + "://" + host + ":" + port +
                              " ssl=" + ssl + " timeout=" + timeout + "s");
        }

        static void CreateUser(string name, int age, string role, bool active, string email)
        {
            Console.WriteLine("  User: " + name + " age=" + age +
                              " role=" + role + " active=" + active + " email=" + email);
        }

        public static void Run()
        {
            // ── C# 7.2 之前: 命名参数之后不能再有位置参数 ───────────────
            // 所有参数要么全位置，要么命名参数必须在最后
            Configure("localhost", 8080, false, 30, "http");  // 全位置参数

            // ── C# 7.2 起: 命名参数可以在中间，后面跟位置参数 ────────────
            // 规则: 命名参数之后的位置参数，必须对应命名参数之后的形参位置
            Configure("api.example.com", port: 443, true, 60, "https");
            Configure(host: "prod.server", 8443, ssl: true, 30, "wss");
            Configure("dev", 3000, false, timeout: 10, "http");

            Console.WriteLine("  ─────────────────────────────");

            // 更实际的例子: 只命名关键参数，其他按位置
            CreateUser("Alice", 30, "admin", true, "alice@example.com");
            CreateUser("Bob", age: 25, "user", active: true, "bob@example.com");

            Console.WriteLine();
            Console.WriteLine("  非尾随命名参数规则:");
            Console.WriteLine("    • 命名参数明确了意图，位置参数保持简洁");
            Console.WriteLine("    • 命名参数后的位置参数必须按顺序对应剩余形参");
            Console.WriteLine("    • 有助于读懂参数含义（尤其是 bool/int 型参数）");
        }
    }
}
