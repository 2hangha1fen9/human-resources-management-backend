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
using HumanResourcesManagementBackend.Models.Dto;
using static HumanResourcesManagementBackend.Models.EmployeeDto;

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
        public PageResponse<SummaryDto> GetSenioritySummary()
        {
            var  getseniority= _employeeService.GetSenioritySummary();
            return new PageResponse<SummaryDto>()
            {
                RecordCount = _employeeService.TotalPeople(),
                Data = getseniority ?? throw new BusinessException
                {
                    Status = ResponseStatus.NoData
                }
            };
        }

        /// <summary>
        /// 查询职别汇总信息
        /// </summary>
        /// <param name="GetGradeSummary"></param>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<SummaryDto> GetGradeSummary()
        {
            var getgrade= _employeeService.GetGradeSummary();
            return new PageResponse<SummaryDto>()
            {
                RecordCount = _employeeService.TotalPeople(),
                Data = getgrade ?? throw new BusinessException
                {
                    Status = ResponseStatus.NoData
                }
            };
        }
        /// <summary>
        /// 查询年龄汇总信息
        /// </summary>
        /// <param name="GetAgeSummary"></param>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<SummaryDto> GetAgeSummary()
        {
            var getage = _employeeService.GetAgeSummary();
            return new PageResponse<SummaryDto>()
            {
                RecordCount = _employeeService.TotalPeople(),
                Data = getage ?? throw new BusinessException
                {
                    Status = ResponseStatus.NoData
                }
            };
        }
    }
}
