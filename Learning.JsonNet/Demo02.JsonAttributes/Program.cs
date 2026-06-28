// =============================================================================
// Demo02.JsonAttributes — Controlling serialization with JSON attributes
//
// Demonstrates:
//   - [JsonProperty] to rename fields in JSON output
//   - [JsonIgnore] to exclude sensitive fields from serialization
//   - [JsonRequired] to enforce mandatory fields on deserialization
// =============================================================================

using Newtonsoft.Json;

var user = new User
{
    UserName = "alice_dev",
    Password = "secret123",
    Email = "alice@example.com",
    DisplayName = "Alice"
};

// Password is ignored; UserName serializes as "user_name"
string json = JsonConvert.SerializeObject(user, Formatting.Indented);
Console.WriteLine("=== Serialized (Password absent, user_name renamed) ===");
Console.WriteLine(json);

// Deserialize — JsonRequired means missing Email throws
string input = """{"user_name":"bob","email":"bob@example.com","display_name":"Bob"}""";
var restored = JsonConvert.DeserializeObject<User>(input);
Console.WriteLine($"\n=== Deserialized ===");
Console.WriteLine($"UserName={restored!.UserName}, Email={restored.Email}, DisplayName={restored.DisplayName}");

// Missing required field → exception
Console.WriteLine("\n=== Missing [JsonRequired] field throws ===");
try
{
    JsonConvert.DeserializeObject<User>("""{"user_name":"carol","display_name":"Carol"}""");
}
catch (JsonSerializationException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}

Console.ReadLine();

class User
{
    [JsonProperty("user_name")]
    public string UserName { get; set; } = "";

    [JsonIgnore]
    public string Password { get; set; } = "";

    [JsonRequired]
    public string Email { get; set; } = "";

    [JsonProperty("display_name")]
    public string DisplayName { get; set; } = "";
}
