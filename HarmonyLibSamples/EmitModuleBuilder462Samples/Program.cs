using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace EmitModuleBuilder462Samples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. 创建程序集（使用 RunAndSave 模式，支持模块文件名）
            AssemblyName assemblyName = new AssemblyName("MyAssembly")
            {
                Version = new Version(1, 0, 20, 0)
            };
            // 注意：使用 RunAndSave 模式，允许模块指定文件名
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
                assemblyName, AssemblyBuilderAccess.RunAndSave);

            // 2. 定义模块时指定文件名（关键：即使不保存，也强制持元数据关联）
            ModuleBuilder moduleBuilder1 = assemblyBuilder.DefineDynamicModule(
                "Module1", "Module1.netmodule"); // 指定文件名
            ModuleBuilder moduleBuilder2 = assemblyBuilder.DefineDynamicModule(
                "Module2", "Module2.netmodule"); // 提前定义第二个模块

            // 3. 在 Module1 中定义类型并生成
            TypeBuilder typeBuilder1 = moduleBuilder1.DefineType(
                "MyNameSpace.MyClass",
                TypeAttributes.Public | TypeAttributes.BeforeFieldInit);
            MethodBuilder methodBuilder1 = typeBuilder1.DefineMethod(
                "MyMethod",
                MethodAttributes.Public | MethodAttributes.Static,
                typeof(void),
                null);
            ILGenerator il1 = methodBuilder1.GetILGenerator();
            il1.EmitWriteLine("Hello from Module1!");
            il1.Emit(OpCodes.Ret);

            Type myClassType = typeBuilder1.CreateType();
            MethodInfo myMethod = myClassType.GetMethod("MyMethod");

            // 4. 在 Module2 中定义类型，引用 Module1 的方法
            TypeBuilder typeBuilder2 = moduleBuilder2.DefineType(
                "MyNameSpace.MyClass2",
                TypeAttributes.Public | TypeAttributes.BeforeFieldInit);
            MethodBuilder methodBuilder2 = typeBuilder2.DefineMethod(
                "MyMethod2",
                MethodAttributes.Public | MethodAttributes.Static,
                typeof(void),
                null);
            ILGenerator il2 = methodBuilder2.GetILGenerator();
            il2.EmitWriteLine("Hello from Module2!");
            il2.EmitCall(OpCodes.Call, myMethod, null); // 跨模块调用
            il2.Emit(OpCodes.Ret);
            Type myClass2Type = typeBuilder2.CreateType();

            // 5. 调用方法
            MethodInfo myMethod2 = myClass2Type.GetMethod("MyMethod2");
            myMethod2.Invoke(null, null);

            // 6. 保存程序集和模块到磁盘
            assemblyBuilder.Save($"{assemblyName.Name}.dll");

            Console.WriteLine($"\n程序集中的模块数：{assemblyBuilder.GetLoadedModules().Length}"); // 输出：3
            assemblyBuilder.GetLoadedModules()?.ToList()?.ForEach(module =>
            {
                Console.WriteLine($"模块名称: {module.ScopeName}");
            });

            Console.Read();
        }
    }
}