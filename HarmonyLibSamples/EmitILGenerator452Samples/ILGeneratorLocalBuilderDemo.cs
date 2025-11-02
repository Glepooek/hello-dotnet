using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGenerator452Samples
{
    internal class ILGeneratorLocalBuilderDemo
    {
        public static void Run(TypeBuilder classBuilder)
        {
            MethodBuilder methodBuilder = classBuilder.DefineMethod(
                     "MyMethod4",
                      MethodAttributes.Public,
                      typeof(int),
                      new Type[] { typeof(int), typeof(int) });
            methodBuilder.DefineParameter(1, ParameterAttributes.None, "arg1");
            methodBuilder.DefineParameter(2, ParameterAttributes.None, "arg2");

            ILGenerator ilGenerator = methodBuilder.GetILGenerator();
            // 定义一个局部变量
            LocalBuilder localVar = ilGenerator.DeclareLocal(typeof(int));
            // 定义一个标签用于条件跳转
            Label returnLabel = ilGenerator.DefineLabel();

            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Ldarg, 2);
            ilGenerator.Emit(OpCodes.Add);
            ilGenerator.Emit(OpCodes.Stloc, localVar);

            // 加载局部变量的值以便返回或使用
            ilGenerator.Emit(OpCodes.Ldloc, localVar);
            ilGenerator.Emit(OpCodes.Ldc_I4, 25);
            // 判断局部变量的值是否大于25，大于25则返回25，否则返回局部变量的值
            ilGenerator.Emit(OpCodes.Bgt_S, returnLabel);

            // else: return localVar
            ilGenerator.Emit(OpCodes.Ldloc, localVar);      // [localVar]
            ilGenerator.Emit(OpCodes.Ret);                  // return localVar

            // returnLabel: return 25
            ilGenerator.MarkLabel(returnLabel);
            ilGenerator.Emit(OpCodes.Ldc_I4, 25);           // [25]
            ilGenerator.Emit(OpCodes.Ret);
        }
    }
}
