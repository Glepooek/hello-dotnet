using System;
using System.Linq;
using Newtonsoft.Json.Linq;

// Build JObject manually
var person = new JObject
{
    ["name"] = "Alice",
    ["age"]  = 30,
    ["address"] = new JObject { ["city"] = "Beijing", ["zip"] = "100000" }
};
Console.WriteLine("=== Constructed JObject ===");
Console.WriteLine(person.ToString());

// Add a JArray property
person["tags"] = new JArray("csharp", "dotnet", "json");
Console.WriteLine("\n=== After adding JArray ===");
Console.WriteLine(person.ToString());

// Query typed values
string name = person["name"]!.Value<string>()!;
string city = person["address"]!["city"]!.Value<string>()!;
Console.WriteLine($"\n=== Query ===\nName={name}, City={city}");

// Parse array and iterate
string json = """[{"id":1,"title":"Post A"},{"id":2,"title":"Post B"},{"id":3,"title":"Post C"}]""";
var posts = JArray.Parse(json);
Console.WriteLine("\n=== JArray iteration ===");
foreach (JObject post in posts)
    Console.WriteLine($"  id={post["id"]}, title={post["title"]}");

// LINQ on JArray
var ids = posts.Select(p => p["id"]!.Value<int>()).ToList();
Console.WriteLine($"\n=== LINQ select ids ===\n{string.Join(", ", ids)}");

// Filter with LINQ where
var filtered = posts.Where(p => p["id"]!.Value<int>() > 1).Select(p => p["title"]).ToList();
Console.WriteLine($"\n=== LINQ filter id > 1 ===\n{string.Join(", ", filtered)}");
