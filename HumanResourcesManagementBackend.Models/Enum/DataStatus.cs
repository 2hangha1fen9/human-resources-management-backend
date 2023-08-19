using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models
{
    /// <summary>
    /// 数据状态枚举
    /// </summary>
    public enum DataStatus 
    {
        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")]
        Enable = 1,
        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Disable = 2,
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Deleted = 99
    }
}
