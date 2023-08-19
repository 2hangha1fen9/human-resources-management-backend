using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Common
{
    /// <summary>
    /// JSON序列化工具类
    /// </summary>
    public static class JsonHelper
    {
        private static readonly JsonSerializerSettings setting = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
        };

        /// <summary>
        /// 对象转JSON
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ToJson(this object o)
        {
            if (o == null)
            {
                return String.Empty;
            }
            if (o is string)
            {
                return o.ToString();
            }
            return JsonConvert.SerializeObject(o, setting);
        }

        /// <summary>
        /// JSON序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ToObject<T>(this string json)
        {
            try
            {
                if (String.IsNullOrEmpty(json))
                {
                    return default(T);
                }
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
