// C# 14 新增功能演示入口
// 依赖: .NET 10，C# 14（net10.0 默认语言版本，无需 LangVersion 配置）
using CsharplangDemo14.Demos;

Run("1. 扩展成员（Extension Members）", ExtensionMembersDemo.Run);
Run("2. field 关键字（正式稳定）", FieldKeywordDemo.Run);
Run("3. Span<T>/ReadOnlySpan<T> 隐式转换增强", SpanConversionDemo.Run);
Run("4. nameof 支持未绑定泛型类型", NameofUnboundDemo.Run);
Run("5. Lambda 简单参数上的修饰符", LambdaModifierDemo.Run);
Run("6. partial 构造函数和事件", PartialCtorEventDemo.Run);
Run("7. 用户定义复合赋值运算符（就地修改）", UserDefinedCompoundAssignDemo.Run);
Run("8. 空条件赋值（Null-Conditional Assignment）", NullConditionalAssignDemo.Run);

static void Run(string title, Action action)
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine($"\n── {title} ──");
    Console.ResetColor();
    action();
}
