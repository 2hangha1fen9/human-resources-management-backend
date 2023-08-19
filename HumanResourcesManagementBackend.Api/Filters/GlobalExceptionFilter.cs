using HumanResourcesManagementBackend.Api.Exceptions;
using HumanResourcesManagementBackend.Common;
using HumanResourcesManagementBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace HumanResourcesManagementBackend.Api.Filters
{
    /// <summary>
    /// 全局异常处理
    /// </summary>
    public class GlobalExceptionFilter : HandleErrorAttribute
    {
        public bool AllowMultiple => true;

        public override void OnException(ExceptionContext filterContext)
        {
            //获取异常对象
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            //处理异常
            if (ex is ServiceException se)
            {
                var response = new Response
                {
                    Status = se.Status.HasValue ? se.Status.Value : ResponseStatus.Error,
                    Message = string.IsNullOrEmpty(se.Message) ? ResponseStatus.Error.Description() : ex.Message
                };

                //返回错误响应
                filterContext.Result = new JsonResult()
                {
                    Data = response
                };
            }
            else
            {
                filterContext.Result = new HttpStatusCodeResult(400, "Bad Request");
            }
        }
    }
}