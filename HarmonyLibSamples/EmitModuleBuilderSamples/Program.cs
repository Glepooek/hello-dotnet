using System.Reflection;
using System.Reflection.Emit;

// +++++++++++++++++++++++++++++++++
// 注意：
// .NET Core /.NET 5+ 中不支持保存模块到磁盘，
// 在同一动态程序集中不支持创建多个模块。
// +++++++++++++++++++++++++++++++++

// 1. 创建动态程序集
AssemblyName assemblyName = new AssemblyName("DynamicMultiModuleAssembly");
AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
    assemblyName, 
    AssemblyBuilderAccess.Run);

// 2. 为程序集创建第一个模块（主模块）
ModuleBuilder module1 = assemblyBuilder.DefineDynamicModule("Module1");
TypeBuilder type1 = module1.DefineType("TypeInModule1", TypeAttributes.Public);
MethodBuilder methodBuilder1 = type1.DefineMethod("SayHello", 
    MethodAttributes.Public | MethodAttributes.Static,
    typeof(void), 
    Type.EmptyTypes);
ILGenerator il1 = methodBuilder1.GetILGenerator();
il1.Emit(OpCodes.Ldstr, "Hello from Module1!");
il1.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new[] { typeof(string) }));
il1.Emit(OpCodes.Ret);
Type finishedType1 = type1.CreateType();

//// 3. 为程序集创建第二个模块
//ModuleBuilder module2 = assemblyBuilder.DefineDynamicModule("Module2");
//TypeBuilder type2 = module2.DefineType("TypeInModule2", TypeAttributes.Public);
//MethodBuilder methodBuilder2 = type2.DefineMethod("SayHi", 
//    MethodAttributes.Public | MethodAttributes.Static,
//    typeof(void), 
//    Type.EmptyTypes);
//ILGenerator il2 = methodBuilder2.GetILGenerator();
//il2.Emit(OpCodes.Ldstr, "Hi from Module2!");
//il2.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new[] { typeof(string) }));
//il2.Emit(OpCodes.Ret);
//Type finishedType2 = type2.CreateType();

// 4. 调用两个模块中的方法
finishedType1.InvokeMember("SayHello",
    BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static,
    null, null, null); // 输出：Hello from Module1!

//finishedType2.InvokeMember("SayHi",
//    BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static,
//    null, null, null); // 输出：Hi from Module2!

// 验证模块数量
Console.WriteLine($"程序集中的模块数：{assemblyBuilder.GetModules().Length}"); // 输出：2

Console.ReadLine();