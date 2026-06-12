// C# 8 新增功能演示入口
// 依赖: net10.0 运行时，LangVersion=8，ImplicitUsings=disable，Nullable=enable
// 注意: 顶级语句是 C# 9 特性，C# 8 必须使用传统 Main 方法
using System;
using System.Threading.Tasks;
using CsharplangDemo8.Demos;

namespace CsharplangDemo8
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Run("1.  可为空引用类型（Nullable Reference Types）",  NullableReferenceDemo.Run);
            Run("2.  switch 表达式",                               SwitchExpressionDemo.Run);
            Run("3.  模式匹配增强（属性/元组/位置模式）",           PatternMatchingDemo.Run);
            Run("4.  using 声明",                                  UsingDeclarationDemo.Run);
            await RunAsync("5.  异步流（Async Streams）",          AsyncStreamDemo.RunAsync);
            Run("6.  索引和范围",                                  IndexRangeDemo.Run);
            Run("7.  readonly 成员",                               ReadonlyMemberDemo.Run);
            Run("8.  默认接口成员",                                DefaultInterfaceMemberDemo.Run);
            Run("9.  Null 合并赋值 ??=",                           NullCoalescingAssignDemo.Run);
            Run("10. 静态本地函数",                                StaticLocalFunctionDemo.Run);
        }

        static void Run(string title, Action action)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n── {title} ──");
            Console.ResetColor();
            action();
        }

        static async Task RunAsync(string title, Func<Task> action)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n── {title} ──");
            Console.ResetColor();
            await action();
        }
    }
}
