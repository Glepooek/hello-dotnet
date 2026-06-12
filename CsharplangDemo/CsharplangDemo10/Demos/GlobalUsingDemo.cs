namespace CsharplangDemo10.Demos;

// GlobalUsings.cs 中已声明 global using CsharplangDemo10.Demos;
// 本文件演示 global using 的行为和用途

public static class GlobalUsingDemo
{
    public static void Run()
    {
        // ── 效果演示: 无需在本文件写任何 using ───────────────────────
        // 下列类型均来自 global using (隐式 + GlobalUsings.cs 中显式):
        //   List<T>          → System.Collections.Generic (ImplicitUsings)
        //   Enumerable       → System.Linq               (ImplicitUsings)
        //   Task             → System.Threading.Tasks     (ImplicitUsings)
        //   CallerFilePathAttribute → System.Runtime.CompilerServices (GlobalUsings.cs)

        var nums = new List<int> { 5, 3, 8, 1, 9, 2 };
        var sorted = nums.OrderBy(x => x).ToList();
        Console.WriteLine($"  List + LINQ (无 using): [{string.Join(", ", sorted)}]");

        // ── global using 的典型组织方式 ──────────────────────────────
        Console.WriteLine();
        Console.WriteLine("  项目中 GlobalUsings.cs 的结构建议:");
        Console.WriteLine("    global using System;                        // 基础");
        Console.WriteLine("    global using System.Collections.Generic;    // 集合");
        Console.WriteLine("    global using System.Linq;                   // LINQ");
        Console.WriteLine("    global using MyApp.Shared;                  // 项目公共类型");
        Console.WriteLine("    global using static System.Console;         // 静态导入");
        Console.WriteLine();

        // ── global using static: 静态成员直接调用 ────────────────────
        // 本演示不实际声明 global using static, 仅展示语法
        Console.WriteLine("  global using static System.Math 后可直接写:");
        Console.WriteLine($"    Sqrt(16) = {Math.Sqrt(16):F0}");
        Console.WriteLine($"    PI       = {Math.PI:F4}");

        // ── global using alias ────────────────────────────────────────
        Console.WriteLine();
        Console.WriteLine("  global using 也支持别名:");
        Console.WriteLine("    global using StringList = System.Collections.Generic.List<string>;");
        Console.WriteLine("  → 整个项目可直接写 new StringList()");
    }
}
