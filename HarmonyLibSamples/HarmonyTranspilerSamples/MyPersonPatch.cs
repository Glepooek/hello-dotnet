using HarmonyLib;
using HarmonyPatch.Shared;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace HarmonyTranspilerSamples
{
    [HarmonyLib.HarmonyPatch(typeof(OriginalPerson), "Greet")]
    public class MyPersonPatch
    {
        // 方式1：使用 Prefix（方法层面修改）
        //static bool Prefix(ref string __result)
        //{
        //    __result = "Modified Hello";
        //    return false;
        //}

        // 方式2：使用 Transpiler（IL层面修改）
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator iLGenerator, MethodBase methodBase)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                // 直接替换所有加载参数的地方
                if (instruction.opcode == OpCodes.Ldarg_1)
                {
                    // 先加载原始值
                    yield return new CodeInstruction(OpCodes.Ldarg_1);
                    // 调用修改方法
                    yield return new CodeInstruction(OpCodes.Call, typeof(MyPersonPatch).GetMethod("ModifyPrefix", new Type[] { typeof(string) }));
                }
                else
                {
                    yield return instruction;
                }
            }
        }

        public static string ModifyPrefix(string originalPrefix)
        {
            if (originalPrefix == "Hi")
            {
                return "Hela";
            }
            return originalPrefix;
        }
    }
}
