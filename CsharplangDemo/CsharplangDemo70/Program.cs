// C# 7.0 新增功能演示入口
// 依赖: net10.0 运行时，LangVersion=7，ImplicitUsings=disable
// C# 7.0 发布于 2017 年 3 月，随 Visual Studio 2017 一同发布
// https://learn.microsoft.com/zh-cn/dotnet/csharp/whats-new/csharp-version-history#c-version-70

using System;
using CsharplangDemo70.Demos;

namespace CsharplangDemo70
{
    class Program
    {
        static void Main(string[] args)
        {
            Run("1.  out 变量",                          OutVariableDemo.Run);
            Run("2.  元组和析构函数",                    TupleAndDeconstructDemo.Run);
            Run("3.  模式匹配",                          PatternMatchingDemo.Run);
            Run("4.  本地函数",                          LocalFunctionDemo.Run);
            Run("5.  扩展的表达式主体成员",               ExpressionBodyDemo.Run);
            Run("6.  ref 局部变量和引用返回值",            RefLocalAndReturnDemo.Run);
            Run("7.  弃元（Discards）",                  DiscardDemo.Run);
            Run("8.  二进制文本和数字分隔符",              LiteralDemo.Run);
            Run("9.  抛出表达式",                        ThrowExpressionDemo.Run);

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
