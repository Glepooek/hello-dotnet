namespace CsharplangDemo13.Demos;

public static class ParamsCollectionDemo
{
    public static void Run()
    {
        // C# 13 前: params 仅限数组 params int[]
        // C# 13 起: 支持任意可识别集合类型

        // 1. params ReadOnlySpan<T> -- 最高效: 参数存于栈, 零堆分配
        static void PrintSpan(params ReadOnlySpan<int> items)
        {
            Console.Write("  ReadOnlySpan: ");
            foreach (var item in items) Console.Write($"{item} ");
            Console.WriteLine();
        }

        // 2. params IEnumerable<T> -- 接口类型: 编译器自动合成存储
        static void PrintEnumerable(params IEnumerable<int> items)
        {
            Console.Write("  IEnumerable:  ");
            foreach (var item in items) Console.Write($"{item} ");
            Console.WriteLine();
        }

        // 3. params List<T> -- 具体集合类型
        static void PrintList(params List<int> items)
        {
            Console.Write("  List<int>:    ");
            foreach (var item in items) Console.Write($"{item} ");
            Console.WriteLine();
        }

        PrintSpan(10, 20, 30);
        PrintEnumerable(10, 20, 30);
        PrintList(10, 20, 30);

        // 也可直接传入已有集合 (不展开)
        int[] arr = [1, 2, 3];
        PrintSpan(arr);
    }
}
