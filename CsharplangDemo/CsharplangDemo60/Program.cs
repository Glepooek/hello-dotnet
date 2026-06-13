// C# 6.0 新增功能演示入口
// 依赖: net10.0 运行时，LangVersion=6，ImplicitUsings=disable
// C# 6.0 发布于 2015 年 7 月，随 Visual Studio 2015 一同发布
// Roslyn 编译器即服务也在此版本发布
// https://learn.microsoft.com/zh-cn/dotnet/csharp/whats-new/csharp-version-history#c-version-60

using System;
using CsharplangDemo60.Demos;

namespace CsharplangDemo60
{
    class Program
    {
        static void Main(string[] args)
        {
            Run("1.  静态导入（using static）",           StaticImportDemo.Run);
            Run("2.  异常筛选器（when）",                  ExceptionFilterDemo.Run);
            Run("3.  自动属性初始化器",                    AutoPropertyInitDemo.Run);
            Run("4.  表达式主体成员",                      ExpressionBodyDemo.Run);
            Run("5.  空传播器（?.）",                      NullPropagatorDemo.Run);
            Run("6.  字符串内插（$\"\"）",                 StringInterpolationDemo.Run);
            Run("7.  nameof 运算符",                      NameofDemo.Run);
            Run("8.  索引初始化器",                        IndexInitializerDemo.Run);
            Run("9.  Catch/Finally 中的 Await",           AwaitInCatchDemo.Run);
            Run("10. 仅限 getter 属性的默认值",            GetterOnlyDefaultDemo.Run);
            
            Console.ReadLine();
        }

        static void Run(string title, Action action)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n── " + title + " ──");
            Console.ResetColor();
            action();
        }
    }
}
