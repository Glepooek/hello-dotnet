using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// Build object graph with circular references: Employee <-> Department
var dept  = new Department { Name = "Engineering" };
var alice = new Employee   { Name = "Alice", Department = dept };
var bob   = new Employee   { Name = "Bob",   Department = dept, Manager = alice };
dept.Employees.Add(alice);
dept.Employees.Add(bob);

// 1. ReferenceLoopHandling.Ignore — stops recursion silently
var ignoreSettings = new JsonSerializerSettings
{
    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
    Formatting            = Formatting.Indented
};
Console.WriteLine("=== ReferenceLoopHandling.Ignore ===");
Console.WriteLine(JsonConvert.SerializeObject(alice, ignoreSettings));

// 2. PreserveReferencesHandling — keeps full graph using $id/$ref
var preserveSettings = new JsonSerializerSettings
{
    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
    Formatting                 = Formatting.Indented
};
Console.WriteLine("\n=== PreserveReferencesHandling.Objects ===");
Console.WriteLine(JsonConvert.SerializeObject(alice, preserveSettings));

// 3. Default (no handling) — throws on loop
Console.WriteLine("\n=== Default throws on circular reference ===");
try
{
    JsonConvert.SerializeObject(alice);
}
catch (JsonSerializationException ex)
{
    Console.WriteLine($"Caught: {ex.Message[..60]}...");
}

class Department
{
    public string Name { get; set; } = "";
    public List<Employee> Employees { get; set; } = new();
}

class Employee
{
    public string Name { get; set; } = "";
    public Department? Department { get; set; }
    public Employee? Manager { get; set; }
}
