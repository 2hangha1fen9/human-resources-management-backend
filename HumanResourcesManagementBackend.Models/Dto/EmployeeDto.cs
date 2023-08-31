using MiniExcelLibs.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            [ExcelIgnore]
            public long Id { get; set; }
            /// <summary>
            /// 数据状态 1启用 2禁用 99删除
            /// </summary>
            [ExcelIgnore]
            public DataStatus Status { get; set; }
            /// <summary>
            /// 数据状态 1启用 2禁用 99删除
            /// </summary>
            [ExcelIgnore]
            public string StatusStr { get; set; }
            /// <summary>
            /// 创建时间
            /// </summary>
            [ExcelIgnore]
            public DateTime CreateTime { get; set; }
            /// <summary>
            /// 更新时间
            /// </summary>
            [ExcelIgnore]
            public DateTime UpdateTime { get; set; }
            /// <summary>
            /// 工号
            /// </summary>
            [ExcelColumn(Name = "工号",Width = 20)]
            public string WorkNum { get; set; }
            /// <summary>
            /// 工作状态 (详见枚举定义)
            /// </summary>
            [ExcelIgnore]
            public WorkStatus WorkStatus { get; set; }
            /// <summary>
            /// 工作状态 (详见枚举定义)
            /// </summary>
            [ExcelColumn(Name = "工作状态",Width = 20)]
            public string WorkStatusStr { get; set; }
            /// <summary>
            /// 员工名称
            /// </summary>
            [ExcelColumn(Name = "员工名称",Width = 20)]
            public string Name { get; set; }
            /// <summary>
            /// 性别 0未知 1男 2女
            /// </summary>
            [ExcelIgnore]
            public Gender Gender { get; set; }
            /// <summary>
            /// 性别 0未知 1男 2女
            /// </summary>
            [ExcelColumn(Name = "性别", Width = 8)]
            public string GenderStr { get; set; }
            /// <summary>
            /// 婚姻状态 0 未知 1已婚 2未婚
            /// </summary>
            [ExcelIgnore]
            public MaritalStatus MaritalStatus { get; set; }
            /// <summary>
            /// 婚姻状态 0 未知 1已婚 2未婚
            /// </summary>
            [ExcelColumn(Name = "婚姻状态", Width = 15)]
            public string MaritalStatusStr { get; set; }
            /// <summary>
            /// 出生日期
            /// </summary>
            [ExcelColumn(Name = "出生日期", Width = 20)]
            public DateTime? BirthDay { get; set; }
            /// <summary>
            /// 身份证号
            /// </summary>
            [ExcelColumn(Name = "身份证号", Width = 30)]
            public string IdCard { get; set; }
            /// <summary>
            /// 籍贯
            /// </summary>
            [ExcelColumn(Name = "籍贯", Width = 15)]
            public string Native { get; set; }
            /// <summary>
            /// 手机号
            /// </summary>
            [ExcelColumn(Name = "手机号", Width = 20)]
            public string Phone { get; set; }
            /// <summary>
            /// 电子邮箱
            /// </summary>
            [ExcelColumn(Name = "电子邮箱", Width = 30)]
            public string Email { get; set; }
            /// <summary>
            /// 学历 （详见枚举定义）
            /// </summary>
            [ExcelIgnore]
            public AcademicDegree AcademicDegree { get; set; }
            /// <summary>
            /// 学历 （详见枚举定义）
            /// </summary>
            [ExcelColumn(Name = "学历", Width = 15)]
            public string AcademicDegreeStr { get; set; }
            /// <summary>`
            /// 入职日期
            /// </summary>
            [ExcelColumn(Name = "入职日期", Width = 20)]
            public DateTime HireDate { get; set; }
            /// <summary>
            /// 职位ID
            /// </summary>
            [ExcelIgnore]
            public long PositionId { get; set; }
            /// <summary>
            /// 职位名
            /// </summary>
            [ExcelColumn(Name = "职位名", Width = 15)]
            public string PositionName { get; set; }
            /// <summary>
            /// 部门ID
            /// </summary>
            [ExcelIgnore]
            public long DepartmentId { get; set; }
            /// <summary>
            /// 部门名称
            /// </summary>
            [ExcelColumn(Name = "部门名称", Width = 15)]
            public string DepartmentName { get; set; }
            /// <summary>
            /// 职位等级 
            /// </summary>
            [ExcelIgnore]
            public PositionLevel PositionLevel { get; set; }
            /// <summary>
            /// 职位等级 
            /// </summary>
            [ExcelColumn(Name = "职位等级", Width = 15)]
            public string PositionLevelStr { get; set; }
            /// <summary>
            /// 是否创建默认账号
            /// </summary>
            [ExcelIgnore]
            public bool CreateUser { get; set; } = false;
        }
        /// <summary>
        /// 员工编辑
        /// </summary>
        public class Edit
        {
            /// <summary>
            /// 姓名
            /// </summary>
            public string Name { get; set; }
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
            /// 婚姻状态 0 未知 1已婚 2未婚
            /// </summary>
            public MaritalStatus MaritalStatus { get; set; }
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
            /// <summary>`
            /// 入职日期
            /// </summary>
            public DateTime HireDate { get; set; }
            /// <summary>
            /// 性别
            /// </summary>
            public Gender Gender { get; set; }
            /// <summary>
            /// 出生日期
            /// </summary>
            public DateTime? BirthDay { get; set; }
            /// <summary>
            /// 数据状态 1启用 2禁用 99删除
            /// </summary>
            public DataStatus Status { get; set; }
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
            /// 工作状态 (详见枚举定义)
            /// </summary>
            public WorkStatus WorkStatus { get; set; }
            /// <summary>
            /// 性别 0未知 1男 2女
            /// </summary>
            public Gender Gender { get; set; }
            /// <summary>
            /// 婚姻状态 0 未知 1已婚 2未婚
            /// </summary>
            public MaritalStatus MaritalStatus { get; set; }
            /// <summary>
            /// 学历 （详见枚举定义）
            /// </summary>
            public AcademicDegree AcademicDegree { get; set; }
            /// <summary>
            /// 职位等级 
            /// </summary>
            public PositionLevel PositionLevel { get; set; }
            /// <summary>
            /// 职位ID
            /// </summary>
            public long PositionId { get; set; }
            /// <summary>
            /// 部门ID
            /// </summary>
            public long DepartmentId { get; set; }
            /// <summary>
            /// 搜索关键字
            /// </summary>
            public string SearchKey { get; set; }
            /// <summary>
            /// 员工id
            /// </summary>
            public long EmployeeId { get; set; }
        }
        /// <summary>
        /// 汇总
        /// </summary>
        public class Summary : DataResponse<dynamic>
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
        public class BirthdaySummary : DataResponse<dynamic>
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
