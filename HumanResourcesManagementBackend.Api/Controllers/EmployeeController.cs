using HumanResourcesManagementBackend.Api.Extensions;
using HumanResourcesManagementBackend.Models;
using HumanResourcesManagementBackend.Services.Interface;
using HumanResourcesManagementBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HumanResourcesManagementBackend.Common;

namespace HumanResourcesManagementBackend.Api.Controllers
{
    public class EmployeeController : BaseApiController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController()
        {
            _employeeService = new EmployeeService();
        }
        /// <summary>
        /// 查询员工列表
        /// </summary>
        /// <param name="seleemployee"></param>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<EmployeeDto.Employee> QueryUserByPage(EmployeeDto.Search search)
        {
            var employees =_employeeService.GetEmploysees(search);
            //返回响应结果
            return new PageResponse<EmployeeDto.Employee>()
            {
                RecordCount = search.RecordCount,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description(),
                Data = employees ?? throw new BusinessException
                {
                    Status = ResponseStatus.NoData
                }
            };
        }
        /// <summary>
        /// 根据id查询员工
        /// </summary>
        /// <param name="seleemployeebyid"></param>
        /// <returns></returns>
        [HttpPost]
        public DataResponse<EmployeeDto.Employee> GetEmployeeById(long id)
        {
            var employee = _employeeService.GetEmployseeById(id);
            return new DataResponse<EmployeeDto.Employee>
            {
                Data = employee,
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
        /// <summary>
        /// 添加员工
        /// </summary>
        /// <param name="employsee"></param>
        /// <returns></returns>
        [HttpPost]
        public Response AddEmploysee(EmployeeDto.Employee employee)
        {
            _employeeService.AddEmploysee(employee);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }

        /// <summary>
        /// 修改员工
        /// </summary>
        /// <param name="editemploysee"></param>
        /// <returns></returns>
        [HttpPut]
        public Response EditEmployee(EmployeeDto.Edit edit)
        {
            _employeeService.EditEmploysee(edit);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="deleemploysee"></param>
        /// <returns></returns>
        [HttpDelete]
        public Response DeleteEmploy(long id)
        {
            _employeeService.DeleEmploysee(id);
            return new Response
            {
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }

        /// <summary>
        /// 查询工龄汇总信息
        /// </summary>
        /// <param name="GetSenioritySummary"></param>
        /// <returns></returns>
        [HttpPost]
        public DataResponse<dynamic> GetSenioritySummary()
        {
            var  getseniority= _employeeService.GetSenioritySummary();
            int count = _employeeService.TotalPeople();
            return new DataResponse<dynamic>
            {
                Data = new { SeniorityCount = getseniority, Count = count },
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }

        /// <summary>
        /// 查询职别汇总信息
        /// </summary>
        /// <param name="GetGradeSummary"></param>
        /// <returns></returns>
        [HttpPost]
        public DataResponse<dynamic> GetGradeSummary()
        {
            var getgrade= _employeeService.GetGradeSummary();
            int count = _employeeService.TotalPeople();
            return new DataResponse<dynamic>
            {
                Data = new { GradeCount = getgrade, Count = count },
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
        /// <summary>
        /// 查询年龄汇总信息
        /// </summary>
        /// <param name="GetAgeSummary"></param>
        /// <returns></returns>
        [HttpPost]
        public DataResponse<dynamic> GetAgeSummary()
        {
            var getage = _employeeService.GetAgeSummary();
            int count = _employeeService.TotalPeople();
            return new DataResponse<dynamic>
            {
                Data = new { AgeConunt = getage, Count = count },
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
        /// <summary>
        /// 查询学历汇总信息
        /// </summary>
        /// <param name="GetEducationSummary"></param>
        /// <returns></returns>
        [HttpPost]
        public DataResponse<dynamic> GetEducationSummary()
        {
            var geteducation = _employeeService.GetEducationSummary();
            int count = _employeeService.TotalPeople();
            return new DataResponse<dynamic>
            {
                Data = new { EducationCount= geteducation, Count=count},               
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
        /// <summary>
        /// 查询部门汇总信息
        /// </summary>
        /// <param name="GetDepartmentSummary"></param>
        /// <returns></returns>
        [HttpPost]
        public DataResponse<dynamic> GetDepartmentSummary()
        {
            var getdepartment = _employeeService.GetDepartmentSummary();
            int count = _employeeService.TotalPeople();
            return new DataResponse<dynamic>
            {
                Data = new { DepartmentCount = getdepartment, Count = count },
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
        /// <summary>
        /// 查询性别汇总信息
        /// </summary>
        /// <param name="GetGenderSummary"></param>
        /// <returns></returns>
        [HttpPost]
        public DataResponse<dynamic> GetGenderSummary()
        {
            var getgender = _employeeService.GetGenderSummary();
            int count = _employeeService.TotalPeople();
            return new DataResponse<dynamic>
            {
                Data = new { GenderCount = getgender, Count = count },
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
        /// <summary>
        /// 查询婚姻汇总信息
        /// </summary>
        /// <param name="GetMaritalSummary"></param>
        /// <returns></returns>
        [HttpPost]
        public DataResponse<dynamic> GetMaritalSummary()
        {
            var getmarital = _employeeService.GetMaritalSummary();
            int count = _employeeService.TotalPeople();
            return new DataResponse<dynamic>
            {
                Data = new { MaritalCount = getmarital, Count = count },
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }
        /// <summary>
        /// 查询生日汇总信息
        /// </summary>
        /// <param name="GetBirthdaySummary"></param>
        /// <returns></returns>
        [HttpPost]
        public DataResponse<dynamic> GetBirthdaySummary()
        {
            var getbirthday = _employeeService.GetBirthdaySummary();
            int count = _employeeService.TotalPeople();
            return new DataResponse<dynamic>
            {
                Data = new { BirthdayCount = getbirthday, Count = count },
                Status = ResponseStatus.Success,
                Message = ResponseStatus.Success.Description()
            };
        }

        //#region 读取Excel数据
        ///// <summary>
        ///// 读取excel数据
        ///// </summary>
        ///// <param name="ReadExcel"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public Response ReadExcel(string fileName, string sheetName, bool isFirstRowColumn)
        //{
        //    _employeeService.ReadExcel(fileName,sheetName,isFirstRowColumn);
        //    return new Response
        //    {
        //        Status = ResponseStatus.Success,
        //        Message = ResponseStatus.Success.Description()
        //    };
        //}
        //#endregion
    }
}
