using EmitILGeneratorLdInstruction462Samples;
using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorLdInstruction462Samples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AssemblyName assemblyName = new AssemblyName("MyDynamicAssembly");
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
                assemblyName,
                AssemblyBuilderAccess.RunAndSave);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(
                    "Mudule1",
                    "Module1.netmocule");

            #region Class

            TypeBuilder classBuilder = moduleBuilder.DefineType(
                    "MyNamespace.MyClass",
                    TypeAttributes.Public | TypeAttributes.Class);

            LdargInstructionDemo.Run(classBuilder);
            LdargInstructionDemo.Run1(classBuilder);
            LdtokenInstructionDemo.Run(classBuilder);

            Type classType = classBuilder.CreateType();

            #endregion

            #region Struct

            // 定义一个 struct: Point { public int value; }
            var structBuilder = moduleBuilder.DefineType(
                    "Point",
                    TypeAttributes.Public | TypeAttributes.SequentialLayout,
                    typeof(ValueType));
            FieldBuilder fieldBuilder = structBuilder.DefineField("value", typeof(int), FieldAttributes.Public);

            var getMethod = structBuilder.DefineMethod(
                      "GetValue",
                      MethodAttributes.Public,
                      typeof(int),
                      Type.EmptyTypes);
            var ilGet = getMethod.GetILGenerator();
            ilGet.Emit(OpCodes.Ldarg_0);     // 加载 this（struct 副本，读取安全）
            ilGet.Emit(OpCodes.Ldflda, fieldBuilder);
            ilGet.Emit(OpCodes.Ldind_I4);
            ilGet.Emit(OpCodes.Ret);

            LdfldaInstructionDemo.Run(structBuilder, fieldBuilder);

            Type pointType = structBuilder.CreateType();

            #endregion

            assemblyBuilder.Save($"{assemblyName.Name}.dll");

            #region 测试执行

            Console.WriteLine("++++++++++++++++++Ldarg指令++++++++++++++++:");
            object dynamicClassInstance = Activator.CreateInstance(classType);
            object result = classType.GetMethod("Add").Invoke(dynamicClassInstance, new object[] { 42, 0 });
            Console.WriteLine($"Add Result: {result}");

            int x = 5;
            object[] parameters = { x };
            classType.GetMethod("Increment").Invoke(null, parameters);
            Console.WriteLine($"Increment Result: {parameters[0]}"); // 输出：6

            Console.WriteLine("++++++++++++++++++Ldarga指令++++++++++++++++:");
            LdargaInstructionDemo.Run();

            Console.WriteLine("++++++++++++++++++Ldloca指令++++++++++++++++:");
            LdlocaInstructionDemo.Run();

            
            Console.WriteLine("++++++++++++++++++Ldfld指令++++++++++++++++:");
            LdfldInstructionDemo.Run();

            Console.WriteLine("++++++++++++++++++Ldflda指令++++++++++++++++:");
            object counter = Activator.CreateInstance(pointType);
            var getValue = pointType.GetMethod("GetValue");
            var increment = pointType.GetMethod("Increment");

            Console.WriteLine($"Before: {getValue.Invoke(counter, null)}");
            increment.Invoke(counter, null);
            Console.WriteLine($"After: {getValue.Invoke(counter, null)}");


            Console.WriteLine("++++++++++++++++++Ldtoken指令++++++++++++++++:");
            object result2 = classType.GetMethod("GetTypeToken").Invoke(null, new object[] { typeof(string) });
            Type type = Type.GetTypeFromHandle((RuntimeTypeHandle)result2);
            Console.WriteLine($"GetTypeToken Result: {type.Name}");

            LdtokenInstructionDemo.CreateTypeChecker();

            Console.WriteLine("++++++++++++++++++Ldelem指令++++++++++++++++:");
            LdelemInstructionDemo.TestValueTypeArray();
            LdelemInstructionDemo.TestReferenceTypeArray();
            #endregion

            Console.ReadLine();
        }
    }
}
