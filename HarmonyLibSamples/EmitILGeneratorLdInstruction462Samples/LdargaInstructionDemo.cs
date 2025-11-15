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
    internal class LdargaInstructionDemo
    {
        //// C# 无法直接写，但 IL 可以模拟：
        //static void Wrapper(int x)
        //{
        //    // 将局部参数 x 的地址传给一个 ref 方法
        //    Increment(ref x);  // ← 这需要 x 的地址！
        //}

        //static void Increment(ref int value)
        //{
        //    value++;
        //}

        public static void Run()
        {
            var assemblyName = new AssemblyName("LdargaDemo");
            var assembly = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            var module = assembly.DefineDynamicModule("Main");

            // 1. 定义一个静态辅助方法：Increment(ref int)
            var helperType = module.DefineType("Helpers", TypeAttributes.Public);
            var incMethod = helperType.DefineMethod(
                "Increment",
                MethodAttributes.Public | MethodAttributes.Static,
                typeof(void),
                new Type[] { typeof(int).MakeByRefType() }
            );
            var ilInc = incMethod.GetILGenerator();
            ilInc.Emit(OpCodes.Ldarg_0);        // ref int
            ilInc.Emit(OpCodes.Dup);
            ilInc.Emit(OpCodes.Ldind_I4);
            ilInc.Emit(OpCodes.Ldc_I4_1);
            ilInc.Emit(OpCodes.Add);
            ilInc.Emit(OpCodes.Stind_I4);
            ilInc.Emit(OpCodes.Ret);
            var helperTypeReal = helperType.CreateType();
            var incrementRefMethod = helperTypeReal.GetMethod("Increment");

            // 2. 定义主方法：Wrapper(int x)，内部调用 Increment(ref x)
            var mainType = module.DefineType("MainType", TypeAttributes.Public);
            var wrapperMethod = mainType.DefineMethod(
                "Wrapper",
                MethodAttributes.Public | MethodAttributes.Static,
                typeof(void),
                new Type[] { typeof(int) } // 参数是普通 int（值类型）
            );

            ILGenerator il = wrapperMethod.GetILGenerator();

            // ========== 关键：使用 Ldarga 获取参数 x 的地址 ==========
            il.Emit(OpCodes.Ldarga_S, 0);   // ← Ldarga.0: 取参数 x 的地址（&x）
            il.Emit(OpCodes.Call, incrementRefMethod); // 调用 Increment(ref int)

            // 读取 x 的新值并打印
            il.Emit(OpCodes.Ldarga_S, 0);   // &x
            il.Emit(OpCodes.Ldind_I4);            // x = *(&x)
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(int) }));

            il.Emit(OpCodes.Ret);

            var mainTypeReal = mainType.CreateType();
            var wrapper = mainTypeReal.GetMethod("Wrapper");

            Console.WriteLine("调用 Wrapper(5)...");
            wrapper.Invoke(null, new object[] { 5 });
            Console.WriteLine("Wrapper 执行完成（内部 x 被修改，但外部不可见）");
        }
    }
}
