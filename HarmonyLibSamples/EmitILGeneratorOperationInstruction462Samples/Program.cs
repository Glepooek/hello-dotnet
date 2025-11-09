using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorOperationInstruction462Samples
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

            LogicalOperationInstructionDemo.Run(classBuilder);
            CompareInstructionDemo.Run(classBuilder);
            //BoxInstructionDemo.Run(classBuilder);
            //UnboxInstructionDemo.Run(classBuilder);
            //ConvInstructionDemo.Run(classBuilder);

            Type type = classBuilder.CreateType();
            assemblyBuilder.Save($"{assemblyName.Name}.dll");

            #region 测试执行

            var result = type.GetMethod("AndInstructionMethod").Invoke(null, new object[] { 12, 0 });
            Console.WriteLine(result);

            result = type.GetMethod("CompareValueTypeNotEqual").Invoke(null, new object[] { 11, 11 });
            Console.WriteLine(result);

            result = type.GetMethod("CompareValueTypeLessEqual").Invoke(null, new object[] { 12, 11 });
            Console.WriteLine(result);

            result = type.GetMethod("CompareValueTypeGreaterEqual").Invoke(null, new object[] { 12, 11 });
            Console.WriteLine(result);
            #endregion

            Console.Read();
        }
    }
}
