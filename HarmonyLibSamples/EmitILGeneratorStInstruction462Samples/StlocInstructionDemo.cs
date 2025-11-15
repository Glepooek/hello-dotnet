using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorStInstruction462Samples
{
    internal class StlocInstructionDemo
    {
        public static void Run(TypeBuilder classBuilder)
        {
            MethodBuilder methodBuilder = classBuilder.DefineMethod(
                "Add",
                MethodAttributes.Public,
                typeof(int),
                new[] { typeof(int), typeof(int) });
            methodBuilder.DefineParameter(1, ParameterAttributes.None, "a");
            methodBuilder.DefineParameter(2, ParameterAttributes.None, "b");

            ILGenerator ilGenerator = methodBuilder.GetILGenerator();
            // 定义局部变量
            LocalBuilder result = ilGenerator.DeclareLocal(typeof(int));
            ilGenerator.Emit(OpCodes.Ldarg_1); // 加载第一个参数
            ilGenerator.Emit(OpCodes.Ldarg_2); // 加载第二个参数
            ilGenerator.Emit(OpCodes.Add);      // 执行加法操作
            ilGenerator.Emit(OpCodes.Stloc, result); // 将结果存储到局部变量
            // 加载局部变量并返回结果
            ilGenerator.Emit(OpCodes.Ldloc, result);
            ilGenerator.Emit(OpCodes.Ret);
        }
    }
}
