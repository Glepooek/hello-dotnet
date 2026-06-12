// C# 10 新增功能演示入口
// 依赖: net10.0 运行时，LangVersion=10（边界约束）
// 注意: ImplicitUsings=enable 已隐式引入 System/Collections/Linq/Threading 等
//       GlobalUsings.cs 额外引入了 CsharplangDemo10.Demos (global using)

Run("1.  record struct（记录结构）",                     RecordStructDemo.Run);
Run("2.  struct 改进（无参构造 + 字段初始化器）",          StructImprovementDemo.Run);
Run("3.  内插字符串处理程序",                             InterpolatedStringHandlerDemo.Run);
Run("4.  global using 指令",                             GlobalUsingDemo.Run);
Run("5.  文件范围的命名空间声明",                         FileScopedNamespaceDemo.Run);
Run("6.  扩展属性模式",                                   ExtendedPropertyPatternDemo.Run);
Run("7.  Lambda 表达式改进",                              LambdaImprovementDemo.Run);
Run("8.  const 内插字符串",                               ConstInterpolatedStringDemo.Run);
Run("9.  sealed override ToString",                      SealedToStringDemo.Run);
Run("10. CallerArgumentExpression 特性",                  CallerArgumentExpressionDemo.Run);
Run("11. 解构中混合赋值与声明",                           DeconstructMixedDemo.Run);

static void Run(string title, Action action)
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine($"\n── {title} ──");
    Console.ResetColor();
    action();
}
