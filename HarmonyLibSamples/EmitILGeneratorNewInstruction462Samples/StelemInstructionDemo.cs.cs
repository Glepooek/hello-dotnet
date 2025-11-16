using HarmonyPatch.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorNewInstruction462Samples
{
    internal class StelemInstructionDemo
    {
        public static void Run(TypeBuilder classBuilder)
        {
            MethodBuilder methodBuilder = classBuilder.DefineMethod(
                 "CreateValueArray",
                 MethodAttributes.Public | MethodAttributes.Static,
                 typeof(Person[]),
                 Type.EmptyTypes);

            ILGenerator iLGenerator = methodBuilder.GetILGenerator();

            // 创建数组
            iLGenerator.Emit(OpCodes.Ldc_I4, 3);
            iLGenerator.Emit(OpCodes.Newarr, typeof(DateTime));

            iLGenerator.DeclareLocal(typeof(DateTime[]));
            iLGenerator.DeclareLocal(typeof(DateTime));
            iLGenerator.Emit(OpCodes.Stloc_0);

            for (int i = 0; i < 3; i++)
            {
                // 调用 DateTime.Parse 方法创建 DateTime 实例
                MethodInfo parseMethod = typeof(DateTime).GetMethod("Parse", new Type[] { typeof(string) });
                iLGenerator.Emit(OpCodes.Ldstr, DateTime.Now.ToString()); // 传递当前时间字符串
                iLGenerator.Emit(OpCodes.Call, parseMethod);    // 调用 Parse 方法
                iLGenerator.Emit(OpCodes.Stloc_1);

                iLGenerator.Emit(OpCodes.Ldloc_0);// 加载数组
                iLGenerator.Emit(OpCodes.Ldc_I4, i);// 加载索引
                iLGenerator.Emit(OpCodes.Ldloc_1);// 加载DateTime变量

                iLGenerator.Emit(OpCodes.Stelem, typeof(DateTime));// 赋值
            }

            iLGenerator.Emit(OpCodes.Ldloc_0);
            iLGenerator.Emit(OpCodes.Ret);     // 返回该值
        }
    }
}
