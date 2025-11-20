using HarmonyLib;
using HarmonyPatch.Shared;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace HarmonyReversePatchSamples
{
    [HarmonyLib.HarmonyPatch(typeof(OriginalCalculator), "Add")]
    public class MyCalculatorPatch
    {
        [HarmonyReversePatch]
        public static int OriginalAdd(OriginalCalculator instance, int a, int b)
        {
            // 这里的实现会被Harmony替换为原始Add的实现
            throw new NotImplementedException("Stub for reverse patching.");
        }

        static bool Prefix(OriginalCalculator __instance, ref int __result, int a, int b)
        {
            // 在Patch中调用原始实现
            __result = OriginalAdd(__instance, a, b) * 2;
            return false; // 跳过原方法
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(OriginalCalculator), "SpecialCalculation")]
    public class MyCalculatorPatch1
    {
        // When reverse patched, StringOperation will contain all the
        // code from the original including the Join() but not the +n
        //
        // Basically
        // var parts = original.Split('-');
        // return string.Join("", parts)
        //
        [HarmonyReversePatch]
        public static string StringOperation(string original)
        {
            // This inner transpiler will be applied to the original and
            // the result will replace this method
            //
            // That will allow this method to have a different signature
            // than the original and it must match the transpiled result
            //
            IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                // 把所有 OpCodes.Ldarg_1 的指令改为 OpCodes.Ldarg_0：
                // 原因：原方法是实例方法（arg0 = this, arg1 = original）。
                // 但目标 StringOperation(string original) 是一个非实例/不同签名的方法，original 在新方法中是 arg0。
                // 因此所有原来访问 original 的 ldarg.1 必须改为 ldarg.0，
                // 否则会加载错误的值（this 或其它），导致语义错乱或运行时错误。
                List<CodeInstruction>? list = instructions.Manipulator(
                    item => item.opcode == OpCodes.Ldarg_1,
                    item => item.opcode = OpCodes.Ldarg_0)
                    .ToList();
                MethodInfo? mJoin = SymbolExtensions.GetMethodInfo(() => string.Join(null, null));
                int idx = list.FindIndex(item => item.opcode == OpCodes.Call && item.operand as MethodInfo == mJoin);
                if (idx >= 0)
                {
                    list.RemoveRange(idx + 1, list.Count - (idx + 1));
                }
                return list.AsEnumerable();
            }

            // make compiler happy
            _ = Transpiler(null);
            return original;
        }

        static bool Prefix(OriginalCalculator __instance, ref string __result, string original)
        {
            // 在Patch中调用原始实现
            __result = StringOperation(original);
            return false; // 跳过原方法
        }
    }
}
