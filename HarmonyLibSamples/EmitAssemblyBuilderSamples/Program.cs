using EmitAssemblyBuilderSamples;
using System.Reflection;
using System.Reflection.Emit;

// 创建动态程序集
AssemblyName assemblyName = new AssemblyName("DynamicAssembly");
AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
    assemblyName, 
    AssemblyBuilderAccess.RunAndCollect);

// 创建动态模块
ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("DynamicModule");

// 创建动态类。可以指定继承的父类、接口等
TypeBuilder typeBuilder = moduleBuilder.DefineType(
    "DynamicCalculator", 
    TypeAttributes.Public);

// 创建Add方法
MethodBuilder addMethod = typeBuilder.DefineMethod(
    "Add",
    MethodAttributes.Public | MethodAttributes.Static,
    typeof(int),
    new Type[] { typeof(int), typeof(int) });

// 生成Add方法的IL代码
ILGenerator ilGenerator = addMethod.GetILGenerator();
ilGenerator.Emit(OpCodes.Ldarg_0);  // 加载第一个参数
ilGenerator.Emit(OpCodes.Ldarg_1);  // 加载第二个参数
ilGenerator.Emit(OpCodes.Add);      // 执行加法
ilGenerator.Emit(OpCodes.Ret);      // 返回结果

// 完成类型创建
Type dynamicType = typeBuilder.CreateType();

// 调用动态生成的方法
object? result = dynamicType.InvokeMember(
    "Add",
    BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static,
    null,
    null,
    new object[] { 10, 20 });

Console.WriteLine($"10 + 20 = {result}");  // 输出: 10 + 20 = 30

// ++++++++++++++Roslyn方式生成代码并保存到本地DLL文件++++++++++++++
RoslynCodeGenerator.Main();

Console.ReadLine();
