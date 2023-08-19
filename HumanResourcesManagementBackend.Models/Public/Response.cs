using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models
{
    /// <summary>
    /// 返回响应基类
    /// </summary>
    public class Response
    {
        /// <summary>
        /// 响应状态
        /// </summary>
        public ResponseStatus Status { get; set; }
        /// <summary>
        /// 响应描述
        /// </summary>
        public string Message { get; set; }
    }
    /// <summary>
    /// 返回对象响应基类
    /// </summary>
    public class DataResponse<T> : Response
    {
        /// <summary>
        /// 数据部分
        /// </summary>
        public T Data { get; set; }
        
    }
    /// <summary>
    /// 返回分页响应基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResponse<T> : Response
    {
        /// <summary>
        /// 列表数据
        /// </summary>
        public List<T> Data { get; set; }
        /// <summary>
        /// 总计录数
        /// </summary>
        public int RecordCount { get; set; }
    }
}
