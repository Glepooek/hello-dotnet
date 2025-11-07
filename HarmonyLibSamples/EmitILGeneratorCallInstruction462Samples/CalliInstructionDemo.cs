using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorCallInstruction462Samples
{
    public class CalliInstructionDemo
    {
        public static void PrintHello()
        {
            Console.WriteLine("Hello, world!");
        }

        public static void Run(TypeBuilder classBuilder)
        {
            MethodBuilder methodBuilder = classBuilder.DefineMethod(
                        "CallDelegate",
                        MethodAttributes.Public | MethodAttributes.Static,
                        CallingConventions.Standard,
                        typeof(void),
                        Type.EmptyTypes);

            ILGenerator iLGenerator = methodBuilder.GetILGenerator();
            // 加载一个委托实例到栈上
            iLGenerator.Emit(OpCodes.Ldftn, typeof(CalliInstructionDemo).GetMethod("PrintHello", Type.EmptyTypes));
            // 使用 Calli 指令调用委托所指向的方法
            iLGenerator.EmitCalli(OpCodes.Calli, CallingConventions.Standard, typeof(void), null, null);
            iLGenerator.Emit(OpCodes.Ret);     // 返回该值
        }
    }
}
