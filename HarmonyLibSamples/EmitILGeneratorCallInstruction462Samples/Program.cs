using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorCallInstruction462Samples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //CallInstructionDemo.Run();
            //EmitCallDemo.Run();

            // 创建动态程序集
            AssemblyName assemblyName = new AssemblyName("MyDynamicAssembly");
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
                assemblyName, AssemblyBuilderAccess.RunAndSave);

            // 创建动态模块
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(
                    "MyModule",
                    "MyModule.netmoudle");

            // 创建动态类
            TypeBuilder classBuilder = moduleBuilder.DefineType(
                "MyNameSpace.MyClass",
                TypeAttributes.Public | TypeAttributes.Class);

            CalliInstructionDemo.Run(classBuilder);

            Type callerType = classBuilder.CreateType();

            assemblyBuilder.Save($"{assemblyName.Name}.dll");

            #region 调用执行

            MethodInfo callMethod = callerType.GetMethod("CallDelegate");
            callMethod.Invoke(null, null);

            CallInstructionDemo.Run();
            #endregion

            Console.ReadLine();
        }
    }
}
