using System;

namespace CsharplangDemo73.Demos
{
    public static class RefLocalReassignDemo
    {
        public static void Run()
        {
            // ── C# 7.0: ref 局部变量声明后不能重新指向新目标 ────────────
            // ── C# 7.3: ref 局部变量可以用 ref 重新赋值（重定向目标）────

            int x = 10, y = 20;

            ref int r = ref x;              // r 指向 x
            Console.WriteLine("  r = ref x:  r=" + r + ", x=" + x);

            r = ref y;                      // C# 7.3 新增: 重新指向 y
            Console.WriteLine("  r = ref y:  r=" + r + ", y=" + y);

            r = 99;                         // 修改 r 即修改 y
            Console.WriteLine("  r = 99:     r=" + r + ", y=" + y + " (y 被修改)");
            Console.WriteLine("  x 不变:     x=" + x);

            // ── 在数组中选择性指向元素 ────────────────────────────────
            int[] arr = { 1, 2, 3, 4, 5 };

            ref int chosen = ref arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] > chosen)
                    chosen = ref arr[i];    // 重新指向更大的元素
            }
            chosen = -1;  // 将最大元素改为 -1
            Console.WriteLine("  最大元素改为 -1: [" + string.Join(", ", arr) + "]");

            // ── 条件性选择 ref ────────────────────────────────────────
            int a = 100, b = 200;
            bool useA = false;

            ref int target = ref a;
            if (!useA) target = ref b;      // 根据条件重定向
            target *= 2;

            Console.WriteLine("  useA=false, 修改 b*2: a=" + a + ", b=" + b);

            Console.WriteLine();
            Console.WriteLine("  C# 7.0: ref 局部变量声明后不可重定向");
            Console.WriteLine("  C# 7.3: ref r = ref newTarget 可以重新指向");
        }
    }
}
