using System;

namespace CsharplangDemo9.Demos
{
    public static class TopLevelStatementDemo
    {
        public static void Run()
        {
            Console.WriteLine("  Program.cs 使用了顶级语句 (C# 9 新增)");
            Console.WriteLine();
            Console.WriteLine("  C# 9 前的 Program.cs 结构:");
            Console.WriteLine("  ┌────────────────────────────────────────");
            Console.WriteLine("  │ using System;");
            Console.WriteLine("  │ namespace MyApp");
            Console.WriteLine("  │ {");
            Console.WriteLine("  │     class Program");
            Console.WriteLine("  │     {");
            Console.WriteLine("  │         static void Main(string[] args)");
            Console.WriteLine("  │         {");
            Console.WriteLine("  │             Console.WriteLine(\"Hello\");");
            Console.WriteLine("  │         }");
            Console.WriteLine("  │     }");
            Console.WriteLine("  │ }");
            Console.WriteLine("  └────────────────────────────────────────");
            Console.WriteLine();
            Console.WriteLine("  C# 9 起的 Program.cs 结构:");
            Console.WriteLine("  ┌────────────────────────────────────────");
            Console.WriteLine("  │ Console.WriteLine(\"Hello\");  // 直接写语句");
            Console.WriteLine("  └────────────────────────────────────────");
            Console.WriteLine();
            Console.WriteLine("  顶级语句规则:");
            Console.WriteLine("    • 整个项目只能有一个文件使用顶级语句");
            Console.WriteLine("    • 可以包含本地函数和 using 指令");
            Console.WriteLine("    • 编译器自动生成 Program 类和 Main 方法");
            Console.WriteLine("    • args 参数自动可用（隐式 string[] args）");
            Console.WriteLine("    • await 和 return 都可以直接使用");
        }
    }
}
