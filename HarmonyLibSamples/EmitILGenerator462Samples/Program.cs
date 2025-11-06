using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGenerator462Samples
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

            ILGeneratorWriteLineDemo.Run(classBuilder);
            ILGeneratorCatchExceptionDemo.Run(classBuilder);
            ILGeneratorThrowExceptionDemo.Run(classBuilder);
            ILGeneratorLocalBuilderDemo.Run(classBuilder);

            Type classType = classBuilder.CreateType();

            assemblyBuilder.Save($"{assemblyName.Name}.dll");

            #region 测试执行

            object dynamicClassInstance = Activator.CreateInstance(classType);
            classType.GetMethod("MyMethod1").Invoke(dynamicClassInstance, new object[] { 42, "Hello from EmitWriteLine!" });

            classType.GetMethod("MyMethod2").Invoke(dynamicClassInstance, new object[] { 42, "Hello on try..catch..finally!" });

            object result = classType.GetMethod("MyMethod4").Invoke(dynamicClassInstance, new object[] { 1, 13 });
            Console.WriteLine($"MyMethod4 Result: {result}");
            #endregion

            Console.ReadLine();
        }
    }
}
