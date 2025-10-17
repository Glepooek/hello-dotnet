using Newtonsoft.Json;

namespace JsonSubTypesDemo3
{
    class DemoData
    {
        [JsonProperty("demoId")]
        public int DemoId { get; set; }

        [JsonProperty("demos")]
        public List<DemoBase> Demos { get; set; }
    }
}
