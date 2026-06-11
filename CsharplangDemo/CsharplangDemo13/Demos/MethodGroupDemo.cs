namespace CsharplangDemo13.Demos;

public static class MethodGroupDemo
{
    // C# 13 前: 重载解析会收集所有作用域内同名方法组成候选集, 再推断自然类型
    // C# 13 起: 按作用域逐层剪枝, 移除不适用重载, 减少歧义

    static void Print(int x) => Console.WriteLine($"  Print(int): {x}");
    static void Print(string x) => Console.WriteLine($"  Print(string): {x}");
    static void Print(object x) => Console.WriteLine($"  Print(object): {x}");

    public static void Run()
    {
        // 方法组赋值给委托 -- 编译器从局部作用域向外逐层查找最佳匹配
        Action<int> printInt = Print;        // 精确匹配 Print(int)
        Action<string> printStr = Print;     // 精确匹配 Print(string)

        printInt(42);
        printStr("hello");

        // 改进体现: 若外层作用域存在同名方法且签名冲突,
        // C# 13 编译器只在当前作用域找到匹配就停止, 不再继续查找外层造成歧义
        static void Print(double x) => Console.WriteLine($"  本地 Print(double): {x}");

        Action<double> printDouble = Print;  // 精确绑定到本地 Print(double), 不受外层 Print 干扰
        printDouble(3.14);
    }
}
