using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models
{
    /// <summary>
    /// 打卡类型
    /// </summary>
    public enum CheckInType
    {
        /// <summary>
        /// 上班卡
        /// </summary>
        [Description("上班卡")]
        ClockIn = 1,
        /// <summary>
        /// 下班卡
        /// </summary>
        [Description("下班卡")]
        ClockOut = 2
    }
}
