using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models.Enum
{
    public enum MaritalStatus
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        None = 0,
        /// <summary>
        /// 已婚
        /// </summary>
        [Description("已婚")]
        Married = 1,
        /// <summary>
        /// 未婚
        /// </summary>
        [Description("未婚")]
        Single = 2,
    }
}
