using HumanResourcesManagementBackend.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Repository.Entity
{
    /// <summary>
    /// 休假申请
    /// </summary>
    public class VacationApply : BaseEntity
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmployeeId { get; set; }
        /// <summary>
        /// 休假开始时间
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 休假结束时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 休假原因
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// 休假类型（详见枚举定义）
        /// </summary>
        public int VacationType { get; set; }
        /// <summary>
        /// 审核状态 1待审核 2同意 3拒绝
        /// </summary>
        public int AuditStatus { get; set; } = 1;
        /// <summary>
        /// 审核类型 1 部门主管审批 2校区主任审批
        /// </summary>
        public int AuditType { get; set; }
    }
}
