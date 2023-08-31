using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models
{
    /// <summary>
    /// 审核类型
    /// </summary>
    public enum AuditType
    {
        /// <summary>
        /// 部门主管
        /// </summary>
        [Description("部门主管")]
        DepartmentManager = 1,
        /// <summary>
        /// 校区主任
        /// </summary>
        [Description("校区主任")]
        GeneralManager = 2,
    }
}
