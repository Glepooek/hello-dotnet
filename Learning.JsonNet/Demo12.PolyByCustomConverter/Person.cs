using JsonSubTypes;
using Newtonsoft.Json;

namespace JsonSubtypesDemo2
{
    [JsonConverter(typeof(PersonJsonConverter))]
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PersonType PersonType { get; set; }
    }

    [JsonConverter(typeof(JsonSubtypes))]
    public class Employee : Person
    {
        public string Department { get; set; }
        public string JobTitle { get; set; }
    }

    [JsonConverter(typeof(JsonSubtypes))]
    public class Artist : Person
    {
        public string Skill { get; set; }
    }

    public enum PersonType
    {
        A,
        B
    }
}
