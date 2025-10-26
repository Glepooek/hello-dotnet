using HarmonyLib;
using HarmonyPatch.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 抑制异常。在 Finalizer 中捕获异常并处理它，从而防止异常传播到调用者
// 捕获异常后，重新包装异常信息并返回一个默认值

namespace HarmonyFinalizerSamples
{
    [HarmonyLib.HarmonyPatch(typeof(OriginalPerson), "Divide")]
    internal class MyPersonPatch
    {
        static Exception? Finalizer(ref double __result, Exception __exception)
        {
            if (__exception != null)
            {
                Console.WriteLine($"In Finalizer, caught exception: {__exception.Message}");
                FileLog.Log($"In Finalizer, caught exception: {__exception.Message}");
                // Handle the exception and set a default result
                __result = double.NaN; // Return NaN in case of exception
                return null; // Indicate that the exception has been handled
            }

            Console.WriteLine("In Finalizer, method completed successfully.");
            return null; // No exception to handle
        }
    }
}
