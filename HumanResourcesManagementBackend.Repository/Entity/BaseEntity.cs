using HumanResourcesManagementBackend.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models
{
    public class BaseEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 数据状态 1启用 2禁用 99删除
        /// </summary>
        public int Status { get; set; } = 1;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
