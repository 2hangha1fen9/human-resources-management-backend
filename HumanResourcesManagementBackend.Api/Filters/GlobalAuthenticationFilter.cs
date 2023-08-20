using HumanResourcesManagementBackend.Api.Extensions;
using HumanResourcesManagementBackend.Common;
using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Services;
using HumanResourcesManagementBackend.Services.Interface;
using System.Linq;
using System.Net.Http;
using System.Net;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http;
using System.Web;
using System.Text;

namespace HumanResourcesManagementBackend.Api.Filters
{
    /// <summary>
    /// 全局鉴权
    /// </summary>
    public class GlobalAuthenticationFilter : AuthorizationFilterAttribute
    {
        public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                try
                {
                    //匿名Action放行
                    if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
                    {
                        return;
                    }
                    //获取Token
                    var token = GetTokenFromRequest(actionContext.Request);
                    //验证Token,并获取自定义信息
                    var payLoad = JwtHelper.VerifyWithPayLoad(token);
                    var user = payLoad.ToObject<UserDto.User>();
                    //DODO: 验证权限

                    //将用户信息放入控制器
                    if(actionContext.ControllerContext.Controller is BaseApiController controller)
                    {
                        controller.CurrentUser = user;
                    }
                }
                catch (Exception)
                {
                    HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    HttpContent httpContent = new StringContent(new Response
                    {
                        Status = ResponseStatus.NoPermission,
                        Message = ResponseStatus.NoPermission.Description()
                    }.ToJson());
                    httpResponse.Content = httpContent;
                    actionContext.Response = httpResponse;
                }
            });
        }

        /// <summary>
        /// 获取Header中的Token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string GetTokenFromRequest(HttpRequestMessage request)
        {
            try
            {
                // 在请求中获取并返回 JWT 令牌
                return request.Headers.FirstOrDefault(h => h.Key == "access_token").Value.FirstOrDefault();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}