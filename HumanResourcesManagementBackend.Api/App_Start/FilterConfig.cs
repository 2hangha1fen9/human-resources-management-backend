using HumanResourcesManagementBackend.Api.Filters;
using System.Web;
using System.Web.Mvc;

namespace HumanResourcesManagementBackend.Api
{
    public class FilterConfig
    {
        /// <summary>
        /// 注册过滤器
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //注册全局异常过滤器
            filters.Add(new GlobalExceptionFilter());
        }
    }
}
