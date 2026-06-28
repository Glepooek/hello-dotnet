// =============================================================================
// Demo07.DynamicDeserialization — Four approaches to deserializing JSON without a full model
//
// Demonstrates:
//   - dynamic keyword for schema-free access
//   - JObject.Parse for strongly-typed token navigation
//   - Dictionary<string,object> for flat key-value access
//   - Typed deserialization with a concrete model class
// =============================================================================

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

// 4. Typed deserialization using a local Person model
string typedJson = """{"Name":"Bob","Age":25,"Sex":1,"Address":{"City":"Shanghai","Province":"Shanghai","Town":"","Village":""}}""";
var person = JsonConvert.DeserializeObject<Person>(typedJson);
Console.WriteLine($"[Typed] Name={person!.Name}, City={person.Address?.City}, Sex={person.Sex}");

class Person
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public Gender Sex { get; set; }
    public Address? Address { get; set; }
}

class Address
{
    public string Province { get; set; } = "";
    public string City { get; set; } = "";
    public string Town { get; set; } = "";
    public string Village { get; set; } = "";
}

enum Gender { Male = 0, Female }
