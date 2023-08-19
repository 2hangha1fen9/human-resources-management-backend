using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models
{ 
    /// <summary>
    /// 工作状态枚举
    /// </summary>
    public enum WorkStatus
    {
        /// <summary>
        /// 在职
        /// </summary>
        [Description("在职")]
        Active = 1,
        /// <summary>
        /// 试用期
        /// </summary>
        [Description("试用期")]
        Probation = 2,
        /// <summary>
        /// 休假
        /// </summary>
        [Description("休假")]
        Vacation = 3,
        /// <summary>
        /// 病假
        /// </summary>
        [Description("病假")]
        SickLeave = 4,
        /// <summary>
        /// 离职
        /// </summary>
        [Description("离职")]
        Resignation = 5,
        /// <summary>
        /// 辞退
        /// </summary>
        [Description("辞退")]
        Termination = 6,
        /// <summary>
        /// 外派
        /// </summary>
        [Description("外派")]
        Secondment = 7,
        /// <summary>
        /// 转岗
        /// </summary>
        [Description("转岗")]
        Transfer = 8,
        /// <summary>
        /// 调休
        /// </summary>
        [Description("调休")]
        CompensatoryTimeOff = 9
    }
}
