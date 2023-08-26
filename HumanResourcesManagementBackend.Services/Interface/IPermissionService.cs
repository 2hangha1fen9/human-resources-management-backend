using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Services.Interface
{
    public interface IPermissionService
    {
        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="permission"></param>
        void AddPermission(PermissionDto.Save permission);
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="permissionId"></param>
        void DeletePermission(long permissionId);
        /// <summary>
        /// 编辑权限
        /// </summary>
        /// <param name="permission"></param>
        void EditPermission(PermissionDto.Save permission);
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<PermissionDto.Permission> GetPermissions(PermissionDto.Search search);

        /// <summary>
        /// 根据用户ID查询对应权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        List<PermissionDto.Permission> GetPermissionsByUserId(long userId, PermissionType type);
    }
}
