using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorLdInstruction462Samples
{
    internal class LdtokenInstructionDemo
    {
        public static void Run(TypeBuilder classBuilder)
        {
            MethodBuilder methodBuilder = classBuilder.DefineMethod(
                    "GetTypeToken",
                    MethodAttributes.Public | MethodAttributes.Static,
                    typeof(RuntimeTypeHandle),
                    new Type[] { typeof(Type) });
            methodBuilder.DefineParameter(1, ParameterAttributes.None, "type");
            ILGenerator ilGenerator = methodBuilder.GetILGenerator();
            // Load the Type argument onto the stack
            ilGenerator.Emit(OpCodes.Ldarg_0);
            // Load the runtime type handle of the Type argument
            // 调用 type.TypeHandle属性的get方法 获取运行时句柄
            ilGenerator.Emit(OpCodes.Callvirt, typeof(Type).GetProperty("TypeHandle").GetGetMethod());
            ilGenerator.Emit(OpCodes.Ret);
        }

        public static void CreateTypeChecker()
        {
            DynamicMethod method = new DynamicMethod(
                "IsStringType",
                typeof(bool),
                new[] { typeof(object) },
                typeof(LdtokenInstructionDemo).Module,
                skipVisibility: true);

            ILGenerator il = method.GetILGenerator();

            Label notString = il.DefineLabel();
            Label end = il.DefineLabel();

            // 加载参数（对象）
            il.Emit(OpCodes.Ldarg_0);

            // 如果对象为 null，跳转到 notString
            il.Emit(OpCodes.Brfalse, notString);

            // 加载对象并调用 GetType 获取实际类型
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Callvirt, typeof(object).GetMethod("GetType"));

            // 加载 string 类型的运行时句柄并转换为 Type 对象
            il.Emit(OpCodes.Ldtoken, typeof(string));
            il.Emit(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle"));

            // 调用 Type.Equals 方法比较两个 Type 对象
            il.Emit(OpCodes.Callvirt, typeof(Type).GetMethod("Equals", new[] { typeof(Type) }));

            // 跳转到结束标签
            il.Emit(OpCodes.Br, end);

            // notString 标签：处理 null 情况
            il.MarkLabel(notString);
            il.Emit(OpCodes.Ldc_I4_0); // 加载 false

            // end 标签：返回结果
            il.MarkLabel(end);
            il.Emit(OpCodes.Ret);

            // 创建委托并测试
            var checker = (Func<object, bool>)method.CreateDelegate(typeof(Func<object, bool>));

            Console.WriteLine("+++++++++++++++动态方法测试结果++++++++++++++:");
            Console.WriteLine($"checker(\"hello\"): {checker("hello")}");    // 应输出 True
            Console.WriteLine($"checker(123): {checker(123)}");            // 应输出 False
            Console.WriteLine($"checker(null): {checker(null)}");          // 应输出 False
        }
    }
}
