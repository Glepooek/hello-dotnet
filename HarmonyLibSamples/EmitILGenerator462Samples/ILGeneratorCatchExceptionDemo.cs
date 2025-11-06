using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGenerator462Samples
{
    internal class ILGeneratorCatchExceptionDemo
    {
        public static void Run(TypeBuilder classBuilder)
        {
            MethodBuilder methodBuilder = classBuilder.DefineMethod(
                     "MyMethod2",
                      MethodAttributes.Public,
                      typeof(void),
                      new Type[] { typeof(int), typeof(string) });

            methodBuilder.DefineParameter(1, ParameterAttributes.None, "id");
            methodBuilder.DefineParameter(2, ParameterAttributes.Optional, "message");

            ILGenerator ilGenerator = methodBuilder.GetILGenerator();

            ilGenerator.BeginExceptionBlock();// 开始 try
            ilGenerator.EmitWriteLine("hello world on try!");

            // 定义本地变量，用于存储格式化后的字符串
            LocalBuilder resultLocal = ilGenerator.DeclareLocal(typeof(string));
            ilGenerator.Emit(OpCodes.Ldstr, "ID: {0}, Message: {1}");
            ilGenerator.Emit(OpCodes.Ldarg_1); // 实例方法获取第一个参数，Ldarg_0是this
            ilGenerator.Emit(OpCodes.Box, typeof(int)); // Box the int argument
            ilGenerator.Emit(OpCodes.Ldarg_2);
            ilGenerator.Emit(OpCodes.Call, typeof(string).GetMethod("Format", new Type[] { typeof(string), typeof(object), typeof(object) }));
            //ilGenerator.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
            // 将结果存储到本地变量
            ilGenerator.Emit(OpCodes.Stloc_0, resultLocal);
            ilGenerator.EmitWriteLine(resultLocal);

            ilGenerator.BeginCatchBlock(typeof(Exception));// 开始 catch
            ilGenerator.EmitWriteLine("hello world on error!");

            ilGenerator.BeginFinallyBlock();// 开始 finally
            ilGenerator.EmitWriteLine("hello world on finally!");

            ilGenerator.EndExceptionBlock();// 结束
            ilGenerator.Emit(OpCodes.Ret);
        }
    }
}
