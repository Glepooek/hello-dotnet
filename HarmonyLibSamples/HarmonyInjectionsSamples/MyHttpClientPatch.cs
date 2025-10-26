using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HarmonyInjectionsSamples
{
    [HarmonyLib.HarmonyPatch(typeof(HttpClient), "SendAsync", new Type[] { typeof(HttpRequestMessage), typeof(HttpCompletionOption), typeof(CancellationToken) })]
    internal class MyHttpClientPatch
    {
        static void Prefix(object[] __args)
        {
            HttpRequestMessage request = (HttpRequestMessage)__args[0];
            request.RequestUri = new Uri("https://www.baidu.com/");
        }
    }
}
