// file 关键字限制类型可见性在当前文件内
// 典型用途: 源生成器内部类型, 避免与用户代码命名冲突

// 只有本文件内可见
file class InternalBuilder
{
    private readonly System.Text.StringBuilder _sb = new();

    public InternalBuilder Append(string text) { _sb.Append(text); return this; }
    public InternalBuilder AppendLine(string text) { _sb.AppendLine(text); return this; }
    public string Build() => _sb.ToString().TrimEnd();
}

// file 也可用于 struct/enum/interface
file struct BuildConfig
{
    public string Name;
    public int Version;
}

// 公开的 Demo 类通过 file 类型完成内部工作
namespace CsharplangDemo11.Demos;

public static class FileLocalTypeDemo
{
    public static void Run()
    {
        // 使用 file 局部 class
        var builder = new InternalBuilder();
        string result = builder
            .Append("C# 11 ")
            .Append("file ")
            .AppendLine("局部类型")
            .Append("仅在当前文件可见")
            .Build();

        Console.WriteLine($"  Builder 输出: {result}");

        // 使用 file struct
        var config = new BuildConfig { Name = "Demo", Version = 11 };
        Console.WriteLine($"  BuildConfig: {config.Name} v{config.Version}");

        Console.WriteLine();
        Console.WriteLine("  file 类型的典型用途:");
        Console.WriteLine("    • 源生成器: 生成的辅助类不污染用户命名空间");
        Console.WriteLine("    • 不同文件可声明同名 file 类型, 互不冲突");
        Console.WriteLine("    • 替代 internal + 命名混淆来隐藏实现细节");
    }
}
