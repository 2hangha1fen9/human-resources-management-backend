using HumanResourcesManagementBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesManagementBackend.Services.Interface
{
    public interface IEmployeeService
    {
        /// <summary>
        /// 获取员工列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<EmployeeDto.Employee> GetEmploysees(EmployeeDto.Search search);
        /// <summary>
        /// 根据id查员工
        /// </summary>
        /// <param name="searchid"></param>
        /// <returns></returns>
        EmployeeDto.Employee GetEmployseeById(long id);
        /// <summary>
        /// 添加员工
        /// </summary>
        void AddEmploysee(EmployeeDto.Employee employee);
        /// <summary>
        /// 修改员工
        /// </summary>
        /// <param name="editemploysee"></param>
        /// <returns></returns>
        void EditEmploysee(EmployeeDto.Edit edit);
        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="deleemploysee"></param>
        /// <returns></returns>
        void DeleEmploysee(long eid);
    }
}
