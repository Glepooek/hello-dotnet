using Demo00.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// Three ways to deserialize JSON without a concrete model

const string json =
    @"{ 'Name': 'Alice', 'Address': { 'City': 'Beijing', 'Province': 'Beijing' }, 'Age': 30 }";

// 1. dynamic keyword — no compile-time type safety
dynamic stuff = JsonConvert.DeserializeObject(json)!;
Console.WriteLine($"[dynamic] Name={stuff.Name}, City={stuff.Address.City}");

// 2. JObject.Parse — strongly typed token access
var jobj = JObject.Parse(json);
string name = jobj["Name"]!.Value<string>()!;
string city = jobj["Address"]!["City"]!.Value<string>()!;
Console.WriteLine($"[JObject] Name={name}, City={city}");

// 3. Dictionary<string, object> — flat access only
var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json)!;
Console.WriteLine($"[Dictionary] Name={dict["Name"]}, Age={dict["Age"]}");

// 4. Typed deserialization using Demo00.Shared.Person
string typedJson = """{"Name":"Bob","Age":25,"Sex":1,"Address":{"City":"Shanghai","Province":"Shanghai","Town":"","Village":""}}""";
var person = JsonConvert.DeserializeObject<Person>(typedJson);
Console.WriteLine($"[Typed] Name={person!.Name}, City={person.Address?.City}, Sex={person.Sex}");
