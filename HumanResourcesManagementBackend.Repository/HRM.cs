using HumanResourcesManagementBackend.Models.Entity;
using HumanResourcesManagementBackend.Repository.Entity;
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
        /// 调用父类的构造方法, 传入数据库连接字符串 的名字作为参数
        /// </summary>
        public HRM() : base("DB") //在API项目的Web.config中配置了名为DB的连接字符串
        {

        }

        #region 数据库表
        /// <summary>
        /// 用户表
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// 角色表
        /// </summary>
        public DbSet<Role> Roles { get; set; }
        #endregion
    }
}
