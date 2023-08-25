using HumanResourcesManagementBackend.Common;
using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Models.Dto;
using HumanResourcesManagementBackend.Repository;
using HumanResourcesManagementBackend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Services
{
    public class PermissionService : IPermissionService
    {
        public void AddPermission(PermissionDto.Save permission)
        {
            using (var db = new HRM())
            {
                //安全保障
                permission.Id = 0;
                //查询是否存在
                var permissionEx = db.Permissions.FirstOrDefault(r => r.Name == permission.Name && r.Type == permission.Type);
                if (permissionEx != null)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "权限已存在",
                        Status = ResponseStatus.ParameterError
                    };
                }
                //DTO映射为真正的实体，添加到数据库中
                var permissionR = permission.MapTo<R_Permission>();
                permissionR.CreateTime = DateTime.Now;
                permissionR.UpdateTime = DateTime.Now;
                db.Permissions.Add(permissionR);

                if (db.SaveChanges() == 0)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "权限添加失败",
                        Status = ResponseStatus.AddError
                    };
                }
            }
        }

        public void DeletePermission(long permissionId)
        {
            using (var db = new HRM())
            {
                var permission = db.Permissions.FirstOrDefault(r => r.Id == permissionId) ?? throw new BusinessException
                {
                    ErrorMessage = "权限不存在",
                    Status = ResponseStatus.ParameterError
                };

                permission.Status = DataStatus.Deleted;
                permission.UpdateTime = DateTime.Now;
                db.SaveChanges();
            }
        }

        public void EditPermission(PermissionDto.Save permission)
        {
            using (var db = new HRM())
            {
                //查询是否存在
                var permissionEx = db.Permissions.FirstOrDefault(u => u.Id == permission.Id);
                if (permissionEx == null)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "权限不存在",
                        Status = ResponseStatus.ParameterError
                    };
                }
                //在Context里查询到对象才能这样赋值做修改操作，自己new是不行的
                permissionEx.Name = permission.Name;
                permissionEx.Type = permission.Type;
                permission.IsPublic = permission.IsPublic;
                permission.Resource = permission.Resource;
                permissionEx.UpdateTime = DateTime.Now;
                permissionEx.Status = permission.Status;

                if (db.SaveChanges() == 0)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "权限修改失败",
                        Status = ResponseStatus.AddError
                    };
                }
            }
        }

        public List<PermissionDto.Permission> GetPermissions(PermissionDto.Search search)
        {
            using (var db = new HRM())
            {
                var query = from permission in db.Permissions
                            where permission.Status != DataStatus.Deleted
                            select permission;
                if (!string.IsNullOrEmpty(search.Name))
                {
                    query = query.Where(r => r.Name.Contains(search.Name));
                }
                if (!string.IsNullOrEmpty(search.Resource))
                {
                    query = query.Where(r => r.Resource.Contains(search.Resource));
                }
                if (search.Status > 0)
                {
                    query = query.Where(r => r.Status != DataStatus.Deleted);
                }
                if (search.Type > 0)
                {
                    query = query.Where(r => r.Type == search.Type);
                }
                if (search.IsPublic > 0)
                {
                    query = query.Where(r => r.IsPublic == search.IsPublic);
                }

                var list = query.OrderBy(q => q.Status).Pageing(search).MapToList<PermissionDto.Permission>();
                list.ForEach(r =>
                {
                    r.StatusStr = r.Status.Description();
                    r.TypeStr = r.Type.Description();
                    r.IsPublicStr = r.IsPublic.Description();
                });
                return list;
            }
        }
    }
}
