using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorOperationInstruction462Samples
{
    internal class CompareInstructionDemo
    {
        public static void Run(TypeBuilder classBuilder)
        {
            // 动态生成方法：判断!=
            MethodBuilder methodBuilder = classBuilder.DefineMethod(
                   "CompareValueTypeNotEqual",
                   MethodAttributes.Public | MethodAttributes.Static,
                   typeof(bool),
                   new[] { typeof(int), typeof(int) });
            methodBuilder.DefineParameter(1, ParameterAttributes.None, "arg1");
            methodBuilder.DefineParameter(2, ParameterAttributes.None, "arg2");

            ILGenerator il = methodBuilder.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0); // 加载第一个 int 参数（a）
            il.Emit(OpCodes.Ldarg_1); // 加载第二个 int 参数（b）
            il.Emit(OpCodes.Ceq);     // 比较 a == b
            il.Emit(OpCodes.Ldc_I4_0);// 加载常数 0
            il.Emit(OpCodes.Ceq);     // 比较 ceq result 与 0
            il.Emit(OpCodes.Ret);     // 返回结果

            // 动态生成方法：判断<=
            methodBuilder = classBuilder.DefineMethod(
                   "CompareValueTypeLessEqual",
                   MethodAttributes.Public | MethodAttributes.Static,
                   typeof(bool),
                   new[] { typeof(int), typeof(int) });
            methodBuilder.DefineParameter(1, ParameterAttributes.None, "arg1");
            methodBuilder.DefineParameter(2, ParameterAttributes.None, "arg2");

            il = methodBuilder.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0); // 加载第一个 int 参数（a）
            il.Emit(OpCodes.Ldarg_1); // 加载第二个 int 参数（b）
            il.Emit(OpCodes.Cgt);     // 比较 a >= b
            il.Emit(OpCodes.Ldc_I4_0);// 加载常数 0
            il.Emit(OpCodes.Ceq);     // 比较 ceq result 与 0
            il.Emit(OpCodes.Ret);     // 返回结果

            // 动态生成方法：判断>=
            methodBuilder = classBuilder.DefineMethod(
                   "CompareValueTypeGreaterEqual",
                   MethodAttributes.Public | MethodAttributes.Static,
                   typeof(bool),
                   new[] { typeof(int), typeof(int) });
            methodBuilder.DefineParameter(1, ParameterAttributes.None, "arg1");
            methodBuilder.DefineParameter(2, ParameterAttributes.None, "arg2");

            il = methodBuilder.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0); // 加载第一个 int 参数（a）
            il.Emit(OpCodes.Ldarg_1); // 加载第二个 int 参数（b）
            il.Emit(OpCodes.Clt);     // 比较 a <= b
            il.Emit(OpCodes.Ldc_I4_0);// 加载常数 0
            il.Emit(OpCodes.Ceq);     // 比较 ceq result 与 0
            il.Emit(OpCodes.Ret);     // 返回结果
        }
    }
}
