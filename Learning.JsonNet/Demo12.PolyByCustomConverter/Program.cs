using Newtonsoft.Json;
using System.Collections.Generic;

namespace JsonSubtypesDemo2
{
    class Program
    {
        static void Main(string[] args)
        {
            string json = "[    {        \"Department\": \"Department1\",        \"JobTitle\": \"JobTitle1\",        \"FirstName\": \"FirstName1\",        \"LastName\": \"LastName1\",        \"PersonType\": \"B\"    },    {        \"Department\": \"Department1\",        \"JobTitle\": \"JobTitle1\",        \"FirstName\": \"FirstName1\",        \"LastName\": \"LastName1\",        \"PersonType\": \"B\"    },    {        \"Skill\": \"Painter\",        \"FirstName\": \"FirstName1\",        \"LastName\": \"LastName1\",        \"PersonType\": \"A\"    }]";

            // 移除Model特性JsonConverter
            //var settings = new JsonSerializerSettings();
            //settings.Converters.Add(new PersonJsonConverter());
            //var persons = JsonConvert.DeserializeObject<List<Person>>(json, settings);

            var persons = JsonConvert.DeserializeObject<List<Person>>(json);
        }
    }
}
