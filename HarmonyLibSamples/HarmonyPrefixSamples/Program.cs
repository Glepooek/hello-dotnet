// See https://aka.ms/new-console-template for more information
using HarmonyLib;
using HarmonyPatch.Shared;

Harmony.DEBUG = true;

OriginalPerson person = new("John", 25);
Console.WriteLine("Before patch: " + person.ToString());

Harmony harmony = new("com.example.harmonyprofixsamples");

FileLog.Log("Hello, Harmony!");
harmony.PatchAll();

FileLog.FlushBuffer();

Console.WriteLine("After patch GetName: " + person.GetName());
Console.WriteLine("After patch MakeTotalMoney: " + person.MakeTotalMoney(10));

Console.ReadLine();