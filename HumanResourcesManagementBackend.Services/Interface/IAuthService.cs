using HumanResourcesManagementBackend.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Services.Interface
{
    public interface IAuthService
    {
        /// <summary>
        /// 用户角色绑定
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        void UserRoleBind(AuthDto.UserRoleBind bind);
        /// <summary>
        /// 角色权限绑定
        /// </summary>
        /// <param name="bind"></param>
        void RolePermissionBind(AuthDto.RolePermissionBind bind);

        /// <summary>
        /// 判断用户是否有权限访问api资源（没有返回异常）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        void CheckApi(AuthDto.CheckApi check);

        /// <summary>
        /// 同步权限
        /// </summary>
        /// <param name="permissions"></param>
        void SyncPermission(List<PermissionDto.Save> permissions);
    }
}
