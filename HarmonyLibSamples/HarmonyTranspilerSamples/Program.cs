using HarmonyLib;
using HarmonyPatch.Shared;

Harmony harmony = new Harmony("com.example.harmonyinjections.samples");
harmony.PatchAll();

OriginalPerson person = new OriginalPerson("Alice", 30);
Console.WriteLine(person.Greet("Hi"));

Console.ReadLine();