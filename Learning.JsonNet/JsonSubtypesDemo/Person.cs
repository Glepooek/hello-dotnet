using JsonSubTypes;
using Newtonsoft.Json;

namespace JsonSubtypesDemo
{
    [JsonConverter(typeof(JsonSubtypes))]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(Employee), "JobTitle")]
    [JsonSubtypes.KnownSubTypeWithProperty(typeof(Artist), "Skill")]
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
