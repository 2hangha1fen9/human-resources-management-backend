using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models
{
    /// <summary>
    /// 用户休假申请
    /// </summary>

    public class VacationApplyDto
    {
        /// <summary>
        /// 休假申请
        /// </summary>
        public class VacationApply
        {
            /// <summary>
            /// Id
            /// </summary>
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
            /// 休假开始时间
            /// </summary>
            public DateTime BeginDate { get; set; }
            /// <summary>
            /// 休假结束时间
            /// </summary>
            public DateTime EndDate { get; set; }
            /// <summary>
            /// 合计时长
            /// </summary>
            public string Duration { get; set; }
            /// <summary>
            /// 休假原因
            /// </summary>
            public string Reason { get; set; }
            /// <summary>
            /// 休假类型（详见枚举定义）
            /// </summary>
            public VacationType VacationType { get; set; }
            /// <summary>
            /// 休假类型（详见枚举定义）
            /// </summary>
            public string VacationTypeStr { get; set; }
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
            public string AuditTypeStr { get; set;}
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
        /// 查询休假记录
        /// </summary>
        public class Search:PageRequest
        {
            /// <summary>
            /// 员工ID
            /// </summary>
            public long EmployeeId { get; set; }
            /// <summary>
            /// 数据状态
            /// </summary>
            public DataStatus Status { get; set; }
            /// <summary>
            /// 休假类型（详见枚举定义）
            /// </summary>
            public VacationType VacationType { get; set; }
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
