using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Slides;

namespace HarmonyAsposeSamples
{
    [HarmonyLib.HarmonyPatch(typeof(License))]
    internal class AsposeSlidesPatch
    {
        [HarmonyLib.HarmonyPatch("SetLicense", new Type[] { typeof(string) })]
        [HarmonyLib.HarmonyPrefix]
        public static bool SetLicensePrefix()
        {
            //
            return false;
        }

        [HarmonyLib.HarmonyPatch("IsLicensed")]
        [HarmonyLib.HarmonyPrefix]
        public static bool IsLicensedPrefix(ref bool __result)
        {
            __result = true;
            return false;
        }
    }
}
