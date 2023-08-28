using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Net.Http.Formatting;
using HumanResourcesManagementBackend.Api.Filters;

namespace HumanResourcesManagementBackend.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //跨域配置
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            // 修改默认的 JSON 序列化器为 Newtonsoft.Json，并配置日期格式、循环引用处理和小驼峰式命名
            GlobalConfiguration.Configuration.Formatters.Clear();
            GlobalConfiguration.Configuration.Formatters.Add(new JsonMediaTypeFormatter { 
                SerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    DateFormatString = "yyyy-MM-dd HH:mm:ss",
                    Formatting = Formatting.None,
                    NullValueHandling = NullValueHandling.Include,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            });
            //路由配置
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //全局鉴权
            GlobalConfiguration.Configuration.Filters.Add(new GlobalAuthenticationFilter());
            //全局异常配置
            GlobalConfiguration.Configuration.Filters.Add(new GlobalExceptionFilter());
        }
    }
}
