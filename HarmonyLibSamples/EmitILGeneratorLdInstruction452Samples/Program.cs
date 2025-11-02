using EmitILGeneratorLdInstruction452Samples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorLdInstructionSamples
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
            //ILGeneratorThrowExceptionDemo.Run(classBuilder);
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
            #endregion

            Console.ReadLine();
        }
    }
}
