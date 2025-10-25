using HarmonyLib;
using HarmonyPatch.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonyProfixSamples
{
    [HarmonyLib.HarmonyPatch(typeof(OriginalPerson), "GetName")]
    public class MyPersonPatch
    {
        // 返回true无法修改方法返回值，返回false能修改方法返回值；
        // 返回false，会跳过原始方法，及其后的Prefix
        // 能修改输入参数
        // 设置可以在后者中调用的状态
        static bool Prefix(ref string __result)
        {
            Console.WriteLine($"{nameof(MyPersonPatch)}, In Prefix of GetName");
            __result = "PatchedName";
            return true; // Continue to original method
        }

        static void Postfix(ref string __result)
        {
            Console.WriteLine("In Postfix of GetName");
            __result = "Modify Name by Patched";
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(OriginalPerson), "MakeTotalMoney")]
    public class MyPersonPatch1
    {
        static bool Prefix(ref int money)
        {
            Console.WriteLine($"{nameof(MyPersonPatch1)}, In Prefix of MakeTotalMoney");
            money = 20;
            return true; // Continue to original method
        }

        //static void Postfix(ref string __result)
        //{
        //    Console.WriteLine("In Postfix of MakeTotalMoney");
        //    __result = "PatchedName2";
        //}
    }

    [HarmonyLib.HarmonyPatch(typeof(OriginalPerson), "MakeTotalMoney")]
    public class MyPersonPatch2
    {
        static void Prefix(out Stopwatch __state)
        {
            Console.WriteLine($"{nameof(MyPersonPatch2)}, In Prefix of MakeTotalMoney");
            __state = new Stopwatch();
            __state.Start();
        }

        static void Postfix(Stopwatch __state)
        {
            Console.WriteLine($"{nameof(MyPersonPatch2)}, In Postfix of MakeTotalMoney");
            __state.Stop();
            Console.WriteLine($"{nameof(MyPersonPatch2)}, MakeTotalMoney took {__state.Elapsed} ms");
        }
    }

}
