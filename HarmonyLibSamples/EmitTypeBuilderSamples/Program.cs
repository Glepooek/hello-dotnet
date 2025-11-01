using System;
using System.Reflection;
using System.Reflection.Emit;

public class DynamicDelegateEventExample
{
    public static void Main()
    {
        // 1. 创建动态程序集和模块（.NET Framework 用 AppDomain，.NET Core+ 用 AssemblyBuilder.DefineDynamicAssembly）
        AssemblyName assemblyName = new AssemblyName("DynamicEventAssembly");
        AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
            assemblyName, AssemblyBuilderAccess.Run);
        ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("DynamicEventModule");

        // 2. 动态定义委托类型（MyDelegate）
        TypeBuilder delegateBuilder = moduleBuilder.DefineType(
            "MyDelegate",
            TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.Class,
            typeof(MulticastDelegate));

        // 委托构造函数
        ConstructorBuilder ctor = delegateBuilder.DefineConstructor(
            MethodAttributes.RTSpecialName | MethodAttributes.HideBySig | MethodAttributes.Public,
            CallingConventions.Standard,
            new[] { typeof(object), typeof(IntPtr) });
        ctor.SetImplementationFlags(MethodImplAttributes.Runtime);

        // 委托 Invoke 方法
        MethodBuilder invokeMethod = delegateBuilder.DefineMethod(
            "Invoke",
            MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual,
            typeof(void),
            new[] { typeof(string) });
        invokeMethod.SetImplementationFlags(MethodImplAttributes.Runtime);

        Type myDelegateType = delegateBuilder.CreateType();


        // 3. 动态定义包含事件的类（EventDemo）
        TypeBuilder eventDemoType = moduleBuilder.DefineType(
            "EventDemo",
            TypeAttributes.Public | TypeAttributes.Class);

        // 3.1 事件底层字段
        FieldBuilder eventField = eventDemoType.DefineField(
            "_myEvent",
            myDelegateType,
            FieldAttributes.Private);

        // 3.2 定义事件
        EventBuilder myEvent = eventDemoType.DefineEvent(
            "MyEvent",
            EventAttributes.None,
            myDelegateType);

        // 3.3 add 访问器
        MethodBuilder addMethod = eventDemoType.DefineMethod(
            "add_MyEvent",
            MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName,
            typeof(void),
            new[] { myDelegateType });

        ILGenerator addIl = addMethod.GetILGenerator();
        addIl.Emit(OpCodes.Ldarg_0);
        addIl.Emit(OpCodes.Ldarg_0);
        addIl.Emit(OpCodes.Ldfld, eventField);
        addIl.Emit(OpCodes.Ldarg_1);
        addIl.Emit(OpCodes.Call, typeof(Delegate).GetMethod("Combine", new[] { typeof(Delegate), typeof(Delegate) }));
        addIl.Emit(OpCodes.Castclass, myDelegateType);
        addIl.Emit(OpCodes.Stfld, eventField);
        addIl.Emit(OpCodes.Ret);
        myEvent.SetAddOnMethod(addMethod);

        // 3.4 remove 访问器
        MethodBuilder removeMethod = eventDemoType.DefineMethod(
            "remove_MyEvent",
            MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName,
            typeof(void),
            new[] { myDelegateType });

        ILGenerator removeIl = removeMethod.GetILGenerator();
        removeIl.Emit(OpCodes.Ldarg_0);
        removeIl.Emit(OpCodes.Ldarg_0);
        removeIl.Emit(OpCodes.Ldfld, eventField);
        removeIl.Emit(OpCodes.Ldarg_1);
        removeIl.Emit(OpCodes.Call, typeof(Delegate).GetMethod("Remove", new[] { typeof(Delegate), typeof(Delegate) }));
        removeIl.Emit(OpCodes.Castclass, myDelegateType);
        removeIl.Emit(OpCodes.Stfld, eventField);
        removeIl.Emit(OpCodes.Ret);
        myEvent.SetRemoveOnMethod(removeMethod);

        // 3.5 触发事件的方法
        MethodBuilder raiseMethod = eventDemoType.DefineMethod(
            "RaiseEvent",
            MethodAttributes.Public,
            typeof(void),
            new[] { typeof(string) });

        ILGenerator raiseIl = raiseMethod.GetILGenerator();
        Label label = raiseIl.DefineLabel();
        raiseIl.Emit(OpCodes.Ldarg_0);
        raiseIl.Emit(OpCodes.Ldfld, eventField);
        raiseIl.Emit(OpCodes.Brfalse_S, label);
        raiseIl.Emit(OpCodes.Ldarg_0);
        raiseIl.Emit(OpCodes.Ldfld, eventField);
        raiseIl.Emit(OpCodes.Ldarg_1);
        raiseIl.Emit(OpCodes.Callvirt, myDelegateType.GetMethod("Invoke"));
        raiseIl.MarkLabel(label);
        raiseIl.Emit(OpCodes.Ret);

        Type eventDemoTypeFinished = eventDemoType.CreateType();


        // 4. 测试（修正部分）
        object eventDemo = Activator.CreateInstance(eventDemoTypeFinished);

        // 定义事件处理方法（作为静态方法，方便反射获取）
        // 注意：不能直接用 lambda 转换，必须通过 Delegate.CreateDelegate 创建匹配类型的委托

        // 获取处理方法的 MethodInfo
        MethodInfo handlerMethod = typeof(DynamicDelegateEventExample)
            .GetMethod("HandleEvent", BindingFlags.Static | BindingFlags.NonPublic);

        // 创建 MyDelegate 类型的委托实例（关键修正）
        Delegate handler = Delegate.CreateDelegate(myDelegateType, handlerMethod);

        // 注册事件
        eventDemoTypeFinished.GetEvent("MyEvent").AddEventHandler(eventDemo, handler);

        // 触发事件
        eventDemoTypeFinished.GetMethod("RaiseEvent").Invoke(eventDemo, new object[] { "Hello from dynamic event!" });

        // 注销事件
        eventDemoTypeFinished.GetEvent("MyEvent").RemoveEventHandler(eventDemo, handler);

        // 再次触发（无输出）
        eventDemoTypeFinished.GetMethod("RaiseEvent").Invoke(eventDemo, new object[] { "This message won't be handled" });

        Console.ReadLine();
    }

    // 事件处理方法（签名与 MyDelegate 匹配）
    private static void HandleEvent(string message)
    {
        Console.WriteLine($"事件触发：{message}");
    }
}