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
    internal class NewobjInstructionDemo
    {
        public static void Run(TypeBuilder classBuilder)
        {
            //MethodBuilder methodBuilder = classBuilder.DefineMethod(
            //    "CreateObj",
            //    MethodAttributes.Public | MethodAttributes.Static,
            //    typeof(Person),
            //    Type.EmptyTypes);

            //ILGenerator ilGenerator = methodBuilder.GetILGenerator();
            //ilGenerator.Emit(OpCodes.Newobj, typeof(Person).GetConstructor(Type.EmptyTypes));
            //ilGenerator.Emit(OpCodes.Ret);

            MethodBuilder methodBuilder = classBuilder.DefineMethod(
                "CreateObj",
                MethodAttributes.Public | MethodAttributes.Static,
                typeof(Person),
                new Type[] { typeof(string), typeof(int) });
            methodBuilder.DefineParameter(1, ParameterAttributes.None, "name");
            methodBuilder.DefineParameter(2, ParameterAttributes.None, "age");

            ILGenerator ilGenerator = methodBuilder.GetILGenerator();
            ConstructorInfo constructorInfo = typeof(Person).GetConstructor(
                BindingFlags.Public | BindingFlags.Instance, 
                null, 
                new Type[] { typeof(string), typeof(int) }, 
                null);
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Newobj, constructorInfo);
            ilGenerator.Emit(OpCodes.Ret);
        }
    }
}
