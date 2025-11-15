using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorLdInstruction462Samples
{
    internal class LdfldaInstructionDemo
    {
        public static void Run(TypeBuilder structBuilder, FieldBuilder fieldBuilder)
        {
            var methodBuilder = structBuilder.DefineMethod(
                "Increment",
                MethodAttributes.Public,
                typeof(void),
                Type.EmptyTypes);

            var il = methodBuilder.GetILGenerator();
            il.Emit(OpCodes.Ldarg, 0);   // ← Ldarga.0: 加载参数0（this）的地址
            il.Emit(OpCodes.Dup);
            // 获取 Value 字段的地址: &this.Value
            il.Emit(OpCodes.Ldflda, fieldBuilder);  // ldflda = load field address
            // 读取当前 Value 的值
            il.Emit(OpCodes.Ldind_I4);            // ldind.i4 = indirect load int32
            // 加 1
            il.Emit(OpCodes.Ldc_I4_1);            // 加载常量 1
            il.Emit(OpCodes.Add);                 // 相加
            // 存回 Value
            il.Emit(OpCodes.Stind_I4);            // stind.i4 = indirect store int32
            il.Emit(OpCodes.Ret);
        }
    }
}
