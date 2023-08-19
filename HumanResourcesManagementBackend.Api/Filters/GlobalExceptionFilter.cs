using HumanResourcesManagementBackend.Common;
using HumanResourcesManagementBackend.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace HumanResourcesManagementBackend.Api.Filters
{
    /// <summary>
    /// 全局异常处理
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public bool AllowMultiple => true;

        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                //获取异常对象
                Exception ex = actionExecutedContext.Exception;
                var response = new Response();
                //处理异常
                if (ex is BusinessException se)
                {
                    response.Status = se.Status.HasValue ? se.Status.Value : ResponseStatus.Error;
                    response.Message = string.IsNullOrEmpty(se.ErrorMessage) ? ResponseStatus.Error.Description() : se.ErrorMessage;
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = ex.Message;
                }
                HttpResponseMessage httpResponse = new HttpResponseMessage();

                HttpContent httpContent = new StringContent(response.ToJson());

                httpResponse.Content = httpContent;

                actionExecutedContext.Response = httpResponse;

            });
        }
    }
}