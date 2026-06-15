// C# 7.1 新增功能演示入口
// 依赖: net10.0 运行时，LangVersion=7.1，ImplicitUsings=disable
// C# 7.1 发布于 2017 年 8 月，是 C# 首个独立"点版本"
// 需要在 .csproj 中显式指定 <LangVersion>7.1</LangVersion> 才能启用
//
// C# 7.1 新增特性:
//   1. async Main — 入口点支持 async/await
//   2. default 字面量 — 无需重复类型名的默认值表达式
//   3. 推断元组元素名称 — 从变量名自动推断
//   4. 泛型类型参数的模式匹配 — is T 作用于泛型参数
using System;
using System.Threading.Tasks;
using CsharplangDemo71.Demos;

namespace CsharplangDemo71
{
    class Program
    {
        // ── C# 7.1 新增: async Main ──────────────────────────────────
        // C# 7.1 之前: 必须用 .GetAwaiter().GetResult() 阻塞调用
        // C# 7.1 起: 入口点可以是 async Task Main 或 async Task<int> Main
        static async Task Main(string[] args)
        {
            Run("1. async Main（当前方法就是演示）",     AsyncMainDemo.Run);
            Run("2. default 字面量",                   DefaultLiteralDemo.Run);
            Run("3. 推断元组元素名称",                  InferredTupleNamesDemo.Run);
            Run("4. 泛型类型参数的模式匹配",             GenericPatternMatchDemo.Run);

            // async Main 的直接体现: 可以直接 await
            await AsyncMainDemo.RunAsync();

            Console.ReadLine();
        }

        static void Run(string title, Action action)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n── " + title + " ──");
            Console.ResetColor();
            action();
        }
    }
}
