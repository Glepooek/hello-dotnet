// =============================================================================
// Demo09.CustomConverter — Writing a custom JsonConverter<T> for non-standard types
//
// Demonstrates:
//   - WriteJson to serialize Color struct as "#RRGGBB" hex string
//   - ReadJson to parse hex string back to Color struct
//   - [JsonConverter] attribute to attach converter to specific properties
// =============================================================================

using System;
using Newtonsoft.Json;
var palette = new Palette
{
    Name       = "Sunset",
    Background = new Color(255, 94, 58),
    Foreground = new Color(255, 255, 255)
};

string json = JsonConvert.SerializeObject(palette, Formatting.Indented);
Console.WriteLine("=== Serialized ===");
Console.WriteLine(json);

var restored = JsonConvert.DeserializeObject<Palette>(json);
Console.WriteLine($"\n=== Deserialized ===");
Console.WriteLine($"Background: R={restored!.Background.R} G={restored.Background.G} B={restored.Background.B}");
Console.WriteLine($"Foreground: R={restored.Foreground.R} G={restored.Foreground.G} B={restored.Foreground.B}");

// ColorConverter: read/write Color as "#RRGGBB"
class ColorConverter : JsonConverter<Color>
{
    public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
        => writer.WriteValue($"#{value.R:X2}{value.G:X2}{value.B:X2}");

    public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue,
        bool hasExistingValue, JsonSerializer serializer)
    {
        var hex = reader.Value!.ToString()!.TrimStart('#');
        return new Color(
            Convert.ToInt32(hex[0..2], 16),
            Convert.ToInt32(hex[2..4], 16),
            Convert.ToInt32(hex[4..6], 16));
    }
}

class Palette
{
    public string Name { get; set; } = "";

    [JsonConverter(typeof(ColorConverter))]
    public Color Background { get; set; }

    [JsonConverter(typeof(ColorConverter))]
    public Color Foreground { get; set; }
}

readonly struct Color(int r, int g, int b)
{
    public int R { get; } = r;
    public int G { get; } = g;
    public int B { get; } = b;
}
