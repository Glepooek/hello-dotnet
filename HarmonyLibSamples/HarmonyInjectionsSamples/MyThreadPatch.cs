using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonyInjectionsSamples
{
    [HarmonyLib.HarmonyPatch(typeof(Thread))]
    internal class MyThreadPatch
    {
        [HarmonyLib.HarmonyPrefix]
        [HarmonyLib.HarmonyPatch("Name", HarmonyLib.MethodType.Getter)]
        static void ModifyThreadName(Thread __instance, ref string ____name)
        {
            if (string.IsNullOrEmpty(____name))
            {
                ____name = $"Default Thread Name: {__instance.ManagedThreadId}";
            }
        }
    }
}
