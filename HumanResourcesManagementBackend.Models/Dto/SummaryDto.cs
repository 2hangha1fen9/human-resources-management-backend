using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models.Dto
{
    /// <summary>
    /// 汇总Dto
    /// </summary>
    public class SummaryDto:PageRequest
    {
        /// <summary>
        /// 类别
        /// </summary>
        public string Category { get; set; } = "0.00%";
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
