using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitSimpleAOPSamples
{
    // 3. AOP 代理生成器（修复所有 IL 问题）
    public static class AopProxyGenerator
    {
        public static T CreateProxy<T>(T target) where T : class
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            Type interfaceType = typeof(T);
            if (!interfaceType.IsInterface)
                throw new ArgumentException("目标类型必须是接口");

            // 1. 创建动态程序集、模块、代理类
            AssemblyName assemblyName = new AssemblyName("MyAopProxyAssembly");
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
                assemblyName, 
                AssemblyBuilderAccess.RunAndSave);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(
                "AopProxyModule", 
                "AopProxyModule.netmoudle");
            TypeBuilder typeBuilder = moduleBuilder.DefineType(
                $"MyNameSpace.AopProxy_{interfaceType.Name}",
                TypeAttributes.Public | TypeAttributes.Class,
                typeof(object),
                new[] { interfaceType });

            // 2. 定义存储目标对象的字段
            FieldBuilder targetField = typeBuilder.DefineField(
                "_target", interfaceType, FieldAttributes.Private);

            // 3. 定义构造函数
            DefineConstructor(typeBuilder, targetField);

            // 4. 为每个接口方法生成代理
            foreach (MethodInfo method in interfaceType.GetMethods())
            {
                DefineProxyMethod(typeBuilder, method, targetField);
            }

            // 5. 创建类型并实例化
            Type proxyType = typeBuilder.CreateType();
            assemblyBuilder.Save($"{assemblyName.Name}.dll");
            return (T)Activator.CreateInstance(proxyType, target);
        }

        // 构造函数：接收目标对象并赋值
        private static void DefineConstructor(TypeBuilder typeBuilder, FieldBuilder targetField)
        {
            ConstructorBuilder ctor = typeBuilder.DefineConstructor(
                MethodAttributes.Public,
                CallingConventions.Standard,
                new[] { targetField.FieldType });

            ILGenerator il = ctor.GetILGenerator();
            // this → base() → this._target = 参数
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, typeof(object).GetConstructor(Type.EmptyTypes));
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Stfld, targetField);
            il.Emit(OpCodes.Ret);
        }

        // 核心：生成代理方法（修复所有 IL 问题）
        private static void DefineProxyMethod(TypeBuilder typeBuilder, MethodInfo targetMethod, FieldBuilder targetField)
        {
            // 代理方法签名：与目标方法完全一致
            MethodBuilder proxyMethod = typeBuilder.DefineMethod(
                targetMethod.Name,
                MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig,
                targetMethod.ReturnType,
                targetMethod.GetParameters().Select(p => p.ParameterType).ToArray());

            // 关联接口方法
            typeBuilder.DefineMethodOverride(proxyMethod, targetMethod);

            ILGenerator il = proxyMethod.GetILGenerator();
            ParameterInfo[] parameters = targetMethod.GetParameters();

            // 定义标签：异常处理后、方法结束
            Label catchEndLabel = il.DefineLabel();
            Label methodEndLabel = il.DefineLabel();

            // 定义局部变量：计时器、返回值、异常对象
            LocalBuilder stopwatchVar = il.DeclareLocal(typeof(Stopwatch));
            LocalBuilder returnVar = il.DeclareLocal(targetMethod.ReturnType);
            LocalBuilder exceptionVar = il.DeclareLocal(typeof(Exception));

            // ====================== 异常处理块开始（必须在 try 逻辑前）======================
            il.BeginExceptionBlock();

            // ====================== Try 块：前置切面 + 调用目标方法 + 后置切面 ======================
            // 1. 前置切面：启动计时器 + 打印日志
            il.Emit(OpCodes.Newobj, typeof(Stopwatch).GetConstructor(Type.EmptyTypes));
            il.Emit(OpCodes.Stloc, stopwatchVar); // 存储计时器
            il.Emit(OpCodes.Ldloc, stopwatchVar);
            il.Emit(OpCodes.Callvirt, typeof(Stopwatch).GetMethod("Start", Type.EmptyTypes)); // 启动计时

            // 打印进入日志
            il.Emit(OpCodes.Ldstr, $"[AOP前置] 进入方法：{targetMethod.Name}，参数：");
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("Write", new[] { typeof(string) }));

            // 打印所有参数（处理值类型装箱）
            for (int i = 0; i < parameters.Length; i++)
            {
                il.Emit(OpCodes.Ldarg, i + 1); // 加载参数（0是this）
                Type paramType = parameters[i].ParameterType;
                if (paramType.IsValueType)
                    il.Emit(OpCodes.Box, paramType); // 值类型装箱
                il.Emit(OpCodes.Callvirt, typeof(object).GetMethod("ToString", Type.EmptyTypes));
                il.Emit(OpCodes.Call, typeof(Console).GetMethod("Write", new[] { typeof(string) }));
                if (i < parameters.Length - 1)
                {
                    il.Emit(OpCodes.Ldstr, ", ");
                    il.Emit(OpCodes.Call, typeof(Console).GetMethod("Write", new[] { typeof(string) }));
                }
            }
            il.Emit(OpCodes.Ldstr, "\n");
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new[] { typeof(string) }));

            // 2. 调用目标方法
            il.Emit(OpCodes.Ldarg_0); // this
            il.Emit(OpCodes.Ldfld, targetField); // this._target
                                                 // 加载所有参数（传递给目标方法）
            for (int i = 0; i < parameters.Length; i++)
            {
                il.Emit(OpCodes.Ldarg, i + 1);
            }
            // 调用目标方法（接口方法必须用 Callvirt）
            il.Emit(OpCodes.Callvirt, targetMethod);
            // 存储返回值（如果有）
            if (targetMethod.ReturnType != typeof(void))
            {
                il.Emit(OpCodes.Stloc, returnVar);
            }

            // 3. 后置切面：停止计时 + 打印日志
            il.Emit(OpCodes.Ldloc, stopwatchVar);
            il.Emit(OpCodes.Callvirt, typeof(Stopwatch).GetMethod("Stop", Type.EmptyTypes)); // 停止计时

            // 打印执行时间
            il.Emit(OpCodes.Ldstr, $"[AOP后置] 退出方法：{targetMethod.Name}，执行时间：");
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("Write", new[] { typeof(string) }));
            il.Emit(OpCodes.Ldloc, stopwatchVar);
            il.Emit(OpCodes.Callvirt, typeof(Stopwatch).GetMethod("get_ElapsedMilliseconds", Type.EmptyTypes));
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("Write", new[] { typeof(long) }));
            il.Emit(OpCodes.Ldstr, "ms\n");
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new[] { typeof(string) }));

            // 打印返回值（如果有）
            if (targetMethod.ReturnType != typeof(void))
            {
                il.Emit(OpCodes.Ldstr, $"[AOP后置] 方法返回值：");
                il.Emit(OpCodes.Call, typeof(Console).GetMethod("Write", new[] { typeof(string) }));
                il.Emit(OpCodes.Ldloc, returnVar);
                if (targetMethod.ReturnType.IsValueType)
                    il.Emit(OpCodes.Box, targetMethod.ReturnType); // 装箱
                il.Emit(OpCodes.Callvirt, typeof(object).GetMethod("ToString", Type.EmptyTypes));
                il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new[] { typeof(string) }));
            }

            // 跳至方法结束（跳过 catch 块）
            il.Emit(OpCodes.Br_S, methodEndLabel);

            // ====================== Catch 块：异常处理 ======================
            il.BeginCatchBlock(typeof(Exception));
            // 存储异常对象（关键：避免栈不平衡）
            il.Emit(OpCodes.Stloc, exceptionVar);

            // 打印异常日志
            il.Emit(OpCodes.Ldstr, $"[AOP异常] 方法 {targetMethod.Name} 执行失败！异常信息：");
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("Write", new[] { typeof(string) }));
            il.Emit(OpCodes.Ldloc, exceptionVar);
            il.Emit(OpCodes.Callvirt, typeof(Exception).GetMethod("get_Message", Type.EmptyTypes));
            il.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new[] { typeof(string) }));

            // 异常时初始化返回值（关键：避免未初始化的局部变量）
            if (targetMethod.ReturnType != typeof(void))
            {
                if (targetMethod.ReturnType.IsValueType)
                {
                    // 值类型：初始化默认值
                    il.Emit(OpCodes.Ldloca_S, returnVar); // 加载返回值的地址
                    il.Emit(OpCodes.Initobj, targetMethod.ReturnType); // 初始化
                }
                else
                {
                    // 引用类型：赋值为 null
                    il.Emit(OpCodes.Ldnull);
                    il.Emit(OpCodes.Stloc, returnVar);
                }
            }

            // 跳至 catch 块结束
            il.Emit(OpCodes.Br_S, catchEndLabel);

            // ====================== 异常处理块结束 ======================
            il.EndExceptionBlock();

            // ====================== 方法结束：返回结果 ======================
            il.MarkLabel(catchEndLabel); // catch 块结束标签
            il.MarkLabel(methodEndLabel); // 正常流程结束标签

            // 返回返回值（如果有）
            if (targetMethod.ReturnType != typeof(void))
            {
                il.Emit(OpCodes.Ldloc, returnVar);
            }
            il.Emit(OpCodes.Ret);
        }
    }
}
