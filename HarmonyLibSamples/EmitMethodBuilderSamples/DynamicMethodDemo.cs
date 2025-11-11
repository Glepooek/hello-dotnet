using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitMethodBuilderSamples;

internal class DynamicMethodDemo
{
    delegate void IncrementDelegate(ref int value);


    public static void Run()
    {
        //创建动态方法。默认是静态方法
        DynamicMethod dynamicMethod = new DynamicMethod(
            "MyMethod",
            typeof(string),
            new Type[] { typeof(int), typeof(string) });

        ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
        // 字符串格式化：{0}, hello, {1}
        ilGenerator.Emit(OpCodes.Ldstr, "{0}, Hello, {1}");
        ilGenerator.Emit(OpCodes.Ldarg_0); // 类方法获取第一个参数，Ldarg_0是第一个参数
        ilGenerator.Emit(OpCodes.Box, typeof(int)); // Box the int argument
        ilGenerator.Emit(OpCodes.Ldarg_1);
        //string.Format("{0}, Hello, {1}", id, message);
        ilGenerator.Emit(OpCodes.Call, typeof(string).GetMethod("Format", new Type[] { typeof(string), typeof(object), typeof(object) }));
        ilGenerator.Emit(OpCodes.Ret);

        ////创建调用委托
        //var deletegateMethod = dynamicMethod.CreateDelegate(typeof(Func<int, string, string>)) as Func<int, string, string>;
        ////执行委托
        //string result = deletegateMethod(99, "World");

        string result = (string)dynamicMethod.Invoke(null, new object[] { 99, "World" });

        Console.WriteLine($"DynamicMethodDemo, {result}"); // 输出结果：99, Hello, World
    }

    public static void Run1()
    {
        // 1. 创建 DynamicMethod（void Increment(ref int value)）
        DynamicMethod incrementMethod = new DynamicMethod(
            name: "Increment",
            returnType: typeof(void),
            parameterTypes: new[] { typeof(int).MakeByRefType() }, 
            typeof(DynamicMethodDemo).Module);// ref int 参数

        // 2. 生成 IL 逻辑（value++）
        ILGenerator il = incrementMethod.GetILGenerator();
        il.Emit(OpCodes.Ldarg_0); // 加载 ref 参数（value 的地址）
        il.Emit(OpCodes.Ldind_I4); // 读取地址中的 int 值（value）
        il.Emit(OpCodes.Ldc_I4_1); // 压入 1
        il.Emit(OpCodes.Add);     // value + 1
        il.Emit(OpCodes.Stind_I4); // 将结果写回 ref 参数地址
        il.Emit(OpCodes.Ret);     // 返回

        // 3. 编译为委托（Action<int> 不支持 ref，需自定义委托）
        IncrementDelegate incrementDelegate = (IncrementDelegate)incrementMethod.CreateDelegate(typeof(IncrementDelegate));

        // 4. 执行委托
        int num = 5;
        incrementDelegate(ref num);
        Console.WriteLine($"自增后：{num}"); // 输出：6
    }
}
