// =============================================================================
// Demo18.JObjectMerge — Deep-merging JSON objects and applying null-patch removal
//
// Demonstrates:
//   - JObject.Merge for deep config override (nested objects merged, not replaced)
//   - MergeNullValueHandling.Ignore to skip null values in the override
//   - Null-patch pattern: set a key to null then strip all null properties to simulate deletion
// =============================================================================

using Newtonsoft.Json.Linq;
using System.Linq;

// Base config represents defaults; prod override provides environment-specific values
var baseConfig = JObject.Parse("""
{
  "app": "MyApp",
  "version": "1.0.0",
  "database": { "host": "localhost", "port": 5432, "name": "mydb" },
  "features": { "darkMode": false, "beta": false }
}
""");

var prodOverride = JObject.Parse("""
{
  "version": "1.2.3",
  "database": { "host": "prod-db.example.com", "name": "mydb_prod" },
  "features": { "beta": true }
}
""");

Console.WriteLine("=== Base config ===");
Console.WriteLine(baseConfig.ToString());

// Deep merge: nested objects merged, null overrides ignored
baseConfig.Merge(prodOverride, new JsonMergeSettings
{
    MergeNullValueHandling = MergeNullValueHandling.Ignore,
    MergeArrayHandling     = MergeArrayHandling.Union
});
Console.WriteLine("\n=== After prod override merge ===");
Console.WriteLine(baseConfig.ToString());

// Null-patch: set a key to null to delete it
var patch = JObject.Parse("""{"features":{"darkMode":null}}""");
baseConfig.Merge(patch, new JsonMergeSettings
{
    MergeNullValueHandling = MergeNullValueHandling.Merge
});

// Strip all null-valued properties after merge
var nullProps = baseConfig.Descendants()
    .OfType<JProperty>()
    .Where(p => p.Value.Type == JTokenType.Null)
    .ToList();
foreach (var p in nullProps) p.Remove();

Console.WriteLine("\n=== After null-patch (darkMode removed) ===");
Console.WriteLine(baseConfig.ToString());

Console.ReadLine();
