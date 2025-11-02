using System;
using System.Reflection;
using System.Reflection.Emit;

AssemblyName assemblyName = new AssemblyName("MyDynamicAssembly");
AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("Mudule1");

TypeBuilder classBuilder = moduleBuilder.DefineType(
        "MyNamespace.MyClass",
        TypeAttributes.Public | TypeAttributes.Class);
MethodBuilder methodBuilder = classBuilder.DefineMethod(
         "MyMethod",
          MethodAttributes.Public,
          typeof(void),
          new Type[] { typeof(int), typeof(string) });

methodBuilder.DefineParameter(1, ParameterAttributes.None, "id");
methodBuilder.DefineParameter(2, ParameterAttributes.Optional, "message");

ILGenerator ilGenerator = methodBuilder.GetILGenerator();
// 定义本地变量，用于存储格式化后的字符串
LocalBuilder resultLocal = ilGenerator.DeclareLocal(typeof(string));
ilGenerator.Emit(OpCodes.Ldstr, "ID: {0}, Message: {1}");
ilGenerator.Emit(OpCodes.Ldarg_1); // 实例方法获取第一个参数，Ldarg_0是this
ilGenerator.Emit(OpCodes.Box, typeof(int)); // Box the int argument
ilGenerator.Emit(OpCodes.Ldarg_2);
ilGenerator.Emit(OpCodes.Call, typeof(string).GetMethod("Format", new Type[] { typeof(string), typeof(object), typeof(object) }));
//ilGenerator.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
// 将结果存储到本地变量
ilGenerator.Emit(OpCodes.Stloc_0, resultLocal);
ilGenerator.EmitWriteLine(resultLocal);
ilGenerator.Emit(OpCodes.Ret);

Type classType = classBuilder.CreateType();

object dynamicClassInstance = Activator.CreateInstance(classType);
classType.GetMethod("MyMethod").Invoke(dynamicClassInstance, new object[] { 42, "Hello from dynamic method!" });


Console.ReadLine();