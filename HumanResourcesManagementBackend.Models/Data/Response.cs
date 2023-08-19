using HumanResourcesManagementBackend.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models.Data
{
    /// <summary>
    /// 返回响应基类
    /// </summary>
    public class Response<T>
    {
        /// <summary>
        /// 数据部分
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 响应状态
        /// </summary>
        public ResponseStatus Status { get; set; }
        /// <summary>
        /// 响应描述
        /// </summary>
        public string Message { get; set; }
    }
}
