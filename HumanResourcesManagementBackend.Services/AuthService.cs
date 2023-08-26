using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Models.Dto;
using HumanResourcesManagementBackend.Repository;
using HumanResourcesManagementBackend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPermissionService _permissionService;
        //权限缓存
        private static readonly Dictionary<long, List<PermissionDto.Permission>> permissionCache = new Dictionary<long, List<PermissionDto.Permission>>();

        public AuthService()
        {
            _permissionService = new PermissionService();
        }

        /// <summary>
        /// 权限检测，没有权限返回异常
        /// </summary>
        /// <param name="check"></param>
        /// <exception cref="BusinessException"></exception>
        public void CheckApi(AuthDto.CheckApi check)
        {
            using(var db = new HRM())
            {
                //获取用户权限
                List<PermissionDto.Permission> permissionList = null;
                //有缓存查询缓存
                if(!permissionCache.TryGetValue(check.UserId,out permissionList))
                {
                    permissionList = _permissionService.GetPermissionsByUserId(check.UserId, PermissionType.Api);
                    permissionCache.Add(check.UserId, permissionList);
                }
                if(permissionList == null)
                {
                    throw new BusinessException();
                }
                //匹配权限
                var permission = permissionList.FirstOrDefault(p => string.Compare(p.Resource,check.Resource,true) == 0 || "*" == p.Resource);
                if(permission == null)
                {
                    throw new BusinessException();
                }

            }
        }

        public void RolePermissionBind(AuthDto.RolePermissionBind bind)
        {
            throw new NotImplementedException();
        }

        public void UserRoleBind(AuthDto.UserRoleBind bind)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 清理权限缓存
        /// </summary>
        /// <param name="userId"></param>
        public static void ClearAuth(long userId)
        {
            permissionCache.Remove(userId);
        }
    }
}
