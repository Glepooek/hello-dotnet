// See https://aka.ms/new-console-template for more information


using HarmonyLib;
using HarmonyPatch.Shared;
using System.Reflection;

Harmony.DEBUG = true;
OriginalPerson person = new OriginalPerson("Alice", 30);

Harmony harmony = new Harmony("com.example.harmonyfinalizersamples");
harmony.PatchAll(Assembly.GetExecutingAssembly());

double result = person.Divide(10, 0);
Console.WriteLine($"Result of division: {result}");


FileLog.FlushBuffer();

Console.ReadLine();
