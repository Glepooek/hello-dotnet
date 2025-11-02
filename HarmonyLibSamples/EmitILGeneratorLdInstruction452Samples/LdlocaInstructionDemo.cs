using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorLdInstruction452Samples
{
    internal class LdlocaInstructionDemo
    {
        public static void Run()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("a", "aaa");
            var dicType = typeof(Dictionary<string, string>);
            MethodInfo getValue = dicType.GetMethod("TryGetValue");

            DynamicMethod method = new DynamicMethod(
                "GetValue", 
                typeof(object), 
                new[] { typeof(Dictionary<string, string>), typeof(string) }, 
                typeof(Dictionary<string, string>));
            ILGenerator il = method.GetILGenerator();
            LocalBuilder outText = il.DeclareLocal(typeof(string));

            il.Emit(OpCodes.Ldarg_0); // Load the dic object onto the stack
            il.Emit(OpCodes.Ldarg_1);//设置字段名。
            il.Emit(OpCodes.Ldloca_S, outText);// 使用地址变量来接收： out值
            il.Emit(OpCodes.Callvirt, getValue);//bool a=dic.tryGetValue(...,out value)
            il.Emit(OpCodes.Pop);//不需要执行的bool返回值

            il.Emit(OpCodes.Ldloc_0);//加载 out 变量的值。
            il.Emit(OpCodes.Ret); // Return the value

            Func<Dictionary<string, string>, string, object> func = (Func<Dictionary<string, string>, string, object>)method.CreateDelegate(typeof(Func<Dictionary<string, string>, string, object>));

            object result = func(dic, "a");
            Console.WriteLine(result);
        }
    }
}
