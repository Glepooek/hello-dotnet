using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorLdInstruction452Samples
{
    //// 接收ref参数，将其值加1
    //static void Increment(ref int num)
    //{
    //    num++; // 等价于 num = num + 1，需要修改原变量的地址
    //}

    //static void Main()
    //{
    //    int x = 5;
    //    Increment(ref x);
    //    Console.WriteLine(x); // 输出：6
    //}

    internal class LdargaInstructionDemo
    {
        public static void Run(TypeBuilder classBuilder)
        {
            // 定义Increment方法：参数为ref int，返回void
            MethodBuilder methodBuilder = classBuilder.DefineMethod(
                "Increment",
                MethodAttributes.Public | MethodAttributes.Static,
                CallingConventions.Standard,
                typeof(void),
                new[] { typeof(int).MakeByRefType() }
            );
            methodBuilder.DefineParameter(1, ParameterAttributes.None, "num");

            // 使用ILGenerator生成IL指令
            ILGenerator il = methodBuilder.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);        // 加载参数地址（ref int）
            il.Emit(OpCodes.Dup);            // 复制地址
            il.Emit(OpCodes.Ldind_I4);       // 间接加载int值
            il.Emit(OpCodes.Ldc_I4_1);       // 加载常量1
            il.Emit(OpCodes.Add);             // 相加
            il.Emit(OpCodes.Stind_I4);       // 存储回原地址
            il.Emit(OpCodes.Ret);

            il.Emit(OpCodes.Ret);
        }
    }
}
