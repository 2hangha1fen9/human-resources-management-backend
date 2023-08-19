using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models
{
    /// <summary>
    /// 休假类型
    /// </summary>
    public enum VacationType
    {
        /// <summary>
        /// 病假
        /// </summary>
        [Description("病假")]
        SickLeave = 1,
        /// <summary>
        /// 事假
        /// </summary>
        [Description("事假")]
        PersonalLeave = 2, 
        /// <summary>
        /// 婚假
        /// </summary>
        [Description("婚假")]
        MarriageLeave = 3, 
        /// <summary>
        /// 工伤假
        /// </summary>
        [Description("工伤假")]
        WorkInjuryLeave = 4,
        /// <summary>
        /// 产检假
        /// </summary>
        [Description("产检假")]
        PrenatalCheckLeave = 5,
        /// <summary>
        /// 产假
        /// </summary>
        [Description("产假")]
        MaternityLeave = 6, 
        /// <summary>
        /// 丧假
        /// </summary>
        [Description("丧假")]
        BereavementLeave = 7
    }
}
