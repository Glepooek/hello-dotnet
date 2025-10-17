using Newtonsoft.Json;

namespace JsonSubTypesDemo3
{
    public class DemoB : DemoBase
    {
        [JsonProperty("size")]
        public double[] Size { get; set; }
    }
}
