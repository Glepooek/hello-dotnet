using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorLdInstruction462Samples
{
    internal class LdelemInstructionDemo
    {
        public static void TestValueTypeArray()
        {
            // 创建动态方法：从 int[] 获取指定索引的元素
            DynamicMethod method = new DynamicMethod(
                "GetIntElement",
                typeof(int),
                new[] { typeof(int[]), typeof(int) },
                typeof(LdelemInstructionDemo).Module);

            ILGenerator il = method.GetILGenerator();

            // 加载数组参数
            il.Emit(OpCodes.Ldarg_0);
            // 加载索引参数
            il.Emit(OpCodes.Ldarg_1);
            // 使用 Ldelem_I4 加载 int 元素（优化指令）
            il.Emit(OpCodes.Ldelem_I4);
            il.Emit(OpCodes.Ret);

            var func = (Func<int[], int, int>)method.CreateDelegate(typeof(Func<int[], int, int>));

            int[] numbers = { 10, 20, 30, 40, 50 };
            int result = func(numbers, 2);
            Console.WriteLine($"int数组[2] = {result}"); // 输出: 30
        }

        public static void TestReferenceTypeArray()
        {
            // 创建动态方法：从 string[] 获取指定索引的元素
            DynamicMethod method = new DynamicMethod(
                "GetStringElement",
                typeof(string),
                new[] { typeof(string[]), typeof(int) },
                typeof(LdelemInstructionDemo).Module);

            ILGenerator il = method.GetILGenerator();

            // 加载数组参数
            il.Emit(OpCodes.Ldarg_0);
            // 加载索引参数
            il.Emit(OpCodes.Ldarg_1);
            // 使用 Ldelem_Ref 加载引用类型元素
            il.Emit(OpCodes.Ldelem_Ref);
            il.Emit(OpCodes.Ret);

            var func = (Func<string[], int, string>)method.CreateDelegate(typeof(Func<string[], int, string>));

            string[] strings = { "apple", "banana", "cherry", "date" };
            string result = func(strings, 1);
            Console.WriteLine($"string数组[1] = {result}"); // 输出: banana
        }
    }
}
