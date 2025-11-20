using HarmonyPatch.Shared;
using HarmonyLib;
using Microsoft.Extensions.DependencyInjection;
using System;

Harmony harmony = new Harmony("com.example.HarmonyAuxiliaryMethodsSamples");
harmony.PatchAll();

ServiceCollection services = new ServiceCollection();
services.AddTransient<MyService>();
ServiceProvider serviceProvider = services.BuildServiceProvider();

IServiceScope scope = serviceProvider.CreateScope();
MyService myService = scope.ServiceProvider.GetRequiredService<MyService>();

myService.DoSomething();


Console.ReadLine();
