using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorBranchInstruction462Samples
{
    internal class ConditionalBranchInstructionDemo
    {
        public static void Run(TypeBuilder classBuilder)
        {
            MethodBuilder methodBuilder = classBuilder.DefineMethod(
                    "ConditionalBranchMethod",
                    MethodAttributes.Public | MethodAttributes.Static,
                    CallingConventions.Standard,
                    typeof(void),
                    new Type[] { typeof(bool) });

            ILGenerator iLGenerator = methodBuilder.GetILGenerator();
            Label labelEnd = iLGenerator.DefineLabel();
            Label labelFalse = iLGenerator.DefineLabel();
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Brfalse, labelFalse);

            iLGenerator.EmitWriteLine("true");
            iLGenerator.Emit(OpCodes.Br, labelEnd);

            iLGenerator.MarkLabel(labelFalse);
            iLGenerator.EmitWriteLine("false");

            iLGenerator.MarkLabel(labelEnd);
            iLGenerator.Emit(OpCodes.Ret);
        }

        public static void Run1(TypeBuilder classBuilder)
        {
            MethodBuilder methodBuilder = classBuilder.DefineMethod(
                    "ConditionalBranchMethod1",
                    MethodAttributes.Public | MethodAttributes.Static,
                    CallingConventions.Standard,
                    typeof(void),
                    new Type[] { typeof(int), typeof(int) });

            ILGenerator iLGenerator = methodBuilder.GetILGenerator();
            Label labelEnd = iLGenerator.DefineLabel();
            Label labelTrue = iLGenerator.DefineLabel();
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldarg_1);
            iLGenerator.Emit(OpCodes.Bge_S, labelTrue);

            iLGenerator.EmitWriteLine("处理小于");
            iLGenerator.Emit(OpCodes.Br, labelEnd);

            iLGenerator.MarkLabel(labelTrue);
            iLGenerator.EmitWriteLine("处理大于等于");

            iLGenerator.MarkLabel(labelEnd);
            iLGenerator.Emit(OpCodes.Ret);
        }
    }
}
