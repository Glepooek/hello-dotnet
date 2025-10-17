using Newtonsoft.Json;

namespace JsonSubTypesDemo3
{
    public class DemoA : DemoBase
    {
        [JsonProperty("color")]
        public string Color { get; set; }
    }
}
