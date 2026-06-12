// C# 7.3 新增功能演示入口
// 依赖: net10.0 运行时，LangVersion=7.3，ImplicitUsings=disable
// C# 7.3 发布于 2018 年 5 月，.NET Framework 4.7.2 / .NET Core 2.1
using System;
using CsharplangDemo73.Demos;

namespace CsharplangDemo73
{
    class Program
    {
        static void Main(string[] args)
        {
            Run("1. ref 局部变量可重新分配",              RefLocalReassignDemo.Run);
            Run("2. stackalloc 初始化器",                 StackAllocInitDemo.Run);
            Run("3. fixed 支持任意实现模式的类型",         FixedPatternDemo.Run);
            Run("4. 泛型约束: unmanaged / Enum / Delegate", GenericConstraintDemo.Run);
            Run("5. 元组支持 == 和 !=",                   TupleEqualityDemo.Run);
            Run("6. 表达式变量适用范围扩展",               ExpressionVariableDemo.Run);
            Run("7. 自动属性后备字段特性",                 BackingFieldAttributeDemo.Run);
            Run("8. in 参数的重载解析改进",                InOverloadDemo.Run);
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
