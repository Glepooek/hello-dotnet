using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

// https://www.cnblogs.com/qixue/p/5292374.html

namespace JsonSubtypesDemo2
{
    public class PersonJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            //return objectType == typeof(Person);
            return typeof(Person).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                Person result = null;
                var jsonObject = JObject.Load(reader);

                bool success = Enum.TryParse(jsonObject["PersonType"].ToString(), out PersonType action);
                if (!success)
                {
                    Console.WriteLine("未解析的PersonType类型:" + jsonObject["PersonType"].ToString());
                    return result;
                }

                switch (action)
                {
                    case PersonType.A:
                        result = jsonObject.ToObject<Artist>(serializer);
                        break;
                    case PersonType.B:
                        result = jsonObject.ToObject<Employee>(serializer);
                        break;
                    default:
                        break;
                }

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
