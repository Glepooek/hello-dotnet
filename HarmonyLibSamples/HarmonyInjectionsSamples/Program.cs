using HarmonyLib;
using HarmonyPatch.Shared;

Harmony harmony = new Harmony("com.example.harmonyinjections.samples");
harmony.PatchAll();


TestPrivateFieldInjection();
TestArgsInjection();

OriginalPerson person = new OriginalPerson("Alice", 30);
Console.WriteLine(person.GetReferenceToData(2));


Console.ReadLine();

// ___filedName，可以注入私有字段
// 修改字段的值，需ref关键字
void TestPrivateFieldInjection()
{
    Console.WriteLine("----- Thread Name Patch Test -----");
    Thread thread = new Thread(() =>
    {
        Console.WriteLine("Hello from the patched thread!");
    });
    thread.Start();
    Console.WriteLine($"Thread Name: {thread.Name}");
}

// object[] __args，注入原始方法的所有参数
// 无需ref关键字，可以直接修改参数值
async void TestArgsInjection()
{
    Console.WriteLine("----- HttpClient SendAsync Patch Test -----");

    var url = "https://www.cnblogs.com";
    HttpClient httpClient = new HttpClient();

    Console.WriteLine($"1.request：{url}");
    // GetAsync内部调用SendAsync方法
    var response = await httpClient.GetAsync(url);
    var content = await response.Content.ReadAsStringAsync();
    Console.WriteLine($"2.response:\n{content.Substring(0, 500)}");
}
