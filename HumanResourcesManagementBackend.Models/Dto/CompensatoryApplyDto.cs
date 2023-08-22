using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models
{
    public class CompensatoryApplyDto
    {
        public class CompensatoryApply
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
            public DateTime CreateTime { get; set; }=DateTime.Now;
            /// <summary>
            /// 更新时间
            /// </summary>
            public DateTime UpdateTime { get; set; } = DateTime.Now;
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
        }
        public class Search:PageRequest
        {
            /// <summary>
            /// 员工ID
            /// </summary>
            public long EmployeeId { get; set; }
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
    }
}
