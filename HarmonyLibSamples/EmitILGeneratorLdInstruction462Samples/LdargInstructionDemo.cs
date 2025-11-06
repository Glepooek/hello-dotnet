using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorLdInstruction462Samples
{
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
    }
}
