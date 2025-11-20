
using HarmonyLib;
using HarmonyPatch.Shared;

Harmony harmony = new Harmony("com.example.HarmonyReversePatchSamples");
harmony.PatchAll();

OriginalCalculator calculator = new OriginalCalculator();
int result = calculator.Add(3, 5);
Console.WriteLine($"Result of patched Add(3, 5): {result}"); // 应该输出16，因为原始结果8被乘以2

string specialResult = calculator.SpecialCalculation("part1-part2-part3", 10);

Console.WriteLine($"Result of SpecialCalculation: {specialResult}"); // "part1part2part310Prolog" ，最终输出 "part1part2part3"



Console.ReadLine();