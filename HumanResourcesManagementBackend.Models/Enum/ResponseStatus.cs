using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models.Enum
{
    /// <summary>
    /// 响应状态
    /// </summary>
    public enum ResponseStatus
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 0,
        /// <summary>
        /// 无数据
        /// </summary>
        [Description("无数据")] 
        NoData = 2,
        /// <summary>
        /// 内部错误
        /// </summary>
        [Description("内部错误")]
        Error = 4,
        /// <summary>
        /// 参数错误
        /// </summary>
        [Description("参数错误")]
        ParameterError = 6,
        /// <summary>
        /// 添加操作错误
        /// </summary>
        [Description("添加操作错误")]
        AddError = 8,
        /// <summary>
        /// 更新操作错误
        /// </summary>
        [Description("更新操作错误")]
        UpdateError = 10,
        /// <summary>
        /// 删除操作错误
        /// </summary>
        [Description("删除操作错误")]
        DeleteError = 12,
        /// <summary>
        /// 无权限
        /// </summary>
        [Description("无权限")]
        NoPermission = 14,
    }
}
