using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models.Enum
{
    /// <summary>
    /// 审核状态
    /// </summary>
    public enum AuditStatus
    {
        /// <summary>
        /// 待审核
        /// </summary>
        [Description("待审核")]
        Pending = 1,
        /// <summary>
        /// 同意
        /// </summary>
        [Description("同意")]
        Agree = 2,
        /// <summary>
        /// 拒绝
        /// </summary>
        [Description("拒绝")]
        Reject = 3,
    }
}
