using HumanResourcesManagementBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Repository.Entity
{
    /// <summary>
    /// 权限角色关系
    /// </summary>
    public class PermissionRoleRef : BaseEntity
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        public long PermissionId { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public long RoleId { get; set; }
    }
}
