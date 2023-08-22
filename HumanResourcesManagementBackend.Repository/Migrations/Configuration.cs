namespace HumanResourcesManagementBackend.Repository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using HumanResourcesManagementBackend.Common;
    using HumanResourcesManagementBackend.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<HRM>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

        }

        /// <summary>
        /// 种子数据设置
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(HRM context)
        {
            //添加默认管理员用户
            var user = context.Users.FirstOrDefault(u => u.LoginName == "admin");
            if(user == null)
            {
                context.Users.Add(new R_User
                {
                    LoginName = "admin",
                    Password = "admin".Encrypt(),
                    Question = string.Empty,
                    Answer = string.Empty,
                    Status = DataStatus.Enable,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                });
            }
        }
    }
}
