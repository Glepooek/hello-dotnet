using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorLdInstruction462Samples
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

    internal class LdargInstructionDemo
    {
        public static void Run(TypeBuilder classBuilder)
        {
            MethodBuilder methodBuilder = classBuilder.DefineMethod(
                    "Add",
                    MethodAttributes.Public,
                    typeof(object),
                    new Type[] { typeof(int), typeof(int) });
            methodBuilder.DefineParameter(1, ParameterAttributes.None, "a");
            methodBuilder.DefineParameter(2, ParameterAttributes.None, "b");

            ILGenerator ilGenerator = methodBuilder.GetILGenerator();
            LocalBuilder localVir = ilGenerator.DeclareLocal(typeof(int));
            // 正确顺序：a+b -> localVir -> 装箱 -> 返回
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Ldarg, 2);
            ilGenerator.Emit(OpCodes.Add);
            ilGenerator.Emit(OpCodes.Stloc, localVir);
            ilGenerator.Emit(OpCodes.Ldloc, localVir);
            ilGenerator.Emit(OpCodes.Box, typeof(int));
            ilGenerator.Emit(OpCodes.Ret);
        }

        public static void Run1(TypeBuilder classBuilder)
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
            il.Emit(OpCodes.Add);            // 相加
            il.Emit(OpCodes.Stind_I4);       // 存储回原地址
            il.Emit(OpCodes.Ret);
        }
    }
}
