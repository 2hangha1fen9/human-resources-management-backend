using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Models
{
    public class EmployeeDto
    {
        /// <summary>
        /// 员工对象
        /// </summary>
        public class Employee
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
            /// 工号
            /// </summary>
            public string WorkNum { get; set; }
            /// <summary>
            /// 工作状态 (详见枚举定义)
            /// </summary>
            public WorkStatus WorkStatus { get; set; }
            /// <summary>
            /// 工作状态 (详见枚举定义)
            /// </summary>
            public string WorkStatusStr { get; set; }
            /// <summary>
            /// 员工名称
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 性别 0未知 1男 2女
            /// </summary>
            public Gender Gender { get; set; }
            /// <summary>
            /// 性别 0未知 1男 2女
            /// </summary>
            public string GenderStr { get; set; }
            /// <summary>
            /// 婚姻状态 0 未知 1已婚 2未婚
            /// </summary>
            public MaritalStatus MaritalStatus { get; set; }
            /// <summary>
            /// 婚姻状态 0 未知 1已婚 2未婚
            /// </summary>
            public string MaritalStatusStr { get; set; }
            /// <summary>
            /// 出生日期
            /// </summary>
            public DateTime BirthDay { get; set; }
            /// <summary>
            /// 身份证号
            /// </summary>
            public string IdCard { get; set; }
            /// <summary>
            /// 籍贯
            /// </summary>
            public string Native { get; set; }
            /// <summary>
            /// 手机号
            /// </summary>
            public string Phone { get; set; }
            /// <summary>
            /// 电子邮箱
            /// </summary>
            public string Email { get; set; }
            /// <summary>
            /// 学历 （详见枚举定义）
            /// </summary>
            public AcademicDegree AcademicDegree { get; set; }
            /// <summary>
            /// 学历 （详见枚举定义）
            /// </summary>
            public string AcademicDegreeStr { get; set; }
            /// <summary>`
            /// 入职日期
            /// </summary>
            public DateTime HireDate { get; set; }
            /// <summary>
            /// 职位ID
            /// </summary>
            public long PositionId { get; set; }
            /// <summary>
            /// 部门ID
            /// </summary>
            public long DepartmentId { get; set; }
            /// <summary>
            /// 职位等级 
            /// </summary>
            public PositionLevel PositionLevel { get; set; }
            /// <summary>
            /// 职位等级 
            /// </summary>
            public string PositionLevelStr { get; set; }
        }
        /// <summary>
        /// 员工编辑
        /// </summary>
        public class Edit
        {
            /// <summary>
            /// 工号
            /// </summary>
            public string WorkNum { get; set; }
            /// <summary>
            /// 工作状态 (详见枚举定义)
            /// </summary>
            public WorkStatus WorkStatus { get; set; }
            /// <summary>
            /// 身份证号
            /// </summary>
            public string IdCard { get; set; }
            /// <summary>
            /// 籍贯
            /// </summary>
            public string Native { get; set; }
            /// <summary>
            /// 手机号
            /// </summary>
            public string Phone { get; set; }
            /// <summary>
            /// 电子邮箱
            /// </summary>
            public string Email { get; set; }
            /// <summary>
            /// 学历 （详见枚举定义）
            /// </summary>
            public AcademicDegree AcademicDegree { get; set; }
            /// <summary>
            /// 职位ID
            /// </summary>
            public long PositionId { get; set; }
            /// <summary>
            /// 部门ID
            /// </summary>
            public long DepartmentId { get; set; }
            /// <summary>
            /// 职位等级 
            /// </summary>
            public PositionLevel PositionLevel { get; set; }
        }

        /// <summary>
        /// 员工查询
        /// </summary>
        public class Search:PageRequest
        {
            /// <summary>
            /// 数据状态 1启用 2禁用 99删除
            /// </summary>
            public DataStatus Status { get; set; }

            /// <summary>
            /// 工号
            /// </summary>
            public string WorkNum { get; set; }
            /// <summary>
            /// 员工名称
            /// </summary>
            public string Name { get; set; }
        }
        /// <summary>
        /// 汇总
        /// </summary>
        public class SummaryDto : DataResponse<dynamic>
        {
            /// <summary>
            /// 类别
            /// </summary>
            public string Category { get; set; }
            /// <summary>
            /// 人数
            /// </summary>
            public long Number { get; set; }
            /// <summary>
            /// 占比
            /// </summary>
            public string Proportion { get; set; }
        }
        /// <summary>
        /// 生日汇总
        /// </summary>
        public class BirthdaySummaryDto : DataResponse<dynamic>
        {
            /// <summary>
            /// 类别
            /// </summary>
            public string Category { get; set; }
            /// <summary>
            /// 人数
            /// </summary>
            public long Number { get; set; }
            /// <summary>
            /// 占比
            /// </summary>
            public string Proportion { get; set; }
            /// <summary>
            /// 生日月份
            /// </summary>
            public int BirthdayMonth { get; set; }
        }
    }
}
