using lclass.common.lib.Json;
using lclass.common.lib.Json.Serialization;
using lclass.common.lib.Json.Converters;

namespace lclass.common.lib.Utils
{
    public  class JsonHelper
    {
        static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        static JsonHelper()
        {
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
            JsonSerializerSettings.Converters.Add(timeConverter);
        }

        /// <summary>
        ///  转换对象为json格式字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJSON(object obj)
        {
            // 设置参数为Formatting.Indented可输出格式化后的json
            return JsonConvert.SerializeObject(obj, Formatting.None, JsonSerializerSettings);
        }

        /// <summary>
        /// 转换json格式字符串为指定类型对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ParseJSON<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
