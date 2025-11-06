using EmitILGeneratorLdInstruction462Samples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
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

            TypeBuilder classBuilder = moduleBuilder.DefineType(
                    "MyNamespace.MyClass",
                    TypeAttributes.Public | TypeAttributes.Class);

            LdargInstructionDemo.Run(classBuilder);
            LdargaInstructionDemo.Run(classBuilder);
            LdtokenInstructionDemo.Run(classBuilder);
            //ILGeneratorLocalBuilderDemo.Run(classBuilder);

            Type classType = classBuilder.CreateType();

            assemblyBuilder.Save($"{assemblyName.Name}.dll");

            #region 测试执行

            object dynamicClassInstance = Activator.CreateInstance(classType);
            object result = classType.GetMethod("Add").Invoke(dynamicClassInstance, new object[] { 42, 0 });
            Console.WriteLine($"Add Result: {result}");

            int x = 5;
            object[] parameters = { x };
            classType.GetMethod("Increment").Invoke(null, parameters);
            Console.WriteLine($"Increment Result: {parameters[0]}"); // 输出：6

            LdlocaInstructionDemo.Run();
            LdfldInstructionDemo.Run();

            object result2 = classType.GetMethod("GetTypeToken").Invoke(null, new object[] { typeof(bool) });
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
