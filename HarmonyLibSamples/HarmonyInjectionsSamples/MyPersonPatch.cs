using HarmonyLib;
using HarmonyPatch.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarmonyInjectionsSamples
{
    [HarmonyLib.HarmonyPatch(typeof(OriginalPerson), "GetReferenceToData")]
    internal class MyPersonPatch
    {
        public static void Postfix(ref RefResult<int> __resultRef, int index)
        {
            if (__resultRef == null)
            {
                Console.WriteLine("错误: RefResult 委托为 null");
                return;
            }

            // 通过 __resultRef 修改引用返回值
            // 这里我们将返回值乘以 10
            if (index >= 0)
            {
                ref int resultRef = ref __resultRef();
                resultRef *= 10;
                Console.WriteLine($"修改引用返回值: 索引 {index} 的值被放大10倍");
            }
        }
    }
}
