using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitTypeBuilder462Samples
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
            MethodBuilder setMethodBuilder = interfaceBuilder.DefineMethod(
                "set_MyName",
                MethodAttributes.Public | MethodAttributes.Abstract | MethodAttributes.Virtual | MethodAttributes.SpecialName | MethodAttributes.HideBySig,
                typeof(void),
                new Type[] { typeof(string) });
            propertyBuilder.SetSetMethod(setMethodBuilder);

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
                TypeAttributes.Public | TypeAttributes.SequentialLayout | TypeAttributes.Sealed,
                typeof(ValueType)); // 继承自 ValueType

            // 定义字段
            structBuilder.DefineField("id", typeof(int), FieldAttributes.Public);
            structBuilder.DefineField("name", typeof(string), FieldAttributes.Public);

            structBuilder.CreateType();
            Console.WriteLine("结构体类型已创建：");
            #endregion

            #region 定义抽象类

            TypeBuilder abstractClassBuilder = moduleBuilder.DefineType(
            "MyNameSpace.MyClassBase",
                TypeAttributes.Public | TypeAttributes.Abstract | TypeAttributes.Class);

            abstractClassBuilder.DefineField(
                "id", 
                typeof(int), 
                FieldAttributes.Public);

            abstractClassBuilder.DefineMethod(
                "MyProtectedMethod",
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

            //// 定义泛型参数
            //string[] typeParamNames = { "T" };
            //GenericTypeParameterBuilder[] typeParams = classBuilder.DefineGenericParameters(typeParamNames);

            //// 定义泛型方法
            //MethodBuilder genericMethodBuilder = classBuilder.DefineMethod(
            //    "GetT",
            //    MethodAttributes.Public,
            //    typeParams[0],
            //    new Type[] { typeof(object) });
            //ILGenerator ilGenerator = genericMethodBuilder.GetILGenerator();
            //ilGenerator.Emit(OpCodes.Ldarg_0);
            //ilGenerator.Emit(OpCodes.Ret);

            //classBuilder.CreateType();
            Console.WriteLine("类已创建：");
            #endregion

            #region 定义委托

            // 定义委托类型
            TypeBuilder delegateBuilder = moduleBuilder.DefineType(
                "MyNameSpace.AuthDelegate",
                TypeAttributes.Class | TypeAttributes.Public | TypeAttributes.Sealed,
                typeof(MulticastDelegate));

            // 添加委托的构造函数
            ConstructorBuilder constructor = delegateBuilder.DefineConstructor(
                MethodAttributes.RTSpecialName | MethodAttributes.HideBySig | MethodAttributes.Public,
                CallingConventions.Standard,
                new Type[] { typeof(object), typeof(IntPtr) });
            constructor.SetImplementationFlags(MethodImplAttributes.Runtime | MethodImplAttributes.Managed);

            // 添加Invoke方法
            delegateBuilder.DefineMethod(
                "Invoke",
                MethodAttributes.Public,
                typeof(void),
                new Type[] { typeof(string) })
              .SetImplementationFlags(MethodImplAttributes.Runtime | MethodImplAttributes.Managed);

            // 创建内部类和委托类型
            Type delegateType = delegateBuilder.CreateType();
            Console.WriteLine("委托类型已创建：");
            #endregion

            #region 定义事件

            // 事件底层字段
            FieldBuilder eventField = classBuilder.DefineField(
                "_myEvent",
                delegateType,
                FieldAttributes.Private);

            // 定义事件
            EventBuilder myEvent = classBuilder.DefineEvent(
                "MyEvent",
                EventAttributes.None,
                delegateType);

            // add 访问器
            MethodBuilder addMethod = classBuilder.DefineMethod(
                "add_MyEvent",
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName,
                typeof(void),
                new[] { delegateType });

            ILGenerator addIl = addMethod.GetILGenerator();
            addIl.Emit(OpCodes.Ldarg_0);
            addIl.Emit(OpCodes.Ldarg_0);
            addIl.Emit(OpCodes.Ldfld, eventField);
            addIl.Emit(OpCodes.Ldarg_1);
            addIl.Emit(OpCodes.Call, typeof(Delegate).GetMethod("Combine", new[] { typeof(Delegate), typeof(Delegate) }));
            addIl.Emit(OpCodes.Castclass, delegateType);
            addIl.Emit(OpCodes.Stfld, eventField);
            addIl.Emit(OpCodes.Ret);
            myEvent.SetAddOnMethod(addMethod);

            // remove 访问器
            MethodBuilder removeMethod = classBuilder.DefineMethod(
                "remove_MyEvent",
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName,
                typeof(void),
                new[] { delegateType });

            ILGenerator removeIl = removeMethod.GetILGenerator();
            removeIl.Emit(OpCodes.Ldarg_0);
            removeIl.Emit(OpCodes.Ldarg_0);
            removeIl.Emit(OpCodes.Ldfld, eventField);
            removeIl.Emit(OpCodes.Ldarg_1);
            removeIl.Emit(OpCodes.Call, typeof(Delegate).GetMethod("Remove", new[] { typeof(Delegate), typeof(Delegate) }));
            removeIl.Emit(OpCodes.Castclass, delegateType);
            removeIl.Emit(OpCodes.Stfld, eventField);
            removeIl.Emit(OpCodes.Ret);
            myEvent.SetRemoveOnMethod(removeMethod);

            // 触发事件的方法
            MethodBuilder raiseMethod = classBuilder.DefineMethod(
                "RaiseEvent",
                MethodAttributes.Public,
                typeof(void),
                new[] { typeof(string) });
            raiseMethod.DefineParameter(1, ParameterAttributes.None, "message");

            ILGenerator raiseIl = raiseMethod.GetILGenerator();
            Label label = raiseIl.DefineLabel();
            raiseIl.Emit(OpCodes.Ldarg_0);// 加载this
            raiseIl.Emit(OpCodes.Ldfld, eventField);
            raiseIl.Emit(OpCodes.Brfalse_S, label);
            raiseIl.Emit(OpCodes.Ldarg_0);
            raiseIl.Emit(OpCodes.Ldfld, eventField);
            raiseIl.Emit(OpCodes.Ldarg_1);
            raiseIl.Emit(OpCodes.Callvirt, delegateType.GetMethod("Invoke"));
            raiseIl.MarkLabel(label);
            raiseIl.Emit(OpCodes.Ret);

            Console.WriteLine("事件已创建：");
            // 获取泛型类型定义
            Type classType = classBuilder.CreateType();

            #endregion

            #region 测试事件

            object dynamicClass = Activator.CreateInstance(classType);

            // 定义事件处理方法（作为静态方法，方便反射获取）
            // 注意：不能直接用 lambda 转换，必须通过 Delegate.CreateDelegate 创建匹配类型的委托

            // 获取处理方法的 MethodInfo
            MethodInfo handlerMethod = typeof(Program)
                .GetMethod("HandleEvent", BindingFlags.Static | BindingFlags.NonPublic);

            // 创建 MyDelegate 类型的委托实例（关键修正）
            Delegate handler = Delegate.CreateDelegate(delegateType, handlerMethod);

            // 注册事件
            classType.GetEvent("MyEvent").AddEventHandler(dynamicClass, handler);

            // 触发事件
            classType.GetMethod("RaiseEvent").Invoke(dynamicClass, new object[] { "Hello from dynamic event!" });

            // 注销事件
            classType.GetEvent("MyEvent").RemoveEventHandler(dynamicClass, handler);

            // 再次触发（无输出）
            classType.GetMethod("RaiseEvent").Invoke(dynamicClass, new object[] { "This message won't be handled" });

            #endregion

            assemblyBuilder.Save($"{assemblyName.Name}.dll");

            Console.Read();
        }

        // 事件处理方法（签名与 MyDelegate 匹配）
        private static void HandleEvent(string message)
        {
            Console.WriteLine($"事件触发：{message}");
        }
    }
}
