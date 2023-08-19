using HumanResourcesManagementBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Repository
{
    /// <summary>
    /// 缺勤申请
    /// </summary>
    public class AbsenceApply : BaseEntity
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmployeeId { get; set; }
        /// <summary>
        /// 缺勤时间
        /// </summary>
        public DateTime AbsenceDateTime { get; set; }
        /// <summary>
        /// 打卡类型 1上班卡 2下班卡
        /// </summary>
        public CheckInType CheckInType { get; set; }
        /// <summary>
        /// 申请理由
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// 证明人
        /// </summary>
        public string Prover { get; set; }
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
