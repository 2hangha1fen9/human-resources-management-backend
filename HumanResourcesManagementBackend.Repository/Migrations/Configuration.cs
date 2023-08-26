namespace HumanResourcesManagementBackend.Repository.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using HumanResourcesManagementBackend.Common;
    using HumanResourcesManagementBackend.Models;
    using HumanResourcesManagementBackend.Repository.Extensions;
    using static HumanResourcesManagementBackend.Models.UserDto;

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
            context.Transaction(() =>
            {
                //添加默认管理员用户
                var user = context.Users.FirstOrDefault(u => u.LoginName == "admin");
                if (user == null)
                {
                    user = context.Users.Add(new R_User
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

                //添加默认角色
                var role = context.Roles.FirstOrDefault(r => r.Name == "超级管理员");
                if (role == null)
                {
                    role = context.Roles.Add(new R_Role
                    {
                        Name = "超级管理员",
                        Status = DataStatus.Enable,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                    });
                }

                //添加权限
                var permission1 = context.Permissions.FirstOrDefault(p => p.Name == "ALL_APIS");
                if(permission1 == null)
                {
                    permission1 = context.Permissions.Add(new R_Permission
                    {
                        Name = "ALL_APIS",
                        Type = PermissionType.Api,
                        IsPublic = YesOrNo.No,
                        Resource = "*",
                        Status = DataStatus.Enable,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                    });
                }
                var permission2 = context.Permissions.FirstOrDefault(p => p.Name == "ALL_MENUS");
                if (permission2 == null)
                {
                    permission2 = context.Permissions.Add(new R_Permission
                    {
                        Name = "ALL_MENUS",
                        Type = PermissionType.Menu,
                        IsPublic = YesOrNo.No,
                        Resource = "*",
                        Status = DataStatus.Enable,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                    });
                }
                var permission3 = context.Permissions.FirstOrDefault(p => p.Name == "ALL_BUTTONS");
                if (permission3 == null)
                {
                    permission3 = context.Permissions.Add(new R_Permission
                    {
                        Name = "ALL_BUTTONS",
                        Type = PermissionType.Button,
                        IsPublic = YesOrNo.No,
                        Resource = "*",
                        Status = DataStatus.Enable,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                    });
                }

                return true;
            });
        }
    }
}
