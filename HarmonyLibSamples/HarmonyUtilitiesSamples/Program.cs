using HarmonyLib;
using HarmonyPatch.Shared;

Harmony.DEBUG = true;


FileLog.Log("Utilities: Traverse");

// 创建 Foo 类型的 Traverse 对象，调用静态方法 MakeFoo 并获取 Foo 实例
var foo = Traverse.Create<Foo>()
    .Method("MakeFoo")
    .GetValue<Foo>();
// 访问 foo 实例的 MyBar 属性，再访问 Bar 结构体的 secret 字段并设置值为 "world"
Traverse.Create(foo)
    .Property("MyBar")
    .Field("secret")
    .SetValue("world");
// 输出结果：WORLD（secret 已被修改为 "world"，经 ToUpper() 处理后变为大写）
FileLog.Log(foo.GetSecret());
Console.WriteLine(foo.GetSecret());


FileLog.FlushBuffer();


Console.ReadLine();
