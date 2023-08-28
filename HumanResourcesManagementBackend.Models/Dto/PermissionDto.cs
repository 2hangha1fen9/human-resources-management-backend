using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models.Dto
{
    public class PermissionDto
    {
        public class Permission
        {
            /// <summary>
            /// Id
            /// </summary>
            public long Id { get; set; }
            /// <summary>
            /// 数据状态 1启用 2禁用 99删除
            /// </summary>
            public DataStatus Status { get; set; }
            /// <summary>
            /// 数据状态 1启用 2禁用 99删除
            /// </summary>
            public string StatusStr { get; set; }
            /// <summary>
            /// 创建时间
            /// </summary>
            public DateTime CreateTime { get; set; }
            /// <summary>
            /// 更新时间
            /// </summary>
            public DateTime UpdateTime { get; set; }
            /// <summary>
            /// 权限名称
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 权限类型 1Api 2菜单
            /// </summary>
            public PermissionType Type { get; set; }
            /// <summary>
            /// 权限类型 1Api 2菜单
            /// </summary>
            public string TypeStr { get; set; }
            /// <summary>
            /// 是否是公共权限 1 Yes 2 No
            /// </summary>
            public YesOrNo IsPublic { get; set; }
            /// <summary>
            /// 是否是公共权限 1 Yes 2 No
            /// </summary>
            public string IsPublicStr { get; set; }
            /// <summary>
            /// 权限标识 根据权限类型有所区别  *代表所有权限
            /// </summary>
            public string Resource { get; set; }
        }
        public class Save
        {
            /// <summary>
            /// Id
            /// </summary>
            public long Id { get; set; }
            /// <summary>
            /// 数据状态 1启用 2禁用 99删除
            /// </summary>
            public DataStatus Status { get; set; }
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
        public class Search :PageRequest
        {
            /// <summary>
            /// 数据状态 1启用 2禁用 99删除
            /// </summary>
            public DataStatus Status { get; set; }
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
            /// <summary>
            /// 用户ID
            /// </summary>
            public long UserId { get; set; }
        }
    }
}
