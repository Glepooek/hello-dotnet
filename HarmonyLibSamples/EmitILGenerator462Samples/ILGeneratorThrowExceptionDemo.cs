using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGenerator462Samples
{
    internal class ILGeneratorThrowExceptionDemo
    {
        public static void Run(TypeBuilder classBuilder)
        {
            MethodBuilder methodBuilder = classBuilder.DefineMethod(
                     "MyMethod3",
                      MethodAttributes.Public,
                      typeof(void),
                      Type.EmptyTypes);
            ILGenerator ilGenerator = methodBuilder.GetILGenerator();
            //ilGenerator.ThrowException(typeof(InvalidOperationException));
            ilGenerator.Emit(OpCodes.Ldstr, "这是一个异常示例消息");
            // 创建一个新的异常对象
            ConstructorInfo exceptionConstructor = typeof(InvalidOperationException).GetConstructor(new Type[] { typeof(string) });
            ilGenerator.Emit(OpCodes.Newobj, exceptionConstructor);
            // 抛出异常
            ilGenerator.Emit(OpCodes.Throw);
            ilGenerator.Emit(OpCodes.Ret);
        }
    }
}
