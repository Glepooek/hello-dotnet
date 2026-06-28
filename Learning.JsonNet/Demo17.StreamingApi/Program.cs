// =============================================================================
// Demo17.StreamingApi — Low-memory streaming read/write with JsonTextWriter and JsonTextReader
//
// Demonstrates:
//   - JsonTextWriter writes JSON token-by-token without building an object graph
//   - JsonTextReader reads JSON token-by-token to extract specific values
//   - Streaming approach keeps memory usage constant regardless of document size
// =============================================================================

using Newtonsoft.Json;

// JsonTextWriter: stream-write large JSON without building the full object graph in memory
var sb = new System.Text.StringBuilder();
using (var sw     = new System.IO.StringWriter(sb))
using (var writer = new JsonTextWriter(sw) { Formatting = Formatting.Indented })
{
    writer.WriteStartArray();
    for (int i = 1; i <= 5; i++)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("id");    writer.WriteValue(i);
        writer.WritePropertyName("name");  writer.WriteValue($"Item {i}");
        writer.WritePropertyName("price"); writer.WriteValue(Math.Round(i * 9.99, 2));
        writer.WriteEndObject();
    }
    writer.WriteEndArray();
}
Console.WriteLine("=== JsonTextWriter output ===");
Console.WriteLine(sb.ToString());

// JsonTextReader: stream-read without deserializing the whole document
Console.WriteLine("\n=== JsonTextReader: extract all 'name' values ===");
using var sr     = new System.IO.StringReader(sb.ToString());
using var reader = new JsonTextReader(sr);
while (reader.Read())
{
    if (reader.TokenType == JsonToken.PropertyName
        && reader.Value?.ToString() == "name")
    {
        reader.Read();   // advance to the value token
        Console.WriteLine($"  name = {reader.Value}");
    }
}

Console.ReadLine();
