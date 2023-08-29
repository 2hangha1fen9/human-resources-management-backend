using HumanResourcesManagementBackend.Models.Dto;
using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Repository;
using HumanResourcesManagementBackend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HumanResourcesManagementBackend.Common;

namespace HumanResourcesManagementBackend.Services
{
    public class DepartmentService : IDepartmentService
    {
        public void AddDepartment(DepartmentDto.Save department)
        {
            using (var db = new HRM())
            {
                //安全保障
                department.Id = 0;
                //查询是否存在
                var departmentEx = db.Departmentes.FirstOrDefault(r => r.DepartmentName == department.DepartmentName);
                if (departmentEx != null)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "部门已存在",
                        Status = ResponseStatus.ParameterError
                    };
                }
                //DTO映射为真正的实体，添加到数据库中
                var departmentR = department.MapTo<R_Department>();
                departmentR.CreateTime = DateTime.Now;
                departmentR.UpdateTime = DateTime.Now;
                db.Departmentes.Add(departmentR);

                if (db.SaveChanges() == 0)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "部门添加失败",
                        Status = ResponseStatus.AddError
                    };
                }
            }
        }

        public void DeleteDepartment(long departmentId)
        {
            using (var db = new HRM())
            {
                var department = db.Departmentes.FirstOrDefault(r => r.Id == departmentId) ?? throw new BusinessException
                {
                    ErrorMessage = "部门不存在",
                    Status = ResponseStatus.ParameterError
                };

                department.Status = DataStatus.Deleted;
                department.UpdateTime = DateTime.Now;
                db.SaveChanges();
                AuthService.permissionCache.Clear();
            }
        }

        public void EditDepartment(DepartmentDto.Save department)
        {
            using (var db = new HRM())
            {
                //查询是否存在
                var departmentEx = db.Departmentes.FirstOrDefault(u => u.Id == department.Id);
                if (departmentEx == null)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "部门不存在",
                        Status = ResponseStatus.ParameterError
                    };
                }
                //在Context里查询到对象才能这样赋值做修改操作，自己new是不行的
                departmentEx.DepartmentName = department.DepartmentName;
                departmentEx.UpdateTime = DateTime.Now;
                departmentEx.Status = department.Status;

                if (db.SaveChanges() == 0)
                {
                    throw new BusinessException
                    {
                        ErrorMessage = "部门修改失败",
                        Status = ResponseStatus.AddError
                    };
                }

                AuthService.permissionCache.Clear();
            }
        }

        public DepartmentDto.Department GetDepartmentById(long departmentId)
        {
            using (var db = new HRM())
            {
                var departmentR = db.Departmentes.FirstOrDefault(u => u.Id == departmentId && u.Status != DataStatus.Deleted);
                if (departmentR == null)
                {
                    throw new BusinessException
                    {
                        Status = ResponseStatus.NoData,
                        ErrorMessage = ResponseStatus.NoData.Description()
                    };
                }
                var department = departmentR.MapTo<DepartmentDto.Department>();
                department.StatusStr = department.Status.Description();
                return department;
            }
        }

        public List<DepartmentDto.Department> GetDepartments(DepartmentDto.Search search)
        {
            using (var db = new HRM())
            {
                var query = from department in db.Departmentes
                            where department.Status != DataStatus.Deleted
                            select department;
                if (!string.IsNullOrEmpty(search.DepartmentName))
                {
                    query = query.Where(r => r.DepartmentName.Contains(search.DepartmentName));
                }
                if (search.Status > 0)
                {
                    query = query.Where(r => r.Status == search.Status);
                }
                var list = query.OrderBy(q => q.Status).Pageing(search).MapToList<DepartmentDto.Department>();
                list.ForEach(r =>
                {
                    r.StatusStr = r.Status.Description();
                });
                return list;
            }
        }
    }
}
