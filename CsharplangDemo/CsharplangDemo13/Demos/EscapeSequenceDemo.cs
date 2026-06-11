namespace CsharplangDemo13.Demos;

public static class EscapeSequenceDemo
{
    public static void Run()
    {
        // \e = U+001B (ESCAPE 字符), 用于 ANSI 终端控制序列
        // 旧写法:  或 \x1b (\x1b 有隐患: 后续十六进制字符会被吞并)
        // 新写法: \e  (明确、安全)

        const char esc = '\e';
        Console.WriteLine($"  ESCAPE 字符码值: {(int)esc}");  // 27

        // ANSI 颜色序列演示 (若终端支持)
        Console.WriteLine($"  \e[32m绿色文本\e[0m (ANSI 颜色)");
        Console.WriteLine($"  \e[1;33m粗体黄色\e[0m (ANSI 粗体+颜色)");

        // 对比: \x1b 的隐患
        // 下面两行输出相同, 但 \x1b 若后跟合法十六进制字符时行为不同:
        // "\x1b[0m" -> U+001B + "[0m"  (正确)
        // "\x1bX"   -> \x1BX 被解析为单个字符 U+01BX (错误!)
        // "\e" 则始终只代表 U+001B, 没有歧义
        Console.WriteLine("  \\e 无歧义; \\x1b 若后跟十六进制字符则会误解析");
    }
}
