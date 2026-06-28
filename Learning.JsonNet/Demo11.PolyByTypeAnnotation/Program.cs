// =============================================================================
// Demo11.PolyByTypeAnnotation — Polymorphic deserialization using JsonSubtypes
//
// Demonstrates:
//   - [JsonSubtypes] attribute to declare discriminator field and subtype mappings
//   - Deserializing a JSON array into a List<Person> with mixed concrete types
//   - Attribute-based approach requires no custom converter code
// =============================================================================

using Newtonsoft.Json;
using System.Collections.Generic;

namespace JsonSubtypesDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string json =
                "[{\"Department\":\"Department1\",\"JobTitle\":\"JobTitle1\",\"FirstName\":\"FirstName1\",\"LastName\":\"LastName1\"},"
                + "{\"Department\":\"Department1\",\"JobTitle\":\"JobTitle1\",\"FirstName\":\"FirstName1\",\"LastName\":\"LastName1\"},"
                + "{\"Skill\":\"Painter\",\"FirstName\":\"FirstName1\",\"LastName\":\"LastName1\"}]";

            var persons = JsonConvert.DeserializeObject<IReadOnlyCollection<Person>>(json);
        }
    }
}

Console.ReadLine();
