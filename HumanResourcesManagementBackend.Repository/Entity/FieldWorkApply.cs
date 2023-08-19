using HumanResourcesManagementBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Repository
{
    /// <summary>
    /// 外请申请
    /// </summary>
    public class FieldWorkApply : BaseEntity
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmployeeId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 外勤地点
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 外勤事由
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// 审核状态 1待审核 2同意 3拒绝
        /// </summary>
        public AuditStatus AuditStatus { get; set; } = AuditStatus.Pending;
        /// <summary>
        /// 审核类型 1 部门主管审批 2校区主任审批
        /// </summary>
        public AuditType AuditType { get; set; }
    }
}
