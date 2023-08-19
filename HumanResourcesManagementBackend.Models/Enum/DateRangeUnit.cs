using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models.Enum
{
    /// <summary>
    /// 时间跨度单位
    /// </summary>
    public enum DateRangeUnit
    {
        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        All = 0,
        /// <summary>
        /// 今天
        /// </summary>
        [Description("今天")]
        Today = 1,
        /// <summary>
        /// 本周
        /// </summary>
        [Description("本周")]
        ThisWeek = 2,
        /// <summary>
        /// 本月
        /// </summary>
        [Description("本月")]
        ThisMonth = 3,
        /// <summary>
        /// 全年
        /// </summary>
        [Description("全年")]
        ThisYear = 5
    }
}
