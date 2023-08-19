using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models
{
    /// <summary>
    /// 请求基类
    /// </summary>
    public class Request
    {

    }

    /// <summary>
    /// 分页查询基类
    /// </summary>
    public class PageRequest
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageNum { get; set; } = 1;

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 页面大小
        /// </summary>
        public int Rows { get; set; } = 10;

        /// <summary>
        /// 总计录数
        /// </summary>

        public int RecordCount { get; set; }
    }
}
