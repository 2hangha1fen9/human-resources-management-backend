using HumanResourcesManagementBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Repository.Entity
{
    /// <summary>
    /// 员工表
    /// </summary>
    public class Employee : BaseEntity
    {
        /// <summary>
        /// 工号
        /// </summary>
        public string WorkNum { get; set; }
        /// <summary>
        /// 工作状态 (详见枚举定义)
        /// </summary>
        public int WorkStatus { get; set; }
        /// <summary>
        /// 员工名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别 0未知 1男 2女
        /// </summary>
        public int Gender { get; set; } = 0;
        /// <summary>
        /// 婚姻状态 0 未知 1已婚 2未婚
        /// </summary>
        public int MaritalStatus { get; set; } = 0;
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
        public int AcademicDegree { get; set; } = 0;
        /// <summary>
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
        public int PositionLevel { get; set; } = 0;
    }
}
