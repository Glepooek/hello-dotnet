using HarmonyLib;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HarmonyAuxiliaryMethodsSamples
{
    //[HarmonyLib.HarmonyPatch("Microsoft.Extensions.DependencyInjection.ServiceLookup.ServiceProviderEngineScope, Microsoft.Extensions.DependencyInjection", null, HarmonyLib.MethodType.Constructor)]
    //[HarmonyLib.HarmonyPatch(new Type[2] { typeof(ServiceProvider), typeof(bool) })]
    //internal class MyServiceProviderEngineScopePatch
    //{
    //    public static void Prefix(bool isRootScope)
    //    {
    //        Console.WriteLine("----------------------------");
    //        Console.WriteLine($"isRootScope:{isRootScope}");
    //        Console.WriteLine(Environment.StackTrace);
    //        Console.WriteLine("----------------------------");
    //    }
    //}

    [HarmonyLib.HarmonyPatch]
    internal class MyServiceProviderEngineScopePatch
    {
        static void Prepare(MethodBase original, Harmony harmony, Exception ex)
        {
            Console.WriteLine("++++++++++++++ Prepare ++++++++++++++");
            Console.WriteLine($"original:{original?.Name ?? "Unknown"}");
        }

        static MethodBase TargetMethod()
        {
            Console.WriteLine("++++++++++++++ TargetMethod ++++++++++++++");

            //Type type = Type.GetType("Microsoft.Extensions.DependencyInjection.ServiceLookup.ServiceProviderEngineScope, Microsoft.Extensions.DependencyInjection");
            //ConstructorInfo constructor = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)[0];

            Type type = AccessTools.TypeByName("Microsoft.Extensions.DependencyInjection.ServiceLookup.ServiceProviderEngineScope, Microsoft.Extensions.DependencyInjection");
            //ConstructorInfo constructor = AccessTools.Constructor(type, new Type[2] { typeof(ServiceProvider), typeof(bool) });

            ConstructorInfo constructor = AccessTools.FirstConstructor(type, c =>
            {
                var info = c.GetParameters();
                return info.Length == 2 && info[0].ParameterType == typeof(ServiceProvider) && info[1].ParameterType == typeof(bool);
            });

            return constructor;
        }

        static void Prefix(bool isRootScope)
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine($"isRootScope:{isRootScope}");
            Console.WriteLine(Environment.StackTrace);
            Console.WriteLine("----------------------------");
        }

        static void Cleanup(MethodBase original, Harmony harmony, Exception ex)
        {
            Console.WriteLine("++++++++++++++ Cleanup ++++++++++++++");
            Console.WriteLine($"original:{original?.Name ?? "Unknown"}");
        }
    }
}
