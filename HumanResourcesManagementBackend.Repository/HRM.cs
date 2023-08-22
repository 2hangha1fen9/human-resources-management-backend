using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Repository
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public partial class HRM : DbContext
    {
        /// <summary>
        /// 调用父类的构造方法, 传入数据库连接字符串的名字作为参数
        /// </summary>
        public HRM() : base("DB") { } //在API项目的Web.config中配置了名为DB的连接字符串

        #region 数据库表
        /// <summary>
        /// 用户表
        /// </summary>
        public DbSet<R_User> Users { get; set; }
        /// <summary>
        /// 角色表
        /// </summary>
        public DbSet<R_Role> Roles { get; set; }
        /// <summary>
        /// 员工表
        /// </summary>
        public DbSet<R_Employee> Employees { get; set; }
        /// <summary>
        /// 权限表
        /// </summary>
        public DbSet<R_Permission> Permissions { get; set; }
        /// <summary>
        /// 权限角色关系表
        /// </summary>
        public DbSet<R_PermissionRoleRef> PermissionRoleRefs { get; set; }
        /// <summary>
        /// 用户角色关系表
        /// </summary>
        public DbSet<R_UserRoleRef> UserRoleRefs { get; set; }
        /// <summary>
        /// 休假申请表
        /// </summary>
        public DbSet<R_VacationApply> VacationApplies { get; set; }
        /// <summary>
        /// 缺勤申请表
        /// </summary>
        public DbSet<R_AbsenceApply> AbsenceApplies { get; set; }
        /// <summary>
        /// 调休申请表
        /// </summary>
        public DbSet<R_CompensatoryApply> CompensatoryApplies { get; set; }
        /// <summary>
        /// 外请申请表
        /// </summary>
        public DbSet<R_FieldWorkApply> FieldWorkApplies { get; set; }
        /// <summary>
        /// 出差申请表
        /// </summary>
        public DbSet<R_BusinessTripApply> BusinessTripApplies { get; set;}
        #endregion
    }
}
