using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Models.Dto;
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
        /// <summary>
        /// 查询总在职人数
        /// </summary>
        /// <param name="TotalPeople"></param>
        /// <returns></returns>
        int TotalPeople();
        /// <summary>
        /// 查询工龄汇总
        /// </summary>
        /// <param name="GetSenioritySummary"></param>
        /// <returns></returns>
        List<SummaryDto> GetSenioritySummary();
        /// <summary>
        /// 查询职别汇总
        /// </summary>
        /// <param name="GetSenioritySummary"></param>
        /// <returns></returns>
        List<SummaryDto> GetGradeSummary();
        /// <summary>
        /// 查询年龄汇总
        /// </summary>
        /// <param name="GetAgeSummary"></param>
        /// <returns></returns>
        List<SummaryDto> GetAgeSummary();
    }
}
