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
