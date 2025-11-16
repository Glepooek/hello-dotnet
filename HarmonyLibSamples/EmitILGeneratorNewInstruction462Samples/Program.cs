using HarmonyPatch.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorNewInstruction462Samples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AssemblyName assName = new AssemblyName("myAssembly")
            {
                Version = new Version("1.1.1.2")
            };
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
                assName,
                AssemblyBuilderAccess.RunAndSave);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(
                "MyModule", 
             "MyModule.netmodule");
            TypeBuilder classBuilder = moduleBuilder.DefineType(
                "MyNameSpace.MyClass",
                TypeAttributes.Public | TypeAttributes.Class);

            NewobjInstructionDemo.Run(classBuilder);
            NewarrInstructionDemo.Run(classBuilder);
            StelemRefInstructionDemo.Run(classBuilder);
            StelemInstructionDemo.Run(classBuilder);

            Type myClassType = classBuilder.CreateType();
            assemblyBuilder.Save("myAssembly.dll");

            Console.Read();

        }
    }
}
