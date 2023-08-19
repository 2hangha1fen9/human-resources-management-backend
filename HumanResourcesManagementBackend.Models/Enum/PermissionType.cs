using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models.Enum
{
    /// <summary>
    /// 权限类型
    /// </summary>
    public enum PermissionType
    {
        /// <summary>
        /// Url路径
        /// </summary>
        [Description("Url路径")]
        Api = 1,
        /// <summary>
        /// 前端菜单
        /// </summary>
        [Description("前端菜单")]
        Menu = 2,
        /// <summary>
        /// 前端按钮
        /// </summary>
        [Description("前端按钮")]
        Button = 3,
    }
}
