using HarmonyPatch.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorBranchInstruction462Samples
{
    internal class UnboxInstructionDemo
    {
        public static void Run(TypeBuilder classBuilder)
        {
            MethodBuilder methodBuilder = classBuilder.DefineMethod(
                    "UnboxInstructionMethod",
                    MethodAttributes.Public | MethodAttributes.Static,
                    CallingConventions.Standard,
                    typeof(int),
                    new Type[] { typeof(object) });
            methodBuilder.DefineParameter(1, ParameterAttributes.None, "obj");

            ILGenerator iLGenerator = methodBuilder.GetILGenerator();
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Unbox, typeof(int));
            iLGenerator.Emit(OpCodes.Ldobj, typeof(int));
            //iLGenerator.Emit(OpCodes.Unbox_Any, typeof(int));
            iLGenerator.Emit(OpCodes.Ret);
        }
    }
}
