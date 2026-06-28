// =============================================================================
// Demo08.NamingStrategy — Automatic property name conversion with NamingStrategy
//
// Demonstrates:
//   - CamelCaseNamingStrategy (FirstName → firstName)
//   - SnakeCaseNamingStrategy (FirstName → first_name)
//   - Round-trip deserialization from snake_case JSON back to PascalCase model
// =============================================================================

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var user = new UserProfile
{
    FirstName   = "Alice",
    LastName    = "Wang",
    EmailAddress = "alice@example.com",
    PhoneNumber = "13800000000"
};

// CamelCase: FirstName -> firstName
var camelSettings = new JsonSerializerSettings
{
    ContractResolver = new DefaultContractResolver
    {
        NamingStrategy = new CamelCaseNamingStrategy()
    }
};
Console.WriteLine("=== CamelCase ===");
Console.WriteLine(JsonConvert.SerializeObject(user, Formatting.Indented, camelSettings));

// SnakeCase: FirstName -> first_name
var snakeSettings = new JsonSerializerSettings
{
    ContractResolver = new DefaultContractResolver
    {
        NamingStrategy = new SnakeCaseNamingStrategy()
    }
};
Console.WriteLine("\n=== SnakeCase ===");
Console.WriteLine(JsonConvert.SerializeObject(user, Formatting.Indented, snakeSettings));

// Deserialize snake_case JSON back to PascalCase model
string snakeJson = """
    {"first_name":"Bob","last_name":"Li","email_address":"bob@example.com","phone_number":"13900000000"}
    """;
var restored = JsonConvert.DeserializeObject<UserProfile>(snakeJson, snakeSettings);
Console.WriteLine($"\n=== Deserialized from snake_case ===");
Console.WriteLine($"FirstName={restored!.FirstName}, Email={restored.EmailAddress}");

class UserProfile
{
    public string FirstName    { get; set; } = "";
    public string LastName     { get; set; } = "";
    public string EmailAddress { get; set; } = "";
    public string PhoneNumber  { get; set; } = "";
}

Console.ReadLine();
