// C# 13 新增功能演示入口
// 依赖: .NET 10, LangVersion=preview
using CsharplangDemo13.Demos;

Run("1. params 集合扩展", ParamsCollectionDemo.Run);
Run("2. 新的 Lock 类型", NewLockDemo.Run);
Run("3. 新转义序列 \\e", EscapeSequenceDemo.Run);
Run("4. 方法组自然类型改进", MethodGroupDemo.Run);
Run("5. 对象初始化器中的隐式索引访问", ImplicitIndexDemo.Run);
await RunAsync("6. 迭代器和异步方法中的 ref 局部变量", RefInAsyncDemo.RunAsync);
Run("7 & 8. ref struct 实现接口 + allows ref struct", RefStructInterfaceDemo.Run);
Run("9. partial 属性", PartialPropertyDemo.Run);
Run("10. 重载解析优先级", OverloadPriorityDemo.Run);
Run("11. field 上下文关键字（预览）", FieldKeywordDemo.Run);

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
