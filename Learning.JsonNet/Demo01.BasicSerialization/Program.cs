using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// Serialize a single object with indentation
var person = new Person { Name = "Alice", Age = 30, City = "Beijing" };
string json = JsonConvert.SerializeObject(person, Formatting.Indented);
Console.WriteLine("=== Serialize single object ===");
Console.WriteLine(json);

// Deserialize back to typed object
var restored = JsonConvert.DeserializeObject<Person>(json);
Console.WriteLine($"\n=== Deserialize back ===");
Console.WriteLine($"Name={restored!.Name}, Age={restored.Age}, City={restored.City}");

// Serialize a list
var people = new List<Person>
{
    new() { Name = "Bob",   Age = 25, City = "Shanghai" },
    new() { Name = "Carol", Age = 28, City = "Guangzhou" }
};
Console.WriteLine("\n=== Serialize list ===");
Console.WriteLine(JsonConvert.SerializeObject(people, Formatting.Indented));

// Compact (no indentation) output
Console.WriteLine("\n=== Compact output ===");
Console.WriteLine(JsonConvert.SerializeObject(person));

class Person
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public string City { get; set; } = "";
}
