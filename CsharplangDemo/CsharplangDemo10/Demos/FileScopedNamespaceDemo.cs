// 本文件本身就是"文件范围命名空间"的演示
// 整个文件属于此命名空间, 无需大括号, 减少一级缩进
namespace CsharplangDemo10.Demos;

public static class FileScopedNamespaceDemo
{
    public static void Run()
    {
        Console.WriteLine("  本文件使用文件范围命名空间: namespace CsharplangDemo10.Demos;");
        Console.WriteLine();

        // ── 对比 ────────────────────────────────────────────────────
        Console.WriteLine("  C# 10 前 (传统写法):");
        Console.WriteLine("  ┌─────────────────────────────────────────");
        Console.WriteLine("  │ namespace MyApp.Services");
        Console.WriteLine("  │ {");
        Console.WriteLine("  │     public class UserService   // 多一级缩进");
        Console.WriteLine("  │     {");
        Console.WriteLine("  │         public void DoWork() { }");
        Console.WriteLine("  │     }");
        Console.WriteLine("  │ }");
        Console.WriteLine("  └─────────────────────────────────────────");
        Console.WriteLine();
        Console.WriteLine("  C# 10 起 (文件范围命名空间):");
        Console.WriteLine("  ┌─────────────────────────────────────────");
        Console.WriteLine("  │ namespace MyApp.Services;          // 无大括号");
        Console.WriteLine("  │");
        Console.WriteLine("  │ public class UserService            // 无多余缩进");
        Console.WriteLine("  │ {");
        Console.WriteLine("  │     public void DoWork() { }");
        Console.WriteLine("  │ }");
        Console.WriteLine("  └─────────────────────────────────────────");
        Console.WriteLine();
        Console.WriteLine("  限制:");
        Console.WriteLine("    • 一个文件只能有一个文件范围命名空间声明");
        Console.WriteLine("    • 声明前不能有任何类型定义");
        Console.WriteLine("    • 不能与传统 namespace {} 混用");
    }
}
