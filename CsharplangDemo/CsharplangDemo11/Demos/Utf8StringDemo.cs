namespace CsharplangDemo11.Demos;

public static class Utf8StringDemo
{
    public static void Run()
    {
        // ── C# 11 前: 运行时编码, 产生堆分配 ─────────────────────────
        byte[] oldBytes = System.Text.Encoding.UTF8.GetBytes("Hello, World!");

        // ── C# 11 起: u8 后缀 — 编译期 UTF-8 字节, 类型 ReadOnlySpan<byte> ──
        // 字节序列在编译时确定, 存于只读数据段, 零运行时分配
        ReadOnlySpan<byte> helloBytes = "Hello, World!"u8;
        ReadOnlySpan<byte> jsonBytes  = "{\"ok\":true}"u8;
        ReadOnlySpan<byte> httpHeader = "Content-Type: application/json\r\n"u8;

        Console.WriteLine($"  旧写法 (运行时编码):   {oldBytes.Length} 字节");
        Console.WriteLine($"  u8 字面量:             {helloBytes.Length} 字节");
        Console.WriteLine($"  JSON u8:               {jsonBytes.Length} 字节");
        Console.WriteLine($"  HTTP Header u8:        {httpHeader.Length} 字节");

        // 验证内容: 转回字符串
        Console.WriteLine($"  内容验证: {System.Text.Encoding.UTF8.GetString(helloBytes)}");

        // 多字节 Unicode: 正确计算 UTF-8 字节数
        ReadOnlySpan<byte> chineseBytes = "你好世界"u8;
        Console.WriteLine($"  \"你好世界\" u8: {chineseBytes.Length} 字节 (每个汉字 3 字节)");

        // 典型用途: 零拷贝写入网络流/文件
        // await stream.WriteAsync(httpHeader.ToArray()); // 实际场景
        Console.WriteLine();
        Console.WriteLine("  u8 字面量存于程序只读数据段，无堆分配，适合高频网络/IO 路径");

        // 与 byte[] 互操作
        byte[] arr = helloBytes.ToArray();  // 需要时才复制到堆
        Console.WriteLine($"  ToArray() 长度: {arr.Length}");
    }
}
