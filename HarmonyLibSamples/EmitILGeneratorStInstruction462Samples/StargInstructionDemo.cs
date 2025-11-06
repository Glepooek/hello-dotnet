using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorStInstruction462Samples
{
    internal class StargInstructionDemo
    {
        public static void Run()
        {
            DynamicMethod dynamicMethod = new DynamicMethod(
                "Greet",
                typeof(string),
                new[] { typeof(string) },
                typeof(StargInstructionDemo));

            ILGenerator ilGenerator = dynamicMethod.GetILGenerator();

            // 使用 starg 指令将方法参数值传递给局部变量
            ilGenerator.Emit(OpCodes.Ldstr, "Hello, {0}");
            ilGenerator.Emit(OpCodes.Ldarg_0); // 加载第一个参数（name）
            ilGenerator.Emit(OpCodes.Call, typeof(string).GetMethod("Format", new Type[] { typeof(string), typeof(object) }));
            ilGenerator.Emit(OpCodes.Starg, 0); // 将将格式化后的值传递给局部变量

            // 返回局部变量的值
            ilGenerator.Emit(OpCodes.Ldarg_0); // 加载第一个参数（message）
            ilGenerator.Emit(OpCodes.Ret);     // 返回该值

            Func<string, object> func = (Func<string, object>)dynamicMethod.CreateDelegate(typeof(Func<string, object>));
            object result = func("anyu");
            Console.WriteLine(result);
        }

        public static string Greet(string name)
        {
            name = string.Format("Hello, {0}", name);
            return name;
        }
    }
}
