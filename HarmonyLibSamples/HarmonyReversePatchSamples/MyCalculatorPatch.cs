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
        [HarmonyLib.HarmonyReversePatch]
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
                var list = Transpilers.Manipulator(instructions,
                    item => item.opcode == OpCodes.Ldarg_1,
                    item => item.opcode = OpCodes.Ldarg_0
                ).ToList();
                var mJoin = SymbolExtensions.GetMethodInfo(() => string.Join(null, null));
                var idx = list.FindIndex(item => item.opcode == OpCodes.Call && item.operand as MethodInfo == mJoin);
                list.RemoveRange(idx + 1, list.Count - (idx + 1));
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
