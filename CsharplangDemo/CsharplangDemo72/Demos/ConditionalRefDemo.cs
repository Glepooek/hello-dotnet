using System;

namespace CsharplangDemo72.Demos
{
    public static class ConditionalRefDemo
    {
        public static void Run()
        {
            // ── C# 7.2: 条件表达式 ?: 可以返回 ref ───────────────────────
            // C# 7.2 之前: ?: 只能返回值，不能返回引用
            // C# 7.2 起: ref (cond ? ref a : ref b) 可以返回其中一个引用

            int x = 10, y = 20;
            bool useX = true;

            // 根据条件选择引用目标（声明时使用条件 ref）
            ref int r = ref (useX ? ref x : ref y);
            Console.WriteLine("  useX=true, r=" + r + " (指向 x=" + x + ")");

            r = 99;  // 修改 r 即修改 x
            Console.WriteLine("  r=99 后: x=" + x + ", y=" + y);

            // 注: ref 赋值（r = ref y）是 C# 7.3 特性，C# 7.2 只支持声明时的条件 ref
            // 条件 ref 每次调用重新选择
            ref int r2 = ref (useX ? ref x : ref y);  // true 选 x
            useX = false;
            ref int r3 = ref (useX ? ref x : ref y);  // false 选 y
            r3 = 88;
            Console.WriteLine("  useX=false, r3=88 后: x=" + x + ", y=" + y);

            // ── 实用场景: 在数组中选择最大/最小元素的引用 ────────────────
            int[] data = { 5, 3, 8, 1, 9, 2 };

            // C# 7.2: 条件 ref 在声明时使用，不支持 ref 重新赋值（7.3 才支持）
            // 用循环找最大值索引，再用条件 ref 演示语法
            int maxIdx = 0;
            for (int i = 1; i < data.Length; i++)
                if (data[i] > data[maxIdx]) maxIdx = i;

            ref int max = ref (data[maxIdx] > 0 ? ref data[maxIdx] : ref data[0]);
            Console.WriteLine("  最大元素(引用)=" + max + " [index=" + maxIdx + "]");
            max = -1;  // 将最大元素原地改为 -1
            Console.Write("  修改后数组: ");
            foreach (int n in data) Console.Write(n + " ");
            Console.WriteLine();

            // ── 与 readonly 结合: ref readonly 条件表达式 ────────────────
            int a = 100, b = 200;
            bool pickA = true;

            ref readonly int ro = ref (pickA ? ref a : ref b);
            Console.WriteLine("  ref readonly 条件: " + ro + " (a=" + a + ")");
            // ro = 999;  // 编译错误: ref readonly 不可修改

            // ── 方法返回 ref 与条件 ref 配合 ──────────────────────────────
            int[] arr1 = { 1, 2, 3 };
            int[] arr2 = { 10, 20, 30 };

            ref int SelectElement(bool fromFirst, int index)
                => ref (fromFirst ? ref arr1[index] : ref arr2[index]);

            ref int elem = ref SelectElement(true, 1);
            Console.WriteLine("  SelectElement(true,1) = " + elem);  // arr1[1] = 2
            elem = 777;
            Console.WriteLine("  修改后 arr1[1] = " + arr1[1]);       // 777

            Console.WriteLine();
            Console.WriteLine("  条件 ref 的核心价值:");
            Console.WriteLine("    • 运行时根据条件选择引用目标，无需复制");
            Console.WriteLine("    • 可用于原地修改条件选择的元素");
            Console.WriteLine("    • 两个分支必须都是 ref，类型必须相同");
        }
    }
}
