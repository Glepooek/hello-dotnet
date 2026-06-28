// =============================================================================
// Demo03.JsonSettings — Fine-tuning behavior with JsonSerializerSettings
//
// Demonstrates:
//   - NullValueHandling.Ignore to omit null properties from JSON
//   - DefaultValueHandling.Ignore to exclude zero/false/empty defaults
//   - DateFormatString for custom date format patterns
// =============================================================================

using Newtonsoft.Json;
using System;

// 1. NullValueHandling.Ignore — null properties omitted from output
var nullSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
var product = new Product { Name = "Widget", Description = null, Price = 9.99m };
Console.WriteLine("=== NullValueHandling.Ignore ===");
Console.WriteLine(JsonConvert.SerializeObject(product, Formatting.Indented, nullSettings));

// 2. DefaultValueHandling.Ignore — zero/false/empty string omitted
var defaultSettings = new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore };
var order = new Order { Id = 1, Quantity = 0, IsActive = false };
Console.WriteLine("\n=== DefaultValueHandling.Ignore ===");
Console.WriteLine(JsonConvert.SerializeObject(order, Formatting.Indented, defaultSettings));

// 3. DateFormatString — custom date format
var dateSettings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-dd" };
var ev = new Event { Title = "Conference", Date = new DateTime(2026, 6, 28) };
Console.WriteLine("\n=== DateFormatString yyyy-MM-dd ===");
Console.WriteLine(JsonConvert.SerializeObject(ev, Formatting.Indented, dateSettings));

Console.ReadLine();

class Product
{
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public decimal Price { get; set; }
}

class Order
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public bool IsActive { get; set; }
}

class Event
{
    public string Title { get; set; } = "";
    public DateTime Date { get; set; }
}