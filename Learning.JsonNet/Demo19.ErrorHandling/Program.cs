// =============================================================================
// Demo19.ErrorHandling — Fault-tolerant batch deserialization using the Error event handler
//
// Demonstrates:
//   - JsonSerializerSettings.Error delegate intercepts per-field conversion failures
//   - ErrorContext.Handled = true continues deserialization (bad fields get defaults, not skipped)
//   - Collecting all error paths and messages for post-processing
// =============================================================================

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// Batch deserialize: bad fields fall back to defaults; Handled=true prevents the whole deserialization from throwing
string json = """
[
  {"Id":1,"Name":"Alice","Score":95},
  {"Id":2,"Name":"Bob",  "Score":"not-a-number"},
  {"Id":3,"Name":"Carol","Score":88},
  {"Id":"bad-id","Name":"Dave","Score":72},
  {"Id":5,"Name":"Eve",  "Score":100}
]
""";

var errors = new List<string>();
var settings = new JsonSerializerSettings
{
    Error = (_, args) =>
    {
        errors.Add($"  Path={args.ErrorContext.Path}  →  {args.ErrorContext.Error.Message}");
        args.ErrorContext.Handled = true;  // mark handled so deserialization continues
    }
};

var results = JsonConvert.DeserializeObject<List<Student>>(json, settings) ?? [];

Console.WriteLine($"=== Successfully deserialized ({results.Count} items) ===");
foreach (var s in results)
    Console.WriteLine($"  Id={s.Id}, Name={s.Name}, Score={s.Score}");

Console.WriteLine($"\n=== Errors ({errors.Count}) ===");
foreach (var e in errors) Console.WriteLine(e);

Console.ReadLine();

class Student
{
    public int    Id    { get; set; }
    public string Name  { get; set; } = "";
    public int    Score { get; set; }
}

