using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// TypeNameHandling.Auto: emits $type only when declared type != actual type
var settings = new JsonSerializerSettings
{
    TypeNameHandling = TypeNameHandling.Auto,
    Formatting       = Formatting.Indented
};

var shapes = new List<Shape>
{
    new Circle    { Color = "Red",   Radius = 5.0 },
    new Rectangle { Color = "Blue",  Width = 10.0, Height = 4.0 },
    new Circle    { Color = "Green", Radius = 3.5 }
};

string json = JsonConvert.SerializeObject(shapes, settings);
Console.WriteLine("=== Serialized with $type ===");
Console.WriteLine(json);

// Deserialize: $type drives concrete type selection
var restored = JsonConvert.DeserializeObject<List<Shape>>(json, settings);
Console.WriteLine("\n=== Deserialized (polymorphic) ===");
foreach (var shape in restored!)
    Console.WriteLine($"  {shape.GetType().Name,-12} Color={shape.Color,-6}  Area={shape.Area(),7:F2}");

abstract class Shape
{
    public string Color { get; set; } = "";
    public abstract double Area();
}

class Circle : Shape
{
    public double Radius { get; set; }
    public override double Area() => Math.PI * Radius * Radius;
}

class Rectangle : Shape
{
    public double Width  { get; set; }
    public double Height { get; set; }
    public override double Area() => Width * Height;
}
