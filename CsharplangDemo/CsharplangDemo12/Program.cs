// C# 12 新增功能演示入口
// 依赖: net10.0 运行时，LangVersion=12（边界约束）
using CsharplangDemo12.Demos;

Run("1. 主构造函数（Primary Constructors）", PrimaryConstructorDemo.Run);
Run("2. 集合表达式（Collection Expressions）", CollectionExpressionDemo.Run);
Run("3. ref readonly 参数", RefReadonlyDemo.Run);
Run("4. Lambda 默认参数", LambdaDefaultParamDemo.Run);
Run("5. 任意类型别名（Alias Any Type）", AliasAnyTypeDemo.Run);
Run("6. 内联数组（Inline Arrays）", InlineArrayDemo.Run);
Run("7. [Experimental] 特性", ExperimentalAttributeDemo.Run);

static void Run(string title, Action action)
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine($"\n── {title} ──");
    Console.ResetColor();
    action();
}
