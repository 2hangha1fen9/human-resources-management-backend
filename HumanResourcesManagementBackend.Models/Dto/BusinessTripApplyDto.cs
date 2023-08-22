using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models
{
    public class BusinessTripApplyDto
    {
        public class BusinessTripApply
        {
            /// <summary>
            /// Id
            /// </summary>
            public long Id { get; set; }
            /// <summary>
            /// 数据状态 1启用 2禁用 99删除
            /// </summary>
            public DataStatus Status { get; set; } = DataStatus.Enable;
            /// <summary>
            /// 数据状态 1启用 2禁用 99删除
            /// </summary>
            public string StatusStr { get; set; }
            /// <summary>
            /// 创建时间
            /// </summary>
            public DateTime CreateTime { get; set; } = DateTime.Now;
            /// <summary>
            /// 更新时间
            /// </summary>
            public DateTime UpdateTime { get; set; } = DateTime.Now;
            /// <summary>
            /// 员工ID
            /// </summary>
            public long EmployeeId { get; set; }
            /// <summary>
            /// 出差目的地
            /// </summary>
            public string Address { get; set; }
            /// <summary>
            /// 出差事由
            /// </summary>
            public string Reason { get; set; }
            /// <summary>
            /// 出差归来结果
            /// </summary>
            public string Result { get; set; }
            /// <summary>
            /// 出差支持
            /// </summary>
            public string Support { get; set; }
            // <summary>
            /// 开始时间
            /// </summary>
            public DateTime BeginDate { get; set; }
            /// <summary>
            /// 结束时间
            /// </summary>
            public DateTime EndDate { get; set; }
            /// <summary>
            /// 审核状态 1待审核 2同意 3拒绝
            /// </summary>
            public AuditStatus AuditStatus { get; set; } = AuditStatus.Pending;
            /// <summary>
            /// 审核状态 1待审核 2同意 3拒绝
            /// </summary>
            public string AuditStatusStr { get; set; }
            /// <summary>
            /// 审核类型 1 部门主管审批 2校区主任审批
            /// </summary>
            public AuditType AuditType { get; set; } = AuditType.DepartmentManager;
            /// <summary>
            /// 审核类型 1 部门主管审批 2校区主任审批
            /// </summary>
            public string AuditTypeStr { get; set; }
        }
        public class Search : PageRequest
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
