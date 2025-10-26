// See https://aka.ms/new-console-template for more information

using HarmonyLib;

Harmony harmony = new Harmony("com.example.harmonyinjectionssamples");
harmony.PatchAll();


Thread thread = new Thread(() =>
{
    Console.WriteLine("Hello from the patched thread!");
});

thread.Start();

Console.WriteLine(thread.Name);


Console.WriteLine("----- HttpClient SendAsync Patch Test -----");

var url = "https://www.cnblogs.com";

var httpClient = new HttpClient();

Console.WriteLine($"1.request：{url}");
// GetAsync内部调用SendAsync方法
var response = await httpClient.GetAsync(url);
var content = await response.Content.ReadAsStringAsync();
Console.WriteLine($"2.response:\n{content.Substring(0, 500)}");


Console.ReadLine();
