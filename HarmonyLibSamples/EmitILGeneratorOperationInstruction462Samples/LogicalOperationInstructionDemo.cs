using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorOperationInstruction462Samples
{
    /// <summary>
    /// 逻辑运算操作
    /// </summary>
    internal class LogicalOperationInstructionDemo
    {
        public static void Run(TypeBuilder classBuilder)
        {
            MethodBuilder methodBuilder = classBuilder.DefineMethod(
                    "AndInstructionMethod",
                    MethodAttributes.Public | MethodAttributes.Static,
                    CallingConventions.Standard,
                    typeof(int),
                    new Type[] { typeof(int), typeof(int) });
            methodBuilder.DefineParameter(1, ParameterAttributes.None, "num1");
            methodBuilder.DefineParameter(2, ParameterAttributes.None, "num2");

            ILGenerator iLGenerator = methodBuilder.GetILGenerator();
            iLGenerator.Emit(OpCodes.Ldarg_0);
            iLGenerator.Emit(OpCodes.Ldarg_1);
            iLGenerator.Emit(OpCodes.And);
            iLGenerator.Emit(OpCodes.Ret);
        }
    }
}
