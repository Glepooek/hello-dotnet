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
    internal class StelemRefInstructionDemo
    {
        public static void Run(TypeBuilder classBuilder)
        {
            MethodBuilder methodBuilder = classBuilder.DefineMethod(
                "CreateRefArray",
                MethodAttributes.Public | MethodAttributes.Static,
                typeof(Person[]),
                new Type[] { });

            ILGenerator iLGenerator = methodBuilder.GetILGenerator();

            //创建数组
            iLGenerator.Emit(OpCodes.Ldc_I4, 3);
            iLGenerator.Emit(OpCodes.Newarr, typeof(Person));

            iLGenerator.DeclareLocal(typeof(Person[]));
            iLGenerator.DeclareLocal(typeof(Person));
            iLGenerator.Emit(OpCodes.Stloc_0);// 将数组存储到index为0的本地变量中

            for (int i = 0; i < 3; i++)
            {
                iLGenerator.Emit(OpCodes.Newobj, typeof(Person).GetConstructor(Type.EmptyTypes));
                iLGenerator.Emit(OpCodes.Stloc_1);// 将对象存储到index为1的本地变量中

                iLGenerator.Emit(OpCodes.Ldloc_0);// 加载数组
                iLGenerator.Emit(OpCodes.Ldc_I4, i);// 加载索引
                iLGenerator.Emit(OpCodes.Ldloc_1);// 加载Entity

                iLGenerator.Emit(OpCodes.Stelem_Ref);//引用类型赋值
            }

            iLGenerator.Emit(OpCodes.Ldloc_0);//加载数组
            iLGenerator.Emit(OpCodes.Ret);     // 返回该值
        }
    }
}
