using HumanResourcesManagementBackend.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Services.Interface
{
    public interface IRoleService
    {
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        List<RoleDto.Role> GetRoles(RoleDto.Search search);

        /// <summary>
        /// 根据用户ID获取拥有的角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<RoleDto.Role> GetRolesByUserId(long userId);

        /// <summary>
        /// 根据ID获取角色名称
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        RoleDto.Role GetRoleById(long roleId);

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role"></param>
        void AddRole(RoleDto.Save role);

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="role"></param>
        void EditRole(RoleDto.Save role);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId"></param>
        void DeleteRole(long roleId);
    }
}
