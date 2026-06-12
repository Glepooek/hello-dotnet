using System;
using System.Collections.Generic;

namespace CsharplangDemo9.Demos
{
    // ── init 访问器: 只允许在对象初始化期间赋值, 之后只读 ─────────────────
    public class ServerConfig
    {
        public string Host     { get; init; } = "localhost";
        public int    Port     { get; init; } = 8080;
        public bool   UseSsl   { get; init; }
        public int    Timeout  { get; init; } = 30;

        public override string ToString() =>
            $"{(UseSsl ? "https" : "http")}://{Host}:{Port} (timeout={Timeout}s)";
    }

    public class HttpRequest
    {
        public string Method  { get; init; } = "GET";
        public string Url     { get; init; } = "";
        public string? Body   { get; init; }
        public Dictionary<string, string> Headers { get; init; }
            = new Dictionary<string, string>();
    }

    record ImmutablePoint(double X, double Y);

    public static class InitOnlyDemo
    {
        public static void Run()
        {
            var cfg = new ServerConfig
            {
                Host   = "api.example.com",
                Port   = 443,
                UseSsl = true
            };
            Console.WriteLine($"  ServerConfig: {cfg}");

            var devCfg  = new ServerConfig();
            var prodCfg = new ServerConfig { Host = "prod.example.com", UseSsl = true, Port = 443 };
            // 注: 普通 class 不支持 with 表达式 (record/struct 才支持)

            Console.WriteLine($"  Dev:  {devCfg}");
            Console.WriteLine($"  Prod: {prodCfg}");

            var req = new HttpRequest
            {
                Method = "POST",
                Url    = "https://api.example.com/users",
                Body   = "{\"name\":\"Alice\"}",
                Headers = new Dictionary<string, string>
                {
                    ["Content-Type"]  = "application/json",
                    ["Authorization"] = "Bearer token123"
                }
            };
            Console.WriteLine($"  Request: {req.Method} {req.Url}");
            Console.WriteLine($"  Headers: {req.Headers.Count} 个");

            var pt = new ImmutablePoint(1.0, 2.0);
            Console.WriteLine($"  ImmutablePoint: {pt}");

            Console.WriteLine();
            Console.WriteLine("  init vs set:");
            Console.WriteLine("    set:  任何时候都可修改");
            Console.WriteLine("    init: 仅在对象初始化期间可赋值，之后只读");
            Console.WriteLine("    init 兼顾: 对象初始化器的灵活性 + 构造后的不可变性");
        }
    }
}
