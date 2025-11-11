using EmitMethodBuilderSamples;
using System;
using System.Reflection;
using System.Reflection.Emit;

AssemblyName assemblyName = new AssemblyName("MyDynamicAssembly");
AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
        assemblyName,
        AssemblyBuilderAccess.RunAndCollect);

ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("Mudule1");

TypeBuilder classBuilder = moduleBuilder.DefineType(
        "MyNamespace.MyClass",
        TypeAttributes.Public | TypeAttributes.Class);

MethodBuilder methodBuilder = classBuilder.DefineMethod(
       "MyMethod",
        MethodAttributes.Public,/**定义实例方法**/
        /**MethodAttributes.Public | MethodAttributes.Static 定义类方法,**/
        typeof(string),
        new Type[] { typeof(int), typeof(string) });
methodBuilder.DefineParameter(1, ParameterAttributes.None, "id");
methodBuilder.DefineParameter(2, ParameterAttributes.None, "message");

#region 实例方法IL
ILGenerator ilGenerator = methodBuilder.GetILGenerator();
// 字符串格式化：{0}, hello, {1}
ilGenerator.Emit(OpCodes.Ldstr, "{0}, Hello, {1}");
ilGenerator.Emit(OpCodes.Ldarg_1); // 实例方法获取第一个参数，Ldarg_0是this
ilGenerator.Emit(OpCodes.Box, typeof(int)); // Box the int argument
ilGenerator.Emit(OpCodes.Ldarg_2);
//string.Format("{0}, Hello, {1}", id, message);
ilGenerator.Emit(OpCodes.Call, typeof(string).GetMethod("Format", new Type[] { typeof(string), typeof(object), typeof(object) }));
ilGenerator.Emit(OpCodes.Ret);
#endregion

#region 类方法IL

//ILGenerator ilGenerator = methodBuilder.GetILGenerator();
//// 字符串格式化：{0}, hello, {1}
//ilGenerator.Emit(OpCodes.Ldstr, "{0}, Hello, {1}");
//ilGenerator.Emit(OpCodes.Ldarg_0); // 类方法获取第一个参数，Ldarg_0是第一个参数
//ilGenerator.Emit(OpCodes.Box, typeof(int)); // Box the int argument
//ilGenerator.Emit(OpCodes.Ldarg_1);
////string.Format("{0}, Hello, {1}", id, message);
//ilGenerator.Emit(OpCodes.Call, typeof(string).GetMethod("Format", new Type[] { typeof(string), typeof(object), typeof(object) }));
//ilGenerator.Emit(OpCodes.Ret);

#endregion

Type classType = classBuilder.CreateType();

#region 测试执行

object dynamicClassInstance = Activator.CreateInstance(classType);
// 调用实例方法
//object result = classType.GetMethod("MyMethod").Invoke(dynamicClassInstance, new object[] { 99, "World" });
object result = classType.InvokeMember(
    "MyMethod", 
    BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, 
    null,
    dynamicClassInstance, 
    new object[] { 99, "World" });

// 调用类方法
//object result = classType.GetMethod("MyMethod").Invoke(null, new object[] { 99, "World" });
Console.WriteLine(result); // 输出结果：Hello, World

#endregion

DynamicMethodDemo.Run();
DynamicMethodDemo.Run1();

Console.ReadLine();