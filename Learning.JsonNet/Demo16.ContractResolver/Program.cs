// =============================================================================
// Demo16.ContractResolver — Dynamic serialization control with custom ContractResolver
//
// Demonstrates:
//   - CamelCasePropertyNamesContractResolver for global camelCase renaming
//   - Custom resolver suppressing InternalId via ShouldSerialize
//   - Conditional serialization with ShouldSerialize delegate based on runtime value
// =============================================================================

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

var user = new User { FirstName = "Alice", LastName = "Wang", Age = 30, InternalId = 99 };

// 1. Built-in CamelCasePropertyNamesContractResolver
var camelSettings = new JsonSerializerSettings
{
    ContractResolver = new CamelCasePropertyNamesContractResolver(),
    Formatting       = Formatting.Indented
};
Console.WriteLine("=== CamelCase resolver ===");
Console.WriteLine(JsonConvert.SerializeObject(user, camelSettings));

// 2. Custom resolver that hides InternalId
var hiddenSettings = new JsonSerializerSettings
{
    ContractResolver = new HideInternalIdResolver(),
    Formatting       = Formatting.Indented
};
Console.WriteLine("\n=== HideInternalId resolver ===");
Console.WriteLine(JsonConvert.SerializeObject(user, hiddenSettings));

// 3. Dynamic property selection via ShouldSerialize delegate
var conditionalSettings = new JsonSerializerSettings
{
    ContractResolver = new AgeAbove28Resolver(),
    Formatting       = Formatting.Indented
};
Console.WriteLine("\n=== AgeAbove28 resolver (Age omitted because Age=30 > 28 is shown; test with Age=20) ===");
var young = new User { FirstName = "Bob", LastName = "Li", Age = 20, InternalId = 1 };
Console.WriteLine(JsonConvert.SerializeObject(young, conditionalSettings));

// HideInternalIdResolver: suppresses InternalId from output
class HideInternalIdResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var prop = base.CreateProperty(member, memberSerialization);
        if (prop.PropertyName == nameof(User.InternalId))
            prop.ShouldSerialize = _ => false;
        return prop;
    }
}

// AgeAbove28Resolver: omits Age when value <= 28
class AgeAbove28Resolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var prop = base.CreateProperty(member, memberSerialization);
        if (prop.PropertyName == nameof(User.Age))
            prop.ShouldSerialize = obj => obj is User u && u.Age > 28;
        return prop;
    }
}

class User
{
    public string FirstName  { get; set; } = "";
    public string LastName   { get; set; } = "";
    public int    Age        { get; set; }
    public int    InternalId { get; set; }
}

Console.ReadLine();
