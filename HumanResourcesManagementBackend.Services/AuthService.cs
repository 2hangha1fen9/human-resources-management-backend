using HumanResourcesManagementBackend.Common;
using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Models.Dto;
using HumanResourcesManagementBackend.Repository;
using HumanResourcesManagementBackend.Repository.Extensions;
using HumanResourcesManagementBackend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Services
{
    public class AuthService : IAuthService
    {
        private static readonly IPermissionService _permissionService = new PermissionService();
        //权限缓存
        public static readonly Dictionary<long, List<PermissionDto.Permission>> permissionCache = new Dictionary<long, List<PermissionDto.Permission>>();
        private static List<PermissionDto.Permission> publicPermissions = new List<PermissionDto.Permission>();
        public AuthService()
        {
            if(publicPermissions == null)
            {
                //初始化公共权限
                FlushPublicAuth();
            }
        }

        /// <summary>
        /// 刷新公共权限
        /// </summary>
        public static void FlushPublicAuth()
        {
            publicPermissions = _permissionService.GetPermissions(new PermissionDto.Search
            {
                IsPublic = YesOrNo.Yes,
                Rows = 0
            });
        }

        public void CheckApi(AuthDto.CheckApi check)
        {
            using(var db = new HRM())
            {
                //获取用户权限
                List<PermissionDto.Permission> permissionList = null;
                //有缓存查询缓存
                if(!permissionCache.TryGetValue(check.UserId,out permissionList))
                {
                    permissionList = _permissionService.GetPermissionsByUserId(check.UserId, PermissionType.Api);
                    permissionCache.Add(check.UserId, permissionList);
                }
                //添加公共权限
                permissionList.AddRange(publicPermissions);
                if(permissionList == null)
                {
                    throw new BusinessException();
                }
                //匹配权限
                var permission = permissionList.FirstOrDefault(p => string.Compare(p.Resource,check.Resource,true) == 0 || "*" == p.Resource);
                if(permission == null)
                {
                    throw new BusinessException();
                }

            }
        }

        public void RolePermissionBind(AuthDto.RolePermissionBind bind)
        {
            using (var db = new HRM())
            {
                db.Transaction(() =>
                {
                    //先删除所有已存在的绑定
                    var bindsEx = db.PermissionRoleRefs.Where(r => r.RoleId == bind.roleId).ToList();
                    db.PermissionRoleRefs.RemoveRange(bindsEx);
                    db.SaveChanges();
                    //更新绑定
                    var binds = bind.permissionIds.Select(permissionId => new R_PermissionRoleRef
                    {
                         RoleId = bind.roleId,
                         PermissionId = permissionId,
                         Status = DataStatus.Enable,
                         CreateTime = DateTime.Now,
                         UpdateTime = DateTime.Now,
                    }).ToList();
                    db.PermissionRoleRefs.AddRange(binds);
                    db.SaveChanges();

                    return true;
                });

                permissionCache.Clear();
            }
        }

        public void UserRoleBind(AuthDto.UserRoleBind bind)
        {
            using(var db = new HRM())
            {
                db.Transaction(() =>
                {
                    //先删除所有已存在的绑定
                    var bindsEx = db.UserRoleRefs.Where(r => r.UserId == bind.userId).ToList();
                    db.UserRoleRefs.RemoveRange(bindsEx);
                    db.SaveChanges();
                    //更新绑定
                    var binds = bind.roleIds.Select(roleId => new R_UserRoleRef
                    {
                        UserId = bind.userId,
                        RoleId = roleId,
                        Status = DataStatus.Enable,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                    }).ToList();
                    db.UserRoleRefs.AddRange(binds);
                    db.SaveChanges();

                    return true;
                });
                //刷新权限
                permissionCache.Remove(bind.userId);
            }
        }

        public void SyncPermission(List<PermissionDto.Save> syncList)
        {
            using(var db = new HRM())
            {
                db.Transaction(() =>
                {
                    //查询数据库所有权限
                    var savedPermissionList = db.Permissions.Where(i => i.Type == PermissionType.Api && i.Name != "ALL_APIS").ToList();
                    //待入库的权限
                    var batchSave = new List<R_Permission>();
                    //待删除的权限
                    var batchDelete = new List<R_Permission>();
                    //处理每个待同步的权限
                    foreach (var permission in syncList)
                    {
                        //查询数据库是否存在,不存在添加
                        var savedPermission = savedPermissionList.FirstOrDefault(p => p.Resource == permission.Resource);
                        if (savedPermission == null)
                        {
                            var save = permission.MapTo<R_Permission>();
                            save.CreateTime = DateTime.Now;
                            save.UpdateTime = DateTime.Now;
                            batchSave.Add(save);
                        }
                    }
                    foreach(var permission in savedPermissionList)
                    {
                        //查询同步列表是否存在，不存在删除
                        var asyncPermission = syncList.FirstOrDefault(p => p.Resource == permission.Resource);
                        if(asyncPermission == null)
                        {
                            batchDelete.Add(permission);
                        }
                    }
                    //开始数据操作
                    if(batchDelete.Count > 0)
                    {
                        db.Permissions.RemoveRange(batchDelete);
                    }
                    if(batchSave.Count > 0)
                    {
                        db.Permissions.AddRange(batchSave);
                    }
                    db.SaveChanges();

                    return true;
                });
                
            }
        }
    }
}
