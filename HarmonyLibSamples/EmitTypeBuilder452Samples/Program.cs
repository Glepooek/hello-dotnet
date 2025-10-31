using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitTypeBuilder452Samples
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
                "Module1",
                "Module1.netmodule");

            #region 定义一个枚举类型
            EnumBuilder enmuBuilder = moduleBuilder.DefineEnum(
                "MyNameSpace.MyEnum",
                TypeAttributes.Public,
                typeof(int));
            enmuBuilder.DefineLiteral("Spring", 0);
            enmuBuilder.DefineLiteral("Summer", 1);
            enmuBuilder.DefineLiteral("Autumn", 2);
            enmuBuilder.DefineLiteral("Winter", 3);

            enmuBuilder.CreateType();
            Console.WriteLine("枚举类型已创建：");
            #endregion

            #region 定义接口类型
            TypeBuilder interfaceBuilder = moduleBuilder.DefineType(
                "MyNameSpace.IMyInterface",
                TypeAttributes.Public | TypeAttributes.Interface | TypeAttributes.Abstract);

            // 接口中不能定义字段
            //interfaceBuilder.DefineField(
            //    "MyField",
            //    typeof(int),
            //    FieldAttributes.Public);
            PropertyBuilder propertyBuilder = interfaceBuilder.DefineProperty(
                "MyName",
                PropertyAttributes.None,
                typeof(string),
                null);
            MethodBuilder getMethodBuilder = interfaceBuilder.DefineMethod(
                "get_MyName",
                MethodAttributes.Public | MethodAttributes.Abstract | MethodAttributes.Virtual | MethodAttributes.SpecialName | MethodAttributes.HideBySig,
                typeof(string),
                Type.EmptyTypes);
            propertyBuilder.SetGetMethod(getMethodBuilder);

            interfaceBuilder.DefineMethod(
                "GetMyName",
                MethodAttributes.Public | MethodAttributes.Abstract | MethodAttributes.Virtual,
                typeof(string),
                new Type[] { typeof(int) });

            interfaceBuilder.CreateType();
            Console.WriteLine("接口类型已创建：");
            #endregion

            #region 定义结构体
            TypeBuilder structBuilder = moduleBuilder.DefineType(
                "MyNameSpace.MyStruct",
                TypeAttributes.Public | TypeAttributes.SequentialLayout | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit,
                typeof(ValueType)); // 继承自 ValueType

            // 定义字段
            structBuilder.DefineField("ID", typeof(int), FieldAttributes.Public);
            structBuilder.DefineField("Name", typeof(string), FieldAttributes.Public);

            structBuilder.CreateType();
            Console.WriteLine("结构体类型已创建：");
            #endregion

            #region 定义抽象类

            TypeBuilder abstractClassBuilder = moduleBuilder.DefineType(
            "MyNameSpace.MyClassBase",
                TypeAttributes.Public | TypeAttributes.Abstract | TypeAttributes.Class);

            abstractClassBuilder.DefineField("ID", typeof(int), FieldAttributes.Public);
            abstractClassBuilder.DefineMethod("MyProtectedMethod",
                MethodAttributes.Family | MethodAttributes.Abstract | MethodAttributes.Virtual,
                typeof(void),
                Type.EmptyTypes);

            Type classBase = abstractClassBuilder.CreateType();
            Console.WriteLine("抽象类已创建：");
            #endregion

            #region 定义类并继承自抽象类

            TypeBuilder classBuilder = moduleBuilder.DefineType(
                "MyNameSpace.MyClass",
                TypeAttributes.Public | TypeAttributes.Class,
                classBase); // 继承自抽象类

            // 实现抽象方法
            MethodBuilder protectedMethodBuilder = classBuilder.DefineMethod(
                "MyProtectedMethod",
                MethodAttributes.Family | MethodAttributes.Virtual,
                typeof(void),
                Type.EmptyTypes);
            // 定义方法体
            ILGenerator iL = protectedMethodBuilder.GetILGenerator();
            iL.Emit(OpCodes.Ret);

            // 定义泛型参数
            string[] typeParamNames = { "T" };
            GenericTypeParameterBuilder[] typeParams = classBuilder.DefineGenericParameters(typeParamNames);

            // 定义泛型方法
            MethodBuilder genericMethodBuilder = classBuilder.DefineMethod(
                "GetT",
                MethodAttributes.Public,
                typeParams[0],
                new Type[] { typeof(object) });
            ILGenerator ilGenerator = genericMethodBuilder.GetILGenerator();
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ret);

            //classBuilder.CreateType();
            Console.WriteLine("类已创建：");
            #endregion

            #region 定义委托

            // 定义内部类，并在内部类中定义委托类型
            TypeBuilder delegateBuilder = classBuilder.DefineNestedType(
                "MyNameSpace.AuthDelegate",
                TypeAttributes.Class | TypeAttributes.NestedPublic | TypeAttributes.Sealed,
                typeof(MulticastDelegate));

            // 添加委托的构造函数
            ConstructorBuilder constructor = delegateBuilder.DefineConstructor(
                MethodAttributes.Public, CallingConventions.Standard,
                new Type[] { typeof(object),
                typeof(IntPtr) });
            constructor.SetImplementationFlags(MethodImplAttributes.Runtime | MethodImplAttributes.Managed);

            // 添加Invoke方法
            delegateBuilder.DefineMethod(
                "Invoke",
                MethodAttributes.Public,
                typeof(bool),
                new Type[] { typeof(string) })
              .SetImplementationFlags(MethodImplAttributes.Runtime | MethodImplAttributes.Managed);

            // 创建内部类和委托类型
            Type delegateType = delegateBuilder.CreateType();
            Console.WriteLine("委托类型已创建：");
            #endregion

            #region 定义事件

            //定义事件
            EventBuilder eb = classBuilder.DefineEvent(
                "MyEvent", 
                EventAttributes.None,
                delegateBuilder);

            MethodBuilder addMethod = classBuilder.DefineMethod(
                "add_OnAuth", 
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName, 
                typeof(void), 
                new Type[] { delegateBuilder });
            ILGenerator addMethodIL = addMethod.GetILGenerator();
            //......
            addMethodIL.Emit(OpCodes.Ret);
            eb.SetAddOnMethod(addMethod);

            MethodBuilder removeMethod = classBuilder.DefineMethod(
                "remove_OnAuth",
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName,
                typeof(void),
                new Type[] { delegateBuilder });
            ILGenerator removeMethodIL = removeMethod.GetILGenerator();
            //......
            removeMethodIL.Emit(OpCodes.Ret);
            eb.SetRemoveOnMethod(removeMethod);
            Console.WriteLine("事件已创建：");

            classBuilder.CreateType();
            #endregion

            assemblyBuilder.Save("MyDynamicAssembly.dll");

            Console.Read();
        }
    }
}
