// C# 9 新增功能演示入口
// 依赖: net10.0 运行时，LangVersion=9，ImplicitUsings=disable
// 注意: 顶级语句本身就是 C# 9 的特性演示 ──
//       无需 namespace / class Program / static void Main()

using System;
using CsharplangDemo9.Demos;

static void Run(string title, Action action)
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine($"\n── {title} ──");
    Console.ResetColor();
    action();
}

Run("1.  record 类型",                         RecordDemo.Run);
Run("2.  init 仅限初始化的属性",                InitOnlyDemo.Run);
Run("3.  顶级语句（Top-level Statements）",    TopLevelStatementDemo.Run);
Run("4.  模式匹配增强（关系 + 逻辑模式）",      PatternMatchingDemo.Run);
Run("5.  目标类型 new 表达式",                  TargetTypedNewDemo.Run);
Run("6.  static 匿名函数",                     StaticAnonymousDemo.Run);
Run("7.  协变返回类型",                         CovariantReturnDemo.Run);
Run("8.  模块初始化器",                         ModuleInitializerDemo.Run);
Run("9.  partial 方法增强",                    PartialMethodDemo.Run);
Run("10. 其他改进（nint/函数指针/弃元/协变）", MiscDemo.Run);
