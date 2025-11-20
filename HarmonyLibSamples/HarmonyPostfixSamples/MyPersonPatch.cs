using HarmonyPatch.Shared;
using System.Diagnostics;

namespace HarmonyProfixSamples;

// 访问原始方法的参数
// 从前缀读取自定义的状态
// 读取或修改原始方法的返回值
// 确保方法始终被执行

[HarmonyLib.HarmonyPatch(typeof(OriginalPerson), "GetName")]
public class MyPersonPatch
{
    static void Postfix(ref string __result)
    {
        Console.WriteLine($"{nameof(MyPersonPatch)}, In Postfix of GetName");
        __result = "Name Modifyied by Patched";
    }
}

[HarmonyLib.HarmonyPatch(typeof(OriginalPerson), "MakeTotalMoney")]
public class MyPersonPatch1
{
    // Access original method parameters
    // 修改原始方法返回值
    static void Postfix(ref int __result, int money, int personalIncomeTax)
    {
        Console.WriteLine($"{nameof(MyPersonPatch1)}, In Postfix of MakeTotalMoney");
        Console.WriteLine($"{nameof(MyPersonPatch1)}, Original money parameter was: {money}, personalIncomeTax parameter was: {personalIncomeTax}");
        if (personalIncomeTax > 0)
        {
            __result += personalIncomeTax * 10;
        }
    }
}

// 穿透式后缀补丁
[HarmonyLib.HarmonyPatch(typeof(OriginalPerson), "GetNumbers")]
public class MyPersonPatch2
{
    static IEnumerable<int> Postfix(IEnumerable<int> v)
    {
        Console.WriteLine($"{nameof(MyPersonPatch2)}, In Postfix of MakeTotalMoney");

        yield return 0;
        foreach (var num in v)
        {
            if (num > 1)
            {
                yield return num * 10;
            }
        }

        yield return 100;
    }
}

// 从前缀读取自定义的状态
[HarmonyLib.HarmonyPatch(typeof(OriginalPerson), "MakeTotalMoney")]
public class MyPersonPatch3
{
    static void Prefix(out Stopwatch __state)
    {
        Console.WriteLine($"{nameof(MyPersonPatch3)}, In Prefix of MakeTotalMoney");
        __state = new Stopwatch();
        __state.Start();
    }

    static void Postfix(Stopwatch __state)
    {
        Console.WriteLine($"{nameof(MyPersonPatch3)}, In Postfix of MakeTotalMoney");
        __state.Stop();
        Console.WriteLine($"{nameof(MyPersonPatch3)}, MakeTotalMoney took {__state.Elapsed} ms");
    }
}
