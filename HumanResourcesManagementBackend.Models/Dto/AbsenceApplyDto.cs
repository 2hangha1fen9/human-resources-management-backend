using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models
{
    public class AbsenceApplyDto
    {
        /// <summary>
        /// 缺勤申请对象
        /// </summary>
        public class AbsenceApply
        {
            public long Id { get; set; }
            /// <summary>
            /// 数据状态 1启用 2禁用 99删除
            /// </summary>
            public DataStatus Status { get; set; }
            /// <summary>
            /// 数据状态 1启用 2禁用 99删除
            /// </summary>
            public string StatusStr { get; set; }
            /// <summary>
            /// 创建时间
            /// </summary>
            public DateTime CreateTime { get; set; }
            /// <summary>
            /// 更新时间
            /// </summary>
            public DateTime UpdateTime { get; set; }
            /// <summary>
            /// 员工ID
            /// </summary>
            public long EmployeeId { get; set; }
            /// <summary>
            /// 员工姓名
            /// </summary>
            public string EmployeeName { get; set; }
            /// <summary>
            /// 部门名称
            /// </summary>
            public string DepartmentName { get; set; }
            /// <summary>
            /// 缺勤时间
            /// </summary>
            public DateTime AbsenceDateTime { get; set; }
            /// <summary>
            /// 打卡类型 1上班卡 2下班卡
            /// </summary>
            public CheckInType CheckInType { get; set; }
            /// <summary>
            /// 打卡类型 1上班卡 2下班卡
            /// </summary>
            public string CheckInTypeStr { get; set; }
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
            public AuditStatus AuditStatus { get; set; }
            /// <summary>
            /// 审核状态 1待审核 2同意 3拒绝
            /// </summary>
            public string AuditStatusStr { get; set; }
            /// <summary>
            /// 审核类型 1 部门主管审批 2校区主任审批
            /// </summary>
            public AuditType AuditType { get; set; }
            /// <summary>
            /// 审核类型 1 部门主管审批 2校区主任审批
            /// </summary>
            public string AuditTypeStr { get; set; }
            /// <summary>
            /// 审核意见
            /// </summary>
            public string AuditResult { get; set; }
            /// <summary>
            /// 审核节点列表
            /// </summary>
            public string AuditNodeJson { get; set; }
            /// <summary>
            /// 审核节点列表
            /// </summary>
            public List<Examine> AuditNode { get; set; }
        }
        /// <summary>
        /// 缺勤查询对象
        /// </summary>
        public class Search : PageRequest
        {
            /// <summary>
            /// 员工ID
            /// </summary>
            public long EmployeeId { get; set; }
            /// <summary>
            /// 员工姓名
            /// </summary>
            public string EmployeeName { get; set; }
            /// <summary>
            /// 部门id
            /// </summary>
            public long DepartmentId { get; set; }
            /// <summary>
            /// 创建时间
            /// </summary>
            public DateTime CreateTime { get; set; }
            /// <summary>
            /// 打卡类型 1上班卡 2下班卡
            /// </summary>
            public CheckInType CheckInType { get; set; }
            /// <summary>
            /// 数据状态 1启用 2禁用 99删除
            /// </summary>
            public DataStatus Status { get; set; }
            /// <summary>
            /// 审核状态 1待审核 2同意 3拒绝
            /// </summary>
            public AuditStatus AuditStatus { get; set; }
            /// <summary>
            /// 审核类型 1 部门主管审批 2校区主任审批
            /// </summary>
            public AuditType AuditType { get; set; } 
        }
        /// <summary>
        /// 审核
        /// </summary>
        public class Examine
        {
            /// <summary>
            /// 申请Id
            /// </summary>
            public long Id { get; set; }
            /// <summary>
            /// 用户Id
            /// </summary>
            public long UserId { get; set; }
            /// <summary>
            /// 用户ID
            /// </summary>
            public string UserName { get; set; }
            /// <summary>
            /// 角色ID
            /// </summary>
            public long RoleId { get; set; }
            /// <summary>
            /// 角色名
            /// </summary>
            public string RoleName { get; set; }
            /// <summary>
            /// 审核状态 1待审核 2同意 3拒绝
            /// </summary>
            public AuditStatus AuditStatus { get; set; }
            /// <summary>
            /// 审核状态 1待审核 2同意 3拒绝
            /// </summary>
            public string AuditStatusStr { get; set; }
            /// <summary>
            /// 审核意见
            /// </summary>
            public string AuditResult { get; set; }
        }
    }
}
