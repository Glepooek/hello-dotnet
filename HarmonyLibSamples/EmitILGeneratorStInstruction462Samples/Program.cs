using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorStInstruction462Samples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
                new AssemblyName("MyDynamicAssembly"),
                AssemblyBuilderAccess.RunAndSave);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(
                "MyModule1",
                "MyModule1.netmodule");
            TypeBuilder classBuilder = moduleBuilder.DefineType(
                "MyNamespace.MyClass",
                TypeAttributes.Public | TypeAttributes.Class);

            StlocInstructionDemo.Run(classBuilder);

            Type classType = classBuilder.CreateType();
            assemblyBuilder.Save("MyDynamicAssembly.dll");

            #region 测试执行

            StargInstructionDemo.Run();
            Console.WriteLine(StargInstructionDemo.Greet("Yu"));

            object dynamicClassInstance = Activator.CreateInstance(classType);
            object result = classType.GetMethod("Add").Invoke(dynamicClassInstance, new object[] { 20, 22 });
            Console.WriteLine($"Add: {result}"); // 输出：Sum: 42

            StfldInstructionDemo.Run();
            #endregion

            Console.Read();
        }
    }
}
