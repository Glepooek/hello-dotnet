using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonSubTypesDemo3.Utils
{
    public class JsonSubTypeConverter<T> : JsonConverter
    {
        #region Fields

        /// <summary>
        /// 属性名及属性所属类实例映射字典
        /// </summary>
        /// <remarks>
        /// 该属性是所属类独有的属性
        /// </remarks>
        private readonly Dictionary<string, T> mMapping;

        #endregion

        #region Constructor

        public JsonSubTypeConverter(Dictionary<string, T> mapping)
        {
            mMapping = mapping;
        }

        #endregion

        #region JsonConverter Methods

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var target = Create(objectType, jsonObject);
            serializer.Populate(jsonObject.CreateReader(), target);
            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

        }

        #endregion

        #region Methods

        private T Create(Type objectType, JObject jsonObject)
        {
            if (mMapping.Keys.Count == 0)
            {
                return default(T);
            }

            foreach (var key in mMapping.Keys)
            {
                if (!string.IsNullOrEmpty(key) && jsonObject.Property(key) != null)
                {
                    return mMapping[key];
                }
            }

            return default(T);
        }

        #endregion
    }
}
