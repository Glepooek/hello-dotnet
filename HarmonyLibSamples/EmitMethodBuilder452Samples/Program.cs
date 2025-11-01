using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitMethodBuilder452Samples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AssemblyName assemblyName = new AssemblyName("MyDynamicAssembly");
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("Mudule1", "Mudule1.netmoudle");

            TypeBuilder classBuilder = moduleBuilder.DefineType(
                    "MyNamespace.MyClass",
                    TypeAttributes.Public | TypeAttributes.Class);

           
            MethodBuilder methodBuilder = classBuilder.DefineMethod(
                   "MyMethod",
                    MethodAttributes.Public | MethodAttributes.Static, /** 定义类方法 **/
                    typeof(string),
                    new Type[] { typeof(int), typeof(string) });
            methodBuilder.DefineParameter(1, ParameterAttributes.None, "id");
            methodBuilder.DefineParameter(
                2,
                ParameterAttributes.Optional,
                "message");

            ILGenerator ilGenerator = methodBuilder.GetILGenerator();
            // 字符串格式化：{0}, hello, {1}
            ilGenerator.Emit(OpCodes.Ldstr, "{0}, Hello, {1}");
            ilGenerator.Emit(OpCodes.Ldarg_0); // 类方法获取第一个参数，Ldarg_0是第一个参数
            ilGenerator.Emit(OpCodes.Box, typeof(int)); // Box the int argument
            ilGenerator.Emit(OpCodes.Ldarg_1);
            //string.Format("{0}, Hello, {1}", id, message);
            ilGenerator.Emit(OpCodes.Call, typeof(string).GetMethod("Format", new Type[] { typeof(string), typeof(object), typeof(object) }));
            ilGenerator.Emit(OpCodes.Ret);

            ConstructorInfo attributeConstructor = typeof(AssemblyTitleAttribute).GetConstructor(new Type[] { typeof(string) });
            CustomAttributeBuilder attributeBuilder = new CustomAttributeBuilder(attributeConstructor, new object[] { "ExampleAttribute" });
            methodBuilder.SetCustomAttribute(attributeBuilder);

            Type classType = classBuilder.CreateType();
            assemblyBuilder.Save($"{assemblyName.Name}.dll");

            #region 测试执行

            object dynamicClassInstance = Activator.CreateInstance(classType);
            // 调用类方法
            object result = classType.GetMethod("MyMethod").Invoke(null, new object[] { 99, "World" });
            Console.WriteLine(result);

            #endregion

            Console.ReadLine();
        }
    }
}
