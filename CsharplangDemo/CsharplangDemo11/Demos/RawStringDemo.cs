namespace CsharplangDemo11.Demos;

public static class RawStringDemo
{
    public static void Run()
    {
        // ── C# 11 前: 需要大量转义符 ──────────────────────────────────
        string oldJson = "{\n  \"name\": \"Alice\",\n  \"path\": \"C:\\\\Users\\\\Alice\"\n}";
        Console.WriteLine("  旧写法 (转义地狱):");
        Console.WriteLine("  " + oldJson.Replace("\n", "\n  "));

        // ── C# 11 起: 原始字符串 — 用 """ 包围, 原样书写 ─────────────
        // 规则: 开头 """ 之后换行; 结尾 """ 的缩进决定内容的基准缩进
        string json = """
            {
              "name": "Alice",
              "path": "C:\Users\Alice"
            }
            """;
        Console.WriteLine("  新写法 (原始字符串):");
        Console.WriteLine("  " + json.Replace("\n", "\n  "));

        // ── 内插原始字符串 $""" ... """ ───────────────────────────────
        string host = "localhost";
        int port = 8080;
        string config = $"""
            {{
              "host": "{host}",
              "port": {port}
            }}
            """;
        // {{ }} 是字面花括号, { } 是内插表达式 —— 与普通内插字符串一致
        Console.WriteLine("  内插原始字符串:");
        Console.WriteLine("  " + config.Replace("\n", "\n  "));

        // ── 多行正则 — 无需 @"" 也无需转义反斜杠 ─────────────────────
        string pattern = """^\d{4}-\d{2}-\d{2}$""";   // 日期格式正则
        Console.WriteLine($"  正则模式: {pattern}");

        // ── 更多引号数量: """" 或 """"" 可在内容中包含 """ ────────────
        string withQuotes = """"
            包含三引号: """ 没问题
            """";
        Console.WriteLine($"  含三引号: {withQuotes}");
    }
}
