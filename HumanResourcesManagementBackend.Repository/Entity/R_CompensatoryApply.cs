using HumanResourcesManagementBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Repository
{
    /// <summary>
    /// 调休申请
    /// </summary>
    public class R_CompensatoryApply : BaseEntity
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmployeeId { get; set; }
        /// <summary>
        /// 上班日期
        /// </summary>
        public DateTime WorkDate { get; set; }
        /// <summary>
        /// 倒休日期
        /// </summary>
        public DateTime RestDate { get; set; }
        /// <summary>
        /// 工作计划
        /// </summary>
        public string WorkPlan { get; set; }
        /// <summary>
        /// 审核状态 1待审核 2同意 3拒绝
        /// </summary>
        public AuditStatus AuditStatus { get; set; } = AuditStatus.Pending;
        /// <summary>
        /// 审核类型 1 部门主管审批 2校区主任审批
        /// </summary>
        public AuditType AuditType { get; set; } = AuditType.GeneralManager;
        /// <summary>
        /// 审核意见
        /// </summary>
        public string AuditResult { get; set; }
    }
}
