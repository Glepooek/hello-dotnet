using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorBranchInstruction462Samples
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
                    "MyMoudle1",
                    "MyMoudle1.netmoudle");
            TypeBuilder classBuilder = moduleBuilder.DefineType(
                "MyNameSpance.MyClass",
                TypeAttributes.Class);

            ConditionalBranchInstructionDemo.Run(classBuilder);
            ConditionalBranchInstructionDemo.Run1(classBuilder);

            Type type = classBuilder.CreateType();
            assemblyBuilder.Save($"{assemblyName.Name}.dll");

            #region 测试执行

            type.GetMethod("ConditionalBranchMethod").Invoke(null, new object[] { true });
            type.GetMethod("ConditionalBranchMethod1").Invoke(null, new object[] { 10, 20 });

            SwitchBranchInstructionDemo.Run();
            #endregion

            Console.Read();
        }
    }
}
