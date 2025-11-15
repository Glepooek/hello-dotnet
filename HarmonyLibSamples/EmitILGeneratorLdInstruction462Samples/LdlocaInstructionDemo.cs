using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorLdInstruction462Samples
{
    internal class LdlocaInstructionDemo
    {
        public static void Run()
        {
            Type dicType = typeof(Dictionary<string, string>);
            MethodInfo tryGetValue = dicType.GetMethod("TryGetValue");

            DynamicMethod method = new DynamicMethod(
                "GetValue", 
                typeof(object), 
                new[] { typeof(Dictionary<string, string>), typeof(string) }, 
                typeof(Dictionary<string, string>));
            ILGenerator il = method.GetILGenerator();
            LocalBuilder outText = il.DeclareLocal(typeof(string));

            il.Emit(OpCodes.Ldarg_0);// Load the dic object onto the stack
            il.Emit(OpCodes.Ldarg_1);// Load key
            il.Emit(OpCodes.Ldloca_S, outText);// 使用地址变量来接收： out值
            // Callvirt指令用于调用实例方法（如 TryGetValue），它会在运行时检查对象是否为空（null），比Call指令更安全。
            // 调用前必须按顺序压入所有参数：实例引用（dic）、键（key）、out参数地址（&outText）
            il.Emit(OpCodes.Callvirt, tryGetValue);//bool a=dic.tryGetValue(...,out value)
            il.Emit(OpCodes.Pop);//不需要执行的bool返回值

            il.Emit(OpCodes.Ldloc_0);//加载 out 变量的值。
            il.Emit(OpCodes.Ret); // Return the value

            Func<Dictionary<string, string>, string, object> func = (Func<Dictionary<string, string>, string, object>)method.CreateDelegate(typeof(Func<Dictionary<string, string>, string, object>));

            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "a", "aaa" }
            };
            object result = func(dic, "a");
            Console.WriteLine(result);
        }
    }
}
