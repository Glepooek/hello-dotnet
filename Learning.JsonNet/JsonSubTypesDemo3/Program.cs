// 参考https://www.cnblogs.com/qixue/p/5292374.html

using JsonSubTypesDemo3;
using JsonSubTypesDemo3.Utils;
using Newtonsoft.Json;

DemoData data = new DemoData()
{
    DemoId = 1,
    Demos = new List<DemoBase>()
    {
        new DemoA() { Id=1, Name="demoA", Type="A",Color="red" },
        new DemoB() { Id=2, Name="", Type="B", Size= new double[]{ 10,10} }
    }
};

string json = JsonConvert.SerializeObject(data);
DemoData nData = JsonConvert.DeserializeObject<DemoData>(json, new JsonSubTypeConverter<DemoBase>(new Dictionary<string, DemoBase>
{
    { "color", new DemoA()},
    { "size", new DemoB() }
}));


Console.ReadLine();
