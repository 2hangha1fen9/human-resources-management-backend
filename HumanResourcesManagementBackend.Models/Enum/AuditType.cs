using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models.Enum
{
    /// <summary>
    /// 审核类型
    /// </summary>
    public enum AuditType
    {
        /// <summary>
        /// 部门主管审批
        /// </summary>
        [Description("部门主管审批")]
        DepartmentManager = 1,
        /// <summary>
        /// 校区主任审批
        /// </summary>
        [Description("校区主任审批")]
        GeneralManager = 2
    }
}
