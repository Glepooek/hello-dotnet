using EmitILGeneratorBranchInstruction462Samples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorConvInstruction462Samples
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

            CastclassInstructionDemo.Run(classBuilder);
            IsinstInstructionDemo.Run(classBuilder);
            BoxInstructionDemo.Run(classBuilder);
            UnboxInstructionDemo.Run(classBuilder);
            ConvInstructionDemo.Run(classBuilder);

            Type type = classBuilder.CreateType();
            assemblyBuilder.Save($"{assemblyName.Name}.dll");

            #region 测试执行

            var result = type.GetMethod("UnboxInstructionMethod").Invoke(null, new object[] { 11 });
            Console.WriteLine(result);

            result = type.GetMethod("ConvInstructionMethod").Invoke(null, new object[] { 3.1415926 });
            Console.WriteLine(result);
            #endregion

            Console.Read();

        }
    }
}
