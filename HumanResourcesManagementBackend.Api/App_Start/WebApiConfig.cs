using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Net.Http.Formatting;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;
using System.Web;
using HumanResourcesManagementBackend.Api.Filters;
using System.Text;

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
            //全局鉴权
            GlobalConfiguration.Configuration.Filters.Add(new GlobalAuthenticationFilter());
            //全局异常配置
            GlobalConfiguration.Configuration.Filters.Add(new GlobalExceptionFilter());
        }
    }

    /// <summary>
    /// 开启Session支持
    /// </summary>
    public class SessionableControllerHandler : HttpControllerHandler, IRequiresSessionState
    {
        public SessionableControllerHandler(RouteData routeData) : base(routeData) { }
    }
    public class SessionStateRouteHandler : IRouteHandler
    {
        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            return new SessionableControllerHandler(requestContext.RouteData);
        }
    }
}
