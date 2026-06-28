using System;
using Newtonsoft.Json.Linq;

string json = """
{
  "store": {
    "book": [
      { "category": "fiction",  "title": "A Tale", "price": 8.99  },
      { "category": "science",  "title": "Cosmos", "price": 14.99 },
      { "category": "fiction",  "title": "Dune",   "price": 12.99 }
    ],
    "bicycle": { "color": "red", "price": 19.99 }
  }
}
""";

var root = JObject.Parse(json);

// Single value via dot notation
var bicycleColor = root.SelectToken("store.bicycle.color");
Console.WriteLine($"=== SelectToken single ===\nBicycle color: {bicycleColor}");

// Wildcard: all book titles
var titles = root.SelectTokens("store.book[*].title");
Console.WriteLine("\n=== SelectTokens all titles ===");
foreach (var t in titles) Console.WriteLine($"  {t}");

// Filter expression: books where price < 13
var cheap = root.SelectTokens("store.book[?(@.price < 13)]");
Console.WriteLine("\n=== Filter price < 13 ===");
foreach (JObject book in cheap)
    Console.WriteLine($"  {book["title"]} - ${book["price"]}");

// Recursive descent: all price values in the store subtree
var allPrices = root.SelectTokens("store..price");
Console.WriteLine("\n=== Recursive descent all prices ===");
foreach (var p in allPrices) Console.WriteLine($"  ${p}");

// Array index access
var secondTitle = root.SelectToken("store.book[1].title");
Console.WriteLine($"\n=== Array index [1].title ===\n  {secondTitle}");
