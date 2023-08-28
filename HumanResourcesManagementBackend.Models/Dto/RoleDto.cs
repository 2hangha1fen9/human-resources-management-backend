using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models.Dto
{
    public class RoleDto
    {
        public class Role
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
            /// 创建时间
            /// </summary>
            public DateTime CreateTime { get; set; }
            /// <summary>
            /// 更新时间
            /// </summary>
            public DateTime UpdateTime { get; set; }
            /// <summary>
            /// 是否是默认角色
            /// </summary>
            public YesOrNo IsDefault { get; set; }
            /// <summary>
            /// 是否是默认角色
            /// </summary>
            public string IsDefaultStr { get; set; }
            /// <summary>
            /// 角色名称
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 数据状态 1启用 2禁用 99删除
            /// </summary>
            public string StatusStr { get; set; }
        }
        public class Save
        {
            /// <summary>
            /// Id
            /// </summary>
            public long Id { get; set; }
            /// <summary>
            /// 角色名称
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 数据状态
            /// </summary>
            public DataStatus Status { get; set; }
            /// <summary>
            /// 是否是默认角色
            /// </summary>
            public YesOrNo IsDefault { get; set; }
        }
        public class Search:PageRequest  
        {
            /// <summary>
            /// 角色名
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 数据状态
            /// </summary>
            public DataStatus Status { get; set; }
            /// <summary>
            /// 是否是默认角色
            /// </summary>
            public YesOrNo IsDefault { get; set; }
        }
    }
}
