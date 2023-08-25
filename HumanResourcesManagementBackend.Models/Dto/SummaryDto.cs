using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models
{
    /// <summary>
    /// 汇总Dto
    /// </summary>
    public class SummaryDto:DataResponse<dynamic>
    {
        /// <summary>
        /// 类别
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public long Number { get; set; }
        /// <summary>
        /// 占比
        /// </summary>
        public string Proportion { get; set; }

    }
}
