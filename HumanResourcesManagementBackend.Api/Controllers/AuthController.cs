using HumanResourcesManagementBackend.Api.Extensions;
using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Models.Dto;
using HumanResourcesManagementBackend.Services;
using HumanResourcesManagementBackend.Services.Interface;
using HumanResourcesManagementBackend.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace HumanResourcesManagementBackend.Api.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IAuthService authService;

        public AuthController()
        {
            this.authService = new AuthService();
        }

        /// <summary>
        /// 用户-角色绑定
        /// </summary>
        /// <param name="bind"></param>
        /// <returns></returns>
        [HttpPost]
        public Response UserRoleBind(AuthDto.UserRoleBind bind)
        {
            authService.UserRoleBind(bind);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }

        /// <summary>
        /// 角色-权限绑定
        /// </summary>
        /// <param name="bind"></param>
        /// <returns></returns>
        [HttpPost]
        public Response RolePermissionBind(AuthDto.RolePermissionBind bind)
        {
            authService.RolePermissionBind(bind);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
    }
}