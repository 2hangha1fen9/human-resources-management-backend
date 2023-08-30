using HumanResourcesManagementBackend.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Services.Interface
{
    public interface IDepartmentService
    {
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        List<DepartmentDto.Department> GetDepartments(DepartmentDto.Search search);

        /// <summary>
        /// 根据ID获取部门名称
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        DepartmentDto.Department GetDepartmentById(long roleId);

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="role"></param>
        void AddDepartment(DepartmentDto.Save role);

        /// <summary>
        /// 编辑部门
        /// </summary>
        /// <param name="role"></param>
        void EditDepartment(DepartmentDto.Save role);

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="roleId"></param>
        void DeleteDepartment(long roleId);
    }
}
