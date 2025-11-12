using System;
using System.Reflection.Emit;

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
        DynamicMethod incrementMethod = new DynamicMethod(
            name: "Increment",
            typeof(void),
            new[] { typeof(int).MakeByRefType() }, // ref int 参数
            typeof(DynamicMethodDemo).Module);

        // 2. 生成 IL 逻辑（value++）
        ILGenerator il = incrementMethod.GetILGenerator();
        il.Emit(OpCodes.Ldarg_0);        // 加载参数地址（ref int）
        il.Emit(OpCodes.Dup);            // 复制地址
        il.Emit(OpCodes.Ldind_I4);       // 间接加载int值
        il.Emit(OpCodes.Ldc_I4_1);       // 加载常量1
        il.Emit(OpCodes.Add);            // 相加
        il.Emit(OpCodes.Stind_I4);       // 存储回原地址
        il.Emit(OpCodes.Ret);

        //int num = 5;
        //object[] parameters = new object[] { num }; // ref 参数需先传入初始值
        //incrementMethod.Invoke(null, parameters); // 参数数组

        //// 从参数数组中获取修改后的值（ref 参数会修改数组元素）
        //num = (int)parameters[0];
        //Console.WriteLine($"自增后：{num}"); // 输出：6（正常执行）

        IncrementDelegate increment = (IncrementDelegate)incrementMethod.CreateDelegate(typeof(IncrementDelegate));
        int num = 5;
        increment(ref num);
        Console.WriteLine($"自增后：{num}");
    }
}
