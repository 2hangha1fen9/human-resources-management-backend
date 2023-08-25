using HumanResourcesManagementBackend.Common;
using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Models.Dto;
using HumanResourcesManagementBackend.Repository;
using HumanResourcesManagementBackend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HumanResourcesManagementBackend.Services
{
    public class RoleService : IRoleService
    {
        public void AddRole(RoleDto.Save role)
        {
            using (var db = new HRM())
            {
                //安全保障
                role.Id = 0;
                //查询是否存在
                var roleEx = db.Roles.FirstOrDefault(r => r.Name == role.Name);
                if (roleEx != null)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "角色已存在",
                        Status = ResponseStatus.ParameterError
                    };
                }
                //DTO映射为真正的实体，添加到数据库中
                var roleR = role.MapTo<R_Role>();
                roleR.CreateTime = DateTime.Now;
                roleR.UpdateTime = DateTime.Now;
                db.Roles.Add(roleR);

                if (db.SaveChanges() == 0)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "角色添加失败",
                        Status = ResponseStatus.AddError
                    };
                }
            }
        }

        public void DeleteRole(long roleId)
        {
            using (var db = new HRM())
            {
                var role = db.Roles.FirstOrDefault(r => r.Id == roleId) ?? throw new BusinessException
                {
                    ErrorMessage = "角色不存在",
                    Status = ResponseStatus.ParameterError
                };

                role.Status = DataStatus.Deleted;
                role.UpdateTime = DateTime.Now;
                db.SaveChanges();
            }
        }

        public void EditRole(RoleDto.Save role)
        {
            using (var db = new HRM())
            {
                //查询是否存在
                var roleEx = db.Roles.FirstOrDefault(u => u.Id == role.Id);
                if (roleEx == null)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "角色不存在",
                        Status = ResponseStatus.ParameterError
                    };
                }
                //在Context里查询到对象才能这样赋值做修改操作，自己new是不行的
                roleEx.Name = role.Name;
                roleEx.UpdateTime = DateTime.Now;
                roleEx.Status = role.Status;

                if (db.SaveChanges() == 0)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "角色修改失败",
                        Status = ResponseStatus.AddError
                    };
                }
            }
        }

        public RoleDto.Role GetRoleById(long roleId)
        {
            using (var db = new HRM())
            {
                var roleR = db.Roles.FirstOrDefault(u => u.Id == roleId && u.Status != DataStatus.Deleted);
                if (roleR == null)
                {
                    throw new BusinessException
                    {
                        Status = ResponseStatus.NoData,
                        ErrorMessage = ResponseStatus.NoData.Description()
                    };
                }
                var role = roleR.MapTo<RoleDto.Role>();
                role.StatusStr = role.Status.Description();
                return role;
            }
        }

        public List<RoleDto.Role> GetRoles(RoleDto.Search search)
        {
            using(var db = new HRM())
            {
                var query = from role in db.Roles
                            where role.Status != DataStatus.Deleted
                            select role;
                if (!string.IsNullOrEmpty(search.Name))
                {
                    query = query.Where(r => r.Name.Contains(search.Name));
                }
                if(search.Status > 0)
                {
                    query = query.Where(r =>  r.Status != DataStatus.Deleted);
                }
                var list = query.OrderBy(q => q.Status).Pageing(search).MapToList<RoleDto.Role>();
                list.ForEach(r =>
                {
                    r.StatusStr = r.Status.Description();
                });
                return list;
            }
        }
    }
}
