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
        List<EmployeeDto.SummaryDto> GetSenioritySummary();
        /// <summary>
        /// 查询职别汇总
        /// </summary>
        /// <param name="GetSenioritySummary"></param>
        /// <returns></returns>
        List<EmployeeDto.SummaryDto> GetGradeSummary();
        /// <summary>
        /// 查询年龄汇总
        /// </summary>
        /// <param name="GetAgeSummary"></param>
        /// <returns></returns>
        List<EmployeeDto.SummaryDto> GetAgeSummary();
        /// <summary>
        /// 查询学历汇总信息
        /// </summary>
        /// <param name="GetEducationSummary"></param>
        /// <returns></returns>
        List<EmployeeDto.SummaryDto> GetEducationSummary();
        /// <summary>
        /// 查询部门汇总信息
        /// </summary>
        /// <param name="GetDepartmentSummary"></param>
        /// <returns></returns>
        List<EmployeeDto.SummaryDto> GetDepartmentSummary();
        /// <summary>
        /// 查询性别汇总信息
        /// </summary>
        /// <param name="GetGenderSummary"></param>
        /// <returns></returns>
        List<EmployeeDto.SummaryDto> GetGenderSummary();
        /// <summary>
        /// 查询婚姻汇总信息
        /// </summary>
        /// <param name="GetMaritalSummary"></param>
        /// <returns></returns>
        List<EmployeeDto.SummaryDto> GetMaritalSummary();
        /// <summary>
        /// 查询生日汇总信息
        /// </summary>
        /// <param name="GetBirthdaySummary"></param>
        /// <returns></returns>
        List<EmployeeDto.BirthdaySummaryDto> GetBirthdaySummary();
    }
}
