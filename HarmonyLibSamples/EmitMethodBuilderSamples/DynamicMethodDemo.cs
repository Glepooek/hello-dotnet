using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitMethodBuilderSamples;

internal class DynamicMethodDemo
{
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

        //创建调用委托
        var deletegateMethod = dynamicMethod.CreateDelegate(typeof(Func<int, string, string>)) as Func<int, string, string>;
        //执行委托
        string result = deletegateMethod(99, "World");
        Console.WriteLine($"DynamicMethodDemo, {result}"); // 输出结果：99, Hello, World
    }
}
