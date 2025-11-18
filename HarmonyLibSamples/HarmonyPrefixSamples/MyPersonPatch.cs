using HarmonyLib;
using HarmonyPatch.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonyPrefixSamples;

// +++++++++++++++针对同一方法的前缀补丁++++++++++++++++
// 返回true无法修改方法返回值，返回false能修改方法返回值；
// 返回false，会跳过原始方法，及其后的前缀补丁，其后的后缀、终接器补丁不受影响；
// 能访问和修改原始方法的参数
// 设置可以在后缀补丁中调用的状态

[HarmonyLib.HarmonyPatch(typeof(OriginalPerson), "GetName")]
public class MyPersonPatch
{
    static bool Prefix(ref string __result)
    {
        Console.WriteLine($"{nameof(MyPersonPatch)}, In Prefix of GetName");
        __result = "PatchedName";
        return true; // Continue to original method
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
