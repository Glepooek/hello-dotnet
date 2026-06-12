// C# 7.2 新增功能演示入口
// 依赖: net10.0 运行时，LangVersion=7.2，ImplicitUsings=disable
// C# 7.2 发布于 2017 年 11 月，.NET Framework 4.7.1 / .NET Core 2.0
using System;
using CsharplangDemo72.Demos;

namespace CsharplangDemo72
{
    class Program
    {
        static void Main(string[] args)
        {
            Run("1. readonly struct（不可变结构体）",   ReadonlyStructDemo.Run);
            Run("2. in 参数修饰符",                    InParameterDemo.Run);
            Run("3. ref readonly 返回值",              RefReadonlyReturnDemo.Run);
            Run("4. ref struct（栈约束结构体）",        RefStructDemo.Run);
            Run("5. 非尾随命名参数",                   NonTrailingNamedArgDemo.Run);
            Run("6. 数值字面量前导下划线",              LeadingUnderscoreDemo.Run);
            Run("7. private protected 访问修饰符",      PrivateProtectedDemo.Run);
            Run("8. 条件 ref 表达式",                  ConditionalRefDemo.Run);
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
