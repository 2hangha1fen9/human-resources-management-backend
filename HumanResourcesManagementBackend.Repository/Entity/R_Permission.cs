using HumanResourcesManagementBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Repository
{
    /// <summary>
    /// 权限
    /// </summary>
    public class R_Permission : BaseEntity
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 权限类型 1Api 2菜单
        /// </summary>
        public PermissionType Type { get; set; }
        /// <summary>
        /// 是否是公共权限 1 Yes 2 No
        /// </summary>
        public YesOrNo IsPublic { get; set; }
        /// <summary>
        /// 权限标识 根据权限类型有所区别  *代表所有权限
        /// </summary>
        public string Resource { get; set; }
    }
}
