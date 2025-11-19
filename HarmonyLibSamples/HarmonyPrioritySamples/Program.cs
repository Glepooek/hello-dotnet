using HarmonyLib;
using HarmonyPatch.Shared;
using HarmonyPrioritySamples;

MyPlugin1.RunPlugin1();
MyPlugin2.RunPlugin2();

var foo = Traverse.Create<Foo>()
    .Method("MakeFoo")
    .GetValue<Foo>();
string result = Traverse.Create(foo).Method("GetSecret").GetValue<string>();
Console.WriteLine(result);

Console.ReadLine();