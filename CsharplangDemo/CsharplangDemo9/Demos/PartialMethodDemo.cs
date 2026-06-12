using System;

namespace CsharplangDemo9.Demos
{
    // ── partial 方法增强: 支持访问修饰符和非 void 返回类型 ───────────────
    public partial class CodeGenerator
    {
        public partial string  GenerateName(int seed);
        public partial int     ComputeHash(string input);
        public partial bool    TryParse(string raw, out int value);
        internal partial void  LogMessage(string msg);
    }

    public partial class CodeGenerator
    {
        public partial string GenerateName(int seed) => $"Item_{seed:D4}";

        public partial int ComputeHash(string input)
        {
            int hash = 17;
            foreach (char c in input) hash = hash * 31 + c;
            return hash;
        }

        public partial bool TryParse(string raw, out int value) =>
            int.TryParse(raw, out value);

        internal partial void LogMessage(string msg) =>
            Console.WriteLine($"  [Generator] {msg}");
    }

    public static class PartialMethodDemo
    {
        public static void Run()
        {
            var gen = new CodeGenerator();

            string name = gen.GenerateName(42);
            int    hash = gen.ComputeHash("hello");
            Console.WriteLine($"  GenerateName(42): {name}");
            Console.WriteLine($"  ComputeHash(\"hello\"): {hash}");

            if (gen.TryParse("123", out int parsed))
                Console.WriteLine($"  TryParse(\"123\"): {parsed}");

            gen.LogMessage("partial 方法演示完成");

            Console.WriteLine();
            Console.WriteLine("  C# 9 前 partial 方法限制:");
            Console.WriteLine("    • 只能 private void，无返回值");
            Console.WriteLine("    • 无实现时编译器直接删除调用点（静默忽略）");
            Console.WriteLine("  C# 9 起 partial 方法增强:");
            Console.WriteLine("    • 支持 public/internal/protected 等访问修饰符");
            Console.WriteLine("    • 支持非 void 返回类型，但必须提供实现");
            Console.WriteLine("    • 源生成器的核心基础设施");
        }
    }
}
