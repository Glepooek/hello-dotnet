using HarmonyLib;
using HarmonyPatch.Shared;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HarmonyPrioritySamples
{
    internal class MyPlugin2
    {
        public static void RunPlugin2()
        {
            var harmony = new Harmony("net.example.plugin2"); // 初始化Harmony，指定插件ID
            harmony.PatchAll(Assembly.GetExecutingAssembly()); // 为当前程序集的所有补丁注册
        }

        [HarmonyLib.HarmonyPatch(typeof(Foo))] // 指定要补丁的目标类为Foo
        [HarmonyLib.HarmonyPatch("GetSecret")]      // 指定要补丁的目标方法为Bar
        class MyPatch
        {
            // 定义后缀补丁：修改Bar方法的返回结果
            static void Postfix(ref string __result) => __result = "new secret 2";
        }
    }
}
