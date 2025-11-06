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
    internal class NewarrInstructionDemo
    {
        public static void Run(TypeBuilder classBuilder)
        {
            MethodBuilder methodBuilder = classBuilder.DefineMethod(
                "CreateArray",
                MethodAttributes.Public | MethodAttributes.Static,
                typeof(Person[]),
                new Type[] { typeof(int) });
            methodBuilder.DefineParameter(1, ParameterAttributes.None, "length");

            ILGenerator ilGenerator = methodBuilder.GetILGenerator();
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Newarr, typeof(Person));
            ilGenerator.Emit(OpCodes.Ret);
        }
    }
}
