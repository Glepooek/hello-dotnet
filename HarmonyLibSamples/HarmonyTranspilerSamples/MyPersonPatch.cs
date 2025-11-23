using HarmonyLib;
using HarmonyPatch.Shared;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;

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
        static IEnumerable<CodeInstruction> Transpiler(
                IEnumerable<CodeInstruction> instructions, 
                ILGenerator iLGenerator, 
                MethodBase methodBase)
        {
            MethodInfo? methodInfo = typeof(MyPersonPatch).GetMethod("ModifyPrefix", new Type[] { typeof(string) });

            //foreach (CodeInstruction instruction in instructions)
            //{
            //    // 在所有加载参数的地方，将加载的参数替换掉
            //    if (instruction.opcode == OpCodes.Ldarg_1)
            //    {
            //        // 先加载原始值
            //        yield return new CodeInstruction(OpCodes.Ldarg_1);
            //        // 调用修改方法
            //        yield return new CodeInstruction(OpCodes.Call, methodInfo);
            //    }
            //    else
            //    {
            //        yield return instruction;
            //    }
            //}

            CodeMatcher matcher = new CodeMatcher(instructions, iLGenerator);
            while (matcher.MatchStartForward(new CodeMatch(ins => ins.opcode == OpCodes.Ldarg_1)).IsValid)
            {
                matcher.InsertAfterAndAdvance(new CodeInstruction(OpCodes.Call, methodInfo));
            }

            return matcher.InstructionEnumeration();
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
