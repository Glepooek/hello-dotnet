// C# 11 新增功能演示入口
// 依赖: net10.0 运行时，LangVersion=11（边界约束）
using CsharplangDemo11.Demos;

Run("1.  原始字符串字面量（Raw String Literals）",  RawStringDemo.Run);
Run("2.  泛型数学支持（Generic Math）",              GenericMathDemo.Run);
Run("3.  泛型特性（Generic Attributes）",            GenericAttributeDemo.Run);
Run("4.  UTF-8 字符串字面量",                        Utf8StringDemo.Run);
Run("5.  字符串内插中的换行",                        InterpolationNewlineDemo.Run);
Run("6.  列表模式（List Patterns）",                 ListPatternDemo.Run);
Run("7.  file 局部类型",                             FileLocalTypeDemo.Run);
Run("8.  required 成员",                             RequiredMemberDemo.Run);
Run("9.  自动默认结构（Auto-default Structs）",      AutoDefaultStructDemo.Run);
Run("10. ref 字段与 scoped ref",                     RefFieldDemo.Run);
Run("11. 其他改进（nameof/nint/Span 模式匹配）",     MiscDemo.Run);

static void Run(string title, Action action)
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine($"\n── {title} ──");
    Console.ResetColor();
    action();
}
