using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmitILGeneratorBranchInstruction462Samples
{
    internal class SwitchBranchInstructionDemo
    {
        public static void Run()
        {
            DynamicMethod dynamicMethod = new DynamicMethod(
                "WriteAOrB",
                typeof(void),
                new Type[] { typeof(int) },
                typeof(SwitchBranchInstructionDemo));

            ILGenerator il = dynamicMethod.GetILGenerator();

            Label labelEnd = il.DefineLabel();
            Label labelIndex_0 = il.DefineLabel();
            Label labelIndex_1 = il.DefineLabel();
            Label labelIndex_2 = il.DefineLabel();
            //BreakOp None=-1，Null=0，Empty=1，NullOrEmpty=2
            Label[] lables = new Label[] { labelIndex_0, labelIndex_1, labelIndex_2 };//0、1、2

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Switch, lables);

            il.MarkLabel(labelIndex_0);
            il.EmitWriteLine("0.");
            il.Emit(OpCodes.Br_S, labelEnd);

            il.MarkLabel(labelIndex_1);
            il.EmitWriteLine("1.");
            il.Emit(OpCodes.Br_S, labelEnd);

            il.MarkLabel(labelIndex_2);
            il.EmitWriteLine("2.");
            il.Emit(OpCodes.Br_S, labelEnd);

            il.MarkLabel(labelEnd);
            il.Emit(OpCodes.Ret);     // 返回该值

            dynamicMethod.Invoke(null, new object[] { 2 });
            Console.Read();
        }
    }
}
