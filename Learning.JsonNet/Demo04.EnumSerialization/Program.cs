// =============================================================================
// Demo04.EnumSerialization — Serializing enums as strings instead of integers
//
// Demonstrates:
//   - Default integer enum serialization behavior
//   - StringEnumConverter via JsonSerializerSettings for string representation
//   - [JsonConverter] attribute to apply converters directly on enum types
//   - Round-trip deserialization from string enum values
// =============================================================================

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// Default: enum serializes as integer
var order = new Order { Id = 1, Status = OrderStatus.Processing };
Console.WriteLine("=== Default (integer) ===");
Console.WriteLine(JsonConvert.SerializeObject(order, Formatting.Indented));

// Global StringEnumConverter via settings
var settings = new JsonSerializerSettings();
settings.Converters.Add(new StringEnumConverter());
Console.WriteLine("\n=== StringEnumConverter via settings ===");
Console.WriteLine(JsonConvert.SerializeObject(order, Formatting.Indented, settings));

// Deserialize from string enum
string json = """{"Id":2,"Status":"Shipped"}""";
var restored = JsonConvert.DeserializeObject<Order>(json, settings);
Console.WriteLine($"\n=== Deserialized from string ===");
Console.WriteLine($"Id={restored!.Id}, Status={restored.Status} ({(int)restored.Status})");

// [JsonConverter] attribute on the enum type itself
var item = new Item { Name = "Book", Category = Category.Education };
Console.WriteLine("\n=== [JsonConverter] on enum type ===");
Console.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented));

enum OrderStatus { Pending, Processing, Shipped, Delivered }

class Order
{
    public int Id { get; set; }
    public OrderStatus Status { get; set; }
}

[JsonConverter(typeof(StringEnumConverter))]
enum Category { Electronics, Education, Sports }

class Item
{
    public string Name { get; set; } = "";
    public Category Category { get; set; }
}
