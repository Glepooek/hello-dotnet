// =============================================================================
// Demo10.SerializationCallbacks — Hooking into the serialization lifecycle with callback attributes
//
// Demonstrates:
//   - [OnSerializing] / [OnSerialized] fired during serialization
//   - [OnDeserializing] / [OnDeserialized] fired during deserialization
//   - Rebuilding computed properties (ItemCount, Summary) in OnDeserialized
// =============================================================================

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

var order = new Order
{
    Id    = 42,
    Items = new List<string> { "Apple", "Banana", "Cherry" },
    CreatedAt = new DateTime(2026, 6, 28)
};

Console.WriteLine("=== Serialize ===");
string json = JsonConvert.SerializeObject(order, Formatting.Indented);
Console.WriteLine(json);

Console.WriteLine("\n=== Deserialize ===");
var restored = JsonConvert.DeserializeObject<Order>(json);
Console.WriteLine($"Id={restored!.Id}");
Console.WriteLine($"ItemCount (computed in OnDeserialized)={restored.ItemCount}");
Console.WriteLine($"Summary={restored.Summary}");

class Order
{
    public int Id { get; set; }
    public List<string> Items { get; set; } = new();
    public DateTime CreatedAt { get; set; }

    // These are computed after deserialization — not stored in JSON
    [JsonIgnore] public int ItemCount { get; private set; }
    [JsonIgnore] public string Summary { get; private set; } = "";

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
        => Console.WriteLine($"  [OnSerializing]  Id={Id}, Items={Items.Count}");

    [OnSerialized]
    private void OnSerialized(StreamingContext ctx)
        => Console.WriteLine($"  [OnSerialized]   done");

    [OnDeserializing]
    private void OnDeserializing(StreamingContext ctx)
        => Console.WriteLine($"  [OnDeserializing] starting...");

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
        // Recompute derived state after all properties are set
        ItemCount = Items.Count;
        Summary   = $"Order #{Id}: {ItemCount} items";
        Console.WriteLine($"  [OnDeserialized] ItemCount={ItemCount}");
    }
}
